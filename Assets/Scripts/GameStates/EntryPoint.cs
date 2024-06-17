using System.Threading;
using FpvDroneSimulator.GameStates.Core;
using FpvDroneSimulator.GameStates.GameStates;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.GameStates
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private IGameStateSwitcher gameStateSwitcher;

        private void Awake()
        {
            gameStateSwitcher.SwitchState<InitializationState>();
        }
    }
}