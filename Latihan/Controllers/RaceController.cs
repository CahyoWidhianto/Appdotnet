using Latihan.Data;
using Latihan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Controllers
{
    public class RaceController : Controller
    {
        private readonly AplicationDbContext _context;
        public RaceController(AplicationDbContext context) 
        { 
            _context = context;
        }
        public IActionResult Index()
        {
            List<Race> races = _context.Races.ToList();
            return View(races);
        }

        public IActionResult Detail(int id)
        {
            Race race = _context.Races.Include(a => a.Address).FirstOrDefault(r => r.Id == id);
            return View(race);
        }
    }
}
