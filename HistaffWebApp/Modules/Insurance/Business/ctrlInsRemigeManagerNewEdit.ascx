<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsRemigeManagerNewEdit.ascx.vb"
    Inherits="Insurance.ctrlInsRemigeManagerNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin chế độ bảo hiểm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE_CODE" SkinID="ReadOnly" Enabled="false" runat="server"
                        Width="130px" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="txtEMPLOYEE_CODE"
                        AutoPostBack="true" CausesValidation="false" runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên để làm chế độ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Họ và tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEMPLOYEE_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtORG_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức vụ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE_NAME" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số sổ BHXH")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSoBHXH" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số thẻ BHYT")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTheBHXH" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày sinh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNgaySinh" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCapNS" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin hưởng chế độ")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại chế độ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRemigeType" AutoPostBack="true" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboRemigeType" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn loại chế độ. %>"  ToolTip="<%$ Translate: Bạn phải chọn loại chế độ. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusRemigeType" runat="server" ErrorMessage="<%$ Translate: Loại chế độ không tồn tại hoặc đã ngừng áo dụng. %>"
                        ToolTip="<%$ Translate: Loại chế độ không tồn tại hoặc đã ngừng áp dụng. %>" ClientValidationFunction="cusRemigeType">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalRemigeType" runat="server" ErrorMessage="<%$ Translate: Bạn đã nghỉ quá số lần quy định khám thai. %>"
                        ToolTip="<%$ Translate: Bạn đã nghỉ quá số lần quy định khám thai. %>">
                    </asp:CustomValidator>
                </td>
                <td>
                </td>
                <td>
                    <tlk:RadButton AutoPostBack="true" ID="chkIS_ATHOME" CausesValidation="false" ToggleType="Radio"
                        GroupName="gpLoaiNghi" Checked="true" ButtonType="ToggleButton" runat="server"
                        Text="Nghỉ tại nhà">
                    </tlk:RadButton>
                    <tlk:RadButton AutoPostBack="true" ID="chkis_FOCUSED" ToggleType="Radio" CausesValidation="false"
                        GroupName="gpLoaiNghi" ButtonType="ToggleButton" runat="server" Text="Nghỉ tập trung">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpFromDate" runat="server" CausesValidation="false" AutoPostBack="true"
                        DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="dpFromDate"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập từ ngày. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dpToDate" runat="server" CausesValidation="false" AutoPostBack="true"
                        DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cvalToDate" ControlToValidate="dpToDate" runat="server"
                        ErrorMessage="<%$ Translate: Thời gian hiệu lực bị trùng với 1 loại chế độ khác. %>"
                        ToolTip="<%$ Translate: Thời gian hiệu lực bị trùng với 1 loại chế độ khác. %>">
                    </asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="dpToDate"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập đến ngày. %>"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày %>"
                        ErrorMessage="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày %>" Type="Date" Operator="GreaterThanEqual"
                        ControlToCompare="dpFromDate" ControlToValidate="dpToDate"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số ngày")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmNUM_DATE" SkinID="Number" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày sinh con")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDATE_OFF_BIRT" runat="server" DateInput-DateFormat="dd/MM/yyyy">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ToolTip="<%$ Translate: Ngày sinh con phải lớn hơn từ ngày. %>"
                        ErrorMessage="<%$ Translate: Ngày sinh con phải lớn hơn từ ngày. %>" Type="Date"
                        Operator="GreaterThanEqual" ControlToCompare="dpFromDate" ControlToValidate="rdDATE_OFF_BIRT"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ToolTip="<%$ Translate: Ngày sinh con phải nhỏ hơn đến ngày %>"
                        ErrorMessage="<%$ Translate: Ngày sinh con phải nhỏ hơn đến ngày %>" Type="Date"
                        Operator="GreaterThan" ControlToCompare="rdDATE_OFF_BIRT" ControlToValidate="dpToDate"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên con")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBABY_NAME" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Con thứ mấy")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmNUM_BABY" SkinID="Number" MaxLength="250" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số ngày lũy kế")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmACCUMULATED_DATE" ReadOnly="true" SkinID="ReadOnly"
                        MaxLength="18" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tiền lương tính chế độ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmALLOWANCE_SAL" SkinID="ReadOnly" ReadOnly="true" MaxLength="18"
                        runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tiền trợ cấp")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmALLOWANCE_MONEY" SkinID="Money" MaxLength="18" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tiền trợ cấp chỉnh sửa")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmALLOWANCE_MONEY_EDIT" SkinID="Money" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thời điểm tính trợ cấp")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTIME_ALLOWANCE" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Điều kiện hưởng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCONDITION_ALLOWANCE" runat="server" Text="Bình thường">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số ngày bị xuất toán")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmAPPROVAL_NUM" SkinID="Number" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tháng biến động")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdThangBiendong" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                        DateInput-DateFormat="dd/MMM/yyyy">
                    </tlk:RadMonthYearPicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdThangBiendong"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tháng biến động. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập tháng biến động. %>"> </asp:RequiredFieldValidator>
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
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function cusRemigeType(oSrc, args) {
            var cbo = $find("<%# cboRemigeType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        $(document).ready(function () {
            registerOnfocusOut('ctl00_MainContent_ctrlInsRemigeManagerNewEdit_RadSplitter3');
        });
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
        var enableAjax = true;
        var oldSize = 0;
        function clientButtonClicking(sender, args) {
//            if (args.get_item().get_commandName() == 'CANCEL') {
//                getRadWindow().close(null);
//                args.set_cancel(true);
//            }
        }
       
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
      
    </script>
</tlk:RadCodeBlock>
