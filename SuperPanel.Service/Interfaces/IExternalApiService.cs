using SuperPanel.Dto.ExternalApi;
using System.Threading.Tasks;

namespace SuperPanel.Service.Interfaces
{
    public interface IExternalApiService
    {
        Task<ExternalApiResponse> FindById(int userId);
        Task<ExternalApiResponse> AnomalizeUser(int userId);
    }
}
