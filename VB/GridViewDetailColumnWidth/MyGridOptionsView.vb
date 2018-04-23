Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports DevExpress.Utils.Controls
Imports DevExpress.Utils.Serializing
Imports DevExpress.XtraGrid.Views.Grid

Namespace GridViewDetailColumnWidth
	Public Class MyGridOptionsView
		Inherits GridOptionsView
		Private autoSynchronizeDetailsLayout_Renamed As Boolean

		Public Sub New()
			MyBase.New()
			autoSynchronizeDetailsLayout_Renamed = False
		End Sub

		<DefaultValue(False), XtraSerializableProperty()> _
		Public Overridable Property AutoSynchronizeDetailsLayout() As Boolean
			Get
				Return autoSynchronizeDetailsLayout_Renamed
			End Get
			Set(ByVal value As Boolean)
				If AutoSynchronizeDetailsLayout = value Then
					Return
				End If

				Dim prevValue As Boolean = AutoSynchronizeDetailsLayout
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
