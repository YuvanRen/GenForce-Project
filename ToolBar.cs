using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GenForce
{
    public class ToolBar : Form
    {
        private ToolStrip toolStrip;
        private ToolStripDropDownButton file_Button;
        private ToolStripDropDownButton edit_Button;
        private ToolStripButton restart_Button;
        private Form1 mainForm;

        public ToolStrip ToolStrip => toolStrip; // Expose ToolStrip


        public ToolBar(Form1 form)
        {
            mainForm = form;
            InitializeToolbar();
        }

        private void InitializeToolbar()
        {
            toolStrip = new ToolStrip();
            file_Button = new ToolStripDropDownButton();
            edit_Button = new ToolStripDropDownButton();
            restart_Button = new ToolStripButton();

            toolStrip.Items.AddRange(new ToolStripItem[] { file_Button, edit_Button, restart_Button });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(800, 25);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "toolStrip";


            // file Button
            file_Button.Text = "File";
            file_Button.DropDownItems.Add("New", null, newItem_Click);
            file_Button.DropDownItems.Add("Open", null, openFile_Click);
            file_Button.DropDownItems.Add("Save", null, saveFile_Click);
            file_Button.DropDownItems.Add("Save As", null, saveFileAs_Click);
            file_Button.DropDownItems.Add(new ToolStripSeparator());
            file_Button.DropDownItems.Add("Exit", null, exitApp_Click);



            // edit Button
            edit_Button.Text = "Edit";
            edit_Button.DropDownItems.Add("Add Row(s)", null, addRows_Click);
            edit_Button.DropDownItems.Add("Delete Row(s)", null, deleteRows_Click);
            edit_Button.DropDownItems.Add("Reset Table", null, resetTable_Click);


            // restart app for whatever reason incase it bugs out
            restart_Button.Text = "Restart App";
            restart_Button.Click += restartButton_Click;

        }

        // ------------------------------------------------------------------------- File functions
        private void newItem_Click(object sender, EventArgs e)
        {
  
            MessageBox.Show("New menu item clicked");
        }

        private void openFile_Click(object sender, EventArgs e) // Requires the logic aspect of the table to be completed in order for me to specify what gets opened
        {
            string dir_Downloads = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads"; 
            Process.Start("explorer.exe", dir_Downloads);
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
          
            MessageBox.Show("Save menu item clicked");
        }

        private void saveFileAs_Click(object sender, EventArgs e)
        {
 
            MessageBox.Show("Save menu item clicked");
        }

        private void exitApp_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        // ------------------------------------------------------------------------- Edit functions
        private void addRows_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Add Rows");
        }

        private void deleteRows_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Delete Rows");
        }

        private void resetTable_Click(object sender, EventArgs e)
        {

            mainForm.ResetTable();
        }

        // ---------------------------------------------------------------------- Undecided functions

        private void restartButton_Click(object sender, EventArgs e)
        {
            //Compilier assigns the most appropriate datatype with the "var" keyword, kinda crazy ngl

            var result = MessageBox.Show("Are you sure you want to restart the application?", "Restart Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if(result == DialogResult.Yes)
            {
                Application.Restart();
            }

        }
    }
}
