<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_SettingAssessmentForm.ascx.vb"
    Inherits="Training.ctrlTR_SettingAssessmentForm" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="LeftPane" runat="server" Height="50px">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mẫu biểu đánh giá")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboAssForm" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgCriGroupFormNotForm" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="TR_CRITERIA_GROUP_ID" Caption="<%$ Translate: Nhóm tiêu chí không thuộc mẫu biểu đánh giá %>">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhóm tiêu chí %>" DataField="TR_CRITERIA_GROUP_CODE"
                                UniqueName="TR_CRITERIA_GROUP_CODE" SortExpression="TR_CRITERIA_GROUP_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhóm tiêu chí %>" DataField="TR_CRITERIA_GROUP_NAME"
                                UniqueName="TR_CRITERIA_GROUP_NAME" SortExpression="TR_CRITERIA_GROUP_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                UniqueName="REMARK" SortExpression="REMARK" />
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Width="60px">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnDelete" runat="server" Font-Bold="true" Text="<--" Width="45px"
                                OnClientClicking="btnDelete_Click" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <tlk:RadButton ID="btnInsert" runat="server" Font-Bold="true" Text="-->" Width="45px"
                                OnClientClicking="btnInsert_Click">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgCriGroupForm" runat="server" Height="100%" AllowSorting="false">
                    <MasterTableView DataKeyNames="ID" Caption="<%$ Translate: Nhóm tiêu chí thuộc mẫu biểu đánh giá %>">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhóm tiêu chí %>" DataField="TR_CRITERIA_GROUP_CODE"
                                UniqueName="TR_CRITERIA_GROUP_CODE" SortExpression="TR_CRITERIA_GROUP_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhóm tiêu chí %>" DataField="TR_CRITERIA_GROUP_NAME"
                                UniqueName="TR_CRITERIA_GROUP_NAME" SortExpression="TR_CRITERIA_GROUP_NAME" />
                                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                UniqueName="REMARK" SortExpression="REMARK" />

                        </Columns>
                    </MasterTableView>
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function btnInsert_Click(sender, args) {
            var bCheck = $find('<%# rgCriGroupFormNotForm.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }

        }

        function btnDelete_Click(sender, args) {
            var bCheck = $find('<%# rgCriGroupForm.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
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


        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'PRINT_STUDENT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'PRINT_COMPLETE') {
                enableAjax = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
