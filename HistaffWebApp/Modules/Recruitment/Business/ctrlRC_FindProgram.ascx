<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_FindProgram.ascx.vb"
    Inherits="Recruitment.ctrlRC_FindProgram" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Height="33px" Scrolling="None">
        <table class="table-form">
            <tr>
                <td>
                    <b>
                        <%# Translate("Lưu ý: Khi chuyển sang vị trí tuyển dụng khác chỉ chuyển thông tin của ứng viên sang")%></b>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,STATUS_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày gửi yêu cầu %>" DataField="SEND_DATE"
                        SortExpression="SEND_DATE" UniqueName="SEND_DATE" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tuyển dụng %>" DataField="CODE"
                        SortExpression="CODE" UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                        UniqueName="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí tuyển dụng %>" DataField="TITLE_NAME"
                        SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Trong ngân sách? %>" DataField="IS_IN_PLAN"
                        UniqueName="IS_IN_PLAN" SortExpression="IS_IN_PLAN" HeaderStyle-Width="70px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu tuyển dụng %>" DataField="RECRUIT_START"
                        SortExpression="RECRUIT_START" UniqueName="RECRUIT_START" HeaderStyle-Width="120px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc nhận hồ sơ %>" DataField="RECEIVE_END"
                        SortExpression="RECEIVE_END" UniqueName="RECEIVE_END" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do tuyển dụng %>" DataField="RECRUIT_REASON_NAME"
                        SortExpression="RECRUIT_REASON_NAME" UniqueName="RECRUIT_REASON_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do tuyển dụng chi tiết %>" DataField="RECRUIT_REASON"
                        SortExpression="RECRUIT_REASON" UniqueName="RECRUIT_REASON" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng trong kế hoạch %>" DataField="CANDIDATE_REQUEST"
                        SortExpression="CANDIDATE_REQUEST" UniqueName="CANDIDATE_REQUEST" AllowFiltering="false"
                        HeaderStyle-Width="90px" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Hồ sơ đã nhận %>" DataField="CANDIDATE_COUNT"
                        SortExpression="CANDIDATE_COUNT" UniqueName="CANDIDATE_COUNT" AllowFiltering="false"
                        HeaderStyle-Width="90px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                        SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                    <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                        SortExpression="ORG_DESC" Visible="false" />
                </Columns>
                <HeaderStyle Width="150px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

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


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;

            if (args.get_item().get_commandName() == "SAVE") {
                var bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                if (bCheck > 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return;
                }
                var id = $find('<%# rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                getRadWindow().close('TRANSFER;' + id);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

    </script>
</tlk:RadCodeBlock>
