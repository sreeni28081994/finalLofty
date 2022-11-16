using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;
/// <summary>
/// Summary description for LoftyFunctions
/// </summary>
public class LoftyFunctions
{
    public LoftyFunctions()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public CultureInfo cInfo = new CultureInfo("hi-IN");
    public string GetNodeValue(XElement XE, string Node)
    {
        string strResult = string.Empty;
        try
        {
            strResult = XE.Descendants(Node).ElementAt(0).Value;
        }
        catch
        {
        }
        return strResult;
    }
    public string GetEncodedXMLString(string xml)
    {
        string strResult = string.Empty;
        try
        {
            strResult = xml.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }
        catch
        {
        }
        return strResult;
    }
    public int GetIntegerValue(string value)
    {
        int intValue = 0;
        bool boolValue = int.TryParse(value, out intValue);
        return intValue;
    }
    public string pReturnDate(string strDate)
    {
        string strResult = string.Empty;
        try
        {
            if (strDate == "")
                strResult = "";
            else
                strResult = Convert.ToDateTime(strDate, cInfo).ToString("yyyy/MM/dd");

        }
        catch (Exception ex)
        {
            strResult = "";

        }
        return strResult;
    }
    public bool IsDate(string strDate)
    {
        string strResult;
        try
        {
            if (strDate == "")
                strResult = "";
            else
                strResult = Convert.ToDateTime(strDate, cInfo).ToString("yyyy/MM/dd");
        }
        catch (Exception ex)
        {
            strResult = "";
        }
        if (strResult == "")
            return false;
        else
            return true;
    }
    public string pReturnDateorNull(string strDate)
    {
        string strResult;
        try
        {
            if (strDate == "")
                strResult = "NULL";
            else
                strResult = "'" + Convert.ToDateTime(strDate, cInfo).ToString("yyyy/MM/dd") + "'";

        }
        catch (Exception ex)
        {
            strResult = "NULL";

        }
        return strResult;
    }
    public string pReturnDateorNullDefault(string strDate)
    {
        string strResult;
        try
        {
            if (strDate == "")
                strResult = "NULL";
            else
                strResult = "'" + Convert.ToDateTime(strDate).ToString("yyyy/MM/dd") + "'";

        }
        catch (Exception ex)
        {
            strResult = "NULL";

        }
        return strResult;
    }
    public string HashCode(string str)
    {
        string rethash = "";
        try
        {
            System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
            System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
            byte[] combined = encoder.GetBytes(str);
            hash.ComputeHash(combined);
            rethash = Convert.ToBase64String(hash.Hash);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return rethash;
    }
    public string GetSHA1HashData(string data)
    {
        //create new instance of md5
        SHA1 sha1 = SHA1.Create();

        //convert the input text to array of bytes
        byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

        //create new instance of StringBuilder to save hashed data
        StringBuilder returnValue = new StringBuilder();

        //loop for each byte and add it to StringBuilder
        for (int i = 0; i < hashData.Length; i++)
        {
            returnValue.Append(hashData[i].ToString());
        }

        // return hexadecimal string
        return returnValue.ToString();
    }
    public bool ValidateSHA1HashData(string inputData, string storedHashData)
    {
        //hash input text and save it string variable
        string getHashInputData = GetSHA1HashData(inputData);

        if (string.Compare(getHashInputData, storedHashData) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}