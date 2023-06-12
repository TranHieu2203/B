<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Salary_Detention.ascx.vb"
    Inherits="Payroll.ctrlPA_Salary_Detention" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type="text/css" href="/Styles/StyleCustom.css" rel="Stylesheet" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="150px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbl1" Text="Năm"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear1" SkinID="dDropdownList" AutoPostBack="true" CausesValidation="false" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                         <asp:Label ID="lbPeriodT" runat="server" Text="Tháng tính lương"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriodT" SkinID="dDropdownList" runat="server" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbl2" Text="Năm"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear2" SkinID="dDropdownList" AutoPostBack="true" CausesValidation="false" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                         <asp:Label ID="lbPayMonth" runat="server" Text="Tháng trả lương"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPayMonth" SkinID="dDropdownList" runat="server" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm" runat="server" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                         <asp:Label ID="Label2" runat="server" Text="Thông tin điền nhanh:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Năm"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear3" SkinID="dDropdownList" AutoPostBack="true" CausesValidation="false" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                         <asp:Label ID="lbPayMonthSearch" runat="server" Text="Tháng trả lương"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPayMonthSearch" SkinID="dDropdownList" runat="server" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                         <asp:Label ID="lbNote" runat="server" Text="Ghi chú"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNote" runat="server" Width="80px">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <tlk:RadButton ID="btnQuickFulfill" Text="Điền nhanh" runat="server">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgSalaryDetention" runat="server" Height="100%" Scrolling="None">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID" ClientDataKeyNames="ID, PAY_MONTH, PERIOD_ID, EMPLOYEE_ID, IS_DETENTION">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                                HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Họ tên" DataField="FULLNAME_VN" UniqueName="FULLNAME_VN"
                                HeaderStyle-Width="120px" SortExpression="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                                HeaderStyle-Width="120px" SortExpression="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                HeaderStyle-Width="120px" SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Tháng lương" DataField="PERIOD_NAME" UniqueName="PERIOD_NAME"
                                HeaderStyle-Width="120px" SortExpression="PERIOD_NAME" />
                             <tlk:GridNumericColumn HeaderText="Số tiền" DataField="INCOME" UniqueName="INCOME"
                                HeaderStyle-Width="120px" SortExpression="INCOME" />
                            <tlk:GridNumericColumn Visible="false" HeaderText="Tháng trả lương" DataField="PAY_MONTH" UniqueName="PAY_MONTH"
                                HeaderStyle-Width="120px" SortExpression="PAY_MONTH" />
                            <tlk:GridBoundColumn HeaderText="Tháng trả lương" DataField="PAY_MONTH_NAME" UniqueName="PAY_MONTH_NAME"
                                HeaderStyle-Width="120px" SortExpression="PAY_MONTH_NAME" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" UniqueName="NOTE"
                                HeaderStyle-Width="120px" SortExpression="NOTE" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlPA_Salary_Detention_RadSplitter3';
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgSalaryDetention.ClientID %>').get_masterTableView().get_selectedItems().length;
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
                }
                else {
                    OpenEditWindow("Edit");
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgSalaryDetention.ClientID %>");
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
        }

        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgSalaryDetention.ClientID %>").get_masterTableView().rebind();
            }
        }
        function OpenInsertWindow() {
            window.open('/Default.aspx?mid=Payroll&fid=ctrlPA_Salary_DetentionNewEdit&group=Business&FormType=0', "_self");
        }
        function OpenEditWindow() {
            var bCheck = $find('<%= rgSalaryDetention.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%= rgSalaryDetention.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Payroll&fid=ctrlPA_Salary_DetentionNewEdit&group=Business&FormType=1&ID=' + id, "_self");
            return 2;
        }

    </script>
</tlk:RadScriptBlock>
