using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExperimentWeb.SignalRHubs;
using Microsoft.AspNet.SignalR;

namespace ExperimentWeb.Services
{
    public class HubService
    {
        private IHubContext _siteWideContext;

        public HubService()
        {
            _siteWideContext = GlobalHost.ConnectionManager.GetHubContext<SiteWideAppendHub>();
        }

        public void TestSend(string name, string message)
        {
            // todo could potentially get current user so that the message isn't pushed downb to the actor.
            _siteWideContext.Clients.All.broadcastMessage(name, message);
        }
    }
}