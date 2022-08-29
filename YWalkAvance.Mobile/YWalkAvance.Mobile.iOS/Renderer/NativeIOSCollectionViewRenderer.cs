using Frontend.Mobile.Commons.Components;
using Frontend.Mobile.iOS.Renderer;
using System.Runtime.Remoting.Contexts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NativeCollectionView), typeof(NativeIOSCollectionViewRenderer))]
namespace Frontend.Mobile.iOS.Renderer
{
    public class NativeIOSCollectionViewRenderer : CollectionViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<GroupableItemsView> e)
        {
            base.OnElementChanged(e);
        }
    }
}