using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace Photogasm
{
    public class DB_Service<T>
    {
        private string WebUrlGetAllUsers = "http://teambrowebapi.azurewebsites.net/users/getusers/";
        private string WebUrlGetAllMessage = "http://teambrowebapi.azurewebsites.net/messages/getmessages/";
        private string WebUrlAddMessage = "http://teambrowebapi.azurewebsites.net/messages/addmessage/";
        private string WebUrlGetAllComments = "http://teambrowebapi.azurewebsites.net/comments/getcomments/";
        public async Task<List<T>> GetAllUsersTask()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebUrlGetAllUsers);

            var taskModel = JsonConvert.DeserializeObject<List<T>>(json);

            return taskModel;
        }
        public async Task<string> AddMessageTask(string toID,string fromID,string msg)
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebUrlAddMessage+toID+"/"+fromID+"/"+msg);


            return json;
        }
    }
}