<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Ward.ascx.vb"
    Inherits="Profile.ctrlHU_Ward" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="160px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server">
        </tlk:RadToolBar>
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Quốc gia")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboNation" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusNation" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Quốc gia %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Quốc gia  %>" ClientValidationFunction="cusNation">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalNation" ControlToValidate="cboNation" runat="server" ErrorMessage="<%$ Translate: Quốc gia không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Quốc gia không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tỉnh thành")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProvince" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusProvince" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Tỉnh thành %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Tỉnh thành  %>" ClientValidationFunction="cusProvince">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalProvince" ControlToValidate="cboProvince" runat="server" ErrorMessage="<%$ Translate: Tỉnh thành không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Tỉnh thành không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Quân huyện")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDistrict" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusDistrict" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Quận huyện %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Quận huyện  %>" ClientValidationFunction="cusDistrict">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalDistrict" ControlToValidate="cboDistrict" runat="server" ErrorMessage="<%$ Translate: Quận huyện không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate:  Quận huyện không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã xã phường")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã xã phường %>" ToolTip="<%$ Translate: Bạn phải nhập Mã xã phường %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã xã phường đã tồn tại %>"
                        ToolTip="<%$ Translate: Mã xã phường đã tồn tại %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên xã phường")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtVNName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqVNName" ControlToValidate="txtVNName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên xã phường %>" ToolTip="<%$ Translate: Bạn phải nhập tên xã phường %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgMain" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,NATION_ID,NATION_NAME,PROVINCE_ID,PROVINCE_NAME,DISTRICT_ID,DISTRICT_NAME,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="<%$ Translate: ID %>" UniqueName="ID"
                        Visible="false" />
                    <tlk:GridBoundColumn DataField="CODE" HeaderText="<%$ Translate: Mã xã phường %>"
                        UniqueName="CODE" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME_VN" HeaderText="<%$ Translate: Tên xã phường %>"
                        UniqueName="NAME_VN" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="DISTRICT_NAME" UniqueName="DISTRICT_NAME" HeaderText="<%$ Translate: Quận huyện %>">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PROVINCE_NAME" UniqueName="PROVINCE_NAME" HeaderText="<%$ Translate: Tỉnh thành %>">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NATION_NAME" UniqueName="NATION_NAME" HeaderText="<%$ Translate: Quốc gia %>">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ACTFLG" UniqueName="ACTFLG" HeaderText="<%$ Translate: Trạng thái %>">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NOTE" UniqueName="NOTE" HeaderText="<%$ Translate: Ghi chú %>">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function cusNation(oSrc, args) {
            var cbo = $find("<%# cboNation.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusProvince(oSrc, args) {
            var cbo = $find("<%# cboProvince.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusDistrict(oSrc, args) {
            var cbo = $find("<%# cboDistrict.ClientID %>");
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
                var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane2.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane1.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
