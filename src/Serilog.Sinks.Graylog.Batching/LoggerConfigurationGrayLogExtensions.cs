﻿using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Graylog.Core;
using Serilog.Sinks.Graylog.Core.Helpers;
using Serilog.Sinks.Graylog.Core.Transport;
using System;

namespace Serilog.Sinks.Graylog.Batching
{
    public static class LoggerConfigurationGrayLogExtensions
    {
        /// <summary>
        /// Graylogs the specified options.
        /// </summary>
        /// <param name="loggerSinkConfiguration">The logger sink configuration.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static LoggerConfiguration Graylog(this LoggerSinkConfiguration loggerSinkConfiguration,
                                                  BatchingGraylogSinkOptions options)
        {
            var sink = (ILogEventSink)new PeriodicBatchingGraylogSink(options);
            return loggerSinkConfiguration.Sink(sink, options.MinimumLogEventLevel);
        }

        /// <summary>
        /// Graylogs the specified hostname or address.
        /// </summary>
        /// <param name="loggerSinkConfiguration">The logger sink configuration.</param>
        /// <param name="hostnameOrAddress">The hostname or address.</param>
        /// <param name="port">The port.</param>
        /// <param name="batchSizeLimit">The batchsize limit.</param>
        /// <param name="period">The batching period.</param>
        /// <param name="queueLimit">The queue limit.</param>
        /// <param name="transportType">Type of the transport.</param>
        /// <param name="minimumLogEventLevel">The minimum log event level.</param>
        /// <param name="messageIdGeneratorType">Type of the message identifier generator.</param>
        /// <param name="shortMessageMaxLength">Short length of the message maximum.</param>
        /// <param name="stackTraceDepth">The stack trace depth.</param>
        /// <param name="facility">The facility.</param>
        /// <returns></returns>
        public static LoggerConfiguration Graylog(this LoggerSinkConfiguration loggerSinkConfiguration,
                                                  string hostnameOrAddress,
                                                  int port,
                                                  int batchSizeLimit,
                                                  TimeSpan period,
                                                  int queueLimit,
                                                  TransportType transportType,
                                                  LogEventLevel minimumLogEventLevel = LevelAlias.Minimum,
                                                  MessageIdGeneratortype messageIdGeneratorType = GraylogSinkOptionsBase.DefaultMessageGeneratorType,
                                                  int shortMessageMaxLength = GraylogSinkOptionsBase.DefaultShortMessageMaxLength,
                                                  int stackTraceDepth = GraylogSinkOptionsBase.DefaultStackTraceDepth,
                                                  string facility = GraylogSinkOptionsBase.DefaultFacility)
        {
            var options = new BatchingGraylogSinkOptions
            {
                HostnameOrAddress = hostnameOrAddress,
                Port = port,
                BatchSizeLimit = batchSizeLimit,
                Period = period,
                QueueLimit = queueLimit,
                TransportType = transportType,
                MinimumLogEventLevel = minimumLogEventLevel,
                MessageGeneratorType = messageIdGeneratorType,
                ShortMessageMaxLength = shortMessageMaxLength,
                StackTraceDepth = stackTraceDepth,
                Facility = facility,
            };

            return loggerSinkConfiguration.Graylog(options);
        }
    }
}