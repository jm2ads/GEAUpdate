using Commons.Bootstrapper;
using Frontend.Mobile.Areas.Componentes.ViewModels.Interfaces;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Mobile.Areas.Componentes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickerView : PopupPage
	{
        #region Services

        private readonly IPickerViewModel PickerViewModel;

        #endregion

        public PickerView ()
		{
            CloseWhenBackgroundIsClicked = false;
            InitializeComponent();
            BindingContext = this.PickerViewModel = ContainerManager.Resolve<IPickerViewModel>();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PickerViewModel.TapGestureRecognizer_Tapped(sender, e);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            PickerViewModel.TapGestureRecognizer_Tapped_1(sender, e);
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            PickerViewModel.TapGestureRecognizer_Tapped_2(sender, e);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            PickerViewModel.TapGestureRecognizer_Tapped_3(sender, e);
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            PickerViewModel.TapGestureRecognizer_Tapped_4(sender, e);
        }

    }
}