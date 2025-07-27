using System;

namespace MM_Modbus_Monitor_.Models
{
    public interface IBatteryData
    {
        int BatteryId { get; }          // 電池編號
        float[] CellVoltages { get; }   // 各 Cell 電壓陣列
        float Temperature { get; }
        float SOC { get; }
        float Current { get; }
        DateTime LastUpdateTime { get; }

        int SeriesCount { get; }        // 幾串
        int ParallelCount { get; }      // 幾並

        void Update(float[] voltages, float temperature, float soc, float current);
    }
}
