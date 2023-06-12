<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_LeavePaymentNewEdit.ascx.vb"
    Inherits="Attendance.ctrlAT_LeavePaymentNewEdit" %>
    <%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpId" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<table class="table-form">
    <tr>
        <td colspan="6">
            <b style="color: red"><%# Translate("Thông tin chung")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtEmpCode" ReadOnly="true">
                <ClientEvents OnKeyPress="OnKeyPress" />
            </tlk:RadTextBox>
            <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false" Width="40px">
            </tlk:RadButton>
            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtFullName"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Họ tên nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtFullName" AutoPostBack="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Phòng ban")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDepartment" ReadOnly="true"></tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Vị trí công việc")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTitle" ReadOnly="true"></tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b style="color: red"><%# Translate("Thông tin thanh toán")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Năm phép thanh toán ")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadNumericTextBox ID="rnYear" runat="server" NumberFormat-DecimalDigits="0" NumberFormat-AllowRounding="true" MinValue="2000">
                <NumberFormat GroupSeparator="" DecimalDigits="0" />
            </tlk:RadNumericTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnYear"
                runat="server" ErrorMessage="<%$ Translate: Chưa nhập năm phép thanh toán. %>"
                ToolTip="<%$ Translate: Chưa nhập năm phép thanh toán. %>"></asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Ngày thực hiện")%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdEffectDate" AutoPostBack="true" DateInput-CausesValidation="false"></tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdEffectDate"
                runat="server" ErrorMessage="<%$ Translate: Chưa nhập ngày thực hiện. %>"
                ToolTip="<%$ Translate: Chưa nhập ngày thực hiện. %>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
              <%# Translate("Số phép cũ hiện tại")%>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rnPreHave" ReadOnly="true"></tlk:RadNumericTextBox>
        </td>
        <td class="lb">
              <%# Translate("Thanh toán phép cũ")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rnLeaveOld" SkinID="Decimal" AutoPostBack="true" CausesValidation="false">
                <NumberFormat DecimalDigits="1" />
            </tlk:RadNumericTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rnLeaveOld"
                runat="server" ErrorMessage="<%$ Translate: Chưa nhập Thanh toán phép cũ. %>"
                ToolTip="<%$ Translate: Chưa nhập Thanh toán phép cũ. %>"></asp:RequiredFieldValidator>
        </td>
        <td class="lb">
              <%# Translate("Mức lương tính phép cũ")%>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rnSalAverage" ReadOnly="true" SkinID="Decimal"></tlk:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
              <%# Translate("Số phép mới hiện tại")%>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rnCurHave" ReadOnly="true"></tlk:RadNumericTextBox>
        </td>
        <td class="lb">
              <%# Translate("Thanh toán phép mới")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rnLeaveNew" SkinID="Decimal" AutoPostBack="true" CausesValidation="false">
                <NumberFormat DecimalDigits="1" />
            </tlk:RadNumericTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rnLeaveNew"
                runat="server" ErrorMessage="<%$ Translate: Chưa nhập Thanh toán phép mới. %>"
                ToolTip="<%$ Translate: Chưa nhập Thanh toán phép mới. %>"></asp:RequiredFieldValidator>
        </td>
        <td class="lb">
              <%# Translate("Mức lương tính phép mới")%>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rnSalNew" ReadOnly="true" SkinID="Decimal"></tlk:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
              <%# Translate("Tổng tiền thanh toán")%>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rnSalPayment" ReadOnly="true"></tlk:RadNumericTextBox>
        </td>
        <td class="lb">
              <%# Translate("Trả vào lương")%>
        </td>
        <td>
            <asp:CheckBox runat="server" ID="chkSal" AutoPostBack="true" CausesValidation="false" />
        </td>
        <td class="lb">
              <%# Translate("Tháng lương ")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtPeriodT" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ghi chú ")%>
        </td>
        <td colspan="3">
            <tlk:RadTextBox runat="server" ID="txtRemark" CausesValidation="false" txtNote="MultiLine" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
</table>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
