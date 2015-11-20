using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;

namespace ExperimentApplication.Classes
{
    public class OrderByVm
    {
        public bool TestBool { get; set; }
        public DateTime DateTime { get; set; }
    }

    public static class OrderByTest
    {
        public static void Test()
        {
            // initialize
            var objectList = new Fixture().CreateMany<OrderByVm>(10);
            objectList = objectList.OrderBy(s => s.TestBool).ThenByDescending(s => s.DateTime);
            foreach (var objectItem in objectList)
                Console.WriteLine(@"{0} - {1}", objectItem.TestBool, objectItem.DateTime.ToShortDateString());
        }
    }
}