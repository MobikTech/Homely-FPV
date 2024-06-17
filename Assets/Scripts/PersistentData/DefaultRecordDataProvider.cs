using FpvDroneSimulator.Services.SettingsSaveLoader;

namespace FpvDroneSimulator.PersistentData
{
    public class DefaultRecordDataProvider : IDefaultDataProvider<RecordData>
    {
        public RecordData DefaultData => new()
        {
            RecordTimePerLevel = new()
            {
                null,
                null,
                null
            },
        };
    }
}