Imports DevExpress.XtraGrid.Views.Grid
Imports System.Collections.Generic
Imports System
Imports DevExpress.XtraGrid.Menu
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Localization

Namespace GridViewDetailColumnWidth
	Public Class MyGridViewColumnMenu
		Inherits GridViewColumnMenu

		Public Sub New(ByVal view As GridView)
			MyBase.New(view)
		End Sub

		Protected Overrides Sub OnMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
'INSTANT VB NOTE: The variable view was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim view_Renamed = TryCast(View, MyGridView)
			Dim item = DirectCast(sender, DXMenuItem)
			If Not(TypeOf item.Tag Is GridStringId) OrElse view_Renamed Is Nothing Then
				Return
			End If
			Dim id = DirectCast(item.Tag, GridStringId)
			MyBase.OnMenuItemClick(sender, e)
			If id = GridStringId.MenuColumnRemoveColumn Then
				view_Renamed.AutoSynchronizeDetailsColumnWidths()
			End If
		End Sub
	End Class
End Namespace