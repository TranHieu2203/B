<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Title_tbl.ascx.vb"
    Inherits="Profile.ctrlHU_Title_tbl" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidTITLE_GROUP_ID" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCode" Text="Mã chức vụ Hội đồng"></asp:Label><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtCode" runat="server" Width="200%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="Bạn phải nhập Mã chức danh" ToolTip="Bạn phải nhập Mã chức danh">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="Mã chức danh đã tồn tại."
                        ToolTip="Mã chức danh đã tồn tại.">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Mã không được chứa ký tự đặc biệt và khoảng trắng"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>              
                
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbNameVN" Text="Tên chức vụ Hội đồng"></asp:Label>                    
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNameVN" runat="server" Width="200%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="Bạn phải nhập Tên chức vụ (VN)" ToolTip="Bạn phải nhập Tên chức vụ (VN)">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                        ControlToValidate="txtNameVN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
                <td class="lb" style="display:none">
                    <asp:Label runat="server" ID="Label1" Text="Tên chức vụ (EN)"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td style="display:none">
                    <tlk:RadTextBox ID="txtNameEN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNameEN" runat="server"
                        ErrorMessage="Bạn phải nhập Tên chức vụ (EN)" ToolTip="Bạn phải nhập Tên chức vụ (EN)">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                        ControlToValidate="txtNameVN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
           
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemark" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Height="70" Width="200%">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                        ControlToValidate="txtRemark" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
                <td style="visibility: hidden">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">          
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,NAME_EN,REMARK,ID">
                <Columns>
                    <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã chức danh" DataField="CODE"
                        HeaderStyle-Width="100px" UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="Tên công ty" DataField="ORG_ID_NAME"
                        HeaderStyle-Width="100px" UniqueName="ORG_ID_NAME" SortExpression="ORG_ID_NAME" />
                    <tlk:GridBoundColumn HeaderText="Loại tổ chức" DataField="ORG_TYPE_NAME"
                        HeaderStyle-Width="100px" UniqueName="ORG_TYPE_NAME" SortExpression="ORG_TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Tên chức danh" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="Nhóm chức danh" DataField="TITLE_GROUP_NAME"
                        UniqueName="TITLE_GROUP_NAME" SortExpression="TITLE_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="Mô tả công việc" DataField="REMARK"
                        UniqueName="REMARK" SortExpression="REMARK" />
                    <tlk:GridCheckBoxColumn HeaderText="Độc hại" DataField="HURTFUL_CHECK"
                        UniqueName="HURTFUL_CHECK" SortExpression="HURTFUL_CHECK" AllowFiltering="false" />
                    <tlk:GridCheckBoxColumn HeaderText="Đặc biệt độc hại" DataField="SPEC_HURFUL_CHECK"
                        UniqueName="SPEC_HURFUL_CHECK" SortExpression="SPEC_HURFUL_CHECK" AllowFiltering="false" />
                    <tlk:GridBoundColumn HeaderText="Đối tượng độc hại" DataField="HURT_TYPE_NAME"
                        UniqueName="HURT_TYPE_NAME" SortExpression="HURT_TYPE_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="Tính OVT" DataField="OVT_CHECK" UniqueName="OVT_CHECK"
                        SortExpression="OVT_CHECK" AllowFiltering="false" />
                    <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                    <tlk:GridBoundColumn HeaderText="Tài liệu đính kèm" DataField="FILENAME"
                        HeaderStyle-Width="100px" UniqueName="FILENAME" SortExpression="FILENAME" />--%>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true"> 
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

        var splitterID = 'ctl00_MainContent_ctrlHU_Chucvu_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Chucvu_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_Chucvu_RadPane2';
        var validateID = 'MainContent_ctrlHU_Chucvu_valSum';
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
