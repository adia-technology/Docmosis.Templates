using System.Collections.Generic;
using System.Threading.Tasks;

namespace Docmosis.Templates
{
    public interface ITemplateManager
    {
        Task<IReadOnlyCollection<Template>> ListTemplates();

        Task UploadTemplate(Template template);
        
        Task DeleteTemplate(string templateName);
    }
}