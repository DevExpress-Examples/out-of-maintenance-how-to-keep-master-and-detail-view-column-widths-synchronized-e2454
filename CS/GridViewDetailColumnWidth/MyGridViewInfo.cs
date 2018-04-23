using System.Drawing;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace GridViewDetailColumnWidth
{
	public class MyGridViewInfo : GridViewInfo
	{
		public MyGridViewInfo(MyGridView gridView)
			: base(gridView)
		{
		}

		public new MyGridView View
		{
			get
			{
				return (MyGridView)base.View;
			}
		}

		protected override void CalcColumnInfo(GridColumnInfoArgs ci, ref int lastLeft)
		{
			base.CalcColumnInfo(ci, ref lastLeft);

			if ( ci.Type == GridColumnInfoType.Indicator || ci.Type == GridColumnInfoType.BehindColumn ||
				View.OptionsView.ColumnAutoWidth || !View.OptionsView.AutoSynchronizeDetailsLayout )
				return;

			GridView parentView = View.ParentView as GridView;
			if ( parentView == null || parentView.OptionsView.ColumnAutoWidth )
				return;

			GridColumnsInfo parentColumnsInfo = ((GridViewInfo)parentView.GetViewInfo()).ColumnsInfo;
			if ( parentColumnsInfo.ColumnsCount <= ci.Column.VisibleIndex )
				return;

			GridColumnInfoArgs parentColumn = parentColumnsInfo[parentView.VisibleColumns[ci.Column.VisibleIndex]];
			lastLeft -= ci.Bounds.Width;
			ci.Bounds = new Rectangle(ci.Bounds.Location, new Size(parentColumn.Bounds.Width - ci.Bounds.Left + parentColumn.Bounds.Left, ci.Bounds.Height));
			Painter.ElementsPainter.Column.CalcObjectBounds(ci);
			lastLeft += ci.Bounds.Width;
		}
	}
}
