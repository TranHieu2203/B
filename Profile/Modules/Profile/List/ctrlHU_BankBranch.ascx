<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_BankBranch.ascx.vb"
    Inherits="Profile.ctrlHU_BankBranch" %>
    <%@ Import Namespace = "Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server" Height="125px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Tên ngân hàng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboBank" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqBank" ControlToValidate="cboBank" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn tên ngân hàng %>" ToolTip="<%$ Translate:  Bạn phải chọn tên ngân hàng %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusBank" runat="server" ErrorMessage="<%$ Translate: Tên ngân hàng không tồn tại hoặc đã ngừng áp dụng %>"
                        ToolTip="<%$ Translate: Tên ngân hàng không tồn tại hoặc đã ngừng áp dụng%>" ControlToValidate="cboBank" ></asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên chi nhánh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên chi nhánh %>" ToolTip="<%$ Translate: Bạn phải nhập tên chi nhánh %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="display:none">
                <td class="lb">
                    <%# Translate("Mã chi nhánh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="ReadOnly" runat="server">
                    </tlk:RadTextBox>
                    <%--<asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã chi nhánh %>" ToolTip="<%$ Translate:  Bạn phải nhập mã chi nhánh %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" runat="server" ErrorMessage="<%$ Translate: Mã chi nhánh đã tồn tại %>"
                        ToolTip="<%$ Translate: Mã chi nhánh đã tồn tại%>"></asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>--%>
                </td>
              
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%" Height="38px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgBankBranchs" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,BANK_ID,REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="<%$ Translate: ID %>" UniqueName="ID"
                        Visible="false" />
                    <%--<tlk:GridBoundColumn DataField="CODE" HeaderText="<%$ Translate: Mã chi nhánh %>"
                        UniqueName="CODE">--%>
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="NAME" HeaderText="<%$ Translate: Tên chi nhánh %>"
                        UniqueName="NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="BANK_NAME" HeaderText="<%$ Translate: Tên ngân hàng %>"
                        UniqueName="BANK_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="REMARK" HeaderText="<%$ Translate: Ghi chú %>" UniqueName="REMARK">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ACTFLG" HeaderText="<%$ Translate: Trạng thái %>">
                    </tlk:GridBoundColumn>
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

    var splitterID = 'ctl00_MainContent_ctrlHU_BankBranch_RadSplitter3';
    var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_BankBranch_RadPane2';
    var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_BankBranch_RadPane1';
    var validateID = 'MainContent_ctrlHU_BankBranch_valSum';
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
            var rows = $find('<%= rgBankBranchs.ClientID %>').get_masterTableView().get_dataItems().length;
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
                ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgBankBranchs');
            else
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
        } else if (args.get_item().get_commandName() == "EDIT") {
            ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            var bCheck = $find('<%= rgBankBranchs.ClientID %>').get_masterTableView().get_selectedItems().length;
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


    function cusBank(oSrc, args) {
        var cbo = $find("<%# cboBank.ClientID %>");
        args.IsValid = (cbo.get_value().length != 0);
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }
</script>
</tlk:RadCodeBlock>
