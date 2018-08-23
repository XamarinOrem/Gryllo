using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrylooProject.Model;

namespace GrylooProject.Data
{
    public  class DBgrylloo
    {

        /// <summary>
        /// Declaratipn part
        /// </summary>
        readonly SQLiteConnection database;


        /// <summary>
        /// Constructor part
        /// </summary>
        /// <param name="dbPath"></param>
        public DBgrylloo(string dbPath)
        {
            try
            {
                database = new SQLiteConnection(dbPath);
                database.CreateTable<loggedInUser>();

            }

            catch (Exception ex)
            {

            }



        }




        public bool GetLoginUser(out loggedInUser user)
        {
            user = new loggedInUser();
            bool isLogin = false;
            try
            {
                user = database.Table<loggedInUser>().First();
                if (user != null)
                {
                    isLogin = true;
                }
            }
            catch (Exception ex)
            {
                isLogin = false;
            }
            return isLogin;
        }


        public int SaveLoggedUser(loggedInUser objLoggedUser)
        {
            int status = 0;
            try
            {
                //database.DeleteAll<loggedInUser>();
                status = database.Insert(objLoggedUser);
            }
            catch (Exception ex)
            {
                status = 0;
            }
            return status;
        }

        public int UpdateLoggedUser(loggedInUser objLoggedUser)
        {
            int status = 0;
            try
            {
                status = database.Update(objLoggedUser);
            }
            catch (Exception ex)
            {
                status = 0;
            }
            return status;
        }

    }
}
