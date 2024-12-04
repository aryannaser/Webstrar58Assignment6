using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
namespace Assignment6CSE445

{
    public static class P
    {
        // Convert input to a secure hash
        public static string S(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(input);
                byte[] hashed = sha256.ComputeHash(data);
                return Convert.ToBase64String(hashed);
            }
        }
    }
}