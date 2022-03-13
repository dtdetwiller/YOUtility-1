using System;
using System.Collections.Generic;

namespace YOUtility.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string Address { get; set; }

        public string UserName { get; set; }
        public string ProfilePic { get; set; }
        public string Biography { get; set; }
        public string Achievements { get; set; }
        public string Goals { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
