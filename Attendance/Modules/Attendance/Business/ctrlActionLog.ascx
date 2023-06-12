<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlActionLog.ascx.vb"
    Inherits="Attendance.ctrlActionLog" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="rtbActionLog" runat="server">
        </tlk:RadToolBar>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgActionLog" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" ReadOnly="true" Visible="false" HeaderText="<%$ Translate:Id %>"
                        SortExpression="ID" />
                    <tlk:GridBoundColumn DataField="username" ReadOnly="true" HeaderText="<%$ Translate:Tên tài khoản %>"
                        SortExpression="username" />
                    <tlk:GridBoundColumn DataField="fullname" ReadOnly="true" HeaderText="<%$ Translate:Họ và tên %>"
                        SortExpression="fullname" />
                    <tlk:GridBoundColumn DataField="email" ReadOnly="true" HeaderText="<%$ Translate:Email %>"
                        SortExpression="email" />
                    <tlk:GridBoundColumn DataField="mobile" ReadOnly="true" HeaderText="<%$ Translate:Mobile %>"
                        SortExpression="mobile" />
                    <tlk:GridBoundColumn DataField="action_name" ReadOnly="true" HeaderText="<%$ Translate:Thao tác %>"
                        SortExpression="action_name" />
                         <tlk:GridBoundColumn DataField="PERIOD_NAME" ReadOnly="true" HeaderText="<%$ Translate:Kỳ công %>"
                        SortExpression="PERIOD_NAME" />
                    <tlk:GridDateTimeColumn DataField="action_date" ReadOnly="true" HeaderText="<%$ Translate:Ngày thao tác %>"
                        SortExpression="action_date" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" AllowFiltering="false" />
                    <tlk:GridBoundColumn DataField="object_name" ReadOnly="true" HeaderText="<%$ Translate:Nội dung thao tác %>"
                        SortExpression="object_name" />
                    <tlk:GridBoundColumn DataField="IP" ReadOnly="true" HeaderText="<%$ Translate:Địa chỉ IP %>"
                        SortExpression="IP" />
                    <tlk:GridBoundColumn DataField="computer_name" ReadOnly="true" HeaderText="<%$ Translate:Tên máy %>"
                        SortExpression="computer_name" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="rwmMain" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<script type="text/javascript">
    var enableAjax = true;
    var splitterID = 'RAD_SPLITTER_ctl00_MainContent_RadSplitter1';

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
        if (item.get_commandName() == "EXPORT") {
            var grid = $find("<%=rgActionLog.ClientID %>");
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

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

</script>
