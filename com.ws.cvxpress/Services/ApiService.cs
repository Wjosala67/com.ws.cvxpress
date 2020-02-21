using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;
using com.ws.cvxpress.ViewModels;
using Microsoft.AppCenter;
//using Microsoft.AppCenter;
using Newtonsoft.Json;
using Openpay.Xamarin.Abstractions;
using WSViews;
using WSViews.Classes;

namespace com.ws.cvxpress.Services
{
    public class ApiService
    {
        public ApiService()
        {
        }

        #region App Required Info

        public async Task<ObservableCollection<App_Con>> getConfAsync()
        {
            var client = new HttpClient();

            ObservableCollection<App_Con> lConf = new ObservableCollection<App_Con>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.ConfURL + "?AppCode=" + Constants.AppCode);

                if (response.IsSuccessStatusCode)
                {
                    var resultS = response.Content.ReadAsStringAsync().Result;

                   

                    IList<App_Con> result = App_Con.FromJson(resultS);

                   
                    //var result = JsonConvert.DeserializeObject<List<App_Con>>(resultS);

                    lConf = new ObservableCollection<App_Con>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<bool> UpdateTravelerStatus(int status, User_Session user_session)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");


            user_session.Token = status.ToString();


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(user_session);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PutAsync(Constants.UserSession +  "/?id="+ user_session.Email, content);


            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            bool sResponse = false;
            if (response != null)
            {
                if (response.StatusCode.ToString() != "NoContent") sResponse = false;
                else sResponse = true;
            }
            else sResponse = false;

            return sResponse;
        }

        public async Task<ObservableCollection<User_Session>> getNewTravelers()
        {
            var client = new HttpClient();

            ObservableCollection<User_Session> lConf = new ObservableCollection<User_Session>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UserSession);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<User_Session>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<User_Session>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<string> RegisterForTraveler(User_Session uInfo)
        {
            var client = new HttpClient();
            string sResponse = "NotCreated";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");



            var json = Newtonsoft.Json.JsonConvert.SerializeObject(uInfo);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.UserSession, content);

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response.ReasonPhrase == "Created") sResponse = "Created";



            return sResponse;
        }


        public async Task<bool> UpdateClientRequesAsync(Client_Service sendCliente_service)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");




            var json = Newtonsoft.Json.JsonConvert.SerializeObject(sendCliente_service);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PutAsync(Constants.RegisterClientServiceURL+ sendCliente_service.Id, content);





                //Update local data






            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            bool sResponse =false;
            if (response != null)
            {
                if (response.StatusCode.ToString() != "NoContent") sResponse = false;
                else sResponse = true;
            }
            else sResponse = false;

            return sResponse;




        }

      

        public async Task<string> RegisterPInfo(UInfo uInfo)
        {
            var client = new HttpClient();
            string sResponse = "NotCreated";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

           

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(uInfo);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.RegisterUinfoURL, content);

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response.ReasonPhrase == "Created") sResponse = "Created";
           


            return sResponse;
        }

       

        public async Task<ObservableCollection<Travels_Conversations>> getConvo(int travelID, int requesID)
        {
            var client = new HttpClient();

            ObservableCollection<Travels_Conversations> lConf = new ObservableCollection<Travels_Conversations>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.ConvoURL + "?Id_Travel=" + travelID + "&Id_Request=" + requesID );
                //HttpResponseMessage response = await client.GetAsync(Constants.ConvoURL );

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<Travels_Conversations>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<Travels_Conversations>(result);



                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        internal async Task<bool> SetItemToTravelerAsync(ReserveItemObj reserveItem)
        {
            var client = new HttpClient();
            bool sResponse = false;
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            reserveItem.requestSpecs.imageSource = null;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(reserveItem);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.RegisterItemToTravelerURL, content);

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response.ReasonPhrase == "Created") sResponse = true;
            else sResponse = false;


            return sResponse;
        }

        internal async Task<string> SendRating(Users_Ratings users_Ratings)
        {
            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");



            var json = Newtonsoft.Json.JsonConvert.SerializeObject(users_Ratings);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.UsersRatings, content);



            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();


            return sResponse;
        }

        internal async Task<bool> getTravelerAuthStatus(string email)
        {
            var client = new HttpClient();

            int count = 0;
            User_Session obj = new User_Session();
            bool IsAuth = true;
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            HttpResponseMessage response = null;
            try
            {
                string path = Constants.UserSession + "?id=" + email;
                response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    obj = JsonConvert.DeserializeObject<User_Session>(await response.Content.ReadAsStringAsync());
                    


                    if(obj.Token == "1") { IsAuth = false; }
                }


            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return IsAuth;
        }

        internal async Task<int> getOpenRequestsAsync(int id)
        {
            var client = new HttpClient();

            int count = 0;
            RequestSpecs obj = new RequestSpecs();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            HttpResponseMessage response = null;
            try
            {
                string path = Constants.openRequestsURL + "?idTravel=" + id;
                response =  await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<List<RequestSpecs>>(await response.Content.ReadAsStringAsync());

                    count = (int)result.Count();

                }


            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return count;
        }

        public async Task<bool> AuthChange(RequestSpecs Rspecs)
        {
            var client = new HttpClient();
            bool sResponse = false;
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Rspecs.imageSource = null;
           
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Rspecs);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {

                response = await client.PutAsync(Constants.RegisterRequestSpecsURL + "?Id=" + Rspecs.Id, content);

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response.StatusCode.ToString() != "NoContent") sResponse = false;
            else sResponse = true;

            return sResponse;
        }

        public async Task<ObservableCollection<RequestSpecs>> GetRequestToTravelAsync(int status, string from, string to, string email)
        {
            var client = new HttpClient();

            ObservableCollection<RequestSpecs> lConf = new ObservableCollection<RequestSpecs>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.RegisterRequestSpecsURL + "?status=" + status + "&from="+from+"&to=" + to + "&email=" + email );

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<RequestSpecs>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<RequestSpecs>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }


        
       public async Task<TravelInfoClass> GetInfoTravelLists(int id, int status, string from, string to, string email)
        {
            var client = new HttpClient();

            TravelInfoClass lConf = new TravelInfoClass();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.TravelInfoURL + "?id="+ id + "&status=" + status + "&from=" + from + "&to=" + to + "&email=" + email);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<TravelInfoClass>(await response.Content.ReadAsStringAsync());

                    lConf = result;




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }


        internal async Task<ObservableCollection<CollectTypes>> getCollectTypesAsync()
        {
            var client = new HttpClient();

            ObservableCollection<CollectTypes> lConf = new ObservableCollection<CollectTypes>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.CollectTypesURL);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<CollectTypes>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<CollectTypes>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<ObservableCollection<Client_Service>> GetClientsQuestionsAsync()
        {
            var client = new HttpClient();

            ObservableCollection<Client_Service> lConf = new ObservableCollection<Client_Service>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.RegisterClientServiceURL);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<Client_Service>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<Client_Service>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<Users_Ratings> getUserRatings(int idTr, int idRq)
        {
            var client = new HttpClient();

            Users_Ratings UserR = new Users_Ratings();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UsersRatings +"?id=0" +"&idTravel=" + idTr + "&idRequest="+ idRq);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<Users_Ratings>(await response.Content.ReadAsStringAsync());

                    UserR =  result ;




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return UserR;
        }

        public async Task saveUserIdents(ObservableCollection<IdInfo> idinfos)
        {
            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();

            UserIdentList Ob = new UserIdentList();


            Profile profile = DatabaseHelper.GetProfile(App.Os_Folder);

            Ob.email = profile.Email;

            List<IdInfo> myList = new List<IdInfo>(idinfos);
            List<User_Ident> u_identlist = new List<User_Ident>();
            int i = 1;
            foreach (IdInfo item in myList)
            { 
                u_identlist.Add(
                    new User_Ident 
                    {  
                     Email = profile.Email,
                     Description = item.comments + " " + i,
                     Id_Type = i,
                     Image = item.ImageByte
                    });

                i++;
            }
            Ob.u_identlist = u_identlist;
           
         

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Ob);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.RegisterUserIdentsURL , content);



            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();







        }

        internal async Task<List<User_CollectTypes>> getUserCollectTypesAsync(string email)
        {
            var client = new HttpClient();

            List<User_CollectTypes> lConf = new List<User_CollectTypes>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UserCollect + "?extid=" + email);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<User_CollectTypes>>(await response.Content.ReadAsStringAsync());

                    lConf = new List<User_CollectTypes>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        internal async Task<ObservableCollection<Traveler_Request>> getRequestesAcceptedbyTravelerAsync(TravelerSpecs idInfo)
        {
            var client = new HttpClient();

            ObservableCollection<Traveler_Request> lConf = new ObservableCollection<Traveler_Request>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.RegisterItemToTravelerURL + "?travel=" + idInfo.Id +"&status=0");

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<Traveler_Request>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<Traveler_Request>(result);


                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        internal async Task<string> GetRequestRewardsTravelAsync(TravelerSpecs idInfo)
        {
            var client = new HttpClient();

          
            Traveler_Request obj = new Traveler_Request();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            HttpResponseMessage response = null;
            try
            {
                string path = Constants.RewardsToTravelerURL + "?travelId=" + idInfo.Id + "&email=" + idInfo.Email;
                 response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                   
                }


            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return response.IsSuccessStatusCode.ToString();
        }

        internal async Task<SelectedUser> getRequestesAcceptedbyTravelerAsync(RequestSpecs idInfo)
        {
            var client = new HttpClient();

            SelectedUser lConf = new SelectedUser();
    
            Traveler_Request obj = new Traveler_Request();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.RegisterItemToTravelerURL + "?Id=" + idInfo.Id);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;

                    var result3 = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<Traveler_Request>(result3);

                    response = await client.GetAsync(Constants.RegisterUserURL + "?Id=" + result.Email);
                    if (response.IsSuccessStatusCode)
                    {
                        lConf.user = JsonConvert.DeserializeObject<Users>(await response.Content.ReadAsStringAsync());
                        lConf.image = ImageManager.BytesToImage(lConf.user.UserPhoto);

                        lConf.travelerSpecs = await GetTravelSpecsAsync(result.IdTravelerSpecs);
                    }

                }


            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        internal async Task<RequestSpecs> getRequestById(RequestSpecs idInfo)
        {
            var client = new HttpClient();

            RequestSpecs lConf = new RequestSpecs();

            Traveler_Request obj = new Traveler_Request();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.RegisterRequestSpecsURL + "?Id=" + idInfo.Id);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;

                    var result3 = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<RequestSpecs>(result3);

                    lConf = result;

                }


            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }


        public async Task<string> RegisterClientReques(Client_Service cs)
        {
            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");



            var json = Newtonsoft.Json.JsonConvert.SerializeObject(cs);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.RegisterClientServiceURL, content);



            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();


            return sResponse;
        }

        public async Task<string> RegisterUserCollectAsync(List<User_CollectTypes> selectedDelivery, string email)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();



            UserCollectList DeliveryObject = new UserCollectList
            {
                email = email,
                u_collect = selectedDelivery
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(DeliveryObject);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.UserCollect, content);

                if (response.StatusCode.ToString() == "Created")
                {





                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;
        }

        public async Task<ObservableCollection<Types>> getTypesAsync()
        {
            var client = new HttpClient();

            ObservableCollection<Types> lConf = new ObservableCollection<Types>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.TypesURL);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<Types>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<Types>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<Users> UserRequesAsync(UserRequestObj obj)
        {
            var client = new HttpClient();
            Users User = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj.UserObject);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.RegisterUserURL, content);

                if (response.IsSuccessStatusCode)
                {
                    string users = await response.Content.ReadAsStringAsync();

                    User = Newtonsoft.Json.JsonConvert.DeserializeObject<Users>(users);



                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }




            return User;







        }

        internal async Task<ObservableCollection<TravelerSpecs>> GetTravelsAsync(string sEmail, int status)
        {
            var client = new HttpClient();

            ObservableCollection<TravelerSpecs> lConf = new ObservableCollection<TravelerSpecs>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.RegisterTravelSpecsURL+ "?status="+status+"&email="+ sEmail);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<TravelerSpecs>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<TravelerSpecs>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        internal async Task<TravelerSpecs> GetTravelSpecsAsync( int id)
        {
            var client = new HttpClient();

            TravelerSpecs lConf = new TravelerSpecs();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.RegisterTravelSpecsURL + "?Id=" + id);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<TravelerSpecs>(await response.Content.ReadAsStringAsync());

                    lConf = result;




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }


        internal async Task<ObservableCollection<RequestSpecs>> GetRequestsAsync(string email, int status, string from, string to)
        {
            var client = new HttpClient();

            ObservableCollection<RequestSpecs> lConf = new ObservableCollection<RequestSpecs>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {
                string cons = Constants.RegisterRequestSpecsURL + "?status=" + status + "&from=" + from + "&to=" + to + "&email=" + email;
                HttpResponseMessage response = await client.GetAsync(cons);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<RequestSpecs>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<RequestSpecs>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }


        internal async Task<ObservableCollection<RequestSpecs>> GetRequestsOnRouteAsync(string email, string from, string to, DateTime fromdate)
        {
            var client = new HttpClient();

            ObservableCollection<RequestSpecs> lConf = new ObservableCollection<RequestSpecs>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.RegisterRequestSpecsURL);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<RequestSpecs>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<RequestSpecs>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }


        public async Task<ObservableCollection<User_Ident>> getUserIdent(string email)
        {
            var client = new HttpClient();

            ObservableCollection<User_Ident> lConf = new ObservableCollection<User_Ident>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UserIdents + "?email=" + email + "&type=1");

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<User_Ident>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<User_Ident>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<ObservableCollection<DeliveryTypes>> getDeliveryTypesAsync()
        {
            var client = new HttpClient();

            ObservableCollection<DeliveryTypes> lConf = new ObservableCollection<DeliveryTypes>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.DeliveryTypesURL);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<DeliveryTypes>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<DeliveryTypes>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<ObservableCollection<Categories>> getCategoriesAsync()
        {
            var client = new HttpClient();

            ObservableCollection<Categories> lConf = new ObservableCollection<Categories>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.Categories);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<Categories>>(await response.Content.ReadAsStringAsync());

                    lConf = new ObservableCollection<Categories>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<ObservableCollection<Countries>> getCountriesAsync()
        {
            var client = new HttpClient();

            ObservableCollection<Countries> lConf = new ObservableCollection<Countries>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.Countries);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();


                    var result2 = JsonConvert.DeserializeObject<List<Countries>>(result);


                    lConf = new ObservableCollection<Countries>(result2);




                }


            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        internal async Task<string> RegisterTravelSpecs(TravelerSpecs travelSpecs)
        {
            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");



            var json = Newtonsoft.Json.JsonConvert.SerializeObject(travelSpecs);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {


                response = await client.PostAsync(Constants.RegisterTravelSpecsURL, content);



            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();


            return sResponse;
        }

        internal async Task<string> RegisterRequestSpecs(RequestPObject ObjRequestSpecs)
        {
            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");



            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ObjRequestSpecs);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.RegisterRequestSpecsURL, content);



            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.ReasonPhrase.ToString();


            return sResponse;
        }

        internal async Task<string> DeleteItemFTravel(ReserveItemObj Ro)
        {
            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Ro.requestSpecs.imageSource = null;

            Ro.requestSpecs.status = 16;
            RequestSpecs Rspecs = new RequestSpecs();
            Rspecs = Ro.requestSpecs;
            Rspecs.CountryCodeFrom = Ro.requestSpecs.CountryCodeFrom.Split('-')[0].Trim();
            Rspecs.CountryCodeTo = Ro.requestSpecs.CountryCodeTo.Split('-')[0].Trim();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Rspecs);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.DeleteAsync(Constants.RegisterItemToTravelerURL + "?Id=" + Rspecs.Id);


                response = await client.PutAsync(Constants.RegisterRequestSpecsURL + "?Id=" + Rspecs.Id, content);

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();


            return sResponse;
        }

        internal async Task<string> UpdateItemFTravel(ReserveItemObj Ro)
        {
            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Ro.requestSpecs.imageSource = null;


            RequestSpecs Rspecs = new RequestSpecs();
            Rspecs = Ro.requestSpecs;
            Rspecs.CountryCodeFrom = Ro.requestSpecs.CountryCodeFrom.Split('-')[0].Trim();
            Rspecs.CountryCodeTo = Ro.requestSpecs.CountryCodeTo.Split('-')[0].Trim();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Rspecs);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            { 

                response = await client.PutAsync(Constants.RegisterRequestSpecsURL + "?Id=" + Rspecs.Id, content);

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();


            return sResponse;
        }
       
        public async Task<string> UpdateRequestSpecs(RequestSpecs requestSpecs, int status)
        {

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");


            requestSpecs.status = status;
            string codefrom = requestSpecs.CountryCodeFrom.Split('-')[0].Trim();
            string codeto = requestSpecs.CountryCodeTo.Split('-')[0].Trim();
            requestSpecs.CountryCodeFrom = codefrom;
            requestSpecs.CountryCodeTo = codeto;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestSpecs);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PutAsync(Constants.UpdateRequestSpecsURL + requestSpecs.Id, content);





                //Update local data






            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;





        }
        #endregion


        #region related to user

        public async Task<string> RegisterAsync(string email, string password, string RegType, int RequestId, int status)
        {

            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();

            UserRequestObj Ob = new UserRequestObj();

            System.Guid? Token =  await AppCenter.GetInstallIdAsync();
            string token = (Token != null) ? Token.Value.ToString().Trim() : "";

            var model = new Users
            {

                Email = email.ToLower(),
                Password = Crypto.encryptar(password, Constants.AppCode),
                PhoneNumber = "",
                FirstName = "",
                LastName = "",
                Address = token,
                ZipCode = "",
                Number = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString(),
                VerificationCode = Numbers.getNumberCode().ToString(),
                PassportLimitDate = "",
                UserPhoto = null,
                RegisteredWith = RegType,
                External_id = email

            };
          


            Ob.RequestID = RequestId;
            Ob.status = status;
            Ob.UserObject = model;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Ob);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.RegisterUserURL, content);

                if (response.StatusCode.ToString() == "Created")
                {



                    // 2- when we get the answer save it to database.
                    Profile profile = new Profile();

                    profile.Email = email;
                    profile.FirstName = "";
                    profile.Picture = "";
                    profile.Token = token;
                    profile.registeredwith = RegType;
                    Criptografia criptografia = new Criptografia();
                    profile.PhoneNumber = criptografia.encryptar("", Constants.AppCode);
                    profile.Password = criptografia.encryptar(password, Constants.AppCode);

                    DatabaseHelper.Insert(ref profile, App.Os_Folder);

                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();


            return sResponse;





        }

        public async Task<string> UpdateUserAsync(Profile profile, int ID)
        {

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();

            UserRequestObj ob = new UserRequestObj();

           

            var model = new Users
            {

                Email = profile.Email,
                Password = profile.Password,
                PhoneNumber = profile.PhoneNumber,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Address = profile.Token,
                ZipCode = profile.VerificationCode,
                Number = DateTime.Now.ToShortDateString(),
                VerificationCode = profile.VerificationCode,
                PassportLimitDate = profile.PassportDate,
                RegisteredWith = profile.registeredwith,
                UserPhoto = null,
                External_id = profile.Email

            };

            ob.RequestID = ID;
            ob.status = 0;
            ob.UserObject = model;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ob);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PutAsync(Constants.UpdateUserURL + profile.Email, content);





                //Update local data






            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;





        }

        public async Task<string> UpdateUserAsync(Users model)
        {

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();

            UserRequestObj ob = new UserRequestObj();

           
            ob.RequestID = 2;
            ob.status = 0;
            ob.UserObject = model;



            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ob);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PutAsync(Constants.UpdateUserURL + model.Email, content);





                //Update local data






            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;





        }

        public async Task<string> UpdateTravelSpecsAsync(TravelerSpecs model)
        {

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            model.CountryCodeFrom = model.CountryCodeFrom.Split('-')[0].Trim();
            model.CountryCodeTo = model.CountryCodeTo.Split('-')[0].Trim();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PutAsync(Constants.UpdateTravelerSpecsURL + model.Id, content);


            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;





        }

        public async Task<Users> LoginAsync(string email)
        {

            var client = new HttpClient();
            Users User = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");


            try
            {
                HttpResponseMessage response = await client.GetAsync(Constants.LoginUserURL + email);

                if (response.IsSuccessStatusCode)
                {
                    string users = await response.Content.ReadAsStringAsync();

                    User = Newtonsoft.Json.JsonConvert.DeserializeObject<Users>(users);



                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }


            return User;

        }

        public async Task<string> RegisterUserCategoriesAsync(List<User_Categories> categories, string semail)
        {

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();



            UserCategoriesList categoriesObject = new UserCategoriesList
            {
                email = semail,
                u_categories = categories
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(categoriesObject);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.UserCategories, content);

                if (response.StatusCode.ToString() == "Created")
                {





                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }


            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;




        }

        public async Task<string> RegisterUserTypesAsync(List<User_Types> types, string semail)
        {

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();



            UserTypesList typesObject = new UserTypesList
            {
                email = semail,
                u_types = types
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(typesObject);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.UserTypes, content);

                if (response.StatusCode.ToString() == "Created")
                {





                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;





        }

        public async Task<string> RegisterUserDeliveryAsync(List<User_DeliveryTypes> deliverytypes, string semail)
        {

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();



            UserDeliveryList DeliveryObject = new UserDeliveryList
            {
                email = semail,
                u_delivery = deliverytypes
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(DeliveryObject);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.UserDelivery, content);

                if (response.StatusCode.ToString() == "Created")
                {





                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;





        }

        public async Task<string> RegisterConvoMessage(Travels_Conversations convo)
        {

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

          

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(convo);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.ConvoURL, content);

                if (response.StatusCode.ToString() == "Created")
                {





                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            string sResponse = "";
            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();

            return sResponse;





        }

        public async Task<List<User_Categories>> getUserCategoriesAsync(string email)
        {
            var client = new HttpClient();

            List<User_Categories> lConf = new List<User_Categories>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UserCategories + "?extid=" + email);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<User_Categories>>(await response.Content.ReadAsStringAsync());

                    lConf = new List<User_Categories>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<List<User_Types>> getUserTypesAsync(string email)
        {
            var client = new HttpClient();

            List<User_Types> lConf = new List<User_Types>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UserTypes + "?extid=" + email);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<User_Types>>(await response.Content.ReadAsStringAsync());

                    lConf = new List<User_Types>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<List<User_DeliveryTypes>> getUserDeliveryTypesAsync(string email)
        {
            var client = new HttpClient();

            List<User_DeliveryTypes> lConf = new List<User_DeliveryTypes>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UserDelivery + "?extid=" + email);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;


                    var result = JsonConvert.DeserializeObject<List<User_DeliveryTypes>>(await response.Content.ReadAsStringAsync());

                    lConf = new List<User_DeliveryTypes>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

        public async Task<List<RewardObject>> getUserRewardsAsync(string Email)
        {
            var client = new HttpClient();

            List<RewardObject> lConf = new List<RewardObject>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UsersRewardsURL + "?Email=" + Email);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<List<RewardObject>>(await response.Content.ReadAsStringAsync());

                    lConf = new List<RewardObject>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }

      
        public async Task<string> RegisterUInfo(string email, string password, string RegType, int RequestId, int status)
        {

            var client = new HttpClient();
            string sResponse = "";
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            Criptografia Crypto = new Criptografia();

            UserRequestObj Ob = new UserRequestObj();

            //System.Guid? Token = await AppCenter.GetInstallIdAsync();

            var model = new Users
            {

                Email = email,
                Password = Crypto.encryptar(password, Constants.AppCode),
                PhoneNumber = "",
                FirstName = "",
                LastName = "",
                Address ="",
                ZipCode = "",
                Number = DateTime.Now.ToShortDateString(),
                VerificationCode = Numbers.getNumberCode().ToString(),
                PassportLimitDate = "",
                UserPhoto = null,
                RegisteredWith = RegType,

            };


            Ob.RequestID = RequestId;
            Ob.status = status;
            Ob.UserObject = model;

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Ob);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.RegisterUserURL, content);

                if (response.StatusCode.ToString() == "Created")
                {



                    // 2- when we get the answer save it to database.
                    Profile profile = new Profile();

                    profile.Email = email;
                    profile.FirstName = "";
                    profile.Picture = "";
                    profile.Token = "";
                    profile.registeredwith = RegType;
                    Criptografia criptografia = new Criptografia();
                    profile.PhoneNumber = criptografia.encryptar("", Constants.AppCode);
                    profile.Password = criptografia.encryptar(password, Constants.AppCode);

                    DatabaseHelper.Insert(ref profile, App.Os_Folder);

                }

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }

            if (response == null) sResponse = "Connection refused";
            else sResponse = response.StatusCode.ToString();


            return sResponse;





        }


        #endregion


        #region Card Managment
        internal async Task<cMessage> deleteUserCard(string email, string id)
        {
            var client = new HttpClient();

            cMessage cMess = new cMessage();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.DeleteAsync(Constants.UsersCardsList + "?email=" + email + "&IdCard="+ id);

                if (response.IsSuccessStatusCode)
                {
                

                    var result = JsonConvert.DeserializeObject<cMessage>(await response.Content.ReadAsStringAsync());

                    cMess =  result;




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return cMess;
        }

        public async Task<List<Card>> getUserCards(string Email)
        {
            var client = new HttpClient();

            List<Card> lConf = new List<Card>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(Constants.ServerUrl);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            try
            {

                HttpResponseMessage response = await client.GetAsync(Constants.UsersCardsList + "?email=" + Email);

                if (response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<List<Card>>(await response.Content.ReadAsStringAsync());

                    lConf = new List<Card>(result);




                }
            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }
            return lConf;
        }


        #endregion

        #region Push




        public async Task<string> PushAsync(string email)
        {

            var client = new HttpClient();
            Dictionary<string, string> customData = default(Dictionary<string, string>);
            string sResponse = "";
            String token = "51213c919ffd494a82c40520f7b01245c6c7e983";
            client.DefaultRequestHeaders.Accept.Clear();

            client.BaseAddress = new Uri(Constants.PushBaseURL);
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("X-API-Token", token);
         

            // get user

            Users user = await LoginAsync(email);

            if (user != null)
            {
                string[] devices = new string[] { user.Address };

                customData = new Dictionary<string, string>();
                customData.Add("GoTo", "ClientService");


                mPush push = new mPush
                {
                    Content = new Content
                    {
                        Name = Constants.Organization,
                        Title = Translator.getText("OneNotification"),
                        Body = Translator.getText("ClientServiceNot"),
                        CustomData = customData
                    },
                    Target = new Target
                    {
                        Type = "devices_target",
                        Devices = devices
                    }

                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(push);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                try
                {
                    response = await client.PostAsync(Constants.PushBaseURL + Constants.PushRestUrl, content);

                }
                catch (TimeoutException ex)
                {

                    Debug.WriteLine(ex);

                }
                catch (Exception exc)
                {

                    Debug.WriteLine(exc);
                }
            }

          


            return sResponse;





        }


        public async Task<string> PushAsyncGeneral(string email, string stBody, string TypeMessage)
        {

            var client = new HttpClient();
            string sResponse = "";
            Dictionary<string, string> customData = default(Dictionary<string, string>);
            String token = Constants.ApiKey;
            client.DefaultRequestHeaders.Accept.Clear();

            client.BaseAddress = new Uri(Constants.PushBaseURL);
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("X-API-Token", token);

            customData = new Dictionary<string, string>();
            customData.Add("GoTo", TypeMessage);

            // get user

            Users user = await LoginAsync(email);

            string[] devices = { user.Address };

            mPush push = new mPush
            {
                Content = new Content
                {
                    Name = Constants.Organization ,
                    Title = Translator.getText("OneNotification"),
                    Body = stBody, 
                    CustomData = customData
                },
                Target = new Target
                {
                    Type = "devices_target",
                    Devices = devices
                }

            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(push);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(Constants.PushBaseURL + Constants.PushRestUrl, content);

            }
            catch (TimeoutException ex)
            {

                Debug.WriteLine(ex);

            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc);
            }




            return sResponse;





        }



        #endregion
    }
}
