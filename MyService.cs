using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using iText.Forms.Xfdf;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml.Linq;

/// <summary>
/// Summary description for MyService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MyService : System.Web.Services.WebService
{
    string strResult = string.Empty;
    public MyService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public ReturnResult ReturnSingleValue(string Query)
    {
        string strQuery=GetQuery(Query);
        ClassSql objClassSql = new ClassSql();
        string strResult = objClassSql.pReturnSingleValue(strQuery);
        ReturnResult RR = new ReturnResult();
        RR.Result = strResult;
        return RR;
    }
    protected string GetQuery(string strQuery)
    {
        string strResultString = string.Empty;
        JObject json = JObject.Parse(strQuery);
        string MasterName = string.Empty;
        switch (json["SlNo"].Value<string>())
        {
            case "1":
                strResultString = "EXEC procCheckExistID '" + json["FieldName"].Value<string>().Replace("-", "").Replace("'", "").Replace("\"", "") + "','" + json["TableName"].Value<string>().Replace("-", "").Replace("'", "").Replace("\"", "") + "'," + Convert.ToInt32(json["FieldValue"].Value<string>());
                break;
            case "2":
                MasterName = json["MasterName"].Value<string>().Replace("-", "").Replace("'", "").Replace("\"", "");
                strResultString = "SELECT COUNT(" + MasterName + "Name) FROM " + MasterName + "Master WHERE " + MasterName + "Name='" + json["Name"].Value<string>().Replace("'", "''") + "' AND " + MasterName + "ID<>" + json["ID"].Value<int>() + "";
                break;
            case "3":
                MasterName = json["MasterName"].Value<string>().Replace("-", "").Replace("'", "").Replace("\"", "");
                strResultString = "SELECT COUNT(" + MasterName + "Name) FROM Lib" + MasterName + "Master WHERE " + MasterName + "Name='" + json["Name"].Value<string>().Replace("'", "''") + "' AND Lib" + MasterName + "ID<>" + json["ID"].Value<int>() + "";
                break;
            default:
                break;
        }
        return strResultString;
    }
}
public class ReturnResult
{
    public string Result { get; set; }
}
