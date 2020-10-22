using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace Docmosis.Templates.Tests
{
    public class TemplateManagerTests : MockedServerTests
    {
        private string _url;

        [SetUp]
        public void Setup()
        {
            ConfigureListTemplatesEndpoint();
            ConfigureGetTemplateEndpoint();
            _url = DocmosisServer.Urls.First();
        }

        [Test]
        public async Task TemplateManager_FetchesListOfNonEmptyTemplates()
        {
            var templateManager = new TemplateManager(new Uri($"{_url}/api/"), "some access key");
            var response =  await templateManager.ListTemplates();
            
            response.Count.ShouldBe(1);
            response.Single().Name.ShouldBe("Contracts/en_CH/SomeFile.docx");
        }
        
        
        [Test]
        public async Task TemplateManager_FetchesContentOfTemplate()
        {
            var templateManager = new TemplateManager(new Uri($"{_url}/api/"), "some access key");
            var response =  await templateManager.ListTemplates();
            
            response.Count.ShouldBe(1);
            response.Single().Name.ShouldBe("Contracts/en_CH/SomeFile.docx");
            (await response.Single().TemplateContent).Length.ShouldBePositive();
        }
    }
}