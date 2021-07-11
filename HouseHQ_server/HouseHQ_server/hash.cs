using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HouseHQ_server
{
    public class hash
    {
        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /*


         input: 

         output:
         */
        public string getUserNameHash(httpServer Http, string hashUser)
        {
            List<string> userName = Http.db.getAllUsers(Http.con);

            foreach (string user in userName)
            {
                if (hashUser == ComputeSha256Hash(user))
                {
                    return user;
                }
            }
            return "";
        }

        /*


         input: 

         output:
         */
        public string getCodeHash(string hashCode)
        {
            Codes code = new Codes();

            Dictionary<string, string>.KeyCollection keys = code.codes().Keys;

            foreach (string key in keys)
            {
                if (hashCode == ComputeSha256Hash(key))
                {
                    return key;
                }
            }
            return "";
        }
    }
}
