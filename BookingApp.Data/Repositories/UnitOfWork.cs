using BookingApp.Core.Repositories;
using BookingApp.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;
        public IGymClassRepository GymClassRepository { get; }
        public IApplicationUserGymRepository UserGymRepository { get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            this.db = db;
            GymClassRepository = new GymClassRepository(db);
            UserGymRepository = new ApplicationUserGymRepository(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
