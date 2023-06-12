<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlViewKPI.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlViewKPI" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="35px" Scrolling="None" Visible="false">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane runat="server" ID="RadPane4" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" AllowMultiRowSelection="true"
            Height="100%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <NoRecordsTemplate>
                    Không có bản ghi nào
                </NoRecordsTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="EMPLOYEE_NAME"
                        SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                        UniqueName="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm đánh giá %>" DataField="PE_PERIO_YEAR"
                        UniqueName="PE_PERIO_YEAR" SortExpression="PE_PERIO_YEAR" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kỳ đánh giá %>" DataField="PE_PERIO_NAME"
                        UniqueName="PE_PERIO_NAME" SortExpression="PE_PERIO_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu đánh giá %>" DataField="PE_PERIO_TYPE_ASS_NAME"
                        UniqueName="PE_PERIO_TYPE_ASS_NAME" SortExpression="PE_PERIO_TYPE_ASS_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="PE_PERIO_START_DATE"
                        UniqueName="PE_PERIO_START_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PE_PERIO_START_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="PE_PERIO_END_DATE"
                        UniqueName="PE_PERIO_END_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PE_PERIO_END_DATE"
                        ShowFilterIcon="true">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng điểm đánh giá %>" DataField="RESULT_CONVERT"
                        UniqueName="RESULT_CONVERT" SortExpression="RESULT_CONVERT" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp hạng %>" DataField="CLASSIFICATION_NAME"
                        UniqueName="CLASSIFICATION_NAME" SortExpression="CLASSIFICATION_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="PE_STATUS_NAME"
                        UniqueName="PE_STATUS_NAME" SortExpression="PE_STATUS_NAME" ShowFilterIcon="false"
                        CurrentFilterFunction="Contains" FilterControlWidth="100%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="150px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
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
                var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane4.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
