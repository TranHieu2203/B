<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTime_Timesheet_ot.ascx.vb"
    Inherits="Attendance.ctrlTime_Timesheet_ot" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type="text/css" href="/Styles/StyleCustom.css" rel="Stylesheet" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="80px" Scrolling="None">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="150px" AutoPostBack="true"
                                runat="server">
                            </tlk:RadComboBox>
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
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" SkinID="Readonly" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAll" runat="server" Text="<%$ Translate: Khóa/Mở khóa tất cả dữ liệu %>" />
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgTimeTimesheet_ot" runat="server" Height="100%">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" HeaderStyle-Width="120px" SortExpression="VN_FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh chính %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="150px" SortExpression="TITLE_NAME" />
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                    </asp:Label>
                                    <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                        RelativeTo="Element" Position="BottomCenter">
                                        <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                    </tlk:RadToolTip>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm hệ số 1.0 %>"
                                DataField="TOTAL_FACTOR1" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_FACTOR1" UniqueName="TOTAL_FACTOR1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm hệ số 1.5 %>"
                                DataField="TOTAL_FACTOR1_5" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_FACTOR1_5" UniqueName="TOTAL_FACTOR1_5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm hệ số 1.8 %>"
                                DataField="TOTAL_FACTOR1_8" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_FACTOR1_8" UniqueName="TOTAL_FACTOR1_8">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm hệ số 2 %>"
                                DataField="TOTAL_FACTOR2" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_FACTOR2" UniqueName="TOTAL_FACTOR2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm hệ số 2.1 %>"
                                DataField="TOTAL_FACTOR2_1" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_FACTOR2_1" UniqueName="TOTAL_FACTOR2_1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm hệ số 2.7 %>"
                                DataField="TOTAL_FACTOR2_7" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_FACTOR2_7" UniqueName="TOTAL_FACTOR2_7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm hệ số 3 %>"
                                DataField="TOTAL_FACTOR3" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_FACTOR3" UniqueName="TOTAL_FACTOR3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số giờ làm thêm hệ số 3.9 %>"
                                DataField="TOTAL_FACTOR3_9" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="TOTAL_FACTOR3_9" UniqueName="TOTAL_FACTOR3_9">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng giờ làm thêm ngày thường %>"
                                DataField="OT_DAY" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="OT_DAY" UniqueName="OT_DAY">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng giờ làm thêm đêm thường %>"
                                DataField="OT_NIGHT" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="OT_NIGHT" UniqueName="OT_NIGHT">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng giờ làm thêm ngày nghỉ %>"
                                DataField="OT_WEEKEND_DAY" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="OT_WEEKEND_DAY" UniqueName="OT_WEEKEND_DAY">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng giờ làm thêm đêm nghỉ %>"
                                DataField="OT_WEEKEND_NIGHT" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="OT_WEEKEND_NIGHT" UniqueName="OT_WEEKEND_NIGHT">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng giờ làm thêm ngày lễ %>"
                                DataField="OT_HOLIDAY_DAY" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="OT_HOLIDAY_DAY" UniqueName="OT_HOLIDAY_DAY">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng giờ làm thêm đêm lễ %>"
                                DataField="OT_HOLIDAY_NIGHT" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="OT_HOLIDAY_NIGHT" UniqueName="OT_HOLIDAY_NIGHT">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số giờ làm thêm được chuyển nghỉ bù %>"
                                DataField="NUMBER_FACTOR_CP" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}"
                                SortExpression="NUMBER_FACTOR_CP" UniqueName="NUMBER_FACTOR_CP">
                            </tlk:GridNumericColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="100PX" />
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
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
<common:ctrlupload id="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlTime_Timesheet_ot_RadSplitter3';
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
            OpenEditWindow();
        }
        function OpenEditWindow() {
            var grid = $find('<%# rgTimeTimesheet_ot.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                var oWindow = radopen('Dialog.aspx?mid=Attendance&fid=ctrlTime_TimeSheet_OtEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
                oWindow.setSize(700, 250);
                oWindow.center();
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgTimeTimesheet_ot.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgTimeTimesheet_ot.ClientID %>");
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
            } else if (args.get_item().get_commandName() == 'NEXT') {
                enableAjax = false;
            }

        if (args.get_item().get_commandName() == "CALCULATE") {
            if (!UserConfirmation()) args.set_cancel(true);
        }


    }
    function OnClientClose(sender, args) {
        var m;
        var arg = args.get_argument();
        if (arg == '1') {
            m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
            var n = noty({ text: m, dismissQueue: true, type: 'success' });
            setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            $find("<%= rgTimeTimesheet_ot.ClientID %>").get_masterTableView().rebind();
        }
        $get("<%# btnSearch.ClientId %>").click();
    }

    function postBack(url) {
        var ajaxManager = $find("<%= AjaxManagerId %>");
        ajaxManager.ajaxRequest(url); //Making ajax request with the argument
    }

    function UserConfirmation() {
        return confirm('Bạn có chắc chắn đã thực hiện: \n ' +
    '- Cập nhật kế hoạch làm thêm giờ');
    }

    </script>
</tlk:RadScriptBlock>
