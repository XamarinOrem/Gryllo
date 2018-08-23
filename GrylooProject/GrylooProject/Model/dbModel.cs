using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrylooProject.Model
{
    public class loggedInUser
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string userId { get; set; }

        public string sessionId { get; set; }

        public string mobile { get; set; }
        
        public int userType { get; set; }
        public bool lifeLine { get; set; }
    }

    public class LoginDetails
    {
        public static string userId { get; set; }


        public static string sessionId { get; set; }
        public static string mobile { get; set; }

        public static int userType { get; set; }
        public static bool lifeLine { get; set; }

    }
}
