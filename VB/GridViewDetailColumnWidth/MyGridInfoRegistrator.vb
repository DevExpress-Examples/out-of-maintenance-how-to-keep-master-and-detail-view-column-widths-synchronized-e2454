' Developer Express Code Central Example:
' How to keep master and detail view column widths synchronized
' 
' When you have a master-detail view and for some reason don't want to use the
' AutoColumnWidth option, the detail view is still stretched to accommodate the
' entire master view's width. So, in this example we've decided to create a grid
' view that changes this behavior and synchronizes the detail view's width with
' the mater view's columns.
' We create and register a GridView descendant that
' introduces the AutoSynchronizeDetailsLayout option by using a descendant of the
' GridOptionsView class. If this option is enabled and the AutoColumnWidth option
' is disabled the new behavior takes effect. The detail view's width is now
' calculated based on the master view columns' width, so, the detail view ends up
' with the last master view's column. In addition, if the number of detail and
' master view columns is equal, the detail view's columns are aligned with that of
' the master view for even a smoother view.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2454


Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Base.ViewInfo

Namespace GridViewDetailColumnWidth
	Public Class MyGridInfoRegistrator
		Inherits GridInfoRegistrator
		Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
			Return New MyGridView(TryCast(grid, GridControl))
		End Function
		Public Overrides Function CreateViewInfo(ByVal view As BaseView) As BaseViewInfo
			Return New MyGridViewInfo(CType(view, MyGridView))
		End Function

		Public Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property
	End Class
End Namespace
