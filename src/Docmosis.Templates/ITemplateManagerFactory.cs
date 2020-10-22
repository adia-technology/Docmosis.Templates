namespace Docmosis.Templates
{
    public interface ITemplateManagerFactory
    {
        ITemplateManager CreateTemplateManager(Region region, string accessKey);
    }
}