using System;

namespace MPED.Domain.Entities
{
    public class BookingRoom : BaseEntity
    {
        public Rooms Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestNumber { get; set; }
    }
}
