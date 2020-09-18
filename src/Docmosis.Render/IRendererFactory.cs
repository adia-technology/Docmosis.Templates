namespace Docmosis.Render
{
    public interface IRendererFactory
    {
        IRenderer CreateRenderer(Region region, string accessKey);
    }
}