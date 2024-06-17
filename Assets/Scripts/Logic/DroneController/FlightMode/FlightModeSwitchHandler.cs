using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using FpvDroneSimulator.Services.InputProvider;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.Logic.DroneController
{
    public class FlightModeSwitchHandler : MonoBehaviour
    {
        [Inject] private EventBus eventBus;
        [Inject] private IInputProvider inputProvider;
   
        public FlightMode CurrentFlightMode { get; private set; }
        
        private void Start()
        {
            inputProvider.OnFlightModeChanged += HandleFlightModeChanged;
        }

        private void OnDestroy()
        {
            inputProvider.OnFlightModeChanged -= HandleFlightModeChanged;
        }

        private void HandleFlightModeChanged()
        {
            switch (CurrentFlightMode)
            {
                case FlightMode.Angle:
                    CurrentFlightMode = FlightMode.Manual;
                    break;
                case FlightMode.Manual:
                    CurrentFlightMode = FlightMode.Angle;
                    break;
            }
            eventBus.Raise(new OnFlightModeChanged { FlightMode = CurrentFlightMode });
        }
    }
}