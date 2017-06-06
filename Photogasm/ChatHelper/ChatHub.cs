using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;

namespace Photogasm.ChatHelper
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {

        //private async Task<List<User>> GetUsersAsync()
        //{
        //    var dbService = new DB_Service<User>();
        //    var UserList = await dbService.GetAllUsersAsync();

        //    return UserList;
        //}
        protected async Task<string> AddMessageAsync(string toID, string fromID, string msg)
        {
            var dbService = new DB_Service<User>();
            var UserList = await dbService.AddMessageTask(toID, fromID, msg);

            return UserList;
        }
        #region---Data Members---
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        #endregion

        #region---Methods---

        public void Users()
        {
            Clients.All.ConnectedUsers(ConnectedUsers);

        }

        public void Connect(string UserName, string UserID)
        {
            string id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId.Equals(id)) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserID = UserID, UserName = UserName });
            }
            UserDetail CurrentUser = ConnectedUsers.Where(u => u.ConnectionId == id).FirstOrDefault();
            // send to caller           
            Clients.Caller.onConnected(CurrentUser.UserID, CurrentUser.UserName, ConnectedUsers, CurrentMessage, CurrentUser.UserID);
            // send to all except caller client           
            Clients.AllExcept(CurrentUser.ConnectionId).onNewUserConnected(CurrentUser.UserID, CurrentUser.UserName, CurrentUser.UserID);
        }

        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            //AddMessageinCache(userName, message);

            // Broad cast message
            //Clients.All.messageReceived(userName, message);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {
            try
            {
                string fromconnectionid = Context.ConnectionId;
                string strfromUserId = (ConnectedUsers.Where(u => u.ConnectionId == Context.ConnectionId).Select(u => u.UserID).FirstOrDefault());
                string _fromUserId;
                _fromUserId = strfromUserId;

                string _toUserId;
                _toUserId = toUserId;

                List<UserDetail> FromUsers = ConnectedUsers.Where(u => u.UserID == _fromUserId).ToList();
                List<UserDetail> ToUsers = ConnectedUsers.Where(x => x.UserID == _toUserId).ToList();

                if (FromUsers.Count != 0 && ToUsers.Count() != 0)
                {


                    foreach (var FromUser in FromUsers)
                    {
                        // send to caller user                                                                                //Chat Title
                        Clients.Client(FromUser.ConnectionId).sendPrivateMessage(_toUserId, FromUsers[0].UserName, ToUsers[0].UserName, message);
                    }



                    foreach (var ToUser in ToUsers)
                    {
                        // send to                                                                                            //Chat Title
                        Clients.Client(ToUser.ConnectionId).sendPrivateMessage(_fromUserId, FromUsers[0].UserName, FromUsers[0].UserName, message);

                    }



                    // send to caller user
                    //Clients.Caller.sendPrivateMessage(_toUserId.ToString(), FromUsers[0].UserName, message);
                    //ChatDB.Instance.SaveChatHistory(_fromUserId, _toUserId, message);
                    MessageDetail _MessageDeail = new MessageDetail { FromUserID = _fromUserId, FromUserName = FromUsers[0].UserName, ToUserID = _toUserId, ToUserName = ToUsers[0].UserName, Message = message };
                    AddMessageinCache(_MessageDeail);

                    AddMessageAsync(toUserId, _fromUserId, message);
                }


            }
            catch { }
        }

        public void SendNotifications(string toUserId)
        {
            try
            {
                string message = "";
                string fromconnectionid = Context.ConnectionId;
                string strfromUserId = (ConnectedUsers.Where(u => u.ConnectionId == Context.ConnectionId).Select(u => u.UserID).FirstOrDefault());
                string _fromUserId;
                _fromUserId = strfromUserId;

                string _toUserId;
                _toUserId = toUserId;

                List<UserDetail> FromUsers = ConnectedUsers.Where(u => u.UserID == _fromUserId).ToList();
                List<UserDetail> ToUsers = ConnectedUsers.Where(x => x.UserID == _toUserId).ToList();

                if (FromUsers.Count != 0 && ToUsers.Count() != 0)
                {


                    foreach (var FromUser in FromUsers)
                    {
                        // send to caller user                                                                                //Chat Title

                        Clients.Client(FromUser.ConnectionId).receiveNotification(_toUserId, FromUsers[0].UserName, ToUsers[0].UserName, message);
                    }



                    foreach (var ToUser in ToUsers)
                    {
                        // send to                                                                                            //Chat Title
                        message = FromUsers[0].UserName + "liked your photo!";
                        Clients.Client(ToUser.ConnectionId).receiveNotification(_fromUserId, FromUsers[0].UserName, FromUsers[0].UserName, message);

                    }



                    // send to caller user

                    MessageDetail _MessageDeail = new MessageDetail { FromUserID = _fromUserId, FromUserName = FromUsers[0].UserName, ToUserID = _toUserId, ToUserName = ToUsers[0].UserName, Message = message };
                    AddMessageinCache(_MessageDeail);

                }


            }
            catch { }
        }



        public void PrivateMessage(string toUserId, string message)
        {
            try
            {
                string fromconnectionid = Context.ConnectionId;
                string strfromUserId = (ConnectedUsers.Where(u => u.ConnectionId == Context.ConnectionId).Select(u => u.UserID).FirstOrDefault());
                string _fromUserId;
                _fromUserId = strfromUserId;

                string _toUserId;
                _toUserId = toUserId;

                List<UserDetail> FromUsers = ConnectedUsers.Where(u => u.UserID == _fromUserId).ToList();
                List<UserDetail> ToUsers = ConnectedUsers.Where(x => x.UserID == _toUserId).ToList();

                if (FromUsers.Count != 0 && ToUsers.Count() != 0)
                {

                    foreach (var ToUser in ToUsers)
                    {
                        // send to                                                                                            //Chat Title
                        Clients.Client(ToUser.ConnectionId).sendPrivateMessage(_fromUserId, FromUsers[0].UserName, FromUsers[0].UserName, message);
                    }


                    foreach (var FromUser in FromUsers)
                    {
                        // send to caller user                                                                                //Chat Title
                        Clients.Client(FromUser.ConnectionId).sendPrivateMessage(_toUserId, FromUsers[0].UserName, ToUsers[0].UserName, message);
                    }
                    // send to caller user
                    //Clients.Caller.sendPrivateMessage(_toUserId.ToString(), FromUsers[0].UserName, message);
                    //ChatDB.Instance.SaveChatHistory(_fromUserId, _toUserId, message);
                    MessageDetail _MessageDeail = new MessageDetail { FromUserID = _fromUserId, FromUserName = FromUsers[0].UserName, ToUserID = _toUserId, ToUserName = ToUsers[0].UserName, Message = message };
                    AddMessageinCache(_MessageDeail);

                }


            }
            catch { }
        }



        public void RequestLastMessage(string FromUserID, string ToUserID)
        {
            List<MessageDetail> CurrentChatMessages = (from u in CurrentMessage where ((u.FromUserID == FromUserID && u.ToUserID == ToUserID) || (u.FromUserID == ToUserID && u.ToUserID == FromUserID)) select u).ToList();
            //send to caller user
            Clients.Caller.GetLastMessages(ToUserID, CurrentChatMessages);
        }


        public List<MessageDetail> LastMessageXamarin(string FromUserID, string ToUserID)
        {
            List<MessageDetail> CurrentChatMessages = (from u in CurrentMessage where ((u.FromUserID == FromUserID && u.ToUserID == ToUserID) || (u.FromUserID == ToUserID && u.ToUserID == FromUserID)) select u).ToList();
            //send to caller user
            Clients.Caller.GetLastMessages(ToUserID, CurrentChatMessages);
            return CurrentChatMessages;
        }


        public void SendUserTypingRequest(string toUserId)
        {
            string strfromUserId = (ConnectedUsers.Where(u => u.ConnectionId == Context.ConnectionId).Select(u => u.UserID).FirstOrDefault());

            string _toUserId;
            _toUserId = toUserId;

            List<UserDetail> ToUsers = ConnectedUsers.Where(x => x.UserID == _toUserId).ToList();

            foreach (var ToUser in ToUsers)
            {
                // send to                                                                                            
                Clients.Client(ToUser.ConnectionId).ReceiveTypingRequest(strfromUserId);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);
                if (ConnectedUsers.Where(u => u.UserID == item.UserID).Count() == 0)
                {
                    string id = item.UserID;
                    Clients.All.onUserDisconnected(id, item.UserName);
                }
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region---private Messages---
        private void AddMessageinCache(MessageDetail _MessageDetail)
        {
            CurrentMessage.Add(_MessageDetail);
            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }
        #endregion
    }
}