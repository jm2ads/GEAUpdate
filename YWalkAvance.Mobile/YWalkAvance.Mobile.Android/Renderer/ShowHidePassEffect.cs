using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(Frontend.Mobile.Droid.Renderer.ShowHidePassEffect), "ShowHidePassEffect")]
namespace Frontend.Mobile.Droid.Renderer
{
    public class ShowHidePassEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            ConfigureControl();
        }

        protected override void OnDetached()
        {
        }

        private void ConfigureControl()
        {
            EditText editText = ((TextInputLayout)Control).EditText;
            Drawable show = ResourceManager.GetDrawable(this.Control.Context.Resources, "show");
            show.SetBounds(0, 0, editText.LineHeight + 10, editText.LineHeight + 10);

            editText.SetCompoundDrawables(null, null, show, null);
            editText.SetOnTouchListener(new OnDrawableTouchListener());
        }
    }

    public class OnDrawableTouchListener : Java.Lang.Object, Android.Views.View.IOnTouchListener
    {
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            if (v is EditText && e.Action == MotionEventActions.Up)
            {
                EditText editText = (EditText)v;


                if (e.RawX >= (editText.Right - editText.GetCompoundDrawables()[2].Bounds.Width()))
                {
                    if (editText.TransformationMethod == null)
                    {
                        editText.TransformationMethod = PasswordTransformationMethod.Instance;
                        Drawable show = ResourceManager.GetDrawable(v.Context, "show");
                        show.SetBounds(0, 0, editText.LineHeight + 10, editText.LineHeight + 10);
                        editText.SetCompoundDrawables(null, null, show, null);

                    }
                    else
                    {
                        editText.TransformationMethod = null;
                        Drawable hide = ResourceManager.GetDrawable(v.Context, "hide");
                        hide.SetBounds(0, 0, editText.LineHeight + 10, editText.LineHeight + 10);
                        editText.SetCompoundDrawables(null, null, hide, null);
                    }
                    return true;
                }
            }

            return false;
        }
    }
}