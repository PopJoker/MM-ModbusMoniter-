using System;
using System.Threading.Tasks;

namespace MM_Modbus_Monitor_.Services
{
    public class ModbusService
    {
        // 這裡暫時用事件模擬資料接收
        public event Action<ushort[]> OnDataReceived;

        public bool IsConnected { get; private set; } = false;

        public bool Connect(string comPort)
        {
            // TODO: 實作真正連接 COM Port 邏輯
            IsConnected = true;
            return IsConnected;
        }

        public void Disconnect()
        {
            // TODO: 實作斷線
            IsConnected = false;
        }

        public async Task ReadDataAsync(int slaveId)
        {
            if (!IsConnected) return;

            // TODO: 這裡改成實際 Modbus RTU 讀取命令

            // 模擬收到資料 (隨機)
            await Task.Delay(100);
            var random = new Random();
            ushort[] data = new ushort[30];
            for (int i = 0; i < data.Length; i++)
                data[i] = (ushort)random.Next(3000, 3300);

            OnDataReceived?.Invoke(data);
        }
    }
}
