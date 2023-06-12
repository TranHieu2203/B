<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTestSendMail.ascx.vb"
    Inherits="Common.ctrlTestSendMail" %>
FROM: <tlk:RadTextBox ID="txtFROM" runat="server">
</tlk:RadTextBox>
<br />
TO: <tlk:RadTextBox ID="txtTO" runat="server">
</tlk:RadTextBox>
<br />
SUBJECT: <tlk:RadTextBox ID="txtSUBJECT" runat="server">
</tlk:RadTextBox>
<br />
CONTENT: <tlk:RadTextBox ID="txtCONTENT" runat="server" TextMode="MultiLine">
</tlk:RadTextBox>
<br />
<asp:Button ID="btnInsert" Text="Insert" runat="server" />
<asp:Button ID="btnSend" Text="Send" runat="server" />