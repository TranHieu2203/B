<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_BHLDItem.ascx.vb"
    Inherits="Profile.ctrlHU_BHLDItem" %>
<%@ Import Namespace ="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="190px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContractTypes" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                  <asp:Label ID="lbCode" runat="server" Text="Mã loại BHLĐ"></asp:Label>
                  <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" CausesValidation="false">
                    </tlk:RadTextBox>
                  <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="Bạn phải nhập Mã loại BHLĐ." ToolTip="Bạn phải nhập Mã loại BHLĐ.">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="Mã loại BHLĐ đã tồn tại."
                        ToolTip="Mã loại BHLĐ đã tồn tại.">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Mã không được chứa ký tự đặc biệt và khoảng trắng"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>                
                <td class="lb">
                    <asp:Label ID="lbName" runat="server" Text="Tên loại BHLĐ"></asp:Label>
                   <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                   <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="Bạn phải nhập Tên loại BHLĐ." ToolTip="Bạn phải nhập Tên loại BHLĐ."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                
                <td class="lb">
                    <asp:Label ID="lbPeriod" runat="server" Text="Số lượng tính"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPeriod" runat="server" MinValue="0">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Đơn giá"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtdg" runat="server" MinValue="0">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                    </tlk:RadNumericTextBox>
                </td>  
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Số thứ tự"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnSTT" runat="server" MinValue="0">
                        <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                    </tlk:RadNumericTextBox>
                </td>  
                               
            </tr>      
            <tr>
                 <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsRequirement" Text="Tự động sinh" />
                </td> 
                <td class="lb">
                </td> 
                <td>
                    <asp:CheckBox runat="server" ID="chkHocviec" Text="Ẩn dữ liệu" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                   <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%" Height="43px" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgContractType" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME_VN,UNIT,REMARK,MONEY,ACTFLG,AUTOGEN,HIDE,ORDER_NUM">
                <Columns>
                  <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã loại BHLĐ" DataField="CODE" SortExpression="CODE"  HeaderStyle-Width="100px"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="Tên loại BHLĐ" DataField="NAME_VN"  HeaderStyle-Width="200px"
                        SortExpression="NAME_VN" UniqueName="NAME_VN" />

                    <tlk:GridNumericColumn HeaderText="Số lượng tính" DataField="UNIT" HeaderStyle-Width="200px"
                        SortExpression="UNIT" UniqueName="UNIT" ItemStyle-HorizontalAlign="Right" />
                    <tlk:GridNumericColumn HeaderText="Đơn giá" DataField="MONEY" HeaderStyle-Width="200px"
                        SortExpression="MONEY" UniqueName="MONEY" ItemStyle-HorizontalAlign="Right" />

                    <tlk:GridNumericColumn HeaderText="STT" DataField="ORDER_NUM" HeaderStyle-Width="60px"
                        SortExpression="ORDER_NUM" UniqueName="ORDER_NUM" ItemStyle-HorizontalAlign="Right" />

                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Tự động sinh %>" DataField="AUTOGEN"
                        DataType="System.Boolean" FilterControlWidth="20px" SortExpression="AUTOGEN" UniqueName="AUTOGEN" AllowFiltering="false">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="Ẩn dữ liệu" DataField="HIDE"
                        DataType="System.Boolean" FilterControlWidth="20px" SortExpression="HIDE" UniqueName="HIDE" AllowFiltering="false">
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

    var splitterID = 'ctl00_MainContent_ctrlHU_BHLDItem_RadSplitter3';
    var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_BHLDItem_RadPane1';
    var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_BHLDItem_RadPane2';
    var validateID = 'MainContent_ctrlHU_BHLDItem_valSum';
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
</script>
</tlk:RadCodeBlock>
