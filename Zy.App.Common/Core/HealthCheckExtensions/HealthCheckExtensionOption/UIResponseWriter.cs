using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Zy.App.Common.Core.HealthCheckExtensions.HealthCheckExtensionOption
{
    internal class UIResponseWriter
    {
        private const string DefaultContentType = "application/json";

        public static Task WriteHealthCheckUIResponse(HttpContext httpContext, HealthReport report) => WriteHealthCheckUIResponse(httpContext, report, null);

        public static Task WriteHealthCheckUIResponse(HttpContext httpContext, HealthReport report, Action<JsonSerializerOptions>? jsonConfigurator)
        {
            var response = "{}";

            if (report != null)
            {
                var settings = new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    IgnoreNullValues = true,
                };

                jsonConfigurator?.Invoke(settings);

                settings.Converters.Add(new JsonStringEnumConverter());

                settings.Converters.Add(new TimeSpanConverter());

                httpContext.Response.ContentType = DefaultContentType;

                var uiReport = UIHealthReport
                    .CreateFrom(report);

                response = JsonSerializer.Serialize(uiReport, settings);
            }

            return httpContext.Response.WriteAsync(response);
        }

        private class TimeSpanConverter
            : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return default;
            }

            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
