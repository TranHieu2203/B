<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Setup_HSTDT.ascx.vb"
    Inherits="Payroll.ctrlPA_Setup_HSTDT" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="250px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
             <tr>
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
                    <asp:Label runat="server" ID="lbBrand" Text="Nhãn hàng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboBrand" runat="server" SkinID="dDropdownList" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqBrand" ControlToValidate="cboBrand" runat="server"
                        ErrorMessage="Bạn phải chọn Nhãn hàng" ToolTip="Bạn phải chọn Nhãn hàng">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbFromRate" Text="TLHT KPI/ DT từ"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="numFromRate" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqFromRate" ControlToValidate="numFromRate" runat="server"
                        ErrorMessage="Bạn phải nhập TLHT KPI/ DT từ" ToolTip="Bạn phải nhập TLHT KPI/ DT từ">
                    </asp:RequiredFieldValidator>
                </td>
             </tr>
             <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbStaffGroup" Text="Nhóm nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStaffGroup" runat="server" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqStaffGroup" ControlToValidate="cboStaffGroup" runat="server"
                        ErrorMessage="Bạn phải chọn Nhóm nhân viên" ToolTip="Bạn phải chọn Nhóm nhân viên">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbToRate" Text="TLHT KPI/ DT Đến"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="numToRate" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqToRate" ControlToValidate="numToRate" runat="server"
                        ErrorMessage="Bạn phải nhập TLHT KPI/ DT đến" ToolTip="Bạn phải nhập TLHT KPI/ DT đến">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compareToRate" runat="server" ControlToValidate="numToRate"
                        Type="Double" ControlToCompare="numFromRate" Operator="GreaterThanEqual" ErrorMessage="TLHT KPI/ DT đến phải lớn hơn TLHT KPI/ DT từ"
                        ToolTip="TLHT KPI/ DT đến phải lớn hơn TLHT KPI/ DT từ">
                    </asp:CompareValidator>
                </td>
             </tr>
             <tr>
                <td class="lb" style="display:none">
                    <asp:Label runat="server" ID="lbModelShop" Text="Mô hình cửa hàng"></asp:Label>
                </td>
                <td style="display:none">
                    <tlk:RadComboBox ID="cboModelShop" runat="server">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="reqModelShop" ControlToValidate="cboModelShop" runat="server"
                        ErrorMessage="Bạn phải chọn Mô hình cửa hàng" ToolTip="Bạn phải chọn Mô hình cửa hàng">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbHSTDT" Text="HSTDT"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="numHSTDT" runat="server" MinValue="0" MaxValue="100" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqHSTDT" ControlToValidate="numHSTDT" runat="server"
                        ErrorMessage="Bạn phải nhập HSTDT" ToolTip="Bạn phải nhập HSTDT">
                    </asp:RequiredFieldValidator>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,BRAND,EFFECT_DATE,STAFF_GROUP,FROM_RATE,TO_RATE,MODELSHOP,COEFFICIENT,NOTE">
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
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Setup_HSTDT_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Setup_HSTDT_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Setup_HSTDT_RadPane2';
        var validateID = 'MainContent_ctrlPA_Setup_HSTDT_valSum';
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
            if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
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
