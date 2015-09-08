using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ExperimentLibrary;
using MassTransit;

namespace ExperimentService
{
    public partial class Service1 : ServiceBase
    {
        private IServiceBus _bus;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            DoStart();
        }

        protected override void OnStop()
        {
            DoStop();
        }

        public void DoStart()
        {
            _bus = MassTransitInitializer.CreateBus("EmailServiceSubscriber", x =>
            {
                x.Subscribe(subs =>
                {
                    subs.Consumer<MailMessageConsumer>().Permanent();
                });
            });
        }

        public void DoStop()
        {
            _bus?.Dispose();
        }
    }
}
