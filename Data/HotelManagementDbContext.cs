using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models
{
    public class HotelManagementDbContext : DbContext
    {
        public DbSet <RoomType> RoomTypes { get; set; }

        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options) : base(options) 
        { 

        }
    }
}
