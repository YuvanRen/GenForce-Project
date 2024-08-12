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
            file_Button.DropDownItems.Add("Save as", null, saveFileAs_Click);
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

            // Create a new instance of the main form
            Form1 newForm = new Form1();

            // Show the new form
            newForm.Show();
        }

        private void openFile_Click(object sender, EventArgs e) // Requires the logic aspect of the table to be completed in order for me to specify what gets opened
        {
            string dir_Downloads = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            Process.Start("explorer.exe", dir_Downloads);
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog to ask the user where to save the file
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",  // Set the file types
                Title = "Save DataGridView to CSV",                       // Set the title of the dialog
                DefaultExt = "csv",                                       // Set the default extension
                AddExtension = true                                       // Automatically add the extension
            };

            // Show the dialog and if the user clicks Save
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Call the method to save the DataGridView content to a CSV file
                    SaveDataGridViewToCSV(mainForm.inputDataGridView, saveFileDialog.FileName);
                    MessageBox.Show("File saved successfully!", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while saving the file:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveDataGridViewToCSV(DataGridView dataGridView, string filePath)
        {
            // Create a StreamWriter to write to the file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the header row
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    writer.Write(dataGridView.Columns[i].HeaderText);
                    if (i < dataGridView.Columns.Count - 1)
                    {
                        writer.Write(","); // Add a comma between columns
                    }
                }
                writer.WriteLine(); // Move to the next line after writing the header row

                // Write all the data rows
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        writer.Write(row.Cells[i].Value?.ToString());
                        if (i < dataGridView.Columns.Count - 1)
                        {
                            writer.Write(","); // Add a comma between columns
                        }
                    }
                    writer.WriteLine(); // Move to the next line after writing each row
                }
            }
        }


        // TODO: give option to print input or output table
        private void saveFileAs_Click(object sender, EventArgs e)
        {
            DataGridView selectedDgv = mainForm.inputDataGridView;

            if (selectedDgv != null)
            {
                printInputTable printer = new printInputTable(selectedDgv);
                printer.Print();
            }
            else
            {
                MessageBox.Show("Please select a DataGridView to print.");
            }
            MessageBox.Show("Save menu item clicked");
        }

        private void exitApp_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        // ------------------------------------------------------------------------- Edit functions
        private void addRows_Click(object sender, EventArgs e)
        {

            mainForm.AddNewRow();
        }

        private void deleteRows_Click(object sender, EventArgs e)
        {
            // Check if there are any rows to delete
            if (mainForm.inputDataGridView.Rows.Count > 0)
            {
                // Calculate the index of the last row
                int lastRowIndex = mainForm.inputDataGridView.Rows.Count - 1;

                // Remove the last row
                mainForm.inputDataGridView.Rows.RemoveAt(lastRowIndex);

                // Find and remove the corresponding delete button
                Button buttonToRemove = null;
                foreach (Control control in mainForm.mainPanel.Controls)
                {
                    if (control is Button deleteButton && deleteButton.Tag != null && (int)deleteButton.Tag == lastRowIndex)
                    {
                        buttonToRemove = deleteButton;
                        break;
                    }
                }

                if (buttonToRemove != null)
                {
                    mainForm.mainPanel.Controls.Remove(buttonToRemove);
                    buttonToRemove.Dispose(); // Dispose of the button to remove it completely
                }

                // Update the positions and tags of the remaining delete buttons
                mainForm.UpdateButtonPositions();
            }
            else
            {
                MessageBox.Show("No rows available to delete.", "Delete Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }

        }
    }
}
