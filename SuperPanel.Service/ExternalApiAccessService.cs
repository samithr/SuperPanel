using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuperPanel.Dto.ExternalApi;
using SuperPanel.Service.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperPanel.Service
{
    public class ExternalApiAccessService : IExternalApiService
    {
        private readonly ILogger<ExternalApiAccessService> _logger;
        private readonly IConfiguration _configuration;
        private string _externalApiBaseUri;

        public ExternalApiAccessService(ILogger<ExternalApiAccessService> logger,
                                        IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _externalApiBaseUri = _configuration["ExternalApiBaseUrl"];
        }

        public async Task<ExternalApiResponse> FindById(int userId)
        {
            try
            {
                var response = new ExternalApiResponse();
                if (userId > 0)
                {
                    var url = $"{_externalApiBaseUri}/v1/contacts/{userId}";
                    var httpContent = new StringContent("", Encoding.UTF8, "application/json");
                    using var client = new HttpClient();
                    using HttpResponseMessage result = await client.GetAsync(url);

                    var responseString = await result.Content.ReadAsStringAsync();
                    response = await ProcessApiResponse(result.StatusCode, responseString, response);
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<ExternalApiResponse> AnomalizeUser(int userId)
        {
            try
            {
                var response = new ExternalApiResponse();
                if (userId > 0)
                {
                    var url = $"{_externalApiBaseUri}/v1/contacts/{userId}/gdpr";
                    var httpContent = new StringContent("", Encoding.UTF8, "application/json");
                    using var client = new HttpClient();
                    using HttpResponseMessage result = await client.PutAsync(url, httpContent);

                    var responseString = await result.Content.ReadAsStringAsync();
                    response = await ProcessApiResponse(result.StatusCode, responseString, response);
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task<ExternalApiResponse> ProcessApiResponse(HttpStatusCode statusCode, string responseString, ExternalApiResponse response)
        {
            try
            {
                if (statusCode == HttpStatusCode.OK)
                {
                    var resultString = JsonConvert.DeserializeObject(responseString);
                    response.StatusCode = (int)statusCode;
                    response.Message = "Success";
                    response.Data = resultString.ToString();
                }
                else if (statusCode == HttpStatusCode.NotFound)
                {
                    response.StatusCode = (int)statusCode;
                    response.Message = "User not availabe!";
                    response.IsError = true;
                    response.Data = null;
                }
                else
                {
                    response.StatusCode = (int)statusCode;
                    response.Message = "Error occured on external API!";
                    response.IsError = true;
                    response.Data = null;
                }
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
