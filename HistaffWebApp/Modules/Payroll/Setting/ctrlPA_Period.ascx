<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Period.ascx.vb"
    Inherits="Payroll.ctrlPA_Period" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" CheckBoxes="All" CheckChildNodes="true" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="135px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="nmrYear" runat="server" MinValue="1900" MaxLength="4"
                                SkinID="Number">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="nmrYear"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập năm %>" ToolTip="<%$ Translate: Bạn phải nhập năm %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên kỳ lương")%><span class="lbReq">*</span>
                        </td>
                        <td>
                           <tlk:RadComboBox ID="cboPeriod" runat="server" SkinID="dDropdownList" AutoPostBack="false">
                           </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboPeriod"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên kỳ lương %>" ToolTip="<%$ Translate: Bạn phải nhập tên kỳ lương %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Thời gian khóa kỳ công")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDateTimePicker runat="server" ID="rdtpDATE_LOCK" Width="100%">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"  >
                                </DateInput>
                            </tlk:RadDateTimePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdtpDATE_LOCK"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian khóa kỳ công %>" ToolTip="<%$ Translate: Bạn phải nhập Thời gian khóa kỳ công %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Thời gian gửi cảnh báo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDateTimePicker runat="server" ID="rdtpDATE_NOTI" Width="100%">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"  >
                                </DateInput>
                            </tlk:RadDateTimePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdtpDATE_NOTI"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian gửi cảnh báo %>" ToolTip="<%$ Translate: Bạn phải nhập Thời gian gửi cảnh báo %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Thời gian dừng cảnh báo")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDateTimePicker runat="server" ID="rdtpDATE_END_NOTI" Width="100%">
                                <DateInput runat="server" DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"  >
                                </DateInput>
                            </tlk:RadDateTimePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdtpDATE_END_NOTI"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian dừng cảnh báo %>" ToolTip="<%$ Translate: Bạn phải nhập Thời gian dừng cảnh báo %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="lb">
                            <%# Translate("Ngày bắt đầu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="dpStartDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                       <%-- <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="dpStartDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu %>" ToolTip="<%$ Translate: Bạn phải nhập ngày bắt đầu %>">
                            </asp:RequiredFieldValidator>
                        </td>--%>
                        <td class="lb">
                            <%# Translate("Ngày kết thúc")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="dpEndDate" runat="server">
                            </tlk:RadDatePicker>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="dpEndDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày kết thúc %>" ToolTip="<%$ Translate: Bạn phải nhập ngày kết thúc %>">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày bắt đầu phải nhỏ hơn ngày kết thúc %>"
                                ErrorMessage="<%$ Translate: Ngày bắt đầu phải nhỏ hơn ngày kết thúc %>" Type="Date"
                                Operator="GreaterThan" ControlToCompare="dpStartDate" ControlToValidate="dpEndDate"></asp:CompareValidator>
                            
                            <asp:CustomValidator ID="cvalEffedate" runat="server" ErrorMessage="<%$ Translate: Khoảng thời gian của kỳ lương đã tồn tại. %>"
                                ToolTip="<%$ Translate: Khoảng thời gian của kỳ lương đã tồn tại. %>">
                            </asp:CustomValidator>--%>
                        </td>
                    </tr>
                    <tr>
                       <%-- <td class="lb">
                            <%# Translate("Ngày công chuẩn")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtPeriodStanDard" runat="server" MinValue="0" MaxValue="31">
                                <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" DecimalSeparator="." />
                        <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPeriodStanDard"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày công chuẩn %>" ToolTip="<%$ Translate: Bạn phải nhập ngày công chuẩn %>">
                            </asp:RequiredFieldValidator>
                        </td>--%>
                        <td class="lb" style="display:none">
                            <%# Translate("Ngày chi thưởng")%><span class="lbReq">*</span>
                        </td>
                        <td style="display:none">
                            <tlk:RadDatePicker ID="dpBonusDate" runat="server">
                            </tlk:RadDatePicker>
                            <%--<asp:RequiredFieldValidator ID="cvalBonusDate" ControlToValidate="dpBonusDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày chi thưởng %>" ToolTip="<%$ Translate: Bạn phải nhập ngày chi thưởng %>">
                            </asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" SkinID="GridSingleSelect" Height="100%" OnPreRender="rgData_PreRender">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,YEAR,PERIOD_NAME,START_DATE,END_DATE,BONUS_DATE,PERIOD_STANDARD,PERIOD_STANDARD1,REMARK,ACTFLG,MONTH,DATE_LOCK,DATE_NOTI,DATE_END_NOTI">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" SortExpression="YEAR"
                                UniqueName="YEAR" HeaderStyle-Width="50px"  DataFormatString="{0:0}"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên kỳ lương %>" DataField="PERIOD_NAME"
                                SortExpression="PERIOD_NAME" UniqueName="PERIOD_NAME" HeaderStyle-Width="250px">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian khóa kỳ công %>" DataField="DATE_LOCK"
                                SortExpression="DATE_LOCK" UniqueName="DATE_LOCK" HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian gửi cảnh báo %>" DataField="DATE_NOTI"
                                SortExpression="DATE_NOTI" UniqueName="DATE_NOTI" HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian dừng cảnh báo %>" DataField="DATE_END_NOTI"
                                SortExpression="DATE_END_NOTI" UniqueName="DATE_END_NOTI" HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                            <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="START_DATE"
                                SortExpression="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc%>" DataField="END_DATE"
                                SortExpression="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>--%>
                            
                            <%--   <tlk:GridBoundColumn HeaderText="<%$ Translate:Ngày công chuẩn %>" 
                        HeaderStyle-Width="110px" DataField="PERIOD_STANDARD1" UniqueName="PERIOD_STANDARD1" SortExpression="PERIOD_STANDARD1" />--%>
                            <%--<tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày chi thưởng%>" DataField="BONUS_DATE"
                                SortExpression="BONUS_DATE" UniqueName="BONUS_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>--%>
                           <%-- <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                SortExpression="ACTFLG" UniqueName="ACTFLG" HeaderStyle-Width="130px" />--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK" HeaderStyle-Width="100px" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Period_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Period_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Period_RadPane2';
        var validateID = 'MainContent_ctrlPA_Period_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

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
        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
