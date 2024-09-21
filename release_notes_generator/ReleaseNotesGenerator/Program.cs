using System.Text;
using System.Text.RegularExpressions;
using Octokit;

namespace ReleaseNotesGenerator;

public partial class ReleaseNoteGenerator
{
    private const string RepositoryOwner = "Studio-Lovelies";
    private const string RepositoryName = "GG-JointJustice-Unity";
    
    public static async Task Main()
    {
        var github = new GitHubClient(new ProductHeaderValue("StudioLoveliesReleaseNoteGenerator"))
        {
            Credentials = new Credentials(Environment.GetEnvironmentVariable("GITHUB_TOKEN"), AuthenticationType.Bearer)
        };

        var gitTagOfLastGitHubPrerelease = (await github.Repository.Release.GetAll(RepositoryOwner, RepositoryName))
            .Where(release => release.Prerelease)
            .OrderByDescending(release => release.CreatedAt)
            .First();
        var gitCommitHashOfLastGitHubPrerelease = (await github.Repository.Commit.Get(RepositoryOwner, RepositoryName, gitTagOfLastGitHubPrerelease.TagName));
        var currentCommitOnDevelop = await github.Repository.Commit.Get(RepositoryOwner, RepositoryName, "develop");

        var commitsSinceLastPrerelease = (await github.Repository.Commit.Compare(RepositoryOwner, RepositoryName, gitCommitHashOfLastGitHubPrerelease.Sha, currentCommitOnDevelop.Sha)).Commits;

        var commitMessages = commitsSinceLastPrerelease.Select(commit => commit.Commit.Message);
        await File.WriteAllTextAsync("changelog.md", ParseLines(commitMessages));
    }

    public static string ParseLines(IEnumerable<string> commitMessages)
    {
        var conventionalCommits = commitMessages.Select(commitMessage => ConventionalCommit().Match(commitMessage)).Where(match => match.Success).ToList();
        
        if (conventionalCommits.Count == 0)
        {
            return "No player-facing changes since last prerelease";
        }
        
        var commitsByTypeAndScopeWithBreakingChangePrefix = conventionalCommits
            .GroupBy(match => match.Groups["type"].Value)
            .ToDictionary(typeGroup => typeGroup.Key, typeGroup => typeGroup
                .GroupBy(match => match.Groups["scope"].Value)
                .ToDictionary(scopeGroup => scopeGroup.Key, scopeGroup => scopeGroup
                    .Select(match => match.Groups["breaking"].Value == "!" || match.Groups["breaking2"].Value == "!" ? "**BREAKING CHANGE**: " + match.Groups["message"].Value : match.Groups["message"].Value)
                    .ToList()
                )
            );

        var markdownBuilder = new StringBuilder();
        foreach (var (type, scopes) in commitsByTypeAndScopeWithBreakingChangePrefix)
        {
            var displayType = type switch
            {
                "feat" => "New Features",
                "fix" => "Bug Fixes",
                _ => throw new NotSupportedException("Only `feat` and `fix` are supported")
            };
            markdownBuilder.AppendLine($"# {displayType}{Environment.NewLine}");
            foreach (var (scope, messages) in scopes.Where(scope => scope.Key != ""))
            {
                markdownBuilder.AppendLine($"## {scope}{Environment.NewLine}");
                foreach (var message in messages)
                {
                    markdownBuilder.AppendLine($"- {message}");
                }
            }

            var otherCommits = scopes.Where(scope => scope.Key == "").SelectMany(scope => scope.Value).ToList();
            if (otherCommits.Count == 0)
            {
                markdownBuilder.AppendLine();
                continue;
            }

            markdownBuilder.AppendLine();
            markdownBuilder.AppendLine($"## Other{Environment.NewLine}");
            foreach (var message in otherCommits)
            {
                markdownBuilder.AppendLine($"- {message}");
            }

            markdownBuilder.AppendLine();
        }

        return markdownBuilder.ToString().Trim();
    }

    [GeneratedRegex(@"(?<type>feat|fix)(?<breaking2>!)?(?:\((?<scope>.*)\))?(?<breaking>!)?: (?<message>.*)")]
    private static partial Regex ConventionalCommit();
}