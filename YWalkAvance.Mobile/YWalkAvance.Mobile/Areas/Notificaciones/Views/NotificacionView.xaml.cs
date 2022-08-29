using Commons.Bootstrapper;
using Frontend.Mobile.Areas.Notificaciones.ViewModels.Interfaces;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Mobile.Areas.Notificaciones.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificacionView : PopupPage
	{

        #region Services

        private readonly INotificacionViewModel NotificacionViewModel;

        #endregion

        public NotificacionView ()
		{
            CloseWhenBackgroundIsClicked = false;
            InitializeComponent ();
            BindingContext = this.NotificacionViewModel = ContainerManager.Resolve<INotificacionViewModel>();
        }
	}
}