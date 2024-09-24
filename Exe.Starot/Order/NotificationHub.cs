using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exe.Starot.Application.Order
{
    public class NotificationHub : Hub
    {
        public async Task NotifyStaff(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
