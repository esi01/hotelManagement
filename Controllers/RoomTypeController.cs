using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System.Linq;

namespace HotelManagement.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public RoomTypeController(HotelManagementDbContext context)
        {
            _context = context;
        }

        public IActionResult RoomTypeView()
        {
            var roomTypes = _context.RoomTypes.ToList();
            var model = new RoomTypeViewModel
            {
                RoomTypes = roomTypes,
                NewRoomType = new RoomType(), 
                EditRoomType = new RoomType() 
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoomType([FromForm] RoomTypeViewModel model)  
        {

            if (model?.NewRoomType != null)
            {
                try
                {
                    _context.RoomTypes.Add(model.NewRoomType);
                    _context.SaveChanges();
                    return RedirectToAction("RoomTypeView");
                }
                catch (Exception )
                {
                    return RedirectToAction("RoomTypeView");
                }
            }

            model = model ?? new RoomTypeViewModel();
            model.RoomTypes = _context.RoomTypes.ToList();
            return View("RoomTypeView", model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoomType(RoomTypeViewModel model)
        {
            //System.Diagnostics.Debug.WriteLine($"EditRoomType action hit with id: {model?.EditRoomType?.Id}");

            if (model?.EditRoomType != null)
            {
                try
                {
                    var roomType = _context.RoomTypes.Find(model.EditRoomType.Id);
                    if (roomType != null)
                    {
                        roomType.Emer = model.EditRoomType.Emer;
                        roomType.Siperfaqe = model.EditRoomType.Siperfaqe;
                        roomType.Pershkrim = model.EditRoomType.Pershkrim;
                        roomType.Kapacitet = model.EditRoomType.Kapacitet;

                        _context.SaveChanges();
                        return RedirectToAction("RoomTypeView");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error saving: {ex.Message}");
                    ModelState.AddModelError("", "Error saving changes");
                }
            }
            model = model ?? new RoomTypeViewModel();
            model.RoomTypes = _context.RoomTypes.ToList();
            return View("RoomTypeView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRoomType(int id)
        {

            var associations = new Dictionary<string, bool>
            {
                { "Rooms", _context.Rooms.Any(r => r.RoomTypeId == id) },
                { "RoomRates", _context.RoomRates.Any(rr => rr.RoomTypeId == id) }
            };

            
            var associatedEntity = associations.FirstOrDefault(a => a.Value).Key;

            if (!string.IsNullOrEmpty(associatedEntity))
            {
                TempData["ErrorMessage"] = $"Cannot delete this room type as it is associated with existing {associatedEntity}.";
                return RedirectToAction("RoomTypeView");
            }

            var roomType = _context.RoomTypes.Find(id);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
                _context.SaveChanges();
            }

            return RedirectToAction("RoomTypeView");
        }

    }
}
