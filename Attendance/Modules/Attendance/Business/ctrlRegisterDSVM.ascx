<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterDSVM.ascx.vb"
    Inherits="Attendance.ctrlRegisterDSVM" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane><tlk:RadPane ID="RadPane3" runat="server" Height="70px" Scrolling="None">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr style="display:none">
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" AutoPostBack="true"
                                MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbFromDate" Text="Từ ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbUntilDate" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đối tượng nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboObjectEmployee" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại đăng ký")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTypeDMVS" runat="server" >
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboStatus" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rglateCom" runat="server" Height="100%" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID,WORKINGDAY,ID_AT_SWIPE,STATUS">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="VN_FULLNAME"
                                SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px"/>--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" />--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" HeaderStyle-Width="200px"
                                DataField="ORG_NAME" SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày đăng ký %>" DataField="WORKINGDAY"
                                UniqueName="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}" SortExpression="WORKINGDAY">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại thông tin %>" HeaderStyle-Width="200px"
                                DataField="REGIST_INFO_NAME" SortExpression="REGIST_INFO_NAME" UniqueName="REGIST_INFO_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại đăng ký %>" DataField="TYPE_DMVS_NAME"
                                SortExpression="TYPE_DMVS_NAME" UniqueName="TYPE_DMVS_NAME" HeaderStyle-Width="100px"/>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ giờ %>" DataField="FROM_HOUR" HeaderStyle-Width="70px"
                                AllowFiltering="false" UniqueName="FROM_HOUR" DataFormatString="{0:HH:mm}" SortExpression="FROM_HOUR">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến giờ %>" DataField="TO_HOUR" HeaderStyle-Width="70px"
                                AllowFiltering="false" UniqueName="TO_HOUR" DataFormatString="{0:HH:mm}" SortExpression="TO_HOUR">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số phút %>" DataField="MINUTE" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px"
                                DataFormatString="{0:n0}" SortExpression="MINUTE" UniqueName="MINUTE">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do %>" DataField="REMARK" SortExpression="REMARK" HeaderStyle-Width="100px"
                                UniqueName="REMARK" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị checkin %>" DataField="ORG_CHECK_IN_NAME" SortExpression="ORG_CHECK_IN_NAME" HeaderStyle-Width="100px"
                                UniqueName="ORG_CHECK_IN_NAME" />--%>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
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

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('ctl00_MainContent_ctrlRegisterDSVM_RadSplitter3');
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var selectedCount = $find('<%= rglateCom.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (selectedCount == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else if (selectedCount > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                } else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rglateCom.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
}

function onRequestStart(sender, eventArgs) {
    eventArgs.set_enableAjax(enableAjax);
    enableAjax = true;
}

function OpenInsertWindow() {
    var m;
    var cbo = $find("<%# cboPeriod.ClientID %>");
    var periodID = cbo.get_value();
    if (periodID.length = 0) {
        m = '<%# Translate("Bạn phải chọn kỳ công.") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterDSVMNewEdit&group=Business&FormType=0&periodid=' + periodID, "_self"); /*
            oWindow.setSize(850, 520);
            oWindow.center(); */
        }

        function OpenEditWindow() {
            var grid = $find('<%# rglateCom.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                var m;
                var cbo = $find("<%# cboPeriod.ClientID %>");
                var periodID = cbo.get_value();
                if (periodID.length = 0) {
                    m = '<%# Translate("Bạn phải chọn kỳ công.") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterDSVMNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&periodid=' + periodID, "_self"); /*
                oWindow.setSize(800, 500);
                oWindow.center(); */
            }
        }

        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rglateCom.ClientID %>").get_masterTableView().rebind();
            }
        }
    </script>
</tlk:RadScriptBlock>
