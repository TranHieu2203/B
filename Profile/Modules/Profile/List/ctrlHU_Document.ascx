<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Document.ascx.vb"
    Inherits="Profile.ctrlHU_Document" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidTITLE_GROUP_ID" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleGroup" Text="Mã tài liệu"></asp:Label><span
                        class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCode">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="Bạn phải nhập mã tài liệu" ToolTip="Bạn phải nhập mã tài liệu">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbOrgLevel" Text="Tên tài liệu"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtName">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkAllowUpload" Text="Cho phép upload file" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Dạng tài liệu"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTypeDocument">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkOriginal" Text="Là tài liệu bắt buộc" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,TYPE_DOCUMENT_ID,TYPE_DOCUMENT_NAME,MUST_HAVE,ALLOW_UPLOAD_FILE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã tài liệu" DataField="CODE" HeaderStyle-Width="100px"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="Tên tài liệu" DataField="NAME_VN" HeaderStyle-Width="200px"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="Dạng tài liệu" DataField="TYPE_DOCUMENT_NAME" HeaderStyle-Width="200px"
                        UniqueName="TYPE_DOCUMENT_NAME" SortExpression="TYPE_DOCUMENT_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="Là tài liệu bắt buộc" DataField="MUST_HAVE" UniqueName="MUST_HAVE"
                        SortExpression="MUST_HAVE" AllowFiltering="false" HeaderStyle-Width="50px" />
                    <tlk:GridCheckBoxColumn HeaderText="Cho phép upload file" DataField="ALLOW_UPLOAD_FILE" UniqueName="ALLOW_UPLOAD_FILE"
                        SortExpression="ALLOW_UPLOAD_FILE" AllowFiltering="false" HeaderStyle-Width="50px" />
                    <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="ACTFLG" UniqueName="ACTFLG"
                        SortExpression="ACTFLG" HeaderStyle-Width="100px" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_TypeDocument_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_TypeDocument_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_TypeDocument_RadPane2';
        var validateID = 'MainContent_ctrlHU_TypeDocument_valSum';
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
                    var l = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var k = noty({ text: l, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(k.options.id); }, 5000);
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



function rbtClicked(sender, eventArgs) {
    enableAjax = false;
}
    </script>
</tlk:RadCodeBlock>
