using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using FpvDroneSimulator.GameStates.Core;
using FpvDroneSimulator.GameStates.Options;
using FpvDroneSimulator.PersistentData;
using FpvDroneSimulator.Services.SettingsSaveLoader;
using FpvDroneSimulator.UI.Core;
using FpvDroneSimulator.UI.Options;
using FpvDroneSimulator.UI.Views.ResultView;

namespace FpvDroneSimulator.GameStates.GameStates
{
    public class LevelResultState : IGameState, IEventReceiver<OnResultContinued>
    {
        private readonly IGameStateSwitcher gameStateSwitcher;
        private readonly IViewVisualizer viewVisualizer;
        private readonly EventBus eventBus;
        private readonly IDataSaveLoader<RecordData> recordDataSaveLoader;

        public LevelResultState(IGameStateSwitcher gameStateSwitcher, IViewVisualizer viewVisualizer, EventBus eventBus, IDataSaveLoader<RecordData> recordDataSaveLoader)
        {
            this.gameStateSwitcher = gameStateSwitcher;
            this.viewVisualizer = viewVisualizer;
            this.eventBus = eventBus;
            this.recordDataSaveLoader = recordDataSaveLoader;
        }

        public async Task Run(CancellationToken cancellationToken, IOptions options = null)
        {
            LevelResultStateOptions stateOptions = options as LevelResultStateOptions;
            if (stateOptions == null)
            {
                gameStateSwitcher.SwitchState<MainMenuState>();
            }
            else
            {
                await ShowResultScreen(stateOptions, cancellationToken);
            }
        }

        public void Dispose()
        {
            eventBus.Unsubscribe(this);
        }

        private async Task ShowResultScreen(LevelResultStateOptions stateOptions, CancellationToken cancellationToken)
        {
            RecordData recordData = recordDataSaveLoader.Load();
            string recordTime = recordData.RecordTimePerLevel[(int)stateOptions.GameLevel];
            string currentTime = stateOptions.SessionTimeSpent.ToString(@"hh\:mm\:ss");
            
            eventBus.Subscribe(this);
            await viewVisualizer.Visualize<ResultView, ResultViewOpenOptions>(new ResultViewOpenOptions()
            {
                BestTime = string.IsNullOrEmpty(recordTime) ? currentTime : recordTime,
                CurrentTime = currentTime
            }, cancellationToken);

            if (string.IsNullOrEmpty(recordTime) || stateOptions.SessionTimeSpent < TimeSpan.ParseExact(recordTime, @"hh\:mm\:ss", CultureInfo.InvariantCulture))
            {
                recordData.RecordTimePerLevel[(int)stateOptions.GameLevel] = currentTime;
                recordDataSaveLoader.Save(recordData);
            }
        }

        public async void OnEventHappened(OnResultContinued @event)
        {
            await viewVisualizer.Hide<ResultView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
            gameStateSwitcher.SwitchState<MainMenuState>();
        }
    }
}