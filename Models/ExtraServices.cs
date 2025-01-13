using HotelManagement.Models;

namespace HotelManagement.Models;
public class ExtraService
{
    public int Id { get; set; }
    public string? ServiceName { get; set; }
    public decimal ServiceCharge { get; set; } 
    public int BillId { get; set; }  
    public virtual Bill Bill { get; set; }  
}
