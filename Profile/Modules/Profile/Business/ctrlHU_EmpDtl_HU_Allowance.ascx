<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtl_HU_Allowance.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtl_HU_Allowance" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .rgFilterRow td {
        text-align: center;
    }
</style>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal" SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" AllowFilteringByColumn="true" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Các khoản phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                        UniqueName="ALLOWANCE_LIST_NAME" SortExpression="ALLOWANCE_LIST_NAME" ShowFilterIcon="false"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%"/>
                   <%-- <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Là khoản trừ %>" DataField="IS_DEDUCT"
                        UniqueName="IS_DEDUCT" SortExpression="IS_DEDUCT" HeaderStyle-Width="50px" ShowFilterIcon="true" AllowFiltering="false" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT" UniqueName="AMOUNT"
                        SortExpression="AMOUNT" DataFormatString="{0:n0}" HeaderStyle-Width="100px" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%"/>
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số %>" DataField="FACTOR" UniqueName="FACTOR"
                        SortExpression="FACTOR" HeaderStyle-Width="100px" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%"/>--%>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" 
                        ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                        DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE" 
                        ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                        DataFormatString="{0:dd/MM/yyyy}" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%">
                        <HeaderStyle Width="120px" />
                        <ItemStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Diễn giải cộng khác %>" DataField="DETAIL_ADD_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        FilterControlWidth="100%" UniqueName="DETAIL_ADD_NAME" SortExpression="DETAIL_ADD_NAME" HeaderStyle-Width="120px" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK" ShowFilterIcon="false"
                        SortExpression="REMARK" HeaderStyle-Width="200px" />
                    <%--<tlk:GridCheckBoxColumn DataField="IS_DEDUCT" UniqueName="IS_DEDUCT" HeaderText="<%$ Translate: Là khoản trừ%>"
                        SortExpression="IS_DEDUCT" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        >
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>--%>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
