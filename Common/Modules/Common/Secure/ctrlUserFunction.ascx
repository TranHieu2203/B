<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="ctrlUserFunction.ascx.vb"
    Inherits="Common.ctrlUserFunction" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" MinHeight="30" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" Width="100%">
        </tlk:RadToolBar>
        <div style="margin-top: 10px" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="GridPlaceHolder" runat="server">
            <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                <tlk:RadPane ID="RadPane3" runat="server" Height="40px" Scrolling="None">
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
                            <%# Translate("Nhóm chức năng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboFunctionGroup">
                            </tlk:RadComboBox>
                        </td>
                            <td class="lb">
                                <%# Translate("Tên chức năng")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtFUNCTION_NAME" runat="server">
                                </tlk:RadTextBox>
                            </td>
                            <td>
                                <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
                </tlk:RadPane>
                <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                    <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" Height="100%">
                        <MasterTableView DataKeyNames="ID">
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn DataField="ID" Visible="False" />
                                <tlk:GridBoundColumn DataField="FUNCTION_CODE" HeaderText="<%$ Translate: Mã chức năng Visible="false"%>"
                                    UniqueName="FUNCTION_CODE" SortExpression="FUNCTION_CODE" HeaderStyle-Width="10%" />
                                <tlk:GridBoundColumn DataField="FUNCTION_NAME" HeaderText="<%$ Translate: Tên chức năng %>"
                                    UniqueName="FUNCTION_NAME" SortExpression="FUNCTION_NAME" HeaderStyle-Width="25%" />
                                <tlk:GridBoundColumn DataField="FUNCTION_GROUP_NAME" HeaderText="<%$ Translate: Nhóm chức năng %>"
                                    UniqueName="FUNCTION_GROUP_NAME" SortExpression="FUNCTION_GROUP_NAME" HeaderStyle-Width="25%" />
                                <tlk:GridBoundColumn DataField="MODULE_NAME" HeaderText="<%$ Translate: Tên phân hệ %>"
                                    UniqueName="MODULE_NAME" SortExpression="MODULE_NAME" HeaderStyle-Width="10%" />
                                <tlk:GridTemplateColumn UniqueName="ALLOW_CREATE">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_CREATE") %><br />
                                        <asp:CheckBox ID="chkCREATE_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCREATE" runat="server" Checked='<%#CBool(Eval("ALLOW_CREATE"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_MODIFY">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_MODIFY")%><br />
                                        <asp:CheckBox ID="chkMODIFY_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkMODIFY" runat="server" Checked='<%#CBool(Eval("ALLOW_MODIFY"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_DELETE">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_DELETE")%><br />
                                        <asp:CheckBox ID="chkDELETE_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDELETE" runat="server" Checked='<%#CBool(Eval("ALLOW_DELETE"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_PRINT">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_PRINT")%><br />
                                        <asp:CheckBox ID="chkPRINT_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkPRINT" runat="server" Checked='<%#CBool(Eval("ALLOW_PRINT"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_IMPORT">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_IMPORT")%><br />
                                        <asp:CheckBox ID="chkIMPORT_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIMPORT" runat="server" Checked='<%#CBool(Eval("ALLOW_IMPORT"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_EXPORT">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_EXPORT")%><br />
                                        <asp:CheckBox ID="chkEXPORT_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEXPORT" runat="server" Checked='<%#CBool(Eval("ALLOW_EXPORT"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL1">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_SPECIAL1")%><br />
                                        <asp:CheckBox ID="chkSPECIAL1_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSPECIAL1" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL1"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL2">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_SPECIAL2")%><br />
                                        <asp:CheckBox ID="chkSPECIAL2_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSPECIAL2" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL2"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL3">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_SPECIAL3")%><br />
                                        <asp:CheckBox ID="chkSPECIAL3_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSPECIAL3" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL3"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL4">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_SPECIAL4")%><br />
                                        <asp:CheckBox ID="chkSPECIAL4_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSPECIAL4" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL4"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL5">
                                    <HeaderTemplate>
                                        <%# Translate("ALLOW_SPECIAL5")%><br />
                                        <asp:CheckBox ID="chkSPECIAL5_ALL" runat="server" OnCheckedChanged="CheckBox_CheckChanged"
                                            AutoPostBack="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSPECIAL5" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL5"))%>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                </tlk:RadPane>
            </tlk:RadSplitter>
        </asp:PlaceHolder>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
