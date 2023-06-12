<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGroupReport.ascx.vb"
    Inherits="Common.ctrlGroupReport" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="40px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Tên phân hệ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboMODULE" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Tên báo cáo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtREPORT_NAME" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td style="padding: 0px 20px 0px 20px; vertical-align: middle">
                    <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridTemplateColumn UniqueName="IS_USE">
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbSELECT_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbIS_USE" runat="server" Checked='<%# If(rgGrid.Columns.Contains("IS_USE") = True, CBool(Eval("IS_USE")), False) %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên báo cáo %>" DataField="REPORT_NAME"
                        UniqueName="REPORT_NAME" SortExpression="REPORT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phân hệ %>" DataField="MODULE_NAME"
                        UniqueName="MODULE_NAME" SortExpression="MODULE_NAME" />
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnGridCreated="GridCreated" />
           </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('ctl00_MainContent_ctrlGroup_ctrlGroupReport_RadSplitter1');
        }

        $(document).ready(function () {
            registerOnfocusOut('ctl00_MainContent_ctrlGroup_ctrlGroupReport_RadSplitter1');
        });
    </script>
</tlk:RadScriptBlock>