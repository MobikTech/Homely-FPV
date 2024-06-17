using FpvDroneSimulator.Logic.Respawn;
using FpvDroneSimulator.Services.InputProvider;
using FpvDroneSimulator.Services.LevelInfoHolder;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.Logic.DroneController.Respawn
{
    public class RespawnHandler : MonoBehaviour
    {
        [Inject] private IInputProvider inputProvider;
        [Inject] private ILevelInfoHolder levelInfoHolder;
        
        [SerializeField] private Rigidbody droneRigidbody;
        private RespawnPointHolder respawnPointHolder;

        private void Start()
        {
            respawnPointHolder = levelInfoHolder.LevelInstance.GetComponent<RespawnPointHolder>();
            inputProvider.OnResetPressed += HandleReset;
        }

        private void OnDestroy()
        {
            inputProvider.OnResetPressed -= HandleReset;
        }

        private void HandleReset()
        {
            transform.position = respawnPointHolder.RespawnPoint.position;
            transform.rotation = respawnPointHolder.RespawnPoint.rotation;
            droneRigidbody.velocity = Vector3.zero;
            droneRigidbody.angularVelocity = Vector3.zero;
        }
    }
}