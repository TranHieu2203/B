<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEmpBasicInfo.ascx.vb"
    Inherits="Profile.ctrlEmpBasicInfo" %>
<%@ Import Namespace="Common" %>
<table class="table-form">
    <tr>
        <td class="lb" style="width: 130px">
            <%# Translate("Mã nhân viên")%>
        </td>
        <td>
            <asp:HiddenField ID="hidID" runat="server" />
            <asp:HiddenField ID="hid_ctrl" runat="server" />
            <asp:HiddenField ID="hidIsTer" runat="server" />
            <tlk:RadTextBox ID="txtEmployeeCODE1" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 150px">
            <%# Translate("Họ và tên ")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtFullName" runat="server" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td>
            <tlk:RadButton runat="server" Text="Thêm" ID="btnAdd" RenderMode="Lightweight" OnClientClicking="btnadd"  AutoPostBack="false">
                <Icon PrimaryIconCssClass="rbAdd"></Icon>
                    </tlk:RadButton>
        </td>
    </tr>
</table>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1050px"
            Height="300px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        var enableAjax = true;
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function OnClientClose(oWnd, args) {
            var arg = args.get_argument();
            if (arg == '1') {
                location.reload();
            }
        }
        function btnadd(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var ctrl = $get("<%= hid_ctrl.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi làm Hồ sơ lương';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            //oWindow = radopen('/Dialog.aspx?mid=Profile&fid='+ctrl+'&group=Business&empID=' + empID+ '&Is_dis=dis_emp', "rwPopup");
            oWindow = radopen(ctrl, "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }
    </script>
</tlk:RadScriptBlock>
