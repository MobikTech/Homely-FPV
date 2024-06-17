using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FpvDroneSimulator.UI.Utils
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleObjectsActivator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> objectsIfOn;
        [SerializeField] private List<GameObject> objectsIfOff;
        private Toggle toggle;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(HandleValueChanged);
            objectsIfOff.ForEach(o => o.SetActive(!toggle.isOn));
            objectsIfOn.ForEach(o => o.SetActive(toggle.isOn));
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveListener(HandleValueChanged);
        }

        private void HandleValueChanged(bool isActive)
        {
            objectsIfOff.ForEach(o => o.SetActive(!isActive));
            objectsIfOn.ForEach(o => o.SetActive(isActive));
        }
    }
}