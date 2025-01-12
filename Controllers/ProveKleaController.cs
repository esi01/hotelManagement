using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    public class ProveKleaController : Controller
    {
        private readonly HotelManagementDbContext _context;

        public ProveKleaController(HotelManagementDbContext context)
        {
            _context = context;
        }
        public IActionResult ProveKlea()
        {
            return View();
        }


    }
}
