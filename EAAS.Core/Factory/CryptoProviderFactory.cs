﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core.Factory
{
    
    public class CryptoProviderFactory
    {
        public static ICryptoProviderFactory CreateEncryptionFactory(string encryptionType)
        {           
            if(string.IsNullOrEmpty(encryptionType))
            {
                throw new NullReferenceException(encryptionType);
            }
            switch (encryptionType.Trim().ToLower())
            {
                case "md5":
                    return new MD5Encryption();
                case "rijndael":
                    return new RijndaelEncryption();
                case "des":
                    return new DESEncryption();
                case "tripledes":
                    return new TripleDESEncryption();
                case "aes":
                    return new AESEncryption();
                case "aes256":
                    return new AES256Encryption();
                case "fpean":
                    return new FPEAlphanumeric();
                case "fpen":
                    return new FPENumeric();
                default:                    
                    return null;
            }         
                
        }
    }
}
