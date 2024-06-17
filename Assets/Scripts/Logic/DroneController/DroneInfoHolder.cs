using FpvDroneSimulator.Logic.DroneController.Extensions;
using FpvDroneSimulator.Logic.DroneController.PID;
using FpvDroneSimulator.Logic.DroneController.Propeller;
using FpvDroneSimulator.PersistentData;
using FpvDroneSimulator.Services.SettingsSaveLoader;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.Logic.DroneController
{
    public class DroneInfoHolder : MonoBehaviour
    {
        [Inject] private IDataSaveLoader<SettingsData> dataSaveLoader;
        
        [field:SerializeField] public DroneSettingsSO DroneSettings { get; private set; }
        [field:SerializeField] public DronePIDController PIDController { get; private set; }
        [field:SerializeField] public Gyroscope Gyroscope { get; private set; }
        [field:SerializeField] public Accelerometer Accelerometer { get; private set; }
        [field:SerializeField] public Rotor RotorCW1 { get; private set; }
        [field:SerializeField] public Rotor RotorCW2 { get; private set; }
        [field:SerializeField] public Rotor RotorCCW1 { get; private set; }
        [field:SerializeField] public Rotor RotorCCW2 { get; private set; }

        public FlightMode CurrentFlightMode => flightModeSwitchHandler.CurrentFlightMode;
        public float Mass { get => rb.mass; set => rb.mass = value; }
        public float Weight => rb.mass * Physics.gravity.magnitude;
        public float Radius => Vector3.Distance(transform.position, RotorCW1.transform.position);
        // Power required to maintain height
        public float HoverPower => Mathf.Clamp01(Weight / DroneSettings.maxThrottle / 4);

        [SerializeField] private FlightModeSwitchHandler flightModeSwitchHandler;
        private Rigidbody rb;

        private void Start()
        {
            DroneSettings.SetValuesFrom(dataSaveLoader.Load());
            rb = GetComponent<Rigidbody>();
            ApplyRigidbodyParameters();
        }
        
        private void ApplyRigidbodyParameters()
        {
            rb.mass = DroneSettings.mass;
            rb.drag = DroneSettings.maxDragCoefficient;
            rb.angularDrag = DroneSettings.angularDrag;
            rb.maxAngularVelocity = DroneSettings.maxAngularSpeed;
            rb.centerOfMass = Vector3.zero;
        }
    }
}
