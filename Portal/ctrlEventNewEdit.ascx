<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEventNewEdit.ascx.vb"
    Inherits="Portal.ctrlEventNewEdit" %>
<tlk:RadToolBar ID="rtbMain" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" />
<table class="table-form">
    <tr>
        <td class="lb">
            <%# Translate("TITLE") %>: <span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox ID="rtTITLE" runat="server">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rtTITLE"
                runat="server" ErrorMessage="<%$ Translate: VALIDATE_EVENT_TITLE %>" ToolTip="<%$ Translate: VALIDATE_EVENT_TITLE %>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <%# Translate("DETAIL") %>: <span class="lbReq">*</span>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rEditor"
                runat="server" ErrorMessage="<%$ Translate: VALIDATE_EVENT_DETAIL %>" ToolTip="<%$ Translate: VALIDATE_EVENT_DETAIL %>"></asp:RequiredFieldValidator>
            <asp:CustomValidator ID="CustomValidator1" ControlToValidate="rEditor" runat="server"
                ErrorMessage="<%$ Translate: VALIDATE_EVENT_DETAIL_MAX_LENGTH %>" ToolTip="<%$ Translate: VALIDATE_EVENT_DETAIL_MAX_LENGTH %>"></asp:CustomValidator>
        </td>
    </tr>
</table>
<tlk:RadEditor ID="rEditor" runat="server" ToolbarMode="RibbonBarPageTop" Style="width: 100%">
</tlk:RadEditor>
