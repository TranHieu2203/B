using System;
using System.Collections;
using System.Configuration;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using MailKit;
using MimeKit;

namespace HiStaffAPI.AppHelpers
{
    public class MailHelper
    {
        private string _bcc;
        private string _bodyHtml = string.Empty;
        private string _bodyText = string.Empty;
        private string _cc;
        private string _from = string.Empty;
        private string _password = string.Empty;
        private int _port = 0;
        private string _server = null;
        private string _subject = string.Empty;
        private string _to;
        private string _username = string.Empty;
        private ArrayList _attachFiles;
        public static readonly string SmtpLog = ConfigurationManager.AppSettings.Get("SMTPLOG");
        public MailHelper(string server, int port, string username, string password)
        {
            this._server = server;
            this._port = port;
            this._username = username;
            this._password = password;
            this._to = string.Empty;
            this._cc = string.Empty;
            this._bcc = string.Empty;
            this._bcc = string.Empty;
        }
        public string BCC
        {
            get
            {
                return this._bcc;
            }
            set
            {
                this._bcc = value;
            }
        }

        public ArrayList AttachFiles
        {
            get
            {
                return this._attachFiles;
            }
            set
            {
                this._attachFiles = value;
            }
        }

        public string BodyHTML
        {
            get
            {
                return this._bodyHtml;
            }
            set
            {
                this._bodyHtml = value;
            }
        }

        public string BodyText
        {
            get
            {
                return this._bodyText;
            }
            set
            {
                this._bodyText = value;
            }
        }

        public string CC
        {
            get
            {
                return this._cc;
            }
            set
            {
                this._cc = value;
            }
        }

        public string From
        {
            get
            {
                return this._from;
            }
            set
            {
                this._from = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public int Port
        {
            get
            {
                return this._port;
            }
            set
            {
                this._port = value;
            }
        }

        public string Server
        {
            get
            {
                return this._server;
            }
            set
            {
                this._server = value;
            }
        }

        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }

        public string To
        {
            get
            {
                return this._to;
            }
            set
            {
                this._to = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
        public string SendMail()
        {
            try
            {
                int num;
                string mess = "SUCCESSFUL";
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", _from));
                string[] strArray = _to.Split(new char[] { ';' });
                for (num = 0; num < strArray.Length; num++)
                {
                    if (strArray[num].ToString().Trim() != "")
                    {
                        message.To.Add(new MailboxAddress("", strArray[num].ToString().Trim()));
                    }
                }
                string[] strArray2 = _cc.Split(new char[] { ';' });
                for (num = 0; num < strArray2.Length; num++)
                {
                    if (strArray2[num].ToString().Trim() != "")
                    {
                        message.Cc.Add(new MailboxAddress("", strArray2[num].ToString().Trim()));
                    }
                }
                string[] strArray3 = _bcc.Split(new char[] { ';' });
                for (num = 0; num < strArray3.Length; num++)
                {
                    if (strArray3[num].ToString().Trim() != "")
                    {
                        message.Bcc.Add(new MailboxAddress("", strArray3[num].ToString().Trim()));
                    }
                }
                message.Subject = Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = _bodyHtml;
                if (_attachFiles != null)
                {
                    for (num = 0; num < _attachFiles.Count; num++)
                    {
                        builder.Attachments.Add((string)_attachFiles[num]);
                    }
                }

                message.Subject = Subject;

                message.Body = builder.ToMessageBody();
                //var logFile = AppDomain.CurrentDomain.BaseDirectory + @"\" + SmtpLog + @"\smtp.log";
                var logFile = AppDomain.CurrentDomain.BaseDirectory +  @"\smtp.log";

                using (var client = new SmtpClient(new ProtocolLogger(logFile, false)))
                {
                    client.Connect(_server, _port, SecureSocketOptions.StartTls);

                    client.Authenticate(_from, _password);

                    client.Send(message);
                    client.Disconnect(true);
                }
                return mess;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}