using SuperPanel.Dto.EntityDto;
using SuperPanel.Dto.Pagination;
using System.Threading.Tasks;

namespace SuperPanel.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<PaginationResponse<UserDto>> QueryAll(int page, int recordCount);
        Task<bool> Delete(int userId);
    }
}
