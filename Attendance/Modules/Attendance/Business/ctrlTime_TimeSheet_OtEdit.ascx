<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTime_TimeSheet_OtEdit.ascx.vb"
    Inherits="Attendance.ctrlTime_TimeSheet_OtEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb" style="width: 100px">
                    <%# Translate("Mã nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server" SkinID="ReadOnly" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 80px">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucDanh" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDonVi" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 120px">
                    <%# Translate("Làm thêm được thanh toán (h)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtNUMBER_FACTOR_PAY" SkinID="Number" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Làm thêm chuyển nghỉ bù (h)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtNUMBER_FACTOR_CP" SkinID="Number" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 120px">
                    <%# Translate("Làm thêm tháng trước được chuyển vào nghỉ bù (h)")%>
                </td>
                <td colspan="3">
                    <tlk:RadNumericTextBox ID="txtbackup" SkinID="Number" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }
        
    </script>
</tlk:RadCodeBlock>
