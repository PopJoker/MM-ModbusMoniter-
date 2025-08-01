# MM-ModbusMonitor

**MM-ModbusMonitor** 是一款使用 C# Windows Forms 製作的應用程式，透過 Modbus RTU 協議即時監控多組電池系統資料。它支援多種組合電池配置 (5S2P、22S2P、28S2P)，並且提供電壓、電流、SOC、溫度等即時顯示與資料記錄功能。

---

## 🚀 功能特色

- ✅ 支援多組電池配置（5S2P、22S2P、28S2P）
- 📡 Modbus RTU 通訊，支援多 COM Port 與多個 Slave ID 同時監控
- ⚡ 即時顯示電池電壓、電流、SOC 與溫度
- 📊 圖形化面板顯示各電池資訊
- 💾 可自訂 Log 資料夾及檔名，自動記錄異常電壓與電壓差異
- 🛠 支援使用者自訂電壓最大值與 Delta 門檻
- 🔒 未來可擴充權限管理與加密功能

---

## 🖥 使用環境

- 作業系統：Windows 10 以上
- 開發平台：.NET Framework 4.7+ 或 .NET 6+
- 程式語言：C# (Windows Forms)
- 依賴套件：
  - System.IO.Ports (序列埠通訊)
  - System.Windows.Forms (UI)
  - 其他圖表套件可視需求加入（目前未內建）

---

## ⚙️ 如何使用

```
git clone https://github.com/PopJoker/MM-ModbusMoniter-.git
```

1. **開啟專案**

2. **編譯並執行**

3. **選擇通訊模式**  
   在主頁面選擇模式 (ID Mode / COM Mode)，選擇 COM Port，按下連接。

4. **監控電池**  
   監控頁面將顯示 10 個電池面板，支援即時資料更新。

5. **設定日誌**  
   在設定區設定 Log 資料夾路徑、Cell Voltage 最大值與 Delta 門檻，系統會自動記錄異常資料。

## 📂 專案結構簡介
```
MM-ModbusMonitor
│
├── Forms
│ ├── FormMain.cs // 模式選擇頁
│ └── FormMonitor.cs // 電池監控頁
│
├── Controls
│ └── BatteryPanel.cs // 自訂 UserControl，顯示單一電池資訊
│
├── Services
│ └── ModbusService.cs // Modbus RTU 通訊封裝
│
├── Utils
│ └── Logger.cs // 日誌記錄工具
│
└── Models
├── IBatteryData.cs // 電池資料介面定義
├── BatteryBase.cs // 電池資料基底類別
├── Battery5S2P.cs // 5S2P 電池組實作
├── Battery22S2P.cs // 22S2P 電池組實作
└── Battery28S2P.cs // 28S2P 電池組實作
```
---

## 👨‍💻 開發者

👤 PopJoker  
📫 GitHub: [https://github.com/PopJoker](https://github.com/PopJoker)

---

## 📄 License

MIT License - 自由使用與修改

---

如果有任何問題或建議，歡迎在 GitHub 上開 issue！
