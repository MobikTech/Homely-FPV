using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.PersistentData;
using FpvDroneSimulator.Services.SettingsSaveLoader;
using FpvDroneSimulator.UI.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FpvDroneSimulator.UI.Views.SettingsView
{
    public class SettingsView : ViewBase
    {
        [Inject] private IDataSaveLoader<SettingsData> dataSaveLoader;

        [SerializeField] private Button backButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private SettingsDataHandler settingsDataHandler;

        protected override Task Initialize()
        {
            settingsDataHandler.Initialize(dataSaveLoader.Load());
            SubscribeHandlers();
            return Task.CompletedTask;
        }

        protected override void Dispose()
        {
            settingsDataHandler.Dispose();
            UnsubscribeHandlers();
        }

        private void SubscribeHandlers()
        {
            backButton.onClick.AddListener(HandleBackClick);
            saveButton.onClick.AddListener(HandleSaveClick);
        }

        private void UnsubscribeHandlers()
        {
            backButton.onClick.RemoveListener(HandleBackClick);
            saveButton.onClick.RemoveListener(HandleSaveClick);
        }

        private async void HandleBackClick()
        {
            await viewVisualizer.Hide<SettingsView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
            await viewVisualizer.Visualize<MainMenuView.MainMenuView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
        }

        private async void HandleSaveClick()
        {
            await viewVisualizer.Hide<SettingsView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
            dataSaveLoader.Save(settingsDataHandler.GetCurrentValuesData());
            await viewVisualizer.Visualize<MainMenuView.MainMenuView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
        }
    }
}