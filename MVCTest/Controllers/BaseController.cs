using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVCTest.Encryptor;
using MVCTest.Models.User;//DB

namespace MVCTest.Controllers
{
    public class BaseController : Controller
    {
        UserContext db;
        public BaseController(UserContext context)
        {
            db = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionId = HttpContext.Session.GetString("sessionId");
            var userId = HttpContext.Session.GetString("userId");
            var cookieAuth = HttpContext.Request.Cookies["cookieAuth"];
            var userIdC = HttpContext.Request.Cookies["userId"];
            string cookieAuthDB = "";
            string message;

            var selUser = db.Users.SingleOrDefault(u => u.Id == Convert.ToInt32(userId));
            if (selUser == null)
            {
                selUser = db.Users.SingleOrDefault(u => u.Id == Convert.ToInt32(userIdC));
            }

            try
            {
                cookieAuthDB = Encoding.UTF8.GetString(selUser.CookieAuthToken);
            }
            catch
            {
                message = "cookie not set";
            }
            

            if (selUser != null)
            {
                var sessionIdDB = Encoding.UTF8.GetString(selUser.SessionId);

                if (!String.IsNullOrEmpty(sessionId)) 
                {
                    if (sessionId == sessionIdDB)
                    {
                        ViewBag.Login = selUser.Login;
                    }
                }
                else
                {
                    if (cookieAuthDB == cookieAuth)
                    {
                        ViewBag.Login = selUser.Login;

                        var enc = new ServerEncryptor(selUser.Login + DateTime.Now.ToString());//md-hash encryptor
                        var sessionId_b = enc.Hash;

                        selUser.SessionId = sessionId_b;//set new session
                    }
                }


            }




            base.OnActionExecuting(context);    
        }

    }
}