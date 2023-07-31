using Microsoft.Extensions.Options;
using SuperPanel.Infrastructure;
using SuperPanel.Repository;
using SuperPanel.Repository.Shared;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SuperPanel.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task Query_Page_Get_RecordPerPapge()
        {
            var r = new UserRepository(Options.Create<DataOptions>(new DataOptions()
            {
                JsonFilePath = "./../../../../data/users.json"
            }), new EntityMapper());

            var records = await r.QueryAll(1, 15);

            Assert.Equal(15, records.Data.Count());
        }

        [Fact]
        public async Task Delete_Get_DeleteUserResponse()
        {
            var r = new UserRepository(Options.Create<DataOptions>(new DataOptions()
            {
                JsonFilePath = "./../../../../data/users.json"
            }), new EntityMapper());

            var response = await r.Delete(13675);

            Assert.True(response);
        }
    }
}
