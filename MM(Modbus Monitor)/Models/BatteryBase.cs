using System;

namespace MM_Modbus_Monitor_.Models
{
    public abstract class BatteryBase : IBatteryData
    {
        public int BatteryId { get; protected set; }
        public float[] CellVoltages { get; protected set; }
        public float Temperature { get; protected set; }
        public float SOC { get; protected set; }
        public float Current { get; protected set; }
        public DateTime LastUpdateTime { get; protected set; }

        public int SeriesCount { get; protected set; }
        public int ParallelCount { get; protected set; }

        public virtual void Update(float[] voltages, float temperature, float soc, float current)
        {
            if (voltages.Length != SeriesCount)
                throw new ArgumentException("Voltage count does not match SeriesCount.");

            CellVoltages = voltages;
            Temperature = temperature;
            SOC = soc;
            Current = current;
            LastUpdateTime = DateTime.Now;
        }
    }
}
