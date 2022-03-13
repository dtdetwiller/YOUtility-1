using System;
using YOUtility.Models;

namespace YOUtility.Models
{
    // This model represents a single utility bill.
    public class Bill
    {
        public enum Type
        {
            water, gas, electricity
        }
        // Primary DB Key
        public int ID { get; set; }
        // Bill Type
        public Type utility { get; set; }
        // Balance Payed
        public double due { get; set; }
        // Amount of Utility Consumed
        public double consumed { get; set; }
        // When Bill was Uploaded
        public DateTime uploadTime { get; set; }
        // Billing Period
        public DateTime beginningPeriod { get; set; }
        public DateTime endingPeriod { get; set; }
    }
}
