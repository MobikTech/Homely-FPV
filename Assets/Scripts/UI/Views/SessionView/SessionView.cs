using System.Threading;
using System.Threading.Tasks;
using FpvDroneSimulator.Common.Core;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using FpvDroneSimulator.UI.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FpvDroneSimulator.UI.Views.SessionView
{
    public class SessionView : ViewBase
    {
        [Inject] private EventBus eventBus;
        
        [SerializeField] private Button continueButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button exitButton;

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
            continueButton.onClick.AddListener(HandleContinueClick);
            menuButton.onClick.AddListener(HandleMenuClick);
            exitButton.onClick.AddListener(HandleExitClick);
        }

        private void UnsubscribeHandlers()
        {
            continueButton.onClick.RemoveListener(HandleContinueClick);
            menuButton.onClick.RemoveListener(HandleMenuClick);
            exitButton.onClick.RemoveListener(HandleExitClick);
        }

        private async void HandleContinueClick()
        {
            await viewVisualizer.Hide<SessionView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
        }

        private async void HandleMenuClick()
        {
            await viewVisualizer.Hide<SessionView, IOptions>(IOptions.NoneOptions, CancellationToken.None);
            eventBus.Raise(new OnSessionExitClicked());
        }

        private void HandleExitClick()
        {
            Application.Quit();
        }
    }
}