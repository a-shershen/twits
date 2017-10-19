using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twits.WEB.Hash
{
    public static class Hash
    {
        public static string GetHash(string plainText)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            using (System.Security.Cryptography.SHA256 sha = System.Security.Cryptography.SHA256.Create())
            {
                bytes = sha.ComputeHash(bytes);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                foreach(byte b in bytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}