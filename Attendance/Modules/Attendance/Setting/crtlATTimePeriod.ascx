<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="crtlATTimePeriod.ascx.vb"
    Inherits="Attendance.crtlATTimePeriod" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="185px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server"/>
                <table class="table-form">
                <tr>
                     <td class="lb">
                            <%# Translate("Tháng hiệu lực")%><span class="lbReq">*</span>
                     </td>
                    <td>
                        <tlk:RadMonthYearPicker runat="server" ID="rdEffectMonth" TabIndex="4" Culture="en-US">
                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="MM/yyyy">
                            </DateInput>
                        </tlk:RadMonthYearPicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEffectMonth"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy chọn tháng hiệu lực. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Đối tượng nhân viên")%><span class="lbReq">*</span>
                     </td>
                    <td>
                        <tlk:RadComboBox ID="cboObjEmployee" runat="server"></tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboObjEmployee"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy chọn đối tượng nhân viên. %>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                     <td class="item-head" colspan="4">
                            <%# Translate("Chu kỳ chấm công")%>
                     </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="lb">
                            <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                     </td>
                    <td>
                        <tlk:RadNumericTextBox ID="ntxtFromDate" runat="server"></tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ntxtFromDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập từ ngày. %>"></asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                            <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                     </td>
                    <td>
                        <tlk:RadNumericTextBox ID="ntxtToDate" runat="server"></tlk:RadNumericTextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ntxtToDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập Đến ngày. %>"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="lb"></td>
                    <td>
                        <asp:CheckBox ID="chkFromDate" runat="server" CssClass="cheb" Text="Từ ngày tháng trước"  onclick="NotCheck(this.id)" />
                    </td>
                    <td class="lb"></td>
                    <td>
                        <asp:CheckBox ID="chkToDate" runat="server" CssClass="cheb" Text="Đến ngày cuối tháng" onclick="enableTextbox(this.id)" />
                    </td>
                </tr>
                <tr>
                     <td class="lb"> <%# Translate("Ghi chú")%></td>
                    <td colspan="3">
                        <tlk:RadTextBox ID="txtNote" runat="server" Width ="100%"/>
                    </td>
                </tr>  
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EFFECTMONTH,OBJ_EMPLOYEE_ID,FROMDATE_PERIOD,TODATE_PERIOD,FROMDATE_BEFOREMONTH,TODATE_ENDMONTH,NOTE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tháng hiệu lực%>" DataField="EFFECTMONTH"
                                UniqueName="EFFECTMONTH" SortExpression="EFFECTMONTH" DataFormatString="{0:MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng chấm công%>" DataField="OBJECT_ATTENDACE_NAME"
                                UniqueName="OBJECT_ATTENDACE_NAME" SortExpression="OBJECT_ATTENDACE_NAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chu kỳ từ ngày%>" DataField="FROMDATE_PERIOD"
                                UniqueName="FROMDATE_PERIOD" SortExpression="FROMDATE_PERIOD" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chu kỳ đến ngày %>" DataField="TODATE_PERIOD"
                                UniqueName="TODATE_PERIOD" SortExpression="TODATE_PERIOD" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày tháng trước%>" DataField="FROMDATE_BEFOREMONTHNAME"
                                UniqueName="FROMDATE_BEFOREMONTHNAME" SortExpression="FROMDATE_BEFOREMONTHNAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày cuối tháng%>" DataField="TODATE_ENDMONTHNAME"
                                UniqueName="TODATE_ENDMONTHNAME" SortExpression="TODATE_ENDMONTHNAME" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú%>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        var splitterID = 'ctl00_MainContent_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlATTimePeriod_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlATTimePeriod_RadPane2';
        var validateID = 'MainContent_crtlATTimePeriod_valSum';
        var oldSize = $('#' + pane1ID).height();

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
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgData.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
}

function onRequestStart(sender, eventArgs) {
    eventArgs.set_enableAjax(enableAjax);
    enableAjax = true;
}
function displayDecimalFormat(sender, args) {
    sender.set_textBoxValue(sender.get_value().toString());
}

function enableTextbox(checkbox) {
    document.getElementById('<%= ntxtToDate.ClientID%>').disabled = document.getElementById(checkbox).checked;
    if (document.getElementById(checkbox).checked) {
        $find("<%= ntxtToDate.ClientID%>").set_value(null);
        document.getElementById('<%= chkFromDate.ClientID%>').checked = false; 
    }
}

     function NotCheck(checkbox) {
         if (document.getElementById(checkbox).checked) {
             document.getElementById('<%= ntxtToDate.ClientID%>').disabled = false;
             document.getElementById('<%= chkToDate.ClientID%>').checked = false;
            }
        }

    </script>
</tlk:RadCodeBlock>
