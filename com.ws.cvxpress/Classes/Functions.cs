using System;
using System.Collections.Generic;
using System.Linq;
using com.ws.cvxpress.Helpers;
using com.ws.cvxpress.Models;

namespace com.ws.cvxpress.Classes
{
    public class Functions
    {
        public Functions()
        {
        }

        public static decimal getReward (decimal productValue, decimal Weight, int qty)
        {
            decimal reward;

            List<App_Con> lstSetting = DatabaseHelper.getConfiguration(App.Os_Folder);

            decimal PorRecSobreCostoArticulo = decimal.Parse((from list in lstSetting where list.Name == "PorRecSobreCostoArticulo" select list.Value).FirstOrDefault());
            decimal CostoRecPorKilo = decimal.Parse((from list in lstSetting where list.Name == "CostoRecPorKilo" select list.Value).FirstOrDefault());
            decimal ValMinSerCompRec = decimal.Parse((from list in lstSetting where list.Name == "ValMinSerCompRec" select list.Value).FirstOrDefault());


            decimal serviciodeCompra = 0;

            serviciodeCompra = (((productValue * qty) * (PorRecSobreCostoArticulo / 100)) < ValMinSerCompRec) ? ValMinSerCompRec : ((productValue * qty) * (PorRecSobreCostoArticulo / 100));

            reward = (Weight * CostoRecPorKilo) + serviciodeCompra;

            return reward;

        }


        public static decimal getDeliveryPrice(decimal productValue, decimal Weight,  int qty)
        {
            decimal deliveryPrice = 0;

            List<App_Con> lstSetting = DatabaseHelper.getConfiguration(App.Os_Folder);

            decimal serviciodeCompra = 0;

           
          
            decimal PorSobreCostoArticulo = decimal.Parse((from list in lstSetting where list.Name == "PorSobreCostoArticulo" select list.Value).FirstOrDefault());
            decimal CostoPorKilo = decimal.Parse((from list in lstSetting where list.Name == "CostoPorKilo" select list.Value).FirstOrDefault());
            decimal ValMinSerCompra = decimal.Parse((from list in lstSetting where list.Name == "ValMinSerCompra" select list.Value).FirstOrDefault());


            //serviciodeCompra = ((productValue * RecoxValorArt / 100) < ValMinSerCompra) ? ValMinSerCompra : (productValue * RecoxValorArt) / 100;

            serviciodeCompra = (((productValue * qty) * (PorSobreCostoArticulo / 100)) < ValMinSerCompra) ? ValMinSerCompra : ((productValue * qty) * (PorSobreCostoArticulo / 100));

            deliveryPrice = (Weight * CostoPorKilo)  + serviciodeCompra;

            return deliveryPrice;
        }
    }
}
