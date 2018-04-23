using System.ComponentModel;
using DevExpress.Utils.Controls;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Views.Grid;
using System;

namespace GridViewDetailColumnWidth {

    [Serializable]
    public enum DetailsLayoutSynchronizationType {
        None,
        Layout,
        LayoutAndColumnOrder
    }
    public class MyGridOptionsView : GridOptionsView {
        DetailsLayoutSynchronizationType autoSynchronizeDetailsLayout;

        public MyGridOptionsView() {
            autoSynchronizeDetailsLayout = DetailsLayoutSynchronizationType.None;
        }

        [DefaultValue(DetailsLayoutSynchronizationType.None), XtraSerializableProperty]
        public virtual DetailsLayoutSynchronizationType AutoSynchronizeDetailsLayout {
            get {
                return autoSynchronizeDetailsLayout;
            }
            set {
                if(AutoSynchronizeDetailsLayout == value)
                    return;
                var prevValue = AutoSynchronizeDetailsLayout;
                autoSynchronizeDetailsLayout = value;
                OnChanged(new BaseOptionChangedEventArgs("AutoSynchronizeDetailsLayout", prevValue, AutoSynchronizeDetailsLayout));
            }
        }


        public override void Assign(BaseOptions options) {
            base.Assign(options);
            MyGridOptionsView opt = options as MyGridOptionsView;
            if(opt == null)
                return;

            this.autoSynchronizeDetailsLayout = opt.AutoSynchronizeDetailsLayout;
        }

    }
}
