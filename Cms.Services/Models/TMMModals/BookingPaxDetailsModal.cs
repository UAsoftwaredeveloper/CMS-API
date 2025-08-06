using System;

namespace Cms.Services.Models.TMMModals
{
    public class BookingPaxDetailsModal
    {
        public long Id { get; set; }
        public long? TransactionId { get; set; }
        public int? PaxOrder { get; set; }
        public string PassengerType { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string ETicketNumber { get; set; }
        public string TicketStatus { get; set; }
        public DateTime? DOB { get; set; }
    }
}
