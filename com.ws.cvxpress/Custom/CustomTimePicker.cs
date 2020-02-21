using System;
using Xamarin.Forms;

namespace com.ws.cvxpress.Custom
{
    public class CustomTimePicker : TimePicker
    {
        public static readonly BindableProperty ImageProperty =
          BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomTimePicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor),
            typeof(Color), typeof(CustomEntry), Color.Gray);
        // Gets or sets BorderColor value  
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
    }
}
