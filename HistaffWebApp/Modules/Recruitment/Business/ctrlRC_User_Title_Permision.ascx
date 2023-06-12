<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_User_Title_Permision.ascx.vb"
    Inherits="Recruitment.ctrlRC_User_Title_Permision" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidProgramID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tài khoản")%>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboUser" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqTrainField" ControlToValidate="cboUser"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tài khoản %>"
                                ToolTip="<%$ Translate: Bạn phải chọn tài khoản %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Nhóm chức danh")%>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboGroupTitle" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Localization-CheckAllString="All">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboGroupTitle"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm chức danh %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Nhóm chức danh %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                   
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" >
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,USER_ID,GROUP_TITLE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMP_CODE"
                                SortExpression="EMP_CODE" UniqueName="EMP_CODE"  ItemStyle-HorizontalAlign="Center" >
                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMP_NAME"
                                SortExpression="EMP_NAME" UniqueName="EMP_NAME"  ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME"  ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITTLE_NAME"
                                SortExpression="TITTLE_NAME" UniqueName="TITTLE_NAME"  ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tài khoản %>" DataField="USER_NAME"
                                SortExpression="USER_NAME" UniqueName="USER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chức danh %>" DataField="GROUP_TITLE_NAME"
                                SortExpression="GROUP_TITLE_NAME" UniqueName="GROUP_TITLE_NAME"  ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
        }

    </script>
</tlk:RadCodeBlock>
