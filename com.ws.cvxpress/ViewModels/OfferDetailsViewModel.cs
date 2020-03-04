using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WSViews.Classes;
using WSViews.Models;

namespace com.ws.cvxpress.ViewModels
{
    public class OfferDetailsViewModel : BaseViewModel
    {
        private ObservableCollection<cCountry> lstCountries;
        public ObservableCollection<cCountry> LstCountries
        {

            get { return lstCountries; }
            set
            {
                if (Equals(value, lstCountries)) return;
                lstCountries = value;
                OnPropertyChanged(nameof(lstCountries
                ));
            }



        }


        private ObservableCollection<cCountry> lstCountriesDes;
        public ObservableCollection<cCountry> LstCountriesDes
        {

            get { return lstCountriesDes; }
            set
            {
                if (Equals(value, lstCountriesDes)) return;
                lstCountriesDes = value;
                OnPropertyChanged(nameof(lstCountriesDes
                ));
            }



        }


        public OfferDetailsViewModel()
        {
            IList<cCountry> countries = ProvideCountries.InfoListByDemand("America");

            lstCountries = new ObservableCollection<cCountry>(countries.AsEnumerable<cCountry>());


            IList<cCountry> countriesdes = ProvideCountries.InfoListByDemand("America");

            lstCountriesDes = new ObservableCollection<cCountry>(countriesdes.AsEnumerable<cCountry>());


        }
    }
}
