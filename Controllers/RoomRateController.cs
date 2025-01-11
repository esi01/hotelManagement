using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HotelManagement.Controllers
{
    public class RoomRateController : Controller
    {
        private readonly HotelManagementDbContext _context;
        


        public RoomRateController(HotelManagementDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            var roomRates = _context.RoomRates.ToList();
            return View(roomRates);
        }

        
        public IActionResult Create()
        {
            var model = new RoomRate
            {
                RateType = "Only Bed"  
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoomRate roomRate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    roomRate.Cmimi_baze = CalculatePrice(roomRate);

                    _context.RoomRates.Add(roomRate);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error occurred while saving the room rate.");
                }
            }
            return View(roomRate);
        }

        public IActionResult Edit(int id)
        {
            var roomRate = _context.RoomRates.Find(id);
            if (roomRate == null)
            {
                return NotFound();
            }
            return View(roomRate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoomRate roomRate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    roomRate.Cmimi_baze = CalculatePrice(roomRate);

                    _context.RoomRates.Update(roomRate);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error occurred while updating the room rate.");
                }
            }
            return View(roomRate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var roomRate = _context.RoomRates.Find(id);
            if (roomRate != null)
            {
                _context.RoomRates.Remove(roomRate);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private decimal CalculatePrice(RoomRate roomRate)
        {
            decimal basePrice = roomRate.Cmimi_baze;

            if (roomRate.RateType == "Bed & Breakfast")
            {
                basePrice += 10; 
            }

            if (IsWeekend(roomRate.StartDate) || IsHoliday(roomRate.StartDate))
            {
                basePrice += 20;  
            }

            return basePrice;
        }

        
        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        private bool IsHoliday(DateTime date)
        {
            var holidays = new DateTime[] {
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
