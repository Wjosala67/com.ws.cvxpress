using System;
namespace com.ws.cvxpress.ViewModels
{
    public class PopDelayViewModel : BaseViewModel
    {
        public DateTime todate;
        public DateTime ToDate
        {
            get { return todate; }
            set
            {
                todate = value;
                OnPropertyChanged();
            }
        }
        public PopDelayViewModel()
        {
        }
    }
}
