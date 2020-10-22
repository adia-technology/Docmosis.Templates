using System;
using System.Threading.Tasks;

namespace Docmosis.Templates
{
    public class Template
    {
        private readonly Lazy<Task<byte[]>> _templateGetter;

        public Template(string name, Func<string, Task<byte[]>> templateGetter)
        {
            _templateGetter = new Lazy<Task<byte[]>>(async () => await templateGetter(Name));
            Name = name;
        }
        public string Name { get; }

        public Task<byte[]> TemplateContent => _templateGetter.Value;
    }
}