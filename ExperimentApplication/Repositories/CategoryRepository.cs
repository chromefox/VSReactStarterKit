using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ExperimentApplication.Classes;
using ExperimentApplication.Models;

namespace ExperimentApplication.Repositories
{
    public interface ICategoryRepository : IRepository<Category, long>
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }

    public class CategoryRepository : BaseRepository<Category, long>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public IEnumerable<Category> GetAll()
        {
            return DbSet.ToList();
        }
    }
}