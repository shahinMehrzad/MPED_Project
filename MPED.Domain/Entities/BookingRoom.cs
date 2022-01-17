using System;
using System.Collections.Generic;

namespace MPED.Domain.Entities
{
    public class BookingRoom : BaseEntity
    {
        public int RoomId { get; set; }
        public virtual Rooms Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestNumber { get; set; }
    }
}
