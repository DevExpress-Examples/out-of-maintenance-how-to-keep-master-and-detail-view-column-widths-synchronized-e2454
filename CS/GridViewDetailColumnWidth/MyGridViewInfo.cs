// Developer Express Code Central Example:
// How to keep master and detail view column widths synchronized
// 
// When you have a master-detail view and for some reason don't want to use the
// AutoColumnWidth option, the detail view is still stretched to accommodate the
// entire master view's width. So, in this example we've decided to create a grid
// view that changes this behavior and synchronizes the detail view's width with
// the mater view's columns.
// We create and register a GridView descendant that
// introduces the AutoSynchronizeDetailsLayout option by using a descendant of the
// GridOptionsView class. If this option is enabled and the AutoColumnWidth option
// is disabled the new behavior takes effect. The detail view's width is now
// calculated based on the master view columns' width, so, the detail view ends up
// with the last master view's column. In addition, if the number of detail and
// master view columns is equal, the detail view's columns are aligned with that of
// the master view for even a smoother view.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2454

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
