using Gymnastic.Application.Interface.Infrastructure;
using Hangfire;
using System.Linq.Expressions;


namespace Gymnastic.Infrastructure.BackgroundJobs
{
    public class HangfireService : IBackgroundJobService
    {
        public void Enqueue<T>(Expression<Action<T>> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }

        public string EnqueueWithId<T>(Expression<Action<T>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        public void Enqueue<T>(Expression<Func<T, Task>> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }

        public string EnqueueWithId<T>(Expression<Func<T, Task>> methodCall)
        {
            return BackgroundJob.Enqueue(methodCall);
        }

        public void Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay)
        {
            BackgroundJob.Schedule(methodCall, delay);
        }

        public string ScheduleWithId<T>(Expression<Action<T>> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        public void Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay)
        {
            BackgroundJob.Schedule(methodCall, delay);
        }

        public string ScheduleWithId<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        public void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> methodCall, string cronExpression)
        {
            RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression);
        }

        public void AddOrUpdateRecurring<T>(string jobId, Expression<Func<T, Task>> methodCall, string cronExpression)
        {
            RecurringJob.AddOrUpdate(jobId, methodCall, cronExpression);
        }

        public void ContinueWith<T>(string parentJobId, Expression<Action<T>> methodCall)
        {
            BackgroundJob.ContinueJobWith(parentJobId, methodCall);
        }

        public string ContinueWithId<T>(string parentJobId, Expression<Action<T>> methodCall)
        {
            return BackgroundJob.ContinueJobWith(parentJobId, methodCall);
        }

        public void ContinueWith<T>(string parentJobId, Expression<Func<T, Task>> methodCall)
        {
            BackgroundJob.ContinueJobWith(parentJobId, methodCall);
        }

        public string ContinueWithId<T>(string parentJobId, Expression<Func<T, Task>> methodCall)
        {
            return BackgroundJob.ContinueJobWith(parentJobId, methodCall);
        }

        public void Delete(string jobId)
        {
            BackgroundJob.Delete(jobId); // For scheduled jobs
            RecurringJob.RemoveIfExists(jobId); // For recurring jobs
        }
    }
}
