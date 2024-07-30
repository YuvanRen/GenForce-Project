using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace GenForce
{
    public class SettingsForm : Form
    {
        private CheckBox darkMode_CheckBox;
        private Form1 mainForm;
        private bool is_DarkMode;

        private Color darkBackColor = Color.FromArgb(45, 45, 48);
        private Color darkForeColor = Color.FromArgb(220, 220, 220);

        public SettingsForm(Form1 form, bool darkMode) //Constructor
        {
            mainForm = form;
            is_DarkMode = darkMode;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Customize the settings form here
            this.Text = "Settings";
            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;


            //DarkMode checkBox in mini window
            darkMode_CheckBox = new CheckBox
            {
                Text = "Dark Mode",
                Location = new Point(10, 10),
                AutoSize = true,
                Checked = is_DarkMode
            };
            darkMode_CheckBox.CheckedChanged += DM_CheckBox_CheckedChanged;

            this.Controls.Add(darkMode_CheckBox);

            if(is_DarkMode) //When settings window is closed, this will keep track of whether it is in darkmode or not when reopening settings.

            {
                ApplyDarkMode(this);
            }
            else
            {
                ApplyLightMode(this);
            }
        }

        private void DM_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (darkMode_CheckBox.Checked) //Dictates whether program enables or disables dark mode
            {
                mainForm.ApplyDarkMode(mainForm);
                ApplyDarkMode(this);
            }
            else
            {
                mainForm.ApplyLightMode(mainForm);
                ApplyLightMode(this);
            }
        }

        public void ApplyDarkMode(Control control) //Needed for darkening/lightening of the setting window ONLY
        {
            control.BackColor = darkBackColor;
            control.ForeColor = darkForeColor;

            foreach (Control child in control.Controls)
            {
                ApplyDarkMode(child);
            }
        }

        public void ApplyLightMode(Control control)
        {
            control.BackColor = SystemColors.Control;
            control.ForeColor = SystemColors.ControlText;

            foreach (Control child in control.Controls)
            {
                ApplyLightMode(child);
            }
        }
    }
}

