using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using ClientPoC.Services;

namespace ClientPoC.Services.HubData
{
    public class BaseHubDataService : Service, IDisposable
    {
        private readonly string _username;
        private readonly string _authToken;

        public BaseHubDataService(string serviceRequestUrl, string useranme, string authToken) : base(Constants.Constants.ServerBaseUrl, serviceRequestUrl)
        {
            this._username = useranme;
            this._authToken = authToken;
            _serviceRequestUrl = serviceRequestUrl;
        }

        protected override HttpClient SetupHttpClient(string serviceBaseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(serviceBaseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
            _httpClient.DefaultRequestHeaders.Add("Device-ID", _username);
            _httpClient.DefaultRequestHeaders.Add("Device-Type", "ipad");
            _httpClient.DefaultRequestHeaders.Add("App-Context", "clinician");
            return _httpClient;
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }
    }
}