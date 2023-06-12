<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSwipeMachineStatus.ascx.vb"
    Inherits="Attendance.ctrlSwipeMachineStatus" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rglSwipeMachine" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
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
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Port %>" DataField="PORT" UniqueName="PORT"
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
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }
        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
