using System;

namespace Docmosis.Render
{
    public class RendererFactory
    {
        public IRenderer CreateRenderer(Region region, string accessKey)
        {
            switch (region)
            {
                case Region.US:
                    return new Renderer(new Uri("https://us.dws3.docmosis.com/api/render"), accessKey);
                case Region.EU:
                    return new Renderer(new Uri("https://eu.dws3.docmosis.com/api/render"), accessKey);
                case Region.AU:
                    return new Renderer(new Uri("https://au.dws3.docmosis.com/api/render"), accessKey);
                default:
                    throw new ArgumentOutOfRangeException(nameof(region), region, null);
            }
        }
    }
}