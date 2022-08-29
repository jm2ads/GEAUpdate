using Commons.Commons.Constants;
using Xamarin.Forms;

namespace Frontend.Mobile.Commons.Components
{
    public class PasswordInput : LabeledInput
    {
        public static readonly new BindableProperty IconSourceProperty =
            BindableProperty.Create("IconSource", typeof(string), typeof(LabeledInput), ApplicationConstants.IMG_PASSWORD);

        //public static BindableProperty ShowHideEffectProperty =
        //BindableProperty.Create("ShowHideEffect", typeof(ShowHidePassEffect), typeof(Entry));

        public new string IconSource
        {
            get { return (string)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); OnPropertyChanged("IconSource"); }
        }
        public PasswordInput()
        {
            StackLayout container = BuildContainer();


            #region Annotation
            Label annotation = BuildInputAnnotation();
            #endregion

            #region Entry + Icon (Relative Layout)
            Entry inputValue = new CustomEntry()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsPassword = true,
                TextColor = Color.Black
            };

            if (App.Current.Resources.ContainsKey("grayLight"))
            {
                var color = (Xamarin.Forms.Color)App.Current.Resources["grayLight"];
                inputValue.PlaceholderColor = Color.FromRgba(color.R, color.G, color.B, 0.5);
                inputValue.TextColor = color;
            }

            inputValue.Effects.Add(new ShowHidePassEffect() { });

            Image editIcon = new Image() { Source = ApplicationConstants.IMG_PASSWORD, WidthRequest = 35, HeightRequest = 35, VerticalOptions = LayoutOptions.CenterAndExpand, Margin = 3 };

            RelativeLayout entryAndIcon = BuildEntryAndIconRelativeLayout(inputValue, editIcon);
            #endregion


            container.Children.Add(entryAndIcon);
            container.Children.Add(annotation);

            inputValue.SetBinding(Entry.PlaceholderProperty, nameof(InputLabel), mode: BindingMode.TwoWay);
            inputValue.SetBinding(Entry.TextProperty, nameof(InputValue), mode: BindingMode.TwoWay);
            inputValue.SetBinding(Entry.KeyboardProperty, nameof(InputKeyboard));
            inputValue.SetBinding(Entry.HorizontalTextAlignmentProperty, nameof(HorizontalTextAlignment), mode: BindingMode.TwoWay);

            inputValue.SetBinding(Entry.FontFamilyProperty, nameof(FontFamily), mode: BindingMode.TwoWay);
            inputValue.SetBinding(Entry.IsEnabledProperty, nameof(InputEnabled), mode: BindingMode.TwoWay);

            editIcon.SetBinding(Image.IsVisibleProperty, nameof(InputEnabled));
            editIcon.SetBinding(Image.SourceProperty, nameof(this.IconSource));

            annotation.SetBinding(Label.TextProperty, "InputAnnotation.Text");
            annotation.SetBinding(Label.TextColorProperty, "InputAnnotation.TextColor");
            annotation.SetBinding(Label.HorizontalTextAlignmentProperty, "InputAnnotationTextAlignment");
            annotation.SetBinding(Label.HorizontalOptionsProperty, "InputAnnotationAlignment");

            //Binding magic
            container.BindingContext = this;
            this.Content = container;
        }
    }
}
