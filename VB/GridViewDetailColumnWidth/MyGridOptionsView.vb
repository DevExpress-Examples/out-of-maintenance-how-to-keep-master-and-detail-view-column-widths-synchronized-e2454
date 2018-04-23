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
