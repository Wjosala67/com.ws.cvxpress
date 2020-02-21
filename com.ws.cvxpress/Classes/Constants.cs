using System;
using System.Runtime.Serialization;

namespace com.ws.cvxpress.Classes
{
    public class Constants
    {

        public const string persistUser = "User";
        public const string persistPass = "Password";
        public const string IsTheFirst = "Yes";
        public const string UserType = "Type";
        public const string DatabaseName = "cvxprss_db.sqlite";
        public const string iOS_db_folder = "Library";
        public const string Android_db_folder = "personal";
        public const string terms = "https://www.google.com.mx";
        public const string privacy = "https://www.google.com.mx";
        public Constants()
        {
        }
        public const string AppCode = "cf684aadaf4ed7f1349c65ad1b3f9c4d114ddd2a";
        public const string Traveler = "T";
        public const string Sender = "S";
        public const string currency = "USD ";
        public const string ServerUrl = "http://wjosala67-001-site1.ctempurl.com/";
        //public const string ServerUrl = "http://192.168.100.47/CVXPRSService/";

        //public const string ServerUrl = "http://www.passbox.com.mx/";

        public const string ServerUrlImages = "Images/";

        public const string ServerUrlPdf = "Pdf/";

        public static string ConfURL = ServerUrl + "api/App_Con/";

        public static string TypesURL = ServerUrl + "api/Types/";

        public static string DeliveryTypesURL = ServerUrl + "api/DeliveryTypes/";

        public static string UsersRewardsURL = ServerUrl + "api/Users_Rewards/";

        public static string UsersCardsList= ServerUrl + "api/ClientsCards/";

        public static string CollectTypesURL = ServerUrl + "api/CollectTypes/";

        public static string Categories = ServerUrl + "api/Categories/";

        public static string Countries = ServerUrl + "api/Countries/";

        public static string UpdateCatalog = "UpdateCatalog";

        public static string RegisterUserURL = ServerUrl + "api/Users/";

        public static string RegisterTravelSpecsURL = ServerUrl + "api/TravelerSpecs/";

        public static string RegisterRequestSpecsURL = ServerUrl + "api/RequestSpecs/";

        public static string TravelInfoURL = ServerUrl + "api/TraveInfoClass";

        public static string UserSession = ServerUrl + "api/User_Session/";

        public static string UserCategories = ServerUrl + "api/User_Categories/";

        public static string UserTypes = ServerUrl + "api/User_Types/";

        public static string UserIdents = ServerUrl + "api/User_Ident/"; 

        public static string UserDelivery = ServerUrl + "api/User_DeliveryTypes/";

        public static string UserCollect = ServerUrl + "api/User_CollectTypes/";

        public static string GetUserCategories = ServerUrl + "api/User_Categories/?id=";

        public static string UpdateUserURL = ServerUrl + "api/Users/?id=";

        public static string UpdateRequestSpecsURL = ServerUrl + "api/RequestSpecs/?id=";

        public static string UpdateTravelerSpecsURL = ServerUrl + "api/TravelerSpecs/?id=";

        public static string LoginUserURL = ServerUrl + "api/Users/?id=";

        public static string FB = ServerUrl + "Facebook";

        public static string LC = ServerUrl + "AppLogin";

        public static string RegFB = "RegFB";
        public static string RegAP = "RegAP";

        public static string RegisterCall = "RegisterCall";
        public static string RegisterNoCall = "RegisterNoCall";

        public static string NoModal = "NoModal";
        public static string Modal = "Modal";

        public static string ForUserPic = "ForUserPic";
        public static string ForIdPics = "ForIdPics";
        public static string ForProductPic = "ForProductPic";
        public static string ForIdTrav = "ForIdTrav";


        public static string Tspecs = "Tspecs";
        public static string Sspecs = "Sspecs";

        public static string notified = "notified";

        public static string PdfTerms = "Terms";
        public static string PdfPrivacy = "Privacy";
        public static string PdfUSGenerals = "US";

        public static int RegisterStep = 1;
        public static int RecoverStep = 2;
        public static int UpdatePassStep = 3;

        public static int Done = 1;
        public static int UnDone = 0;

        public static string RegisterUserIdentsURL = ServerUrl + "api/User_Ident/";

        public static string RegisterClientServiceURL = ServerUrl + "api/Client_Service/";

        public static string UpdateClientServiceURL = ServerUrl + "api/Client_Service/?id=";

        public static int ItemTaken = 1;
        public static int RequestAuth = 21;
        public static int Authorized = 20;
        public static int Confirmed = 22;
        public static int Delete = 16;
        public static int Refused = 17;
        public static int Finished = 9;


        public static string RegisterItemToTravelerURL = ServerUrl + "api/Traveler_Request/";

        public static string Notifications = "Notification";

        public static string Disabled = "Disabled";

        public static string Enabled = "Enabled";

        public static string ConvoURL = ServerUrl + "api/Travels_Conversations/";

        public static string RegisterUinfoURL = ServerUrl + "api/UInfoes/";

        public static string RewardsToTravelerURL = ServerUrl + "api/Traveler_RequestCalc/";

        public static string UsersRatings = ServerUrl + "api/Users_Ratings/";

        public static string openRequestsURL = ServerUrl + "api/OpenRequests/";

        // PUSH
        // API TOKEN 
        public static string PushBaseURL = "https://appcenter.ms/api/v0.1/apps/FlightBox/FlightBox";
        public static string PushRestUrl = "/push/notifications";
        public const string Url = "https://api.appcenter.ms/v0.1/apps/";
        public const string ApiKeyName = "FlightBoxToken";
        public const string ApiKey = "6b939d58a42258dc80c8d1f25a40364d5768e96e";
        public const string Organization = "FlightBox";
        public const string Android = "FlightBox";
        public const string IOS = "FlightBox";
        public const string DeviceTarget = "devices_target";
       


        public class Apis { public const string Notification = "push/notifications"; }


        // Open pay
        public const string OPPublicKey = "pk_1df3c605b2104f55936aac5667b1f41d";
        public const string OPPrivateKey = "sk_bae01527c4874dee985d5bdf1746a9eb";
        public const string OPID = "mbptidetnpln150cvh0q";
        public const string OPSandBoxRoute = "https://sandbox-api.openpay.mx/v1/";

       
    }
}
