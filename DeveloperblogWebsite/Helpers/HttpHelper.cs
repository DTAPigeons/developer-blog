using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace DeveloperblogWebsite.Helpers {
    public static class HttpHelper {
        private static readonly string API_URL = "http://localhost:64691/api";
        private static readonly string TOKEN_URL = "http://localhost:64691/Token";
        private static HttpClient client;

        public static Task<HttpResponseMessage> GetResponsetMassage(string url, string token = "") {
            ResetClient(token);
            return client.GetAsync(API_URL + url);
        }

        public static Task<HttpResponseMessage> GetResponsetMassage(string url, HttpContent content, string token = "") {
            ResetClient(token);
            return client.GetAsync(API_URL + url);
        }

        public static Task<HttpResponseMessage> PostResponsetMassage(string url, HttpContent content, string token) {
            ResetClient(token);
            return client.PostAsync(API_URL + url, content);
        }

        public static Task<HttpResponseMessage> DeleteResponsetMassage(string url, string token) {
            ResetClient(token);
            return client.DeleteAsync(API_URL + url);
        }

        public static string GetToken(string userName, string password) {
            var pairs = new List<KeyValuePair<string, string>>
            {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", userName ),
                        new KeyValuePair<string, string> ( "Password", password )
             };

            var content = new FormUrlEncodedContent(pairs);
            client = new HttpClient();
            HttpResponseMessage requestMessage = client.PostAsync(TOKEN_URL, content).Result;
            if (requestMessage.IsSuccessStatusCode) { return requestMessage.Content.ReadAsStringAsync().Result; }
            return "";
        }

        private static void ResetClient(string token) {
            client = new HttpClient();
            client.BaseAddress = new Uri(API_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrWhiteSpace(token)) {
                var t = JsonConvert.DeserializeObject<Token>(token);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + t.access_token);
            }
        }
    }

    class Token {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        [JsonProperty(".issued")]
        public string issued { get; set; }
        [JsonProperty(".expires")]
        public string expires { get; set; }
    }
}
