using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DAL.Context;
using Entity.Identity;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;
using BLL.Identity;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using Entity.Entity;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;

namespace PL.Hubs
{
    public class ChatHub : Hub
    {
        public static ConcurrentDictionary<string, string> ConnectedUser = new ConcurrentDictionary<string, string>();
        SatiyorumContext ent = new SatiyorumContext();


        [Authorize]
        public override  Task OnConnected()
        {
            var ad = Context.User.Identity.Name;
            if(ad!="")
            {
                ConnectedUser.AddOrUpdate(Context.ConnectionId, ad, (k, v) => ad);
            }
            return base.OnConnected();
                 

        }

        public override async Task OnDisconnected(bool stopCalled)
        {
            var str = string.Empty;
            ConnectedUser.TryRemove(Context.ConnectionId, out str);
        }

        [Authorize]
        public void receiveMessage(string message, string to)
        {
            if (message.Trim() == "")
            {
                Clients.Caller.EmptyMessage("Lütfen bir mesaj giriniz.");
                return;
            }
            if (Context.User.Identity.Name == "")
            {

                Clients.Caller.InfoMessage("Mesaj Gönderebilmek için Giriş Yapmalısınız!");
            }
            else
            {
                var connectionId = ConnectedUser.FirstOrDefault(u => u.Value == to);
                if (connectionId.Key != null)
                {
                    Clients.Client(connectionId.Key).GetMessage(Context.User.Identity.Name, message);
                    Clients.Caller.GetMessageCaller(message);
                }
                else
                {
                    Clients.Caller.Information(message);
                    Message m = new Message();
                    m.ReceiverId = to;
                    m.SenderId = Context.User.Identity.Name;
                    m.Messagee = message;
                    ent.Messages.Add(m);
                    ent.SaveChanges();
                }
            }
        }


    }
}