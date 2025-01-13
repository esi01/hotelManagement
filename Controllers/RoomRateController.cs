using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    public class RoomRateController : Controller {
        private readonly HotelManagementDbContext _context;

        public RoomRateController(HotelManagementDbContext context)
        {
            _context = context;
        }
        public IActionResult RoomRateView()
        {
            var roomRates = _context.RoomRates.Include(r => r.RoomType).ToList();
            var roomTypes = _context.RoomTypes.ToList();


            var model = new RoomRateViewModel
            {
                RoomRates = roomRates,
                NewRoomRate = new RoomRate(),
                RoomTypes = roomTypes,
                EditRoomRate = new RoomRate()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoomRate([FromForm] RoomRateViewModel model)
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
                model.RoomRates = _context.RoomRates.Include(r => r.RoomType).ToList();
                return View("RoomRateView", model);
            }

            var roomRate = new RoomRate
            {
                Name = model.NewRoomRate.Name,
                RoomTypeId = model.NewRoomRate.RoomTypeId,
                Base_price = model.NewRoomRate.Base_price,
            };

            _context.RoomRates.Add(roomRate);
            _context.SaveChanges();

            return RedirectToAction("RoomRateView");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoomRate(RoomRate editRoomRate)
        {
            System.Diagnostics.Debug.WriteLine($"EditRoomRate action hit with id: {editRoomRate.Id}");
            if (ModelState.IsValid)
            {

                var roomRate = _context.RoomRates.Include(r => r.RoomType).FirstOrDefault(r => r.Id == editRoomRate.Id);

                if (roomRate != null)
                {
                    roomRate.Name = editRoomRate.Name;
                    roomRate.Base_price = editRoomRate.Base_price;


                    var roomType = _context.RoomTypes.FirstOrDefault(rt => rt.Id == editRoomRate.RoomTypeId);
                    if (roomType != null)
                    {
                        roomRate.RoomType = roomType;
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction("RoomRateView");
            }

            var model = new RoomRateViewModel
            {
                RoomRates = _context.RoomRates.Include(r => r.RoomType).ToList(),
                RoomTypes = _context.RoomTypes.ToList(),
                EditRoomRate = editRoomRate
            };

            return View("RoomRateView", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRoomRate(int id)
        {
            var roomRate = _context.RoomRates.Find(id);
            if (roomRate != null)
            {
                _context.RoomRates.Remove(roomRate);
                _context.SaveChanges();
            }
            return RedirectToAction("RoomRateView");
        }
    }
}
