<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSalaryDetailPopup.ascx.vb"
    Inherits="Common.ctrlSalaryDetailPopup" %>
<tlk:RadGrid PageSize="50" ID="rgSalary" runat="server" AutoGenerateColumns="False"
    AllowPaging="True" Height="100%">
    <MasterTableView DataKeyNames="EMPLOYEE_CODE_DT">
        <Columns>
         <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE_DT"
                        UniqueName="EMPLOYEE_CODE_DT" SortExpression="EMPLOYEE_CODE_DT" />
        </Columns>
        <HeaderStyle Width="120px" />
    </MasterTableView>
</tlk:RadGrid>
