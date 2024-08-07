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
        private System.ComponentModel.IContainer components;
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            toolbar = new ToolBar();
            mainPanel = new Panel();
            inputDataGridView = new DataGridView();
            outputDataGridView = new DataGridView();
            addRowButton = new Button();
            parseButton = new Button();
            deleteMenu = new ContextMenuStrip(components);
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).BeginInit();
            SuspendLayout();
            // 
            // toolbar
            // 
            toolbar.ClientSize = new Size(282, 253);
            toolbar.Location = new Point(224, 224);
            toolbar.Name = "toolbar";
            toolbar.Visible = false;
            // 
            // mainPanel
            // 
            mainPanel.AutoScroll = true;
            mainPanel.Controls.Add(inputDataGridView);
            mainPanel.Location = new Point(12, 25);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(575, 451);
            mainPanel.TabIndex = 1;
            // 
            // inputDataGridView
            // 
            inputDataGridView.AllowUserToAddRows = false;
            inputDataGridView.ColumnHeadersHeight = 29;
            inputDataGridView.Location = new Point(0, 0);
            inputDataGridView.Name = "inputDataGridView";
            inputDataGridView.RowHeadersWidth = 4;
            inputDataGridView.Size = new Size(525, 451);
            inputDataGridView.TabIndex = 0;
            inputDataGridView.Scroll += inputDataGridView_Scroll;
            // 
            // outputDataGridView
            // 
            dataGridViewCellStyle1.Font = new Font("Arial", 8F, FontStyle.Bold);
            outputDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            outputDataGridView.ColumnHeadersHeight = 40;
            outputDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            outputDataGridView.Location = new Point(595, 25);
            outputDataGridView.Name = "outputDataGridView";
            outputDataGridView.RowHeadersWidth = 4;
            outputDataGridView.Size = new Size(548, 451);
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
            addRowButton.Click += buttonAddRow_Click;
            // 
            // parseButton
            // 
            parseButton.Location = new Point(150, 500);
            parseButton.Name = "parseButton";
            parseButton.Size = new Size(120, 40);
            parseButton.TabIndex = 3;
            parseButton.Text = "Material Wizard";
            parseButton.UseVisualStyleBackColor = true;
            parseButton.Click += buttonParse_Click;
            // 
            // deleteMenu
            // 
            deleteMenu.ImageScalingSize = new Size(20, 20);
            deleteMenu.Name = "deleteMenu";
            deleteMenu.Size = new Size(61, 4);
            // 
            // Form1
            // 
            ClientSize = new Size(1200, 550);
            Controls.Add(mainPanel);
            Controls.Add(outputDataGridView);
            Controls.Add(addRowButton);
            Controls.Add(parseButton);
            Name = "Form1";
            Text = "Logistics Automation";
            Load += Form1_Load;
            mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
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

        }

        private void SetupOutputTable()
        {
            // Define columns similar to the image
            outputTable.Columns.Add("Column1");
            outputTable.Columns.Add("1\"");
            outputTable.Columns.Add("1 1/2\"");
            outputTable.Columns.Add("2\"");
            outputTable.Columns.Add("2 1/2\"");
            outputTable.Columns.Add("3\"");
            outputTable.Columns.Add("4\"");

         
            // Example data
            outputTable.Rows.Add("EMT Pipe");
            outputTable.Rows.Add("EMT Connector compression");
            outputTable.Rows.Add("EMT coupling compression");
            outputTable.Rows.Add("Grounding bushing");
            outputTable.Rows.Add("Plastic bushing");
            outputTable.Rows.Add("Cowboy strap");
            outputTable.Rows.Add("EMT kindorf strap");
            outputTable.Rows.Add("Myers hub");
            outputTable.Rows.Add("EMT 90 deg elbow");
            outputTable.Rows.Add("EMT 45 deg elbow");
            outputTable.Rows.Add("EMT 22 1/2 deg elbow");
            outputTable.Rows.Add("EMT 15 deg elbow");
            outputTable.Rows.Add("Threded Mogule LB");
            outputTable.Rows.Add("Threded Type C");
            outputTable.Rows.Add("Threded Type LB");
            outputTable.Rows.Add("Threded Type LR");
            outputTable.Rows.Add("Threded Type LL");
            outputTable.Rows.Add("Threded T");
            outputTable.Rows.Add("Sealtite Metal NON metalic");
            outputTable.Rows.Add("Sealtite connector starit NM");
            outputTable.Rows.Add("Sealtite connector 90 deg NM");
            outputTable.Rows.Add("Rigid couplings");
            outputTable.Rows.Add("Threded nipple");
            outputTable.Rows.Add("EMT setscrew connector");
            outputTable.Rows.Add("EMT setscrew coupling");
            outputTable.Rows.Add("PVC coupling");
            outputTable.Rows.Add("PVC female adapter");
            outputTable.Rows.Add("PVC connectors with locknyts");
            outputTable.Rows.Add("PVC pipe");
            outputTable.Rows.Add("PVC glue");
            outputTable.Rows.Add("PVC 90 deg elbow");
            outputTable.Rows.Add("PVC 45 deg elbow");
            outputTable.Rows.Add("PVC expention couplings");
            outputTable.Rows.Add("Rigid pipe");
            outputTable.Rows.Add("Kindorf 1/2");
            outputTable.Rows.Add("Kindorf L-brackets");
            outputTable.Rows.Add("Kindorf 1 1/2");
            outputTable.Rows.Add("Kindorf L-45deg");
            outputTable.Rows.Add("Kindor 3");
            outputTable.Rows.Add("Pulling Lube");
            outputTable.Rows.Add("1/4 nuts");
            outputTable.Rows.Add("Tape-BOY");
            outputTable.Rows.Add("1/4/20x1 bolts");
            outputTable.Rows.Add("Tape- BLUE, RED");
            outputTable.Rows.Add("1/4x1 1/4 washers");
            outputTable.Rows.Add("Tape-black");
            outputTable.Rows.Add("1/4 spring nuts");
            outputTable.Rows.Add("Tape-white");
            outputTable.Rows.Add("3/8 nuts");
            outputTable.Rows.Add("Tape-green");
            outputTable.Rows.Add("3/8x1 bolts");
            outputTable.Rows.Add("Number booklet 0-9");
            outputTable.Rows.Add("3/8x1 1/4 washers");
            outputTable.Rows.Add("3/8 spring nuts");
            outputTable.Rows.Add("Roof chocks");
            outputTable.Rows.Add("PV wire Black");
            outputTable.Rows.Add("3/8 expansion anchors");
            outputTable.Rows.Add("PV wire Red");
            outputTable.Rows.Add("The Wrap plastic 11\"");
            outputTable.Rows.Add("#6 green");
            outputTable.Rows.Add("lugs 1/0");
            outputTable.Rows.Add("1/2 anchors x3\"");
            outputTable.Rows.Add("3/8 threaded rod");
            outputTable.Rows.Add("2gang bell deep + voer 1\"ko");
            outputTable.Rows.Add("1\"-1/2\" threaded reducers");
            outputTable.Rows.Add("1 1/2\"-1' threaded reducers");
            outputTable.Rows.Add("2\"-1/2\" threaded reducers");
            outputTable.Rows.Add("2 1/2\"-1' threaded reducers");
            outputTable.Rows.Add("Wiring Trough, Type 3R");
            outputTable.Rows.Add("Pull box 3R");
            outputTable.Rows.Add("U PIPE beam clamps");
            

            // Set outputDataGridView DataSource
            outputDataGridView.DataSource = outputTable;
            outputDataGridView.Columns["Column1"].Width = 200;
            outputDataGridView.Columns["1\""].Width = 55;
            outputDataGridView.Columns["1 1/2\""].Width = 55;
            outputDataGridView.Columns["2\""].Width = 55;
            outputDataGridView.Columns["2 1/2\""].Width = 55;
            outputDataGridView.Columns["3\""].Width = 55;
            outputDataGridView.Columns["4\""].Width = 55;

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
                Tag = inputTable.Rows.Count // Store the row index in the Tag property
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
            // Dictionary to group rows by their attributes (size, metric, material)
            var groupedRows = new Dictionary<(string size, string metric, string material), List<DataRow>>();

            foreach (DataRow row in inputTable.Rows)
            {
                int sets;
                bool num = true;
                string timesXSize;
                string metric;
                string material;
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

                timesXSize = row["Times x Size"].ToString();
                metric = dataGridViewRow.Cells["Metric"].Value.ToString();
                material = dataGridViewRow.Cells["Material"].Value.ToString();

                var parsedResult = ParseTimesXSize(timesXSize);

                if (parsedResult.times != 0 && parsedResult.size != null)
                {
                    var key = (parsedResult.size, metric, material);

                    if (!groupedRows.ContainsKey(key))
                    {
                        groupedRows[key] = new List<DataRow>();
                    }

                    groupedRows[key].Add(row);
                }
            }

            // Perform calculations on grouped rows
            foreach (var key in groupedRows.Keys)
            {
                string size = key.size;
                string metric = key.metric;
                string material = key.material;
                List<DataRow> rows = groupedRows[key];

                // Calculation: Multiply sets, times, and length for each row and sum them for only grouped rows
                double total = 0;
                foreach (var row in rows)
                {
                    int sets = Convert.ToInt32(row["Sets"]);
                    var parsedResult = ParseTimesXSize(row["Times x Size"].ToString());
                    int times = parsedResult.times;
                    double length = Convert.ToDouble(row["Length"]);
                    total += sets * times * length;
                }
                //Change this to put on out put table
                MessageBox.Show($"Total: {total} of Size: {size} Metric: {metric}, Material: {material}");
            }

        }
    }
}

