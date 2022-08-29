using Frontend.Mobile.Commons.Extensions;
using Frontend.Mobile.Commons.Helpers;
using System;
using Xamarin.Forms;

namespace Frontend.Mobile.Commons.Components
{
    public class CustomRadioButton : View
    {
        public static readonly BindableProperty CheckedProperty = BindableProperty.Create(nameof(Checked), typeof(bool), typeof(CustomRadioButton), defaultValue: false);
        //BindableProperty.Create<CustomRadioButton, bool>(
        //    p => p.Checked, false);

        /// <summary>
        /// The default text property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomRadioButton), defaultValue: string.Empty);
        //BindableProperty.Create<CustomRadioButton, string>(
        //    p => p.Text, string.Empty);

        /// <summary>
        /// The checked changed event.
        /// </summary>
        public EventHandler<EventArgs<bool>> CheckedChanged;


        /// <summary>
        /// Identifies the TextColor bindable property.
        /// </summary>
        /// 
        /// <remarks/>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomRadioButton), defaultValue: Color.Black);

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CustomRadioButton), defaultValue: -1);

        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty = BindableProperty.Create(nameof(FontName), typeof(string), typeof(CustomRadioButton), defaultValue: default(string));

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get
            {
                return this.GetValue<bool>(CheckedProperty);
            }

            set
            {
                this.SetValue(CheckedProperty, value);
                var eventHandler = this.CheckedChanged;
                if (eventHandler != null)
                {

                    eventHandler.Invoke(this, value);
                }
            }
        }

        public string Text
        {
            get
            {
                return this.GetValue<string>(TextProperty);
            }

            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        public Color TextColor
        {
            get
            {
                return this.GetValue<Color>(TextColorProperty);
            }

            set
            {
                this.SetValue(TextColorProperty, value);
            }
        }

        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public string FontName
        {
            get
            {
                return (string)GetValue(FontNameProperty);
            }
            set
            {
                SetValue(FontNameProperty, value);
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }


    }
}
