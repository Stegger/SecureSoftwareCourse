using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
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

            string hash = HashBytesToString(bytes);

            Console.WriteLine(hash);
        }

        private static string HashBytesToString(byte[] bytes)
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

            string hashA = HashBytesToString(bytesHashA);
            string hashB = HashBytesToString(bytesHashB);
            
            Console.WriteLine("Tegridy_A: " + hashA);
            Console.WriteLine("Tegridy_B: " + hashB);
            
        }
        
        
    }
}