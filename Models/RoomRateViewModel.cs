namespace HotelManagement.Models
{
    public class RoomRateViewModel
    {
        public RoomRate? NewRoomRate {  get; set; }
        public RoomRate? EditRoomRate { get; set; }
        public List<RoomRate>? RoomRates { get; set; }
        public List<RoomType>? RoomTypes { get; set; }
    }
}
