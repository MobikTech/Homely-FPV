using System;
using UnityEngine;

namespace FpvDroneSimulator.Services.GamePauseService
{
    public class GamePauseService : IGamePauseService
    {
        public event Action<bool> OnPauseStateChanged;

        public bool IsGamePaused
        {
            get => isGamePaused;
            set
            {
                if (value == isGamePaused)
                {
                    return;
                }

                isGamePaused = value;
                SetGamePauseState(isGamePaused);
                OnPauseStateChanged?.Invoke(isGamePaused);
            }
        }

        private bool isGamePaused;

        private void SetGamePauseState(bool pauseState)
        {
            Time.timeScale = pauseState ? 0 : 1;
        }
    }
}