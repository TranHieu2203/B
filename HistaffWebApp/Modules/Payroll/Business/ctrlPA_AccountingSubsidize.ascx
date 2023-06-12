<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_AccountingSubsidize.ascx.vb"
    Inherits="Payroll.ctrlPA_AccountingSubsidize" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" AutoPostBack="true" runat="server" TabIndex="12" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label5" Text="Tháng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Đối tượng nhân viên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboObjEmp" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Từ ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdtungay" MaxLength="12" runat="server" ToolTip="" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdDenngay" MaxLength="12" runat="server" ToolTip="" Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label2" Text="Loại trợ cấp"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSubsidize" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip=""
                                SkinID="ButtonFind">
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
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,ORG_SET_ID,IS_LOCK"
                        ClientDataKeyNames="ID,EMPLOYEE_ID,ORG_SET_ID,IS_LOCK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_SET_NAME"
                                SortExpression="ORG_SET_NAME" UniqueName="ORG_SET_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridCheckBoxColumn HeaderText="Kiêm nhiệm" DataField="IS_KIEMNHIEM"
                                SortExpression="IS_KIEMNHIEM" UniqueName="IS_KIEMNHIEM" HeaderStyle-Width="60px"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false"  ReadOnly="true" /> 

                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            
                            <tlk:GridBoundColumn HeaderText="Loại trợ cấp" DataField="SUBSIDIZE_TYPE_NAME"
                                SortExpression="SUBSIDIZE_TYPE_NAME" UniqueName="SUBSIDIZE_TYPE_NAME" HeaderStyle-Width="100px"/>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày chu kỳ công %>" DataField="NUMBERDAY_PERIOD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="NUMBERDAY_PERIOD"
                                UniqueName="NUMBERDAY_PERIOD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số ngày làm việc vị trí QLCH/TC %>" DataField="NUMBERWORKING_PERIOD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="NUMBERWORKING_PERIOD"
                                UniqueName="NUMBERWORKING_PERIOD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Ngày nghỉ TS  %>" DataField="BHXH"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BHXH"
                                UniqueName="BHXH">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Định mức trợ cấp  %>" DataField="NORM_MONEY"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="NORM_MONEY"
                                UniqueName="NORM_MONEY">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Mức thưởng  %>" DataField="VALUE"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="VALUE"
                                UniqueName="VALUE">
                            </tlk:GridNumericColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate:Trạng thái khóa  %>" DataField="IS_LOCK"
                                ItemStyle-HorizontalAlign="Center" AllowFiltering="false" SortExpression="IS_LOCK" 
                                UniqueName="IS_LOCK"  ReadOnly="true" HeaderStyle-Width="60px">
                            </tlk:GridCheckBoxColumn>
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
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgData.ClientID%>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
