﻿using EAAS.Core.Factory;
using System;

using System.Security.Cryptography;

namespace EAAS.Core
{
    public class Encryption : IEncryption
    {
        public string Encrypt (string encryptionType, string plainText, string key = "", byte[] salt = null)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionType);
            return cryptoProviderFactory.Encrypt(plainText, key, salt);
        }

        public byte[] Encrypt(string encryptionType, byte[] plainBytes, string key = "", byte[] salt = null)
        {
            ICryptoProviderFactory cryptoProviderFactory = null;

            cryptoProviderFactory = CryptoProviderFactory.CreateEncryptionFactory(encryptionType);
            return cryptoProviderFactory.Encrypt(plainBytes, key, salt);
        }
    }
}
