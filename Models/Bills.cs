using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagement.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }  
        public decimal TotalAmount { get; set; }  
        public DateTime DateIssued { get; set; }  

        public virtual ICollection<RoomRate> RoomRates { get; set; }  
        public virtual ICollection<ExtraService> ExtraServices { get; set; } 

        public Bill()
        {
            RoomRates = new List<RoomRate>();
            ExtraServices = new List<ExtraService>();
            DateIssued = DateTime.Now; 
        }

     
        public void CalculateTotalAmount()
        {
           // TotalAmount = RoomRates.Sum(r => r.Cmimi_baze) + ExtraServices.Sum(s => s.ServiceCharge);
        }
    }
}
