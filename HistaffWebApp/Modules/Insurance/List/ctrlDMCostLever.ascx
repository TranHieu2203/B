<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMCostLever.ascx.vb"
    Inherits="Insurance.ctrlDMCostLever" %>
    <%@ Import Namespace="Common" %>
        <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="130px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhóm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã bệnh viện đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã nhóm đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="txtCode" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập mã nhóm. %>"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên Nhóm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập tên nhóm. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectiveDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdEffectiveDate"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectiveDateTo" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdEffectiveDateTo"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn chưa nhập ngày hết hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hiêu lực phải nhỏ hơn ngày hết hiệu lực %>"
                        ErrorMessage="<%$ Translate: Ngày hiêu lực phải nhỏ hơn ngày hết hiệu lực %>" Type="Date"
                        Operator="GreaterThan" ControlToCompare="rdEffectiveDate" ControlToValidate="rdEffectiveDateTo"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phí BH năm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtMoney" SkinID="Money" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server"
            Height="100%" OnPreRender="rgDanhMuc_PreRender" OnItemCreated="rgDanhMuc_ItemCreated">
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="true" />
                 <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="COST_CODE,COST_NAME,COST_MONEY,EFFECTDATE_FROM,EFFECTDATE_TO">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhóm %>" DataField="COST_CODE"
                        UniqueName="COST_CODE" SortExpression="COST_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhóm %>" DataField="COST_NAME"
                        UniqueName="COST_NAME" SortExpression="COST_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Phí BH năm  %>" DataField="COST_MONEY"
                        ItemStyle-HorizontalAlign="Right" UniqueName="COST_MONEY" DataFormatString="{0:N0}"
                        SortExpression="COST_MONEY" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataFormatString="{0:dd/MM/yyyy}"
                        DataField="EFFECTDATE_FROM" UniqueName="EFFECTDATE_FROM" SortExpression="EFFECTDATE_FROM" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataFormatString="{0:dd/MM/yyyy}"
                        DataField="EFFECTDATE_TO" UniqueName="EFFECTDATE_TO" SortExpression="EFFECTDATE_TO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG" ItemStyle-HorizontalAlign="Center"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlDMCostLever_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMCostLever_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMCostLever_RadPane2';
        var validateID = 'MainContent_ctrlDMCostLever_valSum';
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
            if (item.get_commandName() == "EXPORT") {
                var rows = $find('<%= rgDanhMuc.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgDanhMuc.ClientID %>').get_masterTableView().get_selectedItems().length;
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
    </script>
</tlk:RadCodeBlock>
