using BookingApp.Core.Entitis;
using System.Diagnostics.CodeAnalysis;

namespace BookingApp.Core.Repositories
{
    public interface IApplicationUserGymRepository
    {
        void Add(ApplicationUserGymClass booking);

        [return: MaybeNull]
        Task<ApplicationUserGymClass?> FindAsync(string userId, int gymClassId);
        void Remove(ApplicationUserGymClass attending);
    }
}