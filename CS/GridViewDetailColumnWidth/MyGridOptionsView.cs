using System.ComponentModel;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Views.Grid;

namespace GridViewDetailColumnWidth
{
	public class MyGridOptionsView : GridOptionsView
	{
		bool autoSynchronizeDetailsLayout;
		
		public MyGridOptionsView()
			: base()
		{
			autoSynchronizeDetailsLayout = false;
		}

		[DefaultValue(false), XtraSerializableProperty()]
		public virtual bool AutoSynchronizeDetailsLayout
		{
			get
			{
				return autoSynchronizeDetailsLayout;
			}
			set
			{
				if ( AutoSynchronizeDetailsLayout == value )
					return;

				bool prevValue = AutoSynchronizeDetailsLayout;
				autoSynchronizeDetailsLayout = value;
				OnChanged(new BaseOptionChangedEventArgs("AutoSynchronizeDetailsLayout", prevValue, AutoSynchronizeDetailsLayout));
			}
		}

		public override void Assign(BaseOptions options)
		{
			base.Assign(options);
			MyGridOptionsView opt = options as MyGridOptionsView;
			if ( opt == null )
				return;

			this.autoSynchronizeDetailsLayout = opt.AutoSynchronizeDetailsLayout;
		}

	}
}
