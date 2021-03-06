﻿namespace NewRelic.Synthetics.Api.Tests.Tests
{
    using System;
    using System.Configuration;
    using System.Linq;

    using NewRelic.Synthetics.Api.Classes;
    using NewRelic.Synthetics.Api.Classes.ApiProxies;

    using NUnit.Framework;

    public class MonitorsTest
    {
        private string apiKey;
        private SyntheticsJsonRoot monitorsCollection;
        private GetMonitors monitorRetriver;


        [SetUp]
        protected void SetUp()
        {
            var appSettingKey = ConfigurationManager.AppSettings.Get("ApiKey");
            this.apiKey = !string.IsNullOrWhiteSpace(appSettingKey) ? appSettingKey : string.Empty;
            monitorRetriver = new GetMonitors(this.apiKey);
            this.monitorsCollection = this.monitorRetriver.GetAllMonitors();
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

            var updatedMonitorsCollection = this.monitorRetriver.GetAllMonitors();
            Assert.IsFalse(CheckAllMonitorsStatus(updatedMonitorsCollection));

            ChangeMonitorsStatus(this.monitorsCollection, true);

            updatedMonitorsCollection = this.monitorRetriver.GetAllMonitors();
            Assert.IsTrue(CheckAllMonitorsStatus(updatedMonitorsCollection));

        }

        /// <summary>
        /// Randomly changes one of monitors status
        /// </summary>
        [Test]
        public void ChangeMonitorStatus()
        {
            var index = new Random().Next(this.monitorsCollection.Count -  1);
            var monitor = this.monitorsCollection.Monitors[index];

            var currentStatus = monitor.IsEnabled;
            var monitorUpdater = new UpdateMonitor(this.apiKey);
            //change monitor status to another
            monitorUpdater.Execute(monitor, !currentStatus);
            monitor = this.monitorRetriver.GetMonitorByName(monitor.Name);
            Assert.IsFalse(currentStatus.Equals(monitor.IsEnabled));
            //revert back
            currentStatus = monitor.IsEnabled;
            monitorUpdater.Execute(monitor, !currentStatus);
            monitor = this.monitorRetriver.GetMonitorByName(monitor.Name);
            Assert.IsFalse(currentStatus.Equals(monitor.IsEnabled));
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