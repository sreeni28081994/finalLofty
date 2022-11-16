using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
/// <summary>
/// Summary description for LoftyUtility
/// </summary>
public class LoftyUtility
{
	public LoftyUtility()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string RemoveReservedChars(string name)
    {
        string result = string.Empty;
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
    public decimal ReturnDecimal(string InputValue)
    {
        decimal RetValue = 0;
        try
        {
            bool IsRetValue = decimal.TryParse(InputValue, out RetValue);
        }
        catch (Exception Ex)
        {

        }

        return RetValue;
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
    public bool validateAadharNumber(string aadharNumber)
    {
        bool isValidAadhar = false;
        if (Regex.IsMatch(aadharNumber, "^[0-9]{12}$"))
        {
            isValidAadhar = Verhoeff.validateVerhoeff(aadharNumber);
            
        }
                
        return isValidAadhar;
    }
    public string AmountInWords(decimal Num)
    {
        string returnValue;
        //I have created this function for converting amount in indian rupees (INR).
        //You can manipulate as you wish like decimal setting, Doller (any currency) Prefix.


        string strNum;
        string strNumDec;
        string StrWord;
        strNum = Num.ToString();


        if (strNum.IndexOf(".") + 1 != 0)
        {
            strNumDec = strNum.Substring(strNum.IndexOf(".") + 2 - 1);


            if (strNumDec.Length == 1)
            {
                strNumDec = strNumDec + "0";
            }
            if (strNumDec.Length > 2)
            {
                strNumDec = strNumDec.Substring(0, 2);
            }


            strNum = strNum.Substring(0, strNum.IndexOf(".") + 0);
            StrWord = ((double.Parse(strNum) == 1) ? " Rupee " : " Rupees ") + NumToWord((decimal)(double.Parse(strNum))) + ((double.Parse(strNumDec) > 0) ? (" and Paise" + cWord3((decimal)(double.Parse(strNumDec)))) : "");
        }
        else
        {
            StrWord = ((double.Parse(strNum) == 1) ? " Rupee " : " Rupees ") + NumToWord((decimal)(double.Parse(strNum)));
        }
        returnValue = StrWord + " Only";
        return returnValue;
    }
    static public string NumToWord(decimal Num)
    {
        string returnValue;


        //I divided this function in two part.
        //1. Three or less digit number.
        //2. more than three digit number.
        string strNum;
        string StrWord;
        strNum = Num.ToString();


        if (strNum.Length <= 3)
        {
            StrWord = cWord3((decimal)(double.Parse(strNum)));
        }
        else
        {
            StrWord = cWordG3((decimal)(double.Parse(strNum.Substring(0, strNum.Length - 3)))) + " " + cWord3((decimal)(double.Parse(strNum.Substring(strNum.Length - 2 - 1))));
        }
        returnValue = StrWord;
        return returnValue;
    }
    static public string cWordG3(decimal Num)
    {
        string returnValue;
        //2. more than three digit number.
        string strNum = "";
        string StrWord = "";
        string readNum = "";
        strNum = Num.ToString();
        if (strNum.Length % 2 != 0)
        {
            readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 1)));
            if (readNum != "0")
            {
                StrWord = retWord(decimal.Parse(readNum));
                readNum = System.Convert.ToString(double.Parse("1" + strReplicate("0", strNum.Length - 1) + "000"));
                StrWord = StrWord + " " + retWord(decimal.Parse(readNum));
            }
            strNum = strNum.Substring(1);
        }
        while (!System.Convert.ToBoolean(strNum.Length == 0))
        {
            readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 2)));
            if (readNum != "0")
            {
                StrWord = StrWord + " " + cWord3(decimal.Parse(readNum));
                readNum = System.Convert.ToString(double.Parse("1" + strReplicate("0", strNum.Length - 2) + "000"));
                StrWord = StrWord + " " + retWord(decimal.Parse(readNum));
            }
            strNum = strNum.Substring(2);
        }
        returnValue = StrWord;
        return returnValue;
    }
    static public string cWord3(decimal Num)
    {
        string returnValue;
        //1. Three or less digit number.
        string strNum = "";
        string StrWord = "";
        string readNum = "";
        if (Num < 0)
        {
            Num = Num * -1;
        }
        strNum = Num.ToString();


        if (strNum.Length == 3)
        {
            readNum = System.Convert.ToString(double.Parse(strNum.Substring(0, 1)));
            StrWord = retWord(decimal.Parse(readNum)) + " Hundred";
            strNum = strNum.Substring(1, strNum.Length - 1);
        }


        if (strNum.Length <= 2)
        {
            if (double.Parse(strNum) >= 0 && double.Parse(strNum) <= 20)
            {
                StrWord = StrWord + " " + retWord((decimal)(double.Parse(strNum)));
            }
            else
            {
                StrWord = StrWord + " " + retWord((decimal)(System.Convert.ToDouble(strNum.Substring(0, 1) + "0"))) + " " + retWord((decimal)(double.Parse(strNum.Substring(1, 1))));
            }
        }


        strNum = Num.ToString();
        returnValue = StrWord;
        return returnValue;
    }
    static public string retWord(decimal Num)
    {
        string returnValue;
        //This two dimensional array store the primary word convertion of number.
        returnValue = "";
        object[,] ArrWordList = new object[,] { { 0, "" }, { 1, "One" }, { 2, "Two" }, { 3, "Three" }, { 4, "Four" }, { 5, "Five" }, { 6, "Six" }, { 7, "Seven" }, { 8, "Eight" }, { 9, "Nine" }, { 10, "Ten" }, { 11, "Eleven" }, { 12, "Twelve" }, { 13, "Thirteen" }, { 14, "Fourteen" }, { 15, "Fifteen" }, { 16, "Sixteen" }, { 17, "Seventeen" }, { 18, "Eighteen" }, { 19, "Nineteen" }, { 20, "Twenty" }, { 30, "Thirty" }, { 40, "Forty" }, { 50, "Fifty" }, { 60, "Sixty" }, { 70, "Seventy" }, { 80, "Eighty" }, { 90, "Ninety" }, { 100, "Hundred" }, { 1000, "Thousand" }, { 100000, "Lakh" }, { 10000000, "Crore" } };


        int i;
        for (i = 0; i <= (ArrWordList.Length - 1); i++)
        {
            if (Num == System.Convert.ToDecimal(ArrWordList[i, 0]))
            {
                returnValue = (string)(ArrWordList[i, 1]);
                break;
            }
        }
        return returnValue;
    }
    static public string strReplicate(string str, int intD)
    {
        string returnValue;
        //This fucntion padded "0" after the number to evaluate hundred, thousand and on....
        //using this function you can replicate any Charactor with given string.
        int i;
        returnValue = "";
        for (i = 1; i <= intD; i++)
        {
            returnValue = returnValue + str;
        }
        return returnValue;
    }
}
public static class Verhoeff
{

    // The multiplication table
    static int[,] d = new int[,]
    {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
            {2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
            {3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
            {4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
            {5, 9, 8, 7, 6, 0, 4, 3, 2, 1},
            {6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
            {7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
            {8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
            {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}
    };

    // The permutation table
    static int[,] p = new int[,]
    {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 5, 7, 6, 2, 8, 3, 0, 9, 4},
            {5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
            {8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
            {9, 4, 5, 3, 1, 2, 6, 8, 7, 0},
            {4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
            {2, 7, 9, 3, 8, 0, 6, 4, 1, 5},
            {7, 0, 4, 6, 9, 1, 3, 2, 5, 8}
    };

    // The inverse table
    static int[] inv = { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };


    /// <summary>
    /// Validates that an entered number is Verhoeff compliant.
    /// NB: Make sure the check digit is the last one!
    /// </summary>
    /// <param name="num"></param>
    /// <returns>True if Verhoeff compliant, otherwise false</returns>
    public static bool validateVerhoeff(string num)
    {
        int c = 0;
        int[] myArray = StringToReversedIntArray(num);

        for (int i = 0; i < myArray.Length; i++)
        {
            c = d[c, p[(i % 8), myArray[i]]];
        }

        return c == 0;

    }

    /// <summary>
    /// For a given number generates a Verhoeff digit
    /// Append this check digit to num
    /// </summary>
    /// <param name="num"></param>
    /// <returns>Verhoeff check digit as string</returns>
    public static string generateVerhoeff(string num)
    {
        int c = 0;
        int[] myArray = StringToReversedIntArray(num);

        for (int i = 0; i < myArray.Length; i++)
        {
            c = d[c, p[((i + 1) % 8), myArray[i]]];
        }

        return inv[c].ToString();
    }


    /// <summary>
    /// Converts a string to a reversed integer array.
    /// </summary>
    /// <param name="num"></param>
    /// <returns>Reversed integer array</returns>
    private static int[] StringToReversedIntArray(string num)
    {
        int[] myArray = new int[num.Length];

        for (int i = 0; i < num.Length; i++)
        {
            myArray[i] = int.Parse(num.Substring(i, 1));
        }

        Array.Reverse(myArray);

        return myArray;

    }
}
public static class EncryptSHAI
{
    public static string Hash(string input)
    {
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}