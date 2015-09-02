using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExperimentApplication.Models;
using ExperimentApplication.Repositories;
using Ploeh.AutoFixture;

namespace ExperimentApplication
{
    public class DatabaseService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IUserRepository _userRepo;

        public DatabaseService(ICategoryRepository categoryRepo, IUserRepository userRepo)
        {
            _categoryRepo = categoryRepo;
            _userRepo = userRepo;
        }

        public void CreateSampleCategory()
        {
            _categoryRepo.Create(new Category("Random string..."));
            _categoryRepo.SaveChanges();
        }

        public void InitialSetup()
        {
            // Setup all the possible data relationship in one shot.
            var user = new User("John Doe");
            var travelList = new Listing("Travel");
            var localFoodList = new Listing("Local Food");
            var favFoodList = new Listing("Favorite Food");

            var foodCategory = new Category("Food");
            var country = new Category("Country");

            var singapore = new Item("Singapore", country);
            var food1 = new Item("food1", foodCategory);
            var food2 = new Item("food2", foodCategory);
            var food3 = new Item("food3", foodCategory);

            food1.Galleries = new List<Gallery>() { new Gallery("http://somehwere.com"), new Gallery("http://somehwere2.com") };

            travelList.ListingItems = new List<ListingItem>() { new ListingItem(travelList, singapore) };
            localFoodList.ListingItems = new List<ListingItem>() { new ListingItem(localFoodList, food1), new ListingItem(localFoodList, food2), new ListingItem(localFoodList, food3) };
            favFoodList.ListingItems = new List<ListingItem>() { new ListingItem(favFoodList, food2) };

            user.ListingItems = new List<Listing>() { travelList, localFoodList, favFoodList };

            _userRepo.Create(user);
            _userRepo.SaveChanges();
        }

        public void TestUserCascadeDelete()
        {
            var johnDoeUser = _userRepo.DbSet.SingleOrDefault(s => s.Name.Equals("John Doe"));
            _userRepo.Delete(johnDoeUser);
            _userRepo.SaveChanges();
        }
    }
}