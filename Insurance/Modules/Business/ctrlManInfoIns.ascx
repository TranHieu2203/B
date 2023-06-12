<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlManInfoIns.ascx.vb"
    Inherits="Insurance.ctrlManInfoIns" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="none">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="40px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("BHXH từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("Đến")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgManagerInfo" runat="server" Height="100%" AllowSorting="True"
                    AllowMultiRowSelection="true" AllowPaging="True" AutoGenerateColumns="False">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_CODE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="ID" Visible="false"
                                SortExpression="ID" UniqueName="ID" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="POSITIONNAME"
                                SortExpression="POSITIONNAME" UniqueName="POSITIONNAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORGNAME" SortExpression="ORGNAME"
                                UniqueName="ORGNAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>--%>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORGNAME" SortExpression="ORGNAME"
                                UniqueName="ORGNAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORGNAME") %>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                UniqueName="STAFF_RANK_NAME" HeaderStyle-Width="100px" SortExpression="STAFF_RANK_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: CMND %>" DataField="CMND" UniqueName="CMND"
                                HeaderStyle-Width="100px" SortExpression="CMND" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="NGAYSINH"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="NGAYSINH"
                                UniqueName="NGAYSINH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Lương cơ bản đóng BH %>" DataField="SALARY"
                                DataFormatString="{0:n0}" SortExpression="SALARY" HeaderStyle-HorizontalAlign="Right"
                                UniqueName="SALARY" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị đóng BH %>" DataField="INSCENTER_NAME"
                                SortExpression="INSCENTER_NAME" UniqueName="INSCENTER_NAME" Visible="false" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHXH %>" DataField="SO" SortExpression="SO"
                                UniqueName="SO" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHYT %>" DataField="HE" SortExpression="HE"
                                UniqueName="HE" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTN %>" DataField="UE" SortExpression="UE"
                                UniqueName="UE" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                                <HeaderStyle Width="50px" />
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ tháng BHXH %>" DataField="SOFROM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/yyyy}" SortExpression="SOFROM"
                                UniqueName="SOFROM">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thời gian đóng BH trước khi vào CT %>"
                                DataField="TOTAL_TIME_INS_BEFOR" SortExpression="TOTAL_TIME_INS_BEFOR" HeaderStyle-HorizontalAlign="Right"
                                UniqueName="TOTAL_TIME_INS_BEFOR" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số sổ BHXH %>" DataField="SOBOOKNO"
                                SortExpression="SOBOOKNO" UniqueName="SOBOOKNO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng sổ BHXH %>" DataField="SOPRVDBOOKSTATUS_NAME"
                                SortExpression="SOPRVDBOOKSTATUS_NAME" UniqueName="SOPRVDBOOKSTATUS_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp %>" DataField="SOPRVDBOOKDAY"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SOPRVDBOOKDAY" UniqueName="SOPRVDBOOKDAY"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nộp sổ cho cty %>" DataField="DAYPAYMENTCOMPANY"
                                ItemStyle-HorizontalAlign="Center" SortExpression="DAYPAYMENTCOMPANY" UniqueName="DAYPAYMENTCOMPANY"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi Chú %>" DataField="NOTE_SO" SortExpression="NOTE_SO"
                                UniqueName="NOTE_SO">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số thẻ BHYT %>" DataField="HECARDNO"
                                SortExpression="HECARDNO" UniqueName="HECARDNO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng thẻ BHYT %>" DataField="HEPRVDCARDSTATUS_NAME"
                                SortExpression="HEPRVDCARDSTATUS_NAME" UniqueName="HEPRVDCARDSTATUS_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="HECARDEFFFROM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="HECARDEFFFROM"
                                UniqueName="HECARDEFFFROM">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="HECARDEFFTO"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="HECARDEFFTO"
                                UniqueName="HECARDEFFTO">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi khám chữa bệnh %>" DataField="HEWHRREGIS_NAME"
                                SortExpression="HEWHRREGIS_NAME" UniqueName="HEWHRREGIS_NAME">
                                <HeaderStyle Width="200px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ tháng BHTN %>" DataField="UEFROM"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="UEFROM"
                                UniqueName="UEFROM">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Tới tháng BHTN %>" DataField="UETO"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="UETO"
                                UniqueName="UETO">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" AllowKeyboardNavigation="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnBienDong" runat="server" Text="<%$ Translate: Biến động bảo hiểm %>"
                                OnClientClicking="btnBienDongClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnCheDo" runat="server" Text="<%$ Translate: Quản lý hưởng chế độ %>"
                                OnClientClicking="btnCheDoClick" AutoPostBack="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
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
        var oldSize = 0;

        var splitterID = 'ctl00_MainContent_ctrlManInfoIns_RadSplitter3';

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

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
        }

        function OpenInsertWindow() {
            window.open('/Default.aspx?mid=Insurance&fid=ctrlManInfoInsNewEdit&group=Business&FormType=0', "_self"); /*
            oWindow.setSize(800, 500);
            oWindow.center(); */
        }

        function OpenEditWindow() {
            var grid = $find('<%# rgManagerInfo.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                window.open('/Default.aspx?mid=Insurance&fid=ctrlManInfoInsNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '', "_self"); /*
                oWindow.setSize(800, 500);
                oWindow.center(); */
            }
        }

        function OpenImportWindow() {
            window.open('/Default.aspx?mid=Insurance&fid=ctrlImport_ManagerInfo&group=Business&FormType=0', "_self"); /*
            oWindow.setSize(800, 500);
            oWindow.center(); */
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
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgManagerInfo.ClientID %>").get_masterTableView().rebind();
            };
        }

        function btnBienDongClick(sender, args) {
            var extented = '';
            var bCheck = $find('<%= rgManagerInfo.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck > 0) {
                empID = $find('<%= rgManagerInfo.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_CODE');
                if (empID)
                    extented = '&EMPCODE=' + empID;
            }
            OpenInNewTab('Default.aspx?mid=Insurance&fid=ctrlInsChange&group=Business' + extented);
        }

        function btnCheDoClick(sender, args) {
            var extented = '';
            var bCheck = $find('<%= rgManagerInfo.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck > 0) {
                empID = $find('<%= rgManagerInfo.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_CODE');
                if (empID)
                    extented = '&EMPCODE=' + empID;
            }
            OpenInNewTab('Default.aspx?mid=Insurance&fid=ctrlInsRemigeManager&group=Business' + extented);
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%= rgManagerInfo.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgManagerInfo.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
                else if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            }
        }

        function OpenInNewTab(url) {
            var win = window.open(url, '_blank');
            win.focus();
        }
    </script>
</tlk:RadScriptBlock>
