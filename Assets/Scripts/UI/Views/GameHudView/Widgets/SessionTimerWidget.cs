using System;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Services.GamePauseService;
using TMPro;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.UI.Views.GameHudView.Widgets
{
    public class SessionTimerWidget : MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private IGamePauseService gamePauseService;

        public TimeSpan CurrentTime { get; private set; }
        [SerializeField] private TMP_Text timerText;
        private CancellationTokenSource cancellationTokenSource;
        
        public void Initialize()
        {
            CurrentTime = TimeSpan.Zero;
            cancellationTokenSource = new();
            SetTime(CurrentTime);
            StartTimer(cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        private async Task StartTimer(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1f), cancellationToken);
                if (gamePauseService.IsGamePaused)
                {
                    continue;
                }
                CurrentTime += TimeSpan.FromSeconds(1f);
                SetTime(CurrentTime);
            }
        }

        private void SetTime(TimeSpan timeSpan)
        {
            timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }
}