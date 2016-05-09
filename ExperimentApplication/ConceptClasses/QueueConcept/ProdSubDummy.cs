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
    /// Main class that start the Pub-Sub in different threads
    /// </summary>
    public class ProdSubDummy
    {
        public void StartTest()
        {
            var queue = new ConcurrentQueue<WorkItem>();
            var randomGenerator = new Random();

            var newProducer = new DummyProducer(queue, randomGenerator);
            var newSubscriber = new DummySubscriber(queue, randomGenerator, "thread 1");
            var newSubscriber2 = new DummySubscriber(queue, randomGenerator, "thread 2");

            Console.WriteLine("Task 1 starts...");
            var task1 = Task.Run(() => newProducer.StartProducerWork());

            Console.WriteLine("Task 2 starts...");
            var task2 = Task.Run(() => newSubscriber.StartSubscriberWork());

            Console.WriteLine("Task 3 starts...");
            var task3 = Task.Run(() => newSubscriber2.StartSubscriberWork());

            Task.WaitAll(task1, task2, task3);
            Console.WriteLine("Test done");
        }
    }
}
