using BookingApp.Core.Entitis;

namespace BookingApp.Core.Repositories
{
    public interface IGymClassRepository
    {
        Task<IEnumerable<GymClass>> GetAsync();
        Task<IEnumerable<GymClass>> GetWithAttendinAsync();
        Task<IEnumerable<GymClass>> GetHistoryAsync();
        Task<GymClass?> GetAsync(int? id);
        void Add(GymClass gymclass);
    }


    //public class MockGymClassRepo : IGymClassRepository
    //{
    //    public void Add(GymClass gymclass)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<GymClass>> GetAsync()
    //    {
            
    //    }

    //    public Task<GymClass?> GetAsync(int? id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<GymClass>> GetHistoryAsync()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<GymClass>> GetWithAttendinAsync()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

}