using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for EncryptionUtility
/// </summary>
public class EncryptionUtility
{
	public EncryptionUtility()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void EncryptFile(string source, string destination, string sKey)
    {
        FileStream fsInput = new FileStream(source, FileMode.Open, FileAccess.Read);

        FileStream fsEncrypted = new FileStream(destination,
                        FileMode.Create,
                        FileAccess.Write);

        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        ICryptoTransform desencrypt = DES.CreateEncryptor();
        CryptoStream cryptostream = new CryptoStream(fsEncrypted,
                            desencrypt,
                            CryptoStreamMode.Write);
        byte[] bytearrayinput = new byte[fsInput.Length - 1];
        fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
        cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
        cryptostream.Close();
        fsInput.Close();
        fsEncrypted.Close();
        desencrypt.Dispose();
        cryptostream.Dispose();
        fsInput.Dispose();
        fsEncrypted.Dispose();
    }

    //function to generate a 64 bit key
    public string GenerateKey()
    {
        // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
        DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

        // Use the Automatically generated key for Encryption. 
        return ASCIIEncoding.ASCII.GetString(desCrypto.Key);

    }

    
    public void DecryptFile(string source, string destination, string sKey)
    {
        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        //A 64 bit key and IV is required for this provider.
        //Set secret key For DES algorithm.
        DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        //Set initialization vector.
        DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

        //Create a file stream to read the encrypted file back.
        FileStream fsread = new FileStream(source,
                                       FileMode.Open,
                                       FileAccess.Read);
        //Create a DES decryptor from the DES instance.
        ICryptoTransform desdecrypt = DES.CreateDecryptor();
        //Create crypto stream set to read and do a 
        //DES decryption transform on incoming bytes.
        CryptoStream cryptostreamDecr = new CryptoStream(fsread,
                                                     desdecrypt,
                                                     CryptoStreamMode.Read);
        //Print the contents of the decrypted file.
        StreamWriter fsDecrypted = new StreamWriter(destination);
        fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
        fsDecrypted.Flush();
        fsDecrypted.Close();
        cryptostreamDecr.Close();
        fsread.Close();
        desdecrypt.Dispose();
        fsread.Dispose();
        fsDecrypted.Dispose();
        cryptostreamDecr.Dispose();
    }
    public void Compress(FileInfo fi)
    {
        // Get the stream of the source file.
        using (FileStream inFile = fi.OpenRead())
        {
            // Prevent compressing hidden and 
            // already compressed files.
            if ((File.GetAttributes(fi.FullName)
                & FileAttributes.Hidden)
                != FileAttributes.Hidden & fi.Extension != ".gz")
            {
                // Create the compressed file.
                using (FileStream outFile =
                            File.Create(fi.FullName + ".gz"))
                {
                    using (GZipStream Compress =
                        new GZipStream(outFile,
                        CompressionMode.Compress))
                    {
                        // Copy the source file into 
                        // the compression stream.
                        inFile.CopyTo(Compress);

                        //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                        //    fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                    }
                }
            }
        }
    }

    public void Decompress(FileInfo fi)
    {
        // Get the stream of the source file.
        using (FileStream inFile = fi.OpenRead())
        {
            // Get original file extension, for example
            // "doc" from report.doc.gz.
            string curFile = fi.FullName;
            string origName = curFile.Remove(curFile.Length -
                    fi.Extension.Length);

            //Create the decompressed file.
            using (FileStream outFile = File.Create(origName))
            {
                using (GZipStream Decompress = new GZipStream(inFile,
                        CompressionMode.Decompress))
                {
                    // Copy the decompression stream 
                    // into the output file.
                    Decompress.CopyTo(outFile);

                    //Console.WriteLine("Decompressed: {0}", fi.Name);

                }
            }
        }
    }
//    ◾< (less than)
//◾> (greater than)
//◾: (colon)
//◾" (double quote)
//◾/ (forward slash)
//◾\ (backslash)
//◾| (vertical bar or pipe)
//◾? (question mark)
//◾* (asterisk)

    public string RemoveReservedChars(string name)
    {
        string result=string.Empty;
        result = name.Replace("<", "").Replace(">", "").Replace(":", "").Replace("\"", "").Replace("/", "").Replace("\\", "").Replace("|", "").Replace("?", "").Replace("*", "");
        //result = result.Replace("'", "");
        return result;
    }

    public string ExceptBlanks(string str)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            switch (c)
            {
                case '\r':
                case '\n':
                case '\t':
                case ' ':
                    continue;
                default:
                    sb.Append(c);
                    break;
            }
        }
        return sb.ToString();
    }
    public string ExceptBreaks(string str)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            switch (c)
            {
                case '\r':
                case '\n':
                case '\t':
                    continue;
                default:
                    sb.Append(c);
                    break;
            }
        }
        return sb.ToString();
    }
    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
}