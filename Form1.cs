using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace GenForce
{
    public class Form1 : Form
    {
        private ToolBar toolbar;
  
        private Panel mainPanel;
        private DataGridView inputDataGridView;
        private DataGridView outputDataGridView;
        private Button addRowButton;
        private Button parseButton;
        private DataTable inputTable = new DataTable();
        private DataTable outputTable = new DataTable();
        private ContextMenuStrip deleteMenu;

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
            toolbar = new ToolBar();

            mainPanel = new Panel();
            inputDataGridView = new DataGridView();
            outputDataGridView = new DataGridView();
            addRowButton = new Button();
            parseButton = new Button();
            deleteMenu = new ContextMenuStrip();
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).BeginInit();
            SuspendLayout();

            Controls.Add(toolbar.ToolStrip); //ToolStrip
            toolbar.ToolStrip.Dock = DockStyle.Top;

            // 
            // mainPanel
            // 
            mainPanel.Location = new Point(12, toolbar.ToolStrip.Height + 10);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(670, 451); // Keep size in mind 
            mainPanel.AutoScroll = true;
            // 
            // inputDataGridView
            // 
            inputDataGridView.ColumnHeadersHeight = 29;
            inputDataGridView.Location = new Point(0, 0);
            inputDataGridView.Name = "inputDataGridView";
            inputDataGridView.RowHeadersWidth = 50;
            inputDataGridView.Size = new Size(570, 451); // Keep size in mind 
            inputDataGridView.TabIndex = 0;
            inputDataGridView.CellContentClick += inputDataGridView_CellContentClick;
            inputDataGridView.AllowUserToAddRows = false;
            inputDataGridView.Scroll += inputDataGridView_Scroll;
            // 
            // outputDataGridView
            // 
            outputDataGridView.ColumnHeadersHeight = 29;
            outputDataGridView.Location = new Point(747, toolbar.ToolStrip.Height + 10);
            outputDataGridView.Name = "outputDataGridView";
            outputDataGridView.RowHeadersWidth = 51;
            outputDataGridView.Size = new Size(441, 451);
            outputDataGridView.TabIndex = 1;
            // 
            // addRowButton
            // 
            addRowButton.Location = new Point(12, 500);
            addRowButton.Name = "addRowButton";
            addRowButton.Size = new Size(120, 40);
            addRowButton.TabIndex = 2;
            addRowButton.Text = "Add Row";
            addRowButton.UseVisualStyleBackColor = true;
            addRowButton.Click += new EventHandler(buttonAddRow_Click);

            // 
            // parseButton FOR DEBUGGING
            // 
            parseButton.Location = new Point(150, 500);
            parseButton.Name = "parseButton";
            parseButton.Size = new Size(120, 40);
            parseButton.TabIndex = 3;
            parseButton.Text = "Parse Times x Size";
            parseButton.UseVisualStyleBackColor = true;
            parseButton.Click += new EventHandler(buttonParse_Click);


            // 
            // Form1
            // 
            ClientSize = new Size(1200, 550);
            Controls.Add(mainPanel);
            mainPanel.Controls.Add(inputDataGridView);
            Controls.Add(outputDataGridView);
            Controls.Add(addRowButton);
            Controls.Add(parseButton);
            Name = "Form1";
            Text = "Logistics Automation";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).EndInit();
            ResumeLayout(false);
        }

        private void SetupInputTable()
        {
            inputDataGridView.ColumnHeadersHeight = 40;
            inputTable.Columns.Add("Letter");
            inputTable.Columns.Add("Sets");
            inputTable.Columns.Add("Times x Size");
            inputTable.Columns.Add("Minimum Conduit");
            inputTable.Columns.Add("Length");

            // Create and add ComboBox columns
            DataGridViewComboBoxColumn metricColumn = new DataGridViewComboBoxColumn
            {
                Name = "Metric",
                HeaderText = "Metric",
                DataSource = new string[] { "AWG", "KCMIL", "MCM" }
            };
            DataGridViewComboBoxColumn materialColumn = new DataGridViewComboBoxColumn
            {
                Name = "Material",
                HeaderText = "Material",
                DataSource = new string[] { "CU", "AL" }
            };

            inputDataGridView.DataSource = inputTable;

            // Set Width
            inputDataGridView.Columns["Times x Size"].Width = 100;
            inputDataGridView.Columns["Letter"].Width = 50;
            inputDataGridView.Columns["Sets"].Width = 50;
            inputDataGridView.Columns["Minimum Conduit"].Width = 65;
            inputDataGridView.Columns["Length"].Width = 55;

            // Add ComboBox columns to the DataGridView at specified positions
            inputDataGridView.Columns.Insert(3, metricColumn);
            inputDataGridView.Columns.Insert(4, materialColumn);

            // Set up the delete context menu
            deleteMenu.Items.Add("Delete Row", null, DeleteRow);

            // Add the Default Row

            DataRow newRow = inputTable.NewRow();
            inputTable.Rows.Add(newRow);

            // Create the delete button
            Button deleteButton = new Button
            {
                Text = "...",
                Size = new Size(40, 20),
                Tag = inputTable.Rows.Count // Store the row index in the Tag property
            };
            deleteButton.Click += DeleteButton_Click;

            // Position the button
            mainPanel.Controls.Add(deleteButton);
            UpdateButtonPositions();
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
            // No need to handle this event for delete button
        }

        private void inputDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            // Update button positions when scrolling
            UpdateButtonPositions();
        }

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void AddNewRow()
        {
            // Add the new row to the DataTable
            DataRow newRow = inputTable.NewRow();
            inputTable.Rows.Add(newRow);

            // Create the delete button
            Button deleteButton = new Button
            {
                Text = "...",
                Size = new Size(40, 20),
                Tag = inputTable.Rows.Count - 1 // Store the row index in the Tag property
            };
            deleteButton.Click += DeleteButton_Click;

            // Position the button
            mainPanel.Controls.Add(deleteButton);
            UpdateButtonPositions();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (sender is Button deleteButton)
            {
                int rowIndex = (int)deleteButton.Tag;
                if (rowIndex >= 0 && rowIndex < inputTable.Rows.Count)
                {
                    inputTable.Rows.RemoveAt(rowIndex);
                    mainPanel.Controls.Remove(deleteButton);
                    UpdateButtonPositions();
                }
            }
        }

        private void UpdateButtonPositions()
        {
            // Update the positions of the delete buttons to match the rows
            int buttonIndex = 0;
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Button deleteButton && deleteButton.Text == "...")
                {
                    if (buttonIndex < inputDataGridView.Rows.Count)
                    {
                        DataGridViewRow row = inputDataGridView.Rows[buttonIndex];
                        Rectangle rowRect = inputDataGridView.GetRowDisplayRectangle(row.Index, true);
                        deleteButton.Location = new Point(inputDataGridView.Location.X + inputDataGridView.Width + 5,
                            mainPanel.AutoScrollPosition.Y + rowRect.Top);
                        deleteButton.Tag = buttonIndex; // Update the tag with the new row index
                    }
                    buttonIndex++;
                }
            }
        }

        private void DeleteRow(object sender, EventArgs e)
        {
            if (deleteMenu.Tag != null)
            {
                int rowIndex = (int)deleteMenu.Tag;
                if (rowIndex >= 0 && rowIndex < inputDataGridView.Rows.Count)
                {
                    inputDataGridView.Rows.RemoveAt(rowIndex);
                    UpdateButtonPositions();
                }
            }
        }

        //Parses the Times x Size  and returns it, if empty returns nothing 
        private (int times, string size) ParseTimesXSize(string timesXSize)
        {
            var parts = timesXSize.Split('x');
            if (parts.Length == 2)
            {
                var times = Convert.ToInt32(parts[0].Trim()); // Gotta make sure this is an integer else throw error

                var size = parts[1].Trim();

                return (times, size);
            }
            return (0, null);
        }

        //Calls the parsing function and displays the parse - 1st update
        //Gets the other needed values
        private void buttonParse_Click(object sender, EventArgs e)
        {
            foreach (DataRow row in inputTable.Rows)
            {
                int sets;
                bool num = true;
                int rowIndex = inputTable.Rows.IndexOf(row);
                DataGridViewRow dataGridViewRow = inputDataGridView.Rows[rowIndex];

                if (row["Sets"] == DBNull.Value)
                {
                    sets = 1;
                }
                else
                {
                    try
                    {
                        sets = Convert.ToInt32(row["Sets"]);
                    }
                    catch (FormatException ex)
                    {
                        dataGridViewRow.Cells["Sets"].Style.BackColor = Color.Red;
                        MessageBox.Show(ex.Message + " Must be an Integer! ");

                        num = false;
                    }

                    if (num)
                    {
                        sets = Convert.ToInt32(row["Sets"]);
                        dataGridViewRow.Cells["Sets"].Style.BackColor = Color.White;
                    }
                    else
                    {
                        sets = 1;
                    }
                }

                string timesXSize = row["Times x Size"].ToString();
                // string metric = row["Metric"].ToString();
                // string material = row["Material"].ToString(); // , Metric: {metric}, Material: {material}

                var parsedResult = ParseTimesXSize(timesXSize);

                if (parsedResult.times != 0 && parsedResult.size != null)
                {
                    MessageBox.Show($"Times: {parsedResult.times}, Size: {parsedResult.size}, Sets: {sets}");
                }
            }
        }


    }
}

