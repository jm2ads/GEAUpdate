using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Frontend.Mobile.Commons.Components
{
    public class EntryLengthValidatorBehavior : Behavior<Entry>
    {
        public int MaxLength { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            try {
                var entry = (Entry)sender;

                if (entry.Text != null && entry.Text.Length > MaxLength)
                {
                    string entryText = entry.Text;

                    entryText = entryText.Remove(entryText.Length - 1);

                    entry.Text = entryText;
                }
            }
            catch (Exception ex) {
                var exception = ex.Message;
            }
            
        }
    }
}
