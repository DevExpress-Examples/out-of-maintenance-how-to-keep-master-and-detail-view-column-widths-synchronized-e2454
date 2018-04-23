Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Namespace GridViewDetailColumnWidth
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Me.carsTableAdapter.Fill(Me.carsDBDataSet_Renamed.Cars)
			Me.carSchedulingTableAdapter.Fill(Me.carsDBDataSet_Renamed.CarScheduling)
		End Sub
	End Class
End Namespace
