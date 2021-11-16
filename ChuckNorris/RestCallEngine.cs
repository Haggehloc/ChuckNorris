using Newtonsoft.Json;
using System.Management.Automation;
using System.Text.Json;

namespace ChuckNorris
{
    public static class RestCallEngine
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<ChuckNorrisInfo> Start(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            var stringTask = client.GetStringAsync(url);

            string response = await stringTask;

            ChuckNorrisInfo? info = JsonConvert.DeserializeObject<ChuckNorrisInfo>(response);

            return info != null ? info : new ChuckNorrisInfo();
        }
    }
}
