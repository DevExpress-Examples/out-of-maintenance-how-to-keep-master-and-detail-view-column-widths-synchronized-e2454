using System.ComponentModel;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Collections.Generic;
using System;

namespace GridViewDetailColumnWidth {
    public class MyGridViewColumnMenu : GridViewColumnMenu {
        public MyGridViewColumnMenu(GridView view) : base(view) { }

        protected override void OnMenuItemClick(object sender, EventArgs e) {
            var view = View as MyGridView;
            var item = (DXMenuItem)sender;
            if(!(item.Tag is GridStringId) || view == null)
                return;
            var id = (GridStringId)item.Tag;
            base.OnMenuItemClick(sender, e);
            if(id == GridStringId.MenuColumnRemoveColumn)
                view.AutoSynchronizeDetailsColumnWidths();
        }
    }
}
