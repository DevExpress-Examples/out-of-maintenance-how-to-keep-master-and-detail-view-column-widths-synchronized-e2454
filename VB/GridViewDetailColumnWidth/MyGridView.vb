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
			Dim viewInfo As MyGridViewInfo = CType(view.GetViewInfo(), MyGridViewInfo)

			Dim borderWidth As Integer = viewInfo.ViewRects.Client.X - viewInfo.ViewRects.Bounds.X
			Me.VisibleColumns(0).MinWidth = view.VisibleColumns(0).MinWidth + viewInfo.LevelIndent + viewInfo.ViewRects.IndicatorWidth + borderWidth
		End Sub

		Protected Overrides Function CreateOptionsView() As ColumnViewOptionsView
			Return New MyGridOptionsView()
		End Function

		Protected Overridable Sub UpdateDetailViewsLayout()
			If (Not Me.IsDetailView) AndAlso (Not OptionsView.ColumnAutoWidth) AndAlso OptionsView.AutoSynchronizeDetailsLayout Then
				Try
					BeginUpdate()

					Dim rowVisibleIndex As Integer = TopRowIndex
					Dim rowHandle As Integer = GetVisibleRowHandle(rowVisibleIndex)
					Do While rowHandle <> GridControl.InvalidRowHandle
						For i As Integer = 0 To GetRelationCount(rowHandle) - 1
							Dim detailView As MyGridView = TryCast(GetDetailView(rowHandle, i), MyGridView)
							If detailView IsNot Nothing Then
								detailView.LayoutChanged()
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