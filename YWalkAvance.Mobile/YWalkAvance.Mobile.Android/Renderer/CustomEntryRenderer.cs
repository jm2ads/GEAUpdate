using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Frontend.Mobile.Droid.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Java.Util.ResourceBundle;
//ASOSA Pone el teclado numerico en celulares viejos
[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace Frontend.Mobile.Droid.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);


            if (Control != null)
            {
                if (((Entry)e.NewElement).Keyboard == Xamarin.Forms.Keyboard.Numeric)
                {
                    this.Control.KeyListener = DigitsKeyListener.GetInstance(true, true);
                    this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
                }
            }
        }
    }
}