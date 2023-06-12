<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAtCompensationYear.ascx.vb"
    Inherits="Attendance.ctrlAtCompensationYear" %>
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
                            <tlk:GridTemplateColumn HeaderText=" Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME"
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
                            <tlk:GridBoundColumn HeaderText="Ngày vào công ty" DataField="JOIN_DATE"
                                UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="Năm kết phép" DataField="YEAR" UniqueName="YEAR"
                                HeaderStyle-Width="120px" SortExpression="YEAR" />
                            <tlk:GridTemplateColumn DataField="YEAR_NB" HeaderText="<%$ Translate:Số phép bù còn lại %>"
                                SortExpression="YEAR_NB" UniqueName="YEAR_NB">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <tlk:RadTextBox ID="txtYEAR_NB" ShowSpinButtons="false" MinValue="0"
                                        runat="server" CausesValidation="false" Text='<%# (Eval("YEAR_NB").ToString.Replace(",", "."))%>' Width="78px">
                                       <%-- <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" DecimalSeparator="." />
                                         <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />   --%>
                                    </tlk:RadTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn DataField="NB_TRANFER" HeaderText="<%$ Translate:Số phép bù kết chuyển %>"
                                SortExpression="NB_TRANFER" UniqueName="NB_TRANFER">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <tlk:RadTextBox ID="txtNB_TRANFER" ShowSpinButtons="false" MinValue="0"
                                        runat="server" CausesValidation="false" Text='<%# (Eval("NB_TRANFER").ToString.Replace(",", "."))%>' Width="78px">
                                         <%--<NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" DecimalSeparator="." />
                                         <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />--%>
                                    </tlk:RadTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>


                             <tlk:GridTemplateColumn DataField="NB_EDIT" HeaderText="<%$ Translate:Số phép bù điều chỉnh %>"
                                SortExpression="NB_EDIT" UniqueName="ANNUAL_TRANFER">
                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                <ItemTemplate>
                                    <tlk:RadTextBox ID="txtNB_EDIT" ShowSpinButtons="false" MinValue="0"
                                        runat="server" CausesValidation="false" Text='<%# (Eval("NB_EDIT").ToString.Replace(",", "."))%>' Width="78px">
                                        <%-- <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" DecimalSeparator="." />
                                         <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />--%>
                                    </tlk:RadTextBox>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
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
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlAtConcludeAnnualYear_RadSplitter3';
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
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
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

function displayDecimalFormat(sender, args) {
    sender.set_textBoxValue(sender.get_value().toString());
}

    </script>
</tlk:RadScriptBlock>
