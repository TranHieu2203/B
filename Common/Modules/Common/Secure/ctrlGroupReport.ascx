<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGroupReport.ascx.vb"
    Inherits="Common.ctrlGroupReport" %>
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
                    <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridTemplateColumn UniqueName="IS_USE">
                        <HeaderTemplate>
                            <asp:CheckBox ID="cbSELECT_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbIS_USE" runat="server" Checked='<%#CBool(Eval("IS_USE"))%>' />
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
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
