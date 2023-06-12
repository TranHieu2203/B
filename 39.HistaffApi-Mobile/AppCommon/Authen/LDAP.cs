using System;
using System.DirectoryServices;
namespace HiStaffAPI.AppCommon.Authen
{
    public class LDAP
    {
        #region Properties
        private string LdapURL; //ldap://tinhvan.com
        private string LDAPDomain; // tinhvan.com
        private string LDAPBaseDN; //ldap://tinhvan.com/path
        private string Username;
        private string Password;

        //PrincipalContext insPrincipalContext;
        int LDAPType;
        #endregion

        /// <summary>
        /// Khởi tạo Object LDAP
        /// </summary>
        /// <param name="_LdapURL">Đường dẫn LDAP</param>
        /// <param name="_LDAPDomain">Domain LDAP</param>
        /// <param name="_LDAPBaseDN">BaseDN LDAP</param>
        /// <param name="_LDAPType">Loại LDAP</param>
        /// <param name="_Username">Tài khoản quản lý LDAP</param>
        /// <param name="_Password">Mật khẩu quản lý LDAP</param>
        /// <returns></returns>
        public LDAP(string _LdapURL, string _LDAPDomain, string _LDAPBaseDN, int _LDAPType,
            string _Username, string _Password)
        {
            LdapURL = _LdapURL; LDAPDomain = _LDAPDomain; LDAPBaseDN = _LDAPBaseDN;
            Username = _Username; Password = _Password; LDAPType = _LDAPType;
            try
            {
                //switch (LDAPType)
                //{
                //    case 1: //ContextType.Domain
                //        string d = GetLDAPDomain(LDAPDomain);
                //        insPrincipalContext = new PrincipalContext(ContextType.Domain, LdapURL, d, Username, Password);
                //        break;
                //    case 2: //ContextType.ApplicationDirectory
                //        insPrincipalContext = new PrincipalContext(ContextType.ApplicationDirectory, LDAPDomain, Username, Password);
                //        break;
                //    default: //ContextType.Machine
                //        insPrincipalContext = new PrincipalContext(ContextType.Machine, LDAPDomain, Username, Password);
                //        break;
                //}
            }
            catch { }
        }

        /// <summary>
        /// Check xác thực
        /// </summary>
        /// <returns></returns>
        public bool ValidateUser()
        {
            try
            {
                DirectoryEntry oDE = new DirectoryEntry(LDAPDomain, Username, Password, AuthenticationTypes.Secure);
                DirectoryEntry de=oDE;
                DirectorySearcher  deSearch = new  DirectorySearcher();
                deSearch.SearchRoot = de;
                deSearch.Filter = "(&(objectClass=user) (sAMAccountName=" + Username + "))";
                SearchResultCollection results = deSearch.FindAll();
                if (results.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex) { return  false; }
        }

        /// <summary>
        /// Xác định Domain LDAP
        /// </summary>
        /// <param name="Domain">Domain LDAP</param>
        /// <returns></returns>
        private string GetLDAPDomain(string Domain)
        {
            string r = "";
            string[] d = Domain.Split(new string[] { "." }, StringSplitOptions.None);
            for (int i = 0; i < d.Length - 1; i++) r = r + "DC=" + d[i] + ",";
            r = r + "DC=" + d[d.Length];
            return r;
        }

    }
}