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
			if ( !this.IsDetailView && !OptionsView.ColumnAutoWidth && OptionsView.AutoSynchronizeDetailsLayout )
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