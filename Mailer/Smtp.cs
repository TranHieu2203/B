using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Mailer
{
    public class Smtp : TcpClient
    {
        private ArrayList _bcc;
        private string _bodyHtml = string.Empty;
        private string _bodyText = string.Empty;
        private ArrayList _cc;
        private string _from = string.Empty;
        private string _password = string.Empty;
        private int _port = 0;
        private string _server = null;
        private string _subject = string.Empty;
        private ArrayList _to;
        private string _username = string.Empty;

        public Smtp(string server, int port, string username, string password)
        {
            this._server = server;
            this._port = port;
            this._username = username;
            this._password = password;
            this._to = new ArrayList();
            this._cc = new ArrayList();
            this._bcc = new ArrayList();
        }

        private string EncodeBase64(string value)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(value), Base64FormattingOptions.None);
        }

        private SslStream GetSSLStreamForTcpClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            SslStream stream = new SslStream(base.GetStream(), false, new RemoteCertificateValidationCallback(Smtp.ValidateServerCertificate), null);
            stream.AuthenticateAsClient(this.Server, new X509CertificateCollection(), SslProtocols.Tls, false);
            stream.ReadTimeout = 0x1388;
            stream.WriteTimeout = 0x1388;
            return stream;
        }

        private string HMAC_MD5(byte[] data, byte[] key)
        {
            byte[] buffer = new HMACMD5(key).ComputeHash(data);
            StringBuilder builder = new StringBuilder();
            foreach (byte num in buffer)
            {
                builder.Append(num.ToString("x2"));
            }
            return builder.ToString();
        }

        public string Response(SslStream sslStream)
        {
            byte[] buffer = new byte[0x400];
            int count = sslStream.Read(buffer, 0, 0x400);
            if (count == 0)
            {
                return string.Empty;
            }
            return Encoding.ASCII.GetString(buffer, 0, count);
        }

        public string SendMailViaSSL()
        {
            string command = string.Empty;
            try
            {
                base.Connect(this.Server, this.Port);
                SslStream sSLStreamForTcpClient = this.GetSSLStreamForTcpClient();
                if (this.Response(sSLStreamForTcpClient).Substring(0, 3) != "220")
                {
                    return "Kh\x00f4ng thể kết nối đến mail server!!!";
                }
                command = "EHLO " + Environment.MachineName + "\r\n";
                this.Write(command, sSLStreamForTcpClient);
                if (this.Response(sSLStreamForTcpClient).Substring(0, 3) != "250")
                {
                    return "Kh\x00f4ng thể truy xuất mail server!!!";
                }
                command = "AUTH LOGIN\r\n";
                this.Write(command, sSLStreamForTcpClient);
                if (this.Response(sSLStreamForTcpClient).Substring(0, 3) != "334")
                {
                    return "Kh\x00f4ng thể sử dụng AUTH LOGIN để authenticate đến mail server!!!";
                }
                command = this.EncodeBase64(this.UserName) + "\r\n";
                this.Write(command, sSLStreamForTcpClient);
                string str2 = this.Response(sSLStreamForTcpClient);
                if (str2.Substring(0, 3) != "334")
                {
                    return str2;
                }
                command = this.EncodeBase64(this.Password) + "\r\n";
                this.Write(command, sSLStreamForTcpClient);
                str2 = this.Response(sSLStreamForTcpClient);
                if (str2.Substring(0, 3) != "235")
                {
                    return str2;
                }
                command = "MAIL FROM:<" + this._from + ">\r\n";
                this.Write(command, sSLStreamForTcpClient);
                str2 = this.Response(sSLStreamForTcpClient);
                if (str2.Substring(0, 3) != "250")
                {
                    return str2;
                }
                foreach (string str3 in this._to)
                {
                    try
                    {
                        command = "RCPT TO:<" + str3 + ">\r\n";
                        this.Write(command, sSLStreamForTcpClient);
                        if (this.Response(sSLStreamForTcpClient).Substring(0, 3) != "250")
                        {
                            return "Mail To bị lỗi!!!";
                        }
                    }
                    catch
                    {
                        return "Mail To bị lỗi!!!";
                    }
                }
                foreach (string str3 in this._cc)
                {
                    try
                    {
                        command = "RCPT TO:<" + str3 + ">\r\n";
                        this.Write(command, sSLStreamForTcpClient);
                        if (this.Response(sSLStreamForTcpClient).Substring(0, 3) != "250")
                        {
                            return "Mail CC bị lỗi!!!";
                        }
                    }
                    catch
                    {
                        return "Mail CC bị lỗi!!!";
                    }
                }
                foreach (string str3 in this._bcc)
                {
                    try
                    {
                        command = "RCPT TO:<" + str3 + ">\r\n";
                        this.Write(command, sSLStreamForTcpClient);
                        if (this.Response(sSLStreamForTcpClient).Substring(0, 3) != "250")
                        {
                            return "Mail BCC bị lỗi!!!";
                        }
                    }
                    catch
                    {
                        return "Mail BCC bị lỗi!!!";
                    }
                }
                command = "DATA\r\n";
                this.Write(command, sSLStreamForTcpClient);
                str2 = this.Response(sSLStreamForTcpClient);
                if (str2.Substring(0, 3) != "354")
                {
                    return str2;
                }
                command = "Subject: " + this._subject + "\r\n";
                foreach (string str3 in this._to)
                {
                    command = command + "To: " + str3 + "\r\n";
                }
                foreach (string str3 in this._cc)
                {
                    command = command + "Cc: " + str3 + "\r\n";
                }
                command = command + "From: " + this._from + "\r\n";
                if (this._bodyHtml.Length > 0)
                {
                    command = (command + "MIME-Version: 1.0\r\nContent-Type: text/html;\r\n    charset=\"utf-8\"\r\n") + "\r\n" + this._bodyHtml;
                }
                else
                {
                    command = command + "\r\n" + this._bodyText;
                }
                command = command + "\r\n.\r\n";
                byte[] bytes = Encoding.UTF8.GetBytes(command);
                sSLStreamForTcpClient.Write(bytes, 0, bytes.Length);
                if (this.Response(sSLStreamForTcpClient).Substring(0, 3) != "250")
                {
                    return "Send email bị lỗi!!!";
                }
                command = "QUIT\r\n";
                this.Write(command, sSLStreamForTcpClient);
                if (this.Response(sSLStreamForTcpClient).IndexOf("221") == -1)
                {
                    return "Kh\x00f4ng thể đ\x00f3ng kết nối!!!";
                }
            }
            catch
            {
            }
            return string.Empty;
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public void Write(string command, SslStream sslStream)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(command);
            sslStream.Write(bytes, 0, bytes.Length);
        }

        public ArrayList BCC
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

        public ArrayList CC
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

        public ArrayList To
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
    }
}
