using System;
using System.Linq;
using Xamarin.Forms;

namespace com.ws.cvxpress.Custom
{
    public class RoundedCornerView : Image
    {
        public static readonly BindableProperty RoundedCornersProperty = BindableProperty.Create(nameof(RoundedCorners), typeof(string), typeof(RoundedCornerView), "All", validateValue: OnRoundedCornersPropertyValidateValue);
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(RoundedCornerView), Convert.ToDouble(11));
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(RoundedCornerView), Color.Black);
        public static readonly BindableProperty VerticalShadowOffsetProperty = BindableProperty.Create(nameof(VerticalShadowOffset), typeof(double), typeof(RoundedCornerView), -1d);
        public static readonly BindableProperty HorizontalShadowOffsetProperty = BindableProperty.Create(nameof(HorizontalShadowOffset), typeof(double), typeof(RoundedCornerView), 1d);
        public static readonly BindableProperty ShadowOpacityProperty = BindableProperty.Create(nameof(ShadowOpacity), typeof(float), typeof(RoundedCornerView), 0.8f);
        public static readonly BindableProperty ShadowRadiusProperty = BindableProperty.Create(nameof(ShadowRadius), typeof(float), typeof(RoundedCornerView), 2.0f);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RoundedCornerView), Color.Transparent);
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(float), typeof(RoundedCornerView), 2f);


        /// <summary>
        /// The default value is "AllCorners" witch makes all corners rounder.
        /// To round the corners individually, uses a combination of these values "TopLeft, TopRight, BottomLeft, BottomRight" separated by comma.
        /// </summary>
        public string RoundedCorners
        {
            get => (string)GetValue(RoundedCornersProperty);
            set => SetValue(RoundedCornersProperty, value);
        }

        public double CornerRadius
        {
            get => (double)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public float BorderThickness
        {
            get => (float)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        public double VerticalShadowOffset
        {
            get => (double)GetValue(VerticalShadowOffsetProperty);
            set => SetValue(VerticalShadowOffsetProperty, value);
        }

        public double HorizontalShadowOffset
        {
            get => (double)GetValue(HorizontalShadowOffsetProperty);
            set => SetValue(HorizontalShadowOffsetProperty, value);
        }

        public float ShadowOpacity
        {
            get => (float)GetValue(ShadowOpacityProperty);
            set => SetValue(ShadowOpacityProperty, value);
        }

        public float ShadowRadius
        {
            get => (float)GetValue(ShadowRadiusProperty);
            set => SetValue(ShadowRadiusProperty, value);
        }
         

        private static bool OnRoundedCornersPropertyValidateValue(BindableObject bindable, object value)
        {
            var allowedValues = new string[] { "topleft", "topright", "bottomleft", "bottomright", "all", "none" };

            return value.ToString().Split(',').Select(x => x.Trim().ToLower())
                                              .All(item => allowedValues.Contains(item));
        }
    }
}
