<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_ReportList.ascx.vb"
    Inherits="Attendance.ctrlAT_ReportList" %>
<%@ Import Namespace="Common" %>
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
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
                <table class="table-form padding-10" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                             <label id="lblYear" runat="server"><%# Translate("Năm")%></label> 
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" AutoPostBack="true" runat="server"  SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <label id="lblPeriod" runat="server"><%# Translate("Kỳ công")%></label> 
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server"  SkinID="dDropdownList">
                            </tlk:RadComboBox>
                        </td>
                             <td class="lb" >
                            <label id="lblNgay" runat="server"><%# Translate("Ngày")%></label> 
                        </td>
                        <td>

                            <tlk:RadDatePicker ID="rdDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                        <td class="lb">
                            <label id="lblTuNgay" runat="server"><%# Translate("Từ Ngày")%></label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server" >
                            </tlk:RadDatePicker>
                        </td>
                        
                        <td class="lb">
                            <label id="lblDenNgay" runat="server"><%# Translate("Đến Ngày")%></label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server" >
                            </tlk:RadDatePicker>
                        </td>
                     </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgReportList" runat="server" Height="100%" SkinID="GridSingleSelect">
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
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

    </script>
</tlk:RadScriptBlock>
