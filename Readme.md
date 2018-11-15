<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/GridViewDetailColumnWidth/Form1.cs) (VB: [Form1.vb](./VB/GridViewDetailColumnWidth/Form1.vb))
* [MyGridControl.cs](./CS/GridViewDetailColumnWidth/MyGridControl.cs) (VB: [MyGridControl.vb](./VB/GridViewDetailColumnWidth/MyGridControl.vb))
* [MyGridInfoRegistrator.cs](./CS/GridViewDetailColumnWidth/MyGridInfoRegistrator.cs) (VB: [MyGridInfoRegistrator.vb](./VB/GridViewDetailColumnWidth/MyGridInfoRegistrator.vb))
* [MyGridOptionsView.cs](./CS/GridViewDetailColumnWidth/MyGridOptionsView.cs) (VB: [MyGridOptionsView.vb](./VB/GridViewDetailColumnWidth/MyGridOptionsView.vb))
* [MyGridView.cs](./CS/GridViewDetailColumnWidth/MyGridView.cs) (VB: [MyGridViewHandler.vb](./VB/GridViewDetailColumnWidth/MyGridViewHandler.vb))
* [MyGridViewColumnMenu.cs](./CS/GridViewDetailColumnWidth/MyGridViewColumnMenu.cs) (VB: [MyGridViewColumnMenu1.vb](./VB/GridViewDetailColumnWidth/MyGridViewColumnMenu1.vb))
* [MyGridViewColumnMenu1.cs](./CS/GridViewDetailColumnWidth/MyGridViewColumnMenu1.cs) (VB: [MyGridViewColumnMenu1.vb](./VB/GridViewDetailColumnWidth/MyGridViewColumnMenu1.vb))
* [MyGridViewHandler.cs](./CS/GridViewDetailColumnWidth/MyGridViewHandler.cs) (VB: [MyGridViewHandler.vb](./VB/GridViewDetailColumnWidth/MyGridViewHandler.vb))
* [MyGridViewInfo.cs](./CS/GridViewDetailColumnWidth/MyGridViewInfo.cs) (VB: [MyGridViewInfo.vb](./VB/GridViewDetailColumnWidth/MyGridViewInfo.vb))
* [Program.cs](./CS/GridViewDetailColumnWidth/Program.cs) (VB: [Program.vb](./VB/GridViewDetailColumnWidth/Program.vb))
<!-- default file list end -->
# How to keep master and detail view column widths synchronized


<p>When you have a master-detail view and for some reason don't want to use the AutoColumnWidth option, the detail view is still stretched to accommodate the entire master view's width. So, in this example we've decided to create a grid view that changes this behavior and synchronizes the detail view's width with the mater view's columns.<br />
We create and register a GridView descendant that introduces the AutoSynchronizeDetailsLayout option by using a descendant of the GridOptionsView class. If this option is enabled and the AutoColumnWidth option is disabled the new behavior takes effect. The detail view's width is now calculated based on the master view columns' width, so, the detail view ends up with the last master view's column. <br />
In addition, if the number of detail and master view columns is equal, the detail view's columns are aligned with that of the master view for even a smoother view.</p>

<br/>


