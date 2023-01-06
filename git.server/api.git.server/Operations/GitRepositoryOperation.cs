using api.git.server.Constants;
using api.git.server.Interfaces;
using System.Diagnostics;

namespace api.git.server.Operations
{
    public class GitRepositoryOperation : IGitRepositoryOperation
    {
        public IConfiguration Configuration { get; set; }

        public ILogger<GitRepositoryOperation> Logger { get; set; }

        public GitRepositoryOperation(IConfiguration configuration, ILogger<GitRepositoryOperation> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        public bool Create(string accountName, string name)
        {
            var fullRepName = string.Concat(name, DefaultConstants.GitRepositoryFolderPostfix);

            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = DefaultConstants.GitProgramName,
                Arguments = string.Format(DefaultConstants.GitInitBareCommandArgumentsFormat, fullRepName),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = string.Format("/home/{0}", accountName),
                UserName = accountName,
            });
            
            if (process == null)
                return false;
            
            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();

            Logger.LogInformation($"try to create repository {name} of user {accountName}, ExitCode: {process.ExitCode}, output: {output}, error {error}");

            return process.ExitCode.Equals(0);
        }
    }
}
