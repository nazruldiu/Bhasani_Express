
namespace BMS_Scheduler.Common.Services
{
    public interface IBackgroundTaskManager
    {
        void Initialize();
        void Register(IBackgroundTask task);
        void Reset();
    }
}
