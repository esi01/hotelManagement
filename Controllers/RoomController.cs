using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    public class RoomController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public RoomController(HotelManagementDbContext context)
        {
            _context = context;
        }
        public IActionResult RoomView()
        {
            var rooms = _context.Rooms.Include(r => r.RoomType).ToList(); 
            var roomTypes = _context.RoomTypes.ToList();
            

            var model = new RoomViewModel
            {
                Room = rooms,
                NewRoom = new Room(),
                RoomTypes = roomTypes ,
                EditRoom = new Room()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoom([FromForm] RoomViewModel model)
        {
            System.Diagnostics.Debug.WriteLine($"RoomTypeId: {model.NewRoom.RoomTypeId}");
            System.Diagnostics.Debug.WriteLine($"RoomFloor: {model.NewRoom.room_floor}");
            System.Diagnostics.Debug.WriteLine($"RoomNumber: {model.NewRoom.room_number}");
            if (!ModelState.IsValid)
            {
                
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Validation error on {key}: {error.ErrorMessage}");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                model.NewRoom.RoomType = _context.RoomTypes
                    .FirstOrDefault(r => r.Id == model.NewRoom.RoomTypeId);

                _context.Rooms.Add(model.NewRoom);
                _context.SaveChanges();

                return RedirectToAction("RoomView");
            }
            model.Room = _context.Rooms.Include(r => r.RoomType).ToList();
            model.RoomTypes = _context.RoomTypes.ToList();
            return View("RoomView", model);
        }
    }
}
