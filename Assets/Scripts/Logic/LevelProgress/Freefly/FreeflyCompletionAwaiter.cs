using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Extensions;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;

namespace FpvDroneSimulator.Logic.LevelProgress.Freefly
{
    public class FreeflyCompletionAwaiter : ILevelCompletionAwaiter, IEventReceiver<OnSessionExitClicked>
    {
        private readonly EventBus eventBus;
        private TaskCompletionSource<bool> sessionCompletionSource;
        
        public FreeflyCompletionAwaiter(EventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public void Initialize()
        {
            sessionCompletionSource = new();
            eventBus.Subscribe(this);
        }

        public void Dispose()
        {
            sessionCompletionSource = null;
            eventBus.Unsubscribe(this);
        }

        public async Task WaitLevelCompletion(CancellationToken cancellationToken)
        {
            await sessionCompletionSource.AttachCancellationToken(cancellationToken).Task;
        }

        public void OnEventHappened(OnSessionExitClicked @event)
        {
            sessionCompletionSource.SetResult(true);
            sessionCompletionSource = null;
        }
    }
}