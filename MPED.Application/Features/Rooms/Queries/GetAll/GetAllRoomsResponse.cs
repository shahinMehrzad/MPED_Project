using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Application.Features.Rooms.Queries.GetAll
{
    public class GetAllRoomsResponse
    {
        public int Id { get; set; }
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
    }
}
