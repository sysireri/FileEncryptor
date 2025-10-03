using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileEncryptor
{
    internal class FileEncryptorBC
    {
        // === Paramètres ===
        private const int intPbkdf2Iterations = 100_000;
        private const int intKeySizeBytes = 32;   // 256 bits
        private const int intIvSizeBytes = 16;    // 128 bits (AES bloc)
        private const int intSaltSizeBytes = 16;  // 128 bits

        // En-tête (MAGIC = "ENCZ")
        private static readonly byte[] bytMagic = Encoding.ASCII.GetBytes("ENCZ");
        private const byte bytVersion = 1;

        public static void EncryptAndCompress(string strInputPath, 
                                              string strOutputPath, 
                                              string strPassword)
        {
            // 1) Génère sel + IV aléatoires pour ce fichier
            byte[] salt = RandomBytes(intSaltSizeBytes);
            byte[] iv = RandomBytes(intIvSizeBytes);

            // 2) Dérive clé depuis mot de passe + sel
            byte[] bytkey = DeriveKeyFromPassword(strPassword, 
                                                  salt, 
                                                  intKeySizeBytes);

            // 3) Prépare l'en-tête dans un MemoryStream temporaire
            byte[] bytheaderBytes;
            using (MemoryStream StreamHeader = new MemoryStream())
            {
                using (BinaryWriter objBinaryWriter = new BinaryWriter(StreamHeader, 
                                                                       Encoding.UTF8))
                {
                    objBinaryWriter.Write(bytMagic);
                    objBinaryWriter.Write(bytVersion);
                    objBinaryWriter.Write((byte)salt.Length);
                    objBinaryWriter.Write((byte)iv.Length);
                    objBinaryWriter.Write(salt);
                    objBinaryWriter.Write(iv);
                    objBinaryWriter.Flush();
                    bytheaderBytes = StreamHeader.ToArray();
                }
            }

            // 4) Ouvre FileStream pour écrire le fichier final
            using (FileStream StreamOut = new FileStream(strOutputPath, 
                                                         FileMode.Create, 
                                                         FileAccess.Write, 
                                                         FileShare.None))
            {
                // 5) Écrit l'en-tête
                StreamOut.Write(bytheaderBytes, 
                                0, 
                                bytheaderBytes.Length);

                // 6) Chiffre et compresse le flux
                using (Aes objAes = Aes.Create())
                {
                    objAes.KeySize = 256;
                    objAes.Mode = CipherMode.CBC;
                    objAes.Padding = PaddingMode.PKCS7;
                    objAes.Key = bytkey;
                    objAes.IV = iv;

                    using (CryptoStream StreamCrypto = new CryptoStream(StreamOut, 
                                                                        objAes.CreateEncryptor(), 
                                                                        CryptoStreamMode.Write))
                    {
                        using (GZipStream objGzipStream = new GZipStream(StreamCrypto,
                                                                         CompressionMode.Compress))
                        {
                            using (FileStream objFileInpunt = new FileStream(strInputPath, 
                                                                             FileMode.Open, 
                                                                             FileAccess.Read, 
                                                                             FileShare.Read))
                            {
                                objFileInpunt.CopyTo(objGzipStream);
                            }
                        }
                    }
                }
            }
        }

        public static void DecryptAndDecompress(string strInputPath, 
                                                string strOutputPath, 
                                                string strPassword)
        {
            byte[] salt, iv;
            byte[] key;

            // 1) Lire l’en-tête depuis le fichier
            using (FileStream streamIn = new FileStream(strInputPath, 
                                                        FileMode.Open, 
                                                        FileAccess.Read, 
                                                        FileShare.Read))
            {
                using (BinaryReader ReaderBinary = new BinaryReader(streamIn, 
                                                                    Encoding.UTF8))
                {
                    byte[] bytMagicRead = ReaderBinary.ReadBytes(4);
                    if (bytMagicRead.Length != 4 || !BytesEqual(bytMagicRead, bytMagic))
                        throw new InvalidDataException("Signature de fichier invalide (MAGIC).");

                    byte bytReaderBinary = ReaderBinary.ReadByte();
                    if (bytReaderBinary != bytVersion)
                        throw new InvalidDataException($"Version non supportée: {bytReaderBinary}.");

                    int saltLen = ReaderBinary.ReadByte();
                    int ivLen = ReaderBinary.ReadByte();
                    if (saltLen <= 0 || ivLen <= 0 || saltLen > 64 || ivLen > 32)
                        throw new InvalidDataException("Tailles d’en-tête invalides.");

                    salt = ReaderBinary.ReadBytes(saltLen);
                    iv = ReaderBinary.ReadBytes(ivLen);
                    if (salt.Length != saltLen || iv.Length != ivLen)
                        throw new EndOfStreamException("En-tête tronqué.");

                    key = DeriveKeyFromPassword(strPassword, salt, intKeySizeBytes);

                    // 2) Lire le reste du fichier chiffré dans un MemoryStream
                    long lngRemainingLength = streamIn.Length - streamIn.Position;
                    byte[] bytencryptedData = ReaderBinary.ReadBytes((int)lngRemainingLength);

                    // 3) Déchiffre + Décompresse dans le fichier de sortie
                    using (FileStream FileStreamOut = new FileStream(strOutputPath, 
                                                                     FileMode.Create, 
                                                                     FileAccess.Write, 
                                                                     FileShare.None))
                    {
                        using (MemoryStream encryptedStream = new MemoryStream(bytencryptedData))
                        {
                            using (Aes objAes = Aes.Create())
                            {
                                objAes.KeySize = 256;
                                objAes.Mode = CipherMode.CBC;
                                objAes.Padding = PaddingMode.PKCS7;
                                objAes.Key = key;
                                objAes.IV = iv;

                                using (CryptoStream StreamCrypto = new CryptoStream(encryptedStream, 
                                                                                    objAes.CreateDecryptor(), 
                                                                                    CryptoStreamMode.Read))
                                {
                                    using (GZipStream objGzipStream = new GZipStream(StreamCrypto, 
                                                                                     CompressionMode.Decompress))
                                    {
                                        objGzipStream.CopyTo(FileStreamOut);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static byte[] DeriveKeyFromPassword(string strPassword, 
                                                    byte[] salt, 
                                                    int intKeyBytes)
        {
            using (var kdf = new Rfc2898DeriveBytes(strPassword, 
                                                    salt, 
                                                    intPbkdf2Iterations, 
                                                    HashAlgorithmName.SHA256))
            {
                return kdf.GetBytes(intKeyBytes);
            }
        }

        private static byte[] RandomBytes(int intCount)
        {
            byte[] bytNew = new byte[intCount];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytNew);
            }
            return bytNew;
        }

        private static bool BytesEqual(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i]) return false;
            return true;
        }
    }
}
