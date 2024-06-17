using System;
using System.Collections.Generic;
using System.Reflection;
using FpvDroneSimulator.PersistentData;
using TMPro;
using UnityEngine;

namespace FpvDroneSimulator.UI.Views.SettingsView
{
    public class SettingsDataHandler : MonoBehaviour, IDisposable
    {
        [SerializeField] private List<TMP_InputField> settingsValuesInputFields;

        public void Initialize(SettingsData settingsData)
        {
            SetValuesFromData(settingsData);
        }

        public void Dispose()
        {
            
        }

        public SettingsData GetCurrentValuesData()
        {
            SettingsData result = new SettingsData();
            FieldInfo[] valueFields = result.GetType().GetFields();
            for (var i = 0; i < valueFields.Length; i++)
            {
                string valueString = settingsValuesInputFields[i].text;
                FieldInfo fieldInfo = valueFields[i];

                object typedValue = Convert.ChangeType(valueString, fieldInfo.FieldType);
                
                TypedReference reference = __makeref(result);
                fieldInfo.SetValueDirect(reference, (float)typedValue);
            }

            return result;
        }

        private void SetValuesFromData(SettingsData settingsData)
        {
            FieldInfo[] valueFields = settingsData.GetType().GetFields();
            for (var i = 0; i < valueFields.Length; i++)
            {
                FieldInfo fieldInfo = valueFields[i];
                object obj = fieldInfo.GetValue(settingsData);
                
                string value = null;
                if (fieldInfo.FieldType == typeof(float))
                {
                    value = ((float)obj).ToString();
                }
                else if (fieldInfo.FieldType == typeof(int))
                {
                    value = ((int)obj).ToString();
                }

                settingsValuesInputFields[i].text = value;
            }
        }
    }
}