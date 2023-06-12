<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="ctrlUserFunctionExceptionEdit.ascx.vb"
    Inherits="Common.ctrlUserFunctionExceptionEdit" %>
<%@ Register Src="../ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
</style>
<asp:HiddenField ID="hidID" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" MinHeight="30" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" Width="100%">
        </tlk:RadToolBar>
        <div style="margin-top: 10px" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Vertical">
            <tlk:RadPane ID="RadPane3" runat="server" Height="60px" Scrolling="None"  width="300px">
            <common:ctrlorganization id="ctrlOrg" runat="server" CheckBoxes="All" CheckChildNodes="true"/>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server">
                <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID" EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="False" />
                            <tlk:GridBoundColumn DataField="FUNCTION_CODE" HeaderText="<%$ Translate: Mã chức năng %>"
                                UniqueName="FUNCTION_CODE" SortExpression="FUNCTION_CODE" HeaderStyle-Width="10%" Visible="false" />
                            <tlk:GridBoundColumn DataField="FUNCTION_NAME" HeaderText="<%$ Translate: Tên chức năng %>"
                                UniqueName="FUNCTION_NAME" SortExpression="FUNCTION_NAME" HeaderStyle-Width="20%" />
                            <tlk:GridBoundColumn DataField="FUNCTION_GROUP_NAME" HeaderText="<%$ Translate: Nhóm chức năng %>"
                                UniqueName="FUNCTION_GROUP_NAME" SortExpression="FUNCTION_GROUP_NAME" HeaderStyle-Width="10%" />
                            <tlk:GridBoundColumn DataField="MODULE_NAME" HeaderText="<%$ Translate: Tên phân hệ %>"
                                UniqueName="MODULE_NAME" SortExpression="MODULE_NAME" HeaderStyle-Width="10%" />
                            <tlk:GridTemplateColumn UniqueName="WORK_LEVEL_NAME">
                                <HeaderTemplate>Cấp bậc<br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tlk:RadComboBox runat="server"  ID="cboWorkLevel" EnableCheckAllItemsCheckBox="true" CheckBoxes="true" 
                                        CausesValidation="false">
                                    </tlk:RadComboBox>
                                </ItemTemplate>
                                <HeaderStyle Width="18%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" width="100px"/>
                            </tlk:GridTemplateColumn>

                            <tlk:GridTemplateColumn UniqueName="ALLOW_CREATE">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_CREATE") %><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCREATE" runat="server" Checked='<%#CBool(Eval("ALLOW_CREATE"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_MODIFY">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_MODIFY")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMODIFY" runat="server" Checked='<%#CBool(Eval("ALLOW_MODIFY"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_DELETE">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_DELETE")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDELETE" runat="server" Checked='<%#CBool(Eval("ALLOW_DELETE"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_PRINT">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_PRINT")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkPRINT" runat="server" Checked='<%#CBool(Eval("ALLOW_PRINT"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_IMPORT">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_IMPORT")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIMPORT" runat="server" Checked='<%#CBool(Eval("ALLOW_IMPORT"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_EXPORT">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_EXPORT")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkEXPORT" runat="server" Checked='<%#CBool(Eval("ALLOW_EXPORT"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="4%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL1">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_SPECIAL1")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSPECIAL1" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL1"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="7%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL2">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_SPECIAL2")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSPECIAL2" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL2"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="8%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL3">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_SPECIAL3")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSPECIAL3" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL3"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL4">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_SPECIAL4")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSPECIAL4" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL4"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                            <tlk:GridTemplateColumn UniqueName="ALLOW_SPECIAL5">
                                <HeaderTemplate>
                                    <%# Translate("ALLOW_SPECIAL5")%><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSPECIAL5" runat="server" Checked='<%#CBool(Eval("ALLOW_SPECIAL5"))%>'
                                         />
                                </ItemTemplate>
                                <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

    </script>
</tlk:RadCodeBlock>