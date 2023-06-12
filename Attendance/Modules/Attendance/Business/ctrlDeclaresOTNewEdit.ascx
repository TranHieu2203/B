<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDeclaresOTNewEdit.ascx.vb"
    Inherits="Attendance.ctrlDeclaresOTNewEdit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpId" runat="server" />
<asp:HiddenField ID="hidValid" runat="server" />
<asp:HiddenField ID="hidStatus" runat="server" />
<asp:HiddenField ID="hid100" runat="server" />
<asp:HiddenField ID="hid150" runat="server" />
<asp:HiddenField ID="hid180" runat="server" />
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
            <b style="color: red"><%# Translate("Thông tin nhân viên")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mã nhân viên")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtEmpCode" AutoPostBack="true">
                <ClientEvents OnKeyPress="OnKeyPress" />
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtFullName"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
            <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false" Width="40px">
            </tlk:RadButton>
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
            <%# Translate("Họ tên")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtFullName" ReadOnly="true">
            </tlk:RadTextBox>
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
            <%# Translate("Loại làm thêm")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadComboBox ID="cboTypeOT" runat="server" AutoPostBack="true" CausesValidation="false">
            </tlk:RadComboBox>
            <asp:RequiredFieldValidator ID="reqTypeOT" ControlToValidate="cboTypeOT"
                runat="server" ErrorMessage="<%$ Translate: Chưa chọn Loại làm thêm. %>"
                ToolTip="<%$ Translate: Chưa chọn Loại làm thêm. %>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ca làm việc")%>
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
            <%# Translate("Giờ bắt đầu")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTimePicker runat="server" ID="rdHours_Start" Enabled="false">
                <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                </DateInput>
            </tlk:RadTimePicker>
        </td>
        <td class="lb">
            <%# Translate("Giờ kết thúc")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTimePicker runat="server" ID="rdHours_Stop" Enabled="false">
                <DateInput DateFormat="hh:mm tt" DisplayDateFormat="hh:mm tt">
                </DateInput>
            </tlk:RadTimePicker>
        </td>
        <td style="display:none">
            <asp:CheckBox ID="chkPassDay" runat="server" Text="<%$ Translate: Qua ngày hôm sau %>" Enabled="false" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <b style="color: red"><%# Translate("Khung giờ làm thêm")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("ĐK OT trước ca Từ:")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rntbFromAM" SkinID="Number" Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0" MaxValue="24" oninput="SumOT();" onclick="SumOT();">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ")%>
            <tlk:RadComboBox runat="server" ID="cboFromAM" Width="55px" OnClientSelectedIndexChanged="SumOT" oninput="SumOT();" onclick="SumOT();"  ></tlk:RadComboBox>
            <%# Translate(" phút")%>
        </td>
        <td colspan="2" style="padding-left: 65px;">&emsp;&emsp;&emsp;&emsp;&emsp;<%# Translate("Đến:")%>&emsp;<span class="lbReq">*</span>
            <tlk:RadNumericTextBox runat="server" ID="rntbToAM" SkinID="Number" Width="50px" oninput="SumOT();" onclick="SumOT();"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MaxValue="24" MinValue="0">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ")%>
            <tlk:RadComboBox runat="server" ID="cboToAM" Width="55px" OnClientSelectedIndexChanged="SumOT" oninput="SumOT();" onclick="SumOT();"></tlk:RadComboBox>
            <%# Translate(" phút")%>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("ĐK OT sau ca Từ:")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rntbFromPM" SkinID="Number" Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0" MaxValue="24" oninput="SumOT();" onclick="SumOT();">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ")%>
            <tlk:RadComboBox runat="server" ID="cboFromPM" Width="55px" OnClientSelectedIndexChanged="SumOT" oninput="SumOT();" onclick="SumOT();"></tlk:RadComboBox>
            <%# Translate(" phút")%>
        </td>

        <td colspan="2" style="padding-left: 65px;">&emsp;&emsp;&emsp;&emsp;&emsp;<%# Translate("Đến:")%>&emsp;<span class="lbReq">*</span>
            <tlk:RadNumericTextBox runat="server" ID="rntbToPM" SkinID="Number" Width="50px"
                NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="2" MinValue="0" MaxValue="24" oninput="SumOT();" onclick="SumOT();">
            </tlk:RadNumericTextBox>
            <%# Translate(" giờ")%>
            <tlk:RadComboBox runat="server" ID="cboToPM" Width="55px" OnClientSelectedIndexChanged="SumOT" oninput="SumOT();" onclick="SumOT();"></tlk:RadComboBox>
            <%# Translate(" phút")%>
        </td>


    </tr>
    <tr style="display:none">
        <td></td>
        <td>
            <asp:CheckBox ID="chkIsFrHourAfter" runat="server" Text="<%$ Translate: Qua ngày hôm sau %>" onclick="cboptions_CheckedChanged(this);" />
        </td>
        <td></td>
        <td>
            <asp:CheckBox ID="chkIsToHourAfter" runat="server" Text="<%$ Translate: Qua ngày hôm sau %>" onclick="cboptions_CheckedChanged(this);" />
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
        <td colspan="3">
            <tlk:RadTextBox runat="server" ID="txtNote" Rows="3" Width="100%"></tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNote"
                runat="server" ErrorMessage="<%$ Translate:Bạn cần phải nhập lý do làm thêm.%>"
                ToolTip="<%$ Translate: Bạn cần phải nhập lý do làm thêm. %>"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Tổng số giờ làm thêm")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtSumOT" ReadOnly="true" Text="0"></tlk:RadTextBox>
        </td>
        <td class="lb" style="display: none">
            <asp:Label runat="server" ID="lblOrgOT" Text="Đơn vị/phòng ban làm thêm"></asp:Label><span class="lbReq"></span>
        </td>
        <td style="display: none">
            <tlk:RadTextBox ID="txtOrgID" runat="server" ReadOnly="True" Width="130px">
            </tlk:RadTextBox>
            <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false" />

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
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            //if (args.get_item().get_commandName() == "CANCEL") {
            //    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistration');
            //    args.set_cancel(true);
            //}
        }
        function OpenInNewTab(url) {
            window.location.href = url;
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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
            SumOT();
        };
        function SumOT() {
            debugger;
            var FromHourAM = document.getElementById("<%=rntbFromAM.ClientID %>").value;
            var FromMinAM = document.getElementById("<%=cboFromAM.ClientID %>").value;
            var ToHourAM = document.getElementById("<%=rntbToAM.ClientID %>").value;
            var ToMinAM = document.getElementById("<%=cboToAM.ClientID %>").value;
            var FromHourPM = document.getElementById("<%=rntbFromPM.ClientID %>").value;
            var FromMinPM = document.getElementById("<%=cboFromPM.ClientID %>").value;
            var ToHourPM = document.getElementById("<%=rntbToPM.ClientID %>").value;
            var ToMinPM = document.getElementById("<%=cboToPM.ClientID %>").value;
            if (FromHourAM == "") {
                FromHourAM = 0;
            };
            if (FromMinAM == "") {
                FromMinAM = 0;
            };
            if (ToHourAM == "") {
                ToHourAM = 0;
            };
            if (ToMinAM == "") {
                ToMinAM = 0;
            };
            if (FromHourPM == "") {
                FromHourPM = 0;
            };
            if (FromMinPM == "") {
                FromMinPM = 0;
            };
            if (ToHourPM == "") {
                ToHourPM = 0;
            };
            if (ToMinPM == "") {
                ToMinPM = 0;
            };

            var ToAM60 = ToMinAM / 60;
            var FromAM60 = FromMinAM / 60;

            function myFunc(total, num) {
                return total + num;
            }


            const numbersFromAM = [FromAM60, parseInt(FromHourAM)];
            SumFromAM = numbersFromAM.reduce(myFunc);
            const numbersToAM = [ToAM60, parseInt(ToHourAM)];
            SumToAM = numbersToAM.reduce(myFunc);

            var SogioAM = SumToAM - SumFromAM;

            var ToPM60 = ToMinPM / 60;
            var FromPM60 = FromMinPM / 60;

            const numbersFromPM = [FromPM60, parseInt(FromHourPM)];
            SumFromPM = numbersFromPM.reduce(myFunc);
            const numbersToPM = [ToPM60, parseInt(ToHourPM)];
            SumToPM = numbersToPM.reduce(myFunc);
            //var ToHourAfter = $("#<%=chkIsFrHourAfter.ClientID()%>").checked;
            var SogioPM
            var chkFR = document.getElementById("<%=chkIsFrHourAfter.ClientID()%>").checked;
            var chkTO = document.getElementById("<%=chkIsToHourAfter.ClientID()%>").checked;
            if (chkFR == true && chkTO == true) {
                SogioPM = (SumToPM + 24) - (SumFromPM + 24);
            } else if (chkFR == true && chkTO == false) {
                SogioPM = (SumToPM) - (SumFromPM + 24);
            }
            else if (chkFR == false && chkTO == true) {
                SogioPM = (SumToPM + 24) - (SumFromPM);
            }
            else {
                SogioPM = SumToPM - SumFromPM;
            };


            var SumTotal = SogioPM + SogioAM;
            var CheckError = isNaN(SumTotal);
            if (CheckError == true || SumTotal < 0) { SumTotal = 0; };

            document.getElementById("<%=txtSumOT.ClientID %>").value = SumTotal;
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
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
