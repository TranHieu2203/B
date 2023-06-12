<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterOT.ascx.vb"
    Inherits="Attendance.ctrlRegisterOT" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    #ctl00_MainContent_ctrlRegisterOT_rgRegisterOT_ctl00_ctl02_ctl02_FilterCheckBox_IS_NB
    {
        display :none;
    }
    .clspadright 
    {
        padding-right : 15px !important;
    }
</style>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="35px">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="80px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                TabIndex="12" Width="160px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td class="clspadright">
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="160px" AutoPostBack="true"
                                runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Nhân viên nghỉ việc %>" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" SkinID="Readonly" MaxLength="12" runat="server" Width="100%"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td class="clspadright">
                            <tlk:RadDatePicker ID="rdDenngay" SkinID="Readonly" MaxLength="12" runat="server" Width="100%"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgRegisterOT" runat="server" Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" HeaderStyle-Width="120px"
                                DataField="VN_FULLNAME" SortExpression="VN_FULLNAME" UniqueName="VN_FULLNAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" HeaderStyle-Width="120px"
                                DataField="TITLE_NAME" SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" HeaderStyle-Width="120px"
                                DataField="STAFF_RANK_NAME" SortExpression="STAFF_RANK_NAME" UniqueName="STAFF_RANK_NAME" />--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" HeaderStyle-Width="200px"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />--%>
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
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày đăng ký %>" DataField="WORKINGDAY"
                                UniqueName="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}" SortExpression="WORKINGDAY">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ giờ %>" DataField="FROM_HOUR"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" SortExpression="FROM_HOUR"
                                UniqueName="FROM_HOUR" DataFormatString="{0:HH:mm}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến giờ %>" DataField="TO_HOUR" ItemStyle-HorizontalAlign="Center"
                                SortExpression="TO_HOUR" UniqueName="TO_HOUR" AllowFiltering="false" DataFormatString="{0:HH:mm}" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Hệ số làm thêm  %>" DataField="HS_OT_NAME"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n0}" SortExpression="HS_OT_NAME"
                                UniqueName="HS_OT_NAME">
                            </tlk:GridNumericColumn>
                            <%--<tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Nghỉ bù  %>" DataField="IS_NB"
                                ItemStyle-HorizontalAlign="Center" SortExpression="IS_NB" UniqueName="IS_NB">
                            </tlk:GridCheckBoxColumn>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do làm thêm %>" DataField="NOTE"
                                SortExpression="NOTE" UniqueName="NOTE">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnGridCreated="GridCreated" />
                                <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
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
        var splitterID = 'ctl00_MainContent_ctrlRegisterOT_RadSplitter3';
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

        function gridRowDblClick(sender, eventArgs) {
            OpenEditWindow("Normal");
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
            window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterOTNewEdit&group=Business&FormType=0&periodid=' + periodID, "_self"); /*
            oWindow.setSize(900, 600);
            oWindow.center(); */
        }

        function OpenEditWindow() {
            var grid = $find('<%# rgRegisterOT.ClientID %>');
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
                window.open('/Default.aspx?mid=Attendance&fid=ctrlRegisterOTNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&periodid=' + periodID, "_self"); /*
                oWindow.setSize(900, 600);
                oWindow.center(); */
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
                $find("<%= rgRegisterOT.ClientID %>").get_masterTableView().rebind();
            }
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgRegisterOT.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                } else {
                    if (bCheck == 1) {
                        OpenEditWindow();
                        args.set_cancel(true);
                    } else {
                        m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                        n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                        args.set_cancel(true);
                    }                   
                }

            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgRegisterOT.ClientID %>");
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
    </script>
</tlk:RadScriptBlock>
