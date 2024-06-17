using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Extensions;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using FpvDroneSimulator.Logic.LevelProgress.Models;
using FpvDroneSimulator.Services.LevelInfoHolder;

namespace FpvDroneSimulator.Logic.LevelProgress
{
    public class RaceCompletionAwaiter : ILevelCompletionAwaiter, IEventReceiver<OnSessionExitClicked>
    {
        private readonly EventBus eventBus;
        private readonly ILevelInfoHolder levelInfoHolder;
        
        private LevelCheckpointsHolder levelCheckpointsHolder;
        private TaskCompletionSource<bool> sessionCompletionSource;
        private CancellationTokenRegistration sessionCancellationRegistration;
        private TaskCompletionSource<Checkpoint> checkpointReachingCompletionSource;

        public RaceCompletionAwaiter(EventBus eventBus, ILevelInfoHolder levelInfoHolder)
        {
            this.eventBus = eventBus;
            this.levelInfoHolder = levelInfoHolder;
        }

        public void Initialize()
        {
            sessionCompletionSource = new();
            eventBus.Subscribe(this);
            levelCheckpointsHolder = levelInfoHolder.LevelInstance.GetComponent<LevelCheckpointsHolder>();
            levelCheckpointsHolder.CheckpointsObjectsParent.SetActive(true);
        }

        public void Dispose()
        {
            sessionCompletionSource?.TrySetCanceled();
            sessionCompletionSource = null;
            eventBus.Unsubscribe(this);
            levelCheckpointsHolder = null;
        }

        public async Task WaitLevelCompletion(CancellationToken cancellationToken)
        {
            StartRaceFlow(cancellationToken);
            await sessionCompletionSource.AttachCancellationToken(cancellationToken).Task;
        }

        public void OnEventHappened(OnSessionExitClicked @event)
        {
            sessionCompletionSource.SetResult(false);
        }

        private async Task StartRaceFlow(CancellationToken cancellationToken)
        {
            void HandleCheckpointReached(Checkpoint checkpoint)
            {
                checkpointReachingCompletionSource.SetResult(checkpoint);
            }
            
            foreach (LapInfo lapInfo in levelCheckpointsHolder.LevelLaps)
            {
                lapInfo.Checkpoints.ForEach(checkpoint => checkpoint.Initialize());
                
                foreach (Checkpoint checkpoint in lapInfo.Checkpoints)
                {
                    eventBus.Raise(new OnRaceProgressChanged
                    {
                        CurrentLap = levelCheckpointsHolder.LevelLaps.IndexOf(lapInfo),
                        MaxLaps = levelCheckpointsHolder.LevelLaps.Count,
                        CurrentCheckpoint = lapInfo.Checkpoints.IndexOf(checkpoint),
                        MaxCheckpoints = lapInfo.Checkpoints.Count
                    });
                    
                    checkpointReachingCompletionSource = new();
                    checkpointReachingCompletionSource.AttachCancellationToken(cancellationToken);
                    checkpoint.OnCheckpointReached += HandleCheckpointReached;
                    checkpoint.SetInteractive(true);

                    await checkpointReachingCompletionSource.Task;
                    
                    checkpoint.OnCheckpointReached -= HandleCheckpointReached;
                    checkpoint.SetInteractive(false);
                    checkpointReachingCompletionSource = null;
                }
                
                lapInfo.Checkpoints.ForEach(checkpoint => checkpoint.Dispose());
            }
            
            sessionCompletionSource.SetResult(true);
        }
    }
}