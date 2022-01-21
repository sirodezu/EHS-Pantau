using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApp
{
    public class ApiHelper
    {
        private static string UrlUTOneAPI = Settings.GetAppSetting("UrlUTOneAPI");
        private static string UserUTOneAPI = Settings.GetAppSetting("UserUTOneAPI");
        private static string PassUTOneAPI = Settings.GetAppSetting("PassUTOneAPI");
        private static string UrlLoginAPI = Settings.GetAppSetting("UrlLoginAPI");
        public static string GetToken() {
            HttpClient One_WebAPI = new HttpClient();
            One_WebAPI.BaseAddress = new Uri(UrlUTOneAPI + "/token");
            One_WebAPI.DefaultRequestHeaders.Accept.Clear();
            One_WebAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var listLogin = new List<KeyValuePair<string, string>>();
            listLogin.Add(new KeyValuePair<string, string>("username", UserUTOneAPI));
            listLogin.Add(new KeyValuePair<string, string>("password", PassUTOneAPI));
            listLogin.Add(new KeyValuePair<string, string>("grant_type", "password"));
            HttpResponseMessage response = One_WebAPI.PostAsync(One_WebAPI.BaseAddress, new FormUrlEncodedContent(listLogin)).Result;
            string AccessToken = "";
            if (response.IsSuccessStatusCode)
            {
                if (response.Content.ReadAsAsync<object>().Result != null)
                {
                    dynamic jsonResult = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    AccessToken = jsonResult["access_token"].ToString();
                }
            }
            return AccessToken;
        }
        public static dynamic GetEmployeeByNRP(string nrp)
        {
            string AccessToken = GetToken();
            HttpClient One_WebAPI = new HttpClient();
            One_WebAPI.BaseAddress = new Uri(UrlUTOneAPI);
            One_WebAPI.DefaultRequestHeaders.Accept.Clear();
            // Add an Accept header for JSON format.
            One_WebAPI.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            One_WebAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = One_WebAPI.GetAsync(UrlUTOneAPI + "/api/employee/selectallbynrp/?nrp="+ nrp).Result;
            dynamic json = null;
            if (response.IsSuccessStatusCode)
            {
                json = response.Content.ReadAsAsync<object>().Result;
            }
            return json;
        }
        public static dynamic GetEmployeeDivision(string divisioncode)
        {
            string AccessToken = GetToken();
            HttpClient One_WebAPI = new HttpClient();
            One_WebAPI.BaseAddress = new Uri(UrlUTOneAPI);
            One_WebAPI.DefaultRequestHeaders.Accept.Clear();
            // Add an Accept header for JSON format.
            One_WebAPI.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            One_WebAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = One_WebAPI.GetAsync(UrlUTOneAPI + "/api/employee/selectallbypersarea/?divisioncode="+ divisioncode).Result;
            dynamic json = null;
            if (response.IsSuccessStatusCode)
            {
                json = response.Content.ReadAsAsync<object>().Result;
            }
            return json;
        }
    }
}
