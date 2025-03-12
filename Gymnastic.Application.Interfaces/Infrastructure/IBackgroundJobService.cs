using System.Linq.Expressions;

namespace Gymnastic.Application.Interface.Infrastructure
{
    public interface IBackgroundJobService
    {
        /// <summary>
        /// Enqueues a job to be executed immediately in the background.
        /// </summary>
        /// <typeparam name="T">The type (typically a service interface) that contains the method to call.</typeparam>
        /// <param name="methodCall">The method to execute, expressed as a lambda (e.g., service => service.Method()).</param>
        void Enqueue<T>(Expression<Action<T>> methodCall);

        /// <summary>
        /// Enqueues a job to be executed immediately and returns the job ID.
        /// </summary>
        /// <typeparam name="T">The type that contains the method to call.</typeparam>
        /// <param name="methodCall">The method to execute.</param>
        /// <returns>The ID of the enqueued job.</returns>
        string EnqueueWithId<T>(Expression<Action<T>> methodCall);

        /// <summary>
        /// Enqueues an async job to be executed immediately in the background.
        /// </summary>
        /// <typeparam name="T">The type that contains the async method to call.</typeparam>
        /// <param name="methodCall">The async method to execute (e.g., service => service.AsyncMethod()).</param>
        void Enqueue<T>(Expression<Func<T, Task>> methodCall);

        /// <summary>
        /// Enqueues an async job to be executed immediately and returns the job ID.
        /// </summary>
        /// <typeparam name="T">The type that contains the async method to call.</typeparam>
        /// <param name="methodCall">The async method to execute.</param>
        /// <returns>The ID of the enqueued job.</returns>
        string EnqueueWithId<T>(Expression<Func<T, Task>> methodCall);

        /// <summary>
        /// Schedules a job to be executed after a specified delay.
        /// </summary>
        /// <typeparam name="T">The type that contains the method to call.</typeparam>
        /// <param name="methodCall">The method to execute.</param>
        /// <param name="delay">The time delay before execution.</param>
        void Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);

        /// <summary>
        /// Schedules a job to be executed after a specified delay and returns the job ID.
        /// </summary>
        /// <typeparam name="T">The type that contains the method to call.</typeparam>
        /// <param name="methodCall">The method to execute.</param>
        /// <param name="delay">The time delay before execution.</param>
        /// <returns>The ID of the scheduled job.</returns>
        string ScheduleWithId<T>(Expression<Action<T>> methodCall, TimeSpan delay);

        /// <summary>
        /// Schedules an async job to be executed after a specified delay.
        /// </summary>
        /// <typeparam name="T">The type that contains the async method to call.</typeparam>
        /// <param name="methodCall">The async method to execute.</param>
        /// <param name="delay">The time delay before execution.</param>
        void Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);

        /// <summary>
        /// Schedules an async job to be executed after a specified delay and returns the job ID.
        /// </summary>
        /// <typeparam name="T">The type that contains the async method to call.</typeparam>
        /// <param name="methodCall">The async method to execute.</param>
        /// <param name="delay">The time delay before execution.</param>
        /// <returns>The ID of the scheduled job.</returns>
        string ScheduleWithId<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);

        /// <summary>
        /// Adds or updates a recurring job that runs on a specified schedule (e.g., using a cron expression).
        /// </summary>
        /// <param name="jobId">A unique identifier for the recurring job.</param>
        /// <typeparam name="T">The type that contains the method to call.</typeparam>
        /// <param name="methodCall">The method to execute.</param>
        /// <param name="cronExpression">A cron expression defining the schedule (e.g., "0 0 * * *" for daily at midnight).</param>
        void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> methodCall, string cronExpression);

        /// <summary>
        /// Adds or updates a recurring async job that runs on a specified schedule.
        /// </summary>
        /// <param name="jobId">A unique identifier for the recurring job.</param>
        /// <typeparam name="T">The type that contains the async method to call.</typeparam>
        /// <param name="methodCall">The async method to execute.</param>
        /// <param name="cronExpression">A cron expression defining the schedule.</param>
        void AddOrUpdateRecurring<T>(string jobId, Expression<Func<T, Task>> methodCall, string cronExpression);

        /// <summary>
        /// Enqueues a continuation job that runs after a parent job completes successfully.
        /// </summary>
        /// <typeparam name="T">The type that contains the method to call.</typeparam>
        /// <param name="parentJobId">The ID of the parent job that must complete first.</param>
        /// <param name="methodCall">The method to execute after the parent job.</param>
        void ContinueWith<T>(string parentJobId, Expression<Action<T>> methodCall);

        /// <summary>
        /// Enqueues a continuation job that runs after a parent job completes successfully and returns the continuation job ID.
        /// </summary>
        /// <typeparam name="T">The type that contains the method to call.</typeparam>
        /// <param name="parentJobId">The ID of the parent job that must complete first.</param>
        /// <param name="methodCall">The method to execute after the parent job.</param>
        /// <returns>The ID of the continuation job.</returns>
        string ContinueWithId<T>(string parentJobId, Expression<Action<T>> methodCall);

        /// <summary>
        /// Enqueues an async continuation job that runs after a parent job completes successfully.
        /// </summary>
        /// <typeparam name="T">The type that contains the async method to call.</typeparam>
        /// <param name="parentJobId">The ID of the parent job that must complete first.</param>
        /// <param name="methodCall">The async method to execute after the parent job.</param>
        void ContinueWith<T>(string parentJobId, Expression<Func<T, Task>> methodCall);

        /// <summary>
        /// Enqueues an async continuation job that runs after a parent job completes successfully and returns the continuation job ID.
        /// </summary>
        /// <typeparam name="T">The type that contains the async method to call.</typeparam>
        /// <param name="parentJobId">The ID of the parent job that must complete first.</param>
        /// <param name="methodCall">The async method to execute after the parent job.</param>
        /// <returns>The ID of the continuation job.</returns>
        string ContinueWithId<T>(string parentJobId, Expression<Func<T, Task>> methodCall);

        /// <summary>
        /// Deletes a scheduled or recurring job by its ID.
        /// </summary>
        /// <param name="jobId">The ID of the job to delete.</param>
        void Delete(string jobId);
    }
}
