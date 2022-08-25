using BookingApp.Core.Entitis;

namespace BookingApp.Core.Repositories
{
    public interface IGymClassRepository
    {
        Task<IEnumerable<GymClass>> GetAsync();
    }
}