using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using System;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace GridViewDetailColumnWidth
{
	public class MyGridView : GridView {
        public MyGridView(GridControl ownerGrid)
            : base(ownerGrid) {
        }
        public MyGridView()
            : base() {
            MasterRowExpanded += MyGridView_MasterRowExpanded;
        }

        internal void DoShowGridMenuInternal(GridViewMenu menu, GridHitInfo hitInfo) {
            DoShowGridMenu(menu, hitInfo);
        }


        protected override ColumnViewOptionsView CreateOptionsView() {
            return new MyGridOptionsView();
        }

        protected override void OnColumnWidthChanged(GridColumn column) {
            base.OnColumnWidthChanged(column);
            AutoSynchronizeDetailsColumnWidths();
        }

        public override void BestFitColumns() {
            base.BestFitColumns();
            AutoSynchronizeDetailsColumnWidths();
        }

        protected override void OnColumnVisibleIndexChanged(GridColumn column) {
            base.OnColumnVisibleIndexChanged(column);
            AutoSynchronizeDetailsColumnWidths();
        }

        protected override void OnColumnAdded(GridColumn column) {
            base.OnColumnAdded(column);
            AutoSynchronizeDetailsColumnWidths();
        }

        protected override void OnColumnDeleted(GridColumn column) {
            base.OnColumnDeleted(column);
            AutoSynchronizeDetailsColumnWidths();
        }

        protected override string ViewName {
            get { return "MyGridView"; }
        }

        private void MyGridView_MasterRowExpanded(object sender, CustomMasterRowEventArgs e) {
            if(!OptionsView.ColumnAutoWidth && OptionsView.AutoSynchronizeDetailsLayout != DetailsLayoutSynchronizationType.None) {
                MyGridView currentView = sender as MyGridView;
                var view = currentView.GetDetailView(e.RowHandle, e.RelationIndex) as MyGridView;
                if(view == null)
                    return;
                currentView.AutoSynchronizeDetailsColumnWidths();
            }
        }

        private void MatchColumnLayout(MyGridView otherView) {
            bool otherIsDetail = otherView.ParentView != null && otherView.ParentView.Equals(this);
            var viewInfo = otherIsDetail ? (MyGridViewInfo)otherView.GetViewInfo() : (MyGridViewInfo)this.GetViewInfo();
            int borderWidth = viewInfo.ViewRects.Client.X - viewInfo.ViewRects.Bounds.X;
            otherView.BeginUpdate();
            if(OptionsView.AutoSynchronizeDetailsLayout == DetailsLayoutSynchronizationType.LayoutAndColumnOrder) {
                var matches = new List<Tuple<GridColumn, GridColumn>>();
                foreach(GridColumn column in this.Columns) {
                    GridColumn other = otherView.Columns[column.FieldName];
                    if(other == null)
                        continue;
                    matches.Add(new Tuple<GridColumn, GridColumn>(column, other));
                }
                bool madeChanges;
                do {
                    madeChanges = false;
                    foreach(var item in matches) {
                        GridColumn column = item.Item1;
                        GridColumn other = item.Item2;
                        if(column.VisibleIndex != other.VisibleIndex && column.VisibleIndex < otherView.VisibleColumns.Count) {
                            other.VisibleIndex = -1;
                            other.VisibleIndex = column.VisibleIndex;
                            madeChanges = true;
                        }
                    }
                } while(madeChanges);
            }
            if(otherIsDetail) {
                otherView.VisibleColumns[0].Width = this.VisibleColumns[0].Width - viewInfo.LevelIndent - (viewInfo.LevelIndent * this.GroupCount) - viewInfo.ViewRects.IndicatorWidth - borderWidth + (this.VisibleColumns[0].Fixed == FixedStyle.Left ? this.FixedLineWidth : 0);
            } else
                if(this.VisibleColumns[0] != null)
                    otherView.VisibleColumns[0].Width = this.VisibleColumns[0].Width + viewInfo.LevelIndent + (viewInfo.LevelIndent * otherView.GroupCount) + viewInfo.ViewRects.IndicatorWidth + borderWidth - (otherView.VisibleColumns[0].Fixed == FixedStyle.Left ? otherView.FixedLineWidth : 0);
            for(int i = 1; i < otherView.VisibleColumns.Count; i++) {
                GridColumn column = this.VisibleColumns[i];
                if(column != null)
                    otherView.VisibleColumns[i].Width = column.Width;
            }
            otherView.EndUpdate();
        }

        private void UpdateDetailViewsColumnWidths(MyGridView excludeView) {
            int rowHandle = GetVisibleRowHandle(TopRowIndex);
            int rowIndex = TopRowIndex;
            while(rowHandle != GridControl.InvalidRowHandle) {
                for(int i = 0; i < GetRelationCount(rowHandle); i++) {
                    var detailView = GetDetailView(rowHandle, i) as MyGridView;
                    if(detailView != null && detailView != excludeView) {
                        MatchColumnLayout(detailView);
                        if(detailView is MyGridView) {
                            (detailView as MyGridView).AutoSynchronizeDetailsColumnWidths();
                        }
                    }
                }
                rowIndex = GetNextVisibleRow(rowIndex);
                rowHandle = GetRowHandle(rowIndex);
            }
        }

        private void UpdateParentViewColumnWidths() {
            var parentView = this.ParentView as MyGridView;
            if(parentView == null)
                return;
            MatchColumnLayout(parentView);
            if(this.ParentView is MyGridView)
                (this.ParentView as MyGridView).UpdateParentViewColumnWidths();
        }

        internal void AutoSynchronizeDetailsColumnWidths() {
            if(!OptionsView.ColumnAutoWidth && OptionsView.AutoSynchronizeDetailsLayout != DetailsLayoutSynchronizationType.None) {
                if(this.IsZoomedView)
                    return;
                if(!this.IsDetailView)
                    UpdateDetailViewsColumnWidths(null);
                else {
                    UpdateDetailViewsColumnWidths(null);
                    UpdateParentViewColumnWidths();
                }
            }
        }

        protected override void DoLeftCoordChanged(bool allowPartialScroll) { 
            base.DoLeftCoordChanged(allowPartialScroll);
            AutoSynchronizeDetailsLeftCoord();
        }

        private void UpdateDetailViewsLeftCoord() {
            int rowHandle = GetVisibleRowHandle(TopRowIndex);
            int rowIndex = TopRowIndex;
            while(rowHandle != GridControl.InvalidRowHandle) {
                for(int i = 0; i < GetRelationCount(rowHandle); i++) {
                    var detailView = GetDetailView(rowHandle, i) as MyGridView;
                    if(detailView != null) {
                        detailView.LeftCoord = this.LeftCoord;
                        if(detailView is MyGridView) {
                            (detailView as MyGridView).AutoSynchronizeDetailsLeftCoord();
                        }
                    }
                }
                rowIndex = GetNextVisibleRow(rowIndex);
                rowHandle = GetRowHandle(rowIndex);
            }
        }

        private void UpdateParentViewLeftCoord() {
            var parentView = ParentView as GridView;
            if(parentView == null)
                return;
            parentView.LeftCoord = LeftCoord;
            if(ParentView is MyGridView)
                (ParentView as MyGridView).UpdateParentViewLeftCoord();
        }

        private void AutoSynchronizeDetailsLeftCoord() {
            if(!OptionsView.ColumnAutoWidth && OptionsView.AutoSynchronizeDetailsLayout != DetailsLayoutSynchronizationType.None) {
                if(this.IsZoomedView)
                    return;
                if(!this.IsDetailView)
                    UpdateDetailViewsLeftCoord();
                else {
                    UpdateDetailViewsLeftCoord();
                    UpdateParentViewLeftCoord();
                }
            }
        }

        [Description("Provides access to the View's display options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue), XtraSerializablePropertyId(LayoutIdOptionsView)]
        public new MyGridOptionsView OptionsView {
            get {
                return base.OptionsView as MyGridOptionsView;
            }
        }

    }

}