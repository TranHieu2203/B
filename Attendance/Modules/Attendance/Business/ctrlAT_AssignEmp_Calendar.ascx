<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_AssignEmp_Calendar.ascx.vb"
    Inherits="Attendance.ctrlAT_AssignEmp_Calendar" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" AutoPostBack="true" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Height="70px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Năm")%>             
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true"
                        TabIndex="12" Width="80px">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Lịch làm việc")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboWorkSchedule" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6" style="width: 50%">
                    <%# Translate("Danh sách nhân viên chưa gán lịch")%>
                     <tlk:RadTextBox runat="server" ID="txtEmpNotAssign" Enabled="false">
                    </tlk:RadTextBox>
                </td>
                <td style="min-width: 50px"></td>
                <td class="item-head" colspan="6" style="width: 50%">
                    <%# Translate("Danh sách nhân viên đã gán lịch")%>
                    <tlk:RadTextBox runat="server" ID="txtEmpAssign" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None" Width="100%" Height="100%">
                <tlk:RadGrid PageSize="50" ID="rgCanNotSchedule" runat="server" Height="85%" AllowPaging="false" AllowSorting="false" ClientSettings-Scrolling-FrozenColumnsCount="3">
                    <MasterTableView DataKeyNames="EMPLOYEE_ID,EMPLOYEE_NAME,JOIN_DATE" AllowPaging="false">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên lịch %>" DataField="CALENDAR_NAME"
                                UniqueName="CALENDAR_NAME" SortExpression="CALENDAR_NAME" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="50px">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnInsert" runat="server" Font-Bold="true" Text=">">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnDelete" runat="server" Font-Bold="true" Text="<">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnInsertAll" runat="server" Font-Bold="true" Text=">>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnDeleteAll" runat="server" Font-Bold="true" Text="<<">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Width="100%" Height="100%">
                <tlk:RadGrid AllowPaging="false"  ID="rgCanSchedule" runat="server" Height="85%" AllowSorting="false" ClientSettings-Scrolling-FrozenColumnsCount="3">
                    <MasterTableView DataKeyNames="ID" AllowPaging="false">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
