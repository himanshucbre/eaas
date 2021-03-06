﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;

namespace EAAS.Core.Factory
{
    public class AESEncryption : ICryptoProviderFactory
    {
        public byte[] Decrypt(byte[] cipherBytes, string key, byte[] salt)
        {

            var rfc = new Rfc2898DeriveBytes(key, salt);
            byte[] Key = rfc.GetBytes(16);
            byte[] IV = rfc.GetBytes(16);

            byte[] plain;
            using (MemoryStream mStream = new MemoryStream(cipherBytes)) //add encrypted
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mStream,
                        aesProvider.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        //cryptoStream.Read(encrypted, 0, encrypted.Length);
                        using (StreamReader stream = new StreamReader(cryptoStream))
                        {
                            string sf = stream.ReadToEnd();
                            plain = System.Text.Encoding.Default.GetBytes(sf);
                        }
                    }
                }
            }
            return plain;

        }



        public string Decrypt(string cipherText, string key, byte[] salt)
        {
            string EncryptionKey = key;
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public byte[] Encrypt(byte[] plainBytes, string key, byte[] salt)
        {

            var rfc = new Rfc2898DeriveBytes(key, salt);
            byte[] Key = rfc.GetBytes(16);
            byte[] IV = rfc.GetBytes(16);
            byte[] encrypted; ;
            using (MemoryStream mstream = new MemoryStream())
            {
                using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mstream,
                        aesProvider.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    }
                    encrypted = mstream.ToArray();
                }
            }
            return encrypted;
        }

     

        public string Encrypt(string plainText, string key, byte[] salt)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    plainText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plainText;
        }
    }
}
