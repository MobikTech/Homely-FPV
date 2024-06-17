using System.Threading.Tasks;
using FpvDroneSimulator.Common.Utilities.EventBus;
using FpvDroneSimulator.Events;
using FpvDroneSimulator.UI.Core;
using FpvDroneSimulator.UI.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FpvDroneSimulator.UI.Views.ResultView
{
    public class ResultView : ViewBase
    {
        [Inject] private EventBus eventBus;
        
        [SerializeField] private Button continueButton;
        [SerializeField] private TMP_Text recordTimeText;
        [SerializeField] private TMP_Text currentTimeText;

        protected override Task Initialize()
        {
            ResultViewOpenOptions options = viewOpenOptions as ResultViewOpenOptions;
            recordTimeText.text = options.BestTime;
            currentTimeText.text = options.CurrentTime;
            continueButton.onClick.AddListener(HandleContinueClick);
            return Task.CompletedTask;
        }

        protected override void Dispose()
        {
            continueButton.onClick.RemoveListener(HandleContinueClick);
        }

        private void HandleContinueClick()
        {
            eventBus.Raise(new OnResultContinued());
        }
    }
}