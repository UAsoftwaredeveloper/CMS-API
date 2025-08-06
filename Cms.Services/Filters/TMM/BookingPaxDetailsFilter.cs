using System;

namespace Cms.Services.Filters.TMM
{
    public class BookingPaxDetailsFilter:CommonFilter
    {
        public long? TransactionId { get; set; }
        public string PassengerType { get; set; }
        public string Title { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public DateTime? DOBFrom { get; set; }
        public DateTime? DOBTo { get; set; }
    }
}
