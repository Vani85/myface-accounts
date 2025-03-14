using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MyFace.Helpers;
public class UserPasswordHelper {

    public static byte[] GenerateSalt() {
        byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }
        return salt;
    }
    public static string GenerateHashedPassword(string salt, string userPassword) {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: userPassword,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return hashed;
    }

    public static string[] ReadAuthorizationHeader(string authHeader) {
        if (authHeader != null && authHeader.StartsWith("Basic")) {
            string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

            int seperatorIndex = usernamePassword.IndexOf(':');

            var username = usernamePassword.Substring(0, seperatorIndex);
            var password = usernamePassword.Substring(seperatorIndex + 1);
            return new string[]{username,password};
            
        } else {
            throw new Exception("The authorization header is either empty or isn't Basic.");
        }
    }

    public static bool ValidLogin(string enteredPassword, string salt, string actual_hashed_Password) {
        string entered_hashed_password = GenerateHashedPassword(salt,enteredPassword);
        if(entered_hashed_password == actual_hashed_Password) {
            return true;
        } 
        return false;
    }

}