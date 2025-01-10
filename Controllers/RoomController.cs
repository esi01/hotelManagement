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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoom(Room editRoom)
        {
            if (ModelState.IsValid)
            {
                var room = _context.Rooms.Include(r => r.RoomType).FirstOrDefault(r => r.Id == editRoom.Id);

                if (room != null)
                {
                    room.room_floor = editRoom.room_floor;
                    room.room_number = editRoom.room_number;

                    
                    var roomType = _context.RoomTypes.FirstOrDefault(rt => rt.Id == editRoom.RoomType.Id);
                    if (roomType != null)
                    {
                        room.RoomType = roomType;
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction("RoomView");
            }

            var model = new RoomViewModel
            {
                Room = _context.Rooms.Include(r => r.RoomType).ToList(),
                RoomTypes = _context.RoomTypes.ToList(),
                EditRoom = editRoom
            };

            return View("RoomView", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            return RedirectToAction("RoomView");
        }

    }
}
