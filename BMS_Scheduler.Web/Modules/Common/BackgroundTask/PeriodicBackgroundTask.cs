using Microsoft.Extensions.Logging;
using Serenity;
using Serenity.Abstractions;
using System;
using System.Globalization;
using System.Threading;

namespace BMS_Scheduler.Common.Services
{
    public abstract class PeriodicBackgroundTask : IBackgroundTask
    {
        protected object sync = new object();
        private bool inProgress;
        protected DateTime nextRun;

        protected abstract TimeSpan GetInterval();
        protected abstract void InternalRun();

        protected ILogger Log { get; }
        protected IExceptionLogger ExceptionLog { get; }

        protected PeriodicBackgroundTask(ILogger log, IExceptionLogger exceptionLog)
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
                nextRun = DateTime.Now.Add(GetInterval());
                Log?.LogInformation("Reset: " + GetType().Name + " is scheduled for " + nextRun);
            }
        }

        public void Process()
        {
            lock (sync)
            {
                if (inProgress)
                    return;

                if (DateTime.Now < nextRun)
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
            Log?.LogInformation("Run: Executing " + this.GetType().Name + " now...", this.GetType());

            var prm = item as RunParameters;

            lock (sync)
            {
                if (inProgress)
                    return;

                inProgress = true;
            }
            try
            {
                try
                {
                    var culture = item as CultureInfo;
                    Thread.CurrentThread.CurrentCulture = prm.CurrentCulture;
                    Thread.CurrentThread.CurrentUICulture = prm.CurrentUICulture;
                    nextRun = DateTime.Now.Add(GetInterval());
                    InternalRun();

                    Log?.LogInformation("Run: Done executing " + this.GetType().Name + ".", this.GetType());
                }
                catch (Exception ex)
                {
                    Log?.LogInformation("Run: Error while executing " + this.GetType().Name + ".", this.GetType());
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
    }
}