using api.git.server.Interfaces;
using api.git.server.Models.GitRepository;
using Microsoft.AspNetCore.Mvc;

namespace api.git.server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GitRepositoryController : ControllerBase
    {
        private IGitRepositoryOperation GitRepositoryOperation { get; set; }

        public GitRepositoryController(IGitRepositoryOperation gitRepositoryOperation)
        {
            GitRepositoryOperation = gitRepositoryOperation;
        }

        [HttpPost]
        public HttpResponseMessage Create(GitRepositoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.AccountName))
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            var status = GitRepositoryOperation.Create(model.AccountName, model.Name);

            return new HttpResponseMessage(status ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.BadRequest);
        }
    }
}