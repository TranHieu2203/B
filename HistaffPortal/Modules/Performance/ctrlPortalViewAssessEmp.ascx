<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalViewAssessEmp.ascx.vb"
    Inherits="Performance.ctrlPortalViewAssessEmp" %>
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="350px" AllowFilteringByColumn="true">
     <MasterTableView DataKeyNames="ID,PE_PERIO_ID,OBJECT_GROUP_ID,EMPLOYEE_ID" ClientDataKeyNames="ID,PE_PERIO_ID,OBJECT_GROUP_ID,EMPLOYEE_ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="ID" Visible="false">
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm đánh giá %>" DataField="PE_PERIO_YEAR"
                UniqueName="PE_PERIO_YEAR" SortExpression="PE_PERIO_YEAR"
                ShowFilterIcon="false" CurrentFilterFunction="Contains" FilterControlWidth="100%">
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
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng điểm đánh giá NV %>" DataField="RESULT_CONVERT_NV"
                UniqueName="RESULT_CONVERT_NV" SortExpression="RESULT_CONVERT_NV" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp hạng NV %>" DataField="CLASSIFICATION_NAME_NV"
                UniqueName="CLASSIFICATION_NAME_NV" SortExpression="CLASSIFICATION_NAME_NV" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng điểm đánh giá %>" DataField="RESULT_CONVERT"
                UniqueName="RESULT_CONVERT" SortExpression="RESULT_CONVERT" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
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
    </MasterTableView>
</tlk:RadGrid>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var selectedID = -1;
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'PRINT') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>

