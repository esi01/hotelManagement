using HotelManagement.Models;

namespace HotelManagement.Models;

public class RoomRateViewModel
{
    public int Id { get; set; }
    public RoomViewModel Room { get; set; }
    public RoomTypeViewModel RoomType { get; set; }
    public decimal Rate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
