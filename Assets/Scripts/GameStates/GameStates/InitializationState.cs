using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.GameStates.Core;
using FpvDroneSimulator.PersistentData;
using FpvDroneSimulator.Services.SettingsSaveLoader;
using Zenject;

namespace FpvDroneSimulator.GameStates.GameStates
{
    public class InitializationState : IGameState
    {
        private readonly IGameStateSwitcher gameStateSwitcher;
        private readonly DiContainer container;

        public InitializationState(IGameStateSwitcher gameStateSwitcher, DiContainer container)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.container = container;
        }

        public Task Run(CancellationToken cancellationToken, IOptions options = null)
        {
            container.Resolve<IDataSaveLoader<SettingsData>>().Initialize();
            container.Resolve<IDataSaveLoader<RecordData>>().Initialize();
            gameStateSwitcher.SwitchState<MainMenuState>();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            
        }
    }
}