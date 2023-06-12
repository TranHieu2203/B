<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ReportList.ascx.vb"
    Inherits="Profile.ctrlHU_ReportList" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" Scrolling="None" Width="300px">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form padding-10">
                    <tr>
                        <td class="lb">
                            <label id="lblFromDate" runat="server">
                                <%# Translate("Từ ngày")%></label>
                            <label id="lblMonth" runat="server">
                                <%# Translate("THÁNG:")%></label>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker ID="rdMonth" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                                DateInput-DateFormat="dd/MMM/yyyy" readonly="true">
                            </tlk:RadMonthYearPicker>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <label id="lblToDate" runat="server">
                                <%# Translate("Đến ngày")%></label>
                        </td>
                        <td>
                            <label id="lblMonthMin" runat="server">
                                <%# Translate("THÁNG:")%></label>
                            <tlk:RadComboBox ID="cboMonth" AutoPostBack="true" runat="server" SkinID="dDropdownList"
                                Width="60px">
                            </tlk:RadComboBox>
                            <label id="lblYear" runat="server">
                                <%# Translate("NĂM:")%></label>
                            <tlk:RadComboBox ID="cboYear" AutoPostBack="true" runat="server" SkinID="dDropdownList"
                                Width="60px">
                            </tlk:RadComboBox>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày %>"
                                ErrorMessage="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày %>" Type="Date" Operator="GreaterThan"
                                ControlToCompare="rdFromDate" ControlToValidate="rdToDate"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgEmployeeList" runat="server" Height="100%" SkinID="GridSingleSelect">
                    <MasterTableView DataKeyNames="ID,CODE,NAME" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã báo cáo %>" DataField="CODE" UniqueName="CODE"
                                SortExpression="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên báo cáo %>" DataField="NAME"
                                UniqueName="NAME" SortExpression="NAME" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ClientSettings EnablePostBackOnRowClick="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs)
        {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args)
        {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT")
            {
                enableAjax = false;
            }
        }

        
    </script>
</tlk:RadScriptBlock>
