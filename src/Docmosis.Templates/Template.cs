using System;
using System.Threading.Tasks;

namespace Docmosis.Templates
{
    public class Template
    {
        private readonly Lazy<Task<byte[]>> _templateGetter;

        internal Template(Func<string, Task<byte[]>> templateGetter)
        {
            _templateGetter = new Lazy<Task<byte[]>>(async () => await templateGetter(Name));
        }
        public string Name { get; set; }

        public Task<byte[]> TemplateContent => _templateGetter.Value;
    }
}