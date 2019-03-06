using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.User
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public byte[] Password { get; set; }

        public byte[] SessionId { get; set; }

        public byte[] CookieAuthToken { get; set; }

        public bool IsFirstAuth { get; set; }

    }
}
