using System;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.GameStates.Core;
using FpvDroneSimulator.Logic;
using FpvDroneSimulator.Logic.LevelProgress;
using FpvDroneSimulator.Logic.LevelProgress.Freefly;
using FpvDroneSimulator.Services.GamePauseService;
using FpvDroneSimulator.Services.InputProvider;
using FpvDroneSimulator.Services.LevelInfoHolder;
using FpvDroneSimulator.UI.Core;
using FpvDroneSimulator.UI.Views.SessionView;

namespace FpvDroneSimulator.GameStates.GameStates
{
    public class LevelGameplayState : IGameState
    {
        private readonly IGameStateSwitcher gameStateSwitcher;
        private readonly ILevelInfoHolder levelInfoHolder;
        private readonly IViewVisualizer viewVisualizer;
        private readonly IInputProvider inputProvider;
        private readonly IGamePauseService gamePauseService;
        private readonly RaceCompletionAwaiter raceCompletionAwaiter;
        private readonly FreeflyCompletionAwaiter freeflyCompletionAwaiter;

        private bool isSessionViewShown;

        public LevelGameplayState(IGameStateSwitcher gameStateSwitcher, ILevelInfoHolder levelInfoHolder, IViewVisualizer viewVisualizer, IInputProvider inputProvider, 
            IGamePauseService gamePauseService, RaceCompletionAwaiter raceCompletionAwaiter, FreeflyCompletionAwaiter freeflyCompletionAwaiter)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.levelInfoHolder = levelInfoHolder;
            this.viewVisualizer = viewVisualizer;
            this.inputProvider = inputProvider;
            this.gamePauseService = gamePauseService;
            this.raceCompletionAwaiter = raceCompletionAwaiter;
            this.freeflyCompletionAwaiter = freeflyCompletionAwaiter;
        }

        public async Task Run(CancellationToken cancellationToken, IOptions options = null)
        {
            inputProvider.SwitchScheme(InputSchemeType.LevelScheme);
            inputProvider.OnBackPressed += HandleBackPressed;
            await GetLevelCompletionAwaiter().WaitLevelCompletion(cancellationToken);
            gameStateSwitcher.SwitchState<LevelDisposeState>();
        }

        public void Dispose()
        {
            gamePauseService.IsGamePaused = false;
            inputProvider.OnBackPressed -= HandleBackPressed;
        }

        private async void HandleBackPressed()
        {
            void HandleSessionViewClosed(IView view)
            {
                isSessionViewShown = false;
                view.OnClosed -= HandleSessionViewClosed;
            }
            
            if (!isSessionViewShown)
            {
                await viewVisualizer.Visualize<SessionView, IOptions>(IOptions.NoneOptions, CancellationToken.None, true);
                viewVisualizer.GetVisualizedView<SessionView>().OnClosed += HandleSessionViewClosed;
                isSessionViewShown = true;
                gamePauseService.IsGamePaused = true;
                inputProvider.SwitchScheme(InputSchemeType.None);
            }
            else
            {
                gamePauseService.IsGamePaused = false;
                inputProvider.SwitchScheme(InputSchemeType.LevelScheme);
                await viewVisualizer.Hide<SessionView, IOptions>(IOptions.NoneOptions, CancellationToken.None, true);
            }
        }

        private ILevelCompletionAwaiter GetLevelCompletionAwaiter()
        {
            return levelInfoHolder.GameLevelMode switch
            {
                GameLevelMode.FreeFly => freeflyCompletionAwaiter,
                GameLevelMode.Race => raceCompletionAwaiter,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}