using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Examples.Encryption;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            
            String key = "GMP/zp9aviGP5oxKgxl8cGHP4UYptPjJ+dXC7H+pTQo=";
            String iv1 = "c9lkAITOp7Ey0CVMTUYUgQ==";
            String cipher = "fn8fJbHF26I81jF2nJj6lA==";

            MyAesEncryptionService encrypt = new MyAesEncryptionService(key, iv1);

            //See Cipher
            byte[] seeSecret = Convert.FromBase64String(cipher);
            Console.WriteLine("ENCRYPTED: " + encrypt.DecryptMessage(seeSecret));

            //Make Cipher
            byte[] makeCipher = encrypt.EncryptMessage("Andys mor laver UWU face når hun får min pik ;)");
            string makeCipherToBase = Convert.ToBase64String(makeCipher);
            Console.WriteLine("CREATED: " + makeCipherToBase);
            
            
            
            //With secret number, modulus
            string salt = "D9PCVVBleVC+rz5f+VKcDVXl/ACDmTD3ljULHnDPlO/t8qzmzuCapqnnhjKM4GXobAIXLG2DmdnstifZmwp0E6V/nJ9zQWjjYu3K/Mq+YGlcWfuI9guw/B9vJ6VGu3OhVbzKrQMoGCpoT7iWX0qVqBbxyetXL+jAG58DUIXoy9c=";
            string iv = "C4Eh5yNcSDPva6YEcfLp2g==";
            double s = 13;

            string strS = "" + s;

            PasswordToKeyService keyService = new PasswordToKeyService();

            byte[] byteSalt = Convert.FromBase64String(salt);
            byte[] byteKey = keyService.GetKey(strS, byteSalt, 128);
            byte[] byteIv = Convert.FromBase64String(iv);
            MyAesEncryptionService encryptionService = new MyAesEncryptionService(byteKey, byteIv);

            string cipherText = "gsbd479iAC5Xcysa1altFBxgO5mGWdYtkt+EsetgjKVU5ajxolfwiOKVeeIpzo9P";
            byte[] byteCipherText = Convert.FromBase64String(cipherText);
            string message = encryptionService.DecryptMessage(byteCipherText);
            
            Console.WriteLine("MODULUS MESSAGE: " + message);

        }

        private static void HowLongIsAnAesKey()
        {
            MyAesEncryptionService service = new MyAesEncryptionService();

            string key = service.GetKey();


            byte[] bytes = Convert.FromBase64String(key);

            Console.WriteLine("Key length: " + bytes.Length + " bytes");
            Console.WriteLine("Key length: " + bytes.Length * 8 + " bits");
        }

        private static void FailingExampleInvalidKeyLength()
        {
            byte[] key = Utf8StringToBytes("PASSWORDPASSWORDPASSWORDPASSWORD");
            byte[] iv;
            MyAesEncryptionService encryptionService = new MyAesEncryptionService(key, out iv);
            
            string secret = "Hello secure software developers!";
            byte[] messageBytes = encryptionService.EncryptMessage(secret);
            string message = Program.BytesToString(messageBytes);

            string decrypted = encryptionService.DecryptMessage(messageBytes);
            
            
            Console.WriteLine("Key: " + key);
            Console.WriteLine("IV: " + iv);
            Console.WriteLine("Message: " + message);
            Console.WriteLine("Decrypted message: " + decrypted);
        }

        private static byte[] Utf8StringToBytes(string input)
        {
            var utf8 = new UTF8Encoding();
            byte[] key = utf8.GetBytes(input);
            return key;
        }

        private static void HashingOne()
        {
            Program program = new Program();
            program.SimpleHashExample();

            Console.WriteLine("");

            program.TegridyHash();
        }

        private void SimpleHashExample()
        {
            string input = "Hello World!";

            HashAlgorithm sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(UTF8Encoding.UTF8.GetBytes(input));

            string hash = BytesToString(bytes);

            Console.WriteLine(hash);
        }


        private static string BytesToString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            string hash = builder.ToString();
            return hash;
        }

        private void TegridyHash()
        {
            byte[] tegridyA = File.ReadAllBytes("C:/Users/stegg/Desktop/Workspace/Tegridy_A.zip");
            byte[] tegridyB = File.ReadAllBytes("C:/Users/stegg/Desktop/Workspace/Tegridy_B.zip");

            HashAlgorithm sha = SHA256.Create();

            byte[] bytesHashA = sha.ComputeHash(tegridyA);
            byte[] bytesHashB = sha.ComputeHash(tegridyB);

            string hashA = BytesToString(bytesHashA);
            string hashB = BytesToString(bytesHashB);

            Console.WriteLine("Tegridy_A: " + hashA);
            Console.WriteLine("Tegridy_B: " + hashB);
        }
    }
}