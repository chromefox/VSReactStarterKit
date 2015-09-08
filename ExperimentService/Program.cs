using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace ExperimentService
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            logger.Log(LogLevel.Trace, @"Starting service...");
#if DEBUG
            using (var ms = new Service1())
            {
                ms.DoStart();
                Application.Run(new TestControl(ms));
            }
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
#endif
            logger.Log(LogLevel.Trace, @"Stopping service...");
        }
    }
}
