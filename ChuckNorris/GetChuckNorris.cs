using System.Management.Automation;

namespace ChuckNorris
{
    [Cmdlet(VerbsCommon.Get,"ChuckNorris")]
    [OutputType(typeof(ChuckNorrisInfo))]
    public class GetChuckNorris : Cmdlet
    {

    }
    public class ChuckNorrisInfo
    {
        public string icon_url { get; set; }
        public string id { get; set; }
        public string url { get; set; }
        public string value { get; set; }
        public List<String> categories { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}