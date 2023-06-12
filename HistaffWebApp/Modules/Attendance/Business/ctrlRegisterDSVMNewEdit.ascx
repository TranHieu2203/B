<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterDSVMNewEdit.ascx.vb"
    Inherits="Attendance.ctrlRegisterDSVMNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    
    @media only screen and (min-width: 600px) {
  #ctl00_MainContent_ctrlRegisterDSVMNewEdit_txtGhiChu_wrapper {
        width: calc(11rem + 3vw) !important;
    }
}
    @media only screen and (min-width: 1920px) {
  #ctl00_MainContent_ctrlRegisterDSVMNewEdit_txtGhiChu_wrapper {
        width: 50vh !important;
    }
}
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Ngày đăng ký")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdWorkingday" runat="server">
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
                    <%# Translate("Lý do")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboReason" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboReason"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn Lý do %>" ToolTip="<%$ Translate: Bạn chưa chọn Lý do %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Loại thông tin")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRegistInfo" runat="server"  Enabled="false">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboRegistInfo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn loại thông tin %>" ToolTip="<%$ Translate: Bạn chưa chọn loại thông tin %>"> </asp:RequiredFieldValidator>--%>

                </td>
                <td class="lb">
                    <%# Translate("Lý do chi tiết")%><span class="lbReq">*</span>
                </td>
                <td colspan="3" rowspan="2">
                    <tlk:RadTextBox ID="txtGhiChu" SkinID="Textbox1023" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqtxtGhiChu" ControlToValidate="txtGhiChu"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập Lý do! %>" ToolTip="<%$ Translate: Bạn chưa nhập Lý do! %>"> </asp:RequiredFieldValidator>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTuGio"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập Từ giờ %>" ToolTip="<%$ Translate: Bạn chưa nhập Từ giờ %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="txtDenGio" runat="server" ShowPopupOnFocus="true" onchange="cal_hours_num()">
                        <%--<ClientEvents OnDateSelected="OnDateSelected_DenGio" />--%>
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtDenGio"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập Đến giờ %>" ToolTip="<%$ Translate: Bạn chưa nhập Đến giờ %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số phút")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox MinValue="0" MaxValue="999999999" ID="txtMinute" runat="server">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                    </tlk:RadNumericTextBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtMinute"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập số phút! %>" ToolTip="<%$ Translate: Bạn chưa nhập số phút! %>"> </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Đơn vị checkin")%>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" Width="130px" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <%--<asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator3"
                        ControlToValidate="txtOrgName" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>"
                        ToolTip="<%$ Translate: Bạn phải chọn bộ phận %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgData" AllowPaging="false" runat="server"
            Height="100%">
            <MasterTableView AllowPaging="true" AllowCustomPaging="true" DataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID"
                ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID" CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmployee" runat="server" Text="<%$ Translate: Xóa nhân viên %>"
                                CausesValidation="false" CommandName="DeleteEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                        UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctl00_MainContent_PagePlaceHolderPanel';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRegisterDSVMNewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRegisterDSVMNewEdit_RadPane2';
        var validateID = 'MainContent_ctrlRegisterDSVMNewEdit_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;
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
        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
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

        function OnDateSelected_DenGio(sender, args) {
            var ctrlTuGio = $find('<%= txtTuGio.ClientID %>');
            if (ctrlTuGio.get_selectedDate() && args.get_newDate()) {
                console.log(args.get_newDate());
                console.log(ctrlTuGio.get_selectedDate());
                if (args.get_newDate() > ctrlTuGio.get_selectedDate()) {
                    var ctrlMinute = $find('<%= txtMinute.ClientID %>');
                    ctrlMinute.set_value((args.get_newDate() - ctrlTuGio.get_selectedDate()) / 1000 / 60);
                } else {
                    var ctrlMinute = $find('<%= txtMinute.ClientID %>');
                    ctrlMinute.set_value('');
                }
            }
        }

        function OnDateSelected_TuGio(sender, args) {
            var ctrlDenGio = $find('<%= txtDenGio.ClientID %>');
            if (ctrlDenGio.get_selectedDate() && args.get_newDate()) {
                if (ctrlDenGio.get_selectedDate() > args.get_newDate()) {
                    var ctrlMinute = $find('<%= txtMinute.ClientID %>');
                    ctrlMinute.set_value((ctrlDenGio.get_selectedDate() - args.get_newDate()) / 1000 / 60);
                } else {
                    var ctrlMinute = $find('<%= txtMinute.ClientID %>');
                    ctrlMinute.set_value('');
                }
            }
        }

        function cusTypeDmvs(oSrc, args) {
            var cbo = $find("<%# cboTypeDmvs.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
    </script>
</tlk:RadCodeBlock>
