using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Docmosis.Templates
{
    public class TemplateManager : ITemplateManager
    {
        private readonly Uri _docmosisTemplatesUri;
        private readonly string _accessKey;

        public TemplateManager(Uri docmosisTemplatesUri, string accessKey)
        {
            _docmosisTemplatesUri = docmosisTemplatesUri;
            _accessKey = accessKey;
        }
        private async Task<byte[]> GetTemplateContent(string name)
        {
            HttpContent accessKeyContent = new StringContent(_accessKey);
            HttpContent templateName = new StringContent(name);

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(accessKeyContent, "accessKey", "accessKey");
                formData.Add(templateName, "templateName", "templateName");
                var response = await client.PostAsync(_docmosisTemplatesUri + "uploadTemplate", formData);
                if (!response.IsSuccessStatusCode)
                {
                    throw new DocmosisTemplatesError("Unable to create template");
                }
                return await response.Content.ReadAsByteArrayAsync();
            }
        }
        
        public async Task<IReadOnlyCollection<Template>> ListTemplates()
        {
            HttpContent accessKeyContent = new StringContent(_accessKey);
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(accessKeyContent, "accessKey", "accessKey");
                var response = await client.PostAsync(_docmosisTemplatesUri + "listTemplates", formData);
                
                if (!response.IsSuccessStatusCode)
                    throw new DocmosisTemplatesError("Received invalid response from Docmosis");
                
                var content = await response.Content.ReadAsStringAsync();
                var parsedContent = JsonConvert.DeserializeObject<TemplatesResponse>(content);
                if (parsedContent?.TemplateList == null)
                {
                    return await Task.FromResult(new List<Template>());
                }

                return parsedContent.TemplateList
                    .Where(t => int.Parse(t.SizeBytes) > 0)
                    .Select(t => new Template(GetTemplateContent)
                    {
                        Name = t.Name
                    }).ToList();
            }
        }

        public async Task UploadTemplate(Template template)
        {
            HttpContent accessKeyContent = new StringContent(_accessKey);
            HttpContent bytesContent = new ByteArrayContent(await template.TemplateContent);
            HttpContent templateName = new StringContent(template.Name);

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(accessKeyContent, "accessKey", "accessKey");
                formData.Add(bytesContent, "templateFile", "templateFile");
                formData.Add(templateName, "templateName", "templateName");
                var response = await client.PostAsync(_docmosisTemplatesUri + "uploadTemplate", formData);
                if (!response.IsSuccessStatusCode)
                {
                    throw new DocmosisTemplatesError("Unable to create template");
                }
            }
        }
    }
}