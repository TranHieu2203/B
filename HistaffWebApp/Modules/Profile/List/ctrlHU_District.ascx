<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_District.ascx.vb"
    Inherits="Profile.ctrlHU_District" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<%@ Import Namespace = "Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="105px" Scrolling="None">
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
                    <tlk:RadComboBox ID="cboProvince" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusProvince" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Tỉnh thành %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Tỉnh thành  %>" ClientValidationFunction="cusProvince">
                    </asp:CustomValidator>
                     <asp:CustomValidator ID="cvalProvince" ControlToValidate="cboProvince" runat="server" ErrorMessage="<%$ Translate: Tỉnh thành không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Tỉnh thành không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã quận huyện")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mã quận huyện %>" ToolTip="<%$ Translate: Bạn phải nhập mã quận huyện %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã quận huyện đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã quận huyện đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên quận huyện")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtVNName" SkinID="Textbox255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqVNName" ControlToValidate="txtVNName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên quận huyện %>" ToolTip="<%$ Translate: Bạn phải nhập tên quận huyện %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgMain" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,PROVINCE_ID,PROVINCE_NAME,NATION_ID,NATION_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="<%$ Translate: ID %>" UniqueName="ID"
                        Visible="false" />
                    <tlk:GridBoundColumn DataField="CODE" HeaderText="<%$ Translate: Mã quận huyện %>"
                        UniqueName="CODE" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME_VN" HeaderText="<%$ Translate: Tên quận huyện %>"
                        UniqueName="NAME_VN" Visible="true">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PROVINCE_NAME" HeaderText="<%$ Translate: Tỉnh thành %>">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NATION_NAME" HeaderText="<%$ Translate: Quốc gia %>">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ACTFLG" HeaderText="<%$ Translate: Trạng thái %>">
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <HeaderStyle HorizontalAlign="Center" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_Place_District_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Place_District_RadPane2';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Place_District_RadPane1';
        var validateID = 'MainContent_ctrlHU_Place_District_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }


        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function cusNation(oSrc, args) {
            var cbo = $find("<%# cboNation.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusProvince(oSrc, args) {
            var cbo = $find("<%# cboProvince.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
    </script>
</tlk:RadCodeBlock>
