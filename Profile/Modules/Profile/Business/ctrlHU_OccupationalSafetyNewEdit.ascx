<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_OccupationalSafetyNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_OccupationalSafetyNewEdit" %>
<tlk:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<asp:HiddenField ID="hidIDEmp" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form"  onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%></b> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" Width="130px" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn Nhân viên %>"> 
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
                    <%# Translate("Ngày xảy ra tai nạn")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpDATE_OF_ACCIDENT" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="dpDATE_OF_ACCIDENT"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày xảy ra tai nạn %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày xảy ra tai nạn %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mô tả vụ việc")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDESCRIBED_INCIDENT" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nguyên nhân")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboREASON_ID" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusREASON_ID" ControlToValidate="cboREASON_ID" runat="server" ErrorMessage="<%$ Translate: Lý do đã chọn không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Lý do đã chọn không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mức độ thương tật")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEXTENT_OF_INJURY" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số ngày nghỉ do TN")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmHOLIDAY_ACCIDENTS" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí tai nạn")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtTHE_COST_OF_ACCIDENTS" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
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
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }
        
    </script>
</tlk:RadCodeBlock>
