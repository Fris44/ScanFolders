using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace ScanFolders.Classes;

public class UpdateCheck
{
    public static async Task<int> CheckGitHubNewerVersion()
    {
        var client = new GitHubClient(new ProductHeaderValue("ScanFolders"));
        IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("Fris44", "ScanFolders");

        var latestGitHubVersion = new Version(releases[0].TagName);
        var localVersion = new Version("1.0.0");

        var versionComparison = localVersion.CompareTo(latestGitHubVersion);
        if (versionComparison < 0)
            return 1;
        return 0;
    }
}