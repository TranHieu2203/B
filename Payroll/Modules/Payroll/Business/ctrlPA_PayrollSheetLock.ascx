<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_PayrollSheetLock.ascx.vb"
    Inherits="Payroll.ctrlPA_PayrollSheetLock" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" CheckBoxes="All" CheckChildNodes="true"/>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
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
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" Width="160px" MaxLength="80" runat="server" ToolTip="">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkLockByOrg" Text="Khóa/Mở tất cả dữ liệu theo phòng ban" />
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
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="120px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Họ & tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Đối tượng nhân viên" DataField="OBJ_EMPLOYEE_NAME"
                                SortExpression="OBJ_EMPLOYEE_NAME" UniqueName="OBJ_EMPLOYEE_NAME" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập thực nhận" DataField="TT_THUNHAP_CONLAI" DataFormatString="{0:N2}"
                                SortExpression="TT_THUNHAP_CONLAI" UniqueName="TT_THUNHAP_CONLAI" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Tháng lương" DataField="PERIOD_MONTH"
                                SortExpression="PERIOD_MONTH" UniqueName="PERIOD_MONTH" HeaderStyle-Width="100px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Đã Khóa" DataField="IS_LOCK_TEXT"
                                SortExpression="IS_LOCK_TEXT" UniqueName="IS_LOCK_TEXT" HeaderStyle-Width="50px" ReadOnly="true" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;

        function OnClientButtonClicking(sender, args) {
            var m;
            var bCheck;
            if (args.get_item().get_commandName() == "EXPORT") {
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
