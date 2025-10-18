using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected readonly IServiceClient ServiceClient;
    protected readonly ILocalStorageService LocalStorageService;

    protected BaseHttpService(IServiceClient serviceClient, ILocalStorageService localStorageService)
    {
        ServiceClient = serviceClient;
        LocalStorageService = localStorageService;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new Response<Guid>
            {
                Message = "Invalid data was submitted.",
                Success = false,
                ValidationErrors = ex.Response
            };
        }else if (ex.StatusCode == 404)
        {
            return new Response<Guid>
            {
                Message = "The record was not found.",
                Success = false,
            };
        }
        else
        {
            return new Response<Guid>
            {
                Message = "Something went wrong, please retry again later..",
                Success = false
            };
        }
    }
    
}