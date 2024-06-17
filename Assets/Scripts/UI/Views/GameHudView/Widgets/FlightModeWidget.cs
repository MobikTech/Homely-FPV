using System;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.UI.Views.GameHudView.Widgets
{
    public class FlightModeWidget : MonoBehaviour, IInitializable, IDisposable, IEventReceiver<OnFlightModeChanged>
    {
        [Inject] private EventBus eventBus;
        
        [SerializeField] private TMP_Text modeNameText;
        
        public void Initialize()
        {
            eventBus.Subscribe(this);
        }

        public void Dispose()
        {
            eventBus.Unsubscribe(this);
        }

        public void OnEventHappened(OnFlightModeChanged @event)
        {
            modeNameText.text = @event.FlightMode.ToString();
        }
    }
}