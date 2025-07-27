namespace MM_Modbus_Monitor_.Models
{
    public class Battery5S2P : BatteryBase
    {
        public Battery5S2P(int id)
        {
            BatteryId = id;
            SeriesCount = 5;
            ParallelCount = 2;
            CellVoltages = new float[SeriesCount];
            Temperature = 0f;
            SOC = 0f;
            Current = 0f;
            LastUpdateTime = System.DateTime.Now;
        }
    }
}
