using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SuperPanel.Dto.EntityDto;
using SuperPanel.Dto.Pagination;
using SuperPanel.Repository.Interfaces;
using SuperPanel.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace SuperPanel.Service
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IExternalApiService _externalApiService;

        #region Constructor
        public UserService(IConfiguration configuration,
                           ILogger<UserService> logger,
                           IUserRepository userRepository,
                           IExternalApiService externalApiService)
        {
            _configuration = configuration;
            _logger = logger;
            _userRepository = userRepository;
            _externalApiService = externalApiService;
        }
        #endregion

        public async Task<PaginationResponse<UserDto>> GetAll(int pageNumber)
        {
            try
            {
                var recordsPerPage = Convert.ToInt32(_configuration["RecordsPerPage"] ?? "10");
                return await _userRepository.QueryAll(pageNumber, recordsPerPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<PaginationResponse<UserDto>> DeleteUser(int userId)
        {
            try
            {
                var response = new PaginationResponse<UserDto>();
                if (userId > 0)
                {
                    var user = await _externalApiService.FindById(userId);
                    if (!user.IsError && !string.IsNullOrEmpty(user.Data))
                    {
                        var anomalized = await _externalApiService.AnomalizeUser(userId);
                        if (!anomalized.IsError && !string.IsNullOrEmpty(anomalized.Data))
                        {
                            if (await _userRepository.Delete(userId))
                            {
                                response = await GetAll(1);
                            }
                        }
                        else
                        {
                            response.ErrorMessage = anomalized.Message;
                        }
                    }
                    else
                    {
                        response.ErrorMessage = user.Message;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
