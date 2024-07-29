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
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).BeginInit();
            SuspendLayout();
            // 
            // inputDataGridView
            // 
            inputDataGridView.Location = new Point(12, 12);
            inputDataGridView.Name = "inputDataGridView";
            inputDataGridView.Size = new Size(350, 200);
            inputDataGridView.TabIndex = 0;
            inputDataGridView.CellContentClick += inputDataGridView_CellContentClick;
            // 
            // outputDataGridView
            // 
            outputDataGridView.Location = new Point(370, 12);
            outputDataGridView.Name = "outputDataGridView";
            outputDataGridView.Size = new Size(350, 200);
            outputDataGridView.TabIndex = 1;
            // 
            // Form1
            // 
            ClientSize = new Size(740, 230);
            Controls.Add(inputDataGridView);
            Controls.Add(outputDataGridView);
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
        }

        private void inputDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
