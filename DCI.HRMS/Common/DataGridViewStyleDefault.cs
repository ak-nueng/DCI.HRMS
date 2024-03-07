using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DCI.HRMS.Common
{
    public static class DataGridViewStyleDefault
    {
        public static void SetDefault(DataGridView gridView)
        {
            gridView.RowTemplate.DefaultCellStyle = GetRowStyle();
            
            foreach (DataGridViewColumn column in gridView.Columns)
            {
                column.HeaderCell.Style = GetColumnHeaderStyle();
            }

            gridView.ColumnHeadersHeight = 50;
            gridView.BackgroundColor = System.Drawing.SystemColors.Window;
            gridView.RowHeadersWidth = 40;
            gridView.EnableHeadersVisualStyles = true;
        }
        public static DataGridViewCellStyle GetColumnHeaderStyle()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.ForeColor = Color.Black;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = Color.White;
            style.WrapMode = DataGridViewTriState.True;
            
            return style;
        }
        public static DataGridViewCellStyle GetRowStyle()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();

           style.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            style.SelectionBackColor = Color.LightBlue;
            style.SelectionForeColor = Color.Black;
            style.WrapMode = DataGridViewTriState.True;

            return style;
        }

        public static void ShowRowNumber(DataGridView gridView , DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush sBrush = new SolidBrush(Color.Black))
            {
                int row = e.RowIndex + 1;
                string strRow = row.ToString();

                try
                {
                    if (gridView.Rows[e.RowIndex].IsNewRow)
                        strRow = string.Empty;
                }
                catch { }

                e.Graphics.DrawString(strRow,
                                    gridView.DefaultCellStyle.Font,
                                    sBrush,
                                    e.RowBounds.Location.X + 9,
                                    e.RowBounds.Location.Y + 4);
            }
        }
    }
}
