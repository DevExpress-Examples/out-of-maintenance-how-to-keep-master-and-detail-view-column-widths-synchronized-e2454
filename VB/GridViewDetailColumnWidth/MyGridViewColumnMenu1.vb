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
            Dim aView = TryCast(View, MyGridView)
            Dim item = DirectCast(sender, DXMenuItem)
			If Not(TypeOf item.Tag Is GridStringId) OrElse view Is Nothing Then
				Return
			End If
			Dim id = CType(item.Tag, GridStringId)
			MyBase.OnMenuItemClick(sender, e)
            If id = GridStringId.MenuColumnRemoveColumn Then
                aView.AutoSynchronizeDetailsColumnWidths()
            End If
        End Sub
	End Class
End Namespace