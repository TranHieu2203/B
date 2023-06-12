using System;
using System.Collections;
using System.Net.Mail;
using System.Text;
namespace Mailer
{
    public class clsSendMail
    {
        public static string SendMail(string FromEmail, string ToEmail, string CcEmail, string BccEmail, string Subject, string Body, string SmtpServer, ArrayList attachFiles)
        {
            string str = "";
            MailMessage message = new MailMessage();
            try
            {
                int num;
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
                message.Attachments.Dispose();
                return "";
            }
            catch (Exception exception)
            {
                string str2 = exception.Message;
                message.Attachments.Dispose();
                return str2;
            }
        }

    }
}
