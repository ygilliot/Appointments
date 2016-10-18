using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Appointments.Api.Hubs {
    public class CalendarHub : Hub {
        //Subscribe to the calendar of a particular manager
        public void Subscribe(string managerId) {
            Groups.Add(Context.ConnectionId, managerId);
        }

        //Subscribe to the calendar of a particular manager
        public void Unsubscribe(string managerId) {
            Groups.Remove(Context.ConnectionId, managerId);
        }
    }
}