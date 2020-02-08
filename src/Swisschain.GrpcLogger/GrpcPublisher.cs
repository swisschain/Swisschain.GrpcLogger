using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using DotNetCoreDecorators;
using Swisschain.GrpcLogger.GrpcContracts;
using Swisschain.GrpcLogger.GrpcContracts.Models;
using Swisschain.SystemLog.GrpcContracts.Contracts;

namespace Swisschain.GrpcLogger
{
    public class GrpcPublisher
    {
        private readonly ISystemLogService _systemLogService;

        private List<LogEventModel> _events
            = new List<LogEventModel>();

        private readonly object _lockObject = new object();

        private DateTime _lastDateTime = DateTime.UtcNow.AddDays(-1);

        private readonly TaskTimer _threadTimer = new TaskTimer(SendLogTimeout);

        private readonly string _appName;
        private readonly string _appVersion;

        public static int SendLogTimeout { get; set; } = 100;

        public GrpcPublisher(ISystemLogService systemLogService)
        {
            _systemLogService = systemLogService;
            _appName = @Assembly.GetEntryAssembly()?.GetName().Name;
            _appVersion =@Assembly.GetEntryAssembly()?.GetName().Version.ToString();
        }

        internal void EnqueueEvent(LogEventModel logEvent)
        {
            lock (_lockObject)
            {
                Start();

                while (true)
                {
                    if (logEvent.DateTime != _lastDateTime)
                    {
                        _events.Add(logEvent);
                        _lastDateTime = logEvent.DateTime;
                        return;
                    }

                    logEvent.DateTime = logEvent.DateTime.AddMilliseconds(1);
                }
            }
        }

        private bool _isStarted;

        private void Start()
        {

            if (_isStarted)
                return;

            lock (_lockObject)
            {
                if (_isStarted)
                    return;

                _isStarted = true;
            }

            if (_systemLogService == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"DEBUG\tApplication started without send logs to the remote server");
                Console.ForegroundColor = ConsoleColor.White;
            }

            _threadTimer.Register("SystemLog Pusher", async () =>
            {
                var eventsToPush = GetEventsToPush();

                if (eventsToPush == null)
                    return;

                foreach (var log in eventsToPush)
                {
                    Console.ForegroundColor =
                        log.LogLevel switch
                        {
                            LogLevel.Info => ConsoleColor.Green,
                            LogLevel.Warning => ConsoleColor.Yellow,
                            _ => ConsoleColor.Red
                        };
                    Console.Write(log.LogLevel);
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine($"\t{log.DateTime:HH:mm:ss.mmm} {log.Component}; {log.Process}");
                    Console.WriteLine($"\t{log.Message}");
                    if (!string.IsNullOrEmpty(log.StackTrace))
                    {
                        Console.WriteLine(log.StackTrace);
                    }

                    log.AppName = _appName;
                    log.AppVersion = _appVersion;
                }

                try
                {
                    if (_systemLogService != null)
                    {
                        await _systemLogService.RegisterAsync(new LogEventRequest
                        {
                            Component = _appName,
                            Events = eventsToPush
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("CRITICAL");
                    Console.WriteLine($"\t{DateTime.UtcNow:HH:mm:ss.mmm} Cannot send logs to remote server. Count: {_events.Count}");
                    Console.WriteLine(ex.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });

            _threadTimer.Start();

        }

        private IReadOnlyList<LogEventModel> GetEventsToPush()
        {

            lock (_lockObject)
            {
                if (_events.Count == 0)
                    return null;

                var result = _events;
                _events = new List<LogEventModel>();
                return result;
            }

        }

    }
}