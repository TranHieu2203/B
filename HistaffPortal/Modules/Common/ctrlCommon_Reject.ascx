<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCommon_Reject.ascx.vb" 
    Inherits="Common.ctrlCommon_Reject" %>
<tlk:RadWindow runat="server" ID="rwMessage" Height="250px" Width="450px" Behaviors="Close"
    VisibleStatusbar="false" Modal="true" EnableViewState="false" Title="<%$ Translate: Lý do không phê duyệt %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
<table class="table-form">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="<%$ Translate: Vui lòng nhập lý do không phê duyệt %>">:<span class="lbReq">*</span></asp:Label>          
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
             </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
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
