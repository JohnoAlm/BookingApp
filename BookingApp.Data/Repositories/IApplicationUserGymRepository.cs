using BookingApp.Core.Entitis;

namespace BookingApp.Data.Repositories
{
    public interface IApplicationUserGymRepository
    {
        void Add(ApplicationUserGymClass booking);
        Task<ApplicationUserGymClass> FindAsync(string userId, int gymClassId);
        void Remove(ApplicationUserGymClass attending);
    }
}