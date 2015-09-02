using System.Collections.Generic;
using System.Data.Entity;
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
    }
}