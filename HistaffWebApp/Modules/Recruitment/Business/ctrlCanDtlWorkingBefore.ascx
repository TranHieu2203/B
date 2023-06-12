<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCanDtlWorkingBefore.ascx.vb"
    Inherits="Recruitment.ctrlCanDtlWorkingBefore" %>
<%@ Register Src="../Shared/ctrlCanBasicInfo.ascx" TagName="ctrlCanBasicInfo" TagPrefix="Recruitment" %>
<asp:HiddenField ID="hidCandidateID" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="33px" Scrolling="none">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="70px" Scrolling="None">
        <Recruitment:ctrlCanBasicInfo runat="server" ID="ctrlCanBasicInfo" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server">
        <fieldset>
            <legend>
                <%# Translate("Thông tin chi tiết") %></legend>
            <table class="table-form" style="padding-left: 32px">
                <tr>
                    <td colspan="4">
                        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Tên công ty") %><span class="lbReq"> *</span>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtCompanyName">
                        </tlk:RadTextBox>
                        <asp:RequiredFieldValidator ID="reqCompanyName" ControlToValidate="txtCompanyName"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên công ty %>" ToolTip="<%$ Translate: Bạn phải nhập tên công ty  %>">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
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
                        <%# Translate("Vị trí ban đầu") %>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtTitleFirst">
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Cấp trên trực tiếp")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtManagerFirst">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Vị trí khi nghỉ") %>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtTitleLast">
                        </tlk:RadTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Cấp trên trực tiếp")%>
                    </td>
                    <td>
                        <tlk:RadTextBox runat="server" ID="txtManagerLast">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Mức lương ban đầu")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox runat="server" ID="txtSalaryFirst" MinValue="0" MaxLength="9"
                            NumberFormat-DecimalDigits="0">
                        </tlk:RadNumericTextBox>
                    </td>
                    <td class="lb">
                        <%# Translate("Mức lương khi nghỉ")%>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox runat="server" ID="txtSalaryLast" MinValue="0" MaxLength="9"
                            NumberFormat-DecimalDigits="0">
                        </tlk:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Nhiệm vụ chính") %>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox runat="server" ID="txtJob" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Lý do nghỉ việc") %>
                    </td>
                    <td colspan="3">
                        <tlk:RadTextBox runat="server" ID="txtTerReason" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </fieldset>
        <tlk:RadGrid ID="rgGrid" runat="server" AllowMultiRowSelection="true" Width="99%"
            AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn>
                        <HeaderStyle Width="40px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công ty %>" DataField="COMPANY_NAME"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        UniqueName="COMPANY_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày vào %>" DataField="JOIN_DATE"
                        ShowFilterIcon="true" AutoPostBackOnFilter="true" UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày nghỉ %>" DataField="END_DATE"
                        ShowFilterIcon="true" AutoPostBackOnFilter="true" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí ban đầu %>" DataField="TITLE_FIRST"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        UniqueName="TITLE_FIRST">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí khi nghỉ %>" DataField="TITLE_LAST"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        UniqueName="TITLE_LAST">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SALARY_FIRST" HeaderText="Mức lương ban đầu" UniqueName="SALARY_FIRST"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        Visible="true" DataFormatString="{0:###,###}">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SALARY_LAST" HeaderText="Mức lương khi nghỉ" UniqueName="SALARY_LAST"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo"
                        Visible="true" DataFormatString="{0:###,###}">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do nghỉ %>" DataField="TER_REASON"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        UniqueName="TER_REASON">
                        <HeaderStyle Width="20%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhiệm vụ chính %>" DataField="JOB"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        UniqueName="JOB" Visible="false">
                        <HeaderStyle Width="10%" HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="COMPANY_ADDRESS" HeaderText="COMPANY_ADDRESS" UniqueName="COMPANY_ADDRESS"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TELEPHONE" HeaderText="TELEPHONE" UniqueName="TELEPHONE"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="MANAGER_FIRST" HeaderText="MANAGER_FIRST" UniqueName="MANAGER_FIRST"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="MANAGER_LAST" HeaderText="MANAGER_LAST" UniqueName="MANAGER_LAST"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                        Visible="False">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="ID" UniqueName="ID" Visible="False">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
