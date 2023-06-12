<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMNoiKhamChuaBenh.ascx.vb"
    Inherits="Insurance.ctrlDMNoiKhamChuaBenh" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="170px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã bệnh viện")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="txtCode" runat="server"
                        Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã bệnh viện. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã bệnh viện đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã bệnh viên đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên bệnh viện")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên bệnh viện. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tỉnh/Thành phố")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProvince" AutoPostBack="true" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="cusProvince" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tỉnh/thành phố. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tỉnh/thành phố. %>" ClientValidationFunction="cusProvince">
                    </asp:CustomValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboProvince" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn tỉnh/thành phố. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tỉnh/thành phố. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusProvince" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tỉnh/thành phố. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tỉnh/thành phố. %>" ClientValidationFunction="cusProvince">
                    </asp:CustomValidator>
                     <asp:CustomValidator ID="cvalProvince" runat="server" ControlToValidate="cboProvince"
                        ErrorMessage="<%$ Translate: Tỉnh/Thành phố không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Tỉnh/Thành phố không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Quận/huyện")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDistrict" runat="server">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="cusDistrict" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quận/huyện. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn quận/huyện. %>" ClientValidationFunction="cusDistrict">
                    </asp:CustomValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboDistrict" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn quận/huyện. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn quận/huyện. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusDistrict" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn quận/huyện. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn quận/huyện. %>" ClientValidationFunction="cusDistrict">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalDistrict" runat="server" ControlToValidate="cboDistrict"
                        ErrorMessage="<%$ Translate: Quận/Huyện không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Quận/Huyện không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtAddrress" runat="server" SkinID="Textbox1023" Width="100%" Height="57">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,ADDRESS,ID_PROVINCE,ID_DISTRICT,DISTRICT_NAME,ACTFLG">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã bệnh viện %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên bệnh viện %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ %>" DataField="ADDRESS" UniqueName="ADDRESS"
                        SortExpression="ADDRESS" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉnh/Thành phố %>" DataField="PROVINCE_NAME"
                        UniqueName="PROVINCE_NAME" SortExpression="PROVINCE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Quận/Huyện %>" DataField="DISTRICT_NAME"
                        UniqueName="DISTRICT_NAME" SortExpression="DISTRICT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Sử dụng %>" DataField="ACTFLG" UniqueName="ACTFLG"
                        SortExpression="ACTFLG" />
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlDMNoiKhamChuaBenh_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMNoiKhamChuaBenh_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMNoiKhamChuaBenh_RadPane2';
        var validateID = 'MainContent_ctrlDMNoiKhamChuaBenh_valSum';
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

        function cusProvince(oSrc, args) {
            var cbo = $find("<%# cboProvince.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusDistrict(oSrc, args) {
            var cbo = $find("<%# cboDistrict.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');                    
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }

            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgDanhMuc.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        
    </script>
</tlk:RadCodeBlock>
