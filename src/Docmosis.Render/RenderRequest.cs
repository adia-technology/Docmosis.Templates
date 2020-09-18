using System.Text.Json.Serialization;

namespace Docmosis.Render
{
    public class RenderRequest
    {
        [JsonPropertyName("accessKey")]
        public string AccessKey { get; set; }
        
        [JsonPropertyName("templateName")]
        public string TemplateName { get; set; }
        
        [JsonPropertyName("outputName")]
        public string OutputName { get; set; }
        
        [JsonPropertyName("outputFormat")]
        public string OutputFormat { get; set; }
  
        [JsonPropertyName("data")]
        public object Data { get; set; }
    }
}