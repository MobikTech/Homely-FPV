using UnityEngine;

namespace FpvDroneSimulator.Services.SettingsSaveLoader
{
    public class DataSaveLoader<TData> : IDataSaveLoader<TData>
        where TData : struct, IPersistentData<TData>
    {
        private readonly IDefaultDataProvider<TData> defaultDataProvider;
        private string dataKey;

        public DataSaveLoader(IDefaultDataProvider<TData> defaultDataProvider)
        {
            this.defaultDataProvider = defaultDataProvider;
        }

        public void Initialize()
        {
            dataKey = typeof(TData).Name;
            if (!PlayerPrefs.HasKey(dataKey))
            {
                Debug.Log($"DataSaveLoader:Initialize");
                string json = JsonUtility.ToJson(defaultDataProvider.DefaultData);
                PlayerPrefs.SetString(dataKey, json);
                PlayerPrefs.Save();
            }
        }

        public void Save(TData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(typeof(TData).Name, json);
            PlayerPrefs.Save();
        }

        public TData Load()
        {
            string json = PlayerPrefs.GetString(typeof(TData).Name);
            return JsonUtility.FromJson<TData>(json);
        }
    }
}