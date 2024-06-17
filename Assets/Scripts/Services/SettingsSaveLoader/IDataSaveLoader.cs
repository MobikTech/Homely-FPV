using Zenject;

namespace FpvDroneSimulator.Services.SettingsSaveLoader
{
    public interface IDataSaveLoader<TData> : IInitializable
        where TData : struct, IPersistentData<TData>
    {
        public void Save(TData data);
        public TData Load();
    }
}