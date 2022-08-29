using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Frontend.Mobile.Commons.Components;
using Frontend.Mobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace Frontend.Mobile.Droid.Renderer
{
    public class CustomPickerRenderer : PickerRenderer
    {
        private CustomPicker element;

        public CustomPickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (CustomPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image)) {
                Control.Background = AddPickerStyles(element.Image);
            }

        }

        private LayerDrawable AddPickerStyles(string imagePath)
        {
            Drawable[] layers = { GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0,0,0,0,0);

            return layerDrawable;
        }

        private Drawable GetDrawable(string imagePath)
        {
            //TIRA 0 EL resID 
            //int resID = Resources.GetIdentifier(imagePath,"drawable",this.Context.PackageName);
            //int resID = (int)typeof(Resource.Drawable).GetField(imagePath).GetValue(null);
            var drawable = ResourceManager.GetDrawable(this.Control.Context.Resources, imagePath);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 40, 40, true));
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }
    }
}