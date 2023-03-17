using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHealthCheckOptions = Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions;

namespace Zy.App.Common.Core.HealthCheckExtensions.HealthCheckExtensionOption
{
    public class ZyHealthCheckOptions
    {
        public ZyHealthCheckOptions()
        {
            LiveOptions = new AspNetCoreHealthCheckOptions()
            {
                Predicate = (healthCheckRegistration) => healthCheckRegistration.Tags.Contains(HealthCheckTags.Live),
            };

            LiveDetailOptions = new AspNetCoreHealthCheckOptions()
            {
                Predicate = (healthCheckRegistration) => healthCheckRegistration.Tags.Contains(HealthCheckTags.Live),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            };

            ReadyOptions = new AspNetCoreHealthCheckOptions()
            {
                Predicate = (healthCheckRegistration) => healthCheckRegistration.Tags.Contains(HealthCheckTags.Ready),
            };

            ReadyDetailOptions = new AspNetCoreHealthCheckOptions()
            {
                Predicate = (healthCheckRegistration) => healthCheckRegistration.Tags.Contains(HealthCheckTags.Ready),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            };

            HealthOptions = new AspNetCoreHealthCheckOptions()
            {
                Predicate = (healthCheckRegistration) => !healthCheckRegistration.Tags.Contains(HealthCheckTags.Ignore),
            };

            HealthDetailOptions = new AspNetCoreHealthCheckOptions()
            {
                Predicate = (healthCheckRegistration) => !healthCheckRegistration.Tags.Contains(HealthCheckTags.Ignore),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            };
        }

        public bool Open { get; set; } = true;

        public string LivePath { get; set; } = "/.well-known/health/live";

        public string LiveDetailPath { get; set; } = "/.well-known/health/live/detail";

        public string ReadyPath { get; set; } = "/.well-known/health/ready";

        public string ReadyDetailPath { get; set; } = "/.well-known/health/ready/detail";

        public string HealthPath { get; set; } = "/.well-known/health";

        public string HealthDetailPath { get; set; } = "/.well-known/health/detail";

        public AspNetCoreHealthCheckOptions LiveOptions { get; set; }

        public AspNetCoreHealthCheckOptions LiveDetailOptions { get; set; }

        public AspNetCoreHealthCheckOptions ReadyOptions { get; set; }

        public AspNetCoreHealthCheckOptions ReadyDetailOptions { get; set; }

        public AspNetCoreHealthCheckOptions HealthOptions { get; set; }

        public AspNetCoreHealthCheckOptions HealthDetailOptions { get; set; }

        public HealthCheckItemTags Tag { get; set; } = HealthCheckItemTags.Ready;

        public string? Name { get; set; }

        public TimeSpan? Timeout { get; set; } = new TimeSpan(0, 0, 0, 3);

        public HealthStatus? FailureStatus { get; set; }

        public string[] GetTags()
        {
            switch (this.Tag)
            {
                case HealthCheckItemTags.Ignore:
                    return new string[] { HealthCheckTags.Ignore };
                case HealthCheckItemTags.Both:
                    return new string[] { HealthCheckTags.Live, HealthCheckTags.Ready };
                case HealthCheckItemTags.Live:
                    return new string[] { HealthCheckTags.Live };
                case HealthCheckItemTags.Ready:
                    return new string[] { HealthCheckTags.Ready };
                default:
                    return new string[] { };
            }
        }


        public void Apply(ZyHealthCheckOptions source)
        {
            if (source == null)
            {
                return;
            }

            Open = source.Open;
            LivePath = source.LivePath;
            LiveOptions = source.LiveOptions;
            LiveDetailPath = source.LiveDetailPath;
            LiveDetailOptions = source.LiveDetailOptions;

            ReadyPath = source.ReadyPath;
            ReadyDetailPath = source.ReadyDetailPath;
            ReadyOptions = source.ReadyOptions;
            ReadyDetailOptions = source.ReadyDetailOptions;

            HealthPath = source.HealthPath;
            HealthDetailPath = source.HealthDetailPath;
            HealthOptions = source.HealthOptions;
            HealthDetailOptions = source.HealthDetailOptions;
        }
    }

    public class HealthCheckTags
    {
        public const string Ignore = "Ignore";

        public const string Ready = "ready";

        public const string Live = "live";
    }

    [Flags]
    public enum HealthCheckItemTags
    {
        Ignore = 0,
        Live = 1,
        Ready = 2,
        Both = 4,
        None = 8,
    }
}
