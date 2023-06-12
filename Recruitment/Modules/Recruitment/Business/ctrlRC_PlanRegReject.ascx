<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_PlanRegReject.ascx.vb"
    EnableViewState="false" Inherits="Recruitment.ctrlRC_PlanRegReject" %>
<%@ Import Namespace="Common" %>
<table class="table-form">
    <tr>
        <td>
            <%# Translate("Lý do không phê duyệt")%>:<span class="lbReq">*</span>
        </td>
    </tr>
    <tr>
        <td>
            <tlk:RadTextBox ID="txtRemarkReject" SkinID="Textbox1023" Width="400px" Height="100px" runat="server">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRemarkReject"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập lý do không phê duyệt %>"
                ToolTip="<%$ Translate: Bạn phải nhập lý do không phê duyệt %>"> 
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            <tlk:RadButton ID="cmdSave" runat="server" Text="<%$ Translate: Lưu %>">
            </tlk:RadButton>
        </td>
    </tr>
</table>
<script type="text/javascript">
    function getRadWindow() {
        if (window.radWindow) {
            return window.radWindow;
        }
        if (window.frameElement && window.frameElement.radWindow) {
            return window.frameElement.radWindow;
        }
        return null;
    }                  
</script>