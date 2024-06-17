using System;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.GameStates.Core;
using FpvDroneSimulator.GameStates.Options;
using FpvDroneSimulator.Logic;
using FpvDroneSimulator.Logic.LevelProgress;
using FpvDroneSimulator.Logic.LevelProgress.Freefly;
using FpvDroneSimulator.Logic.Respawn;
using FpvDroneSimulator.Services.AssetLoadingService;
using FpvDroneSimulator.Services.LevelInfoHolder;
using FpvDroneSimulator.Services.SceneUIRootHolder;
using FpvDroneSimulator.UI.Core;
using FpvDroneSimulator.UI.Views.GameHudView;
using UnityEngine;

namespace FpvDroneSimulator.GameStates.GameStates
{
    public class LevelLoadingState : IGameState
    {
        private readonly IGameStateSwitcher gameStateSwitcher;
        private readonly IAssetLoadingService assetLoadingService;
        private readonly SceneRootsHolder sceneRootsHolder;
        private readonly IViewVisualizer viewVisualizer;
        private readonly ILevelInfoHolder levelInfoHolder;
        private readonly RaceCompletionAwaiter raceCompletionAwaiter;
        private readonly FreeflyCompletionAwaiter freeflyCompletionAwaiter;

        public LevelLoadingState(IGameStateSwitcher gameStateSwitcher, IAssetLoadingService assetLoadingService, SceneRootsHolder sceneRootsHolder, IViewVisualizer viewVisualizer, 
            ILevelInfoHolder levelInfoHolder, RaceCompletionAwaiter raceCompletionAwaiter, FreeflyCompletionAwaiter freeflyCompletionAwaiter)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.assetLoadingService = assetLoadingService;
            this.sceneRootsHolder = sceneRootsHolder;
            this.viewVisualizer = viewVisualizer;
            this.levelInfoHolder = levelInfoHolder;
            this.raceCompletionAwaiter = raceCompletionAwaiter;
            this.freeflyCompletionAwaiter = freeflyCompletionAwaiter;
        }

        public async Task Run(CancellationToken cancellationToken, IOptions options = null)
        {
            LevelLoadingStateOptions stateOptions = options as LevelLoadingStateOptions;
            GameObject levelInstance = await assetLoadingService.InstantiateGameObjectAfterLoading(stateOptions.GameLevel.ToString(), sceneRootsHolder.EnvironmentRootObject);
            GameObject droneInstance = await assetLoadingService.InstantiateGameObjectAfterLoading("Drone", sceneRootsHolder.PlayerRootObject, 
                levelInstance.GetComponent<RespawnPointHolder>().RespawnPoint.position, Quaternion.identity);
            
            levelInfoHolder.LevelInstance = levelInstance;
            levelInfoHolder.DroneInstance = droneInstance;
            levelInfoHolder.GameLevel = stateOptions.GameLevel;
            levelInfoHolder.GameLevelMode = stateOptions.GameLevelMode;

            GetLevelCompletionAwaiter().Initialize();

            await viewVisualizer.Visualize<GameHudView, IOptions>(IOptions.NoneOptions, cancellationToken);
            gameStateSwitcher.SwitchState<LevelGameplayState>();
        }

        public void Dispose()
        {
            
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