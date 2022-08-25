using BookingApp.Core.Entitis;

namespace BookingApp.Core.Repositories
{
    public interface IApplicationUserGymRepository
    {
        void Add(ApplicationUserGymClass booking);
        Task<ApplicationUserGymClass> FindAsync(string userId, int gymClassId);
        void Remove(ApplicationUserGymClass attending);
    }
}