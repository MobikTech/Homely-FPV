using System.Collections.Generic;
using FpvDroneSimulator.Logic.DroneController.PID;
using FpvDroneSimulator.Logic.DroneController.Propeller;
using UnityEngine;

namespace FpvDroneSimulator.Logic.DroneController
{
    public class PhysicsApplier : MonoBehaviour
    {
        [SerializeField] private DroneInfoHolder droneInfoHolder;
        [SerializeField] private AngleFlightValuesProvider angleFlightValuesProvider;
        [SerializeField] private ManualFlightValuesProvider manualFlightValuesProvider;
        private DronePIDController pidController => droneInfoHolder.PIDController;
        private Rotor rotorCW1 => droneInfoHolder.RotorCW1;
        private Rotor rotorCW2 => droneInfoHolder.RotorCW2;
        private Rotor rotorCCW1 => droneInfoHolder.RotorCCW1;
        private Rotor rotorCCW2 => droneInfoHolder.RotorCCW2;

        private Dictionary<FlightMode, IFlightValuesProvider> flightValuesProviders;
        private FlightValues currentFlightValues;

        private void Awake()
        {
            flightValuesProviders = new()
            {
                { FlightMode.Angle, angleFlightValuesProvider },
                { FlightMode.Manual, manualFlightValuesProvider },
            };
            currentFlightValues = new FlightValues();
        }
        private void FixedUpdate()
        {
            ResetRotors();
            
            // ROTOR FORCES
            currentFlightValues.Roll = 0;
            currentFlightValues.Pitch = 0;
            currentFlightValues.Yaw = 0;
            currentFlightValues.Lift = 0;

            if (pidController.enabled)
            {
                ApplyFlightInputValues();
            }
            
            HandleRotors();
            
            
            // EXTERNAL FORCES (Drag, Air,...)
            ApplyDrag();
        }

        private void ApplyFlightInputValues()
        {
            flightValuesProviders[droneInfoHolder.CurrentFlightMode].ApplyInput(currentFlightValues);
        }

        private void HandleRotors()
        {
            currentFlightValues.Pitch /= 6;
            currentFlightValues.Roll /= 6;
            currentFlightValues.Yaw /= 6;
            
            AddRotorsPower(
                currentFlightValues.Lift - currentFlightValues.Pitch + currentFlightValues.Roll - currentFlightValues.Yaw,
                currentFlightValues.Lift + currentFlightValues.Pitch - currentFlightValues.Roll - currentFlightValues.Yaw,
                currentFlightValues.Lift + currentFlightValues.Pitch + currentFlightValues.Roll + currentFlightValues.Yaw,
                currentFlightValues.Lift - currentFlightValues.Pitch - currentFlightValues.Roll + currentFlightValues.Yaw
            );
            float max = Mathf.Max(rotorCW1.power, rotorCW2.power, rotorCCW1.power, rotorCCW2.power);
            float min = Mathf.Min(rotorCW1.power, rotorCW2.power, rotorCCW1.power, rotorCCW2.power);
            
            if (max > 1 && min < 0) 
            {
                Debug.Log("Los valores de potencia están fuera de los limites previstos:" + "\nMax: " + max + " Min: " + min);
            }

            if (max > 1) AddRotorsPower(1 - max);
            if (min < 0) AddRotorsPower(-min);
        }

        private void ResetRotors() => SetRotorsPower(0);

        private void ClampPower01()
        {
            rotorCW1.power = Mathf.Clamp01(rotorCW1.power);
            rotorCW2.power = Mathf.Clamp01(rotorCW2.power);
            rotorCCW1.power = Mathf.Clamp01(rotorCCW1.power);
            rotorCCW2.power = Mathf.Clamp01(rotorCCW2.power);
        }

        private void SetRotorsPower(float cw1, float cw2, float ccw1, float ccw2)
        {
            rotorCW1.power = Mathf.Clamp01(cw1);
            rotorCW2.power = Mathf.Clamp01(cw2);
            rotorCCW1.power = Mathf.Clamp01(ccw1);
            rotorCCW2.power = Mathf.Clamp01(ccw2);
        }
        private void SetRotorsPower(float value) => SetRotorsPower(value, value, value, value);

        private void AddRotorsPower(float cw1, float cw2, float ccw1, float ccw2)
        {
            rotorCW1.power += cw1;
            rotorCW2.power += cw2;
            rotorCCW1.power += ccw1;
            rotorCCW2.power += ccw2;
        }
        private void AddRotorsPower(float value) => AddRotorsPower(value, value, value, value);
        
        private void ApplyDrag()
        {
            return;
        }
    }
}