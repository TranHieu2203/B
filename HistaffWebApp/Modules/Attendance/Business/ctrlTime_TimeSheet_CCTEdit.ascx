<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTime_TimeSheet_CCTEdit.ascx.vb"
    Inherits="Attendance.ctrlTime_TimeSheet_CCTEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form"  onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucDanh" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDonVi" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromDay" EnableTyping="false" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDay" EnableTyping="false" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="rdFromDay" ControlToCompare="rdToDay"
                        Operator="LessThanEqual" Type="Date" runat="server" ErrorMessage="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày. %>"
                        ToolTip="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày. %>"> </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ca làm việc")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtShiftCode" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtShiftCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải ca làm việc. %>"></asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Loại kiểu công")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTypeManual" runat="server" DropDownWidth="400px">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalTypeManual" ControlToValidate="cboTypeManual" runat="server" ErrorMessage="<%$ Translate: Kiểu công không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Kiểu công không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
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
