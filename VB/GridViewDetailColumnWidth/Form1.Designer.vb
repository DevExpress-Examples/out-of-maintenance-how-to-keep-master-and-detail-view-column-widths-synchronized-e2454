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

Namespace GridViewDetailColumnWidth
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim gridLevelNode1 As New DevExpress.XtraGrid.GridLevelNode()
			Me.myGridView2 = New GridViewDetailColumnWidth.MyGridView()
			Me.colCarId = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colSubject = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colDescription = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colStartTime = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colEndTime = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colLocation = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.myGridControl1 = New GridViewDetailColumnWidth.MyGridControl()
			Me.carsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.carsDBDataSet_Renamed = New GridViewDetailColumnWidth.CarsDBDataSet()
			Me.myGridView1 = New GridViewDetailColumnWidth.MyGridView()
			Me.colID = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colTrademark = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colModel = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colHP = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colPrice = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.colCategory = New DevExpress.XtraGrid.Columns.GridColumn()
			Me.carsTableAdapter = New GridViewDetailColumnWidth.CarsDBDataSetTableAdapters.CarsTableAdapter()
			Me.carSchedulingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.carSchedulingTableAdapter = New GridViewDetailColumnWidth.CarsDBDataSetTableAdapters.CarSchedulingTableAdapter()
			DirectCast(Me.myGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.myGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.carsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.carsDBDataSet_Renamed, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.myGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.carSchedulingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' myGridView2
			' 
			Me.myGridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.colCarId, Me.colSubject, Me.colDescription, Me.colStartTime, Me.colEndTime, Me.colLocation})
			Me.myGridView2.GridControl = Me.myGridControl1
			Me.myGridView2.Name = "myGridView2"
			Me.myGridView2.OptionsView.AutoSynchronizeDetailsLayout = GridViewDetailColumnWidth.DetailsLayoutSynchronizationType.LayoutAndColumnOrder
			Me.myGridView2.OptionsView.ColumnAutoWidth = False
			Me.myGridView2.OptionsView.ShowGroupPanel = False
			' 
			' colCarId
			' 
			Me.colCarId.FieldName = "CarId"
			Me.colCarId.Name = "colCarId"
			Me.colCarId.Visible = True
			Me.colCarId.VisibleIndex = 0
			' 
			' colSubject
			' 
			Me.colSubject.FieldName = "Subject"
			Me.colSubject.Name = "colSubject"
			Me.colSubject.Visible = True
			Me.colSubject.VisibleIndex = 1
			' 
			' colDescription
			' 
			Me.colDescription.FieldName = "Description"
			Me.colDescription.Name = "colDescription"
			Me.colDescription.Visible = True
			Me.colDescription.VisibleIndex = 2
			' 
			' colStartTime
			' 
			Me.colStartTime.FieldName = "StartTime"
			Me.colStartTime.Name = "colStartTime"
			Me.colStartTime.Visible = True
			Me.colStartTime.VisibleIndex = 3
			' 
			' colEndTime
			' 
			Me.colEndTime.FieldName = "EndTime"
			Me.colEndTime.Name = "colEndTime"
			Me.colEndTime.Visible = True
			Me.colEndTime.VisibleIndex = 4
			' 
			' colLocation
			' 
			Me.colLocation.FieldName = "Location"
			Me.colLocation.Name = "colLocation"
			Me.colLocation.Visible = True
			Me.colLocation.VisibleIndex = 5
			' 
			' myGridControl1
			' 
			Me.myGridControl1.DataSource = Me.carsBindingSource
			Me.myGridControl1.Dock = System.Windows.Forms.DockStyle.Fill
			gridLevelNode1.LevelTemplate = Me.myGridView2
			gridLevelNode1.RelationName = "Cars_CarScheduling"
			Me.myGridControl1.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() { gridLevelNode1})
			Me.myGridControl1.Location = New System.Drawing.Point(0, 0)
			Me.myGridControl1.MainView = Me.myGridView1
			Me.myGridControl1.Name = "myGridControl1"
			Me.myGridControl1.Size = New System.Drawing.Size(599, 515)
			Me.myGridControl1.TabIndex = 0
			Me.myGridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.myGridView1, Me.myGridView2})
			' 
			' carsBindingSource
			' 
			Me.carsBindingSource.DataMember = "Cars"
			Me.carsBindingSource.DataSource = Me.carsDBDataSet_Renamed
			' 
			' carsDBDataSet
			' 
			Me.carsDBDataSet_Renamed.DataSetName = "CarsDBDataSet"
			Me.carsDBDataSet_Renamed.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
			' 
			' myGridView1
			' 
			Me.myGridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() { Me.colID, Me.colTrademark, Me.colModel, Me.colHP, Me.colPrice, Me.colCategory})
			Me.myGridView1.GridControl = Me.myGridControl1
			Me.myGridView1.Name = "myGridView1"
			Me.myGridView1.OptionsDetail.ShowDetailTabs = False
			Me.myGridView1.OptionsView.AutoSynchronizeDetailsLayout = GridViewDetailColumnWidth.DetailsLayoutSynchronizationType.LayoutAndColumnOrder
			Me.myGridView1.OptionsView.ColumnAutoWidth = False
			' 
			' colID
			' 
			Me.colID.FieldName = "ID"
			Me.colID.Name = "colID"
			Me.colID.Visible = True
			Me.colID.VisibleIndex = 0
			' 
			' colTrademark
			' 
			Me.colTrademark.FieldName = "Trademark"
			Me.colTrademark.Name = "colTrademark"
			Me.colTrademark.Visible = True
			Me.colTrademark.VisibleIndex = 1
			' 
			' colModel
			' 
			Me.colModel.FieldName = "Model"
			Me.colModel.Name = "colModel"
			Me.colModel.Visible = True
			Me.colModel.VisibleIndex = 2
			' 
			' colHP
			' 
			Me.colHP.FieldName = "HP"
			Me.colHP.Name = "colHP"
			Me.colHP.Visible = True
			Me.colHP.VisibleIndex = 3
			' 
			' colPrice
			' 
			Me.colPrice.FieldName = "Price"
			Me.colPrice.Name = "colPrice"
			Me.colPrice.Visible = True
			Me.colPrice.VisibleIndex = 4
			' 
			' colCategory
			' 
			Me.colCategory.FieldName = "Category"
			Me.colCategory.Name = "colCategory"
			Me.colCategory.Visible = True
			Me.colCategory.VisibleIndex = 5
			' 
			' carsTableAdapter
			' 
			Me.carsTableAdapter.ClearBeforeFill = True
			' 
			' carSchedulingBindingSource
			' 
			Me.carSchedulingBindingSource.DataMember = "CarScheduling"
			Me.carSchedulingBindingSource.DataSource = Me.carsDBDataSet_Renamed
			' 
			' carSchedulingTableAdapter
			' 
			Me.carSchedulingTableAdapter.ClearBeforeFill = True
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(599, 515)
			Me.Controls.Add(Me.myGridControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load)
			DirectCast(Me.myGridView2, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.myGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.carsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.carsDBDataSet_Renamed, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.myGridView1, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.carSchedulingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

'INSTANT VB NOTE: The variable carsDBDataSet was renamed since it may cause conflicts with calls to static members of the user-defined type with this name:
		Private carsDBDataSet_Renamed As CarsDBDataSet
		Private carsBindingSource As System.Windows.Forms.BindingSource
		Private carsTableAdapter As CarsDBDataSetTableAdapters.CarsTableAdapter
		Private carSchedulingBindingSource As System.Windows.Forms.BindingSource
		Private carSchedulingTableAdapter As CarsDBDataSetTableAdapters.CarSchedulingTableAdapter
		Private myGridControl1 As MyGridControl
		Private myGridView2 As MyGridView
		Private myGridView1 As MyGridView
		Private colID As DevExpress.XtraGrid.Columns.GridColumn
		Private colTrademark As DevExpress.XtraGrid.Columns.GridColumn
		Private colModel As DevExpress.XtraGrid.Columns.GridColumn
		Private colHP As DevExpress.XtraGrid.Columns.GridColumn
		Private colCarId As DevExpress.XtraGrid.Columns.GridColumn
		Private colSubject As DevExpress.XtraGrid.Columns.GridColumn
		Private colDescription As DevExpress.XtraGrid.Columns.GridColumn
		Private colStartTime As DevExpress.XtraGrid.Columns.GridColumn
		Private colEndTime As DevExpress.XtraGrid.Columns.GridColumn
		Private colLocation As DevExpress.XtraGrid.Columns.GridColumn
		Private colPrice As DevExpress.XtraGrid.Columns.GridColumn
		Private colCategory As DevExpress.XtraGrid.Columns.GridColumn
	End Class
End Namespace

