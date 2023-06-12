<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlInsurance.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlInsurance" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal" SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" Height="100%" AllowSorting="True" AllowMultiRowSelection="true"
            AllowPaging="True" AutoGenerateColumns="False">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                <Columns>
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Loại biến động %>" DataField="CHANGE_TYPE_NAME"
                        SortExpression="CHANGE_TYPE_NAME" UniqueName="CHANGE_TYPE_NAME" AutoPostBackOnFilter="true" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Tháng biến động %>" DataField="CHANGE_MONTH"
                        SortExpression="CHANGE_MONTH" UniqueName="CHANGE_MONTH" DataFormatString="{0:dd/MM/yyyy}"
                        ItemStyle-HorizontalAlign="Right" AutoPostBackOnFilter="true" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng thời gian đóng BH (Tháng) %>" DataField="TOTAL_INS"
                        SortExpression="TOTAL_INS" UniqueName="TOTAL_INS" ItemStyle-HorizontalAlign="Right" AutoPostBackOnFilter="true" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền lương đóng BH %>" DataFormatString="{0:n0}"
                        DataField="BAS_SAL" SortExpression="BAS_SAL" UniqueName="BAS_SAL" ItemStyle-HorizontalAlign="Right" AutoPostBackOnFilter="true" />--%>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đợt khai báo %>" DataField="DECLARE_DATE"
                            DataFormatString="{0:MM/yyyy}" UniqueName="DECLARE_DATE" SortExpression="DECLARE_DATE"
                            HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                            AutoPostBackOnFilter="true" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" UniqueName="EFFECTIVE_DATE" SortExpression="EFFECTIVE_DATE"
                            HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                            AutoPostBackOnFilter="true" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị đóng bảo hiểm %>" DataField="INS_ORG_NAME"
                            UniqueName="INS_ORG_NAME" SortExpression="INS_ORG_NAME" ShowFilterIcon="false"
                            HeaderStyle-Width="200px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            FilterControlWidth="100%">
                            <HeaderStyle HorizontalAlign="Center" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại biến động %>" DataField="ARISING_TYPE_NM"
                            UniqueName="ARISING_TYPE_NM" SortExpression="ARISING_TYPE_NM" ShowFilterIcon="false"
                            HeaderStyle-Width="150px" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                            FilterControlWidth="100%">
                            <HeaderStyle HorizontalAlign="Center" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức lương cũ %>" DataFormatString="{0:N0}"
                            DataField="SALARY_PRE_PERIOD" UniqueName="SALARY_PRE_PERIOD" SortExpression="SALARY_PRE_PERIOD"
                            HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                            AutoPostBackOnFilter="true" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức lương mới %>" DataFormatString="{0:N0}"
                            DataField="SALARY_NOW_PERIOD" UniqueName="SALARY_NOW_PERIOD" SortExpression="SALARY_NOW_PERIOD"
                            HeaderStyle-Width="90px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                            AutoPostBackOnFilter="true" />
                        <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHXH %>" DataField="SI_CHK" DataType="System.Boolean"
                            FilterControlWidth="20px" ShowFilterIcon="false" SortExpression="SI_CHK" UniqueName="SI_CHK">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        </tlk:GridCheckBoxColumn>
                        <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHYT %>" DataField="HI_CHK" DataType="System.Boolean"
                            FilterControlWidth="20px" ShowFilterIcon="false" SortExpression="HI_CHK" UniqueName="HI_CHK">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        </tlk:GridCheckBoxColumn>
                        <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTN %>" DataField="UI_CHK" DataType="System.Boolean"
                            FilterControlWidth="20px" ShowFilterIcon="false" SortExpression="UI_CHK" UniqueName="UI_CHK">
                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        </tlk:GridCheckBoxColumn>
                    </Columns>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
