<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsRegimes.ascx.vb"
    Inherits="Insurance.ctrlInsRegimes" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <div style="display: none;">
            <tlk:RadTextBox ID="txtID" Text="0" runat="server">
            </tlk:RadTextBox>
            <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
            </tlk:RadTextBox>
            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtCurrDate" runat="server">
                <NumberFormat DecimalDigits="0" />
            </tlk:RadNumericTextBox>
        </div>
        <div>
            <fieldset style="width: auto; height: auto">
                <legend>
                    <%# Translate("Thông tin nhân viên")%>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:130px;">
                            <%# Translate("MSNV")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEMPLOYEE_ID" AutoPostBack="true" runat="server">
                            </tlk:RadTextBox>
                           <tlk:RadButton  runat="server" ID="btnSearchEmp" TabIndex="7" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqtxtEMPLOYEE_ID" ControlToValidate="txtEMPLOYEE_ID"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Nhân viên. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Nhân viên. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width:110px;">
                            <%# Translate("Họ & tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtFULLNAME" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtDEP" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Vị trí CV bảo hiểm")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtPOSITION" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số sổ BHXH")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtSOCIAL_NUMBER" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số thẻ BHYT")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtHEALTH_NUMBER" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày sinh")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker DateInput-DateFormat="dd/MM/yyyy" runat="server" ID="txtDoB" TabIndex="5">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Nơi sinh")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtBirthPlace" ReadOnly="true" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <div>
            <fieldset style="width: auto; height: auto">
                <legend>
                    <%# Translate("Thông tin hưởng chế độ")%>
                </legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="width:130px;">
                            <%# Translate("Loại chế độ hưởng")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3" align="left">
                            <tlk:RadComboBox ID="ddlREGIME_ID" AutoPostBack="true" runat="server" TabIndex="6" >
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqddlREGIME_ID" ControlToValidate="ddlREGIME_ID"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Loại chế độ hưởng. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Loại chế độ hưởng. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker AutoPostBack="true" DateInput-DateFormat="dd/MM/yyyy" runat="server"
                                ID="txtFROM_DATE" TabIndex="8">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtFROM_DATE" ControlToValidate="txtFROM_DATE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Từ ngày. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Từ ngày. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb" style="width:130px;">
                            <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker AutoPostBack="true" DateInput-DateFormat="dd/MM/yyyy" runat="server"
                                ID="txtTO_DATE" TabIndex="9">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtTO_DATE" ControlToValidate="txtTO_DATE" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Đến ngày. %>" ToolTip="<%$ Translate: Bạn phải nhập Đến ngày. %>"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtTO_DATE"
                                ControlToCompare="txtFROM_DATE" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Từ ngày phải lớn hơn đến ngày %>"
                                ToolTip="<%$ Translate: Từ ngày phải lớn hơn đến ngày %>"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="lb">
                            <%# Translate("Nghỉ tập trung")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtOFF_TOGETHER" runat="server" AutoPostBack="true"
                                TabIndex="10" >
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Nghỉ tại gia đình")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtOFF_IN_HOUSE" runat="server" AutoPostBack="true"
                                TabIndex="10">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số ngày tính")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtDAY_CALCULATOR" runat="server" AutoPostBack="true"
                                TabIndex="10">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                            <asp:CustomValidator ID="cusDayCalculator" runat="server" ErrorMessage="<%$ Translate: Số ngày hưởng không vượt quá số ngày hưởng tối đa %>"
                                 ToolTip="<%$ Translate: Số ngày hưởng không vượt quá số ngày hưởng tối đa %>">
                            </asp:CustomValidator>
                        </td>
                        <td class="lb" style="display: none">
                            <%# Translate("Ngày sinh con")%>
                        </td>
                        <td style="display: none">
                            <tlk:RadDatePicker DateInput-DateFormat="dd/MM/yyyy" runat="server" ID="txtBORN_DATE"
                                TabIndex="11">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Tiền lương tính hưởng BHXH")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSUBSIDY_SALARY" runat="server"
                                TabIndex="12">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="lb">
                            <%# Translate("Tên con")%>
                        </td>
                        <td style="display: none">
                            <tlk:RadTextBox ID="txtNAME_CHILDREN" runat="server" TabIndex="13">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Con thứ mấy")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtMONEY_ADVANCE" runat="server"
                                TabIndex="14">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số ngày lũy kế")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtACCUMULATE_DAY" ReadOnly="true"
                                TabIndex="15" runat="server">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số con")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtCHILDREN_NO" runat="server"
                                TabIndex="16" AutoPostBack="true">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số tiền hưởng theo chế độ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtRegimes" ReadOnly="true"
                                TabIndex="15" runat="server">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số tiền trợ cấp được hưởng")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSUBSIDY" runat="server"
                                TabIndex="15">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số tiền trợ cấp điều chỉnh")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSUBSIDY_MODIFY"  runat="server" TabIndex="16">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" /> 
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                         <td class="lb">
                            <%# Translate("Số tiền trợ cấp tạm ứng")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtSUBSIDY_TEMP" runat="server"
                                TabIndex="15">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                       <td class="lb">
                            <%# Translate("Đợt khai báo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtDECLARE_DATE"
                                TabIndex="18" AutoPostBack="true">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqtxtDECLARE_DATE" ControlToValidate="txtDECLARE_DATE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Đợt khai báo. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Đợt khai báo. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Thời điểm tính")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtPAYROLL_DATE"
                                TabIndex="17">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Tình trạng")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCONDITION" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Hình thức nhận trợ cấp")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboFormAllow" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNOTE" runat="server" width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
                <table class="table-form">
                    <tr>
                        <td colspan="4" align="left">
                            <b>
                                <%# Translate("Thông tin BH duyệt")%></b>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb" style="width:130px;">
                            <%# Translate("Tiền BH duyệt chi")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtINS_PAY_AMOUNT" runat="server">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb" style="width:130px;">
                            <%# Translate("Ngày duyệt chi")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker DateInput-DisplayDateFormat="dd/MM/yyyy" runat="server" ID="txtPAY_APPROVE_DATE"
                                TabIndex="5">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số ngày duyệt chi")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtAPPROV_DAY_NUM" runat="server">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

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
        $(document).keypress(function (e) {
            if (e.which == 13) {
                e.preventDefault();
            }
        });
    </script>
</tlk:RadCodeBlock>
