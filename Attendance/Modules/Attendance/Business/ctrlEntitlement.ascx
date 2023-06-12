<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlEntitlement.ascx.vb"
    Inherits="Attendance.ctrlEntitlement" %>
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
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="50px">
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                         <asp:Label ID="lbYear" runat="server" Text="Năm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                                TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm" runat="server" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                        <td style="display:none">
                            <asp:Label ID="lblProgram" runat="server" Font-Size="Large" Font-Bold="true">&nbsp;</asp:Label>
                            <asp:Label ID="lblRequest" runat="server" Font-Size="Large" Text="..." Font-Bold="true"
                                Visible="false">&nbsp;</asp:Label>
                        </td>
                        <td class="lb" style="padding-right: 5px; padding-left: 20px;display:none" colspan="2">
                            <%# Translate("CM_CTRLPROCESS_SCAN_TIME")%>&nbsp;
                        </td>
                        <td class="lb"></td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="RadNumTB" MinValue="1" MaxValue="30" Width="50px"
                                Value="5" MaxLength="2" AutoPostBack="true" ShowSpinButtons="True" Display="false">
                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                            <asp:Label ID="lblStatus" runat="server" Text="..." Font-Size="Large" CssClass="spancustomstatus"></asp:Label>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgEntitlement" runat="server" Height="100%" Scrolling="None">
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,ORG_DESC">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE"
                                HeaderStyle-Width="100px" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Họ tên" DataField="FULLNAME_VN" UniqueName="FULLNAME_VN"
                                HeaderStyle-Width="120px" SortExpression="FULLNAME_VN" />
                             <tlk:GridTemplateColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME"
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
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN"
                                HeaderStyle-Width="120px" SortExpression="TITLE_NAME_VN" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày vào công ty" DataField="JOIN_DATE"
                                UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                             <tlk:GridDateTimeColumn HeaderText="Ngày vào chính thức" DataField="JOIN_DATE_STATE"
                                UniqueName="JOIN_DATE_STATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE_STATE">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="Số tháng thâm niên" DataField="SENIORITY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY"
                                UniqueName="SENIORITY" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="Phép thâm niên có hiệu lực" DataField="SENIORITYHAVE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITYHAVE"
                                UniqueName="SENIORITYHAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="Phép ứng" DataField="ADVANCELEAVE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="ADVANCELEAVE"
                                UniqueName="ADVANCELEAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép năm cũ chuyển sang" DataField="PREV_HAVE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="PREV_HAVE"
                                UniqueName="PREV_HAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép năm cũ đã nghỉ" DataField="PREV_USED"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="PREV_USED"
                                UniqueName="PREV_USED" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                             <tlk:GridDateTimeColumn HeaderText="Ngày hết phép cũ" DataField="EXPIREDATE"
                                UniqueName="EXPIREDATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIREDATE">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="Phép năm trước còn lại được sử dụng" DataField="PREVTOTAL_HAVE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="PREVTOTAL_HAVE"
                                UniqueName="PREVTOTAL_HAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ năm nay" DataField="QP_YEAR" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="QP_YEAR" UniqueName="QP_YEAR"
                                HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                             <tlk:GridNumericColumn HeaderText="Phép chế độ bổ sung từ đối tượng độc hại" DataField="ADJUST_MONTH_TN" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="ADJUST_MONTH_TN" UniqueName="ADJUST_MONTH_TN"
                                HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ tính theo tháng làm việc" DataField="TOTAL_HAVE1" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="TOTAL_HAVE1" UniqueName="TOTAL_HAVE1"
                                HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã sử dụng" DataField="CUR_USED"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED"
                                UniqueName="CUR_USED" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ còn lại" DataField="CUR_HAVE" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="CUR_HAVE" UniqueName="CUR_HAVE" HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Tổng phép còn được sử dụng" DataField="TOTAL_HAVE" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n2}" SortExpression="TOTAL_HAVE" UniqueName="TOTAL_HAVE"
                                HeaderStyle-Width="100px">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T1" DataField="CUR_USED1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED1"
                                UniqueName="CUR_USED1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T2" DataField="CUR_USED2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED2"
                                UniqueName="CUR_USED2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T3" DataField="CUR_USED3"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED3"
                                UniqueName="CUR_USED3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T4" DataField="CUR_USED4"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED4"
                                UniqueName="CUR_USED4">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T5" DataField="CUR_USED5"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED5"
                                UniqueName="CUR_USED5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T6" DataField="CUR_USED6"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED6"
                                UniqueName="CUR_USED6">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T7" DataField="CUR_USED7"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED7"
                                UniqueName="CUR_USED7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T8" DataField="CUR_USED8"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED8"
                                UniqueName="CUR_USED8">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T9" DataField="CUR_USED9"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED9"
                                UniqueName="CUR_USED9">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T10" DataField="CUR_USED10"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED10"
                                UniqueName="CUR_USED10">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T11" DataField="CUR_USED11"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED11"
                                UniqueName="CUR_USED11">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="Phép chế độ đã nghỉ T12" DataField="CUR_USED12"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="CUR_USED12"
                                UniqueName="CUR_USED12">
                            </tlk:GridNumericColumn>--%>
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
<asp:HiddenField runat="server" ID="hidRequestID" Value="0" />
<asp:Timer ID="TimerRequest" runat="server" Interval="5000" Enabled="False" EnableViewState="True">
</asp:Timer>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlEntitlement_RadSplitter3';
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

        function OpenEditWindow(states) {


        }

        function OpenInsertWindow() {


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
                var bCheck = $find('<%= rgENTITLEMENT.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow("Edit");
                    args.set_cancel(true);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgEntitlement.ClientID %>");
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

function OnClientClose(oWnd, args) {
    postBack(oWnd.get_navigateUrl());
}

function postBack(url) {
    var ajaxManager = $find("<%= AjaxManagerId %>");
    ajaxManager.ajaxRequest(url); //Making ajax request with the argument
}

    </script>
</tlk:RadScriptBlock>
