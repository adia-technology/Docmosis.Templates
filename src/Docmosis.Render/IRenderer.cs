using System;
using System.Collections.Generic;
using System.IO;

namespace Docmosis.Render
{
    public interface IRenderer
    {
        Stream RenderDocumentTemplate(string templateId, IDictionary<string, object> data);

        void SetDebugMode(bool debug);

    }
}