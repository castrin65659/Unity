using System.Collections.Generic;

namespace GitHub.Unity.IO
{
    static class ProcessManagerExtensions
    {
        public static IEnumerable<GitBranch> GetGitBranches(this ProcessManager processManager, string testGitRepoPath)
        {
            var processor = new BranchListOutputProcessor();
            var gitBranches = new List<GitBranch>();
            processor.OnBranch += data => gitBranches.Add(data);

            var process = processManager.Configure("git", "branch -vv", testGitRepoPath);
            var outputManager = new ProcessOutputManager(process, processor);

            process.Run();
            process.WaitForExit();

            return gitBranches;
        }
    }
}