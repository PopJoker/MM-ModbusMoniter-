using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MM_Modbus_Monitor_.Controls;
using MM_Modbus_Monitor_.Models;
using MM_Modbus_Monitor_.Utils;

namespace MM_Modbus_Monitor_.Forms
{
    public partial class FormMonitor : Form
    {
        private List<IBatteryData> batteries = new List<IBatteryData>();
        private List<BatteryPanel> panels = new List<BatteryPanel>();
        private Timer updateTimer;
        private Logger logger;

        private FlowLayoutPanel flowLayout;

        // 設定欄位
        private TextBox txtLogFolder;
        private Button btnBrowseFolder;
        private TextBox txtCellVoltageMax;
        private TextBox txtDeltaVoltage;
        private Button btnApplySettings;

        private string logFolder = "";
        private float cellVoltageMax = 4.2f;
        private float deltaVoltageThreshold = 0.1f;

        public FormMonitor()
        {
            InitializeComponent();
            InitializeLayout();
            InitializeSettingsUI();
            InitializeBatteries();
            InitializeLogger();
            InitializeTimer();
        }

        private void InitializeLayout()
        {
            this.Text = "Battery Monitor";
            this.Width = 900;
            this.Height = 700;

            flowLayout = new FlowLayoutPanel();
            flowLayout.Dock = DockStyle.Top;
            flowLayout.Height = 500;
            flowLayout.AutoScroll = true;
            flowLayout.WrapContents = true;
            flowLayout.FlowDirection = FlowDirection.LeftToRight;
            this.Controls.Add(flowLayout);
        }

        private void InitializeSettingsUI()
        {
            Panel pnlSettings = new Panel();
            pnlSettings.Dock = DockStyle.Bottom;
            pnlSettings.Height = 120;
            this.Controls.Add(pnlSettings);

            Label lblFolder = new Label() { Text = "Log Folder:", Left = 10, Top = 15, Width = 80 };
            txtLogFolder = new TextBox() { Left = 100, Top = 12, Width = 400 };
            btnBrowseFolder = new Button() { Text = "Browse...", Left = 510, Top = 10, Width = 80 };
            btnBrowseFolder.Click += BtnBrowseFolder_Click;

            Label lblCellMax = new Label() { Text = "CellVoltage Max (V):", Left = 10, Top = 50, Width = 120 };
            txtCellVoltageMax = new TextBox() { Left = 140, Top = 48, Width = 60, Text = cellVoltageMax.ToString() };

            Label lblDelta = new Label() { Text = "Delta Voltage (V):", Left = 220, Top = 50, Width = 110 };
            txtDeltaVoltage = new TextBox() { Left = 340, Top = 48, Width = 60, Text = deltaVoltageThreshold.ToString() };

            btnApplySettings = new Button() { Text = "Apply", Left = 510, Top = 46, Width = 80 };
            btnApplySettings.Click += BtnApplySettings_Click;

            pnlSettings.Controls.AddRange(new Control[]
            {
                lblFolder, txtLogFolder, btnBrowseFolder,
                lblCellMax, txtCellVoltageMax,
                lblDelta, txtDeltaVoltage,
                btnApplySettings
            });
        }

        private void BtnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtLogFolder.Text = fbd.SelectedPath;
                    logFolder = fbd.SelectedPath;
                    InitializeLogger();  // 重新建立 Logger 物件
                }
            }
        }

        private void BtnApplySettings_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtCellVoltageMax.Text, out float maxVal))
                cellVoltageMax = maxVal;

            if (float.TryParse(txtDeltaVoltage.Text, out float deltaVal))
                deltaVoltageThreshold = deltaVal;

            MessageBox.Show("Settings applied.");
        }

        private void InitializeBatteries()
        {
            for (int i = 1; i <= 10; i++)
            {
                var battery = new Battery5S2P(i);
                batteries.Add(battery);

                var panel = new BatteryPanel();
                panel.Width = 260;
                panel.Height = 180;
                panel.UpdateUI(battery);

                panels.Add(panel);
                flowLayout.Controls.Add(panel);
            }
        }

        private void InitializeLogger()
        {
            if (string.IsNullOrEmpty(logFolder))
                logFolder = Environment.CurrentDirectory;

            string fileName = $"BatteryLog_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            logger = new Logger(logFolder, fileName);
        }

        private void InitializeTimer()
        {
            updateTimer = new Timer();
            updateTimer.Interval = 1000; // 每秒更新一次
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        private Random rnd = new Random();

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < batteries.Count; i++)
            {
                var battery = batteries[i];

                // 模擬隨機電壓、溫度、SOC、電流
                float[] voltages = new float[battery.SeriesCount];
                for (int v = 0; v < voltages.Length; v++)
                    voltages[v] = 3.2f + (float)rnd.NextDouble() * 1.0f; // 3.2~4.2 V

                float temp = 25f + (float)rnd.NextDouble() * 5f;
                float soc = 50f + (float)rnd.NextDouble() * 50f;
                float current = (float)(rnd.NextDouble() * 10f - 5f);

                battery.Update(voltages, temp, soc, current);
                panels[i].UpdateUI(battery);

                // 依條件寫 Log
                if (logger != null)
                    logger.LogBatteryDataIfThreshold(battery, cellVoltageMax, deltaVoltageThreshold);
            }
        }

        private void FormMonitor_Load(object sender, EventArgs e)
        {

        }
    }
}
