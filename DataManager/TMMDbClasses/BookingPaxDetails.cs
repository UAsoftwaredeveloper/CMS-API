using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.TMMDbClasses
{
    public class BookingPaxDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey(nameof(BookingTransactionDetails))]
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
        public BookingTransactionDetails BookingTransactionDetails { get; set; }
    }
}
