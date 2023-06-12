' Wrapper API for using Microsoft Active Directory Services version 1.0
' Copyright (c) 2004-2005
' by Syed Adnan Ahmed ( adnanahmed235@yahoo.com )
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.

Imports System.DirectoryServices
Imports System.Text

Public Class Utility

#Region "public Constant Variables"

    Public Const DefaultDate As DateTime = #12/30/1899#
    Public Shared ADPath As String = "LDAP://ho.msb.com.vn"
    Public Shared ADUser As String = "msb.ldap"
    Public Shared ADPassword As String = "msb.ldap"
    Public Shared ADUsersPath As String = "OU=TVC,"
#End Region

#Region "private functions"
    Friend Shared Function GetDirectoryObject(ByVal UserName As String, ByVal Password As String) As DirectoryEntry
        Dim oDE As DirectoryEntry
        oDE = New DirectoryEntry(ADPath, UserName, Password, AuthenticationTypes.Secure)
        'oDE = New DirectoryEntry(ADPath, UserName, Password, AuthenticationTypes.Anonymous)
        Return oDE
    End Function
    Friend Shared Function GetDirectoryObjectBySecure(ByVal LdapDomain As String, ByVal UserName As String, ByVal Password As String) As DirectoryEntry
        Dim oDE As DirectoryEntry
        oDE = New DirectoryEntry(LdapDomain, UserName, Password, AuthenticationTypes.Secure)
        'oDE = New DirectoryEntry(ADPath, UserName, Password, AuthenticationTypes.Anonymous)
        Return oDE
    End Function

    Friend Shared Function GetDirectoryObject(ByVal LdapDomain As String, ByVal UserName As String, ByVal Password As String) As DirectoryEntry
        Dim oDE As DirectoryEntry
        oDE = New DirectoryEntry(LdapDomain, UserName, Password, AuthenticationTypes.Secure)
        'oDE = New DirectoryEntry(ADPath, UserName, Password, AuthenticationTypes.Anonymous)
        Return oDE
    End Function
#End Region

#Region "Enums"
    Public Enum LoginResult
        LOGIN_OK = 0
        LOGIN_USER_DOESNT_EXIST
        LOGIN_USER_ACCOUNT_INACTIVE
    End Enum
    Friend Enum UserStatus
        Enable = 544
        Disable = 546
    End Enum
    Friend Enum GroupScope
        ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP = -2147483644
        ADS_GROUP_TYPE_GLOBAL_GROUP = -2147483646
        ADS_GROUP_TYPE_UNIVERSAL_GROUP = -2147483640
    End Enum
    Friend Enum ADAccountOptions
        UF_TEMP_DUPLICATE_ACCOUNT = 256
        UF_NORMAL_ACCOUNT = 512
        UF_INTERDOMAIN_TRUST_ACCOUNT = 2048
        UF_WORKSTATION_TRUST_ACCOUNT = 4096
        UF_SERVER_TRUST_ACCOUNT = 8192
        UF_DONT_EXPIRE_PASSWD = 65536
        UF_SCRIPT = 1
        UF_ACCOUNTDISABLE = 2
        UF_HOMEDIR_REQUIRED = 8
        UF_LOCKOUT = 16
        UF_PASSWD_NOTREQD = 32
        UF_PASSWD_CANT_CHANGE = 64
        UF_ACCOUNT_LOCKOUT = 16
        UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 128
    End Enum

#End Region


    Friend Shared Function GetUser(ByVal UserName As String, ByVal Password As String) As DirectoryEntry
        Dim de As DirectoryEntry = GetDirectoryObject(UserName, Password)
        Dim deSearch As DirectorySearcher = New DirectorySearcher
        deSearch.SearchRoot = de
        deSearch.Filter = "(&(objectClass=user)(sAMAccountName=" + UserName + "))"
        deSearch.SearchScope = SearchScope.Subtree

        Dim results As SearchResult = deSearch.FindOne()
        If Not (results Is Nothing) Then
            'de = New DirectoryEntry(results.Path, ADUser, ADPassword, AuthenticationTypes.Secure)
            Return de
        Else
            Return Nothing
        End If
    End Function

    Friend Shared Function GetUser(ByVal Ldapdomain As String, ByVal UserName As String, ByVal Password As String) As DirectoryEntry
        Dim de As DirectoryEntry = GetDirectoryObject(Ldapdomain, UserName, Password)
        Dim deSearch As DirectorySearcher = New DirectorySearcher
        deSearch.SearchRoot = de
        deSearch.Filter = "(&(objectClass=user)(sAMAccountName=" + UserName + "))"
        deSearch.SearchScope = SearchScope.Subtree

        Dim results As SearchResult = deSearch.FindOne()
        If Not (results Is Nothing) Then
            'de = New DirectoryEntry(results.Path, ADUser, ADPassword, AuthenticationTypes.Secure)
            Return de
        Else
            Return Nothing
        End If
    End Function

    Friend Shared Sub EnableUserAccount(ByVal oDE As DirectoryEntry)
        oDE.Properties("userAccountControl").Value = UserStatus.Enable
        oDE.CommitChanges()
        oDE.Close()
    End Sub
    Friend Shared Function GetGroup(ByVal Name As String) As DirectoryEntry
        Dim de As DirectoryEntry = Utility.GetDirectoryObject()
        Dim deSearch As DirectorySearcher = New DirectorySearcher
        deSearch.SearchRoot = de
        deSearch.Filter = "(&(objectClass=group)(cn=" + Name + "))"
        deSearch.SearchScope = SearchScope.Subtree
        Dim results As SearchResult = deSearch.FindOne()
        If Not (results Is Nothing) Then
            de = New DirectoryEntry(results.Path, Utility.ADUser, Utility.ADPassword, AuthenticationTypes.Secure)
            Return de
        Else
            Return Nothing
        End If
    End Function

    Friend Shared Sub DisableUserAccount(ByVal oDE As DirectoryEntry)
        oDE.Properties("userAccountControl").Value = UserStatus.Disable
        oDE.CommitChanges()
        oDE.Close()
    End Sub
    Friend Shared Function GetDirectoryObject(ByVal DomainReference As String) As DirectoryEntry
        Dim oDE As DirectoryEntry
        oDE = New DirectoryEntry(Utility.ADPath + DomainReference, Utility.ADUser, Utility.ADPassword, AuthenticationTypes.Secure)
        Return oDE
    End Function

    Friend Shared Sub SetUserPassword(ByVal oDE As DirectoryEntry, ByVal Password As String)
        oDE.Invoke("SetPassword", New Object() {Password})
    End Sub

    Friend Shared Function IsAccountActive(ByVal userAccountControl As Integer) As Boolean
        Dim userAccountControl_Disabled As Integer = Convert.ToInt32(ADAccountOptions.UF_ACCOUNTDISABLE)
        Dim flagExists As Integer = userAccountControl And userAccountControl_Disabled
        If flagExists > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Friend Shared Function GetLDAPDomain() As String
        Dim LDAPDomain As StringBuilder = New StringBuilder
        Dim serverName As String = "k2mega.local"
        Dim LDAPDC As String() = serverName.Split(CType(".", Char))
        Dim index As Integer = 0
        While index < LDAPDC.GetUpperBound(0) + 1
            LDAPDomain.Append("DC=" + LDAPDC(index))
            If index < LDAPDC.GetUpperBound(0) Then
                LDAPDomain.Append(",")
            End If
            index += 1
        End While
        Return LDAPDomain.ToString()
    End Function
    Friend Shared Function GetDirectoryObjectByDistinguishedName(ByVal ObjectPath As String) As DirectoryEntry
        Dim oDE As DirectoryEntry
        oDE = New DirectoryEntry(ObjectPath, Utility.ADUser, Utility.ADPassword, AuthenticationTypes.Secure)
        Return oDE
    End Function
    Friend Shared Sub SetProperty(ByVal oDE As DirectoryEntry, ByVal PropertyName As String, ByVal PropertyValue As String)
        If Not (PropertyValue = String.Empty) Then
            If oDE.Properties.Contains(PropertyName) Then
                oDE.Properties(PropertyName)(0) = PropertyValue
            Else
                oDE.Properties(PropertyName).Add(PropertyValue)
            End If
        End If
    End Sub
    Friend Shared Function GetDirectoryObject() As DirectoryEntry
        Dim oDE As DirectoryEntry
        oDE = New DirectoryEntry(Utility.ADPath, Utility.ADUser, Utility.ADPassword, AuthenticationTypes.Secure)
        Return oDE
    End Function

    Friend Shared Function GetProperty(ByVal oDE As DirectoryEntry, ByVal PropertyName As String) As String
        If oDE.Properties.Contains(PropertyName) Then
            Return oDE.Properties(PropertyName)(0).ToString()
        Else
            Return String.Empty
        End If
    End Function
    Public Shared Function GetDirectoryEntry() As DirectoryEntry
        'Of course change the information for the LDAP to your network
        Dim dirEntry As New DirectoryEntry("LDAP://192.168.60.12/CN=Users;DC=histaff.vn")
        'Setting username & password to Nothing forces
        'the connection to use your logon credentials
        dirEntry.Username = Nothing
        dirEntry.Password = Nothing
        'Always use a secure connection
        dirEntry.AuthenticationType = AuthenticationTypes.Secure
        Return dirEntry
    End Function

    Public Function IsValidADLogin(ByVal loginName As String, ByVal givenName As String, ByVal surName As String) As Boolean
        Try
            'Create a DirectorySearcher Object (used for searching the AD)
            Dim search As New DirectorySearcher
            'Set the filter on the searcher object to look for the SAMAccountName, givenName
            ' and the sn (Sur Name)
            '  search.Filter = String.Format("(&(SAMAccountName={0})(givenName={1})(sn={2}))", ExtractUserName(loginName), givenName, surName)
            search.Filter = String.Format("(&(SAMAccountName={0})(givenName={1})(sn={2}))", "sondh")
            'Now load these properties to the search
            search.PropertiesToLoad.Add("cn")
            search.PropertiesToLoad.Add("SAMAccountName")   'Users login name
            search.PropertiesToLoad.Add("givenName")    'Users first name         search.PropertiesToLoad.Add("sn")   'Users last name
            'Use the .FindOne() Method to stop as soon as a match is found
            Dim result As SearchResult = search.FindOne()
            'Now check to see if a result was found
            If result Is Nothing Then
                'Login isn't valid
                Return False
            Else
                'Valid login
                Return True
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Active Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Function


    ''' <summary>
    ''' Function to extract just the login from the provided string (given in the format YOURDOMAIN\Username)
    ''' </summary>
    ''' <param name="path">Full AD login of the associate</param>
    ''' <returns>The login with the "YOURDOMAIN\" stripped</returns>
    ''' <remarks></remarks>

    Friend Shared Function ListAllADComputers() As Collection
        Dim dirEntry As DirectoryEntry = GetDirectoryEntry()
        Dim pcList As New Collection
        '   1. Search the Active Directory for all objects with type of computer
        Dim dirSearcher As DirectorySearcher = New DirectorySearcher(dirEntry)
        dirSearcher.Filter = ("(objectClass=computer)")
        '   2. Check the search results
        Dim dirSearchResults As SearchResult
        '   3. Loop through all the computer names returned
        For Each dirSearchResults In dirSearcher.FindAll()
            '   4. Check to ensure the computer name isnt already listed in 
            'the collection
            'If Not pcList.Contains(dirSearchResults.GetDirectoryEntry().Name.ToString()) Then
            '    '   5. Add the computer name to the collection (since 
            '    'it dont already exist)
            '    pcList.Add(dirSearchResults.GetDirectoryEntry().Name.ToString())
            'End If
        Next
        '   6. Return the results
        Return pcList
    End Function
End Class
