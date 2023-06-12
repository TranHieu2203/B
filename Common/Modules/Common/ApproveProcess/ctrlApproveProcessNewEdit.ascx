<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlApproveProcessNewEdit.ascx.vb"
    Inherits="Common.ctrlApproveProcessNewEdit" %>
    <tlk:RadToolBar ID="tbarApproveProcess" runat="server" />
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:ValidationSummary ID="valSum" runat="server" />
    <table class="table-form">
        <tr>
            <td class="lb">
                <%# Translate("Tên quy trình")%><span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                </tlk:RadTextBox>
                <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName"
                    runat="server" ErrorMessage="<%$ Translate: Chưa nhập tên quy trình %>" ToolTip="<%$ Translate: Chưa nhập tên quy trình %>">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="lb" style="vertical-align: top">
                <%# Translate("Số ngày giữ Request")%>
            </td>
            <td>
                <tlk:RadNumericTextBox runat="server" ID="rntxtRequestDate"></tlk:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb" style="vertical-align: top">
                <%# Translate("Email thông báo")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtEmail"></tlk:RadTextBox>
            </td>
        </tr>
    </table>
