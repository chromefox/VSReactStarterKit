using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExperimentApplication.ConceptClasses.QueueConcept
{
    /// <summary>
    /// Produces the work item in a random manner
    /// </summary>
    public class DummyProducer
    {
        public static int MaxItem = 10;

        private readonly ConcurrentQueue<WorkItem> _workItemQueue;
        private readonly Random _randomGenerator;

        private int _minInterval = 2;
        private int _maxInterval = 5;

        public DummyProducer(ConcurrentQueue<WorkItem> workItemQueue, Random randomGenerator)
        {
            _workItemQueue = workItemQueue;
            _randomGenerator = randomGenerator;
        }

        // Enqueue work item with random Guid at random interval
        public void StartProducerWork()
        {
            try
            {
                var curWork = 0;
                while (curWork < MaxItem)
                {
                    Console.WriteLine($"Add work: {nameof(curWork)} :{curWork}");
                    AddWork();
                    Thread.Sleep(_randomGenerator.Next(_minInterval, _maxInterval) * 1000);
                    curWork++;
                }

            }
            catch (Exception e)
            {

            }
        }

        private void AddWork()
        {
            var work = new WorkItem(Guid.NewGuid().ToString());
            _workItemQueue.Enqueue(work);
            Console.WriteLine($"Added work item with guid {work.Guid}...");
        }
    }
}
