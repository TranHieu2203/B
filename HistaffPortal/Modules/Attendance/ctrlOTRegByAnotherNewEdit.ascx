<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOTRegByAnotherNewEdit.ascx.vb"
    Inherits="Attendance.ctrlOTRegByAnotherNewEdit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidValid" runat="server" />
<asp:HiddenField ID="hidStatus" runat="server" />
<asp:HiddenField ID="hid100" runat="server" />
<asp:HiddenField ID="hid150" runat="server" />
<asp:HiddenField ID="hid200" runat="server" />
<asp:HiddenField ID="hid210" runat="server" />
<asp:HiddenField ID="hid270" runat="server" />
<asp:HiddenField ID="hid300" runat="server" />
<asp:HiddenField ID="hid390" runat="server" />
<asp:HiddenField ID="hidTotal" runat="server" />
<asp:HiddenField ID="hidSignId" runat="server" />
<asp:HiddenField ID="hidTimeCOEff_S" runat="server" />
<asp:HiddenField ID="hidTimeCOEff_E" runat="server" />
<asp:HiddenField ID="hidHourTotalNight" runat="server" />
<asp:HiddenField ID="hidHourTotalDay" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidOrgSlt" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<asp:Label runat="server" ID="lbStatus" ForeColor="Red"></asp:Label>
<table class="table-form">
    <tr>
        <td colspan="6">
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b style="color: red"><%# Translate("Thông tin người đăng ký hộ")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Họ tên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtFullName" ReadOnly="true"></tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Phòng ban")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtDepartment" ReadOnly="true"></tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mã nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtEmpCode" ReadOnly="true"></tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Chức danh")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTitle" ReadOnly="true"></tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b style="color: red"><%# Translate("Thông tin nhân viên")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb" style="width: 200px">
            <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEmployeeCode"  runat="server" Width="130px" AutoPostBack="true">
                <ClientEvents OnKeyPress="OnKeyPress" />
            </tlk:RadTextBox>
            <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                Width="40px">
            </tlk:RadButton>
            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
        </td>
        <td class="lb" style="width: 200px">
            <asp:Label ID="lbEmployeeName" runat="server" Text="<%$ Translate: Họ tên nhân viên %>"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Đơn vị %>"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" SkinID="ReadOnly">
            </tlk:RadTextBox>
        </td>
        <td class="lb" style="width: 200px">
            <asp:Label ID="lbTITLE" runat="server" Text="<%$ Translate: Chức danh %>"></asp:Label>
        </td>
        <td>
            <tlk:RadTextBox ID="txtEmpTitle" runat="server" ReadOnly="True" SkinID="ReadOnly">
            </tlk:RadTextBox>
        </td>
    </tr>

    <tr>
        <td colspan="6">
            <b style="color: red"><%# Translate("Thông tin giờ làm thêm thực tế")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày làm thêm")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdRegDate" AutoPostBack="true"></tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdRegDate"
                runat="server" ErrorMessage="<%$ Translate: Chưa chọn ngày làm thêm. %>"
                ToolTip="<%$ Translate: Chưa chọn ngày làm thêm. %>"></asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Loại làm thêm")%>
        </td>
        <td>
            <tlk:RadComboBox ID="cboTypeOT" runat="server" AutoPostBack="true" CausesValidation="false">
            </tlk:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ký hiệu công")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtSignCode" ReadOnly="true"></tlk:RadTextBox>
        </td>
        <td class="lb" id="Hide_ot" runat="server">
            <%# Translate("Hệ số")%><span id="input_data" runat="server" class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadComboBox ID="cbohs_ot" runat="server">
            </tlk:RadComboBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cbohs_ot"
                runat="server" ErrorMessage="<%$ Translate: Chưa chọn chọn hệ số làm thêm. %>"
                ToolTip="<%$ Translate: Chưa chọn chọn hệ số làm thêm. %>"> </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Giờ bắt đầu LV")%>
        </td>
        <td>
            <tlk:RadTimePicker runat="server" ID="txtHour_Start" Enabled="false">
                <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                </DateInput>
            </tlk:RadTimePicker>
        </td>
        <td class="lb">
            <%# Translate("Giờ kết thúc LV")%>
        </td>
        <td>
            
            <tlk:RadTimePicker runat="server" ID="txtHour_End" Enabled="false">
                <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                </DateInput>
            </tlk:RadTimePicker>
        </td>
         <td>
            <asp:CheckBox ID="chkPassDay" runat="server" Text="<%$ Translate: Qua ngày hôm sau %>" Enabled="false"/>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Giờ làm thêm:")%><span class="lbReq">*</span>
        </td>
        <td colspan="3"></td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("ĐK OT trước ca:")%><span class="lbReq">*</span>
        </td>
        <td>
            <%# Translate("Từ:")%>
            <tlk:RadNumericTextBox runat="server" ID="rntbFromAM" SkinID="Number" Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ:")%>
            <tlk:RadComboBox runat="server" ID="cboFromAM" Width="55px"></tlk:RadComboBox>
            <%# Translate(" phút:")%>
        </td>
        <td></td>
        <td colspan="2" style="padding-left: 65px;">
            <%# Translate("Đến:")%>
            <tlk:RadNumericTextBox runat="server" ID="rntbToAM" SkinID="Number" Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ:")%>
            <tlk:RadComboBox runat="server" ID="cboToAM" Width="55px"></tlk:RadComboBox>
            <%# Translate(" phút:")%>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("ĐK OT sau ca:")%><span class="lbReq">*</span>
        </td>
        <td>
            <%# Translate("Từ:")%>
            <tlk:RadNumericTextBox runat="server" ID="rntbFromPM" SkinID="Number" Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ:")%>
            <tlk:RadComboBox runat="server" ID="cboFromPM" Width="55px"></tlk:RadComboBox>
            <%# Translate(" phút:")%>
        </td>
        <td>
            <asp:CheckBox ID="chkIsFrHourAfter" runat="server" Text="<%$ Translate: Qua ngày hôm sau %>" onclick="cboptions_CheckedChanged(this);"/>
        </td>
        <td colspan="2" style="padding-left: 65px;">
            <%# Translate("Đến:")%>
            <tlk:RadNumericTextBox runat="server" ID="rntbToPM" SkinID="Number" Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ:")%>
            <tlk:RadComboBox runat="server" ID="cboToPM" Width="55px"></tlk:RadComboBox>
            <%# Translate(" phút:")%>
        </td>
        <td>
            <asp:CheckBox ID="chkIsToHourAfter" runat="server" Text="<%$ Translate: Qua ngày hôm sau %>" />
        </td>
    </tr>
    <%--<tr>
        <td class="lb">
            <%# Translate("Loại làm thêm")%><span class="lbReq">*</span>
        </td>
        <td colspan="3">
            <tlk:RadComboBox runat="server" ID="cboTypeOT"></tlk:RadComboBox>
        </td>
    </tr>--%>
    <tr>
        <td class="lb">
            <%# Translate("Lý do làm thêm:")%><span class="lbReq">*</span>
        </td>
        <td colspan="4">
            <tlk:RadTextBox runat="server" ID="txtNote" Rows="3" Width="100%"></tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNote"
                runat="server" ErrorMessage="<%$ Translate: Overtime reason is require. %>"
                ToolTip="<%$ Translate: Overtime reason is require. %>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox runat="server" ID="chkOtherOrg" Text="OT cho Đơn vị/phòng ban khác thì check vào"  AutoPostBack="true" CausesValidation="false" />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <asp:Label runat="server" ID="lblOrgOT" Text="Đơn vị/phòng ban làm thêm" Visible="false"></asp:Label><%--<span class="lbReq"></span>--%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtOrgID" runat="server" ReadOnly="True" Width="130px" Visible="false">
            </tlk:RadTextBox>
            <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false"  Visible="false"/>

        </td>
    </tr>
    <tr style="display: none">
        <td class="lb">
            <%# Translate("Tổng số giờ làm thêm trong năm:")%>
        </td>
        <td>
            <%--<tlk:RadNumericTextBox runat="server" ID="rntTotalAccumulativeOTHours" SkinID="Number"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" ReadOnly="true">
            </tlk:RadNumericTextBox>--%>
            <tlk:RadTextBox runat="server" ID="rntTotalAccumulativeOTHours" SkinID="Number" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
    </tr>
</table>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function clientButtonClicking(sender, args) {
            //if (args.get_item().get_commandName() == "CANCEL") {
            //    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistration');
            //    args.set_cancel(true);
            //}
        }
        function OpenInNewTab(url) {
            window.location.href = url;
        }
        //function clearSelectRadcombo(cbo) {
        //    if (cbo) {
        //        cbo.clearItems();
        //        cbo.clearSelection();
        //        cbo.set_text('');
        //    }
        //}
        //function clearSelectRadtextbox(cbo) {
        //    if (cbo) {
        //        cbo.clear();
        //    }
        //}
        function cboptions_CheckedChanged(chk) {
            debugger;
            var ToHourAfter = $("#<%=chkIsToHourAfter.ClientID()%>");
            if (chk.checked) {
                ToHourAfter.attr('checked', 'checked');
            }
        }
        function OnKeyPress(sender, eventArgs) 
        { 
           var c = eventArgs.get_keyCode(); 
           if (c == 13) 
           { 
             document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);            winH = $(window).height() - 210;            winW = $(window).width() - 90;            $("#ctl00_MainContent_ctl00_MainContent_Panel1Panel").stop().animate({ height: winH, width: winW }, 0);            Sys.Application.add_load(SizeToFitMain);
        }        SizeToFitMain();        $(document).ready(function () {
            SizeToFitMain();
        });        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
