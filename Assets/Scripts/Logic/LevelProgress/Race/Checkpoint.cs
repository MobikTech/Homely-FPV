using System;
using FpvDroneSimulator.Common.Core;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.Logic.LevelProgress
{
    public class Checkpoint : MonoBehaviour, IInitializable, IDisposable
    {
        public event Action<Checkpoint> OnCheckpointReached;

        [SerializeField] private TriggerListener triggerListener;
        [SerializeField] private string droneTagName = "Player";
        [SerializeField] private GameObject objectForActivatedState;
        [SerializeField] private GameObject objectForDeactivatedState;

        public void Initialize()
        {
            triggerListener.OnTriggerEntered += HandleTriggerEnter;
            SetInteractive(false);
            gameObject.SetActive(true);
        }

        public void Dispose()
        {
            triggerListener.OnTriggerEntered -= HandleTriggerEnter;
            SetInteractive(false);
            gameObject.SetActive(false);
        }
        
        public void SetInteractive(bool interactive)
        {
            triggerListener.enabled = interactive;
            objectForActivatedState.SetActive(interactive);
            objectForDeactivatedState.SetActive(!interactive);
        }
        
        private void HandleTriggerEnter(Collider other)
        {
            if (other.CompareTag(droneTagName))
            {
                OnCheckpointReached?.Invoke(this);
            }
        }
    }
}