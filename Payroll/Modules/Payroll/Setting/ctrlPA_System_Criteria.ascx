<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_System_Criteria.ascx.vb"
    Inherits="Payroll.ctrlPA_System_Criteria" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="240px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">

            <tr>
                <td class="lb">
                    <%# Translate("Mã tiêu chí")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mã tham số %>"
                        ToolTip="<%$ Translate: Bạn phải nhập mã tiêu chí %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên tiêu chí")%><span class="lbReq">*</span>
                </td>
                <td >
                    <tlk:RadTextBox ID="txtName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên tham số %>"
                        ToolTip="<%$ Translate: Bạn phải nhập tên tham số %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kiểu dữ liệu")%>
                </td>
                <td >
                    <tlk:RadComboBox ID="cboData_Type" runat="server" SkinID="dDropdownList">
                        <Items>
                            <tlk:RadComboBoxItem Value="1" Text="Kiểu số" />
                            <tlk:RadComboBoxItem Value="0" Text="Kiểu chữ" />
                            <tlk:RadComboBoxItem Value="2" Text="Kiểu ngày" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
                <td >
                    <%# Translate("Hiển thị CT lương")%>
                </td>
                <td >
                    <asp:CheckBox ID="chkIs_Salary" runat="server" />
                </td>
            </tr>
             <tr>
                <td class="lb">
                    <%# Translate("Đối tượng lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboOBJ_SALARY" runat="server" SkinID="dDropdownList" AutoPostBack="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalOBJ_SALARY" runat="server" ControlToValidate="cboOBJ_SALARY"
                        ErrorMessage="<%$ Translate: Đối tượng lương không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Đối tượng lương không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>                   
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CT thực hiện")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtFomuler" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%" Height="40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" id="tdSGlbOrders">
                    <label id="lbOrders">
                        <%# Translate("Thứ tự thực hiện")%></label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtUnit" MinValue="1"
                        Value="1" ShowSpinButtons="True" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtUnit"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự %>"></asp:RequiredFieldValidator>
                </td>
            </tr>

        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE, NAME,FOMULER,NOTE,UNIT,DATA_TYPE,IS_SALARY,OBJ_SAL_ID,DATA_TYPE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã tiêu chí %>" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiêu chí %>" DataField="NAME"
                        SortExpression="NAME" UniqueName="NAME" HeaderStyle-Width="250px" />

                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Công thức thực hiện %>" DataField="FOMULER"
                        SortExpression="FOMULER" UniqueName="FOMULER" HeaderStyle-Width="500px" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="UNIT" SortExpression="UNIT"
                        UniqueName="UNIT">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                      <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu dữ liệu %>" DataField="DATA_TYPE_NAME" SortExpression="DATA_TYPE_NAME"
                        UniqueName="DATA_TYPE_NAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng lương %>" DataField="OBJ_SAL_NAME" SortExpression="OBJ_SAL_NAME"
                        UniqueName="OBJ_SAL_NAME" HeaderStyle-Width="100px" />
                      <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Hiển thị CT lương %>" DataField="IS_SALARY" DataType="System.Boolean"
                        ItemStyle-VerticalAlign="Middle" UniqueName="IS_SALARY" SortExpression="IS_SALARY" HeaderStyle-Width="80px"
                        FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"  AutoPostBackOnFilter="true" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" SortExpression="NOTE"
                        UniqueName="NOTE" HeaderStyle-Width="160px" />

                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_System_Criteria_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_System_Criteria_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_System_Criteria_RadPane2';
        var validateID = 'MainContent_ctrlPA_System_Criteria_ValidationSummary1';
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
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
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
