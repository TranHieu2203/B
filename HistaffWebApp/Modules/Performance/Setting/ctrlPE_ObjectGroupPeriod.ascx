<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_ObjectGroupPeriod.ascx.vb"
    Inherits="Performance.ctrlPE_ObjectGroupPeriod" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane5" runat="server" Height="35px" Scrolling="None" Visible="false">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server" Height="40px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Kỳ đánh giá")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPeriod" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane11" runat="server" Scrolling="None" Height="30px">
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6" style="width: 50%">
                    <%# Translate("Nhóm đối tượng chưa gán kỳ đánh giá")%>
                </td>
                <td style="min-width: 50px">
                </td>
                <td class="item-head" colspan="6" style="width: 50%">
                    <%# Translate("Nhóm đối tượng đã gán kỳ đánh giá")%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgCanNotSchedule" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="OBJECT_GROUP_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhóm đối tượng %>" DataField="OBJECT_GROUP_CODE"
                                UniqueName="OBJECT_GROUP_CODE" SortExpression="OBJECT_GROUP_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhóm đối tượng %>" DataField="OBJECT_GROUP_NAME"
                                UniqueName="OBJECT_GROUP_NAME" SortExpression="OBJECT_GROUP_NAME" />
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
                <tlk:RadGrid PageSize="50" ID="rgCanSchedule" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhóm đối tượng %>" DataField="OBJECT_GROUP_CODE"
                                UniqueName="OBJECT_GROUP_CODE" SortExpression="OBJECT_GROUP_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhóm đối tượng %>" DataField="OBJECT_GROUP_NAME"
                                UniqueName="OBJECT_GROUP_NAME" SortExpression="OBJECT_GROUP_NAME" />
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
            var cbo = $find("<%# cboPeriod.ClientID %>");
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
                var m = '<%# Translate("Bạn phải chọn Kỳ đánh giá") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

        function btnDelete_Click(sender, args) {
            var cbo = $find("<%# cboPeriod.ClientID %>");
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
                var m = '<%# Translate("Bạn phải chọn Kỳ đánh giá") %>';
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
            if (args.get_item().get_commandName() == 'CANCEL') {
                args.set_cancel(true);
            }
        }
    </script>
</tlk:RadCodeBlock>
