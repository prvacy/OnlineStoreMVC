using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVCTest.Encryptor
{
    public class ServerEncryptor
    {
        /// <summary>
        /// Result is stored here
        /// </summary>
        public byte[] Hash { get; set; }


        /// <summary>
        /// Constructor for encrypting
        /// </summary>
        /// <param name="value">String to encrypt</param>
        public ServerEncryptor(string value)
        {
            SHA256 mySHA256 = SHA256.Create();

            var salt = "IOdsi5290zx90320fg902alzpq1146";
            var hsalt = mySHA256.ComputeHash(Encoding.ASCII.GetBytes("IOdsi5290zx90320fg902alzpq1146"));
            var hpass = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(value));

            mySHA256.ComputeHash(Encoding.ASCII.GetBytes(value + salt));
            Hash = mySHA256.Hash;
        }


        /// <summary>
        /// Same as constructor
        /// </summary>
        /// <param name="value"></param>
        public void GetHash(string value)
        {
            SHA256 mySHA256 = SHA256.Create();

            var salt = "IOdsi5290zx90320fg902alzpq1146";
            var hsalt = mySHA256.ComputeHash(Encoding.ASCII.GetBytes("IOdsi5290zx90320fg902alzpq1146"));
            var hpass = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(value));

            mySHA256.ComputeHash(Encoding.ASCII.GetBytes(value + salt));
            Hash = mySHA256.Hash;
        }

    }
}
