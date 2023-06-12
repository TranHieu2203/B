<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_CanBasicInfo.ascx.vb"
    Inherits="Recruitment.ctrlRC_CanBasicInfo" %>
<table class="table-form">
    <tr>
        <td class="lb">
            <%# Translate("Mã ứng viên")%>
        </td>
        <td>
            <asp:HiddenField ID="hidID" runat="server" />
            <div style="float: left;">
                <tlk:RadTextBox ID="txtCandidateCODE1" runat="server" Width="130px" SkinID="Textbox15">
                </tlk:RadTextBox>
                <tlk:RadButton EnableEmbeddedSkins="false" runat="server" ID="btnSearchCan1" CausesValidation="false"
                    SkinID="ButtonView" />
            </div>
        </td>
        <td>
            <%# Translate("Họ tên")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtCandidateName" runat="server" Width="150px">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
    </tr>
</table>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function OnCanCodeKeyPress1(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                $("#<%# txtCandidateCODE1.ClientID%>").blur(); //Fix loi ko lay gia tri moi ma lay gia tri cu tren textbox cua trinh duyet chrome.
                $get('<%# btnSearchCan1.ClientID %>').click();
                eventArgs.set_cancel(true); //ko co cai nay thi ko post back ve server.
            }
        }
        function LoadCan(strCanCode, strMessage, strCurrentPlaceHolder) {

            var oArg = new Object();
            var oWnd = GetRadWindow();
            if (strMessage != '') {
                var notify = noty({ text: strMessage, dismissQueue: true, type: 'success' });
                setTimeout(function () {
                    oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can=' + strCanCode + '&state=Normal&Place=' + strCurrentPlaceHolder + '&noscroll=1&reload=1');
                }, 2000);
            } else {
                oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlRC_CanDtl&group=Business&Can=' + strCanCode + '&state=Normal&Place=' + strCurrentPlaceHolder + '&noscroll=1&reload=1');
            }
        }
    </script>
</tlk:RadScriptBlock>
