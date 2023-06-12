<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_CriteriaObjectGroup.ascx.vb"
    Inherits="Performance.ctrlPE_CriteriaObjectGroup" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane5" runat="server" Height="35px" Scrolling="None" Visible="true">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="40px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm đối tượng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjectGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane11" runat="server" Scrolling="None" Height="30px">
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6" style="width: 50%">
                    <%# Translate("Tiêu chí đánh giá chưa gán nhóm đối tượng")%>
                </td>
                <td style="min-width: 50px">
                </td>
                <td class="item-head" colspan="6" style="width: 50%">
                    <%# Translate("Tiêu chí đánh giá đã gán nhóm đối tượng")%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgCanNotSchedule" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="CRITERIA_ID,CRITERIA_CODE,CRITERIA_NAME">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="CRITERIA_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tiêu chí %>" DataField="CRITERIA_CODE"
                                UniqueName="CRITERIA_CODE" SortExpression="CRITERIA_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiêu chí %>" DataField="CRITERIA_NAME"
                                UniqueName="CRITERIA_NAME" SortExpression="CRITERIA_NAME" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="50px">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnDelete" runat="server" Font-Bold="true" Text="<--" OnClientClicking="btnDelete_Click">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnInsert" runat="server" Font-Bold="true" Text="-->" OnClientClicking="btnInsert_Click">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgCanSchedule" runat="server" Height="100%" AllowSorting="false"
                    AllowMultiRowEdit="true">
                    <MasterTableView DataKeyNames="ID,CRITERIA_ID,CRITERIA_CODE" EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="CRITERIA_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tiêu chí %>" DataField="CRITERIA_CODE"
                                UniqueName="CRITERIA_CODE" SortExpression="CRITERIA_CODE" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiêu chí %>" DataField="CRITERIA_NAME"
                                UniqueName="CRITERIA_NAME" SortExpression="CRITERIA_NAME" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chỉ tiêu %>" DataField="EXPENSE"
                                UniqueName="EXPENSE" SortExpression="EXPENSE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Trọng số %>" DataField="AMONG"
                                UniqueName="AMONG" SortExpression="AMONG" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="FROM_DATE"
                                UniqueName="FROM_DATE" SortExpression="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="TO_DATE"
                                UniqueName="TO_DATE" SortExpression="TO_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                UniqueName="ACTFLG" SortExpression="ACTFLG" />
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

        function btnInsert_Click(sender, args) {
            var cbo = $find("<%# cboObjectGroup.ClientID %>");
            if (cbo.get_value().length != 0) {
                var bCheck = $find('<%# rgCanNotSchedule.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
            else {
                var m = '<%# Translate("Bạn phải chọn Nhóm đối tượng") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

        function btnDelete_Click(sender, args) {
            var cbo = $find("<%# cboObjectGroup.ClientID %>");
            if (cbo.get_value().length != 0) {
                var bCheck = $find('<%# rgCanSchedule.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
            }
            else {
                var m = '<%# Translate("Bạn phải chọn Nhóm đối tượng") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                args.set_cancel(true);
            //            }
        }
    </script>
</tlk:RadCodeBlock>
