namespace FpvDroneSimulator.Services.SettingsSaveLoader
{
    public interface IDefaultDataProvider<out TData>
        where TData : struct, IPersistentData<TData>
    {
        public TData DefaultData { get; }
    }
}