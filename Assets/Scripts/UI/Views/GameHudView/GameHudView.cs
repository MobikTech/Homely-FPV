using System;
using System.Threading.Tasks;
using FpvDroneSimulator.Logic;
using FpvDroneSimulator.Services.LevelInfoHolder;
using FpvDroneSimulator.UI.Core;
using FpvDroneSimulator.UI.Views.GameHudView.Widgets;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.UI.Views.GameHudView
{
    public class GameHudView : ViewBase
    {
        [Inject] protected ILevelInfoHolder levelInfoHolder;

        public TimeSpan CurrentSessionTime => sessionTimerWidget.CurrentTime;
        
        [SerializeField] private FlightModeWidget flightModeWidget;
        [SerializeField] private SessionTimerWidget sessionTimerWidget;
        [SerializeField] private SpeedIndicatorWidget speedIndicatorWidget;
        [SerializeField] private RaceProgressWidget raceProgressWidget;
        
        protected override Task Initialize()
        {
            flightModeWidget.Initialize();
            sessionTimerWidget.Initialize();
            speedIndicatorWidget.Initialize();
            if (levelInfoHolder.GameLevelMode == GameLevelMode.Race)
            {
                raceProgressWidget.Initialize();
                raceProgressWidget.SetActive(true);
            }
            else
            {
                raceProgressWidget.SetActive(false);
            }
            return Task.CompletedTask;
        }

        protected override void Dispose()
        {
            flightModeWidget.Dispose();
            sessionTimerWidget.Dispose();
            speedIndicatorWidget.Dispose();
            if (levelInfoHolder.GameLevelMode == GameLevelMode.Race)
            {
                raceProgressWidget.Dispose();
            }
        }
    }
}