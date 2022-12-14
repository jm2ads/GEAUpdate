using Frontend.Mobile.Commons.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Frontend.Mobile.Commons.Components
{
    public class BindableRadioGroup : StackLayout
    {

        public List<CustomRadioButton> rads;

        public BindableRadioGroup()
        {

            rads = new List<CustomRadioButton>();
            //CheckedChanged += BindableRadioGroup_CheckedChanged;
        }

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create<BindableRadioGroup, IEnumerable>(o => o.ItemsSource, default(IEnumerable), propertyChanged: OnItemsSourceChanged);


        public static BindableProperty SelectedIndexProperty =
            BindableProperty.Create<BindableRadioGroup, int>(o => o.SelectedIndex, -1, BindingMode.TwoWay, propertyChanged: OnSelectedIndexChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); OnPropertyChanged(); }
        }

        public event EventHandler<int> CheckedChanged;

        public static readonly BindableProperty FontNameProperty =
        BindableProperty.Create<BindableRadioGroup, string>(
        p => p.FontName, string.Empty);
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

        private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable oldvalue, IEnumerable newvalue)
        {
            var radButtons = bindable as BindableRadioGroup;

            radButtons.rads.Clear();
            radButtons.Children.Clear();
            if (newvalue != null)
            {

                int radIndex = 0;
                foreach (var item in newvalue)
                {
                    var rad = new CustomRadioButton();
                    rad.Text = item.ToString();
                    rad.Id = radIndex;
                    if (radIndex == radButtons.SelectedIndex)
                        rad.Checked = true;
                    rad.FontSize = 14;

                    rad.CheckedChanged += radButtons.OnCheckedChanged;

                    radButtons.rads.Add(rad);

                    radButtons.Children.Add(rad);
                    radIndex++;
                }
            }
        }

        private void OnCheckedChanged(object sender, EventArgs<bool> e)
        {

            if (e.Value == false) return;

            var selectedRad = sender as CustomRadioButton;
            SelectedIndex = selectedRad.Id;

            foreach (var rad in rads)
            {
                if (!selectedRad.Id.Equals(rad.Id))
                {
                    rad.Checked = false;
                }
                else
                {
                    if (CheckedChanged != null)
                    {
                        CheckedChanged.Invoke(sender, rad.Id);
                    }
                }

            }

        }

        private static void OnSelectedIndexChanged(BindableObject bindable, int oldvalue, int newvalue)
        {
            if (newvalue == -1) return;

            var bindableRadioGroup = bindable as BindableRadioGroup;


            foreach (var rad in bindableRadioGroup.rads)
            {
                if (rad.Id == bindableRadioGroup.SelectedIndex)
                {
                    rad.Checked = true;
                }

            }


        }

    }
}
