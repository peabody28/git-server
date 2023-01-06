using api.git.server.Interfaces;
using api.git.server.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.git.server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        public IAccountOperation AccountOperation { get; set; }

        public AccountController(IConfiguration configuration, IAccountOperation accountOperation)
        {
            Configuration = configuration;
            AccountOperation = accountOperation;
        }

        [HttpPost]
        public HttpResponseMessage Create(AccountModel model)
        {
            if(string.IsNullOrWhiteSpace(model.Name))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var status = AccountOperation.Create(model.Name);

            return new HttpResponseMessage(status ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public HttpResponseMessage AddSshKey(SshKeyModel model)
        {
            var authorizedKeysFile = Path.Combine($"/home/{model.AccountName}/.ssh/authorized_keys");
            using (StreamWriter writer = new StreamWriter(authorizedKeysFile, true))
            {
                writer.WriteLine(model.Key);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
