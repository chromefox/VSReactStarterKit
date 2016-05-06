using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApplication.ConceptClasses.Events
{
    public class TestEventsMethod
    {
        public void StartTest()
        {
            var newCrimeWatch = new CrimeWatch();
            // add polices with custom behaviour
            var anne = new Police("Anne", newCrimeWatch, new GoodBehaviour());
            var victor = new Police("Victor", newCrimeWatch, new BadBehaviour());
            var max = new Police("Mad Max", newCrimeWatch, new ModerateBehaviour());
            newCrimeWatch.StartCrimeWatch();
        }
    }
}
