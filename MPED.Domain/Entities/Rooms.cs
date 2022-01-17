using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPED.Domain.Entities
{
    public class Rooms : BaseEntity
    {
        public string RoomNumber { get; set; }
        public bool AirConditioning { get; set; }
        public bool WiFi { get; set; }
        public bool Hairdryer { get; set; }
        public bool Television { get; set; }
        public bool SeaView { get; set; }
        public int RoomArea { get; set; }
        public int TwinBed { get; set; }
        public int SingleBed { get; set; }
        public int ExtraSingleBed { get; set; }
        [NotMapped]
        public virtual List<BookingRoom> BookingRooms { get; set; }
    }
}
