using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;


namespace MutualBenefitsFundsWcf
{
    public class Secrecy
    {
        public static String HashPassword(String pass)
        {
            SHA1 HashAlgorithm = SHA1.Create(); //Hash algorithm declaration
            byte[] c = null; //Byte array to store the returned hashed data

            //Convert the input string to a byte array and compute the hash.
            c = HashAlgorithm.ComputeHash(Encoding.Default.GetBytes(pass));
            //String variable that will store the returned hashed string
            String hashedpass = "";
            //Loop through each byte of the hashed data and format each one as a hexadecimal string. 
            for (int i = 0; i < c.Length - 1; i++)
            {
                hashedpass = hashedpass + c[i].ToString("x2");
            }

            return hashedpass;
        }
    }
}