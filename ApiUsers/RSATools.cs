using System.Security.Cryptography;

namespace ApiUsers
{
    public static class RSATools
    {
        public static RSA GetPrivateKey()
        {
            return GetRSAKey("rsa/private_key.pem");
        }

        public static RSA GetPublicKey()
        {
            return GetRSAKey("rsa/public_key.pem");
        }

        private static RSA GetRSAKey(string path)
        {
            var f = File.ReadAllText(path);

            var rsa = RSA.Create();
            rsa.ImportFromPem(f);
            return rsa;
        }
    }
}
