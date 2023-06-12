<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CommendFormula.ascx.vb" Inherits="Profile.ctrlHU_CommendFormula" %>
<%@ Import Namespace="Framework.UI" %>
<%@ Import Namespace ="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="192px" Scrolling="Both">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Loại khen thưởng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbCommendList" runat="server" AutoPostBack="true" CausesValidation="False">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="CusTYPE" runat="server" ControlToValidate="cbCommendList"  ErrorMessage="<%$ Translate: Bạn phải chọn Loại khen thưởng %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại khen thưởng %>">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalTYPE" ControlToValidate="cbCommendList" runat="server" ErrorMessage="<%$ Translate: Loại khen thưởng không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại khen thưởng không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50" ReadOnly="true">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã công thức khen thưởng %>" ToolTip="<%$ Translate: Bạn phải nhập Mã công thức khen thưởng %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã công thức khen thưởng đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã công thức khen thưởng đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNamVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên công thức khen thưởng %>" ToolTip="<%$ Translate: Bạn phải nhập Tên công thức khen thưởng %>">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtNameVN" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thứ tự tính thưởng")%><span class="lbReq">*</span>
                </td>
                <td> 
                    <tlk:RadNumericTextBox ID="nmNumberOrder" MinValue="0" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="cvalNumberOrder" ControlToValidate="nmNumberOrder"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập thứ tự tính thưởng. %>" ToolTip="<%$ Translate: Bạn phải nhập thứ tự tính thưởng. %>">
                    </asp:RequiredFieldValidator>
                </td>     
            </tr>           
             <tr>
                <td class="lb">
                    <%# Translate("Công thức")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtFormula" runat="server" SkinID="Textbox1023" Width="100%" TextMode ="MultiLine">
                    </tlk:RadTextBox>                 
                </td>              
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%" TextMode ="MultiLine">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="<%$ Translate: Thông tin nhập liệu có chứa mã html %>"
                        ControlToValidate="txtRemark" ValidationExpression="^(?!.*<[^>]+>).*"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <%-- <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true" >
                <Selecting AllowRowSelect="true" /> ClientEvents-OnRowClick="StopPostback"
            </ClientSettings>--%>
            <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                    Height="100%" AllowSorting="True" AllowMultiRowSelection="false">
            <ClientSettings EnableRowHoverStyle="true" >
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowSelecting="StopPostback2" />
                <ClientEvents OnRowSelected = "StopPostback2" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,FORMULA,COMMENDLIST_NAME,COMMENDTYPE_NAME,COMMENDOBJECT_NAME,REMARK,NUMBER_ORDER,ACTFLG,COMMENDLIST_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" />
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Công thức %>" DataField="FORMULA"
                         UniqueName="FORMULA" SortExpression="FORMULA" >
                         <HeaderStyle Width="400px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại khen thưởng %>" DataField="COMMENDLIST_NAME"
                        UniqueName="COMMENDLIST_NAME" SortExpression="COMMENDLIST_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại DM Khen thưởng %>" DataField="COMMENDTYPE_NAME"
                        UniqueName="COMMENDTYPE_NAME" SortExpression="COMMENDTYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng khen thưởng %>" DataField="COMMENDOBJECT_NAME"
                         UniqueName="COMMENDOBJECT_NAME" SortExpression="COMMENDOBJECT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự tính thưởng %>" DataField="NUMBER_ORDER"
                        UniqueName="NUMBER_ORDER" SortExpression="NUMBER_ORDER" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                </Columns>
            </MasterTableView>
             <HeaderStyle Width="120px" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_CommendFormula_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_CommendFormula_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_CommendFormula_RadPane2';
        var validateID = 'MainContent_ctrlHU_CommendFormula_valSum';
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

        function RowClick(sender, eventArgs) {
            var gridItem = sender.get_masterTableView().get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            if (!gridItem.get_isInEditMode()) {
                __doPostBack("<%= rgMain.UniqueID %>", "RowClick;" + eventArgs.get_itemIndexHierarchical());
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        function StopPostback(sender,args)
        {
           return false;
        }

        function StopPostback2(sender, args) {
           return false;
        }
    </script>
</tlk:RadCodeBlock>