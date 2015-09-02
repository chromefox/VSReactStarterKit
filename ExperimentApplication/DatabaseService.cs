using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentApplication.Models;
using ExperimentApplication.Repositories;
using Ploeh.AutoFixture;

namespace ExperimentApplication
{
    public class DatabaseService
    {
        private ICategoryRepository _categoryRepo;

        public DatabaseService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public void CreateSampleCategory()
        {
            _categoryRepo.Create(new Category("Random string..."));
            _categoryRepo.SaveChanges();
        }
    }
}
