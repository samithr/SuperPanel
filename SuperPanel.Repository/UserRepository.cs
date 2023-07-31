using Microsoft.Extensions.Options;
using SuperPanel.DAL.Entities;
using SuperPanel.Dto.EntityDto;
using SuperPanel.Dto.Pagination;
using SuperPanel.Infrastructure;
using SuperPanel.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuperPanel.Repository
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;
        private readonly IEntityMapper _entityMapper;

        public UserRepository(IOptions<DataOptions> dataOptions,
                              IEntityMapper entityMapper)
        {
            // preload the set of users from file.
            var json = File.ReadAllText(dataOptions.Value.JsonFilePath);
            _users = JsonSerializer.Deserialize<IEnumerable<User>>(json).ToList();
            _entityMapper = entityMapper;
        }

        public async Task<PaginationResponse<UserDto>> QueryAll(int page, int recordCount)
        {
            try
            {
                page = page < 1 ? 1 : page;
                var totalRecordsCount = _users.Count;
                int totalPages = Convert.ToInt32(Math.Ceiling(totalRecordsCount / (recordCount * 1.0)));
                var userData = await Task.FromResult(_users.Skip((page - 1) * recordCount)
                                                           .Take(recordCount));
                var data = _entityMapper.Map<List<User>, List<UserDto>>(userData.ToList());
                return new PaginationResponse<UserDto>
                {
                    CurrentPage = page,
                    IsLastPage = page * recordCount < totalRecordsCount,
                    TotalPage = totalPages,
                    Data = data
                };

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int userId)
        {
            try
            {
                return await Task.FromResult(_users.Remove(_users.Single(o => o.Id == userId)));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
