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
Imports System.Drawing
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace GridViewDetailColumnWidth
	Public Class MyGridViewInfo
		Inherits GridViewInfo
		Public Sub New(ByVal gridView As MyGridView)
			MyBase.New(gridView)
		End Sub

		Public Shadows ReadOnly Property View() As MyGridView
			Get
				Return CType(MyBase.View, MyGridView)
			End Get
		End Property
		Protected Overrides Sub CalcColumnInfo(ByVal ci As GridColumnInfoArgs, ByRef lastLeft As Integer, ByVal lastColumn As Boolean)
			MyBase.CalcColumnInfo(ci, lastLeft, lastColumn)

			If ci.Type = GridColumnInfoType.Indicator OrElse ci.Type = GridColumnInfoType.BehindColumn OrElse View.OptionsView.ColumnAutoWidth OrElse (Not View.OptionsView.AutoSynchronizeDetailsLayout) Then
				Return
			End If

			Dim parentView As GridView = TryCast(View.ParentView, GridView)
			If parentView Is Nothing OrElse parentView.OptionsView.ColumnAutoWidth Then
				Return
			End If

			Dim parentColumnsInfo As GridColumnsInfo = (CType(parentView.GetViewInfo(), GridViewInfo)).ColumnsInfo
			If parentColumnsInfo.ColumnsCount <= ci.Column.VisibleIndex Then
				Return
			End If

			Dim parentColumn As GridColumnInfoArgs = parentColumnsInfo(parentView.VisibleColumns(ci.Column.VisibleIndex))
			lastLeft -= ci.Bounds.Width
			ci.Bounds = New Rectangle(ci.Bounds.Location, New Size(parentColumn.Bounds.Width - ci.Bounds.Left + parentColumn.Bounds.Left, ci.Bounds.Height))
			Painter.ElementsPainter.Column.CalcObjectBounds(ci)
			lastLeft += ci.Bounds.Width
		End Sub

	End Class
End Namespace
