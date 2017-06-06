using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Facebook;

namespace Photogasm
{
    public class FacebookConnection
    {
        ////for server(check logout page)
        //string AppID = "215129715650897";
        //string AppSecret = "174721fa583606962d632dd5adc005ec";
        //string CallBackUrl = "http://teambro.azurewebsites.net/User/FbLogin.aspx";

        //For Local(check logout page)
        string AppSecret = "48860f18008fa7c9cef085ed4abea247";
        string AppID = "300287337070831";
        string CallBackUrl = "http://localhost:1257/User/FbLogin.aspx";
        string Scope = "user_about_me,email,public_profile,user_birthday";

        FacebookClient FacebookTask = new FacebookClient();

        public Uri CreateLoginUrl()
        {
            return FacebookTask.GetLoginUrl(
                                new
                                {
                                    client_id = AppID,
                                    client_secret = AppSecret,
                                    redirect_uri = CallBackUrl,
                                    response_type = "code",
                                    scope = Scope,
                                });
        }

        public dynamic GetAccessToken(string code, string state, string type)
        {
            dynamic result = FacebookTask.Post("oauth/access_token",
                                          new
                                          {
                                              client_id = AppID,
                                              client_secret = AppSecret,
                                              redirect_uri = CallBackUrl,
                                              code = code
                                          });
            return result.access_token;
        }

        public User GetUserInfo(dynamic accessToken)
        {
            var client = new FacebookClient(accessToken);
            User user = new User();
            
            dynamic me = client.Get("me?fields=email,first_name,last_name,birthday");
            user.FName = me.first_name;
            user.SName = me.last_name;
            user.ID = me.id;
            user.Email = me.email;
            user.ImgPath = string.Format("https://graph.facebook.com/{0}/picture?type=large", user.ID);
            return user;
        }

    }
}