<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOrganization.ascx.vb"
    Inherits="Common.ctrlOrganization" %>
 <fieldset style="padding: 0px !important;">
    <legend>
        <asp:Label ID="lblOrgDesc" runat="server"></asp:Label></legend>
    <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Organization_Dissolve %>"
        AutoPostBack="True" />
    <tlk:RadTreeView ID="treeOrg" runat="server" Style="margin: 5px;" CausesValidation="false">
    </tlk:RadTreeView>
    <tlk:RadTreeView ID="treeOrgPostback" runat="server" Style="margin: 5px;" CausesValidation="false">
    </tlk:RadTreeView>
</fieldset>
