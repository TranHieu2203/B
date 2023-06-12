<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlSalary.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlSalary" %>
<%@ Register Src="../Shared/ctrlEmpBasicInfo.ascx" TagName="ctrlEmpBasicInfo" TagPrefix="Profile" %>
<tlk:radsplitter id="RadSplitter2" runat="server" height="100%" width="100%" orientation="Horizontal" skinid="Demo">
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <Profile:ctrlEmpBasicInfo runat="server" ID="ctrlEmpBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" Height="100%" SkinID="GridNotPaging">
            <MasterTableView DataKeyNames="ID,EFFECT_DATE,EMPLOYEE_ID">
                <Columns>
                    <%--<tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridNumericColumn HeaderText="Lương cơ bản" DataField="TAX_TABLE_Name" SortExpression="TAX_TABLE_Name" UniqueName="TAX_TABLE_Name" />
                    <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO" SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                    <tlk:GridNumericColumn HeaderText="Biểu thuế" DataField="SAL_BASIC" SortExpression="SAL_BASIC" UniqueName="SAL_BASIC" DataFormatString="{0:n0}" />
                    <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                        DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridNumericColumn HeaderText="Mức lương đóng bảo hiểm" DataField="SAL_INS" SortExpression="SAL_INS" UniqueName="SAL_INS" DataFormatString="{0:n0}" />
                    <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                    <tlk:GridNumericColumn HeaderText="Tổng lương" DataField="SAL_TOTAL" SortExpression="SAL_TOTAL" UniqueName="SAL_TOTAL" DataFormatString="{0:n0}" />
                    <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Thang lương" DataField="SAL_GROUP_NAME" UniqueName="SAL_GROUP_NAME" SortExpression="SAL_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC" SortExpression="ORG_DESC" HeaderStyle-Width="130px" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Ngạch lương" DataField="SAL_LEVEL_NAME" UniqueName="SAL_LEVEL_NAME" SortExpression="SAL_LEVEL_NAME" />
                    <tlk:GridBoundColumn HeaderText="Bậc lương" DataField="SAL_RANK_NAME" UniqueName="SAL_RANK_NAME" SortExpression="SAL_RANK_NAME" />
                    <tlk:GridNumericColumn HeaderText="Hệ số lương" DataField="FACTORSALARY" SortExpression="FACTORSALARY" UniqueName="FACTORSALARY" DataFormatString="{0:n0}" />
                    <tlk:GridNumericColumn HeaderText="Khoản bổ sung" DataField="OTHERSALARY1" SortExpression="OTHERSALARY1" UniqueName="OTHERSALARY1" DataFormatString="{0:n0}" />
                    <tlk:GridNumericColumn HeaderText="Hệ số khuyến khích" DataField="OTHERSALARY2" SortExpression="OTHERSALARY2" UniqueName="OTHERSALARY2" DataFormatString="{0:n0}" />
                    <tlk:GridDateTimeColumn HeaderText="Ngày ký" DataField="SIGN_DATE"  HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGN_NAME" UniqueName="SIGN_NAME" SortExpression="SIGN_NAME" />
                    <tlk:GridBoundColumn HeaderText="Chức danh người ký" DataField="SIGN_TITLE" UniqueName="SIGN_TITLE" SortExpression="SIGN_TITLE" />
                    <tlk:GridNumericColumn HeaderText="Phần trăm hưởng lương" DataField="PERCENTSALARY" SortExpression="PERCENTSALARY" UniqueName="PERCENTSALARY" DataFormatString="{0:n0}" />
                    <tlk:GridNumericColumn HeaderText="OTHERSALARY3" DataField="OTHERSALARY3" SortExpression="OTHERSALARY3" UniqueName="OTHERSALARY3" DataFormatString="{0:n0}" />
                    <tlk:GridNumericColumn HeaderText="OTHERSALARY4" DataField="OTHERSALARY4" SortExpression="OTHERSALARY4" UniqueName="OTHERSALARY4" DataFormatString="{0:n0}" />
                    <tlk:GridNumericColumn HeaderText="OTHERSALARY5" DataField="OTHERSALARY5" SortExpression="OTHERSALARY5" UniqueName="OTHERSALARY5" DataFormatString="{0:n0}" />--%>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="200px" Visible="false" >
        <tlk:RadGrid PageSize="50" ID="rgAllow" runat="server" Height="100%" SkinID="GridNotPaging">
            <MasterTableView Caption="<%$ Translate: Phụ cấp theo tờ trình/QĐ %>">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="ALLOWANCE_LIST_NAME"
                        SortExpression="ALLOWANCE_LIST_NAME" UniqueName="ALLOWANCE_LIST_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                        SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </tlk:GridNumericColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"/>
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
</tlk:radsplitter>
