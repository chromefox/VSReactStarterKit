using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExperimentApplication.Classes;
using ExperimentApplication.ConceptClasses.QueueConcept;

namespace ExperimentApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
            New simple model generation:
            1) Nested / circular relationship
            2) Ternary relationship
            3) Using interfaces for repository...
            4) Consider using services for multiple repo...
            5) Figure out how to use TDD to develop.
            */
            //var container = SimpleInjectorInitializer.Init();
            //var databaseContainer = container.GetInstance<DatabaseService>();
            //// databaseContainer.InitialSetup();
            //databaseContainer.TestUserCascadeDelete();


            //var stringToTest = "http://ycs.dev.resolutelabs.com/static/media/asdfasdf/image.jpg";
            //var stringToTest2 = "https://ycs.dev.resolutelabs.com/static/media/kikikik/image2.jpg";
            //// SecurityTestClass.TestSanitization(stringToTest);
            //var host = "ycs.dev.resolutelabs.com/static";
            //var regex = new Regex($"http(s)?://{host}?");
            //Console.WriteLine($"Output is  {regex.Replace(stringToTest, string.Empty)}");
            //Console.WriteLine($"Output is  {regex.Replace(stringToTest2, string.Empty)}");

            Console.Write("Start executing...");

            var main = new ProdSubDummy();
            main.StartTest();

            Console.Write("Finished executing...");
            Console.ReadLine();
        }
    }
}