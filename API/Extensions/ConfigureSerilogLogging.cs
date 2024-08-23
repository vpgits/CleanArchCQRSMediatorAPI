// <copyright file="ConfigureSerilogLogging.cs" company="vpgits">
// Copyright (c) vpgits. All rights reserved.
// </copyright>

namespace CleanArchCQRSMediatorAPI.API.Extensions
{
    using Elastic.Channels;
    using Elastic.CommonSchema.Serilog;
    using Elastic.Ingest.Elasticsearch;
    using Elastic.Ingest.Elasticsearch.DataStreams;
    using Elastic.Serilog.Sinks;
    using Serilog;
    using Serilog.Formatting.Elasticsearch;

    public static class ConfigureSerilogLogging
    {
        public static IHostBuilder RegisterSerilogLogging(this IHostBuilder builder, IConfiguration appConfiguration)
        {
            builder.UseSerilog((context, services, configuration) =>
            {
                var httpAccessor = context.Configuration.Get<HttpContextAccessor>()!;
                configuration
                .ReadFrom.Configuration(context.Configuration)
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithEcsHttpContext(httpAccessor);

                // you could enable elasticsearch and microsoft application insights through here
                if (context.HostingEnvironment.IsDevelopment())
                {
                    // elasticsearch

                    // configuration.WriteTo.Elasticsearch(new[] { new Uri(appConfiguration["ElasticConfiguration:Uri"] !) }, opts =>
                    // {
                    //    opts.DataStream = new DataStreamName("logs", "console-example", "demo");
                    //    opts.BootstrapMethod = BootstrapMethod.Failure;
                    //    opts.ConfigureChannel = channelOpts =>
                    //    {
                    //        channelOpts.BufferOptions = new BufferOptions
                    //        {
                    //            ExportMaxConcurrency = 10,
                    //        };
                    //    };
                    // });

                    // application insights

                    // configuration.WriteTo.ApplicationInsights(services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces);
                }
                // else
                // {
                //     configuration.WriteTo.Console(new ElasticsearchJsonFormatter());
                // }
            });
            return builder;
        }
    }
}