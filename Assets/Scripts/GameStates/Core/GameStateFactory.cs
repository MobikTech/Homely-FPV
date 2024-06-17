using Zenject;

namespace FpvDroneSimulator.GameStates.Core
{
    public class GameStateFactory
    {
        private readonly DiContainer container;

        public GameStateFactory(DiContainer container)
        {
            this.container = container;
        }

        public TState Create<TState>()
            where TState : class, IGameState
        {
            return container.Instantiate(typeof(TState)) as TState;
        }
    }
}