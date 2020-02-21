using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using com.ws.cvxpress.Classes;
using Newtonsoft.Json;

namespace com.ws.cvxpress.Models
{
    public class Notify
    {
       
    
        public async Task mNotify(
        Users user,
        string name,
        string title,
        string body,
        IDictionary<string, string> payload)
            {

            var client = new HttpClient();
            string sResponse = "";
            String token = "51213c919ffd494a82c40520f7b01245c6c7e983";
            client.DefaultRequestHeaders.Accept.Clear();


            //client.DefaultRequestHeaders.Add("Authorization", String.Format("X-API-Token {0}", token));

            client.BaseAddress = new Uri(Constants.Url);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.Authorization
                               = new AuthenticationHeaderValue("X-API-Token", token);



            // let's assume you have a User object that contains 
            // * iOS Devices 
            // * Android Devices
            var push = new mPush
                {
                    Content = new Content
                    {
                        Name = name,
                        Title = title,
                        Body = body,
                      
                    },
                    Target = new Target
                    {
                        Type = Constants.DeviceTarget
                    }
                };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(push);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            //if (user.IOSDevices.Any())
            //{
            //    push.Target.Devices = user.IOSDevices;
            //    await PostAsync($"{Constants.Organization}/{Constants.IOS}/{Constants.Apis.Notification}", JsonConvert.SerializeObject(push));
            //}

            //if (user.AndroidDevices.Any())
            {
                    //push.Target.Devices = "0439d754-56fd-47e8-846c-48b798f1843a";
                    var a = await client.PostAsync($"{Constants.Organization}/{Constants.Android}/{Constants.Apis.Notification}", content);
                string b = "";
                }
            }
    }
}
