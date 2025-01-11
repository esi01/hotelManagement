using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models
{
    public class HotelManagementDbContext : DbContext
    {
        public DbSet <RoomType> RoomTypes { get; set; }
        public DbSet <Room> Rooms { get; set; }
        public DbSet<RoomRate> RoomRates { get; set; }  
        public DbSet<ExtraService> ExtraServices { get; set; } 
        public DbSet<Bill> Bills { get; set; }
        


        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options) : base(options) 
        { 

        }
    }
}
