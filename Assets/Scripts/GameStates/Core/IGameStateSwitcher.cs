using FpvDroneSimulator.Common.Core;

namespace FpvDroneSimulator.GameStates.Core
{
    public interface IGameStateSwitcher
    {
        public void SwitchState<TState>(IOptions options = null) where TState : class, IGameState;
    }
}