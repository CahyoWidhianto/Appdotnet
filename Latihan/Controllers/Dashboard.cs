using Latihan.Data;
using Latihan.Interfaces;
using Latihan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Latihan.Controllers
{
    public class Dashboard : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public Dashboard(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task <IActionResult> Index()
        {
            var userRace = await _dashboardRepository.GetAllUserRace();
            var userClub = await _dashboardRepository.GetAllUserClub();
            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRace,
                Clubs = userClub,
            };
            return View(dashboardViewModel);
        }
    }
}
