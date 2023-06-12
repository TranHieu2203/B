<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCanBasicInfo.ascx.vb"
    Inherits="Recruitment.ctrlCanBasicInfo" %>
<table class="table-form">
    <tr>
        <td>
            <%# Translate("Mã ứng viên")%>
        </td>
        <td>
            <asp:HiddenField ID="hidID" runat="server" />
            <div style="float: left;">
                <tlk:RadButton runat="server" ID="btnPre1" Text="<%$ Translate: << %>" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadTextBox ID="txtCandidateCODE1" runat="server" Width="80px" SkinID="Textbox15">
                </tlk:RadTextBox>
                <tlk:RadButton runat="server" ID="btnSearchEmp1" CausesValidation="false" SkinID="ButtonView" />
            </div>
        </td>
        <td>
            <tlk:RadButton runat="server" ID="btnNext1" Text="<%$ Translate: >> %>" CausesValidation="false">
            </tlk:RadButton>
        </td>
    </tr>
    <tr>
        <td>
            <%# Translate("Họ và tên đệm")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtFirstNameVN" runat="server" Width="150px">
            </tlk:RadTextBox>
        </td>
        <td>
            <%# Translate("Tên")%>
        </td>
        <td style="text-align: left; white-space: nowrap">
            <tlk:RadTextBox ID="txtLastNameVN" runat="server" Width="150px">
            </tlk:RadTextBox>
        </td>
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
        function OnEmpCodeKeyPress1(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                $("#<%# txtCandidateCODE1.ClientID%>").blur(); //Fix loi ko lay gia tri moi ma lay gia tri cu tren textbox cua trinh duyet chrome.
                $get('<%# btnSearchEmp1.ClientID %>').click();
                eventArgs.set_cancel(true); //ko co cai nay thi ko post back ve server.
            }
        }
        function LoadEmp(strEmpCode, strMessage, strCurrentPlaceHolder) {

            var oArg = new Object();
            var oWnd = GetRadWindow();
            if (strMessage != '') {
                var notify = noty({ text: strMessage, dismissQueue: true, type: 'success' });
                setTimeout(function () {
                    oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&emp=' + strEmpCode + '&state=Normal&Place=' + strCurrentPlaceHolder + '&noscroll=1&reload=1');
                }, 2000);
            } else {
                oWnd.setUrl('Dialog.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&emp=' + strEmpCode + '&state=Normal&Place=' + strCurrentPlaceHolder + '&noscroll=1&reload=1');
            }
        }
    </script>
</tlk:RadScriptBlock>
