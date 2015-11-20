using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentApplication.Models;

namespace ExperimentApplication.Classes
{
    public class ConceptualClarifications
    {
        public static void TestRefConceptDateTime()
        {
            var startDate = new DateTime(2000, 1, 1);
            ModifyDate(ref startDate);
            Console.WriteLine(startDate.ToLongDateString());
        }

        public static void TestRefConceptObject()
        {
            var newUser = new User("New User");
            ModifyUser(newUser);
            Console.WriteLine(newUser.Name);
        }

        private static void ModifyDate(ref DateTime date)
        {
            date = new DateTime(2010, 1, 1);
        }

        private static void ModifyUser(User user)
        {
            user = new User("Hola");
        }
    }
}