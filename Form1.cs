using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace GenForce
{
    public class Form1 : Form
    {
        private DataGridView inputDataGridView;
        private DataGridView outputDataGridView;
        private DataTable inputTable = new DataTable();
        private DataTable outputTable = new DataTable();

        private Button settings_Button;
        private bool is_DarkMode;


        //-- Used for darkening and lightening certain aspects of the program -------------------
        private Color darkBackColor = Color.FromArgb(45, 45, 48);
        private Color darkForeColor = Color.FromArgb(220, 220, 220);
        private Color darkGridBackColor = Color.FromArgb(30, 30, 30);
        private Color darkGridForeColor = Color.FromArgb(200, 200, 200);
        private Color darkGridLineColor = Color.FromArgb(60, 60, 60);

        private Color lightBackColor = SystemColors.Control;
        private Color lightForeColor = SystemColors.ControlText;
        private Color lightGridBackColor = SystemColors.Window;
        private Color lightGridForeColor = SystemColors.ControlText;
        private Color lightGridLineColor = SystemColors.ControlDark;
        private Color lightGridCellBackColor = SystemColors.Window;
        private Color lightGridCellForeColor = SystemColors.ControlText;
        //---------------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
            SetupInputTable();
            SetupOutputTable();
            inputDataGridView.DataSource = inputTable;
            outputDataGridView.DataSource = outputTable;
        }

        private void InitializeComponent()
        {

            inputDataGridView = new DataGridView();
            outputDataGridView = new DataGridView();
            settings_Button = new Button();
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).BeginInit();
            SuspendLayout();

            // inputDataGridView

            inputDataGridView.Location = new Point(12, 12);
            inputDataGridView.Name = "inputDataGridView";
            inputDataGridView.Size = new Size(700, 300);
            inputDataGridView.TabIndex = 0;
            inputDataGridView.CellContentClick += inputDataGridView_CellContentClick;
      
            // outputDataGridView

            outputDataGridView.Location = new Point(900, 12);
            outputDataGridView.Name = "outputDataGridView";
            outputDataGridView.Size = new Size(700, 300);
            outputDataGridView.TabIndex = 1;

            //Settings Button

            settings_Button.Name = "settingsButton";
            settings_Button.Size = new Size(75, 30);
            settings_Button.Text = "Settings";
            settings_Button.UseVisualStyleBackColor = true;
            settings_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            settings_Button.Click += new EventHandler(SettingsButton_Click);

            // Form1

            ClientSize = new Size(1640, 720);
            Controls.Add(inputDataGridView);
            Controls.Add(outputDataGridView);
            Controls.Add(settings_Button);
            Name = "Form1";
            Text = "Logistics Automation";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).EndInit();
            ResumeLayout(false);
        }

        private void SetupInputTable()
        {
            inputTable.Columns.Add("Letter");
            inputTable.Columns.Add("Wire Size & Type");
            inputTable.Columns.Add("Length");

        }

        private void SetupOutputTable()
        {
            // Example columns for output table
            outputTable.Columns.Add("Result");
            outputTable.Columns.Add("Details");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Add initial data or any required setup here

            settings_Button.Location = new Point(12, ClientSize.Height - 50);  // Position at bottom left
        }

        private void inputDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SettingsButton_Click(object sender, EventArgs e) // Handles the settings button click event
        {
            SettingsForm settingsForm = new SettingsForm(this, is_DarkMode);
            settingsForm.ShowDialog(); 
        }

        public void ApplyDarkMode(Control control) // Darkens certain aspects of the table
        {
            is_DarkMode = true;

            control.BackColor = darkBackColor;
            control.ForeColor = darkForeColor;

            if (control is DataGridView dgv)
            {
                dgv.BackgroundColor = darkBackColor;
                dgv.GridColor = darkGridLineColor;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = darkBackColor;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = darkForeColor;
                dgv.DefaultCellStyle.BackColor = darkGridBackColor;
                dgv.DefaultCellStyle.ForeColor = darkGridForeColor;
                dgv.EnableHeadersVisualStyles = false;
            }

            foreach (Control child in control.Controls)
            {
                ApplyDarkMode(child);
            }
        }

        public void ApplyLightMode(Control control) // Reverts table to default color scheme
        {
            is_DarkMode = false;

            control.BackColor = lightBackColor;
            control.ForeColor = lightForeColor;

            if (control is DataGridView dgv)
            {
                dgv.BackgroundColor = lightGridBackColor;
                dgv.GridColor = lightGridLineColor;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = lightGridBackColor;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = lightGridForeColor;
                dgv.DefaultCellStyle.BackColor = lightGridCellBackColor;
                dgv.DefaultCellStyle.ForeColor = lightGridCellForeColor;
                dgv.EnableHeadersVisualStyles = false;
            }

            foreach (Control child in control.Controls)
            {
                ApplyLightMode(child);
            }
        }
    }
}


