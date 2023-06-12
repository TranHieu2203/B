<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramExams.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramExams" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidProgramID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Height="220px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Phòng ban")%>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblOrgName" runat="server" Font-Bold="true">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Vị trí tuyển dụng")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblTitleName" runat="server" Font-Bold="true">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="item-head" colspan="4">
                            <%# Translate("Thiết lập môn thi")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Vòng thi")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtName" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Phỏng vấn")%><span class="lbReq"></span>
                        </td>
                        <td colspan="2">
                            <tlk:RadButton ButtonType="ToggleButton" ToggleType="CheckBox" runat="server" CausesValidation="false"
                                ID="chkIsPV" >
                            </tlk:RadButton>
                        </td>
                        
                        
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Thang điểm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtPointLadder" runat="server" SkinID="number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Điểm đạt")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtPointPass" runat="server" SkinID="number">
                            </tlk:RadNumericTextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Hệ số")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtCoefficient" runat="server" SkinID="number">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Thứ tự sắp xếp")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtExamsOrder" runat="server" SkinID="number" ReadOnly="true">
                            </tlk:RadNumericTextBox>
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
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true"
                    SkinID="GridSingleSelect">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,COUNT_SCAN" ClientDataKeyNames="ID,NAME,POINT_LADDER,POINT_PASS,EXAMS_ORDER,IS_PV,COEFFICIENT,REMARK,COUNT_SCAN">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                             <tlk:GridBoundColumn DataField="COUNT_SCAN" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên môn thi %>" DataField="NAME"
                                SortExpression="NAME" UniqueName="NAME" HeaderStyle-Width="300px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thang điểm %>" DataField="POINT_LADDER"
                                SortExpression="POINT_LADDER" UniqueName="POINT_LADDER" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Điểm đạt %>" DataField="POINT_PASS"
                                SortExpression="POINT_PASS" UniqueName="POINT_PASS" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số %>" DataField="COEFFICIENT"
                                SortExpression="COEFFICIENT" UniqueName="COEFFICIENT" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Thư tự sắp xếp %>" DataField="EXAMS_ORDER"
                                SortExpression="EXAMS_ORDER" UniqueName="EXAMS_ORDER" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Phỏng vấn %>" DataField="IS_PV"
                                UniqueName="IS_PV" SortExpression="IS_PV" ShowFilterIcon="true" AllowFiltering="false" />
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
