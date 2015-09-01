using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ExperimentApplication.Models;

namespace ExperimentApplication.Repositories
{
    public class CategoryRepository
    {
        private readonly IExperimentContextInterface _context;

        public CategoryRepository(IExperimentContextInterface context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
