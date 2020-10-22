using System.Collections.Generic;
using Newtonsoft.Json;

namespace Docmosis.Templates
{
    public class TemplatesResponse
    {
        [JsonProperty("templateList")]
        public IReadOnlyCollection<TemplateResponse> TemplateList { get; set; }
    }
}