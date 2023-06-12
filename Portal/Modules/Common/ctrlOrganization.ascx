<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOrganization.ascx.vb"
    Inherits="Common.ctrlOrganization" %>
<fieldset>
    <legend>
        <asp:Label ID="lblOrgDesc" runat="server"></asp:Label></legend>
    <tlk:RadTreeView ID="treeOrg" runat="server" Style="margin: 5px;" CausesValidation="false" >
    </tlk:RadTreeView>
    <tlk:RadTreeView ID="treeOrgPostback" runat="server" Style="margin: 5px;" CausesValidation="false" >
    </tlk:RadTreeView>
</fieldset>