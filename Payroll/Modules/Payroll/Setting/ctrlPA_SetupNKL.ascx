<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SetupNKL.ascx.vb"
    Inherits="Payroll.ctrlPA_SetupNKL" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Số ngày NKL từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnValueFrom" MinValue="0" ShowSpinButtons="True">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnValueFrom"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số ngày NKL từ. %>" ToolTip="<%$ Translate: Bạn phải nhập số ngày NKL từ. %>"></asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <%# Translate("Số ngày NKL đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnValueTo" MinValue="0" ShowSpinButtons="True">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnValueTo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số ngày NKL đến. %>" ToolTip="<%$ Translate: Bạn phải nhập số ngày NKL đến. %>"></asp:RequiredFieldValidator>
                </td>
                
                <td class="lb">
                    <%# Translate("Số tháng trừ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnMonth" MinValue="0">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnMonth"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số tháng trừ. %>" ToolTip="<%$ Translate: Bạn phải nhập Số tháng trừ. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgData" runat="server" AutoGenerateColumns="false"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <MasterTableView DataKeyNames="ID,VALUE_FROM,VALUE_TO,MONTH,ID" ClientDataKeyNames="VALUE_FROM,VALUE_TO,MONTH,ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày NKL từ %>" DataField="VALUE_FROM"
                        SortExpression="VALUE_FROM" AutoPostBackOnFilter="true" UniqueName="VALUE_FROM" DataFormatString="{0:N0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày NKL đến %>" DataField="VALUE_TO"
                        SortExpression="VALUE_TO" AutoPostBackOnFilter="true" UniqueName="VALUE_TO" DataFormatString="{0:N0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tháng trừ %>" DataField="MONTH"
                        SortExpression="MONTH" AutoPostBackOnFilter="true" UniqueName="MONTH" DataFormatString="{0:N0}">
                    </tlk:GridNumericColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" >
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
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
