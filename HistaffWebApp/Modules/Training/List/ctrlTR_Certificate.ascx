<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Certificate.ascx.vb"
    Inherits="Training.ctrlTR_Certificate" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="160px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Tên tiếng Việt")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên tiếng Việt %>" ToolTip="<%$ Translate: Bạn phải nhập tên tiếng Việt %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên tiếng anh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameEN" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm chứng chỉ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCertificateGroup" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCertificateGroup" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm chứng chỉ %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nhóm chứng chỉ %>" ClientValidationFunction="cusCertificateGroup">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thời hạn")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtDuration" runat="server" MinValue="0" MaxValue="10000">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Loại phương tiện")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTransportationType" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Hạng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCategory" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="100%"></tlk:RadTextBox>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="NAME_VN,NAME_EN,TR_CER_GROUP_ID,DURATION,TRANSPORTATION_TYPE,CATEGORY,REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng Việt %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng Anh %>" DataField="NAME_EN"
                        UniqueName="NAME_EN" SortExpression="NAME_EN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chứng chỉ %>" DataField="TR_CER_GROUP_NAME"
                        UniqueName="TR_CER_GROUP_NAME" SortExpression="TR_CER_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời hạn %>" DataField="DURATION"
                        UniqueName="DURATION" SortExpression="DURATION" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại phương tiện %>" DataField="TRANSPORTATION_TYPE"
                        UniqueName="TRANSPORTATION_TYPE" SortExpression="TRANSPORTATION_TYPE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hạn %>" DataField="CATEGORY_NAME"
                        UniqueName="CATEGORY_NAME" SortExpression="CATEGORY_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                        UniqueName="REMARK" SortExpression="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" ShowFilterIcon="true" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusCertificateGroup(oSrc, args) {
            var cbo = $find("<%# cboCertificateGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

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
