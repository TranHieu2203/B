<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_BenefitSeniority.ascx.vb"
    Inherits="Payroll.ctrlPA_BenefitSeniority" %>
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
                        <td>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
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
                <tlk:RadGrid PageSize="50" ID="rgBenifits" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID"
                        ClientDataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px"/>
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Ban/Phòng" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày vào làm" DataField="JOIN_DATE"
                                SortExpression="JOIN_DATE" UniqueName="JOIN_DATE" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" />

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T1  %>" DataField="SENIORITY_M1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M1"
                                UniqueName="SENIORITY_M1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T1  %>" DataField="BENEFIT_MONTH_01"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_01"
                                UniqueName="BENEFIT_MONTH_01">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T2  %>" DataField="SENIORITY_M2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M2"
                                UniqueName="SENIORITY_M2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T2  %>" DataField="BENEFIT_MONTH_02"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_02"
                                UniqueName="BENEFIT_MONTH_02">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T3  %>" DataField="SENIORITY_M3"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M3"
                                UniqueName="SENIORITY_M3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T3  %>" DataField="BENEFIT_MONTH_03"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_03"
                                UniqueName="BENEFIT_MONTH_03">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T4  %>" DataField="SENIORITY_M4"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M4"
                                UniqueName="SENIORITY_M4">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T4  %>" DataField="BENEFIT_MONTH_04"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_04"
                                UniqueName="BENEFIT_MONTH_04">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T5  %>" DataField="SENIORITY_M5"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M5"
                                UniqueName="SENIORITY_M5">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T5  %>" DataField="BENEFIT_MONTH_05"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_05"
                                UniqueName="BENEFIT_MONTH_05">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T6  %>" DataField="SENIORITY_M6"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M6"
                                UniqueName="SENIORITY_M6">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T6  %>" DataField="BENEFIT_MONTH_06"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_06"
                                UniqueName="BENEFIT_MONTH_06">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T7  %>" DataField="SENIORITY_M7"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M7"
                                UniqueName="SENIORITY_M7">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T7  %>" DataField="BENEFIT_MONTH_07"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_07"
                                UniqueName="BENEFIT_MONTH_07">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T8  %>" DataField="SENIORITY_M8"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M8"
                                UniqueName="SENIORITY_M8">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T8  %>" DataField="BENEFIT_MONTH_08"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_08"
                                UniqueName="BENEFIT_MONTH_08">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T9  %>" DataField="SENIORITY_M9"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M9"
                                UniqueName="SENIORITY_M9">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T9  %>" DataField="BENEFIT_MONTH_09"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_09"
                                UniqueName="BENEFIT_MONTH_09">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T10  %>" DataField="SENIORITY_M10"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M10"
                                UniqueName="SENIORITY_M10">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T10  %>" DataField="BENEFIT_MONTH_10"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_10"
                                UniqueName="BENEFIT_MONTH_10">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T11  %>" DataField="SENIORITY_M11"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M11"
                                UniqueName="SENIORITY_M11">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T11  %>" DataField="BENEFIT_MONTH_11"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_11"
                                UniqueName="BENEFIT_MONTH_11">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Số năm thâm niên T12  %>" DataField="SENIORITY_M12"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SENIORITY_M12"
                                UniqueName="SENIORITY_M12">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tiền thâm niên T12  %>" DataField="BENEFIT_MONTH_12"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_MONTH_12"
                                UniqueName="BENEFIT_MONTH_12">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng tiền thâm niên  %>" DataField="BENEFIT_TOTAL"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_TOTAL"
                                UniqueName="BENEFIT_TOTAL">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng phụ cấp thâm niên quý 1  %>" DataField="BENEFIT_QUARTER1"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_QUARTER1"
                                UniqueName="BENEFIT_QUARTER1">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng phụ cấp thâm niên quý 2  %>" DataField="BENEFIT_QUARTER2"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_QUARTER2"
                                UniqueName="BENEFIT_QUARTER2">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng phụ cấp thâm niên quý 3 %>" DataField="BENEFIT_QUARTER3"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_QUARTER3"
                                UniqueName="BENEFIT_QUARTER3">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Tổng phụ cấp thâm niên quý 4  %>" DataField="BENEFIT_QUARTER4"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="BENEFIT_QUARTER4"
                                UniqueName="BENEFIT_QUARTER4">
                            </tlk:GridNumericColumn>
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
            if (args.get_item().get_commandName() == "SAVE") {
                var bCheck = $find('<%= rgBenifits.ClientID%>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgBenifits.ClientID%>').get_masterTableView().get_dataItems().length;
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
