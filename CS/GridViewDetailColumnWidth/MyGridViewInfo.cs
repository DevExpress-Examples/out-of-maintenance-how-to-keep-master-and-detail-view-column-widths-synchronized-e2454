using System.Drawing;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace GridViewDetailColumnWidth
{
	public class MyGridViewInfo : GridViewInfo
	{
        public MyGridViewInfo(MyGridView gridView)
            : base(gridView) {
        }

        public new MyGridView View {
            get {
                return (MyGridView)base.View;
            }
        }
       
     
	}
}
