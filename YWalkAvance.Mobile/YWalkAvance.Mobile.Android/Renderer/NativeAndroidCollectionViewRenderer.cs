using Android.Content;
using Frontend.Mobile.Commons.Components;
using Frontend.Mobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NativeCollectionView), typeof(NativeAndroidCollectionViewRenderer))]
namespace Frontend.Mobile.Droid.Renderer
{
    public class NativeAndroidCollectionViewRenderer : CollectionViewRenderer
    {
        Context _context;

        public NativeAndroidCollectionViewRenderer(Context context) : base(context)
        {
            _context = context;
            this.ScrollbarFadingEnabled = false;
        }
    }
}