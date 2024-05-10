
namespace BMS_Scheduler.Common
{
    public enum TaskType
    {
        Daily = 10,
        Periodic = 20
    }    
    
    public enum BackgroundTaskStatus
    {
        Error = -1,
        Scheduled = 0,
        Success = 1,
        InProgress = 2
    }
}