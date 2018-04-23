Imports DevExpress.XtraGrid.Views.Grid
Imports System.Collections.Generic
Imports System
Imports DevExpress.XtraGrid.Menu
Imports DevExpress.XtraGrid.Views.Grid.Handler

Namespace GridViewDetailColumnWidth
	Friend Class MyGridViewHandler
		Inherits GridHandler

		Public Sub New(ByVal view As GridView)
			MyBase.New(view)
		End Sub

		Protected Overrides Sub DoCheckShowMenu()
			If View.OptionsMenu.EnableColumnMenu AndAlso (DownPointHitInfo.InColumnPanel OrElse DownPointHitInfo.InColumn OrElse DownPointHitInfo.InGroupColumn) Then
				Dim menu As GridViewMenu = New MyGridViewColumnMenu(View)
				menu.Init(DownPointHitInfo.Column)
				CType(View, MyGridView).DoShowGridMenuInternal(menu, DownPointHitInfo)
			Else
				MyBase.DoCheckShowMenu()
			End If
		End Sub
	End Class
End Namespace
