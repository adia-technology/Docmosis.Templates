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

namespace Docmosis.Render.Tests
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

        protected void ConfigureRenderEndpoint() =>
            DocmosisServer.Given(Request.Create()
                    .UsingPost()
                    .WithPath("/api/render"))
                .RespondWith(Response.Create()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/octet-stream")
                    .WithBodyFromFile("sample.pdf"));
    }
}