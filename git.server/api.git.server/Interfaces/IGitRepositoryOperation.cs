namespace api.git.server.Interfaces
{
    public interface IGitRepositoryOperation
    {
        bool Create(string accountName, string name);
    }
}
