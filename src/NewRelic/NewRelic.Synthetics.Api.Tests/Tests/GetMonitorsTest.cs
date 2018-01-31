﻿namespace NewRelic.Synthetics.Api.Tests.Tests
{
    using System.Configuration;
    using System.Linq;

    using NewRelic.Synthetics.Api.Classes.ApiProxies;

    using NUnit.Framework;

    public class GetMonitorsTest
    {
        private string apiKey;

        [SetUp]
        protected void SetUp()
        {
            var appSettingKey = ConfigurationManager.AppSettings.Get("ApiKey");
            this.apiKey = !string.IsNullOrWhiteSpace(appSettingKey) ? appSettingKey : string.Empty;
        }

        [Test]
        public void GetAllMonitorsTest()
        {
            var monitors = new GetAllMonitors(this.apiKey).GetMonitors();
            Assert.IsTrue(monitors.Any());
        }
    }
}