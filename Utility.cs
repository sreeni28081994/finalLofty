using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{
	public Utility()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void SendMail(string strTo, string strCC, string strBCC, string strSubject, string strBody, string strAttachments, string strSMTPServer, string strUserName, string strPassword, int strPort, bool blnSSL, bool IsHTML, string strFromAddressDisplayName)
    {
        try
        {
            System.Net.Mail.MailMessage insMail = new System.Net.Mail.MailMessage();
            insMail.From = new System.Net.Mail.MailAddress(strUserName, strFromAddressDisplayName);
            insMail.Sender = new System.Net.Mail.MailAddress(strUserName, strFromAddressDisplayName);
            insMail.To.Add(strTo);
            if (!strBCC.Equals(String.Empty)) insMail.Bcc.Add(strBCC);
            insMail.Subject = strSubject;
            insMail.Body = strBody;
            insMail.Priority = System.Net.Mail.MailPriority.High;
            insMail.IsBodyHtml = IsHTML;
            insMail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.Never;
            if (!strCC.Equals(String.Empty)) insMail.CC.Add(strCC);
            if (!strAttachments.Equals(String.Empty))
            {
                string[] strAttach = strAttachments.Split(';');
                foreach (string strFile in strAttach)
                {
                    insMail.Attachments.Add(new System.Net.Mail.Attachment(strFile.Trim()));
                }
            }

            if (!strSMTPServer.Equals(String.Empty))
            {
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(strUserName, strPassword);
                smtp.EnableSsl = blnSSL;
                smtp.Host = strSMTPServer;
                smtp.Port = strPort;
                smtp.Send(insMail);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public bool IsValidEmail(string emailaddress)
    {
        try
        {
            System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch (Exception ex)
        {
            return "0";
        }
    }
}