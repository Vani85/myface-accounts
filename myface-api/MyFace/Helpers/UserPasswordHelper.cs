using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MyFace.Helpers;
public class UserPasswordHelper {

    public static byte[] GenerateSalt() {
        byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }

        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
        return salt;
    }
    public static string GenerateHashedPassword(string salt, string userPassword) {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: userPassword,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        Console.WriteLine($"Hashed: {hashed}");
        return hashed;
    }

        


}