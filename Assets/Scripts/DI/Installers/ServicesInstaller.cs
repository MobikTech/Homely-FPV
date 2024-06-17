using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Common.Utilities.Logging;
using FpvDroneSimulator.GameStates.Core;
using FpvDroneSimulator.Logic.LevelProgress;
using FpvDroneSimulator.Logic.LevelProgress.Freefly;
using FpvDroneSimulator.PersistentData;
using FpvDroneSimulator.Services.AssetLoadingService;
using FpvDroneSimulator.Services.GamePauseService;
using FpvDroneSimulator.Services.InputProvider;
using FpvDroneSimulator.Services.LevelInfoHolder;
using FpvDroneSimulator.Services.SettingsSaveLoader;
using FpvDroneSimulator.UI;
using Zenject;
using ILogger = FpvDroneSimulator.Common.Utilities.Logging.ILogger;

namespace FpvDroneSimulator.DI.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILogger>().To<UnityEditorLogger>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputProvider>().AsSingle().NonLazy();
            Container.Bind<IAssetLoadingService>().To<AssetLoadingService>().AsSingle().NonLazy();
            Container.Bind<IGamePauseService>().To<GamePauseService>().AsSingle().NonLazy();
            Container.Bind<ILevelInfoHolder>().To<LevelInfoHolder>().AsSingle().NonLazy();
            Container.Bind<RaceCompletionAwaiter>().ToSelf().AsSingle().NonLazy();
            Container.Bind<FreeflyCompletionAwaiter>().ToSelf().AsSingle().NonLazy();
            Container.Bind<IDefaultDataProvider<SettingsData>>().To<DefaultSettingsProvider>().AsSingle().NonLazy();
            Container.Bind<IDefaultDataProvider<RecordData>>().To<DefaultRecordDataProvider>().AsSingle().NonLazy();
            Container.Bind<IDataSaveLoader<SettingsData>>().To<DataSaveLoader<SettingsData>>().AsSingle().NonLazy();
            Container.Bind<IDataSaveLoader<RecordData>>().To<DataSaveLoader<RecordData>>().AsSingle().NonLazy();

            Container.Bind<GameStateFactory>().ToSelf().AsSingle().NonLazy();
            Container.Bind<IGameStateSwitcher>().To<GameStateMachine>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<EventBus>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UserInterfaceManager>().AsSingle().NonLazy();
        }
    }
}
