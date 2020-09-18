using System;
using System.IO;

namespace Docmosis.Render
{
    public interface IRenderer
    {
        Stream RenderDocumentTemplate(string templateId, object data);
    }
}