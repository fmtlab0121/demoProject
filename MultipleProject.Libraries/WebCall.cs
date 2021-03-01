using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MultipleProject.Libraries
{
    public static class WebCall
    {
        

        public static async Task<HttpResponseMessage> GetApiDataAsync(string urlRoot, string endPoint)
        {
            var _httpClientHandler = new HttpClientHandler();

            _httpClientHandler.UseDefaultCredentials = true;
            _httpClientHandler.Credentials = CredentialCache.DefaultNetworkCredentials;
            _httpClientHandler.PreAuthenticate = true;
            _httpClientHandler.ServerCertificateCustomValidationCallback = CertificateValidation;


            HttpClient _client = new HttpClient(_httpClientHandler);
            _client.BaseAddress = new Uri(urlRoot);
            try
            {
                var response = await _client.GetAsync(endPoint);
                if(response.IsSuccessStatusCode)
                {
                    return response;
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return null;
            }

            return null;

        }



        //public bool CertificateValidation2(HttpRequestMessage request, X509Certificate2 certificate, X509Chain certificateChain, SslPolicyErrors policy)
        //{
        //    return true;
        //}

        public static bool CertificateValidation(HttpRequestMessage request, X509Certificate2 certificate, X509Chain certificateChain, SslPolicyErrors policy)
        {
            return true;
        }


    }
}
