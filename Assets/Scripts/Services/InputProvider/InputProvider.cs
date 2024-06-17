using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FpvDroneSimulator.Services.InputProvider
{
    public class InputProvider : IInputProvider
    {
        public event Action OnBackPressed;
        public event Action OnResetPressed;
        public event Action OnFlightModeChanged;
        public InputSchemeType CurrentInputScheme { get; private set; } = InputSchemeType.None;
        private Controls controls;

        public void Initialize()
        {
            controls = new Controls();
            Enum.GetValues(typeof(InputSchemeType)).Cast<InputSchemeType>().ToList().ForEach(scheme => SetSchemeEnabled(scheme, false));
            SubscribeHandlers();
            controls.General.Enable();
        }

        public void Dispose()
        {
            UnsubscribeHandlers();
            controls.General.Disable();
        }

        public void SwitchScheme(InputSchemeType inputSchemeType)
        {
            if (CurrentInputScheme == inputSchemeType)
            {
                return;
            }
            SetSchemeEnabled(CurrentInputScheme, false);
            CurrentInputScheme = inputSchemeType;
            SetSchemeEnabled(CurrentInputScheme, true);
        }

        public Vector2 GetRightStickValue()
        {
            return controls.LevelScheme.PitchRollValue.ReadValue<Vector2>();
        }

        public Vector2 GetLeftStickValue()
        {
            return controls.LevelScheme.LiftYawValue.ReadValue<Vector2>();
        }

        private void SubscribeHandlers()
        {
            controls.General.OnBackPressed.performed += HandleBackPressed;
            controls.LevelScheme.OnResetPressed.performed += HandleResetPressed;
            controls.LevelScheme.OnFlightModeChanged.performed += HandleFlightModeChanged;
        }

        private void UnsubscribeHandlers()
        {
            controls.General.OnBackPressed.performed -= HandleBackPressed;
            controls.LevelScheme.OnResetPressed.performed -= HandleResetPressed;
            controls.LevelScheme.OnFlightModeChanged.performed -= HandleFlightModeChanged;
        }

        private void SetSchemeEnabled(InputSchemeType inputSchemeType, bool enabled)
        {
            switch (inputSchemeType)
            {
                case InputSchemeType.None:
                    controls.MenuScheme.Disable();
                    controls.LevelScheme.Disable();
                    break;
                case InputSchemeType.MainMenuScheme:
                    if (enabled)
                    {
                        controls.MenuScheme.Enable();
                    }
                    else
                    {
                        controls.MenuScheme.Disable();
                    }
                    break;
                case InputSchemeType.LevelScheme:
                    if (enabled)
                    {
                        controls.LevelScheme.Enable();
                    }
                    else
                    {
                        controls.LevelScheme.Disable();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(inputSchemeType), inputSchemeType, null);
            }
        }
        
        private void HandleBackPressed(InputAction.CallbackContext _)
        {
            OnBackPressed?.Invoke();
        }
        
        private void HandleResetPressed(InputAction.CallbackContext _)
        {
            OnResetPressed?.Invoke();
        }

        private void HandleFlightModeChanged(InputAction.CallbackContext _)
        {
            OnFlightModeChanged?.Invoke();
        }
    }
}