using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace GenForce
{
    public class Form1 : MaterialForm
    {
        private ToolBar toolbar;
        private Panel mainPanel;
        public DataGridView inputDataGridView;
        public DataGridView outputDataGridView;
        private MaterialButton addRowButton;
        private MaterialButton parseButton;
        private DataTable inputTable = new DataTable();
        private DataTable outputTable = new DataTable();
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip deleteMenu;
        private int deleteButton_Count = 0;

        private MaterialButton priceWizardButton;

        // New components for TabControl
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;

        public Form1()
        {
            // Initialize MaterialSkinManager
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepOrange900, Primary.Grey700, Primary.Grey200, Accent.LightBlue200, TextShade.WHITE);

            InitializeComponent();

            // Enable double buffering for smoother tab switching
            EnableDoubleBuffering(tabControl);

            // Hook into the tab control's events for smoother transitions
            tabControl.Selecting += new TabControlCancelEventHandler(tabControl_Selecting);
            tabControl.Selected += new TabControlEventHandler(tabControl_Selected);

            SetupInputTable();
            SetupOutputTable();
            inputDataGridView.DataSource = inputTable;
            outputDataGridView.DataSource = outputTable;

            // Enable double buffering on the DataGridView to improve performance
            EnableDoubleBuffering(outputDataGridView);
        }

        private void EnableDoubleBuffering(Control control)
        {
            System.Reflection.PropertyInfo doubleBufferPropertyInfo =
                control.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            doubleBufferPropertyInfo.SetValue(control, true, null);
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            foreach (Control control in e.TabPage.Controls)
            {
                control.Visible = false;
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            foreach (Control control in e.TabPage.Controls)
            {
                control.Visible = true;
            }
        }

        private void InitializeComponent()
        {
            toolbar = new ToolBar(this);

            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();

            // Initialize TabControl and TabPages
            tabControl = new TabControl();
            tabPage1 = new TabPage("Wires");
            tabPage2 = new TabPage("Material");

            mainPanel = new Panel();
            inputDataGridView = new DataGridView();
            outputDataGridView = new DataGridView();
            addRowButton = new MaterialButton();
            parseButton = new MaterialButton();
            deleteMenu = new ContextMenuStrip(components);
            priceWizardButton = new MaterialButton();

            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).BeginInit();
            SuspendLayout();

            Controls.Add(toolbar.ToolStrip); //ToolStrip

            // TabControl setup
            tabControl.Dock = DockStyle.Fill;
            tabControl.TabPages.Add(tabPage1);
            tabControl.TabPages.Add(tabPage2);

            // Add TabControl to the Form
            Controls.Add(tabControl);

            // Setup toolbar
            Controls.Add(toolbar.ToolStrip); // ToolStrip
            toolbar.ToolStrip.Dock = DockStyle.Top;

            // Main panel setup
            mainPanel.AutoScroll = true;
            mainPanel.Controls.Add(inputDataGridView);
            mainPanel.Location = new Point(12, 25); // Adjusted to move closer to the top
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(670, 400); // Adjust size as needed
            mainPanel.TabIndex = 1;

            // Input DataGridView setup
            inputDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            inputDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            inputDataGridView.ColumnHeadersHeight = 40;
            inputDataGridView.Location = new Point(0, 0);
            inputDataGridView.Name = "inputDataGridView";
            inputDataGridView.Size = new Size(527, 400); // Adjust size as needed
            inputDataGridView.TabIndex = 0;
            inputDataGridView.RowHeadersVisible = false;

            // Output DataGridView setup
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            outputDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            outputDataGridView.ColumnHeadersHeight = 40;
            outputDataGridView.Location = new Point(10, 10); // Adjusted to be inside the second tab
            outputDataGridView.Name = "outputDataGridView";
            outputDataGridView.Size = new Size(752, 400);
            outputDataGridView.TabIndex = 1;

            // Optimize DataGridView settings for smooth scrolling
            outputDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            outputDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            outputDataGridView.RowHeadersVisible = false;

            // Add Row Button setup
            addRowButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            addRowButton.Density = MaterialButton.MaterialButtonDensity.Default;
            addRowButton.Depth = 0;
            addRowButton.HighEmphasis = true;
            addRowButton.Icon = null;
            addRowButton.Location = new Point(12, 440); // Adjusted to move closer
            addRowButton.Margin = new Padding(4, 6, 4, 6);
            addRowButton.MouseState = MouseState.HOVER;
            addRowButton.Name = "addRowButton";
            addRowButton.NoAccentTextColor = Color.Empty;
            addRowButton.Size = new Size(86, 36);
            addRowButton.TabIndex = 2;
            addRowButton.Text = "Add Row";
            addRowButton.Type = MaterialButton.MaterialButtonType.Contained;
            addRowButton.UseAccentColor = false;
            addRowButton.Click += buttonAddRow_Click;

            // Parse Button setup
            parseButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            parseButton.Density = MaterialButton.MaterialButtonDensity.Default;
            parseButton.Depth = 0;
            parseButton.HighEmphasis = true;
            parseButton.Icon = null;
            parseButton.Location = new Point(120, 440); // Adjusted to move closer
            parseButton.Margin = new Padding(4, 6, 4, 6);
            parseButton.MouseState = MouseState.HOVER;
            parseButton.Name = "parseButton";
            parseButton.NoAccentTextColor = Color.Empty;
            parseButton.Size = new Size(151, 36);
            parseButton.TabIndex = 3;
            parseButton.Text = "Material Wizard";
            parseButton.Type = MaterialButton.MaterialButtonType.Contained;
            parseButton.UseAccentColor = false;
            parseButton.Click += buttonParse_Click;

            // Price Wizard Button setup
            priceWizardButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            priceWizardButton.Density = MaterialButton.MaterialButtonDensity.Default;
            priceWizardButton.Depth = 0;
            priceWizardButton.HighEmphasis = true;
            priceWizardButton.Icon = null;
            priceWizardButton.Location = new Point(12, 420);
            priceWizardButton.Margin = new Padding(4, 6, 4, 6);
            priceWizardButton.MouseState = MouseState.HOVER;
            priceWizardButton.Name = "priceWizardButton";
            priceWizardButton.NoAccentTextColor = Color.Empty;
            priceWizardButton.Size = new Size(151, 36);
            priceWizardButton.TabIndex = 4;
            priceWizardButton.Text = "Price Wizard";
            priceWizardButton.Type = MaterialButton.MaterialButtonType.Contained;
            priceWizardButton.UseAccentColor = false;
            priceWizardButton.Click += buttonPriceWizard_Click;

            // Delete Menu setup
            deleteMenu.ImageScalingSize = new Size(20, 20);
            deleteMenu.Name = "deleteMenu";
            deleteMenu.Size = new Size(61, 4);

            // Form setup
            AutoSize = true;
            ClientSize = new Size(1200, 600);

            Controls.Add(mainPanel);
            mainPanel.Controls.Add(inputDataGridView);
            Controls.Add(outputDataGridView);
            Controls.Add(addRowButton);
            Controls.Add(parseButton);

            Name = "Form1";
            Text = "GenForce-SW";
            Load += Form1_Load;

            // Add existing components to the first TabPage
            tabPage1.Controls.Add(mainPanel);
            tabPage1.Controls.Add(addRowButton);
            tabPage1.Controls.Add(parseButton);

            // Add the outputDataGridView to the second TabPage
            tabPage2.Controls.Add(outputDataGridView);
            tabPage2.Controls.Add(priceWizardButton);

            inputDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            outputDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;

            mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void SetupInputTable()
        {
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
                DataSource = new string[] { "AWG", "KCMIL", "MCM" },
                FlatStyle = FlatStyle.Popup // Ensures smooth drop-down appearance
            };

            DataGridViewComboBoxColumn materialColumn = new DataGridViewComboBoxColumn
            {
                Name = "Material",
                HeaderText = "Material",
                DataSource = new string[] { "CU", "AL" },
                FlatStyle = FlatStyle.Popup // Ensures smooth drop-down appearance
            };

            inputDataGridView.DataSource = inputTable;

            // Set Width
            inputDataGridView.Columns["Letter"].Width = 50;
            inputDataGridView.Columns["Sets"].Width = 50;
            inputDataGridView.Columns["Times x Size"].Width = 100;
            inputDataGridView.Columns["Minimum Conduit"].Width = 70;
            inputDataGridView.Columns["Length"].Width = 55;

            // Add ComboBox columns to the DataGridView at specified positions
            inputDataGridView.Columns.Insert(3, metricColumn);
            inputDataGridView.Columns.Insert(4, materialColumn);

            // Set up the delete context menu
            deleteMenu.Items.Add("Delete Row", null, DeleteRow);

            // Add the Default Row
            DataRow newRow = inputTable.NewRow();
            AddNewRow();
        }

        public void ResetTable()
        {
            inputTable.Clear(); // Clear all data from the DataTable
            inputTable.Rows.Clear(); // Clear all rows from the DataTable

            List<Button> buttonsToRemove = new List<Button>(); // Keeping track of all buttons

            foreach (Control control in mainPanel.Controls)
            {
                if (control is Button deleteButton && deleteButton.Text == "...")
                {
                    buttonsToRemove.Add(deleteButton);
                }
            }

            for (int i = 0; i < buttonsToRemove.Count; i++) // removes delete buttons
            {
                mainPanel.Controls.Remove(buttonsToRemove[i]);
            }

            AddNewRow(); // invoked once to return to default state
        }

        private void SetupOutputTable()
        {
            // Define columns similar to the image
            outputTable.Columns.Add("Column1");
            outputTable.Columns.Add("BARCODE");
            outputTable.Columns.Add("1\"");
            outputTable.Columns.Add("1 1/2\"");
            outputTable.Columns.Add("2\"");
            outputTable.Columns.Add("2 1/2\"");
            outputTable.Columns.Add("3\"");
            outputTable.Columns.Add("4\"");
            outputTable.Columns.Add("Pricing");

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

            // Set the width of each column
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

        private void buttonAddRow_Click(object? sender, EventArgs e)
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

            deleteButton_Count += 1; // counts all delete buttons

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

        // Parses the Times x Size and returns it, if empty returns nothing 
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

        // Calls the parsing function and displays the parse - 1st update
        // Gets the other needed values
        private void buttonParse_Click(object? sender, EventArgs e)
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
                // Change this to put on output table
                outputTable.Rows.Add($"{total}' of Size: {size} {metric} {material}");
            }
        }

        // Material pricing
        private void buttonPriceWizard_Click(object sender, EventArgs e)
        {
            // Implement what should happen when the "Price Wizard" button is clicked
            MessageBox.Show("Price Wizard button clicked!", "Price Wizard");
        }
    }
}
