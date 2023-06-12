<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_CommitAfterTrain.ascx.vb"
    Inherits="Training.ctrlTR_CommitAfterTrain" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Chi phí đào tạo từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostTrainFrom" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rntxtCostTrainTo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Chi phí đào tạo từ %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Chi phí đào tạo từ %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí đào tạo đến")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostTrainTo" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị tiền tệ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCurrency">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboCurrency"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Đơn vị tiền tệ %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Đơn vị tiền tệ %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian làm việc sau đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCommitWork" runat="server" SkinID="Number" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rntxtCommitWork"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian làm việc sau đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian làm việc sau đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <%# Translate("năm")%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="COST_TRAIN_FROM,COST_TRAIN_TO,TR_CURRENCY_ID,TR_CURRENCY_NAME,COMMIT_WORK,ACTFLG">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí đào tạo từ %>" DataField="COST_TRAIN_FROM"
                        UniqueName="COST_TRAIN_FROM" SortExpression="COST_TRAIN_FROM" DataFormatString="{0:n0}" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí đào tạo đến %>" DataField="COST_TRAIN_TO"
                        UniqueName="COST_TRAIN_TO" SortExpression="COST_TRAIN_TO" DataFormatString="{0:n0}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị tiền tệ %>" DataField="TR_CURRENCY_NAME"
                        UniqueName="TR_CURRENCY_NAME" SortExpression="TR_CURRENCY_NAME" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian làm việc sau đào tạo %>"
                        DataField="COMMIT_WORK" UniqueName="COMMIT_WORK" SortExpression="COMMIT_WORK"
                        HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" ShowFilterIcon="true" />
                </Columns>
                <ItemStyle Width="100px" />
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
