using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Ploeh.AutoFixture;

namespace ExperimentApplication.ConceptClasses.Events
{
    public interface INewsBehaviourDefinition
    {
        void HandleCrime(object sender, NewsReleasedEventArgs args);
    }

    public abstract class BaseBehaviour
    {
        protected string GetName(object sender)
        {
            return string.Empty;
        }
    }

    public class GoodBehaviour : BaseBehaviour, INewsBehaviourDefinition
    {
        public void HandleCrime(object sender, NewsReleasedEventArgs args)
        {
            Console.WriteLine($"{GetName(sender)}Yes, going investigating the crime {args.NewsContent} as reported on {args.ReleaseDateTimeUtc.ToLongDateString()}");
        }
    }

    public class BadBehaviour : BaseBehaviour, INewsBehaviourDefinition
    {
        public void HandleCrime(object sender, NewsReleasedEventArgs args)
        {
            Console.WriteLine($"{GetName(sender)}Unable to investigate the crime {args.NewsContent} as reported on {args.ReleaseDateTimeUtc.ToLongDateString()}");
        }
    }

    public class ModerateBehaviour : BaseBehaviour, INewsBehaviourDefinition
    {
        public void HandleCrime(object sender, NewsReleasedEventArgs args)
        {
            Console.WriteLine($"{GetName(sender)}Will investigate the crime {args.NewsContent} as reported on {args.ReleaseDateTimeUtc.ToLongDateString()} in 2 hours time.");
        }
    }

    public class Police: INewsBehaviourDefinition
    {
        public string Name { get; set; }

        private CrimeWatch _crimeWatch;

        private INewsBehaviourDefinition _crimeHandlerBehaviour;

        private bool _isSubscribed;

        private System.Timers.Timer _timer;

        public Police(string name, CrimeWatch crimeWatch, INewsBehaviourDefinition crimeHandlerBehaviour)
        {
            Name = name;
            _crimeWatch = crimeWatch;
            _crimeHandlerBehaviour = crimeHandlerBehaviour;
            _crimeWatch.NewsReleased += HandleCrime;
            _isSubscribed = true;
            var randomSwitchInterval = new Random().Next(3, 10);
            _timer = new System.Timers.Timer()
            {
                Enabled = true,
                Interval = randomSwitchInterval * 1000,
                AutoReset = true
            };
            _timer.Elapsed += SwitchDuty;
            _timer.Start();
        }

        public void HandleCrime(object sender, NewsReleasedEventArgs args)
        {
            Console.WriteLine($"{Name} reporting.");
            _crimeHandlerBehaviour.HandleCrime(sender, args);
        }

        protected void SwitchDuty(object source, ElapsedEventArgs e) 
        {
            if (_isSubscribed)
            {
                Console.WriteLine($" {Name} going off duty...");
                _isSubscribed = false;
                _crimeWatch.NewsReleased -= HandleCrime;
            }
            else
            {
                Console.WriteLine($" {Name} going on duty...");
                _isSubscribed = true;
                _crimeWatch.NewsReleased += HandleCrime;
            }
        }
    }

    public class CrimeNews
    {
        public string Guid { get; private set; }

        public string NewsContent { get; set; }

        public DateTime CreateDateTimeUtc { get; set; }

        public static CrimeNews CreateNews()
        {
            return new CrimeNews()
            {
                NewsContent = Faker.Lorem.Sentence(),
                CreateDateTimeUtc = DateTime.UtcNow,
                Guid = System.Guid.NewGuid().ToString()
            };
        }
    }

    public class CrimeWatch
    {
        private readonly Random _randomGenerator;
        private readonly int _maxNews = 10;

        public event EventHandler<NewsReleasedEventArgs> NewsReleased;

        protected virtual void OnNewsReleased(NewsReleasedEventArgs e)
        {
            NewsReleased?.Invoke(this, e);
        }

        public List<CrimeNews> CrimeNews = new List<CrimeNews>();

        public CrimeWatch()
        {
            _randomGenerator = new Random();
        }

        // start generating crimenews at random interval until a certain maximum is reached
        public void StartCrimeWatch()
        {
            var counter = 0;
            while (counter < _maxNews)
            {
                Console.WriteLine($"The current news number = {counter}");
                var newCrimeNews = Events.CrimeNews.CreateNews();
                CrimeNews.Add(newCrimeNews);
                counter++;
                var randomSleepInterval = _randomGenerator.Next(0, 5);
                Thread.Sleep(randomSleepInterval * 1000);

                OnNewsReleased(new NewsReleasedEventArgs(newCrimeNews.NewsContent, newCrimeNews.CreateDateTimeUtc));
            }
        }
    }

    public class NewsReleasedEventArgs : EventArgs
    {
        public readonly string NewsContent;

        public readonly DateTime ReleaseDateTimeUtc;

        public NewsReleasedEventArgs(string newsContent, DateTime releaseDateTimeUtc)
        {
            NewsContent = newsContent;
            ReleaseDateTimeUtc = releaseDateTimeUtc;
        }
    }
}
