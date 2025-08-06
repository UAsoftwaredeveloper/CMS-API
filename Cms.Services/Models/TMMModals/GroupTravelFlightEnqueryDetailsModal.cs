using System;

namespace Cms.Services.Models.TMMModals
{
    public class GroupTravelFlightEnqueryDetailsModal
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string ContactNumber { get; set; }
        public string Query { get; set; }
        public string SearchGUID { get; set; }
        public string Depart { get; set; }
        public string Return { get; set; }
        public DateTime DepartDate { get; set; }
        private DateTime? _returnDate;
        public DateTime? ReturnDate
        {
            get => (_returnDate == null || _returnDate == DateTime.MinValue) ? null : _returnDate;
            set => _returnDate = value;
        }
        public string CabinClass { get; set; }
        public int TripType { get; set; }
        public bool IsDirect { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }
        public DateTime SearchDate { get; set; }
        public string IPAddress { get; set; }
        public string DeviceType { get; set; }
        public string UTM_Source { get; set; }
        public int PortalID { get; set; }
        public int InfantInLapCount { get; set; }
        public string Depart1 { get; set; }
        public string Return1 { get; set; }
        public string Depart2 { get; set; }
        public string Return2 { get; set; }
        public string ReferenceId { get; set; }
        public DateTime DepartDate1 { get; set; }

        public DateTime DepartDate2 { get; set; }
        public long CreatedBy { get; set; }
    }
}
