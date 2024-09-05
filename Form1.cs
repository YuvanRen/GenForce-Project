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
        public Panel mainPanel;
        public DataGridView inputDataGridView;
        public DataGridView outputDataGridView;
        public DataGridView priceDataGridView; // New DataGridView for Pricing Tab
        public DataGridView resultDataGridView; // New for results
        private MaterialButton addRowButton;
        private MaterialButton parseButton;
        private DataTable inputTable = new DataTable();
        private DataTable outputTable = new DataTable();
        private DataTable priceTable = new DataTable(); // New DataTable for Pricing Tab
        private DataTable resultTable = new DataTable();


        private System.ComponentModel.IContainer components;
        private ContextMenuStrip deleteMenu;
        private int deleteButton_Count = 0;

        private MaterialButton priceWizardButton;

        // New components for TabControl
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;

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
            SetupPriceTable(); // New function to set up the pricing table
            SetupResultTable(); // Result output table

            inputDataGridView.DataSource = inputTable;
            outputDataGridView.DataSource = outputTable;
            priceDataGridView.DataSource = priceTable;
            resultDataGridView.DataSource = resultTable;

            // Enable double buffering on the DataGridView to improve performance
            EnableDoubleBuffering(outputDataGridView);
            EnableDoubleBuffering(priceDataGridView);
            EnableDoubleBuffering(resultDataGridView);
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
                if (control is DataGridView dataGridView)
                {
                    // Clear the selection to prevent the first cell from being selected
                    dataGridView.ClearSelection();
                }
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
            tabPage1 = new TabPage("Material");
            tabPage2 = new TabPage("Wires");
            tabPage3 = new TabPage("Pricing");
            tabPage4 = new TabPage("Calculation Results");

            mainPanel = new Panel();
            inputDataGridView = new DataGridView();
            outputDataGridView = new DataGridView();
            priceDataGridView = new DataGridView(); // New DataGridView for Pricing Tab
            resultDataGridView = new DataGridView();

            addRowButton = new MaterialButton();
            parseButton = new MaterialButton();
            deleteMenu = new ContextMenuStrip(components);
            priceWizardButton = new MaterialButton();

            // Instructional Panel for Wires tab
            Panel instructionPanel = new Panel
            {
                Size = new Size(250, 275), // Adjusted size
                Location = new Point(615, 20), // Positioned to the right side of the main panel
                BorderStyle = BorderStyle.FixedSingle
            };

            Label instructionLabel = new Label
            {
                Text = "Breakdown:\n\n1. Fill up needed ammount of each material. \n\n2.Edit prices in pricing tab. \n\n3. Click 'Price Wizard' in this tab to get estimate. \n\n4. Fill in wire tab.\n\n Finally check results tab for estimate.",
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10)
            };

            instructionPanel.Controls.Add(instructionLabel);

            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceDataGridView).BeginInit(); // Initialize Pricing DataGridView
            SuspendLayout();

            Controls.Add(toolbar.ToolStrip); //ToolStrip

            // TabControl setup
            tabControl.Dock = DockStyle.Fill;
            tabControl.TabPages.Add(tabPage1);
            tabControl.TabPages.Add(tabPage2);
            tabControl.TabPages.Add(tabPage3); // Add the Pricing Tab
            tabControl.TabPages.Add(tabPage4);

            // Add TabControl to the Form
            Controls.Add(tabControl);

            // Setup toolbar
            Controls.Add(toolbar.ToolStrip); // ToolStrip
            toolbar.ToolStrip.Dock = DockStyle.Top;

            // Main panel setup
            mainPanel.AutoScroll = true;
            mainPanel.Controls.Add(inputDataGridView);
            mainPanel.Location = new Point(10, 10); // Adjusted to move closer to the top
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(600, 400); // Adjust size as needed
            mainPanel.TabIndex = 1;

            // Input DataGridView setup
            inputDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control; // Set to the same as normal background
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            inputDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            inputDataGridView.ColumnHeadersHeight = 40;
            inputDataGridView.Location = new Point(0, 0);
            inputDataGridView.Name = "inputDataGridView";
            inputDataGridView.Size = new Size(527, 400); // Adjust size as needed
            inputDataGridView.TabIndex = 0;
            inputDataGridView.RowHeadersVisible = false;

            // Output DataGridView setup
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control; // Set to the same as normal background
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            outputDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            outputDataGridView.ColumnHeadersHeight = 40;
            outputDataGridView.Location = new Point(10, 10); // Adjusted to be inside the second tab
            outputDataGridView.Name = "outputDataGridView";
            outputDataGridView.Size = new Size(550, 400);
            outputDataGridView.TabIndex = 1;

            // Price DataGridView setup
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control; // Set to the same as normal background
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            priceDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            priceDataGridView.ColumnHeadersHeight = 40;
            priceDataGridView.AllowUserToAddRows = false;
            priceDataGridView.ColumnHeadersHeight = 40;
            priceDataGridView.Location = new Point(10, 10);
            priceDataGridView.Name = "priceDataGridView";
            priceDataGridView.Size = new Size(822, 400);
            priceDataGridView.TabIndex = 2;

            // Result DataGridView setup
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            resultDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            resultDataGridView.ColumnHeadersHeight = 40;
            resultDataGridView.AllowUserToAddRows = false;
            resultDataGridView.ColumnHeadersHeight = 40;
            resultDataGridView.Location = new Point(10, 10);
            resultDataGridView.Name = "resultDataGridView";
            resultDataGridView.Size = new Size(600, 400);
            resultDataGridView.TabIndex = 3;

            // Optimize DataGridView settings for smooth scrolling
            outputDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            outputDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            outputDataGridView.RowHeadersVisible = false;
            priceDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            priceDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            priceDataGridView.RowHeadersVisible = false;

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
            Controls.Add(priceDataGridView);
            Controls.Add(resultDataGridView);

            Controls.Add(addRowButton);
            Controls.Add(parseButton);

            Name = "Form1";
            Text = "GenForce-SW";
            Load += Form1_Load;

            // Add existing components to the first TabPage
            tabPage1.Controls.Add(instructionPanel);
            tabPage1.Controls.Add(outputDataGridView);
            tabPage1.Controls.Add(priceWizardButton);

            // Add the outputDataGridView to the second TabPage
            tabPage2.Controls.Add(mainPanel);
            tabPage2.Controls.Add(addRowButton);
            tabPage2.Controls.Add(parseButton);

            // Add the priceDataGridView to the third TabPage
            tabPage3.Controls.Add(priceDataGridView);

            tabPage4.Controls.Add(resultDataGridView);

            inputDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            outputDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            priceDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            resultDataGridView.EditMode = DataGridViewEditMode.EditOnEnter;

            inputDataGridView.CellClick += (s, e) => { inputDataGridView.ClearSelection(); };
            outputDataGridView.CellClick += (s, e) => { outputDataGridView.ClearSelection(); };
            priceDataGridView.CellClick += (s, e) => { priceDataGridView.ClearSelection(); };
            resultDataGridView.CellClick += (s, e) => { resultDataGridView.ClearSelection(); };

            mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)inputDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceDataGridView).EndInit(); // Initialize Pricing DataGridView
            ResumeLayout(false);
            PerformLayout();
        }

        private void SetupResultTable()
        {
            resultTable.Columns.Add("ORDER QUANT");
            resultTable.Columns.Add("DESCRIPTION");
            resultTable.Columns.Add("UNIT PRICE");
            resultTable.Columns.Add("TOTAL");

            resultDataGridView.DataSource = resultTable;

            resultDataGridView.Columns["ORDER QUANT"].Width = 75;
            resultDataGridView.Columns["DESCRIPTION"].Width = 275;
            resultDataGridView.Columns["UNIT PRICE"].Width = 125;
            resultDataGridView.Columns["TOTAL"].Width = 125;
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
                FlatStyle = FlatStyle.Flat // Ensures smooth drop-down appearance
            };

            DataGridViewComboBoxColumn materialColumn = new DataGridViewComboBoxColumn
            {
                Name = "Material",
                HeaderText = "Material",
                DataSource = new string[] { "CU", "AL" },
                FlatStyle = FlatStyle.Flat // Ensures smooth drop-down appearance
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

            // Set the width of each column
            outputDataGridView.Columns["Column1"].Width = 200;
            outputDataGridView.Columns["1\""].Width = 55;
            outputDataGridView.Columns["1 1/2\""].Width = 55;
            outputDataGridView.Columns["2\""].Width = 55;
            outputDataGridView.Columns["2 1/2\""].Width = 55;
            outputDataGridView.Columns["3\""].Width = 55;
            outputDataGridView.Columns["4\""].Width = 55;
        }

        private void SetupPriceTable()
        {
            // Define columns similar to the output table
            priceTable.Columns.Add("Column1");
            priceTable.Columns.Add("1\"");
            priceTable.Columns.Add("1 1/2\"");
            priceTable.Columns.Add("2\"");
            priceTable.Columns.Add("2 1/2\"");
            priceTable.Columns.Add("3\"");
            priceTable.Columns.Add("4\"");

            // Set prices
            priceTable.Rows.Add("EMT Pipe");
            priceTable.Rows.Add("EMT Connector compression");
            priceTable.Rows.Add("EMT coupling compression");
            priceTable.Rows.Add("Grounding bushing");
            priceTable.Rows.Add("Plastic bushing");
            priceTable.Rows.Add("Cowboy strap");
            priceTable.Rows.Add("EMT kindorf strap");
            priceTable.Rows.Add("Myers hub");
            priceTable.Rows.Add("EMT 90 deg elbow");
            priceTable.Rows.Add("EMT 45 deg elbow");
            priceTable.Rows.Add("EMT 22 1/2 deg elbow");
            priceTable.Rows.Add("EMT 15 deg elbow");
            priceTable.Rows.Add("Threded Mogule LB");
            priceTable.Rows.Add("Threded Type C");
            priceTable.Rows.Add("Threded Type LB");
            priceTable.Rows.Add("Threded Type LR");
            priceTable.Rows.Add("Threded Type LL");
            priceTable.Rows.Add("Threded T");
            priceTable.Rows.Add("Sealtite Metal NON metalic");
            priceTable.Rows.Add("Sealtite connector starit NM");
            priceTable.Rows.Add("Sealtite connector 90 deg NM");
            priceTable.Rows.Add("Rigid couplings");
            priceTable.Rows.Add("Threded nipple");
            priceTable.Rows.Add("EMT setscrew connector");
            priceTable.Rows.Add("EMT setscrew coupling");
            priceTable.Rows.Add("PVC coupling");
            priceTable.Rows.Add("PVC female adapter");
            priceTable.Rows.Add("PVC connectors with locknyts");
            priceTable.Rows.Add("PVC pipe");
            priceTable.Rows.Add("PVC glue");
            priceTable.Rows.Add("PVC 90 deg elbow");
            priceTable.Rows.Add("PVC 45 deg elbow");
            priceTable.Rows.Add("PVC expention couplings");
            priceTable.Rows.Add("Rigid pipe");
            priceTable.Rows.Add("Kindorf 1/2");
            priceTable.Rows.Add("Kindorf L-brackets");
            priceTable.Rows.Add("Kindorf 1 1/2");
            priceTable.Rows.Add("Kindorf L-45deg");
            priceTable.Rows.Add("Kindor 3");
            priceTable.Rows.Add("Pulling Lube");
            priceTable.Rows.Add("1/4 nuts");
            priceTable.Rows.Add("Tape-BOY");
            priceTable.Rows.Add("1/4/20x1 bolts");
            priceTable.Rows.Add("Tape- BLUE, RED");
            priceTable.Rows.Add("1/4x1 1/4 washers");
            priceTable.Rows.Add("Tape-black");
            priceTable.Rows.Add("1/4 spring nuts");
            priceTable.Rows.Add("Tape-white");
            priceTable.Rows.Add("3/8 nuts");
            priceTable.Rows.Add("Tape-green");
            priceTable.Rows.Add("3/8x1 bolts");
            priceTable.Rows.Add("Number booklet 0-9");
            priceTable.Rows.Add("3/8x1 1/4 washers");
            priceTable.Rows.Add("3/8 spring nuts");
            priceTable.Rows.Add("Roof chocks");
            priceTable.Rows.Add("PV wire Black");
            priceTable.Rows.Add("3/8 expansion anchors");
            priceTable.Rows.Add("PV wire Red");
            priceTable.Rows.Add("The Wrap plastic 11\"");
            priceTable.Rows.Add("#6 green");
            priceTable.Rows.Add("lugs 1/0");
            priceTable.Rows.Add("1/2 anchors x3\"");
            priceTable.Rows.Add("3/8 threaded rod");
            priceTable.Rows.Add("2gang bell deep + voer 1\"ko");
            priceTable.Rows.Add("1\"-1/2\" threaded reducers");
            priceTable.Rows.Add("1 1/2\"-1' threaded reducers");
            priceTable.Rows.Add("2\"-1/2\" threaded reducers");
            priceTable.Rows.Add("2 1/2\"-1' threaded reducers");
            priceTable.Rows.Add("Wiring Trough, Type 3R");
            priceTable.Rows.Add("Pull box 3R");
            priceTable.Rows.Add("U PIPE beam clamps");


            priceDataGridView.DataSource = priceTable;

            priceDataGridView.Columns["Column1"].Width = 200;
            priceDataGridView.Columns["1\""].Width = 100;
            priceDataGridView.Columns["1 1/2\""].Width = 100;
            priceDataGridView.Columns["2\""].Width = 100;
            priceDataGridView.Columns["2 1/2\""].Width = 100;
            priceDataGridView.Columns["3\""].Width = 100;
            priceDataGridView.Columns["4\""].Width = 100;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Add initial data or any required setup here
        }

        public void DeleteRow(object sender, EventArgs e)
        {
            if (deleteMenu.Tag != null)
            {
                int rowIndex = (int)deleteMenu.Tag;
                if (rowIndex >= 0 && rowIndex < inputDataGridView.Rows.Count)
                {
                    inputDataGridView.Rows.RemoveAt(rowIndex);
                }
            }
        }
        private void buttonAddRow_Click(object? sender, EventArgs e)
        {
            AddNewRow();
        }

        public void AddNewRow()
        {
            // Add the new row to the DataTable
            DataRow newRow = inputTable.NewRow();
            inputTable.Rows.Add(newRow);

            // Only create the delete button if there are rows
            if (inputDataGridView.Rows.Count > 0)
            {
                // Create the delete button
                Button deleteButton = new Button
                {
                    Text = "...",
                    Size = new Size(40, 20),
                    Tag = inputTable.Rows.Count - 1 // Store the row index in the Tag property
                };
                deleteButton.Click += DeleteButton_Click;

                deleteButton_Count += 1; // counts all delete buttons

                // Position the button relative to the newly added row
                mainPanel.Controls.Add(deleteButton);
                UpdateButtonPositions();
            }
        }


        //Handler for when ... it clicked
        public void DeleteButton_Click(object sender, EventArgs e)
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

        public void UpdateButtonPositions()
        {
            int buttonIndex = 0;
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Button deleteButton && deleteButton.Text == "...")
                {
                    if (buttonIndex < inputDataGridView.Rows.Count)
                    {
                        DataGridViewRow row = inputDataGridView.Rows[buttonIndex];
                        Rectangle rowRect = inputDataGridView.GetRowDisplayRectangle(row.Index, true);

                        // Correctly position the button aligned with the row, not the header
                        deleteButton.Location = new Point(inputDataGridView.Location.X + inputDataGridView.Width + 5,
                                                          mainPanel.AutoScrollPosition.Y + rowRect.Top + row.Height / 2 - deleteButton.Height / 2);

                        // Update the tag with the current row index
                        deleteButton.Tag = buttonIndex;

                        buttonIndex++;
                    }
                    else
                    {
                        // If there's an extra button, remove it
                        mainPanel.Controls.Remove(deleteButton);
                        deleteButton.Dispose();
                    }
                }
            }
        }

        private (int times, string size) ParseTimesXSize(string timesXSize, DataGridViewCell cell)
        {
            if (string.IsNullOrWhiteSpace(timesXSize))
            {
                cell.Style.BackColor = Color.Red;
                MessageBox.Show("The 'Times x Size' field cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return (0, null);
            }

            var parts = timesXSize.Split('x');
            if (parts.Length != 2)
            {
                cell.Style.BackColor = Color.Red;
                MessageBox.Show("The 'Times x Size' format is incorrect. It should be in the format 'Times x Size' (e.g., '3 x 12').", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return (0, null);
            }

            if (!int.TryParse(parts[0].Trim(), out int times))
            {
                cell.Style.BackColor = Color.Red;
                MessageBox.Show("The 'Times' part of 'Times x Size' must be an integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return (0, null);
            }

            var size = parts[1].Trim();
            if (string.IsNullOrWhiteSpace(size))
            {
                cell.Style.BackColor = Color.Red;
                MessageBox.Show("The 'Size' part of 'Times x Size' cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return (0, null);
            }

            cell.Style.BackColor = Color.White; // Reset background color if no error
            return (times, size);
        }

        // Calls Parsing
        private void buttonParse_Click(object? sender, EventArgs e)
        {
            // Dictionary to group rows by their attributes (size, metric, material)
            var groupedRows = new Dictionary<(string size, string metric, string material), List<DataRow>>();
            bool allFieldsValid = true;

            foreach (DataRow row in inputTable.Rows)
            {
                int sets;
                bool num = true;
                string timesXSize;
                string metric;
                string material;
                int length;
                int rowIndex = inputTable.Rows.IndexOf(row);
                DataGridViewRow dataGridViewRow = inputDataGridView.Rows[rowIndex];

                // Handle 'Sets' column
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
                        MessageBox.Show(ex.Message + " Must be an Integer!");
                        num = false;
                    }

                    if (num)
                    {
                        sets = Convert.ToInt32(row["Sets"]);
                        dataGridViewRow.Cells["Sets"].Style.BackColor = Color.White;
                    }
                }

                timesXSize = row["Times x Size"].ToString();
                var timesXSizeCell = dataGridViewRow.Cells["Times x Size"];

                // This does error handling for this cell
                var parsedResult = ParseTimesXSize(timesXSize, timesXSizeCell);

                // Handle 'Metric' column
                metric = dataGridViewRow.Cells["Metric"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(metric))
                {
                    dataGridViewRow.Cells["Metric"].Style.BackColor = Color.Red;
                    MessageBox.Show($"Row {rowIndex + 1}: 'Metric' cannot be empty.");
                    allFieldsValid = false;
                }
                else
                {
                    dataGridViewRow.Cells["Metric"].Style.BackColor = Color.White;
                }

                // Handle 'Material' column
                material = dataGridViewRow.Cells["Material"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(material))
                {
                    dataGridViewRow.Cells["Material"].Style.BackColor = Color.Red;
                    MessageBox.Show($"Row {rowIndex + 1}: 'Material' cannot be empty.");
                    allFieldsValid = false;
                }
                else
                {
                    dataGridViewRow.Cells["Material"].Style.BackColor = Color.White;
                }

                // Handle 'Length' column
                if (row["Length"] == DBNull.Value)
                {
                    dataGridViewRow.Cells["Length"].Style.BackColor = Color.Red;
                    MessageBox.Show($"Row {rowIndex + 1}: 'Length' cannot be empty.");
                    allFieldsValid = false;
                }
                else
                {
                    try
                    {
                        length = Convert.ToInt32(row["Length"]);
                        dataGridViewRow.Cells["Length"].Style.BackColor = Color.White;
                    }
                    catch (FormatException ex)
                    {
                        dataGridViewRow.Cells["Length"].Style.BackColor = Color.Red;
                        MessageBox.Show(ex.Message + " Must be an Integer!");
                        allFieldsValid = false;
                    }
                }

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

            // Call the calculation function only if all fields are valid
            if (allFieldsValid)
            {
                PerformCalculations(groupedRows);
            }
            else
            {
                MessageBox.Show("Please correct the highlighted fields before proceeding.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Function to perform calculations on grouped rows
        private void PerformCalculations(Dictionary<(string size, string metric, string material), List<DataRow>> groupedRows)
        {
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
                    int rowIndex = inputTable.Rows.IndexOf(row);
                    DataGridViewRow dataGridViewRow = inputDataGridView.Rows[rowIndex];
                    int sets = Convert.ToInt32(row["Sets"]);
                    var parsedResult = ParseTimesXSize(row["Times x Size"].ToString(), dataGridViewRow.Cells["Times x Size"]);
                    int times = parsedResult.times;
                    double length = Convert.ToDouble(row["Length"]);
                    total += sets * times * length;
                }
                // Add the result to the output table
                resultTable.Rows.Add($"{total}'",$"{size} {metric} {material}"," ");
            }
        }


        // Material pricing
        private void buttonPriceWizard_Click(object sender, EventArgs e)
        {
            // Create material calculation instance and call for pricing calculation
            CalculatePricing();

        }
        private void CalculatePricing()
        {
            resultTable.Clear();
            double total = 0.0;
            bool missingPrice = false;

            foreach (DataRow row in outputTable.Rows)
            {
                string materialName = row["Column1"].ToString();
                for (int i = 1; i < outputTable.Columns.Count; i++)
                {
                    string columnName = outputTable.Columns[i].ColumnName;
                    if (!string.IsNullOrEmpty(row[columnName].ToString()))
                    {
                        double orderQuantity = Convert.ToDouble(row[columnName]);

                        // Check if the corresponding price cell is empty
                        DataRow[] priceRow = priceTable.Select($"Column1 = '{materialName}'");
                        if (priceRow.Length > 0 && !string.IsNullOrEmpty(priceRow[0][columnName].ToString()))
                        {
                            double unitPrice = Convert.ToDouble(priceRow[0][columnName]);
                            double extendedPrice = orderQuantity * unitPrice;
                            total += extendedPrice;

                            // Concatenate material name with measurement
                            string description = $"{materialName} ({columnName})";
                            resultTable.Rows.Add(orderQuantity, description, unitPrice, extendedPrice);
                        }
                        else
                        {
                            // If the price is missing, inform the user
                            missingPrice = true;
                            MessageBox.Show($"Price is missing for '{materialName}' with measurement '{columnName}'. Please fill in the price before proceeding.",
                                            "Missing Price",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            break; // Stop processing further until the issue is resolved
                        }
                    }
                }

                if (missingPrice)
                {
                    break; // Stop processing further if any price is missing
                }
            }

            if (!missingPrice)
            {
                // Add Net Total row only if no price is missing
                resultTable.Rows.Add("", "NET TOTAL", "", total);
                tabControl.SelectedTab = tabPage4; // Switch to the Calculation Results tab
            }
        }
    }
}
