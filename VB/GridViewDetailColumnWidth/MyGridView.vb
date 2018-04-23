Imports System.ComponentModel
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Collections.Generic
Imports System
Imports DevExpress.XtraGrid.Menu
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace GridViewDetailColumnWidth
	Public Class MyGridView
		Inherits GridView

		Public Sub New(ByVal ownerGrid As GridControl)
			MyBase.New(ownerGrid)
		End Sub
		Public Sub New()
			MyBase.New()
			AddHandler MasterRowExpanded, AddressOf MyGridView_MasterRowExpanded
		End Sub

		Friend Sub DoShowGridMenuInternal(ByVal menu As GridViewMenu, ByVal hitInfo As GridHitInfo)
			DoShowGridMenu(menu, hitInfo)
		End Sub


		Protected Overrides Function CreateOptionsView() As ColumnViewOptionsView
			Return New MyGridOptionsView()
		End Function

		Protected Overrides Sub OnColumnWidthChanged(ByVal column As GridColumn)
			MyBase.OnColumnWidthChanged(column)
			AutoSynchronizeDetailsColumnWidths()
		End Sub

		Public Overrides Sub BestFitColumns()
			MyBase.BestFitColumns()
			AutoSynchronizeDetailsColumnWidths()
		End Sub

		Protected Overrides Sub OnColumnVisibleIndexChanged(ByVal column As GridColumn)
			MyBase.OnColumnVisibleIndexChanged(column)
			AutoSynchronizeDetailsColumnWidths()
		End Sub

		Protected Overrides Sub OnColumnAdded(ByVal column As GridColumn)
			MyBase.OnColumnAdded(column)
			AutoSynchronizeDetailsColumnWidths()
		End Sub

		Protected Overrides Sub OnColumnDeleted(ByVal column As GridColumn)
			MyBase.OnColumnDeleted(column)
			AutoSynchronizeDetailsColumnWidths()
		End Sub

		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property

		Private Sub MyGridView_MasterRowExpanded(ByVal sender As Object, ByVal e As CustomMasterRowEventArgs)
			If (Not OptionsView.ColumnAutoWidth) AndAlso OptionsView.AutoSynchronizeDetailsLayout <> DetailsLayoutSynchronizationType.None Then
				Dim currentView As MyGridView = TryCast(sender, MyGridView)
				Dim view = TryCast(currentView.GetDetailView(e.RowHandle, e.RelationIndex), MyGridView)
				If view Is Nothing Then
					Return
				End If
				currentView.AutoSynchronizeDetailsColumnWidths()
			End If
		End Sub

		Private Sub MatchColumnLayout(ByVal otherView As MyGridView)
			Dim otherIsDetail As Boolean = otherView.ParentView IsNot Nothing AndAlso otherView.ParentView.Equals(Me)
            Dim viewInfo_Renamed = If(otherIsDetail, CType(otherView.GetViewInfo(), MyGridViewInfo), CType(Me.GetViewInfo(), MyGridViewInfo))
			Dim borderWidth As Integer = viewInfo_Renamed.ViewRects.Client.X - viewInfo_Renamed.ViewRects.Bounds.X
			otherView.BeginUpdate()
			If OptionsView.AutoSynchronizeDetailsLayout = DetailsLayoutSynchronizationType.LayoutAndColumnOrder Then
				Dim matches = New List(Of Tuple(Of GridColumn, GridColumn))()
				For Each column As GridColumn In Me.Columns
					Dim other As GridColumn = otherView.Columns(column.FieldName)
					If other Is Nothing Then
						Continue For
					End If
					matches.Add(New Tuple(Of GridColumn, GridColumn)(column, other))
				Next column
				Dim madeChanges As Boolean
				Do
					madeChanges = False
					For Each item In matches
						Dim column As GridColumn = item.Item1
						Dim other As GridColumn = item.Item2
						If column.VisibleIndex <> other.VisibleIndex AndAlso column.VisibleIndex < otherView.VisibleColumns.Count Then
							other.VisibleIndex = -1
							other.VisibleIndex = column.VisibleIndex
							madeChanges = True
						End If
					Next item
				Loop While madeChanges
			End If
			If otherIsDetail Then
				otherView.VisibleColumns(0).Width = Me.VisibleColumns(0).Width - viewInfo_Renamed.LevelIndent - (viewInfo_Renamed.LevelIndent * Me.GroupCount) - viewInfo_Renamed.ViewRects.IndicatorWidth - borderWidth + (If(Me.VisibleColumns(0).Fixed = FixedStyle.Left, Me.FixedLineWidth, 0))
			Else
				If Me.VisibleColumns(0) IsNot Nothing Then
					otherView.VisibleColumns(0).Width = Me.VisibleColumns(0).Width + viewInfo_Renamed.LevelIndent + (viewInfo_Renamed.LevelIndent * otherView.GroupCount) + viewInfo_Renamed.ViewRects.IndicatorWidth + borderWidth - (If(otherView.VisibleColumns(0).Fixed = FixedStyle.Left, otherView.FixedLineWidth, 0))
				End If
			End If
			For i As Integer = 1 To otherView.VisibleColumns.Count - 1
				Dim column As GridColumn = Me.VisibleColumns(i)
				If column IsNot Nothing Then
					otherView.VisibleColumns(i).Width = column.Width
				End If
			Next i
			otherView.EndUpdate()
		End Sub

		Private Sub UpdateDetailViewsColumnWidths(ByVal excludeView As MyGridView)
			Dim rowHandle As Integer = GetVisibleRowHandle(TopRowIndex)
			Do While rowHandle <> GridControl.InvalidRowHandle
				For i As Integer = 0 To GetRelationCount(rowHandle) - 1
					Dim detailView = TryCast(GetDetailView(rowHandle, i), MyGridView)
					If detailView IsNot Nothing AndAlso detailView IsNot excludeView Then
						MatchColumnLayout(detailView)
						If TypeOf detailView Is MyGridView Then
							TryCast(detailView, MyGridView).AutoSynchronizeDetailsColumnWidths()
						End If
					End If
				Next i
                rowHandle = GetNextVisibleRow(rowHandle)
            Loop
		End Sub

		Private Sub UpdateParentViewColumnWidths()
            Dim parentView_Renamed = TryCast(Me.ParentView, MyGridView)
			If parentView_Renamed Is Nothing Then
				Return
			End If
			MatchColumnLayout(parentView_Renamed)
			If TypeOf Me.ParentView Is MyGridView Then
				TryCast(Me.ParentView, MyGridView).UpdateParentViewColumnWidths()
			End If
		End Sub

		Friend Sub AutoSynchronizeDetailsColumnWidths()
			If (Not OptionsView.ColumnAutoWidth) AndAlso OptionsView.AutoSynchronizeDetailsLayout <> DetailsLayoutSynchronizationType.None Then
				If Me.IsZoomedView Then
					Return
				End If
				If Not Me.IsDetailView Then
					UpdateDetailViewsColumnWidths(Nothing)
				Else
					UpdateDetailViewsColumnWidths(Nothing)
					UpdateParentViewColumnWidths()
				End If
			End If
		End Sub

		Protected Overrides Sub DoLeftCoordChanged()
			MyBase.DoLeftCoordChanged()
			AutoSynchronizeDetailsLeftCoord()
		End Sub

		Private Sub UpdateDetailViewsLeftCoord()
			Dim rowHandle As Integer = GetVisibleRowHandle(TopRowIndex)
			Do While rowHandle <> GridControl.InvalidRowHandle
				For i As Integer = 0 To GetRelationCount(rowHandle) - 1
					Dim detailView = TryCast(GetDetailView(rowHandle, i), MyGridView)
					If detailView IsNot Nothing Then
						detailView.LeftCoord = Me.LeftCoord
						If TypeOf detailView Is MyGridView Then
							TryCast(detailView, MyGridView).AutoSynchronizeDetailsLeftCoord()
						End If
					End If
				Next i
				rowHandle = GetNextVisibleRow(rowHandle)
			Loop
		End Sub

		Private Sub UpdateParentViewLeftCoord()
            Dim parentView_Renamed = TryCast(ParentView, GridView)
			If parentView_Renamed Is Nothing Then
				Return
			End If
			parentView_Renamed.LeftCoord = LeftCoord
			If TypeOf ParentView Is MyGridView Then
				TryCast(ParentView, MyGridView).UpdateParentViewLeftCoord()
			End If
		End Sub

		Private Sub AutoSynchronizeDetailsLeftCoord()
			If (Not OptionsView.ColumnAutoWidth) AndAlso OptionsView.AutoSynchronizeDetailsLayout <> DetailsLayoutSynchronizationType.None Then
				If Me.IsZoomedView Then
					Return
				End If
				If Not Me.IsDetailView Then
					UpdateDetailViewsLeftCoord()
				Else
					UpdateDetailViewsLeftCoord()
					UpdateParentViewLeftCoord()
				End If
			End If
		End Sub

		<Description("Provides access to the View's display options."), Category("Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue), XtraSerializablePropertyId(LayoutIdOptionsView)>
		Public Shadows ReadOnly Property OptionsView() As MyGridOptionsView
			Get
				Return TryCast(MyBase.OptionsView, MyGridOptionsView)
			End Get
		End Property

	End Class

End Namespace