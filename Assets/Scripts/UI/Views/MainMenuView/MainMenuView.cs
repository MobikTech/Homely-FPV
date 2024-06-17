using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FpvDroneSimulator.UI.Views.MainMenuView
{
    public class MainMenuView : ViewBase
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;

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
            playButton.onClick.AddListener(HandlePlayClick);
            settingsButton.onClick.AddListener(HandleSettingsClick);
            quitButton.onClick.AddListener(HandleQuitClick);
        }

        private void UnsubscribeHandlers()
        {
            playButton.onClick.RemoveListener(HandlePlayClick);
            settingsButton.onClick.RemoveListener(HandleSettingsClick);
            quitButton.onClick.RemoveListener(HandleQuitClick);
        }

        private async void HandlePlayClick()
        {
            await viewVisualizer.Hide<MainMenuView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
            await viewVisualizer.Visualize<LevelSelectionView.LevelSelectionView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
        }

        private async void HandleSettingsClick()
        {
            await viewVisualizer.Hide<MainMenuView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
            await viewVisualizer.Visualize<SettingsView.SettingsView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
        }

        private void HandleQuitClick()
        {
            Application.Quit();
        }
    }
}