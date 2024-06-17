using System;
using System.Threading;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Common.Utilities.Logging;
using ILogger = FpvDroneSimulator.Common.Utilities.Logging.ILogger;

namespace FpvDroneSimulator.GameStates.Core
{
    public class GameStateMachine : IGameStateSwitcher
    {
        public IGameState CurrentState { get; private set; } = null;

        private readonly GameStateFactory gameStateFactory;
        private readonly ILogger logger;

        private CancellationTokenSource currentCts;

        public GameStateMachine(GameStateFactory gameStateFactory, ILogger logger)
        {
            this.gameStateFactory = gameStateFactory;
            this.logger = logger;
        }

        public async void SwitchState<TState>(IOptions options = null) where TState : class, IGameState
        {
            if (CurrentState != null)
            {
                currentCts.Cancel();
                currentCts.Dispose();
                logger.Log($"{CurrentState.GetType().Name}.Dispose", LogCategory.GameState);
                CurrentState.Dispose();
            }

            TState newState = gameStateFactory.Create<TState>();
            CurrentState = newState;
            currentCts = new CancellationTokenSource();
            logger.Log($"{CurrentState.GetType().Name}.Run", LogCategory.GameState);
            try
            {
                await CurrentState.Run(currentCts.Token, options);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}