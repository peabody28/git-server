namespace api.git.server.Constants
{
    public class DefaultConstants
    {
        public const string GitRepositoryFolderPostfix = ".git";

        public const string GitProgramName = "git";

        public const string GitUserName = "git";

        public const string GitUserHomeDirectory = "/home/git";

        /// <summary>
        /// Repository name for substitution required
        /// </summary>
        public const string GitInitBareCommandArgumentsFormat = "init --bare {0}";
    }
}