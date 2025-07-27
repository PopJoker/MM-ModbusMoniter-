using System;
using System.Windows.Forms;
using MM_Modbus_Monitor_.Models;

namespace MM_Modbus_Monitor_.Controls
{
    public partial class BatteryPanel : UserControl
    {
        public BatteryPanel()
        {
            InitializeComponent();
            InitializeUI();
        }

        private Label lblBatteryId;
        private ListBox lstVoltages;
        private Label lblTemperature;
        private Label lblSOC;
        private Label lblCurrent;

        private void InitializeUI()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Width = 250;
            this.Height = 180;

            lblBatteryId = new Label() { Left = 10, Top = 5, Width = 200, Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold) };
            this.Controls.Add(lblBatteryId);

            lstVoltages = new ListBox() { Left = 10, Top = 30, Width = 100, Height = 100 };
            this.Controls.Add(lstVoltages);

            lblTemperature = new Label() { Left = 120, Top = 30, Width = 110 };
            lblSOC = new Label() { Left = 120, Top = 60, Width = 110 };
            lblCurrent = new Label() { Left = 120, Top = 90, Width = 110 };

            this.Controls.Add(lblTemperature);
            this.Controls.Add(lblSOC);
            this.Controls.Add(lblCurrent);
        }

        public void UpdateUI(IBatteryData data)
        {
            if (data == null) return;

            lblBatteryId.Text = $"Battery ID: {data.BatteryId} ({data.SeriesCount}S{data.ParallelCount}P)";
            lstVoltages.Items.Clear();
            for (int i = 0; i < data.CellVoltages.Length; i++)
            {
                lstVoltages.Items.Add($"Cell {i + 1}: {data.CellVoltages[i]:F3} V");
            }
            lblTemperature.Text = $"Temp: {data.Temperature:F1} °C";
            lblSOC.Text = $"SOC: {data.SOC:F1} %";
            lblCurrent.Text = $"Current: {data.Current:F2} A";
        }

        private void BatteryPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
