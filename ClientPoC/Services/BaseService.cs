using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using ClientPoC.Constants;

namespace ClientPoC.Services
{
    public class Service
    {
        private bool _disposed = false;
        private int _timeOutInterval;
        protected readonly string _serviceBaseAddress;
        protected string _serviceRequestUrl;
        private readonly string jsonMediaType = "application/json";
        protected HttpClient _httpClient;

        public Service(string serviceBaseAddress,string serviceRequestUrl)
        {
            _serviceBaseAddress = serviceBaseAddress;
            _serviceRequestUrl = serviceRequestUrl;
        }

        protected virtual HttpClient SetupHttpClient(string serviceBaseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(serviceBaseAddress);
            return _httpClient;
        }

        public Task<T> GetAsync<T>(string serviceRequestUrl)
        {
            var client = SetupHttpClient(_serviceBaseAddress);
            
            try
            {
                var responseMessage = client.GetAsync(serviceRequestUrl).Result;
                responseMessage.EnsureSuccessStatusCode();
                return responseMessage.Content.ReadAsAsync<T>();
            }
            finally
            {
                client.Dispose();
            }
            
        }

        public Task<string> GetAsyncAsString(string serverRequestUrl)
        {
            var client = SetupHttpClient(_serviceBaseAddress);

            try
            {
                var responseMessage = client.GetAsync(serverRequestUrl).Result;
                responseMessage.EnsureSuccessStatusCode();
                return responseMessage.Content.ReadAsStringAsync();
            }
            finally
            {
                client.Dispose();
            }

        }
        public async Task<HttpResponseMessage> Post<T>(string serviceBaseAddress, string serviceRequestUrl, T objectToPost)
        {
            var client = SetupHttpClient(serviceBaseAddress);
            try
            {
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(serviceRequestUrl, objectToPost);
                return responseMessage;
            }
            finally
            {
                client.Dispose();
            }
            
        }

        public async Task<TReturn> PostAsync<T, TReturn>(string serviceBaseAddress, string serviceRequestUrl, T objectToPost)
         {
            var client = SetupHttpClient(serviceBaseAddress);
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseMessage = await client.PostAsJsonAsync(serviceRequestUrl, objectToPost);
                return await responseMessage.Content.ReadAsAsync<TReturn>();
            }
            finally
            {
                client.Dispose();
            }

        }

        private string SerializeObjectToJson<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}