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
    /// Consumes the WorkItem produced in the Queue by the DummyProducer
    /// </summary>
    public class DummySubscriber
    {
        private readonly ConcurrentQueue<WorkItem> _workItemQueue;
        private readonly Random _randomGenerator;

        private int _minInterval = 4;
        private int _maxInterval = 8;
        private string _name;

        public DummySubscriber(ConcurrentQueue<WorkItem> workItemQueue, Random randomGenerator, string name)
        {
            _workItemQueue = workItemQueue;
            _randomGenerator = randomGenerator;
            _name = name;
        }

        public void StartSubscriberWork()
        {
            try
            {
                WorkItem item;
                var curWork = 0;
                while (curWork < DummyProducer.MaxItem)
                {
                    while (_workItemQueue.TryDequeue(out item))
                    {
                        Console.WriteLine($"{_name} is process work: {nameof(curWork)} :{curWork}");
                        Console.WriteLine($"Processing work item with guid {item.Guid}.");
                        ProcessWork(item);
                        curWork++;
                        Console.WriteLine($"Marked work item with guid {item.Guid} as done.");
                    }
                }
                Console.WriteLine($"Processed all expected max items. Exiting thread task...");
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Process work for a random interval of time before attempting to dequeue.
        /// </summary>
        /// <param name="item"></param>
        private void ProcessWork(WorkItem item)
        {
            Thread.Sleep(_randomGenerator.Next(_minInterval, _maxInterval) * 1000);
            item.MarkAsDone();
        }
    }
}