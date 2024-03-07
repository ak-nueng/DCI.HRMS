//using Infragistics.Win;
//using Infragistics.Win.UltraWinGrid;

namespace DCI.HRMS.Common
{
	/// <summary>
	/// Summary description for UltraGridSetting.
	/// </summary>
	public sealed class UltraGridSetting
	{
		public static void DeleteAllItem(UltraGrid grid)
		{
			foreach (UltraGridRow rw in grid.Rows)
			{
				rw.Selected = true;
			}
			UltraGridSetting.DeleteAllItem(grid);
		}

		public static void DeleteItem(UltraGrid grid)
		{
			grid.DeleteSelectedRows(false);
		}

		public static void Column(UltraGrid grid, int band, string columnName, int visiblePosition, int width, HAlign hAlign, VAlign vAlign)
		{
			UltraGridSetting.Column(grid, band, columnName, visiblePosition, width, hAlign, vAlign, columnName);
		}

		public static void Column(UltraGrid grid, int band, string columnName, int visiblePosition, int width, HAlign hAlign, VAlign vAlign, string headerName)
		{
			UltraGridColumn col = grid.DisplayLayout.Bands[band].Columns[columnName];

			col.Header.Caption = headerName;
			col.Header.VisiblePosition = visiblePosition;
			col.Width = width;

			col.CellAppearance.TextHAlign = hAlign;
			col.CellAppearance.TextVAlign = vAlign;
		}

		public static void ColumnHidden(UltraGrid grid, int band, string[] columns, bool isHidden)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				grid.DisplayLayout.Bands[band].Columns[columns[i]].Hidden = isHidden;
			}
		}
	}
}