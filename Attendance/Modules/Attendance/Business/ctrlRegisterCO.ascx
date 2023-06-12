<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterCO.ascx.vb"
    Inherits="Attendance.ctrlRegisterCO" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="90px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdtungay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Từ ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Từ ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" runat="server"
                                ToolTip="" Width="150px">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdDenngay"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đến ngày %>" ToolTip="<%$ Translate: Bạn phải chọn Đến ngày %>"> </asp:RequiredFieldValidator>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại nghỉ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox  ID="cboManual" runat="server" >
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox  ID="cbStatus" Width="160px"
                               runat="server" >
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                     <tr>
                         <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <asp:Label ID="lblProgram" runat="server" Font-Size="Large" Font-Bold="true">&nbsp;</asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:Label ID="lblRequest" runat="server" Font-Size="Large" Text ="..." Font-Bold="true" Visible="false">&nbsp;</asp:Label>
                        </td>
                         <td class="lb" style="padding-right: 5px; padding-left: 20px">
                            <%# Translate("CM_CTRLPROCESS_SCAN_TIME")%>&nbsp;
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="RadNumTB" MinValue="1" MaxValue="30" Width="50px"
                                Value="5" MaxLength="2" AutoPostBack="true" ShowSpinButtons="True">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                            <asp:Label ID="lblStatus" runat="server" Text ="..."  Font-Size="Large" CssClass="spancustomstatus"></asp:Label>
                         </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgRegisterLeave" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <ClientEvents OnCommand="ValidateFilter" />
                         <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="4" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC,STATUS" ClientDataKeyNames="ID,EMPLOYEE_CODE,STATUS">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" HeaderStyle-Width="120px"
                                DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" HeaderStyle-Width="200px"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số phép còn lại %>" DataField="BALANCE_NOW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BALANCE_NOW"
                                UniqueName="BALANCE_NOW">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số nghỉ bù còn lại %>" DataField="NGHIBUCONLAI"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="NGHIBUCONLAI"
                                UniqueName="NGHIBUCONLAI">
                            </tlk:GridNumericColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Nghỉ từ ngày %>" DataField="LEAVE_FROM"
                                UniqueName="LEAVE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="LEAVE_FROM">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Nghỉ đến ngày %>" DataField="LEAVE_TO"
                                UniqueName="LEAVE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="LEAVE_TO">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày %>" DataField="DAY_NUM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="DAY_NUM"
                                UniqueName="DAY_NUM">
                            </tlk:GridNumericColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate:Ngày làm việc %>" DataField="IS_WORKING_DAY" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="IS_WORKING_DAY" UniqueName="IS_WORKING_DAY" >
                                <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày trong kế hoạch %>" DataField="IN_PLAN_DAYS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="IN_PLAN_DAYS"
                                UniqueName="IN_PLAN_DAYS">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày ngoài kế hoạch %>" DataField="NOT_IN_PLAN_DAYS"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="NOT_IN_PLAN_DAYS"
                                UniqueName="NOT_IN_PLAN_DAYS">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công %>" DataField="MANUAL_NAME"
                                SortExpression="MANUAL_NAME" UniqueName="MANUAL_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công nửa ngày %>" DataField="MORNING_NAME"
                                SortExpression="MORNING_NAME" UniqueName="MORNING_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công nửa ngày %>" DataField="AFTERNOON_NAME"
                                SortExpression="AFTERNOON_NAME" UniqueName="AFTERNOON_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người phê duyệt %>" HeaderStyle-Width="200px"
                                DataField="NOTE" SortExpression="NOTE" UniqueName="NOTE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi Chú %>" HeaderStyle-Width="200px"
                                DataField="NOTE_APP" SortExpression="NOTE_APP" UniqueName="NOTE_APP" />--%>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:Timer ID="TimerRequest" runat="server" Interval="5000" Enabled="False" EnableViewState="True">
</asp:Timer>
<asp:HiddenField runat="server" ID="hidRequestID" Value="0" />

<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

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

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('ctl00_MainContent_ctrlRegisterCO_RadSplitter3');
        //        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }
        function OpenLog(sender, eventArgs) {
        //var requestID = document.getElementById("<%= hidRequestID.ClientID %>").value;
            var requestID = document.getElementById("<%= lblRequest.ClientID %>").innerText;
            var oWindow = radopen('Dialog.aspx?mid=Common&fid=ctrlRESULT&noscroll=1&requestID=' + requestID + '&typeLog=1', "rwPopup");
            return 0;
        }
        function OnClientButtonClicking(sender, args) {
            debugger;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            }
            else if (args.get_item().get_commandName() == "PRINT") {
                OpenLog(sender, args);
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgRegisterLeave.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                } else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            }
            else if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgRegisterLeave.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
            else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
        }

        function OpenInsertWindow() {
            window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterCONewEdit&group=Business&FormType=0', "_self");
        }

        function OpenEditWindow() {
            var grid = $find('<%# rgRegisterLeave.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterCONewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id, "_self");
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgRegisterLeave.ClientID %>").get_masterTableView().rebind();
            }
        }
    </script>
</tlk:RadScriptBlock>
