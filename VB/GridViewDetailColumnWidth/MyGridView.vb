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
Imports System.ComponentModel
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Namespace GridViewDetailColumnWidth
	Public Class MyGridView
		Inherits GridView
		Public Sub New(ByVal ownerGrid As GridControl)
			MyBase.New(ownerGrid)
		End Sub
		Public Sub New()
			MyBase.New()
			AddHandler MasterRowExpanded, AddressOf OnMasterRowExpanded
		End Sub

		Private Sub OnMasterRowExpanded(ByVal sender As Object, ByVal e As CustomMasterRowEventArgs)
			Dim view As MyGridView = CType(Me.GetDetailView(e.RowHandle, e.RelationIndex), MyGridView)
			If view Is Nothing Then
				Return
			End If
			Dim viewInfo As MyGridViewInfo = CType(view.GetViewInfo(), MyGridViewInfo)

			Dim borderWidth As Integer = viewInfo.ViewRects.Client.X - viewInfo.ViewRects.Bounds.X
			Me.VisibleColumns(0).MinWidth = view.VisibleColumns(0).MinWidth + viewInfo.LevelIndent + viewInfo.ViewRects.IndicatorWidth + borderWidth
		End Sub

		Protected Overrides Function CreateOptionsView() As ColumnViewOptionsView
			Return New MyGridOptionsView()
		End Function

		Protected Overridable Sub UpdateDetailViewsLayout()
			If (Not OptionsView.ColumnAutoWidth) AndAlso OptionsView.AutoSynchronizeDetailsLayout Then
				Try
					BeginUpdate()

					Dim rowVisibleIndex As Integer = TopRowIndex
					Dim rowHandle As Integer = GetVisibleRowHandle(rowVisibleIndex)
					Do While rowHandle <> GridControl.InvalidRowHandle
						For i As Integer = 0 To GetRelationCount(rowHandle) - 1
							Dim detailView As MyGridView = TryCast(GetDetailView(rowHandle, i), MyGridView)
							If detailView IsNot Nothing Then
								detailView.LayoutChanged()
								detailView.UpdateDetailViewsLayout()
							End If
						Next i

						rowHandle = GetNextVisibleRow(rowVisibleIndex)
						rowVisibleIndex = GetVisibleIndex(rowHandle)
					Loop
				Finally
					EndUpdate()
				End Try
			End If
		End Sub

		Protected Overrides Sub OnColumnWidthChanged(ByVal column As GridColumn)
			UpdateDetailViewsLayout()
			MyBase.OnColumnWidthChanged(column)
		End Sub

		Public Overrides Sub BestFitColumns()
			MyBase.BestFitColumns()
			UpdateDetailViewsLayout()
		End Sub

		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property

		<Description("Provides access to the View's display options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue), XtraSerializablePropertyId(LayoutIdOptionsView)> _
		Public Shadows ReadOnly Property OptionsView() As MyGridOptionsView
			Get
				Return TryCast(MyBase.OptionsView, MyGridOptionsView)
			End Get
		End Property

	End Class

End Namespace