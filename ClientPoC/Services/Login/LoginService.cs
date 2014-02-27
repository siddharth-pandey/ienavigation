using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json.Linq;

namespace ClientPoC.Services.Login
{
    public class LoginService : Service
    {
        private string _username;
        private AuthenticationTransmitResponse _authResponse;

        public LoginService()
            : base(Constants.Constants.AuthenticationServerBaseUrl, Constants.Constants.AuthenticationServerRequestUrl)
        {
        }

        internal async Task<bool> CheckUserAuthentication(string uname, string pwd)
        {
            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(pwd))
            {
                return false;
            }
            _username = uname;
            var dataToPost = new AuthenticationTransmitRequest(uname, pwd);
            var response = await Post<AuthenticationTransmitRequest>(_serviceBaseAddress, _serviceRequestUrl, dataToPost);

            _authResponse = new AuthenticationTransmitResponse();

            if (response != null && response.IsSuccessStatusCode)
            {
                _authResponse.Token = ProcessTokenResponse(response).Result;
                _authResponse.IsUserAuthenticated = true;
            }
            else
            {
                _authResponse.IsUserAuthenticated = false;
                _authResponse.Token = string.Empty;
            }

            return _authResponse.IsUserAuthenticated;
        }

        internal string GetAuthenticationToken()
        {
            if (_authResponse != null && _authResponse.IsUserAuthenticated)
            {
                return _authResponse.Token;
            }
            else
            {
                return string.Empty;
            }
        }

        protected override HttpClient SetupHttpClient(string serviceBaseAddress)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(serviceBaseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Device-ID", _username);
            _httpClient.DefaultRequestHeaders.Add("Device-Type", "ipad");
            _httpClient.DefaultRequestHeaders.Add("App-Context", "clinician");
            return _httpClient;
        }

        private async Task<string> ProcessTokenResponse(HttpResponseMessage response)
        {
            string tokenResponse = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(tokenResponse);
            var token = jsonObject["access_token"].ToString();

            return token;
        }
    }

    public class AuthenticationTransmitRequest
    {
        public AuthenticationTransmitRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Grant_Type
        {
            get { return Constants.Constants.GrantType; }
        }

        public string Scope
        {
            get { return Constants.Constants.Scope; }
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationTransmitResponse
    {
        public string Token { get; set; }
        public bool IsUserAuthenticated { get; set; }
    }
}