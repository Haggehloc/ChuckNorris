using Newtonsoft.Json;
using System.Management.Automation;

namespace ChuckNorris
{

    [Cmdlet(VerbsCommon.Get,"ChuckNorris")]
    [OutputType(typeof(ChuckNorrisInfo))]
    public class GetChuckNorris : Cmdlet
    {
        [Parameter(Position = 0)]
        public SwitchParameter isRandom 
        {
            get { return true; }
            set { isRandom = true; }
        }
        private string url = "https://api.chucknorris.io/jokes";
        private ChuckNorrisInfo info = new ChuckNorrisInfo();

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (isRandom.IsPresent)
            {
                url += "/random";
            }
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            var task = Start(url);
            task.Wait();
            WriteObject(info);
            return;
        }

        public async Task Start(string url)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            var stringTask = client.GetStringAsync(url);

            string response = await stringTask.ConfigureAwait(false);

            ChuckNorrisInfo? deserializedResult = JsonConvert.DeserializeObject<ChuckNorrisInfo>(response);

            parseInfo(deserializedResult != null ? deserializedResult : new ChuckNorrisInfo());
        }

        public void parseInfo(ChuckNorrisInfo newInfo)
        {
            info.value = newInfo.value;
            info.url = newInfo.url;
            info.icon_url = newInfo.icon_url;
            info.updated_at = newInfo.updated_at;
            info.created_at = newInfo.created_at;
            info.categories = newInfo.categories;
            info.id = newInfo.id;
        }



    }

    public class ChuckNorrisInfo
    {
        public string icon_url { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public string value { get; set; }
        public string[] categories { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}