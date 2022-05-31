using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScanFolders.Views;

namespace ScanFolders.Classes;
using Octokit;

public class UpdateCheck
{
    public static async Task<int> CheckGitHubNewerVersion()
    {
        GitHubClient client = new GitHubClient(new ProductHeaderValue("ScanFolders"));
        IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("Fris44", "ScanFolders");

        Version latestGitHubVersion = new Version(releases[0].TagName);
        Version localVersion = new Version("0.3.0");

        int versionComparison = localVersion.CompareTo(latestGitHubVersion);
        if (versionComparison < 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}