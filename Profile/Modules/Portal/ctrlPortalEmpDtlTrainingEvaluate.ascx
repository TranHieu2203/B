<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalEmpDtlTrainingEvaluate.ascx.vb"
    Inherits="Profile.ctrlPortalEmpDtlTrainingEvaluate" %>
<tlk:RadGrid PageSize="50" ID="rgTrainingEvaluate" runat="server" Height="350px" AllowFilteringByColumn="true">
    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="DECISION_NO,EVALUATE_NAME,RANK_NAME,REMARK,CAPACITY_NAME,YEAR,SIGN_DATE,CONTENT">
        <Columns>
            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
            </tlk:GridClientSelectColumn>
            <tlk:GridBoundColumn DataField="ID" Visible="false" />
            <tlk:GridBoundColumn HeaderText="Năm" DataField="YEAR"
                UniqueName="YEAR" SortExpression="YEAR" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Kỳ đánh giá" DataField="EVALUATE_NAME"
                UniqueName="EVALUATE_NAME" SortExpression="EVALUATE_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Xếp loại" DataField="RANK_NAME"
                UniqueName="RANK_NAME" SortExpression="RANK_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Năng lực" DataField="CAPACITY_NAME"
                UniqueName="CAPACITY_NAME" SortExpression="CAPACITY_NAME" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                UniqueName="DECISION_NO" SortExpression="DECISION_NO" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Ngày ký" DataField="SIGN_DATE"
                UniqueName="SIGN_DATE" SortExpression="SIGN_DATE" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Nhận xét đánh giá" DataField="CONTENT"
                UniqueName="CONTENT" SortExpression="CONTENT" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>
            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK"
                UniqueName="REMARK" SortExpression="REMARK" ShowFilterIcon="false"
                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                <HeaderStyle HorizontalAlign="Center" />
            </tlk:GridBoundColumn>--%>
        </Columns>
    </MasterTableView>
</tlk:RadGrid>