using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Docmosis.Render.Tests
{
    public class RendererTests : MockedServerTests
    {
        [SetUp]
        public void Setup()
        {
            ConfigureRenderEndpoint();
        }

        [Test]
        public void RendererRendersTemplateToAValidPdf()
        {
            var renderer = new Renderer(new Uri($"{DocmosisServer.Urls.First()}/api/render"), "some access key");
            var response = renderer.RenderDocumentTemplate("some_document_template.docx", new
            {
                someDate = "Test Data"
                
            });

            using var file = File.Create("test.pdf");
            var totalBytesRead = 0;
            var bytesRead = 0;
            var buff = new byte[1000];
            while ((bytesRead = response.Read(buff, 0, buff.Length)) > 0)
            {
                file.Write(buff, 0, bytesRead);
                totalBytesRead += bytesRead;
            }
            totalBytesRead.ShouldBe(3028);
            Console.Out.WriteLine("Created file:" + file.Name);
        }
    }
}