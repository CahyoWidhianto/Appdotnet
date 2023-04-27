using Latihan.Models;

namespace Latihan.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRace();
        Task<List<Club>> GetAllUserClub();
    }
}
