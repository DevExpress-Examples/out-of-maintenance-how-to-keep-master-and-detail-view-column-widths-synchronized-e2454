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

		Protected Overrides Sub CalcColumnInfo(ByVal ci As GridColumnInfoArgs, ByRef lastLeft As Integer)
			MyBase.CalcColumnInfo(ci, lastLeft)

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
