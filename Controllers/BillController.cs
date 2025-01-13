using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HotelManagement.Controllers
{
    public class BillController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public BillController(HotelManagementDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bills = _context.Bills.ToList();
            return View(bills);
        }

        public IActionResult Create(int reservationId)
        {
            var bill = new Bill
            {
                ReservationId = reservationId
            };
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bill.CalculateTotalAmount();
                    _context.Bills.Add(bill);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error occurred while saving the bill.");
                }
            }
            return View(bill);
        }

        public IActionResult Edit(int id)
        {
            var bill = _context.Bills.Find(id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bill bill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bill.CalculateTotalAmount();
                    _context.Bills.Update(bill);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error occurred while updating the bill.");
                }
            }
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var bill = _context.Bills.Find(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
