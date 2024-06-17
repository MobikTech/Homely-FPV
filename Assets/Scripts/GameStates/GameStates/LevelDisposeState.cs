using System;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.GameStates.Core;
using FpvDroneSimulator.GameStates.Options;
using FpvDroneSimulator.Logic;
using FpvDroneSimulator.Logic.LevelProgress;
using FpvDroneSimulator.Logic.LevelProgress.Freefly;
using FpvDroneSimulator.Services.AssetLoadingService;
using FpvDroneSimulator.Services.InputProvider;
using FpvDroneSimulator.Services.LevelInfoHolder;
using FpvDroneSimulator.UI.Core;
using FpvDroneSimulator.UI.Views.GameHudView;

namespace FpvDroneSimulator.GameStates.GameStates
{
    public class LevelDisposeState : IGameState
    {
        private readonly IGameStateSwitcher gameStateSwitcher;
        private readonly IViewVisualizer viewVisualizer;
        private readonly IInputProvider inputProvider;
        private readonly IAssetLoadingService assetLoadingService;
        private readonly ILevelInfoHolder levelInfoHolder;
        private readonly RaceCompletionAwaiter raceCompletionAwaiter;
        private readonly FreeflyCompletionAwaiter freeflyCompletionAwaiter;
        
        public LevelDisposeState(IGameStateSwitcher gameStateSwitcher, IViewVisualizer viewVisualizer, IInputProvider inputProvider, IAssetLoadingService assetLoadingService, 
            ILevelInfoHolder levelInfoHolder, RaceCompletionAwaiter raceCompletionAwaiter, FreeflyCompletionAwaiter freeflyCompletionAwaiter)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.viewVisualizer = viewVisualizer;
            this.inputProvider = inputProvider;
            this.assetLoadingService = assetLoadingService;
            this.levelInfoHolder = levelInfoHolder;
            this.raceCompletionAwaiter = raceCompletionAwaiter;
            this.freeflyCompletionAwaiter = freeflyCompletionAwaiter;
        }

        public async Task Run(CancellationToken cancellationToken, IOptions options = null)
        {
            TimeSpan currentSessionTime = viewVisualizer.GetVisualizedView<GameHudView>().CurrentSessionTime;
            await viewVisualizer.Hide<GameHudView, IOptions>(IOptions.NoneOptions, cancellationToken);
            assetLoadingService.DestroyAndUnloadObject(levelInfoHolder.LevelInstance);
            assetLoadingService.DestroyAndUnloadObject(levelInfoHolder.DroneInstance);
            inputProvider.SwitchScheme(InputSchemeType.MainMenuScheme);
            DisposeLevelServices();


            IOptions nextStateOptions = levelInfoHolder.GameLevelMode == GameLevelMode.Race
                ? new LevelResultStateOptions { SessionTimeSpent = currentSessionTime, GameLevel = levelInfoHolder.GameLevel }
                : null;
            gameStateSwitcher.SwitchState<LevelResultState>(nextStateOptions);
        }

        public void Dispose()
        {
           
        }

        private void DisposeLevelServices()
        {
            GetLevelCompletionAwaiter().Dispose();
            levelInfoHolder.LevelInstance = null;
            levelInfoHolder.DroneInstance = null;
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