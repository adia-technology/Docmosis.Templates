using Newtonsoft.Json;

namespace Docmosis.Render
{
    public class RenderRequest
    {
        [JsonProperty("accessKey")]
        public string AccessKey { get; set; }
        
        [JsonProperty("templateName")]
        public string TemplateName { get; set; }
        
        [JsonProperty("outputName")]
        public string OutputName { get; set; }
        
        [JsonProperty("outputFormat")]
        public string OutputFormat { get; set; }
  
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}