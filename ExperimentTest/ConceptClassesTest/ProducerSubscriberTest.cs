using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentApplication.ConceptClasses.QueueConcept;
using Xunit;

namespace ExperimentTest.ConceptClassesTest
{
    public class ProducerSubscriberTest
    {
        [Fact]
        public void BasicUsageTest()
        {
            // act as main
            var main = new ProdSubDummy();
            main.StartTest();
        }
    }
}
