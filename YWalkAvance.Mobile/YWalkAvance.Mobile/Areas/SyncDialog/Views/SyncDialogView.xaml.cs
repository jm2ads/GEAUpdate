using Commons.Bootstrapper;
using Frontend.Mobile.Areas.SyncDialog.ViewModels.Interfaces;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Mobile.Areas.SyncDialog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SyncDialogView : PopupPage
    {
        #region Services

        private readonly ISyncDialogViewModel SyncDialogViewModel;

        #endregion
        public SyncDialogView()
        {
            CloseWhenBackgroundIsClicked = false;
            InitializeComponent();
            BindingContext = this.SyncDialogViewModel = ContainerManager.Resolve<ISyncDialogViewModel>();
        }
    }
}