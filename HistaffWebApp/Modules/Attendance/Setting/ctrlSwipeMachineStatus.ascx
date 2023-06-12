<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSwipeMachineStatus.ascx.vb"
    Inherits="Attendance.ctrlSwipeMachineStatus" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rglSwipeMachine" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="TERMINAL_CODE,TERMINAL_NAME,ADDRESS_PLACE,TERMINAL_IP,PASS,PORT,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã máy chấm công%>" DataField="TERMINAL_CODE"
                        UniqueName="TERMINAL_CODE" SortExpression="TERMINAL_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên máy chấm công%>" DataField="TERMINAL_NAME"
                        UniqueName="TERMINAL_NAME" SortExpression="TERMINAL_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm đặt máy%>" DataField="ADDRESS_PLACE"
                        UniqueName="ADDRESS_PLACE" SortExpression="ADDRESS_PLACE">
                        <ItemStyle Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: IP %>" DataField="TERMINAL_IP" UniqueName="TERMINAL_IP"
                        SortExpression="TERMINAL_IP" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Password %>" DataField="PASS" UniqueName="PASS"
                        SortExpression="PASS" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Port %>" DataField="PORT" UniqueName="PORT"
                        SortExpression="PORT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái kết nối %>" DataField="TERMINAL_STATUS"
                        UniqueName="TERMINAL_STATUS" SortExpression="TERMINAL_STATUS">
                        <HeaderStyle Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Lần kết nối gần nhất %>" DataField="LAST_TIME_STATUS"
                        UniqueName="LAST_TIME_STATUS" SortExpression="LAST_TIME_STATUS" AllowFiltering="false"
                        DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Lần lấy dữ liệu gần nhất %>" DataField="LAST_TIME_UPDATE"
                        UniqueName="LAST_TIME_UPDATE" SortExpression="LAST_TIME_UPDATE" AllowFiltering="false"
                        DataFormatString="{0:dd/MM/yyyy HH:mm}">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số bản ghi MCC %>" DataField="TERMINAL_ROW"
                        UniqueName="TERMINAL_ROW" SortExpression="TERMINAL_ROW" DataFormatString="{0:n0}">
                        <HeaderStyle Width="70px" />
                    </tlk:GridNumericColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
            registerOnfocusOut('ctl00_MainContent_ctrlSwipeMachineStatus_RadSplitter3');
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                var rows = $find('<%= rglSwipeMachine.ClientID %>').get_masterTableView().get_dataItems().length;
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
    </script>
</tlk:RadCodeBlock>
