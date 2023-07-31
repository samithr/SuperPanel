using SuperPanel.Dto.EntityDto;
using SuperPanel.Dto.Pagination;
using System.Threading.Tasks;

namespace SuperPanel.Service.Interfaces
{
    public interface IUserService
    {
        Task<PaginationResponse<UserDto>> GetAll(int pageNumber);
        Task<PaginationResponse<UserDto>> DeleteUser(int userId);
    }
}
