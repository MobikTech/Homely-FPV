using System;

namespace FpvDroneSimulator.Services.GamePauseService
{
    public interface IGamePauseService
    {
        public event Action<bool> OnPauseStateChanged;
        public bool IsGamePaused { get; set; }
    }
}