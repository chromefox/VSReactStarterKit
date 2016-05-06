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
            var anne = new Police(newCrimeWatch, new GoodBehaviour());
            var victor = new Police(newCrimeWatch, new BadBehaviour());
            var max = new Police(newCrimeWatch, new ModerateBehaviour());
            newCrimeWatch.StartCrimeWatch();


        }
    }
}
