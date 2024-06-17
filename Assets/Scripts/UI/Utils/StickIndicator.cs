using System;
using FpvDroneSimulator.Services.InputProvider;
using UnityEngine;
using Zenject;

namespace FpvDroneSimulator.UI.Utils
{
    public class StickIndicator : MonoBehaviour
    {
        [Inject] private IInputProvider inputProvider;
        
        [SerializeField] private GamepadStickType stickType;
        [SerializeField] private RectTransform boundsPanelRectTransform;
        [SerializeField] private RectTransform pointRectTransform;
        private float xPosMultiplier;
        private float yPosMultiplier;

        private void Awake()
        {
            xPosMultiplier = boundsPanelRectTransform.rect.width / 2;
            yPosMultiplier = boundsPanelRectTransform.rect.height / 2;
        }
        
        public void SetPointValue(Vector2 value)
        {
            pointRectTransform.anchoredPosition = new Vector2(value.x * xPosMultiplier, value.y * yPosMultiplier);
        }

        private void Update()
        {
            switch (stickType)
            {
                case GamepadStickType.Left:
                    SetPointValue(inputProvider.GetLeftStickValue());
                    break;
                case GamepadStickType.Right:
                    SetPointValue(inputProvider.GetRightStickValue());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum GamepadStickType
    {
        Left = 0,
        Right = 1,
    }
}