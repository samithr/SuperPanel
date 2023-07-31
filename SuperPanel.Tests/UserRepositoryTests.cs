using Microsoft.Extensions.Options;
using SuperPanel.Infrastructure;
using SuperPanel.Repository;
using SuperPanel.Repository.Interfaces;
using SuperPanel.Repository.Shared;
using Xunit;

namespace SuperPanel.Tests
{
    public class UserRepositoryTests
    {
        //[Fact]
        //public async Task QueryAll_ShouldReturnEverything()
        //{
        //    var r = new UserRepository(Options.Create<DataOptions>(new DataOptions()
        //    {
        //        JsonFilePath = "./../../../../data/users.json"
        //    }), new EntityMapper());

        //    var all = await r.QueryAll(1,15);

        //    Assert.Equal(5000, all.Count());
        //}
    }
}
