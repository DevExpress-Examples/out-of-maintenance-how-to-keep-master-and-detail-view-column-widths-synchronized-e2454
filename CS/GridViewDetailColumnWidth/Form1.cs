using System;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace GridViewDetailColumnWidth
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.carsTableAdapter.Fill(this.carsDBDataSet.Cars);
			this.carSchedulingTableAdapter.Fill(this.carsDBDataSet.CarScheduling);
		}
	}
}
