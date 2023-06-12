<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_AccountingOvertime.ascx.vb"
    Inherits="Payroll.ctrlPA_AccountingOvertime" %>
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
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
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
                            <asp:Label runat="server" ID="Label5" Text="Kỳ công"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server">
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
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,IS_LOCK"
                        ClientDataKeyNames="ID,EMPLOYEE_ID,IS_LOCK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="Phòng ban hạch toán" DataField="ORG_SET_NAME"
                                SortExpression="ORG_SET_NAME" UniqueName="ORG_SET_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Năm & tháng" DataField="PERIOD_NAME"
                                SortExpression="PERIOD_NAME" UniqueName="PERIOD_NAME" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" ReadOnly="true" />
                            
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT ngày thường mức cũ  %>" DataField="OT_DAY_OLD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_DAY_OLD"
                                UniqueName="OT_DAY_OLD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT đêm thường mức cũ  %>" DataField="OT_NIGHT_OLD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_NIGHT_OLD"
                                UniqueName="OT_NIGHT_OLD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT ngày nghỉ mức cũ  %>" DataField="OT_OFFDAY_OLD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_OFFDAY_OLD"
                                UniqueName="OT_OFFDAY_OLD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT đêm nghỉ mức cũ  %>" DataField="OT_OFFNIGHT_OLD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_OFFNIGHT_OLD"
                                UniqueName="OT_OFFNIGHT_OLD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT ngày lễ mức cũ  %>" DataField="OT_LEDAY_OLD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_LEDAY_OLD"
                                UniqueName="OT_LEDAY_OLD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT đêm lễ mức cũ  %>" DataField="OT_LENIGHT_OLD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_LENIGHT_OLD"
                                UniqueName="OT_LENIGHT_OLD">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Lương mức cũ  %>" DataField="SALARY_OLD"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SALARY_OLD"
                                UniqueName="SALARY_OLD">
                            </tlk:GridNumericColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT ngày thường mức mới  %>" DataField="OT_DAY_NEW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_DAY_NEW"
                                UniqueName="OT_DAY_NEW">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT đêm thường mức mới  %>" DataField="OT_NIGHT_NEW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_NIGHT_NEW"
                                UniqueName="OT_NIGHT_NEW">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT ngày nghỉ mức mới  %>" DataField="OT_OFFDAY_NEW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_OFFDAY_NEW"
                                UniqueName="OT_OFFDAY_NEW">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT đêm nghỉ mức mới  %>" DataField="OT_OFFNIGHT_NEW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_OFFNIGHT_NEW"
                                UniqueName="OT_OFFNIGHT_NEW">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT ngày lễ mức mới  %>" DataField="OT_LEDAY_NEW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_LEDAY_NEW"
                                UniqueName="OT_LEDAY_NEW">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:OT đêm lễ mức mới  %>" DataField="OT_LENIGHT_NEW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="OT_LENIGHT_NEW"
                                UniqueName="OT_LENIGHT_NEW">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate:Lương mức mới  %>" DataField="SALARY_NEW"
                                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:n2}" SortExpression="SALARY_NEW"
                                UniqueName="SALARY_NEW">
                            </tlk:GridNumericColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate:Trạng thái khóa  %>" DataField="IS_LOCK"
                                ItemStyle-HorizontalAlign="Center" SortExpression="IS_LOCK" HeaderStyle-Width="60px" AllowFiltering="false"
                                UniqueName="IS_LOCK">
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
        function clientButtonClicking(sender, args) {
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
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
