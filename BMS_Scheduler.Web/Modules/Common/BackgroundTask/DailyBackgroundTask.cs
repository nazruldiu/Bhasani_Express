
using Microsoft.Extensions.Logging;
using Serenity;
using Serenity.Abstractions;
using System;
using System.Globalization;
using System.Threading;

namespace BMS_Scheduler.Common.Services
{
    public abstract class DailyBackgroundTask : IBackgroundTask
    {
        protected object sync = new object();
        private bool inProgress;
        private DateTime nextRun;
        private int retryCount;
        public DateTime NextRunAt => nextRun.Add(GetRunAtTime());

        protected ILogger Log { get; }
        protected IExceptionLogger ExceptionLog { get; }

        protected DailyBackgroundTask(ILogger log, IExceptionLogger exceptionLog)
        {
            Log = log;
            ExceptionLog = exceptionLog;
        }

        public virtual void Initialize()
        {
            Reset();
        }

        public virtual void Reset()
        {
            lock (sync)
            {
                retryCount = 0;
                if (DateTime.Now < DateTime.Today.Add(GetRunAtTime()))
                    nextRun = DateTime.Today;
                else
                    nextRun = DateTime.Today.AddDays(1);

                Log?.LogInformation($"Reset: {GetTaskName()} is scheduled for {NextRunAt}");
                //LogToDb(BackgroundTaskStatus.Scheduled, $"Reset: {GetTaskName()} is scheduled for {NextRunAt}");
            }
        }

        public void Process()
        {
            lock (sync)
            {
                if (inProgress)
                    return;

                var actualNextRun = retryCount == 0 ? NextRunAt : nextRun;

                if (DateTime.Now < actualNextRun)
                    return;
            }

            ThreadPool.QueueUserWorkItem(Run, new RunParameters
            {
                CurrentCulture = CultureInfo.CurrentCulture,
                CurrentUICulture = CultureInfo.CurrentUICulture
            });
        }

        private class RunParameters
        {
            public CultureInfo CurrentCulture { get; set; }
            public CultureInfo CurrentUICulture { get; set; }
        }

        private void Run(object item)
        {
            var prm = item as RunParameters;

            lock (sync)
            {
                if (inProgress)
                    return;

                inProgress = true;
            }
            try
            {
                //LogToDb(BackgroundTaskStatus.InProgress, $"Run: Executing {GetTaskName()} now...");

                try
                {
                    Thread.CurrentThread.CurrentCulture = prm.CurrentCulture;
                    Thread.CurrentThread.CurrentUICulture = prm.CurrentUICulture;

                    InternalRun();
                    nextRun = DateTime.Today.AddDays(1);
                    retryCount = 0;

                    //LogToDb(BackgroundTaskStatus.Success, $"Run: Done executing {GetTaskName()}.");
                    //LogToDb(BackgroundTaskStatus.Scheduled, $"Run: {GetTaskName()} is rescheduled for {NextRunAt}");
                }
                catch (Exception ex)
                {
                    //LogToDb(BackgroundTaskStatus.Error, $"Run: Error while executing {GetTaskName()}.");

                    lock (sync)
                    {
                        if (retryCount < GetMaxRetry())
                        {
                            retryCount++;
                            nextRun = DateTime.Now.AddMinutes(GetRetryInterval());
                        }
                        else
                        {
                            retryCount = 0;
                            nextRun = DateTime.Today.AddDays(1);
                        }
                        //LogToDb(BackgroundTaskStatus.Scheduled, $"Run: {GetTaskName()} is rescheduled for {NextRunAt}");
                    }
                    ex.Log(ExceptionLog);
                }
            }
            finally
            {
                lock (sync)
                {
                    inProgress = false;
                }
            }
        }

        //private void LogToDb(BackgroundTaskStatus status, string msg)
        //{
        //    BackgroundTaskManager.LogToDb(new BackgroundTaskLogRow
        //    {
        //        TaskName = GetTaskName(),
        //        TaskType = TaskType.Daily,
        //        RunAt = DateTime.Now,
        //        NextRunAt = NextRunAt,
        //        Status = status,
        //        Message = msg,
        //    });

        //    //if (status == BackgroundTaskStatus.Error)
        //    //    Serenity.Log.Error(msg, this.GetType());
        //    //else Serenity.Log.Info(msg, this.GetType());
        //}

        protected virtual int GetMaxRetry()
        {
            return 5;
        }

        protected virtual int GetRetryInterval()
        {
            return 10;
        }

        protected virtual string GetTaskName()
        {
            return this.GetType().Name;
        }

        protected abstract void InternalRun();
        protected abstract TimeSpan GetRunAtTime();
    }
}