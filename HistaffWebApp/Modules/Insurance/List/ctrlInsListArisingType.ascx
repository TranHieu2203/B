<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsListArisingType.ascx.vb"
    Inherits="Insurance.ctrlInsListArisingType" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="155px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server">
        </tlk:RadToolBar>
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Loại biến động")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboArisingType" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusArisingType" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại biến động. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn loại biến động. %>" ClientValidationFunction="cusArisingType">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên biến động")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên biến động %>" ToolTip="<%$ Translate: Bạn phải nhập tên biến động %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã biến động")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã biến động %>" ToolTip="<%$ Translate: Bạn phải nhập mã biến động %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại ký hiệu tăng giảm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSIGN_ARISING" runat="server">
                    </tlk:RadComboBox>
						<asp:RequiredFieldValidator ID="reqcboSIGN_ARISING" ControlToValidate="cboSIGN_ARISING"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Loại ký hiệu tăng giảm. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Loại ký hiệu tăng giảm. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chi tiết giảm")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSIGN_ARISING_DETAIL" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Hình thức khai báo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTypeDeclare" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboTypeDeclare"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn Hình thức khai báo. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Hình thức khai báo.%>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" runat="server" Width="100%" Height="40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgInsChangeType" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,arising_name,arising_type,NOTE,sign_arising,sign_arising_detail,CODE,TYPE_DECLARE_ID,TYPE_DECLARE_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="arising_name" HeaderText="<%$ Translate: Tên biến động %>"
                        UniqueName="arising_name">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã biến động %>" DataField="CODE"
                        SortExpression="CODE" UniqueName="CODE" />
                    <tlk:GridBoundColumn DataField="ARISING_NAME_OT" HeaderText="<%$ Translate: Loại biến động %>"
                        UniqueName="ARISING_NAME_OT">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SIGN_ARISING_NAME" HeaderText="<%$ Translate: Loại ký hiệu tăng giảm %>"
                        UniqueName="SIGN_ARISING_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SIGN_ARISING_DETAIL_NAME" HeaderText="<%$ Translate: Chi tiết giảm %>"
                        UniqueName="SIGN_ARISING_DETAIL_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức khai báo %>" DataField="TYPE_DECLARE_NAME"
                        SortExpression="TYPE_DECLARE_NAME" UniqueName="TYPE_DECLARE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" SortExpression="NOTE"
                        UniqueName="NOTE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlInsListArisingType_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsListArisingType_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsListArisingType_RadPane2';
        var validateID = 'MainContent_ctrlInsListArisingType_valSum';
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
                var rows = $find('<%= rgInsChangeType.ClientID %>').get_masterTableView().get_dataItems().length;
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgInsChangeType');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgInsChangeType.ClientID %>').get_masterTableView().get_selectedItems().length;
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

        function cusArisingType(oSrc, args) {
            var cbo = $find("<%# cboArisingType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
    </script>
</tlk:RadCodeBlock>
