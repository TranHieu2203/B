<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlThietLapKieuCong.ascx.vb"
    Inherits="Attendance.ctrlThietLapKieuCong" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="250px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã kiểu công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="RadComboBox2" SkinID="Textbox15" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên tiếng việt")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="NAME_VN" SkinID="Textbox15" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên tiếng anh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="NAME_EN" SkinID="Textbox15" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kiểu công buổi sáng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="RadComboBox1" SkinID="Textbox15" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kiểu công buổi chiều")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="RadComboBox3" SkinID="Textbox15" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td>
                    <tlk:RadTextBox TextMode="MultiLine" Height="50px" Width="300px" ID="rdNote" MaxLength="255"
                        runat="server">
                    </tlk:RadTextBox>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="MA_KIEUCONG,NAME_VN,NAME_EN,CONG_BUOISANG,CONG_BUOICHIEU,GHICHU">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã kiểu công %>" DataField="MA_KIEUCONG"
                        UniqueName="MA_KIEUCONG" SortExpression="MA_KIEUCONG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng việt %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng anh %>" DataField="NAME_EN"
                        UniqueName="NAME_EN" SortExpression="NAME_EN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công buổi sáng%>" DataField="CONG_BUOISANG"
                        UniqueName="CONG_BUOISANG" SortExpression="CONG_BUOISANG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu công buổi chiều%>" DataField="CONG_BUOICHIEU"
                        UniqueName="CONG_BUOICHIEU" SortExpression="CONG_BUOICHIEU" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS"
                        UniqueName="STATUS" SortExpression="STATUS">
                        <HeaderStyle Width="70px" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="GHICHU" UniqueName="GHICHU"
                        SortExpression="GHICHU" />
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
