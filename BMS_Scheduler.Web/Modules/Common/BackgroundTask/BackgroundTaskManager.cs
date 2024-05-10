
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serenity;
using Serenity.Abstractions;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Web;

namespace BMS_Scheduler.Common.Services
{
    public class BackgroundTaskManager : IBackgroundTaskManager
    {
        private static Timer timer;
        private static Double timerInterval = TimeSpan.FromSeconds(10).TotalMilliseconds;
        private static List<IBackgroundTask> tasks;
        private readonly IOptions<BackgroundTaskSettings> options;
        private readonly IExceptionLogger logger;
        private static bool isDisabled;

        public BackgroundTaskManager(IOptions<BackgroundTaskSettings> options, IExceptionLogger logger = null)
        {
            timer = new Timer();
            timer.Interval = timerInterval;
            timer.Elapsed += Timer_Elapsed;
            tasks = new List<IBackgroundTask>();

            isDisabled = !options.Value.Enabled;
            this.options = options;
            this.logger = logger;
        }

        public void Register(IBackgroundTask task)
        {
            tasks.Add(task);
        }

        public void Initialize()
        {
            if (isDisabled)
                return;

            foreach (var service in tasks)
            {
                try
                {
                    service.Initialize();
                }
                catch (Exception ex)
                {
                    ex.Log(logger);
                }
            }

            timer.Start();
        }

        public void Reset()
        {
            if (isDisabled)
                return;

            foreach (var service in tasks)
            {
                try
                {
                    service.Reset();
                }
                catch (Exception ex)
                {
                    ex.Log(logger);
                }
            }
        }

        private void Process()
        {
            if (isDisabled)
                return;

            foreach (var service in tasks)
            {
                try
                {
                    service.Process();
                }
                catch (Exception ex)
                {
                    ex.Log(logger);
                }
            }
        }

        //public static void LogToDb(BackgroundTaskLogRow logRow, IExceptionLogger logger = null)
        //{
        //    try
        //    {
        //        using (var connection = AppStatics.SqlConnections.NewFor<BackgroundTaskLogRow>())
        //        {
        //            logRow.Server = Environment.MachineName;
        //            connection.Insert(logRow);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Log(logger);
        //    }
        //}

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            Process();
        }

    }

    public class BackgroundTaskSettings
    {
        public const string SectionKey = "BackgroundTasks";

        public bool Enabled { get; set; }
    }
}