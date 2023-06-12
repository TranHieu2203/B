<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Setup_Rate_DTTT.ascx.vb"
    Inherits="Payroll.ctrlPA_Setup_Rate_DTTT" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="250px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="Bạn phải chọn Ngày hiệu lực" ToolTip="Bạn phải chọn Ngày hiệu lực">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbBranch" Text="Nhãn hàng"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboBranch" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqBranch" ControlToValidate="cboBranch" runat="server"
                        ErrorMessage="Bạn phải chọn Nhãn hàng" ToolTip="Bạn phải chọn Nhãn hàng">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbShopType" Text="Loại cửa hàng"></asp:Label><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbShopType" runat="server">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="reqShopType" ControlToValidate="cbShopType" runat="server"
                        ErrorMessage="Bạn phải chọn Loại cửa hàng" ToolTip="Bạn phải chọn Loại cửa hàng">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbStoreCode" Text="Cửa hàng"></asp:Label><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <asp:HiddenField ID="hidOrgID" runat="server" />
                    <tlk:RadTextBox ID="txtStoreCode" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <%--<asp:RequiredFieldValidator ID="reqStoreCode" ControlToValidate="txtStoreCode" runat="server"
                        ErrorMessage="Bạn phải chọn Cửa hàng" ToolTip="Bạn phải chọn Cửa hàng">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRate" Text="Tỷ lệ"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnRate" runat="server" MinValue="0" MaxValue="100" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqRate" ControlToValidate="rnRate" runat="server"
                        ErrorMessage="Bạn phải chọn Tỷ lệ" ToolTip="Bạn phải chọn Tỷ lệ">
                    </asp:RequiredFieldValidator>
                </td>
                <td></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDoanhThu" Text="Mức hưởng theo doanh thu" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbNote" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Height="70" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                        ControlToValidate="txtNote" ValidationExpression="^(?!.*<[^>]+>).*">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,BRANCH,SHOP_TYPE,STORE_CODE_NAME,NOTE,RATE,EFFECT_DATE,STORE_CODE,IS_DOANHTHU">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridDateTimeColumn DataField="EFFECT_DATE" ReadOnly="true" HeaderText="Ngày hiệu lực"
                        SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                    <tlk:GridBoundColumn HeaderText="Nhãn hàng" DataField="BRANCH_NAME" SortExpression="BRANCH_NAME"
                        UniqueName="BRANCH_NAME" />
                    <tlk:GridBoundColumn HeaderText="Loại cửa hàng" DataField="SHOP_TYPE_NAME" SortExpression="SHOP_TYPE_NAME"
                        UniqueName="SHOP_TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Cửa hàng" DataField="STORE_CODE_NAME" SortExpression="STORE_CODE_NAME"
                        UniqueName="STORE_CODE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Tỷ lệ" DataField="RATE" SortExpression="RATE"
                        UniqueName="RATE" />
                    <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" SortExpression="NOTE"
                        UniqueName="NOTE" />
                    <tlk:GridBoundColumn HeaderText="Thưởng theo Doanh thu" DataField="IS_DOANHTHU_TEXT" SortExpression="IS_DOANHTHU_TEXT"
                        UniqueName="IS_DOANHTHU_TEXT" HeaderStyle-Width="70px"/>
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
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Setup_Rate_DTTT_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Setup_Rate_DTTT_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Setup_Rate_DTTT_RadPane2';
        var validateID = 'MainContent_ctrlPA_Setup_Rate_DTTT_valSum';
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
