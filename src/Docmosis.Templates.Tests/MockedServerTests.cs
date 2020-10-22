using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Docmosis.Templates.Tests
{
    public abstract class MockedServerTests
    {
        protected WireMockServer DocmosisServer { get; private set; }

        [SetUp]
        public void SetUp()
        {
            DocmosisServer = WireMockServer.Start();
            DocmosisServer.ResetMappings();
        }

        [TearDown]
        public void TearDown()
        {
            DocmosisServer.Stop();
        }

        protected void ConfigureListTemplatesEndpoint() =>
            DocmosisServer.Given(Request.Create()
                    .UsingPost()
                    .WithPath("/api/listTemplates"))
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile("ListTemplatesResponse.json"));
    }
}