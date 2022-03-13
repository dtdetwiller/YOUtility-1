using System;
using YOUtility.Models;

namespace YOUtility.Models
{
    // This model represents a single utility bill.
    public class Friendship
    {
        // Primary DB Key
        public int ID { get; set; }

        // UserID of initiator of Friendship
        public string friend1 { get; set; }

        // UserID of reciever
        public string friend2 { get; set; }

        // Whether or not the friendship request has been accepted
        public bool accepted { get; set; }
    }
}
