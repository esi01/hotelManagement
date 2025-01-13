using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HotelManagement.Controllers
{
    public class ExtraServiceController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public ExtraServiceController(HotelManagementDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var extraServices = _context.ExtraServices.ToList();
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExtraService extraService)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.ExtraServices.Add(extraService);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error occurred while saving the extra service.");
                }
            }
            return View(extraService);
        }

        public IActionResult Edit(int id)
        {
            //var extraService = _context.ExtraServices.Find(id);
            //if (extraService == null)
            //{
            //    return NotFound();
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExtraService extraService)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_context.ExtraServices.Update(extraService);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Error occurred while updating the extra service.");
                }
            }
            return View(extraService);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            //var extraService = _context.ExtraServices.Find(id);
            //if (extraService != null)
            //{
            //    _context.ExtraServices.Remove(extraService);
            //    _context.SaveChanges();
            //}
            return RedirectToAction("Index");
        }
    }
}
