using System;
using System.Collections.Generic;
using FpvDroneSimulator.Services.SettingsSaveLoader;

namespace FpvDroneSimulator.PersistentData
{
    [Serializable]
    public struct RecordData : IPersistentData<RecordData>
    {
        public List<string> RecordTimePerLevel;
    }
}