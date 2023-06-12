<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSetUpTimeEmp.ascx.vb"
    Inherits="Attendance.ctrlSetUpTimeEmp" %>
    <%@ Import Namespace="Common" %>
    <link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmpCode" runat="server" SkinID="ReadOnly" Width="130px" ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEmpCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalStaff" ControlToValidate="txtEmpCode" runat="server"
                        ErrorMessage="<%$ Translate: Nhân viên này đã được thiết lập. %>" ToolTip="<%$ Translate: Nhân viên này đã được thiết lập. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Họ và Tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" SkinID="ReadOnly" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitle" SkinID="ReadOnly" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg" SkinID="ReadOnly" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lần quẹt thẻ tối thiểu tính đủ công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboccdb">
                        <Items>
                            <tlk:RadComboBoxItem runat="server" Value="1" Text="1" />
                            <tlk:RadComboBoxItem runat="server" Value="2" Text="2" />
                            <tlk:RadComboBoxItem runat="server" Value="3" Text="3" />
                            <tlk:RadComboBoxItem runat="server" Value="4" Text="4" />
                            <tlk:RadComboBoxItem runat="server" Value="5" Text="5" />
                        </Items>
                    </tlk:RadComboBox>
                    <%# Translate("Lần/ngày")%>
                    <asp:CustomValidator ID="cusccdb" runat="server" ErrorMessage="<%$ Translate:  Bạn phải chọn số lần quẹt tối thiểu tính đủ công. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn số lần quẹt tối thiểu tính đủ công. %>"
                        ClientValidationFunction="cusccdb">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%> 
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" Width="100%" runat="server" Height = "40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPreRender ="rgDanhMuc_PreRender" OnItemCreated="rgDanhMuc_ItemCreated"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="True">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <ClientEvents OnRowSelected="RowSelected" />
                <ClientEvents OnRowDeselected="RowDeselected" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,TITLE_ID,TITLE_NAME,ORG_ID,ORG_NAME,NUMBER_SWIPECARD,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên%>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và Tên%>" DataField="EMPLOYEE_NAME"
                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh%>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị%>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME">
                        <HeaderStyle Width="150px" />
                        <ItemStyle Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lần quẹt%>" DataField="NUMBER_SWIPECARD"
                        UniqueName="NUMBER_SWIPECARD" SortExpression="NUMBER_SWIPECARD" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả%>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle Width="70px" />
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlSetUpTimeEmp_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSetUpTimeEmp_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSetUpTimeEmp_RadPane2';
        var validateID = 'MainContent_ctrlSetUpTimeEmp_valSum';
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
        function cusccdb(oSrc, args) {
            var cbo = $find("<%# cboccdb.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            debugger;
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
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
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
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

        function GetSelectedItems(str) {
            var grid = $find("<%=rgDanhMuc.ClientID %>");
            var gridSelectedItems = grid.get_selectedItems().length;
            alert(gridSelectedItems);
        }
        function RowDeselected(sender, args) {
            var grid = $find("<%=rgDanhMuc.ClientID %>");
            var gridSelectedItems = grid.get_selectedItems().length;
            if (gridSelectedItems <= 0) {
                $("#ctl00_MainContent_ctrlSetUpTimeEmp_txtEmpCode").val("");
                $("#ctl00_MainContent_ctrlSetUpTimeEmp_txtName").val("");
                $("#ctl00_MainContent_ctrlSetUpTimeEmp_txtTitle").val("");
                $("#ctl00_MainContent_ctrlSetUpTimeEmp_txtOrg").val("");
                $("#ctl00_MainContent_ctrlSetUpTimeEmp_cboccdb_Input").val("");
                $("#ctl00_MainContent_ctrlSetUpTimeEmp_txtNote").val("");
            }
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        /*function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }
        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }*/
    </script>
</tlk:RadCodeBlock>
