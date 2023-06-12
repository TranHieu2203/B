<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_ObjectSalary.ascx.vb"
    Inherits="Payroll.ctrlPA_ObjectSalary" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
   @media screen and (-webkit-min-device-pixel-ratio:0) {
        #ctl00_MainContent_ctrlPA_ObjectSalary_rdNote
        {       
            height: 56px;
        } 
    }
    .RadGrid_Metro .rgRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgAltRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgHeader
    {
        padding :0 !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="164px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã đối tượng lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                    <tlk:RadTextBox ID="txtID" SkinID="Textbox15" runat="server" Visible="false">
                    </tlk:RadTextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mã đối tượng lương. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập mã đối tượng lương. %>"></asp:RequiredFieldValidator>

                    <asp:CustomValidator ID="cvalObjSalary" runat="server" ControlToValidate="txtCode"
                        ErrorMessage="<%$ Translate: Mã đối tượng lương đã tồn tại %>"
                        ToolTip="<%$ Translate: Mã đối tượng lương đã tồn tại %>" >
                    </asp:CustomValidator>
                </td>

                
                <td class="lb">
                    <%# Translate("Nhóm lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboGroupSal">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboGroupSal"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm lương. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nhóm lương. %>"></asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="dpEffectiveDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="dpEffectiveDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên tiếng việt")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên tiếng việt. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên tiếng anh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameEN" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtNameEN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên tiếng anh. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rdNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true" PageSize="50">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME_VN,NAME_EN,EFFECTIVE_DATE,ACTFLG,SDESC,GROUP_SAL">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm lương %>" DataField="GROUP_SAL_NAME"
                        UniqueName="GROUP_SAL_NAME" SortExpression="GROUP_SAL_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã đối tượng lương %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng việt %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng anh %>" DataField="NAME_EN"
                        UniqueName="NAME_EN" SortExpression="NAME_EN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE"
                        DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" UniqueName="EFFECTIVE_DATE"
                        SortExpression="EFFECTIVE_DATE" CurrentFilterFunction="EqualTo" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="SDESC" UniqueName="SDESC"
                        SortExpression="SDESC" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPA_ObjectSalary_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_ObjectSalary_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_ObjectSalary_RadPane2';
        var validateID = 'MainContent_ctrlPA_ObjectSalary_valSum';
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
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlPA_ObjectSalary_dpEffectiveDate_dateInput').val('');
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
        }

    </script>
</tlk:RadCodeBlock>
