using api.git.server.Interfaces;
using System.Diagnostics;

namespace api.git.server.Operations
{
    public class AccountOperation : IAccountOperation
    {
        public ILogger<AccountOperation> Logger { get; set; }

        public AccountOperation(ILogger<AccountOperation> logger)
        {
            Logger = logger;
        }

        public bool Create(string accountName)
        {
            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "/bin/sh",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            });

            if (process == null)
                return false;

            using (var sw = process.StandardInput)
            {
                if(sw.BaseStream.CanWrite)
                {
                    sw.WriteLine(string.Format("useradd -ms /bin/sh {0}", accountName));
                    sw.WriteLine(string.Format("cd /home/{0}", accountName));
                    sw.WriteLine(string.Format("mkdir .ssh && chmod 700 .ssh", accountName));
                    sw.WriteLine("touch .ssh/authorized_keys && chmod 600 .ssh/authorized_keys");
                    sw.WriteLine(string.Format("chown -R {0}:{0} /home/{0}", accountName));
                }
            }

            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            Logger.LogInformation($"try to create account {accountName}, ExitCode: {process.ExitCode}, output: {output}, error {error}");

            return process.ExitCode.Equals(0);
        }
    }
}
