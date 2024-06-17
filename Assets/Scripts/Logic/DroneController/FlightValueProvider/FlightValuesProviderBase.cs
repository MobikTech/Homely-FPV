using FpvDroneSimulator.Services.InputProvider;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.Logic.DroneController
{
    public abstract class FlightValuesProviderBase : MonoBehaviour, IFlightValuesProvider
    {
        [Inject] private IInputProvider inputProvider;
        [SerializeField] protected DroneInfoHolder droneInfoHolder;

        protected float rollInput => inputProvider.GetRightStickValue().x;
        protected float pitchInput => inputProvider.GetRightStickValue().y;
        protected float yawInput => inputProvider.GetLeftStickValue().x;
        protected float liftInput => inputProvider.GetLeftStickValue().y;
        
        public abstract void ApplyInput(FlightValues flightValues);
    }
}