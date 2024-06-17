using System;
using System.Collections.Generic;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.UI.Views.GameHudView.Widgets
{
    public class RaceProgressWidget : MonoBehaviour, IInitializable, IDisposable, IEventReceiver<OnRaceProgressChanged>
    {
        [Inject] private EventBus eventBus;
        
        [SerializeField] private List<GameObject> progressIndicators;
        [SerializeField] private TMP_Text lapsText;
        [SerializeField] private TMP_Text checkpointsText;
        
        public void Initialize()
        {
            eventBus.Subscribe(this);
            SetProgress(0, 0, 0, 0);
        }

        public void Dispose()
        {
            eventBus.Unsubscribe(this);
        }

        public void SetActive(bool isActive)
        {
            progressIndicators.ForEach(o => o.SetActive(isActive));
        }

        public void OnEventHappened(OnRaceProgressChanged @event)
        {
            SetProgress(@event.MaxLaps, @event.CurrentLap, @event.MaxCheckpoints, @event.CurrentCheckpoint);
        }

        private void SetProgress(int laps, int lap, int checkpoints, int checkpoint)
        {
            lapsText.text = $"{lap}/{laps}";
            checkpointsText.text = $"{checkpoint}/{checkpoints}";
        }
    }
}