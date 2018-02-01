namespace NewRelic.Synthetics.Api.Tests.Tests
{
    using System.Configuration;
    using System.Linq;

    using NewRelic.Synthetics.Api.Classes;
    using NewRelic.Synthetics.Api.Classes.ApiProxies;

    using NUnit.Framework;

    public class MonitorsTest
    {
        private string apiKey;
        private SyntheticsJsonRoot monitorsCollection;
        private GetAllMonitors monitorRetriver;


        [SetUp]
        protected void SetUp()
        {
            var appSettingKey = ConfigurationManager.AppSettings.Get("ApiKey");
            this.apiKey = !string.IsNullOrWhiteSpace(appSettingKey) ? appSettingKey : string.Empty;
            monitorRetriver = new GetAllMonitors(this.apiKey);
            this.monitorsCollection = this.monitorRetriver.GetMonitors();
        }

        [Test]
        public void GetAllMonitorsTest()
        {
            Assert.IsTrue(monitorsCollection.Monitors.Any());
            Assert.IsTrue(monitorsCollection.Count > 0);
        }
        [Test]
        public void SwitchOffAndOnAllMonitors()
        {
            ChangeMonitorsStatus(this.monitorsCollection, false);

            var updatedMonitorsCollection = this.monitorRetriver.GetMonitors();
            Assert.IsFalse(CheckAllMonitorsStatus(updatedMonitorsCollection));

            ChangeMonitorsStatus(this.monitorsCollection, true);

            updatedMonitorsCollection = this.monitorRetriver.GetMonitors();
            Assert.IsTrue(CheckAllMonitorsStatus(updatedMonitorsCollection));

        }

        private static bool CheckAllMonitorsStatus(SyntheticsJsonRoot monitorsCollection)
        {
            var returnValue = true;

            foreach (var monitor in monitorsCollection.Monitors)
            {
                returnValue = monitor.IsEnabled;
            }

            return returnValue;
        }

        private void ChangeMonitorsStatus(SyntheticsJsonRoot monitorsCollections, bool targetStatus)
        {
            var monitorUpdater = new UpdateMonitor(apiKey);
            foreach (var monitor in monitorsCollections.Monitors)
            {
                monitorUpdater.Execute(monitor, targetStatus);
            }
        }
    }
}