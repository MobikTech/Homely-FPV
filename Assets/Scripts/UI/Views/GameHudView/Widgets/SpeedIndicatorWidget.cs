using System;
using FpvDroneSimulator.Services.LevelInfoHolder;
using TMPro;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.UI.Views.GameHudView.Widgets
{
    public class SpeedIndicatorWidget : MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private ILevelInfoHolder levelInfoHolder;
        [SerializeField] private TMP_Text speedText;
        private Rigidbody droneRigidbody;

        private bool initialized;

        public void Initialize()
        {
            droneRigidbody = levelInfoHolder.DroneInstance.GetComponent<Rigidbody>();
            initialized = true;
        }

        public void Dispose()
        {
            initialized = false;
        }

        private void Update()
        {
            if (!initialized)
            {
                return;
            }
            
            SetSpeed();
        }

        private void SetSpeed()
        {
            speedText.text = Math.Round(droneRigidbody.velocity.magnitude, 2).ToString();
        }
    }
}