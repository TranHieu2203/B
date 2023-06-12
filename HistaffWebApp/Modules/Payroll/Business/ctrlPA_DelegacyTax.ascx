<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_DelegacyTax.ascx.vb"
    Inherits="Payroll.ctrlPA_DelegacyTax" %>
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
            <tlk:RadPane ID="RadPane1" runat="server" Height="130px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" TabIndex="1" Width="80px">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboEmpType" SkinID="dDropdownList" runat="server" TabIndex="2">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày vào làm từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromdate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến ngày")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
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
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox ID="chkAllIn" runat="server" Text="Lưu tất cả NV theo điều kiện lọc" />
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgDelegacy" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true" AllowMultiRowEdit="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,PIT_CODE,EMPLOYEE_CODE"
                        ClientDataKeyNames="ID,EMPLOYEE_ID,PIT_CODE" EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Ban/Phòng" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Ngày vào làm" DataField="JOIN_DATE"
                                SortExpression="JOIN_DATE" UniqueName="JOIN_DATE" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="Ngày sinh" DataField="BIRTH_DATE"
                                SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="Giới tính" DataField="GENDER"
                                SortExpression="GENDER" UniqueName="GENDER" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Số điện thoại" DataField="MOBILE_PHONE"
                                SortExpression="MOBILE_PHONE" UniqueName="MOBILE_PHONE" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Email" DataField="EMAIL"
                                SortExpression="EMAIL" UniqueName="EMAIL" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Mã số thuế" DataField="PIT_CODE"
                                SortExpression="PIT_CODE" UniqueName="PIT_CODE" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Loại nhân viên" DataField="EMP_STATUS_NAME"
                                SortExpression="EMP_STATUS_NAME" UniqueName="EMP_STATUS_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridTemplateColumn HeaderText="Ủy quyền PIT" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                    SortExpression="DELEGACY" DataField="DELEGACY" UniqueName="DELEGACY" ColumnGroupName="GeneralInformation" HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID ="chkDELEGACY" Checked='<%# DataBinder.Eval(Container.DataItem, "DELEGACY") %>'/>
                                </ItemTemplate>
                             </tlk:GridTemplateColumn>
                            <%--<tlk:GridCheckBoxColumn HeaderText="Ủy quyền PIT" DataField="DELEGACY" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                SortExpression="DELEGACY" UniqueName="DELEGACY" HeaderStyle-Width="70px"/>--%>
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
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgDelegacy.ClientID%>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
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
