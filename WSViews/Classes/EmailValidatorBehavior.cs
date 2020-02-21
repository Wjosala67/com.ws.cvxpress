using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace WSViews
{
    public class EmailValidatorBehavior : Behavior<Entry>
    {
        public static bool IsValid;
        public int min { get; set; }
        public int max { get; set; }
        public string type { get; set; }

        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = false;
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            if (((Entry)sender).Text.ToString().Length < min || ((Entry)sender).Text.ToString().Length > max) IsValid = false;
            ((Entry)sender).TextColor = IsValid ? Color.LightGray : Color.Black;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
