using System;
using System.Threading;
using System.Threading.Tasks;

namespace FpvDroneSimulator.Common.Extensions
{
    public static class TaskExtensions
    {
        public static async Task WaitUntil(Func<bool> predicate, CancellationToken cancellationToken = default)
        {
            await WaitWhile(() => !predicate(), cancellationToken);
        }

        public static async Task WaitWhile(Func<bool> predicate, CancellationToken cancellationToken = default)
        {
            while (predicate() && !cancellationToken.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }
        
        public static TaskCompletionSource<T> AttachCancellationToken<T>(this TaskCompletionSource<T> taskCompletionSource, CancellationToken cancellationToken)
        {
            CancellationTokenRegistration registration = default;
            void HandleCancellation()
            {
                taskCompletionSource.TrySetCanceled();
                registration.Dispose();
            }

            registration = cancellationToken.Register(HandleCancellation);
            return taskCompletionSource;
        }
    }
}