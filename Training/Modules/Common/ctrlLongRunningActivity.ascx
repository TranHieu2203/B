<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLongRunningActivity.ascx.vb" Inherits="Common.ctrlLongRunningActivity" %>
<fieldset>
    <legend><%# Translate("Long running activity test")%></legend>
    <tlk:RadToolBar ID ="rtbLongRunning" runat ="server"></tlk:RadToolBar>
    <asp:Label ID="Result" runat="server" Text="Label"></asp:Label>
</fieldset>


