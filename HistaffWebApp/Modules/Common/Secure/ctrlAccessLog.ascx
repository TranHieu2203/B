<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAccessLog.ascx.vb"
    Inherits="Common.ctrlAccessLog" %>
    <link href = "/Styles/StyleCustom.css" rel = "Stylesheet" type = "text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="rtbAccessLog" runat="server">
        </tlk:RadToolBar>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="75px">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Trạng thái")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Tài khoản")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUSER" runat="server" MaxLength="255">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Địa chỉ IP")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtIP" runat="server" MaxLength="255">
                    </tlk:RadTextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Tên máy")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtComputerName" runat="server" MaxLength="255">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgAccessLog" runat="server" Height="100%">
        <ClientSettings EnableRowHoverStyle="true">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="Id" ReadOnly="true" Visible="false" HeaderText="<%$ Translate:Id %>"
                        SortExpression="Id" />
                    <tlk:GridBoundColumn DataField="Username" ReadOnly="true" HeaderText="<%$ Translate:Tên tài khoản %>"
                        SortExpression="Username" />
                    <tlk:GridBoundColumn DataField="Fullname" ReadOnly="true" HeaderText="<%$ Translate:Họ và tên %>"
                        SortExpression="Fullname" />
                    <tlk:GridBoundColumn DataField="Email" ReadOnly="true" HeaderText="<%$ Translate:Email %>"
                        SortExpression="Email" />
                    <tlk:GridBoundColumn DataField="Mobile" ReadOnly="true" HeaderText="<%$ Translate:Số điện thoại %>"
                        SortExpression="Mobile" />
                    <tlk:GridBoundColumn DataField="GroupName" ReadOnly="true" HeaderText="<%$ Translate:Nhóm %>"
                        SortExpression="GroupName" />
                    <tlk:GridBoundColumn DataField="LoginDate" ReadOnly="true" HeaderText="<%$ Translate:Ngày giờ đăng nhập %>"
                        SortExpression="LoginDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                    <tlk:GridBoundColumn DataField="LogoutDate" ReadOnly="true" HeaderText="<%$ Translate:Ngày giờ thoát %>"
                        SortExpression="LogoutDate" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                    <tlk:GridBoundColumn DataField="LogoutStatus" ReadOnly="true" HeaderText="<%$ Translate:Trạng thái %>"
                        SortExpression="LogoutStatus" />
                    <tlk:GridBoundColumn DataField="IP" ReadOnly="true" HeaderText="<%$ Translate:Địa chỉ IP %>"
                        SortExpression="IP" />
                    <tlk:GridBoundColumn DataField="ComputerName" ReadOnly="true" HeaderText="<%$ Translate:Tên máy %>"
                        SortExpression="ComputerName" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<script type="text/javascript">
    var enableAjax = true;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "EXPORT") {
            enableAjax = false;
        }
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
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
        registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlAccessLog_RadSplitter1');
    }
</script>
