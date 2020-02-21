using System;
namespace WSViews.Classes
{
    public class Numbers
    {
        public Numbers()
        {
        }


        public static int getNumberCode()
        {
            Random rnd = new Random();
           
            int code = rnd.Next(100000, 999999);



            return code;
        }
    }
}
