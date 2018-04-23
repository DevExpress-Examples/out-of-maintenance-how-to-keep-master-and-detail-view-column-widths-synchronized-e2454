Imports System.ComponentModel
Imports DevExpress.Utils.Controls
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraGrid.Views.Grid
Imports System

Namespace GridViewDetailColumnWidth

	<Serializable>
	Public Enum DetailsLayoutSynchronizationType
		None
		Layout
		LayoutAndColumnOrder
	End Enum
	Public Class MyGridOptionsView
		Inherits GridOptionsView

'INSTANT VB NOTE: The variable autoSynchronizeDetailsLayout was renamed since Visual Basic does not allow variables and other class members to have the same name:
		Private autoSynchronizeDetailsLayout_Renamed As DetailsLayoutSynchronizationType

		Public Sub New()
			autoSynchronizeDetailsLayout_Renamed = DetailsLayoutSynchronizationType.None
		End Sub

		<DefaultValue(DetailsLayoutSynchronizationType.None), XtraSerializableProperty>
		Public Overridable Property AutoSynchronizeDetailsLayout() As DetailsLayoutSynchronizationType
			Get
				Return autoSynchronizeDetailsLayout_Renamed
			End Get
			Set(ByVal value As DetailsLayoutSynchronizationType)
				If AutoSynchronizeDetailsLayout = value Then
					Return
				End If
				Dim prevValue = AutoSynchronizeDetailsLayout
				autoSynchronizeDetailsLayout_Renamed = value
				OnChanged(New BaseOptionChangedEventArgs("AutoSynchronizeDetailsLayout", prevValue, AutoSynchronizeDetailsLayout))
			End Set
		End Property


		Public Overrides Sub Assign(ByVal options As BaseOptions)
			MyBase.Assign(options)
			Dim opt As MyGridOptionsView = TryCast(options, MyGridOptionsView)
			If opt Is Nothing Then
				Return
			End If

			Me.autoSynchronizeDetailsLayout_Renamed = opt.AutoSynchronizeDetailsLayout
		End Sub

	End Class
End Namespace
