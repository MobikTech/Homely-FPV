using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using FpvDroneSimulator.GameStates.Core;
using FpvDroneSimulator.GameStates.Options;
using FpvDroneSimulator.Services.InputProvider;
using FpvDroneSimulator.UI.Core;
using FpvDroneSimulator.UI.Views.LevelSelectionView;
using FpvDroneSimulator.UI.Views.MainMenuView;

namespace FpvDroneSimulator.GameStates.GameStates
{
    public class MainMenuState : IGameState, IEventReceiver<OnLevelSelected>
    {
        private readonly IGameStateSwitcher gameStateSwitcher;
        private readonly IViewVisualizer viewVisualizer;
        private readonly EventBus eventBus;
        private readonly IInputProvider inputProvider;

        public MainMenuState(IGameStateSwitcher gameStateSwitcher, IViewVisualizer viewVisualizer, EventBus eventBus,
            IInputProvider inputProvider)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.viewVisualizer = viewVisualizer;
            this.eventBus = eventBus;
            this.inputProvider = inputProvider;
        }

        public async Task Run(CancellationToken cancellationToken, IOptions options = null)
        {
            eventBus.Subscribe(this);
            await viewVisualizer.Visualize<MainMenuView, IOptions>(IOptions.NoneOptions, cancellationToken);
        }

        public void Dispose()
        {
            eventBus.Unsubscribe(this);
        }

        public async void OnEventHappened(OnLevelSelected @event)
        {
            await viewVisualizer.Hide<LevelSelectionView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
            gameStateSwitcher.SwitchState<LevelLoadingState>(new LevelLoadingStateOptions { GameLevel = @event.GameLevel, GameLevelMode = @event.GameLevelMode });
        }
    }
}