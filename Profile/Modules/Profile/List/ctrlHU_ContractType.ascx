<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ContractType.ascx.vb"
    Inherits="Profile.ctrlHU_ContractType" %>
<%@ Import Namespace ="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="190px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContractTypes" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                  <asp:Label ID="lbCode" runat="server" Text="Mã loại hợp đồng"></asp:Label>
                  <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" CausesValidation="false">
                    </tlk:RadTextBox>
                  <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="Bạn phải nhập mã loại hợp đồng." ToolTip="Bạn phải nhập mã loại hợp đồng.">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="Mã loại hợp đồng đã tồn tại."
                        ToolTip="Mã loại hợp đồng đã tồn tại.">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Mã không được chứa ký tự đặc biệt và khoảng trắng"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                 <td class="lb">
                   <asp:Label ID="Label1" runat="server" Text="Theo tháng/ngày"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadComboBox ID="cboFMD" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboFMD" runat="server"
                        ErrorMessage="Bạn phải chọn tháng/ngày" ToolTip="Bạn phải chọn tháng/ngày">
                    </asp:RequiredFieldValidator>
                    <%--<asp:CustomValidator ID="cvalPeriod" runat="server" ErrorMessage="Bạn phải nhập thời hạn loại hợp đồng >= 0."
                        ToolTip="Bạn phải nhập thời hạn loại hợp đồng >= 0.">
                    </asp:CustomValidator>--%>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkHSL" Text="Không bắt nhập HSL" />
                </td>  
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbName" runat="server" Text="Tên loại hợp đồng"></asp:Label>
                   <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                   <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="Bạn phải nhập tên loại hợp đồng." ToolTip="Bạn phải nhập tên loại hợp đồng."></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbPeriod" runat="server" Text="Thời hạn loại hợp đồng"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPeriod" runat="server" MinValue="0">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                    </tlk:RadNumericTextBox>
                  <asp:Label ID="lbmonth" runat="server" Text="(tháng/ngày)"></asp:Label>
                  <asp:RequiredFieldValidator ID="reqPeriod" ControlToValidate="rntxtPeriod" runat="server"
                        ErrorMessage="Bạn phải nhập thời hạn loại hợp đồng >= 0." ToolTip="Bạn phải nhập thời hạn loại hợp đồng >=0.">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalPeriod" runat="server" ErrorMessage="Bạn phải nhập thời hạn loại hợp đồng >= 0."
                        ToolTip="Bạn phải nhập thời hạn loại hợp đồng >= 0.">
                    </asp:CustomValidator>
                </td>  
                <td>
                    <asp:CheckBox runat="server" ID="chkIsRequirement" Text="Offer letter" />
                </td>              
            </tr>      
            <tr>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Tên hiển thị trên biểu mẫu"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVisibleForm" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNameVisibleForm" runat="server"
                        ErrorMessage="Bạn phải nhập Tên hiển thị trên biểu mẫu" ToolTip="Bạn phải nhập Tên hiển thị trên biểu mẫu">
                    </asp:RequiredFieldValidator>
                </td>                
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Quy tắc lấy ngày kết thúc"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCodeGetEndDate" runat="server" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboCodeGetEndDate" runat="server"
                        ErrorMessage="Bạn phải chọn Quy tắt lấy ngày kết thúc" ToolTip="Bạn phải chọn Quy tắt lấy ngày kết thúc">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkHocviec" Text="Hiển thị thời gian học việc" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                     <asp:Label ID="lbContract_Type" runat="server" Text="Loại hợp đồng"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContract_Type" runat="server" CausesValidation="false" >
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                   <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="472px" Height="43px" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgContractType" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,PERIOD,REMARK,TYPE_ID,NAME_VISIBLE_ONFORM,FLOWING_MD_ID,CODE_GET_ENDDATE_ID,IS_HOCVIEC,IS_REQUIREMENT,IS_HSL">
                <Columns>
                  <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã loại HĐLĐ" DataField="CODE" SortExpression="CODE"  HeaderStyle-Width="100px"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="Tên loại hợp đồng" DataField="NAME"  HeaderStyle-Width="200px"
                        SortExpression="NAME" UniqueName="NAME" />
                    <tlk:GridBoundColumn HeaderText="Tên hiển thị trên hợp đồng" DataField="NAME_VISIBLE_ONFORM"  HeaderStyle-Width="200px"
                        SortExpression="NAME_VISIBLE_ONFORM" UniqueName="NAME_VISIBLE_ONFORM" />
                    <tlk:GridBoundColumn HeaderText="Loại hợp đồng" DataField="TYPE_NAME" SortExpression="TYPE_NAME" HeaderStyle-Width="200px"
                        UniqueName="TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Theo tháng/ngày" DataField="FLOWING_MD" SortExpression="FLOWING_MD" HeaderStyle-Width="200px"
                        UniqueName="FLOWING_MD" />
                    <tlk:GridNumericColumn HeaderText="Thời hạn HĐ" DataField="PERIOD" HeaderStyle-Width="200px"
                        SortExpression="PERIOD" UniqueName="PERIOD" ItemStyle-HorizontalAlign="Right" />
                    <tlk:GridBoundColumn HeaderText="Quy tắt lấy ngày kết thúc" DataField="CODE_GET_ENDDATE" SortExpression="CODE_GET_ENDDATE" HeaderStyle-Width="200px"
                        UniqueName="CODE_GET_ENDDATE" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hiển thị thời gian học việc %>" DataField="IS_HOCVIEC"
                        DataType="System.Boolean" FilterControlWidth="20px" SortExpression="IS_HOCVIEC" UniqueName="IS_HOCVIEC" AllowFiltering="false">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="Offer letter" DataField="IS_REQUIREMENT"
                        DataType="System.Boolean" FilterControlWidth="20px" SortExpression="IS_REQUIREMENT" UniqueName="IS_REQUIREMENT" AllowFiltering="false">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridCheckBoxColumn>
                     <tlk:GridCheckBoxColumn HeaderText="Không bắt nhập HSL" DataField="IS_HSL"
                        DataType="System.Boolean" FilterControlWidth="20px" SortExpression="IS_HSL" UniqueName="IS_HSL" AllowFiltering="false">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK" HeaderStyle-Width="200px"
                        UniqueName="REMARK" />
                    <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="ACTFLG" HeaderStyle-Width="100px"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
<script type="text/javascript">

    var splitterID = 'ctl00_MainContent_ctrlHU_ContractType_RadSplitter3';
    var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_ContractType_RadPane1';
    var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_ContractType_RadPane2';
    var validateID = 'MainContent_ctrlHU_ContractType_valSum';
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
            var rows = $find('<%= rgContractType.ClientID %>').get_masterTableView().get_dataItems().length;
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
                ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgContractType');
            else
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
        } else if (args.get_item().get_commandName() == "EDIT") {
            ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            var bCheck = $find('<%= rgContractType.ClientID %>').get_masterTableView().get_selectedItems().length;
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

    function rgContractTypeRadGridDeSelecting() { }
    function rgContractTypeOnClientRowSelected() { }
    function rgContractTypeRadGridSelecting() { }
</script>
</tlk:RadCodeBlock>
