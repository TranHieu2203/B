<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlQuyDinhCa.ascx.vb"
    Inherits="Attendance.ctrlQuyDinhCa" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width = 200px">
                    <%# Translate("Mã quy định ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rdTxtCode" runat="server">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="rdTxtCode" runat="server" ErrorMessage="<%$ Translate: Mã quy định ca đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã quy định ca đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdTxtCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn nhập mã quy định ca. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width = 200px">
                    <%# Translate("Tên quy định ca")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rdTxtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdTxtNameVN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn nhập tên quy định ca. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Sớm nhất được vào")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtSoonest_in" MinValue="0" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Phút)")%>
                </td>
                <td class="lb">
                    <%# Translate("Muộn nhất phải ra")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTxtLatest_Out" MinValue="0" runat="server">
                        <NumberFormat AllowRounding="true" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <%# Translate("(Phút)")%>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,NAME_EN,SOONEST_IN,LATEST_OUT,ACTFLG">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã quy định ca %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên quy định ca %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Sớm nhất được vào %>" DataField="SOONEST_IN"
                        UniqueName="SOONEST_IN" SortExpression="SOONEST_IN" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Muộn nhất phải ra %>" DataField="LATEST_OUT"
                        UniqueName="LATEST_OUT" SortExpression="LATEST_OUT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                    </tlk:GridBoundColumn>
                </Columns>
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
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
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
