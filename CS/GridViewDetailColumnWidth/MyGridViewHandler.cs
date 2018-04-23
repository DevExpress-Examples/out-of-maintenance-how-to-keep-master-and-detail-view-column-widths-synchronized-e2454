using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using System;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid.Handler;

namespace GridViewDetailColumnWidth {
    class MyGridViewHandler : GridHandler {
        public MyGridViewHandler(GridView view) : base(view) { }

        protected override void DoCheckShowMenu() {
            if(View.OptionsMenu.EnableColumnMenu && (DownPointHitInfo.InColumnPanel || DownPointHitInfo.InColumn || DownPointHitInfo.InGroupColumn)) {
                GridViewMenu menu = new MyGridViewColumnMenu(View);
                menu.Init(DownPointHitInfo.Column);
                ((MyGridView)View).DoShowGridMenuInternal(menu, DownPointHitInfo);
            } else base.DoCheckShowMenu();
        }
    }
}
