using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace YOUtility.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the YOUtilityUser class
    public class YOUtilityUser : IdentityUser
    {

        public string BillIds { get; set; }
        public string Address { get; set; }

        public string ProfilePic { get; set; }
        public string Biography { get; set; }
        public string Achievements { get; set; }
        public string Goals { get; set; }
        public DateTime JoinDate { get; set; }

        public int residents { get; set; }
        public int feet { get; set; }
        public bool garden { get; set; }
        public bool lawn { get; set; }
        public bool washer { get; set; }
        public bool led { get; set; }
        public bool fireplace { get; set; }
        public bool trees { get; set; }
    }
}