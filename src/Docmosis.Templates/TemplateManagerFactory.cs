using System;

namespace Docmosis.Templates
{
    public class TemplateManagerFactory : ITemplateManagerFactory
    {
        public ITemplateManager CreateTemplateManager(Region region, string accessKey)
        {
            switch (region)
            {
                case Region.US:
                    return new TemplateManager(new Uri("https://us.dws3.docmosis.com/api/"), accessKey);
                case Region.EU:
                    return new TemplateManager(new Uri("https://eu.dws3.docmosis.com/api/"), accessKey);
                case Region.AU:
                    return new TemplateManager(new Uri("https://au.dws3.docmosis.com/api/"), accessKey);
                default:
                    throw new ArgumentOutOfRangeException(nameof(region), region, null);
            }
        }
    }
}