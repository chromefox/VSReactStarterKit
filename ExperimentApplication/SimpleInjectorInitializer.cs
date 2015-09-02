using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentApplication.Models;
using ExperimentApplication.Repositories;
using SimpleInjector;

namespace ExperimentApplication
{
    public static class SimpleInjectorInitializer
    {
        public static SimpleInjector.Container Init()
        {
            var container = new SimpleInjector.Container();
            Register(container);
            container.Verify();
            return container;
        }

        private static void Register(SimpleInjector.Container container)
        {
            var registration = Lifestyle.Singleton.CreateRegistration<ExperimentContext>(container);
            container.AddRegistration(typeof(DbContext), registration);

            container.Register<ICategoryRepository, CategoryRepository>(Lifestyle.Singleton);
            container.Register<DatabaseService>(Lifestyle.Singleton);
        }
    }
}
