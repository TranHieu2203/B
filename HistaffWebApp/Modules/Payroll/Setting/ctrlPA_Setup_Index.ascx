<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Setup_Index.ascx.vb"
    Inherits="Payroll.ctrlPA_Setup_Index" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="250px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
             <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbBrand" Text="Nhãn hàng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboBrand" runat="server" SkinID="dDropdownList" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqBrand" ControlToValidate="cboBrand" runat="server"
                        ErrorMessage="Bạn phải chọn Nhãn hàng" ToolTip="Bạn phải chọn Nhãn hàng">
                    </asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbFromCompletionRate" Text="TLHT(%) Từ"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="numFromCompletionRate" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqFromCompletionRate" ControlToValidate="numFromCompletionRate" runat="server"
                        ErrorMessage="Bạn phải chọn TLHT(%) Từ" ToolTip="Bạn phải chọn TLHT(%) Từ">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải chọn Ngày hiệu lực" ToolTip="Bạn phải chọn Ngày hiệu lực">
                    </asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbToCompletionRate" Text="TLHT(%) Đến"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="numToCompletionRate" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqToCompletionRate" ControlToValidate="numToCompletionRate" runat="server"
                        ErrorMessage="Bạn phải chọn TLHT(%) Đến" ToolTip="Bạn phải chọn TLHT(%) Đến">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compareToCompletionRate" runat="server" ControlToValidate="numToCompletionRate"
                        Type="Double" ControlToCompare="numFromCompletionRate" Operator="GreaterThanEqual" ErrorMessage="TLDT đến phải lớn hơn TLHT(%) Đến"
                        ToolTip="TLDT đến phải lớn hơn TLHT(%) Đến">
                    </asp:CompareValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbIndexType" Text="Tỷ lệ khung"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboIndexType" runat="server" SkinID="dDropdownList" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqIndexType" ControlToValidate="cboIndexType" runat="server"
                        ErrorMessage="Bạn phải chọn Tỷ lệ khung" ToolTip="Bạn phải chọn Tỷ lệ khung">
                    </asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbFactor" Text="Tỉ lệ(%) đạt"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="numFactor" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqFactor" ControlToValidate="numFactor" runat="server"
                        ErrorMessage="Bạn phải nhập Tỉ lệ(%) đạt" ToolTip="Bạn phải nhập Tỉ lệ(%) đạt">
                    </asp:RequiredFieldValidator>
                </td>
                 <td colspan="2">
                     <asp:CheckBox ID="chkGetTLHT" CausesValidation="false" runat="server" Text="Lấy theo Tỉ lê hoàn thành" ToolTip="Lấy theo Tỉ lê hoàn thành"/>
                 </td>
             </tr>
             <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbNote" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Height="70" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="regexNote" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                        ControlToValidate="txtNote" ValidationExpression="^(?!.*<[^>]+>).*">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">          
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,BRAND,EFFECT_DATE,INDEX_TYPE,FROM_COMPLETION_RATE,TO_COMPLETION_RATE,FACTOR,NOTE,IS_GET_TLHT">
                <Columns>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false"> 
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Setup_Index_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Setup_Index_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Setup_Index_RadPane2';
        var validateID = 'MainContent_ctrlPA_Setup_Index_valSum';
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
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
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
