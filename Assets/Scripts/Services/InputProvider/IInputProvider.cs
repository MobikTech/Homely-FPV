using System;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.Services.InputProvider
{
    public interface IInputProvider : IInitializable, IDisposable
    {
        public event Action OnBackPressed;
        public event Action OnResetPressed;
        public event Action OnFlightModeChanged;
        public InputSchemeType CurrentInputScheme { get; }
        public void SwitchScheme(InputSchemeType inputSchemeType);
        public Vector2 GetRightStickValue();
        public Vector2 GetLeftStickValue();
    }
}