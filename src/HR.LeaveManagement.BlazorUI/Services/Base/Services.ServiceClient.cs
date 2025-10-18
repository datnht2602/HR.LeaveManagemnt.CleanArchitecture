namespace HR.LeaveManagement.BlazorUI.Services.Base;

public partial class ServiceClient : IServiceClient
{
    public HttpClient HttpClient
    {
        get
        {
            return _httpClient;
        }
    }
}