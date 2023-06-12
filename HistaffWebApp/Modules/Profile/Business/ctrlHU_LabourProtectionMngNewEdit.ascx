<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_LabourProtectionMngNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_LabourProtectionMngNewEdit" %>
<asp:HiddenField ID="hidIDEmp" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" Width="130px" runat="server" ReadOnly="True"
                        SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqFirstNameVN" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Mã nhân viên %>" ToolTip="<%$ Translate: Bạn phải nhập Mã nhân viên %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Tên nhân viên")%></b>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE_NAME" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtORG" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên loại bảo hộ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLabourProtect" runat="server" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusLabourProtect" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Tên loại bảo hộ %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Tên loại bảo hộ  %>" ClientValidationFunction="cusLabourProtect">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalLabourProtect" ControlToValidate="cboLabourProtect" runat="server" ErrorMessage="<%$ Translate: Loại bảo hộ không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại bảo hội không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Cấp bậc nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSTAFF_RANK" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Size")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSize" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusLabourProtectSize" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Size. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Size.  %>" ClientValidationFunction="cusLabourProtectSize">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalSize" ControlToValidate="cboSize" runat="server" ErrorMessage="<%$ Translate: Size không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Size không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmQuantity" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tiền đặt cọc")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmDEPOSIT" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Giá trị")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnUnitPrice" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày cấp phát")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpDAYS_ALLOCATED" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cần cấp lại")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpRETRIEVE_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadButton ID="chkRETRIEVED" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="Đã thu hồi" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Ngày thu hồi")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdRecovery" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="compare_WorkPermitDate_PermitExpireDate" runat="server"
                        ToolTip="<%$ Translate: Ngày thu hồi phải lớn hơn ngày cấp phát %>" ErrorMessage="<%$ Translate: Ngày thu hồi phải lớn hơn ngày cấp phát %>"
                        Type="Date" Operator="GreaterThanEqual" ControlToCompare="dpDAYS_ALLOCATED" ControlToValidate="rdRecovery"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtSDESC" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusLabourProtect(oSrc, args) {
            var cbo = $find("<%# cboLabourProtect.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusLabourProtectSize(oSrc, args) {
            var cbo = $find("<%# cboSize.ClientID%>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

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
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }


        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboLabourProtect.ClientID %>':
                    var item = eventArgs.get_item();
                    cbo = $find('<%= rnUnitPrice.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("UNIT_PRICE"));
                    }
                    break;
                default:
                    break;
            }
        }


        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }


        
    </script>
</tlk:RadCodeBlock>
