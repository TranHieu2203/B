<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalWorkingBefore_Edit.ascx.vb"
    Inherits="Profile.ctrlPortalWorkingBefore_Edit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<div style="overflow: auto">
    <table class="table-form">
        <tr>
            <td class="lb" style="width: 130px">
                <%# Translate("Tên công ty") %><%--<span class="lbReq">*</span>--%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtCompanyName">
                </tlk:RadTextBox>
                <%--<asp:RequiredFieldValidator ID="reqCompanyName" ControlToValidate="txtCompanyName"
                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên công ty %>" ToolTip="<%$ Translate: Bạn phải nhập tên công ty  %>">
                </asp:RequiredFieldValidator>--%>
            </td>
            <td class="lb" style="width: 130px">
                <%# Translate("Số điện thoại")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtTelephone">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Địa chỉ công ty") %>
            </td>
            <td colspan="3">
                <tlk:RadTextBox runat="server" ID="txtCompanyAddress" Width="100%">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Ngày vào")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdJoinDate" runat="server">
                </tlk:RadDatePicker>
            </td>
            <td class="lb">
                <%# Translate("Ngày nghỉ")%>
            </td>
            <td>
                <tlk:RadDatePicker ID="rdEndDate" runat="server">
                </tlk:RadDatePicker>
                <asp:CompareValidator ID="compare_JoinDate_EndDate" runat="server" ErrorMessage="<%$ Translate: Ngày nghỉ phải sau ngày vào %>"
                    ControlToCompare="rdJoinDate" ControlToValidate="rdEndDate" ToolTip="<%$ Translate: Ngày nghỉ phải sau ngày vào %>"
                    Type="Date" Operator="GreaterThan"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Mức lương")%>
            </td>
            <td>
                <tlk:RadNumericTextBox runat="server" ID="txtSalary" MinValue="0" MaxLength="9" NumberFormat-DecimalDigits="0">
                </tlk:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Chức danh")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtTitleName">
                </tlk:RadTextBox>
            </td>
            <td class="lb">
               <%-- <%# Translate("Cấp bậc")%>--%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtLevelName" Visible="false">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Lý do nghỉ việc") %>
            </td>
            <td colspan="3">
                <tlk:RadTextBox runat="server" ID="txtTerReason" SkinID="Textbox1023" Width="100%">
                </tlk:RadTextBox>
            </td>
        </tr>
    </table>
    <tlk:RadGrid PageSize=50 ID="rgWorkingBeforeEdit" runat="server" Height="250px" Width="99%">
        <MasterTableView DataKeyNames="ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON,FK_PKEY,REASON_UNAPROVE,STATUS,REASON_UNAPROVE"
                         ClientDataKeyNames = "ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON,FK_PKEY,REASON_UNAPROVE,STATUS,REASON_UNAPROVE"
            Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="COMPANY_NAME"
                    UniqueName="COMPANY_NAME" SortExpression="COMPANY_NAME" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="TELEPHONE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    UniqueName="TELEPHONE">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ công ty %>" DataField="COMPANY_ADDRESS"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    UniqueName="COMPANY_ADDRESS">
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào %>" DataField="JOIN_DATE"
                    UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nghỉ %>" DataField="END_DATE"
                    UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridNumericColumn DataField="SALARY" HeaderText="<%$ Translate: Mức lương %>"
                    UniqueName="SALARY" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true" DataFormatString="{0:n0}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="right" />
                </tlk:GridNumericColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    UniqueName="TITLE_NAME">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ %>" DataField="TER_REASON"
                    UniqueName="TER_REASON" SortExpression="TER_REASON" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                  <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái phê duyệt%>" DataField="STATUS_NAME"
                    UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                  <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không phê duyệt %>" DataField="REASON_UNAPROVE"
                    UniqueName="REASON_UNAPROVE" SortExpression="REASON_UNAPROVE">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <br />
    <tlk:RadGrid PageSize=50 ID="rgWorkingBefore" runat="server" Height="250px" Width="99%">
        <MasterTableView DataKeyNames="ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON"
                         ClientDataKeyNames ="ID,COMPANY_NAME,JOIN_DATE,END_DATE,COMPANY_ADDRESS,TELEPHONE,SALARY,TITLE_NAME,LEVEL_NAME,TER_REASON"
            Caption="<%$ Translate: Thông tin hiện tại %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="COMPANY_NAME"
                    UniqueName="COMPANY_NAME" SortExpression="COMPANY_NAME" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="TELEPHONE"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    UniqueName="TELEPHONE">
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ công ty %>" DataField="COMPANY_ADDRESS"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    UniqueName="COMPANY_ADDRESS">
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào %>" DataField="JOIN_DATE"
                    UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nghỉ %>" DataField="END_DATE"
                    UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE"
                    ShowFilterIcon="true">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridNumericColumn DataField="SALARY" HeaderText="<%$ Translate: Mức lương %>"
                    UniqueName="SALARY" ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                    Visible="true" DataFormatString="{0:n0}">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="right" />
                </tlk:GridNumericColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    UniqueName="TITLE_NAME">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ %>" DataField="TER_REASON"
                    UniqueName="TER_REASON" SortExpression="TER_REASON" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" FilterControlWidth="100%">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function clientButtonClicking(sender, args)
            {
               
            }

           
        </script>
    </tlk:RadCodeBlock>
</div>


<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
