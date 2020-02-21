using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace com.ws.cvxpress.Views.Operation
{
    public partial class RattingBar : ContentView
    {
        public event EventHandler ItemTapped = delegate { };
        public static int selectedStarValue = 0;
        private static string emptyStarImage = string.Empty;
        private static string fillStarImage = string.Empty;
        private static Image star1;
        private static Image star2;
        private static Image star3;
        private static Image star4;
        private static Image star5;
        public RattingBar()
        {
            InitializeComponent();
            star1 = new Image();
            star2 = new Image();
            star3 = new Image();
            star4 = new Image();
            star5 = new Image();

            lb_frame2Func();

            void lb_frame2Func()
            {
                try
                {
                    star1.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            
                            

                        }
                            )
                    });
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            }

            stkRattingbar.Children.Add(star1);
            stkRattingbar.Children.Add(star2);
            stkRattingbar.Children.Add(star3);
            stkRattingbar.Children.Add(star4);
            stkRattingbar.Children.Add(star5);

        }

      

    }
}
