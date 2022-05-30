using System;
using System.Collections.Generic;

namespace ScanFolders.Classes;
using Octokit;

public class UpdateCheck
{
    public static async System.Threading.Tasks.Task CheckGitHubNewerVersion()
    {
        GitHubClient client = new GitHubClient(new ProductHeaderValue("ScanFolders"));
        IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("Fris44", "ScanFolders");

        Version latestGitHubVersion = new Version(releases[0].TagName);
        Version localVersion = new Version("0.3.0");

        int versionComparison = localVersion.CompareTo(latestGitHubVersion);
        if (versionComparison < 0)
        {
            
        }
    }
}