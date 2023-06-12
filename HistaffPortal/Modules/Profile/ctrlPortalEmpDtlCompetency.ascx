<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalEmpDtlCompetency.ascx.vb"
    Inherits="Profile.ctrlPortalEmpDtlCompetency" %>
<tlk:RadGrid PageSize="20" ID="rgTraining" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID">
        <Columns>
            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm %>" DataField="COMPETENCY_PERIOD_YEAR"
                UniqueName="COMPETENCY_PERIOD_YEAR" SortExpression="COMPETENCY_PERIOD_YEAR" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đợt đánh giá %>" DataField="COMPETENCY_PERIOD_NAME"
                UniqueName="COMPETENCY_PERIOD_NAME" SortExpression="COMPETENCY_PERIOD_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm năng lực %>" DataField="COMPETENCY_GROUP_NAME"
                UniqueName="COMPETENCY_GROUP_NAME" SortExpression="COMPETENCY_GROUP_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năng lực %>" DataField="COMPETENCY_NAME"
                UniqueName="COMPETENCY_NAME" SortExpression="COMPETENCY_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức năng lực chuẩn %>" DataField="LEVEL_NUMBER_STANDARD"
                UniqueName="LEVEL_NUMBER_STANDARD" SortExpression="LEVEL_NUMBER_STANDARD" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức năng lực cá nhân %>" DataField="LEVEL_NUMBER_EMP"
                UniqueName="LEVEL_NUMBER_EMP" SortExpression="LEVEL_NUMBER_EMP" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="<%$ Translate: Diễn giải %>" DataField="REMARK"
                UniqueName="REMARK" SortExpression="REMARK" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
        </Columns>
        <HeaderStyle Width="150px" />
    </MasterTableView>
</tlk:RadGrid>