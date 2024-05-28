using RestaurantManagement.Infrastructure.Interfaces;
using RestaurantManagement.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Implementations
{
    public class MD5HashCalculator : IHashCalculator
    {
        public string Calculate(string rawText)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] hashBytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(rawText));

                StringBuilder builder = new StringBuilder();
                foreach (byte hashByte in hashBytes)
                {
                    builder.Append(hashByte.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
