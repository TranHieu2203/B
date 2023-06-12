using System;
using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Mailer
{
    public class clsSendMail20
    {
        public static bool _IsEnableSSL = false;
        public static bool _IsUserDomain = false;
        public static string _PortServermail = "";
        public static bool _UseImplicit = false;
        public static string _UserDomain = "";
        private static string strErr = "";

        private static string GetUserName(string mailAddress)
        {
            try
            {
                if (!mailAddress.Contains("@"))
                {
                    return mailAddress;
                }
                return mailAddress.Split(new char[] { '@' })[0];
            }
            catch
            {
                return mailAddress;
            }
        }

        public static bool SendMail(string FromEmail, string ToEmail, string CcEmail, string BccEmail, string Subject, string Body, string SmtpServer, ArrayList attachFiles)
        {
            string str = "";
            try
            {
                int num;
                MailMessage message = new MailMessage();
                MailAddress address = new MailAddress(FromEmail.Replace(" ", "_"));
                //str = ConfigurationSettings.AppSettings["pstrLogFile"];
                message.From = address;
                string[] strArray = ToEmail.Split(new char[] { ';' });
                for (num = 0; num < strArray.Length; num++)
                {
                    if (strArray[num].ToString().Trim() != "")
                    {
                        message.To.Add(strArray[num].ToString().Trim());
                    }
                }
                string[] strArray2 = CcEmail.Split(new char[] { ';' });
                for (num = 0; num < strArray2.Length; num++)
                {
                    if (strArray2[num].ToString().Trim() != "")
                    {
                        message.CC.Add(strArray2[num].ToString().Trim());
                    }
                }
                string[] strArray3 = BccEmail.Split(new char[] { ';' });
                for (num = 0; num < strArray3.Length; num++)
                {
                    if (strArray3[num].ToString().Trim() != "")
                    {
                        message.Bcc.Add(strArray3[num].ToString().Trim());
                    }
                }
                message.Subject = Subject;
                message.BodyEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Body = Body;
                if (attachFiles != null)
                {
                    for (num = 0; num < attachFiles.Count; num++)
                    {
                        Attachment item = new Attachment((string)attachFiles[num]);
                        message.Attachments.Add(item);
                    }
                }
                new SmtpClient(SmtpServer).Send(message);
                return true;
            }
            catch (Exception exception)
            {
                string str2 = exception.Message;
                return false;
            }
        }

        public static bool SendMail(string FromMail, string authenPass, string ToMail, string CCMail, string BCCMail, string Subject, string Body, string MailServer, ArrayList attachFiles)
        {
            string userName;
            MailMessage message = new MailMessage();
            string str = "";
            str = _PortServermail.Replace(",", "");
            if (!_UseImplicit)
            {
                try
                {
                    int num;
                    if (string.IsNullOrEmpty(ToMail))
                    {
                        throw new ArgumentNullException("ToMail");
                    }
                    if (string.IsNullOrEmpty(MailServer))
                    {
                        throw new ArgumentNullException("MailServer");
                    }
                    if (string.IsNullOrEmpty(FromMail))
                    {
                        throw new ArgumentNullException("FromMail");
                    }
                    message.From = new MailAddress(FromMail);
                    bool flag2 = false;
                    try
                    {
                        flag2 = _IsUserDomain;
                    }
                    catch
                    {
                    }
                    userName = GetUserName(FromMail);
                    if (flag2)
                    {
                        userName = _UserDomain;
                    }
                    string[] strArray = ToMail.Split(new char[] { ';' });
                    for (num = 0; num < strArray.Length; num++)
                    {
                        if (strArray[num].ToString().Trim() != "")
                        {
                            message.To.Add(strArray[num].ToString().Trim());
                        }
                    }
                    string[] strArray2 = CCMail.Split(new char[] { ';' });
                    for (num = 0; num < strArray2.Length; num++)
                    {
                        if (strArray2[num].ToString().Trim() != "")
                        {
                            message.CC.Add(strArray2[num].ToString().Trim());
                        }
                    }
                    string[] strArray3 = BCCMail.Split(new char[] { ';' });
                    for (num = 0; num < strArray3.Length; num++)
                    {
                        if (strArray3[num].ToString().Trim() != "")
                        {
                            message.Bcc.Add(strArray3[num].ToString().Trim());
                        }
                    }
                    NetworkCredential credential = new NetworkCredential(userName, authenPass);
                    message.Subject = Subject;
                    message.BodyEncoding = Encoding.UTF8;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.Body = Body;
                    if (attachFiles != null)
                    {
                        for (num = 0; num < attachFiles.Count; num++)
                        {
                            Attachment item = new Attachment((string)attachFiles[num]);
                            message.Attachments.Add(item);
                        }
                    }
                    SmtpClient client = new SmtpClient();
                    if (str != "")
                    {
                        client = new SmtpClient(MailServer, Convert.ToInt32(str));
                    }
                    else
                    {
                        client = new SmtpClient(MailServer);
                    }
                    try
                    {
                        if (_IsEnableSSL)
                        {
                            client.EnableSsl = true;
                        }
                    }
                    catch
                    {
                    }
                    if (authenPass.Trim() != "")
                    {
                        client.Credentials = credential;
                    }
                    client.Timeout = 0x1c9c380;
                    client.Send(message);
                    message.Attachments.Dispose();
                    return true;
                }
                catch (Exception exception)
                {
                    message.Attachments.Dispose();
                    strErr = exception.Message;
                    return false;
                }
            }
            int port = Convert.ToInt32(str);
            userName = FromMail;
            string str4 = SendMailViaImplicitSsl(MailServer, port, userName, authenPass, FromMail, ToMail, CCMail, BCCMail, Subject, Body, attachFiles);
            strErr = str4;
            return string.IsNullOrEmpty(str4);
        }

        public static string SendMail(string FromMail, string authenPass, string ToMail, string CCMail, string BCCMail, string Subject, string Body, string MailServer, ArrayList attachFiles, string embedImage)
        {
            MailMessage message = new MailMessage();
            try
            {
                int num;
                if (string.IsNullOrEmpty(ToMail))
                {
                    throw new ArgumentNullException("ToMail");
                }
                if (string.IsNullOrEmpty(MailServer))
                {
                    throw new ArgumentNullException("MailServer");
                }
                if (string.IsNullOrEmpty(FromMail))
                {
                    throw new ArgumentNullException("FromMail");
                }
                message.From = new MailAddress(FromMail);
                string userName = GetUserName(FromMail);
                string[] strArray = ToMail.Split(new char[] { ';' });
                for (num = 0; num < strArray.Length; num++)
                {
                    if (strArray[num].ToString().Trim() != "")
                    {
                        message.To.Add(strArray[num].ToString().Trim());
                    }
                }
                string[] strArray2 = CCMail.Split(new char[] { ';' });
                for (num = 0; num < strArray2.Length; num++)
                {
                    if (strArray2[num].ToString().Trim() != "")
                    {
                        message.CC.Add(strArray2[num].ToString().Trim());
                    }
                }
                string[] strArray3 = BCCMail.Split(new char[] { ';' });
                for (num = 0; num < strArray3.Length; num++)
                {
                    if (strArray3[num].ToString().Trim() != "")
                    {
                        message.Bcc.Add(strArray3[num].ToString().Trim());
                    }
                }
                NetworkCredential credential = new NetworkCredential(userName, authenPass);
                message.Subject = Subject;
                message.BodyEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;
                AlternateView item = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
                if ((embedImage != null) && (embedImage != ""))
                {
                    LinkedResource resource = new LinkedResource(embedImage)
                    {
                        ContentId = "imageId",
                        TransferEncoding = TransferEncoding.Base64
                    };
                    item.LinkedResources.Add(resource);
                    string fileName = "";
                    if ((fileName != null) && (fileName != ""))
                    {
                        LinkedResource resource2 = new LinkedResource(fileName)
                        {
                            ContentId = "imageId1",
                            TransferEncoding = TransferEncoding.Base64
                        };
                        item.LinkedResources.Add(resource2);
                    }
                }
                message.AlternateViews.Add(item);
                if (attachFiles != null)
                {
                    for (num = 0; num < attachFiles.Count; num++)
                    {
                        Attachment attachment = new Attachment((string)attachFiles[num]);
                        message.Attachments.Add(attachment);
                    }
                }
                SmtpClient client = new SmtpClient(MailServer);
                if (authenPass.Trim() != "")
                {
                    client.Credentials = credential;
                }
                client.Send(message);
                message.Attachments.Dispose();
                return "Successfull!!! ";
            }
            catch (Exception exception)
            {
                message.Attachments.Dispose();
                return ("Fail - Exception: " + exception.Message);
            }
        }

        public static bool SendMail____(string FromMail, string authenPass, string ToMail, string CCMail, string BCCMail, string Subject, string Body, string MailServer, ArrayList attachFiles, ref string sMessage)
        {
            string userName;
            MailMessage message = new MailMessage();
            string str = "";
            str = _PortServermail.Replace(",", "");
            if (!_UseImplicit)
            {
                try
                {
                    int num;
                    SmtpClient client;
                    if (string.IsNullOrEmpty(ToMail))
                    {
                        throw new ArgumentNullException("ToMail");
                    }
                    if (string.IsNullOrEmpty(MailServer))
                    {
                        throw new ArgumentNullException("MailServer");
                    }
                    if (string.IsNullOrEmpty(FromMail))
                    {
                        throw new ArgumentNullException("FromMail");
                    }
                    message.From = new MailAddress(FromMail);
                    bool flag2 = false;
                    try
                    {
                        flag2 = _IsUserDomain;
                    }
                    catch
                    {
                    }
                    userName = GetUserName(FromMail);
                    if (flag2)
                    {
                        userName = _UserDomain;
                    }
                    string[] strArray = ToMail.Split(new char[] { ';' });
                    for (num = 0; num < strArray.Length; num++)
                    {
                        if (strArray[num].ToString().Trim() != "")
                        {
                            message.To.Add(strArray[num].ToString().Trim());
                        }
                    }
                    string[] strArray2 = CCMail.Split(new char[] { ';' });
                    for (num = 0; num < strArray2.Length; num++)
                    {
                        if (strArray2[num].ToString().Trim() != "")
                        {
                            message.CC.Add(strArray2[num].ToString().Trim());
                        }
                    }
                    string[] strArray3 = BCCMail.Split(new char[] { ';' });
                    for (num = 0; num < strArray3.Length; num++)
                    {
                        if (strArray3[num].ToString().Trim() != "")
                        {
                            message.Bcc.Add(strArray3[num].ToString().Trim());
                        }
                    }
                    NetworkCredential credential = new NetworkCredential(userName, authenPass);
                    message.Subject = Subject;
                    message.BodyEncoding = Encoding.UTF8;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.Body = Body;
                    if (attachFiles != null)
                    {
                        for (num = 0; num < attachFiles.Count; num++)
                        {
                            Attachment item = new Attachment((string)attachFiles[num]);
                            message.Attachments.Add(item);
                        }
                    }
                    if (str != "")
                    {
                        client = new SmtpClient(MailServer, Convert.ToInt32(str));
                    }
                    else
                    {
                        client = new SmtpClient(MailServer);
                    }
                    try
                    {
                        if (_IsEnableSSL)
                        {
                            client.EnableSsl = true;
                        }
                    }
                    catch
                    {
                    }
                    if (authenPass.Trim() != "")
                    {
                        client.Credentials = credential;
                    }
                    client.Send(message);
                    message.Attachments.Dispose();
                    sMessage = "Send th\x00e0nh c\x00f4ng!!!";
                    return true;
                }
                catch (Exception exception)
                {
                    message.Attachments.Dispose();
                    string str4 = exception.Message;
                    sMessage = str4;
                    return false;
                }
            }
            int port = Convert.ToInt32(str);
            userName = FromMail;
            return string.IsNullOrEmpty(SendMailViaImplicitSsl(MailServer, port, userName, authenPass, FromMail, ToMail, CCMail, BCCMail, Subject, Body, attachFiles));
        }

        public static string SendMail_Err(string FromMail, string authenPass, string ToMail, string CCMail, string BCCMail, string Subject, string Body, string MailServer, ArrayList attachFiles)
        {
            strErr = "";
            SendMail(FromMail, authenPass, ToMail, CCMail, BCCMail, Subject, Body, MailServer, attachFiles);
            return strErr;
        }

        private static string SendMailViaImplicitSsl(string mailServer, int port, string username, string password, string fromMail, string toEmail, string ccMail, string bccMail, string subject, string body, ArrayList attachFiles)
        {
            try
            {
                int num;
                Smtp smtp = new Smtp(mailServer, port, username, password)
                {
                    From = fromMail,
                    Subject = subject,
                    BodyHTML = "<HTML><BODY>" + body + "</BODY></HTML>"
                };
                string[] strArray = toEmail.Split(new char[] { ';' });
                for (num = 0; num < strArray.Length; num++)
                {
                    if (!string.IsNullOrEmpty(strArray[num].ToString().Trim()))
                    {
                        smtp.To.Add(strArray[num].ToString().Trim());
                    }
                }
                string[] strArray2 = ccMail.Split(new char[] { ';' });
                for (num = 0; num < strArray2.Length; num++)
                {
                    if (!string.IsNullOrEmpty(strArray2[num].ToString().Trim()))
                    {
                        smtp.CC.Add(strArray2[num].ToString().Trim());
                    }
                }
                string[] strArray3 = bccMail.Split(new char[] { ';' });
                for (num = 0; num < strArray3.Length; num++)
                {
                    if (!string.IsNullOrEmpty(strArray3[num].ToString().Trim()))
                    {
                        smtp.BCC.Add(strArray3[num].ToString().Trim());
                    }
                }
                if (attachFiles != null)
                {
                }
                return smtp.SendMailViaSSL();
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        public static void Setup(string iPortServermail, bool iUseImplicit, bool iIsUserDomain, string iUserDomain, bool iIsEnableSSL)
        {
            _PortServermail = iPortServermail;
            _UseImplicit = iUseImplicit;
            _IsUserDomain = iIsUserDomain;
            _UserDomain = iUserDomain;
            _IsEnableSSL = iIsEnableSSL;
        }
    }
}
