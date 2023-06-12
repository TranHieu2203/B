<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTime_TimeSheet_RiceEdit.ascx.vb"
    Inherits="Attendance.ctrlTime_TimeSheet_RiceEdit" %>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <fieldset style="padding: 0px !important;border-radius: 0px;">
            <legend>
                <%# Translate("Sửa bảng công cơm")%>
            </legend>
            <table class="table-form"  onkeydown="return (event.keyCode!=13)">
                <tr>
                    <td class="lb" style="width: 100px">
                        <%# Translate("Mã nhân viên")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" ReadOnly="True" Width="140px">
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 120px">
                        <%# Translate("Họ tên")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtName" SkinID="ReadOnly" runat="server" ReadOnly="True" Width="140px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 80px">
                        <%# Translate("Chức danh")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtChucDanh" SkinID="ReadOnly" runat="server" ReadOnly="True" Width="140px">
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb" style="width: 100px">
                        <%# Translate("Đơn vị")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtDonVi" SkinID="ReadOnly" runat="server" ReadOnly="True" Width="140px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="width: 120px">
                        <%# Translate("Tiền ăn điều chỉnh")%><span class="lbReq">*</span>
                    </td>
                    <td>
                         <tlk:RadNumericTextBox ID="txtRiceEdit" SkinID="Money1" runat="server"></tlk:RadNumericTextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRiceEdit"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tiền ăn điều chỉnh. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập tiền ăn điều chỉnh. %>">
                        </asp:RequiredFieldValidator>
                    </td>
                   <td class="lb" style="width: 120px">
                        
                    </td>
                    <td>                        
                    </td>
                </tr>
            </table>
        </fieldset>
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
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
        }
        
    </script>
</tlk:RadCodeBlock>
