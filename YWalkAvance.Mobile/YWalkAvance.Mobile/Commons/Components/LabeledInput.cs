using Commons;
using Commons.Commons.Constants;
using Frontend.Mobile.Commons.Models;
using System;
using Xamarin.Forms;

namespace Frontend.Mobile.Commons.Components
{
    public class LabeledInput : ContentView
    {
        public static readonly BindableProperty InputLabelProperty =
          BindableProperty.Create("InputLabel", typeof(string), typeof(LabeledInput), "", BindingMode.TwoWay);

        public string InputLabel
        {
            get { return (string)GetValue(InputLabelProperty); }
            set { SetValue(InputLabelProperty, value); OnPropertyChanged("InputLabel"); }
        }

        public static readonly BindableProperty InputValueProperty =
            BindableProperty.Create("InputValue", typeof(string), typeof(LabeledInput), "", BindingMode.TwoWay);
        public string InputValue
        {
            get { return (string)GetValue(InputValueProperty); }
            set { SetValue(InputValueProperty, value); OnPropertyChanged("InputValue"); }
        }

        public static readonly BindableProperty InputAnnotationProperty =
            BindableProperty.Create("InputAnnotation", typeof(AnnotationModel), typeof(LabeledInput), AnnotationModel.DefaultAnnotation(), BindingMode.TwoWay);
        public AnnotationModel InputAnnotation
        {
            get { return (AnnotationModel)GetValue(InputAnnotationProperty); }
            set { SetValue(InputAnnotationProperty, value); OnPropertyChanged("InputAnnotation"); }
        }

        public static readonly BindableProperty InputAnnotationTextAlignmentProperty =
        BindableProperty.Create("InputAnnotationTextAlignment", typeof(TextAlignment), typeof(LabeledInput), TextAlignment.Start, BindingMode.TwoWay);
        public TextAlignment InputAnnotationTextAlignment
        {
            get { return (TextAlignment)GetValue(InputAnnotationTextAlignmentProperty); }
            set { SetValue(InputAnnotationTextAlignmentProperty, value); OnPropertyChanged("InputAnnotationTextAlignment"); }
        }

        public static readonly BindableProperty InputAnnotationAlignmentProperty =
        BindableProperty.Create("InputAnnotationAlignment", typeof(LayoutOptions), typeof(LabeledInput), LayoutOptions.Start, BindingMode.TwoWay);
        public LayoutOptions InputAnnotationAlignment
        {
            get { return (LayoutOptions)GetValue(InputAnnotationAlignmentProperty); }
            set { SetValue(InputAnnotationAlignmentProperty, value); OnPropertyChanged("InputAnnotationAlignment"); }
        }

        public static readonly BindableProperty InputKeyboardProperty =
            BindableProperty.Create("InputKeyboard", typeof(Xamarin.Forms.Keyboard), typeof(LabeledInput), Keyboard.Text);
        public Keyboard InputKeyboard
        {
            get { return (Xamarin.Forms.Keyboard)GetValue(InputKeyboardProperty); }
            set { SetValue(InputKeyboardProperty, value); OnPropertyChanged("InputKeyboard"); }
        }

        public static readonly BindableProperty HorizontalTextAlignmentProperty =
        BindableProperty.Create("HorizontalTextAlignment", typeof(Xamarin.Forms.TextAlignment), typeof(LabeledInput), TextAlignment.Start);
        public TextAlignment HorizontalTextAlignment
        {
            get { return (Xamarin.Forms.TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); OnPropertyChanged("HorizontalTextAlignment"); }
        }

        public static readonly BindableProperty InputEnabledProperty =
            BindableProperty.Create("InputEnabled", typeof(bool), typeof(LabeledInput), true);
        public bool InputEnabled
        {
            get { return (bool)GetValue(InputEnabledProperty); }
            set { SetValue(InputEnabledProperty, value); OnPropertyChanged("InputEnabled"); }
        }

        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create("IconSource", typeof(string), typeof(LabeledInput), "");
        public string IconSource
        {
            get { return (string)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); OnPropertyChanged("IconSource"); }
        }

        public static readonly BindableProperty FontFamilyProperty =
       BindableProperty.Create("FontFamily", typeof(string), typeof(LabeledInput));
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); OnPropertyChanged("FontFamily"); }
        }

        public static readonly BindableProperty UnfocusedCommandProperty =
            BindableProperty.Create("UnfocusedCommand", typeof(Command), typeof(LabeledInput), null);
        public Command UnfocusedCommand
        {
            get { return (Command)GetValue(UnfocusedCommandProperty); }
            set { SetValue(UnfocusedCommandProperty, value); OnPropertyChanged(nameof(UnfocusedCommand)); }
        }

        public static readonly BindableProperty FocusedCommandProperty =
    BindableProperty.Create("FocusedCommand", typeof(Command), typeof(LabeledInput), null);
        public Command FocusedCommand
        {
            get { return (Command)GetValue(FocusedCommandProperty); }
            set { SetValue(FocusedCommandProperty, value); OnPropertyChanged(nameof(FocusedCommand)); }
        }
        public static readonly BindableProperty CompletedCommandProperty =
    BindableProperty.Create("CompletedCommand", typeof(Command), typeof(LabeledInput), null);
        public Command CompletedCommand
        {
            get { return (Command)GetValue(CompletedCommandProperty); }
            set { SetValue(CompletedCommandProperty, value); OnPropertyChanged(nameof(CompletedCommand)); }
        }

        /// <summary>
        /// IconSource defaults to Edit Pencil, can be modified with custom entry icon.
        /// </summary>
        public LabeledInput()
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
                TextColor = Color.Black
                //HeightRequest = 16
            };

            //if (App.Current.Resources.ContainsKey("grayLight"))
            //{
            //    var color = (Xamarin.Forms.Color)App.Current.Resources["grayLight"];
            //    inputValue.PlaceholderColor = Color.FromRgba(color.R, color.G, color.B, 0.5);
            //    inputValue.TextColor = color;
            //}


            Image editIcon = new Image() { Source = ApplicationConstants.IMG_EDIT, WidthRequest = 30, HeightRequest = 30, VerticalOptions = LayoutOptions.CenterAndExpand, Margin=3 };

            RelativeLayout entryAndIcon = BuildEntryAndIconRelativeLayout(inputValue, editIcon);

            #endregion

            container.Children.Add(entryAndIcon);
            container.Children.Add(annotation);

            inputValue.SetBinding(Entry.PlaceholderProperty, nameof(InputLabel), mode: BindingMode.TwoWay);
            inputValue.SetBinding(Entry.FontFamilyProperty, nameof(FontFamily), mode: BindingMode.TwoWay);
            inputValue.SetBinding(Entry.TextProperty, nameof(InputValue), mode: BindingMode.TwoWay);
            inputValue.SetBinding(Entry.KeyboardProperty, nameof(InputKeyboard));
            inputValue.SetBinding(Entry.HorizontalTextAlignmentProperty, nameof(HorizontalTextAlignment), mode: BindingMode.TwoWay);
            inputValue.SetBinding(Entry.IsEnabledProperty, nameof(InputEnabled), mode: BindingMode.TwoWay);
            //Bind entry unfocused event to unfocused command
            inputValue.Unfocused += InputValue_Unfocused;
            inputValue.Focused += InputValue_Focused;

            editIcon.SetBinding(Image.IsVisibleProperty, nameof(InputEnabled));
            editIcon.SetBinding(Image.SourceProperty, nameof(IconSource));

            annotation.SetBinding(Label.TextProperty, "InputAnnotation.Text");
            annotation.SetBinding(Label.TextColorProperty, "InputAnnotation.TextColor");
            annotation.SetBinding(Label.HorizontalTextAlignmentProperty, "InputAnnotationTextAlignment");
            annotation.SetBinding(Label.HorizontalOptionsProperty, "InputAnnotationAlignment");

            //Binding magic
            container.BindingContext = this;
            this.Content = container;
        }

        protected void InputValue_Unfocused(object sender, FocusEventArgs e)
        {
            if (UnfocusedCommand != null)
                UnfocusedCommand.Execute(sender);
        }

        protected void InputValue_Focused(object sender, FocusEventArgs e)
        {
            if (FocusedCommand != null)
                FocusedCommand.Execute(sender);
        }

        protected static StackLayout BuildContainer()
        {
            return new StackLayout()
            {
                Spacing = Device.RuntimePlatform == Device.iOS ? 10 : 0,
                Margin = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                //BackgroundColor = Color.Gray
            };
        }

        protected static Label BuildInputAnnotation()
        {
            Label annotation = new Label() {
                Margin = 0,
                TextColor = Color.Black
            };
            if (App.Current.Resources.ContainsKey("smallText"))
            {
                annotation.Style = (Xamarin.Forms.Style)App.Current.Resources["smallText"];
            }

            return annotation;
        }

        protected static RelativeLayout BuildEntryAndIconRelativeLayout(Entry inputValue, Image editIcon)
        {
            RelativeLayout entryAndIcon = new RelativeLayout() { HeightRequest = 50 };
            //Add image
            entryAndIcon.Children.Add(editIcon, Constraint.RelativeToParent((parent) =>
            {
                return 0; //X Position
            }), Constraint.RelativeToParent((parent) =>
            {
                if (Device.RuntimePlatform == Device.iOS)
                    return 18; //Y Position
                else
                    return 15; //Y Position
            }));
            //Add input
            entryAndIcon.Children.Add(inputValue, Constraint.RelativeToParent((parent) =>
            {
                if (String.IsNullOrEmpty(editIcon.Source.ToString()))
                    return 0;
                else
                {
                    //if (Device.RuntimePlatform == Device.iOS)
                    //    return editIcon.Width + 5; //X Position
                    //else
                    //    return editIcon.Width + 2; //X Position
                    return 0;
                }

            }), Constraint.RelativeToParent((parent) =>
            {
                if (Device.RuntimePlatform == Device.iOS)
                    return 6; //Y Position
                else
                    return 0; //Y Position
            }), Constraint.RelativeToParent((parent) =>
            {
                return parent.Width;// - editIcon.Width; //Width
            }), Constraint.RelativeToParent((parent) =>
            {
                return parent.Height; //Height
            }));
            return entryAndIcon;
        }
    }
}
