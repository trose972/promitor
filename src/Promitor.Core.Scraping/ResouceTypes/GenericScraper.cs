﻿using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Promitor.Core.Scraping.Configuration.Model;
using Promitor.Core.Scraping.Configuration.Model.Metrics.ResouceTypes;
using Promitor.Core.Telemetry.Interfaces;
using Promitor.Integrations.AzureMonitor;

namespace Promitor.Core.Scraping.ResouceTypes
{
    internal class GenericScraper : Scraper<GenericMetricDefinition>
    {
        private const string ResourceUriTemplate = "subscriptions/{0}/resourceGroups/{1}/providers/{2}";

        public GenericScraper(AzureMetadata azureMetadata, AzureCredentials azureCredentials, ILogger logger, IExceptionTracker exceptionTracker)
            : base(azureMetadata, azureCredentials, logger, exceptionTracker)
        {
        }

        protected override async Task<double> ScrapeResourceAsync(AzureMonitorClient azureMonitorClient, GenericMetricDefinition metricDefinition)
        {
            var resourceUri = string.Format(ResourceUriTemplate, AzureMetadata.SubscriptionId, AzureMetadata.ResourceGroupName, metricDefinition.ResourceUri);
            var metricName = metricDefinition.AzureMetricConfiguration.MetricName;
            var foundMetricValue = await azureMonitorClient.QueryMetricAsync(metricName, metricDefinition.AzureMetricConfiguration.Aggregation, resourceUri, metricDefinition.Filter);

            return foundMetricValue;
        }
    }
}