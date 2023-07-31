namespace SuperPanel.Dto.ExternalApi
{
    public class ExternalApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public bool IsError { get; set; }
    }
}
