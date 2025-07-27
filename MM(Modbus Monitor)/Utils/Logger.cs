using System;
using System.IO;
using MM_Modbus_Monitor_.Models;

namespace MM_Modbus_Monitor_.Utils
{
    public class Logger
    {
        private string logFilePath;

        public Logger(string folderPath, string fileName)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            logFilePath = Path.Combine(folderPath, fileName);
            if (!File.Exists(logFilePath))
            {
                File.WriteAllText(logFilePath, "Timestamp,BatteryId,CellVoltages,Temperature,SOC,Current\r\n");
            }
        }

        public void LogBatteryDataIfThreshold(IBatteryData battery, float cellVoltageMax, float deltaVoltageThreshold)
        {
            float maxVoltage = 0;
            float minVoltage = float.MaxValue;
            foreach (var v in battery.CellVoltages)
            {
                if (v > maxVoltage) maxVoltage = v;
                if (v < minVoltage) minVoltage = v;
            }
            float delta = maxVoltage - minVoltage;

            if (maxVoltage > cellVoltageMax || delta > deltaVoltageThreshold)
            {
                LogBatteryData(battery);
            }
        }

        private void LogBatteryData(IBatteryData battery)
        {
            string voltages = string.Join(";", battery.CellVoltages);
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss},{battery.BatteryId},{voltages},{battery.Temperature:F1},{battery.SOC:F1},{battery.Current:F2}\r\n";
            File.AppendAllText(logFilePath, line);
        }
    }
}
