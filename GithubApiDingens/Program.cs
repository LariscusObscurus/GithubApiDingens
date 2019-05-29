using Octokit;
using System;
using System.Linq;

namespace GithubApiDingens
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("GithubApiDingens"));
            var basicAuth = new Credentials("<UserName>", "<Password>");
            
            client.Credentials = basicAuth;
            var myRepos = await client.Repository.GetAllForCurrent();
            foreach (var repository in myRepos)
            {
                var request = new RepositoryTrafficRequest(TrafficDayOrWeek.Week);
                var views = await client.Repository.Traffic.GetViews(repository.Id, request);
                var clones = await client.Repository.Traffic.GetClones(repository.Id, request);
                Console.WriteLine($"{repository.Owner.Login}/{repository.Name} has {views.Count} views and {clones.Count} clones.");
            }

        }
    }
}
