using Latihan.Data;
using Latihan.Interfaces;
using Latihan.Models;

namespace Latihan.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(AplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClub()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userClub = _context.Clubs.Where(r => r.AppUser.Id == curUser.ToString());
            return userClub.ToList();
        }

        public async Task<List<Race>> GetAllUserRace()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userRace = _context.Races.Where(r => r.AppUser.Id == curUser.ToString());
            return userRace.ToList();
        }
    }
}
