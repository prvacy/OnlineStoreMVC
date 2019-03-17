using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models.User
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(35)]
        [Required]
        public string Login { get; set; }

        [MaxLength(32)]
        [Required]
        public byte[] Password { get; set; }

        [MaxLength(32)]
        public byte[] SessionId { get; set; }

        [MaxLength(32)]
        public byte[] CookieAuthToken { get; set; }

        public bool IsFirstAuth { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }

        public string Adress { get; set; }


    }
}
