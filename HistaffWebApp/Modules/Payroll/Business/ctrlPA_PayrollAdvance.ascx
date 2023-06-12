<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_PayrollAdvance.ascx.vb"
    Inherits="Payroll.ctrlPA_PayrollAdvance" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server"/>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="130px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" TabIndex="12" Width="80px" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="160px" MaxLength="80" runat="server" ToolTip="" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đối tượng nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboEmpObj" runat="server"  AutoPostBack="true"
                                Width="150px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbPeriod"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chốt công từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" runat="server"
                                ToolTip="">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                        </td>
                        <td>
                            <asp:CheckBox ID="chkLock" Text="Đã khóa tạm ứng" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số ngày nghỉ không lương <=")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="ntxtNotSalary" runat="server" SkinID="Number" Width="40px"></tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số ngày nghỉ có lương <=")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="ntxtSalary" runat="server" SkinID="Number" Width="40px"></tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                        </td>
                        <td>
                            <asp:CheckBox ID="chkUnLock" Text="Chưa khóa tạm ứng" runat="server"></asp:CheckBox>
                        </td>
                        <td class="lb">
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:CheckBox ID="chkAll" Text="Khóa/Mở tất cả dữ liệu theo phòng ban" runat="server" Width="100%"></asp:CheckBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số tiền tạm ứng")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="ntxtSalAdvance" runat="server" SkinID="Money"></tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <tlk:RadButton ID="btnEdit" Text="<%$ Translate: Cập nhật%>" runat="server" Width="85px">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="EMPLOYEE_ID"
                        ClientDataKeyNames="EMPLOYEE_ID,IS_LOCK,EMPLOYEE_CODE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Đã Khóa" DataField="LOCK_NAME"
                                SortExpression="LOCK_NAME" UniqueName="LOCK_NAME" HeaderStyle-Width="50px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="120px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Họ & tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Mức lương hiện tại" DataField="SALARY_CONTRACT" DataFormatString="{0:N0}"
                                SortExpression="SALARY_CONTRACT" UniqueName="SALARY_CONTRACT" HeaderStyle-Width="100px" ReadOnly="true" />
                             <tlk:GridBoundColumn HeaderText="Kỳ lương" DataField="PERIOD_T"
                                SortExpression="PERIOD_T" UniqueName="PERIOD_T" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Số tiền tạm ứng" DataField="SALARY_ADVANCE" DataFormatString="{0:N0}"
                                SortExpression="SALARY_ADVANCE" UniqueName="SALARY_ADVANCE" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Công chuẩn" DataField="WORK_STANDARD" DataFormatString="{0:N2}"
                                SortExpression="WORK_STANDARD" UniqueName="WORK_STANDARD" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Công làm thực tế" DataField="WORKING_X" DataFormatString="{0:N2}"
                                SortExpression="WORKING_X" UniqueName="WORKING_X" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Số ngày nghỉ có lương" DataField="WORKING_HAVE_SALARY" DataFormatString="{0:N2}"
                                SortExpression="WORKING_HAVE_SALARY" UniqueName="WORKING_HAVE_SALARY" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Số ngày nghỉ không lương" DataField="WORKING_NO_SALARY" DataFormatString="{0:N2}"
                                SortExpression="WORKING_NO_SALARY" UniqueName="WORKING_NO_SALARY" HeaderStyle-Width="100px" ReadOnly="true" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;

        function OnClientButtonClicking(sender, args) {
            var m;
            var bCheck;
            if (args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == 'EXPORT_TEMPLATE' || args.get_item().get_commandName() == "IMPORT") {
                enableAjax = false;
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
