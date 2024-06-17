using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using FpvDroneSimulator.UI.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FpvDroneSimulator.UI.Views.LevelSelectionView
{
    public class LevelSelectionView : ViewBase
    {
        [Inject] private EventBus eventBus;

        [SerializeField] private Button backButton;
        [SerializeField] private Button playButton;
        [SerializeField] private ToggleGroup levelModeToggleGroup;
        [SerializeField] private ToggleGroup levelToggleGroup;

        protected override Task Initialize()
        {
            SubscribeHandlers();
            return Task.CompletedTask;
        }

        protected override void Dispose()
        {
            UnsubscribeHandlers();
        }

        private void SubscribeHandlers()
        {
            backButton.onClick.AddListener(HandleBackClick);
            playButton.onClick.AddListener(HandlePlayClick);
        }

        private void UnsubscribeHandlers()
        {
            backButton.onClick.RemoveListener(HandleBackClick);
            playButton.onClick.RemoveListener(HandlePlayClick);
        }

        private async void HandleBackClick()
        {
            await viewVisualizer.Hide<LevelSelectionView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
            await viewVisualizer.Visualize<MainMenuView.MainMenuView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
        }

        private void HandlePlayClick()
        {
            GameLevelItem gameLevelItem = levelToggleGroup.GetFirstActiveToggle().GetComponent<GameLevelItem>();
            GameLevelModeItem gameLevelModeItem = levelModeToggleGroup.GetFirstActiveToggle().GetComponent<GameLevelModeItem>();
            eventBus.Raise(new OnLevelSelected
            {
                GameLevel = gameLevelItem.GameLevel,
                GameLevelMode = gameLevelModeItem.GameLevelMode
            });
        }
    }
}