using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class RoomRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public RoomType? RoomType { get; set; }
        public String? Name { get; set; }
        public int? RoomTypeId { get; set; } //foreign key
        public decimal? Base_price { get; set; }
    }
}
