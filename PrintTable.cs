using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GenForce
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class printInputTable
    {
        private DataGridView dgv;
        private PrintDocument printDocument;
        private int currentPage;
        private int totalPages;
        private int rowsPerPage;
        private int columnCount;
        private float pageWidth, pageHeight;
        private float leftMargin = 50f, topMargin = 50f;
        private float cellHeight = 25f, cellWidth = 100f;
        private float headerHeight = 35f;

        public printInputTable(DataGridView dgv)
        {
            this.dgv = dgv;
            this.printDocument = new PrintDocument();
            this.printDocument.PrintPage += PrintPage;
            this.printDocument.BeginPrint += BeginPrint;

            // Set up page settings
            this.printDocument.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
            this.printDocument.DefaultPageSettings.Landscape = true;
        }

        public void Print()
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void BeginPrint(object sender, PrintEventArgs e)
        {
            this.currentPage = 1;
            this.columnCount = dgv.Columns.Count;

            // Use the actual printable area
            this.pageWidth = this.printDocument.DefaultPageSettings.PrintableArea.Width;
            this.pageHeight = this.printDocument.DefaultPageSettings.PrintableArea.Height;

            // Calculate rows per page (excluding header)
            this.rowsPerPage = (int)((pageHeight - topMargin - headerHeight) / cellHeight);

            // Calculate total pages
            this.totalPages = (int)Math.Ceiling((double)dgv.Rows.Count / rowsPerPage);
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = topMargin;
            int rowCount = 0;

            // Print page number
            e.Graphics.DrawString($"Page {currentPage} of {totalPages}",
                new Font("Arial", 8), Brushes.Black, pageWidth - 100, pageHeight - 25);

            // Print column headers
            for (int i = 0; i < columnCount; i++)
            {
                e.Graphics.FillRectangle(Brushes.LightGray, leftMargin + (i * cellWidth), yPos, cellWidth, headerHeight);
                e.Graphics.DrawRectangle(Pens.Black, leftMargin + (i * cellWidth), yPos, cellWidth, headerHeight);
                e.Graphics.DrawString(dgv.Columns[i].HeaderText, new Font("Arial", 10, FontStyle.Bold),
                    Brushes.Black, new RectangleF(leftMargin + (i * cellWidth), yPos, cellWidth, headerHeight));
            }
            yPos += headerHeight;

            // Print rows
            int startIndex = (currentPage - 1) * rowsPerPage;
            int endIndex = Math.Min(startIndex + rowsPerPage, dgv.Rows.Count);

            for (int rowIndex = startIndex; rowIndex < endIndex; rowIndex++)
            {
                DataGridViewRow row = dgv.Rows[rowIndex];
                for (int cellIndex = 0; cellIndex < columnCount; cellIndex++)
                {
                    e.Graphics.DrawRectangle(Pens.Black, leftMargin + (cellIndex * cellWidth), yPos, cellWidth, cellHeight);
                    if (row.Cells[cellIndex].Value != null)
                    {
                        e.Graphics.DrawString(row.Cells[cellIndex].Value.ToString(), dgv.Font, Brushes.Black,
                            new RectangleF(leftMargin + (cellIndex * cellWidth), yPos, cellWidth, cellHeight));
                    }
                }
                yPos += cellHeight;
                rowCount++;
            }

            // Determine if more pages need to be printed
            if (currentPage < totalPages)
            {
                e.HasMorePages = true;
                currentPage++;
            }
            else
            {
                e.HasMorePages = false;
                currentPage = 1;
            }
        }
    }
}
