using System;
using System.Text;
using System.Collections.Generic;

namespace com.ws.cvxpress.Classes
{
    public class Translator
    {
        public static string getText(string key)
        {
            var resourceManager = com.ws.cvxpress.AppResources.ResourceManager;
            var exampleXmlString = resourceManager.GetString(key);

            return exampleXmlString;

           
        }
    }
}
