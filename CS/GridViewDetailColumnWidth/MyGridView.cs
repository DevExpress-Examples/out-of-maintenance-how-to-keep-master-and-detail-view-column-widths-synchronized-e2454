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

using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace GridViewDetailColumnWidth
{
	public class MyGridView : GridView
	{
		public MyGridView(GridControl ownerGrid)
			: base(ownerGrid)
		{
		}
		public MyGridView()
			: base()
		{
			MasterRowExpanded += new CustomMasterRowEventHandler(OnMasterRowExpanded);
		}

		private void OnMasterRowExpanded(object sender, CustomMasterRowEventArgs e)
		{
			MyGridView view = (MyGridView)this.GetDetailView(e.RowHandle, e.RelationIndex);
            if (view == null) return;
			MyGridViewInfo viewInfo = (MyGridViewInfo)view.GetViewInfo();

			int borderWidth = viewInfo.ViewRects.Client.X - viewInfo.ViewRects.Bounds.X;
			this.VisibleColumns[0].MinWidth = view.VisibleColumns[0].MinWidth + viewInfo.LevelIndent + viewInfo.ViewRects.IndicatorWidth + borderWidth;
		}

		protected override ColumnViewOptionsView CreateOptionsView()
		{
			return new MyGridOptionsView();
		}

		protected virtual void UpdateDetailViewsLayout()
		{
			if ( !OptionsView.ColumnAutoWidth && OptionsView.AutoSynchronizeDetailsLayout )
				try
				{
					BeginUpdate();

					int rowVisibleIndex = TopRowIndex;
					int rowHandle = GetVisibleRowHandle(rowVisibleIndex);
					while ( rowHandle != GridControl.InvalidRowHandle )
					{
						for ( int i = 0; i < GetRelationCount(rowHandle); i++ )
						{
							MyGridView detailView = GetDetailView(rowHandle, i) as MyGridView;
							if ( detailView != null )
							{
                                detailView.LayoutChanged();
                                detailView.UpdateDetailViewsLayout();
                            }
						}

						rowHandle = GetNextVisibleRow(rowVisibleIndex);
						rowVisibleIndex = GetVisibleIndex(rowHandle);
					}
				}
				finally
				{
					EndUpdate();
				}
		}

		protected override void OnColumnWidthChanged(GridColumn column)
		{
			UpdateDetailViewsLayout();
			base.OnColumnWidthChanged(column);
		}

		public override void BestFitColumns()
		{
			base.BestFitColumns();
			UpdateDetailViewsLayout();
		}

		protected override string ViewName
		{
			get
			{
				return "MyGridView";
			}
		}

		[Description("Provides access to the View's display options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue), XtraSerializablePropertyId(LayoutIdOptionsView)]
		public new MyGridOptionsView OptionsView
		{
			get
			{
				return base.OptionsView as MyGridOptionsView;
			}
		}

	}

}