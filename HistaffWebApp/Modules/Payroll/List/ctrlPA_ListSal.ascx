<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_ListSal.ascx.vb"
    Inherits="Payroll.ctrlPA_ListSal" %>
    <%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valsum" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã danh mục lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCOL_NAME" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCOL_NAME"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mã danh mục lương. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập mã danh mục lương. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên tiếng việt")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNAME_VN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNAME_VN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên tiếng việt. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập tên tiếng việt. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Kiểu dữ liệu")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDATA_TYPE" runat="server" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Value="1" Text="Kiểu số" />
                            <tlk:RadComboBoxItem Value="0" Text="Kiểu chữ" />
                            <tlk:RadComboBoxItem Value="2" Text="Kiểu ngày" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm ký hiệu")%><span class="lbReq">*</span>
                </td>
                <td>
                <tlk:RadComboBox ID="cboGROUP_TYPE" runat="server" SkinID="dDropdownList">
                </tlk:RadComboBox>
                 <asp:CustomValidator ID="cvalGROUP_TYPE" ControlToValidate="cboGROUP_TYPE" runat="server" ErrorMessage="<%$ Translate: Nhóm ký hiệu không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Nhóm ký hiệu không tồn tại hoặc đã ngừng áp dụng. %>">
                 </asp:CustomValidator>   
                </td>
                <td class="lb">
                    <%# Translate("Thứ tự") %><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCOL_INDEX" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="nmCOL_INDEX"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập thứ tự. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" >
                    <%# Translate("Ghi chú")%>
                </td>
                <td >
                   <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%" Height = "40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowPaging="true" Width="100%" PageSize="50">
             <ClientSettings EnablePostBackOnRowClick="True">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,COL_NAME,NAME_VN,NAME_EN,DATA_TYPE,COL_INDEX,STATUS,GROUP_TYPE,GROUP_TYPE_NAME,COL_CODE,REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã danh mục lương %>" DataField="COL_NAME"
                        SortExpression="COL_NAME" UniqueName="COL_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng việt %>" DataField="NAME_VN"
                        SortExpression="NAME_VN" UniqueName="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu dữ liệu %>" DataField="DATA_TYPE"
                        SortExpression="DATA_TYPE" UniqueName="DATA_TYPE" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu dữ liệu %>" DataField="DATA_TYPE_NAME"
                        SortExpression="DATA_TYPE_NAME" UniqueName="DATA_TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm ký hiệu %>" DataField="GROUP_TYPE_NAME"
                        SortExpression="GROUP_TYPE_NAME" UniqueName="GROUP_TYPE_NAME"  />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="COL_INDEX" SortExpression="COL_INDEX"
                        UniqueName="COL_INDEX">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS"
                        SortExpression="STATUS" UniqueName="STATUS" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                        SortExpression="REMARK" UniqueName="REMARK"  />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlPA_ListSal_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_ListSal_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_ListSal_RadPane2';
        var validateID = 'MainContent_ctrlPA_ListSal_valsum';
        var oldSize = $('#' + pane1ID).height();
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
                var grid = $find("<%=rgData.ClientID %>");
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
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
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
