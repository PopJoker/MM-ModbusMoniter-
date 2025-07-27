using System;
using System.Windows.Forms;
using MM_Modbus_Monitor_.Forms;

namespace MM_Modbus_Monitor_
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            InitializeUI();
        }

        private RadioButton rbIDMode;
        private RadioButton rbCOMMode;
        private ComboBox cbCOMPorts;
        private Button btnNext;

        private void InitializeUI()
        {
            this.Text = "Modbus Monitor - Select Mode";
            this.Width = 300;
            this.Height = 180;

            rbIDMode = new RadioButton() { Text = "ID Mode", Left = 20, Top = 20, Width = 100 };
            rbCOMMode = new RadioButton() { Text = "COM Mode", Left = 20, Top = 50, Width = 100 };
            rbIDMode.Checked = true;

            cbCOMPorts = new ComboBox() { Left = 130, Top = 22, Width = 120 };
            // TODO: 讀取系統 COM Port 加入選單
            cbCOMPorts.Items.AddRange(new string[] { "COM1", "COM2", "COM3" });
            cbCOMPorts.SelectedIndex = 0;

            btnNext = new Button() { Text = "Next", Left = 130, Top = 60, Width = 120 };
            btnNext.Click += BtnNext_Click;

            this.Controls.Add(rbIDMode);
            this.Controls.Add(rbCOMMode);
            this.Controls.Add(cbCOMPorts);
            this.Controls.Add(btnNext);

            rbCOMMode.CheckedChanged += (s, e) =>
            {
                cbCOMPorts.Enabled = rbCOMMode.Checked;
            };
            cbCOMPorts.Enabled = rbIDMode.Checked == false;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            // 讀取模式與選擇 COM Port
            string mode = rbIDMode.Checked ? "ID" : "COM";
            string comPort = cbCOMPorts.SelectedItem?.ToString();

            // 跳轉到 FormMonitor
            var monitorForm = new FormMonitor();

            // TODO: 依 mode 和 comPort 傳入參數給 monitorForm 進行初始化
            monitorForm.Show();
            this.Hide();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
