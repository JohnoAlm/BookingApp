namespace BookingApp.Data.Repositories
{
    public interface IUnitOfWork
    {
        IGymClassRepository GymClassRepository { get; }
        IApplicationUserGymRepository UserGymRepository { get; }

        Task CompleteAsync();
    }
}