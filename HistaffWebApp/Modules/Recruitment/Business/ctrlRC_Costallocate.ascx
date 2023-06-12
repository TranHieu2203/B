<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_Costallocate.ascx.vb" Inherits="Recruitment.ctrlRC_Costallocate" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hdOrgID" runat="server" />
<asp:HiddenField ID="hdStageID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="260px" Scrolling="None">
                <fieldset>
                    <legend><%# Translate("Thông tin đợt tuyển dụng")%></legend>
                        <table class="table-form">
                    <tr>
                         <td class="lb">
                            <%# Translate("Đơn vị khai thác")%>:
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblOrganizationName" runat="server" Font-Bold="true">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên đợt tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="true">
                            </asp:Label>
                        </td>
                        <td>
                            <%# Translate("Năm tuyển dụng")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblYear" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày bắt đầu")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblStartDate" runat="server">
                            </asp:Label>
                        </td>
                        <td>
                            <%# Translate("Ngày kết thúc")%>:
                        </td>
                        <td>
                            <asp:Label ID="lblEndDate" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                </fieldset>
                <fieldset>
                    <legend><%# Translate("Thông tin phân bổ chi phí")%></legend>
                        <table  class="table-form">
                    <tr>
                         <td class="lb">
                            <%# Translate("Đơn vị khai thác")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="true" Width="250px">
                            </tlk:RadTextBox><span class="lbReq">*</span>
                            <asp:RequiredFieldValidator ID="rfvOrgName" ControlToValidate="txtOrgName" runat="server"
                                                            ErrorMessage="<%$ Translate: Bạn phải nhập Tên đợt tuyển dụng. %>"
                                                            ToolTip="<%$ Translate: Bạn phải nhập tên đợt tuyển dụng. %>"> </asp:RequiredFieldValidator>
                            <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindORG" runat="server"
                                SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Chi phí")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="ntbCostAmount" runat="server" SkinID="Money" >
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="rfvCostAmount" ControlToValidate="ntbCostAmount" runat="server"
                                                            ErrorMessage="<%$ Translate: Bạn phải nhập Tên đợt tuyển dụng. %>"
                                                            ToolTip="<%$ Translate: Bạn phải nhập tên đợt tuyển dụng. %>"> </asp:RequiredFieldValidator>
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
                </fieldset>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true" SkinID="GridSingleSelect">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,ORG_ID,ORG_NAME,COSTAMOUNT,REMARK">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="ORG_ID" UniqueName="ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị được phân bố chi phí %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí %>" DataField="COSTAMOUNT"
                                SortExpression="COSTAMOUNT" UniqueName="COSTAMOUNT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
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