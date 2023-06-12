<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Cost.ascx.vb"
    Inherits="Recruitment.ctrlRC_Cost" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="250px" Scrolling="None">
                <table  class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Chương trình tuyển dụng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboProgramName" runat="server">
                            </tlk:RadComboBox>
                            <asp:CustomValidator ID="cusProgramName" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Vị trí tuyển dụng %>"
                                ToolTip="<%$ Translate: Bạn phải chọn Vị trí tuyển dụng %>" ClientValidationFunction="cusProgramName">
                            </asp:CustomValidator>
                            <asp:CustomValidator ID="cvalCode" ControlToValidate="cboProgramName" runat="server"
                                ErrorMessage="<%$ Translate: Chương trình tuyển dụng đã tồn tại %>" ToolTip="<%$ Translate: Chương trình tuyển dụng đã tồn tại %>">
                            </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Chi phí dự tính")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCostExpected" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtCostExpected"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Chi phí dự tính %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Chi phí dự tính %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Chi phí thực tế")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCostActual" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtCostActual"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Chi phí thực tế %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Chi phí thực tế %>"> 
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả chi phí thực tế")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtCostDescription" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Hình thức")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadListBox ID="lstbForm" CheckBoxes="true" runat="server" Height="80px" Width="100%"
                                OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                            </tlk:RadListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" SkinID="GridSingleSelect">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,COST_EXPECTED,RC_PROGRAM_ID,RC_FORM_NAMES,RC_FORM_IDS,COST_ACTUAL,COST_DESCRIPTION,REMARK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chương trình tuyển dụng %>" DataField="RC_PROGRAM_NAME"
                                SortExpression="RC_PROGRAM_NAME" UniqueName="RC_PROGRAM_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí dự tính %>" DataField="COST_EXPECTED"
                                SortExpression="COST_EXPECTED" UniqueName="COST_EXPECTED" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí thực tế %>" DataField="COST_ACTUAL"
                                SortExpression="COST_ACTUAL" UniqueName="COST_ACTUAL" DataFormatString="{0:n0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả chi phí thực tế %>" DataField="COST_DESCRIPTION"
                                SortExpression="COST_DESCRIPTION" UniqueName="COST_DESCRIPTION" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức %>" DataField="RC_FORM_NAMES"
                                SortExpression="RC_FORM_NAMES" UniqueName="RC_FORM_NAMES" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                                UniqueName="REMARK" />
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


        function cusProgramName(oSrc, args) {
            var cbo = $find("<%# cboProgramName.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }


        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
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
