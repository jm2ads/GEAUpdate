using Frontend.Mobile.Commons.Components;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(ShowHidePassEffect), "ShowHidePassEffect")]
namespace Frontend.Mobile.iOS.Renderer
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
            if (Control != null)
            {
                UITextField vUpdatedEntry = (UITextField)Control;
                var buttonRect = UIButton.FromType(UIButtonType.Custom);
                buttonRect.SetImage(new UIImage("ShowPass"), UIControlState.Normal);
                buttonRect.TouchUpInside += (object sender, EventArgs e1) =>
                {
                    if (vUpdatedEntry.SecureTextEntry)
                    {
                        vUpdatedEntry.SecureTextEntry = false;
                        buttonRect.SetImage(new UIImage("HidePass"), UIControlState.Normal);
                    }
                    else
                    {
                        vUpdatedEntry.SecureTextEntry = true;
                        buttonRect.SetImage(new UIImage("ShowPass"), UIControlState.Normal);
                    }
                };

                vUpdatedEntry.ShouldChangeCharacters += (textField, range, replacementString) =>
                {
                    string text = vUpdatedEntry.Text;
                    var result = text.Substring(0, (int)range.Location) +
                     replacementString + text.Substring((int)range.Location + (int)range.Length);
                    vUpdatedEntry.Text = result;
                    return false;
                };

                buttonRect.Frame = new CoreGraphics.CGRect(0, -6, 28, 28);

                buttonRect.ContentMode = UIViewContentMode.Right;

                UIView paddingViewRight = new UIView(new System.Drawing.RectangleF
                                                     (0f, -5.0f, 30.0f, 18.0f));
                paddingViewRight.Add(buttonRect);
                paddingViewRight.ContentMode = UIViewContentMode.BottomLeft;


                vUpdatedEntry.RightView = paddingViewRight;
                vUpdatedEntry.RightViewMode = UITextFieldViewMode.Always;

                Control.Layer.CornerRadius = 4;
                Control.Layer.BorderColor = new CoreGraphics.CGColor(255, 255, 255);
                Control.Layer.MasksToBounds = true;
                vUpdatedEntry.TextAlignment = UITextAlignment.Left;
            }
        }
    }
}