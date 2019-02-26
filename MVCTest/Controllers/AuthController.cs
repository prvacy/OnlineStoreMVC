using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCTest.Encryptor;
using MVCTest.Models.User;

namespace MVCTest.Controllers
{
    public class AuthController : Controller
    {
        UserContext db1;
        public AuthController(UserContext context)
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
            var enc = new ServerEncryptor(Password);
            var pass = enc.Hash;

            var selUser = db1.Users.Single(u => u.Login == Login);
            if (selUser.Password.ToString() == pass.ToString())
            {
                ViewBag.Login = Login;

                enc.GetHash(Login + DateTime.Now.ToString());
                var sessionId = enc.Hash;
                var sessionIdStr = Encoding.UTF8.GetString(sessionId);

                HttpContext.Session.SetString("sessionId", sessionIdStr);
                selUser.SessionId = sessionId;
                db1.SaveChanges();

                return View("~/Views/Auth/LoginSuccess.cshtml");
            }
            else return View();

        }


    }
}