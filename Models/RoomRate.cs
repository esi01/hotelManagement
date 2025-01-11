namespace HotelManagement.Models
{
    public class RoomRate 
    {
        public int Id { get; set; }
        public int Emer { get; set; }
        public decimal Cmimi_baze { get; set; } 
        public bool IsBedAndBreakfast { get; set; }
        public decimal WeekendSurcharge { get; set; } 
        public decimal HolidaySurcharge { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RateType { get; set; }
        public virtual RoomType RoomType { get; set; }
        public decimal Price { get; set; }


        public decimal GetDynamicPrice(DateTime checkInDate)
        {
            decimal finalPrice = Cmimi_baze;

            if (checkInDate.DayOfWeek == DayOfWeek.Saturday || checkInDate.DayOfWeek == DayOfWeek.Sunday)
            {
                finalPrice += WeekendSurcharge;
            }

            if (IsHoliday(checkInDate))
            {
                finalPrice += HolidaySurcharge;
            }

            return finalPrice;
        }

        private bool IsHoliday(DateTime date)
        {
            var holidays = new List<DateTime>
            {
                new DateTime(date.Year, 12, 25),  
                new DateTime(date.Year, 1, 1),
                new DateTime(date.Year, 3,14),  
                new DateTime(date.Year, 11, 28),  
                new DateTime(date.Year, 11, 29)
            };

            return holidays.Contains(date.Date);
        }
    }
}
