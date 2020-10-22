using Newtonsoft.Json;

namespace Docmosis.Templates
{
    public class TemplateResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; } 
      
        [JsonProperty("lastModifiedMillisSinceEpoch")]
        public string LastModifiedMillisSinceEpoch { get; set; } 
        
        [JsonProperty("lastModifiedISO8601")]
        public string LastModifiedIso8601 { get; set; }
        
        [JsonProperty("sizeBytes")]
        public string SizeBytes { get; set; }
    }
}