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

                model.RoomTypes = _context.RoomTypes.ToList();
                model.Room = _context.Rooms.Include(r => r.RoomType).ToList();
                return View("RoomView", model);
            }

            var room = new Room
            {
                RoomTypeId = model.NewRoom.RoomTypeId,
                room_floor = model.NewRoom.room_floor,
                room_number = model.NewRoom.room_number
            };

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return RedirectToAction("RoomView");
        }

    }
}
