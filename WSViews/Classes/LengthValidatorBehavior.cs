using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace WSViews
{
    public class LengthValidatorBehavior : Behavior<Entry>
    {
        public static bool IsValid;
        public int min { get; set; }
        public int max { get; set; }
        public string type { get; set; }
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = true;

            if (((Entry)sender).Text != null) { 
 
            if (type == "C")
            {
                if (((Entry)sender).Text.ToString().Length < min || ((Entry)sender).Text.ToString().Length > max) IsValid = false;
                ((Entry)sender).TextColor = IsValid ? Color.LightGray : Color.DarkGray;

                    ((Entry)sender).Text =   System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(((Entry)sender).Text.ToLower());


            } else if (type == "N") {

                ((Entry)sender).TextColor = Color.DarkGray;
                int result;
                if (!(int.TryParse(((Entry)sender).Text, out result)) || ((Entry)sender).Text.Length != max) { ((Entry)sender).TextColor = Color.Black; } else { IsValid = true;}

            }
            else if (type == "P")
            {
                ((Entry)sender).TextColor = Color.Green;

                if (((Entry)sender).Text.Length == max) { IsValid = true; } else { ((Entry)sender).TextColor = Color.Red; }

            }
            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }

}