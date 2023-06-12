<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsArisingLeaveSheet.ascx.vb"
    Inherits="Insurance.ctrlInsArisingLeaveSheet" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:HiddenField runat="server" ID="hidID" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="140px">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tháng báo từ")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker runat="server" ID="rdFromMonth" TabIndex="5" Culture="en-US">
                                <DateInput ID="DateInput4" runat="server" DisplayDateFormat="MM/yyyy"></DateInput>
                            </tlk:RadMonthYearPicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến tháng")%>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker runat="server" ID="rdToMonth" TabIndex="5" Culture="en-US">
                                <DateInput ID="DateInput5" runat="server" DisplayDateFormat="MM/yyyy"></DateInput>
                            </tlk:RadMonthYearPicker>
                            <tlk:RadButton runat="server" ID="RadButton1" OnClientClicked="function (button, args){clearRadDatePicker();}" CausesValidation="false"                                    Text="Xóa" Width="25px" Style="margin-top: 0px">
                                </tlk:RadButton>
                        </td>
                        <td class="lb"></td>
                        <td>
                            <tlk:RadButton ID="btnFastComplate" runat="server" Text="<%$ Translate: Điền nhanh %>" SkinID="Button" Width="90px">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdFromLeave"></tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdToLeave"></tlk:RadDatePicker>
                        </td>
                         <td class="lb">
                            <%# Translate("Loại nghỉ")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboLeaveType" EnableCheckAllItemsCheckBox="true" DropDownAutoWidth="Enabled" SkinID="LoadDemand" CheckBoxes="true"></tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb"></td>
                        <td>
                            <tlk:RadButton ID="btnFind" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb"></td>
                        <td>
                            <tlk:RadButton ID="btnCreate" runat="server" Text="<%$ Translate: Tạo DL %>" SkinID="Button" Width="90px">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgGridData" runat="server" Height="100%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true" AllowFilteringByColumn="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="5" />
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                    </ClientSettings>
                    <MasterTableView TableLayout="Fixed" CommandItemDisplay="None" DataKeyNames="ID"
                        ClientDataKeyNames="ID,EMPLOYEE_ID,ORG_DESC">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" FilterControlWidth="99%" HeaderStyle-Width="70px"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" HeaderStyle-Width="150px" SortExpression="ORG_NAME" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" HeaderStyle-Width="150px" SortExpression="TITLE_NAME" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nghỉ phép %>" DataField="MANUAL_NAME"
                                UniqueName="MANUAL_NAME" HeaderStyle-Width="150px" SortExpression="MANUAL_NAME" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="LEAVE_FROM"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="LEAVE_FROM" SortExpression="LEAVE_FROM"
                                HeaderStyle-Width="85px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="LEAVE_TO"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="LEAVE_TO" SortExpression="LEAVE_TO"
                                HeaderStyle-Width="85px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày nghỉ %>" DataFormatString="{0:N1}"
                                DataField="DAY_NUM" UniqueName="DAY_NUM" SortExpression="DAY_NUM" HeaderStyle-Width="80px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tháng báo từ %>" DataField="FROM_MONTH"
                                DataFormatString="{0:MM/yyyy}" UniqueName="FROM_MONTH" SortExpression="FROM_MONTH"
                                HeaderStyle-Width="85px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến tháng  %>" DataField="TO_MONTH"
                                DataFormatString="{0:MM/yyyy}" UniqueName="TO_MONTH" SortExpression="TO_MONTH"
                                HeaderStyle-Width="85px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                        </Columns>
                    </MasterTableView>
                    <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
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

        function clearRadDatePicker() {
            var datepicker;
            var datepicker_1;
            datepicker = $find("<%= rdFromMonth.ClientID%>");
            datepicker1 = $find("<%= rdToMonth.ClientID%>");
            datepicker.clear();
            datepicker1.clear();
        }

    </script>
</tlk:RadCodeBlock>
