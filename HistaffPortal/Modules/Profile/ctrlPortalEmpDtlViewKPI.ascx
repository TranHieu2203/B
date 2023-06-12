<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalEmpDtlViewKPI.ascx.vb"
    Inherits="Profile.ctrlPortalEmpDtlViewKPI" %>
<tlk:RadGrid PageSize="20" ID="rgTraining" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
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
                UniqueName="ORG_NAME" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" Visible="false" />
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
    </MasterTableView>
</tlk:RadGrid>