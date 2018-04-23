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


	End Class
End Namespace
