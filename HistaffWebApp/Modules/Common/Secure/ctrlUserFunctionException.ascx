<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="ctrlUserFunctionException.ascx.vb"
    Inherits="Common.ctrlUserFunctionException" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #ctl00_MainContent_ctrlUserException_ctrlUserFunctionException_rgGrid .ButtonFind{
        width: 100px!important;
    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" MinHeight="30" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking">
        </tlk:RadToolBar>
        <div style="margin-top: 10px" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
        <asp:PlaceHolder ID="GridPlaceHolder" runat="server">
            <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                <tlk:RadPane ID="RadPane3" runat="server" Height="60px" Scrolling="None">
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
                            <td class="lb">
                                <%# Translate("Nơi làm việc")%>
                            </td>
                            <td>
                                <tlk:RadComboBox runat="server" ID="cboLocation">
                                </tlk:RadComboBox>
                            </td>
                            <td>
                                <tlk:RadButton ID="btnFIND" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
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
                                    <HeaderTemplate>
                                        <%# Translate("Cấp bậc") %><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tlk:RadComboBox runat="server" Enabled="false" ID="cboWorkLevel" EnableCheckAllItemsCheckBox="true" CheckBoxes="true" AutoPostBack="true" 
                                            CausesValidation="false" OnSelectedIndexChanged="Combobox_ItemChecked">
                                        </tlk:RadComboBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="18%" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" width="100px"/>
                                </tlk:GridTemplateColumn>

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
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
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
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
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
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
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
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
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
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
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
                                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
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
                                    <HeaderStyle Width="7%" HorizontalAlign="Center" />
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
                                    <HeaderStyle Width="8%" HorizontalAlign="Center" />
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
                        <ClientSettings EnableRowHoverStyle="true">
                            <ClientEvents OnGridCreated="GridCreated" />
                            <ClientEvents OnCommand="ValidateFilter" />
                        </ClientSettings>
                    </tlk:RadGrid>
                </tlk:RadPane>
            </tlk:RadSplitter>
        </asp:PlaceHolder>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientClose="popupclose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Phân quyền ngoại lệ người dùng %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlUserException_ctrlUserFunctionException_RadSplitter1';
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EDIT") {
                var userId = $("#MainContent_ctrlUserException_hidID").val();
                var oWindow = radopen('Dialog.aspx?mid=Common&fid=ctrlUserFunctionExceptionEdit&group=Secure&noscroll=1&IDSelect=' + userId, "rwPopup");
                var pos = $("html").offset();
                oWindow.moveTo(pos.left, pos.top);
                oWindow.maximize();
                oWindow.center();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "CREATE") {
                document.getElementById("ctl00_MainContent_ctrlUserException_rgGrid").style.display = "none";
                document.getElementById("ctl00_MainContent_ctrlUserException_ctrlOrg_trvOrgPostback").style.display = "block";
            }
            if (args.get_item().get_commandName() == "CANCEL") {
                document.getElementById("ctl00_MainContent_ctrlUserException_rgGrid").style.display = "block";
                document.getElementById("ctl00_MainContent_ctrlUserException_ctrlOrg_trvOrgPostback").style.display = "none";
            }
            if (args.get_item().get_commandName() == 'SAVE') {
                if ($('#ctl00_MainContent_ctrlUserException_ctrlUserFunctionException_ctrlUserFunctionExceptionAddEdit_cboLocation_Input').val() == '') {
                    var m = '<%= Translate("Bạn chưa chọn cấp bậc") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
        }
        function popupclose(sender, args) {
            $find("<%= rgGrid.ClientId %>").get_masterTableView().rebind();
        }

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }
        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>

