using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DeepShiShared
{
    public class ApiHelper
    {
        private readonly ISession _session;
        public HttpClient ApiClient { get; }
        public ApiHelper(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _session.UserAuthToken());
            ApiClient = httpClient;
        }


    }
}
