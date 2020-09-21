using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Docmosis.Render
{
    public class Renderer : IRenderer
    {
        private readonly Uri _docmosisRenderUri;
        private readonly string _accessKey;

        public Renderer(Uri docmosisRenderUri, string accessKey)
        {
            _docmosisRenderUri = docmosisRenderUri;
            _accessKey = accessKey;
        }

        private const string OutputFormat = "pdf";

        private HttpWebResponse SendRequest(string templateId, object renderData)
        {
            var request = (HttpWebRequest) WebRequest.Create(_docmosisRenderUri.ToString());

            var renderRequest = JsonConvert.SerializeObject(new RenderRequest()
            {
                Data = renderData,
                AccessKey = _accessKey,
                OutputFormat = OutputFormat,
                OutputName = $"{Path.GetFileNameWithoutExtension(templateId)}.{OutputFormat}",
                TemplateName = templateId
            });

            var data = new UTF8Encoding().GetBytes(renderRequest);

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.ContentLength = data.Length;

            var stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);

            return (HttpWebResponse) request.GetResponse();
        }

        public Stream RenderDocumentTemplate(string templateId, IDictionary<string, object> data)
        {
            try
            {
                using (var response = SendRequest(templateId, data))
                {
                    if (response.StatusCode != HttpStatusCode.OK) throw new DocmosisRenderError();
                    var returnedStream = new MemoryStream();
                    response.GetResponseStream()?.CopyTo(returnedStream);
                    returnedStream.Seek(0, SeekOrigin.Begin);
                    return returnedStream;
                }
            }
            catch (WebException exception)
            {
                if (exception.Response != null && ((HttpWebResponse) exception.Response).StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new DocmosisRenderBadRequest();
                }
                throw new DocmosisRenderError();
            }
        }
    }
}