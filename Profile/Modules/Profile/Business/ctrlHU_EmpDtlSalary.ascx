<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlSalary.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlSalary" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal" SkinID="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" Height="100%" SkinID="GridNotPaging">
            <MasterTableView DataKeyNames="ID,EFFECT_DATE,EMPLOYEE_ID">
                <Columns>
                     <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: ID %>" DataField="ID" SortExpression="ID" UniqueName="ID" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO" SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"                       SortExpression="ORG_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC" SortExpression="ORG_DESC" HeaderStyle-Width="130px" Visible="false">
                    </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="" DataField="SAL_TYPE_NAME" SortExpression="SAL_TYPE_NAME" UniqueName="SAL_TYPE_NAME" />
                            <tlk:GridNumericColumn HeaderText="" DataField="TAX_TABLE_Name" SortExpression="TAX_TABLE_Name" UniqueName="TAX_TABLE_Name"/>
                            <tlk:GridNumericColumn HeaderText="" DataField="SAL_BASIC" SortExpression="SAL_BASIC" UniqueName="SAL_BASIC" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="SAL_INS" SortExpression="SAL_INS" UniqueName="SAL_INS" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="SAL_TOTAL" SortExpression="SAL_TOTAL" UniqueName="SAL_TOTAL" DataFormatString="{0:n0}" />--%>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="200px" Visible="false">
        <tlk:RadGrid PageSize=50 ID="rgAllow" runat="server" Height="100%" SkinID="GridNotPaging">
            <MasterTableView Caption="<%$ Translate: Phụ cấp theo tờ trình/QĐ %>">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                        SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                        SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </tlk:GridNumericColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đóng bảo hiểm %>" DataField="IS_INSURRANCE"
                        SortExpression="IS_INSURRANCE" UniqueName="IS_INSURRANCE" HeaderStyle-Width="100px">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </tlk:GridCheckBoxColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
