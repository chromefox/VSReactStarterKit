using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ExperimentApplication.Classes;
using ExperimentApplication.Models;

namespace ExperimentApplication.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {
    }

    public class UserRepository : BaseRepository<User, long>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }

        public IEnumerable<User> GetInactiveUsers()
        {
            var dateTime = SystemTime.Now();
            dateTime = dateTime.AddDays(-2);
            return DbSet.Where(s => s.LastSeen < dateTime);
        }

        public IEnumerable<User> GetInactiveUsers(Func<DateTime> curDate)
        {
            var dateTime = curDate.Invoke();
            dateTime = dateTime.AddDays(-2);
            return DbSet.Where(s => s.LastSeen < dateTime);
        }
    }
}