<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDSVMNewEdit.ascx.vb"
    Inherits="Attendance.ctrlDSVMNewEdit" %>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />

<%--<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />--%>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidOrgID1" runat="server" />
<asp:HiddenField ID="hidShiftID" runat="server" />
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<tlk:RadToolBar ID="tbarDMVS" runat="server" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<table class="table-form" onkeydown="return (event.keyCode!=13)">
    <tr>
        <td colspan="4">
            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <b style="color: red"><%# Translate("Thông tin nhân viên")%></b>
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
        <td colspan="4">
            <b style="color: red"><%# Translate("Thông tin giờ làm thêm thực tế")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày đăng ký")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker ID="rdWorkingday" runat="server" AutoPostBack="true" DateInput-CausesValidation="false">
            </tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdWorkingday"
                runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn ngày đăng ký %>" ToolTip="<%$ Translate: Bạn chưa chọn ngày đăng ký %>"> </asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Loại đăng ký")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadComboBox ID="cboTypeDmvs" runat="server">
            </tlk:RadComboBox>
            <asp:CustomValidator ID="cusTypeDmvs" runat="server" ErrorMessage="<%$ Translate: Chưa chọn loại đăng ký. %>"
                ToolTip="<%$ Translate: Chưa chọn loại đăng ký. %>" ClientValidationFunction="cusTypeDmvs">
            </asp:CustomValidator>
        </td>
        <td class="lb">
            <%# Translate("Loại thông tin")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadComboBox ID="cboRegistInfo" runat="server">
            </tlk:RadComboBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboRegistInfo"
                runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn loại thông tin %>" ToolTip="<%$ Translate: Bạn chưa chọn loại thông tin %>"> </asp:RequiredFieldValidator>

        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ký hiệu công")%>
        </td>
        <td>
            <tlk:RadTextBox ID="txtShiftCode" runat="server" ReadOnly="true" SkinID="ReadOnly">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Giờ bắt đầu")%>
        </td>
        <td>
            <tlk:RadTimePicker ID="rdShiftStart" runat="server" DateInput-ReadOnly="true" Enabled="false" SkinID="Readonly" 
                 DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
            </tlk:RadTimePicker>
        </td>
        <td class="lb">
            <%# Translate("Giờ kết thúc")%>
        </td>
        <td>
            <tlk:RadTimePicker ID="rdShiftEnd" runat="server" DateInput-ReadOnly="true" Enabled="false" SkinID="Readonly"
                 DateInput-DateFormat="HH:mm" TimeView-TimeFormat="HH:mm">
            </tlk:RadTimePicker>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Từ giờ")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTimePicker ID="txtTuGio" runat="server" ShowPopupOnFocus="true" onchange="cal_hours_num()">
                <%--<ClientEvents OnDateSelected="OnDateSelected_TuGio" />--%>
            </tlk:RadTimePicker>
            <asp:RequiredFieldValidator ID="rq1" ControlToValidate="txtTuGio"
                runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập giờ %>" ToolTip="<%$ Translate: Bạn chưa nhập giờ %>"> </asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Đến giờ")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTimePicker ID="txtDenGio" runat="server" ShowPopupOnFocus="true" onchange="cal_hours_num()">
                <%--<ClientEvents OnDateSelected="OnDateSelected_DenGio" />--%>
            </tlk:RadTimePicker>
            <asp:RequiredFieldValidator ID="rq2" ControlToValidate="txtDenGio"
                runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập giờ %>" ToolTip="<%$ Translate: Bạn chưa nhập giờ %>"> </asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Số phút")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadNumericTextBox MinValue="0" MaxValue="999999999" ID="txtMinute" runat="server">
                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
            </tlk:RadNumericTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtMinute"
                runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập số phút! %>" ToolTip="<%$ Translate: Bạn chưa nhập số phút! %>"> </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Lý do")%><span class="lbReq">*</span>
        </td>
        <td colspan="3">
            <tlk:RadTextBox ID="txtGhiChu" SkinID="Textbox1023" runat="server" Width="100%" Height="40px">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="reqtxtGhiChu" ControlToValidate="txtGhiChu"
                runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập Lý do! %>" ToolTip="<%$ Translate: Bạn chưa nhập Lý do! %>"> </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox runat="server" ID="chkOtherOrg" Text="Quẹt thẻ đơn vị/phòng ban khác thì check vào"  AutoPostBack="true" CausesValidation="false" />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <asp:Label runat="server" ID="lblOrgOT" Text="Đơn vị checkin" Visible="false"></asp:Label><%--<span class="lbReq"></span>--%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" Width="130px" Visible="false" />
            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" Visible="false" />
        </td>
    </tr>
</table>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function clientButtonClicking(sender, args) {
            //if (args.get_item().get_commandName() == "CANCEL") {
            //    OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlOTRegistration');
            //    args.set_cancel(true);
            //}
        }
        function cal_hours_num() {
            try {
                debugger;
                var a1 = $find("<%= txtTuGio.ClientID%>")._dateInput._text;
                var a2 = $find("<%= txtDenGio.ClientID%>")._dateInput._text;
                if (a1 != "" & a2 != "") {

                    var a11 = a1.split(":", 1);
                    var a12 = a1.slice(3, 5);

                    var a21 = a2.split(":", 1);
                    var a22 = a2.slice(3, 5);

                    var s = (a11 * 60 + parseFloat(a12));
                    var s1 = (a21 * 60 + parseFloat(a22));


                    //document.getElementById("demo").innerHTML = (res * 60 + parseFloat(a)) / 60;

                    if (s != 0 & s1 != 0 & s1 > s) {
                        var s2 = Math.round((s1 - s) * 100) / 100;

                        $find("<%= txtMinute.ClientID%>").set_value(Math.floor(s2));
                    } else {
                        $find("<%= txtMinute.ClientID%>").set_value("");
                    }
                }
            }
            catch (err) {
                console.log(err);
            }
        }
        function cusTypeDmvs(oSrc, args) {
            var cbo = $find("<%# cboTypeDmvs.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);            winH = $(window).height() - 210;            winW = $(window).width() - 90;            $("#ctl00_MainContent_ctl00_MainContent_PagePlaceHolderPanel").stop().animate({ height: winH, width: winW }, 0);            Sys.Application.add_load(SizeToFitMain);
        }        SizeToFitMain();        $(document).ready(function () {
            SizeToFitMain();
        });        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>
