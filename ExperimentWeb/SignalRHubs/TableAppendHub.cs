using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ExperimentWeb.SignalRHubs
{
    public class TableAppendHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.Others.broadcastMessage(name, message);
        }
    }

    public class SiteWideAppendHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }

    public class SpecificObjectHub : Hub
    {
        //this method will be called from the client
        public void Send(string id, string name, string message)
        {
            Clients.OthersInGroup(id).broadcastMessage(id, name, message);
        }

        public async Task AddGroup(string id)
        {
            await Groups.Add(Context.ConnectionId, id);
        }
    }
}