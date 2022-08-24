using BookingApp.Core.Entitis;

namespace BookingApp.Data.Repositories
{
    public interface IGymClassRepository
    {
        Task<List<GymClass>> GetAsync();
    }
}