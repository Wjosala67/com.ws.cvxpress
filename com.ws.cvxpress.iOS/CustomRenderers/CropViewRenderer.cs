using System;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.iOS.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer( typeof(CropView) , typeof(CropViewRenderer))]

namespace com.ws.cvxpress.iOS.CustomRenderers
{
    public class CropViewRenderer: PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            var page = Element as CropView;
            if (page != null)
            { 

            }
            }
    }
}
