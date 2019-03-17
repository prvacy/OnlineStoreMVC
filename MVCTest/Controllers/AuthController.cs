using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCTest.Encryptor;
using MVCTest.Models;
using MVCTest.Models.User;

namespace MVCTest.Controllers
{
    public class AuthController : BaseController
    {
        SiteDbContext db1;
        public AuthController(SiteDbContext context) : base(context)
        {
            db1 = context;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(string Login, string Password, string RPassword)
        {
            if (Password == RPassword)
            {
                var user = new User
                {
                    Login = Login
                };

                var enc = new ServerEncryptor(Password);
                user.Password = enc.Hash;
                
                db1.Users.Add(user);
                db1.SaveChanges();
                ViewBag.Login = user.Login;
            }
            else return View();


            return View("~/Views/Auth/RegisterSuccess.cshtml");

        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(string Login, string Password)
        {
            var enc = new ServerEncryptor(Password);//md-hash encryptor
            var pass = enc.Hash;

            var selUser = db1.Users.Single(u => u.Login == Login);
            if (selUser.Password.ToString() == pass.ToString())
            {
                ViewBag.Login = Login;

                enc.GetHash(Login + DateTime.Now.ToString());
                var sessionId = enc.Hash;
                var sessionIdStr = Encoding.UTF8.GetString(sessionId);

                var encCookie = new ServerEncryptor(Login + DateTime.Now.ToString() + "sdsd" + "random string" + "54POdsxc");
                var cookie = encCookie.Hash;
                var cookieStr = Encoding.UTF8.GetString(cookie);

                HttpContext.Session.SetString("sessionId", sessionIdStr);             //save session
                HttpContext.Session.SetString("userId", Convert.ToString(selUser.Id));//save user id in session



                selUser.CookieAuthToken = cookie;                                           //save cookies on client and server for every auth
                HttpContext.Response.Cookies.Append("cookieAuth", cookieStr);
                HttpContext.Response.Cookies.Append("userId", Convert.ToString(selUser.Id));

                if (selUser.IsFirstAuth)
                {
                    selUser.IsFirstAuth = false;
                }


                HttpContext.Session.GetString("sessionId");

                selUser.SessionId = sessionId;
                db1.SaveChanges();

                return View("~/Views/Auth/LoginSuccess.cshtml");
            }
            else return View();

        }


    }
}