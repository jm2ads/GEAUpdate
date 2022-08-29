using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Frontend.Mobile.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRendereriOS))]
namespace Frontend.Mobile.iOS.Renderer
{
    public class CustomSearchBarRendereriOS : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            var searchBar = (UISearchBar)Control;

            if (e.NewElement != null) {
                searchBar.ShowsCancelButton = false;
            }
        }
    }
}