using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace TravelRecordApp.Helpers
{
    class RestClient<T>
    {
        public async Task<T> GetAsync(string webServiceUrl)
        {
            try
            {
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(webServiceUrl);
                var taskmodels = JsonConvert.DeserializeObject<T>(json);
                return taskmodels;
            }
            catch(Exception e)
            {
                Console.WriteLine("GetAsync erroe:" + e.Message);
                return default(T);
            }
        }
    }
}
