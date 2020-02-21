using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using com.ws.cvxpress.Classes;
using com.ws.cvxpress.Models;
using SQLite;

namespace com.ws.cvxpress.Helpers
{
    public class DatabaseHelper
    {
        public DatabaseHelper()
        {
        }

        #region DB Basics

        // Insert by reference any table/object
        public static bool Insert<T>(ref T item, string db_path)
        {

            db_path = ReturnConnection(db_path);

            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(db_path))
            {

                try
                {
                    connection.CreateTable<T>();

                    if (connection.Insert(item) > 0)
                        return true;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }

            }

            return false;


        }

        public static Profile GetProfile(string db_path)
        {
            Profile objProfile = new Profile();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    objProfile = connection.Table<Profile>().First();
                }
                catch (Exception ex)
                {
                    connection.CreateTable<Profile>();
                    objProfile = null;


                }


            }


            return objProfile;
        }

        internal static void UpdateProfile(string os_Folder, Profile profile)
        {
            throw new NotImplementedException();
        }

        internal static List<App_Con> getConfiguration(string db_path)
        {
            List<App_Con> lstConf = new List<App_Con>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    lstConf = connection.Table<App_Con>().ToList();
                }
                catch (Exception ex)
                {
                    try
                    {

                        connection.CreateTable<App_Con>().GetType();
                        lstConf = null;
                    
                    }
                    catch(Exception exx)
                    {

                    }

                }


            }


            return lstConf;
        }

        internal static bool getUserPhotos()
        {
            return false;
        }

        internal static bool getUserServices()
        {
            List<User_DeliveryTypes> LD = geDeliveryTypes(App.Os_Folder);
            List<User_Types> LT = getTypes(App.Os_Folder);
            List<User_Categories> LC = getCategories(App.Os_Folder);
            if (LD != null && LT != null && LC != null)
            {
                if (LD.Count != 0 && LT.Count != 0 && LC.Count != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Delete<T>(ref T item, string db_path, string table , string strWhere)
        {

            db_path = ReturnConnection(db_path);

            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(db_path))
            {

                try
                {
                    if (connection.Execute(string.Concat("DELETE FROM ", table, strWhere)) > 0)
                        return true;
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }

            }

            return false;


        }

        public static int Update<T>(ref T item, string db_path)
        {
            int updated = 0;
            db_path = ReturnConnection(db_path);

            using (SQLite.SQLiteConnection connection = new SQLite.SQLiteConnection(db_path))
            {

                try
                {
                    if (connection.Update(item) > 0)
                    {
                        updated = 1;
                        return updated;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.ToString());
                }

            }

            return updated;


        }
        #endregion

        #region Related to user



        public static List<User_Categories> getCategories(string db_path)
        {

            List<User_Categories> lstConf = new List<User_Categories>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    lstConf  = connection.Table<User_Categories>().ToList();
                }
                catch (Exception ex)
                {
                    connection.CreateTable<User_Categories>();
                    lstConf = null;


                }


            }


            return lstConf;
        }

        public static List<User_Types> getTypes(string db_path)
        {

            List<User_Types> lstConf = new List<User_Types>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    lstConf = connection.Table<User_Types>().ToList();
                }
                catch (Exception ex)
                {
                    connection.CreateTable<User_Types>();
                    lstConf = null;


                }


            }


            return lstConf;
        }

        internal static List<CollectTypes> geCollectTypesApp(string db_path)
        {
            List<CollectTypes> lstConf = new List<CollectTypes>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    lstConf = connection.Table<CollectTypes>().ToList();
                }
                catch (Exception ex)
                {
                    connection.CreateTable<CollectTypes>();
                    lstConf = null;


                }


            }


            return lstConf;
        }

        public static List<User_DeliveryTypes> geDeliveryTypes(string db_path)
        {

            List<User_DeliveryTypes> lstConf = new List<User_DeliveryTypes>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    lstConf = connection.Table<User_DeliveryTypes>().ToList();
                }
                catch (Exception ex)
                {
                    connection.CreateTable<User_DeliveryTypes>();
                    lstConf = null;


                }


            }


            return lstConf;
        }


        /// <summary>
        /// Category Catalog
        /// </summary>
        /// <returns>The categories app.</returns>
        /// <param name="db_path">Db path.</param>

        public static List<Categories> getCategoriesApp(string db_path, int sWhere)
        {

            List<Categories> lstConf = new List<Categories>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    if(sWhere != 0) 
                    {
                        lstConf = connection.Table<Categories>().Where(x => x.Type_Id == sWhere ).ToList();
                    } else 
                    { 
                        lstConf = connection.Table<Categories>().ToList();
                    }
                }
                catch (Exception ex)
                {
                    connection.CreateTable<Categories>();
                    lstConf = null;


                }


            }


            return lstConf;
        }

        public static List<Types> getTypesApp(string db_path)
        {

            List<Types> lstConf = new List<Types>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    lstConf = connection.Table<Types>().ToList();

                }
                catch (Exception ex)
                {
                    connection.CreateTable<Types>();
                    lstConf = null;


                }


            }


            return lstConf;
        }

        public static List<DeliveryTypes> geDeliveryTypesApp(string db_path)
        {

            List<DeliveryTypes> lstConf = new List<DeliveryTypes>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    lstConf = connection.Table<DeliveryTypes>().ToList();
                }
                catch (Exception ex)
                {
                    connection.CreateTable<DeliveryTypes>();
                    lstConf = null;


                }


            }


            return lstConf;
        }

        //public static int Logout(string db_path)
        //{
        //    int userInfo = new int();
        //    db_path = ReturnConnection(db_path);
        //    using (SQLiteConnection connection = new SQLiteConnection(db_path))
        //    {


        //        try
        //        {
        //            userInfo = connection.Table<Users>().Delete();
        //        }
        //        catch (Exception ex)
        //        {






        //        }


        //    }


        //    return userInfo;
        //}


        #endregion

        #region Related to Catalogs

        internal static ObservableCollection<Countries> getCountries(string db_path)
        {
            List<Countries> lstCon = new List<Countries>();
            ObservableCollection<Countries> countries = new ObservableCollection<Countries>();
            db_path = ReturnConnection(db_path);
            using (SQLiteConnection connection = new SQLiteConnection(db_path))
            {


                try
                {
                    lstCon = connection.Table<Countries>().ToList();

                    lstCon.ForEach(x => countries.Add(x));


                }
                catch (Exception ex)
                {
                    connection.CreateTable<Countries>();
                    lstCon = null;


                }


            }


            return countries;
        }




        #endregion

        #region Get connection for iOS or Android
        // Returns the connection for Android or iOS depends of the folder value sent to the method
        public static string ReturnConnection(string OS_Folder)
        {

            string CompleteRoute = "";
            string FileName = Constants.DatabaseName;

            string RouteFolder = "";
            switch (OS_Folder)
            {

                case Constants.iOS_db_folder:

                    RouteFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", Constants.iOS_db_folder);
                    // RouteFolder = "/Users/gabrielasanchez/Library/";
                    break;

                case Constants.Android_db_folder:
                    RouteFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    break;
            }

            CompleteRoute = Path.Combine(RouteFolder, FileName);

            return CompleteRoute;

        }


        #endregion

    }
}
