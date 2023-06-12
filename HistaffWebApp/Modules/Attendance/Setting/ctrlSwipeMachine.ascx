<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSwipeMachine.ascx.vb"
    Inherits="Attendance.ctrlSwipeMachine" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidOrg" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="172px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:Label ID="lblViewTitle" runat="server"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
                    CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã máy chấm công")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50" TabIndex="1">
                            </tlk:RadTextBox>
                            <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã máy chấm công đã tồn tại. %>"
                                ToolTip="<%$ Translate: Mã máy chấm công đã tồn tại. %>">
                            </asp:CustomValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                                ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên máy chấm công")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtName" runat="server" ToolTip="" TabIndex="2">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn nhập tên máy chấm công. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbOrgName" runat="server" Text="Bộ phận"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtOrgName" SkinID="Readonly" ReadOnly="true" />
                            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Địa điểm đặt máy")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtAddress" runat="server" ToolTip="" TabIndex="3">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAddress"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập địa điểm máy chấm công. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Port")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" MaxLength="10" ID="txtPort" SkinID="Number" TabIndex="6" FocusedStyle-PaddingLeft="1px"
                                runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại máy")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTerminalType" runat="server" ToolTip="" TabIndex="3">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboTerminalType"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn hãy chọn loại máy. %>"></asp:RequiredFieldValidator>
                        </td>

                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("IP")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtIP" runat="server" ToolTip="" TabIndex="5">
                            </tlk:RadTextBox>
                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ControlToValidate="txtIP" ErrorMessage="<%$ Translate: IP định dạng chưa đúng %>" 
                            Font-Size="Small" 
                            ValidationExpression="^(([01]?\d\d?|2[0-4]\d|25[0-5])\.){3}([01]?\d\d?|25[0-5]|2[0-4]\d)$"></asp:RegularExpressionValidator>--%>
                        </td>
                        <td class="lb">
                            <%# Translate("Mật khẩu")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox MinValue="0" MaxLength="10" ID="txtpasswords" SkinID="Number" TabIndex="6" FocusedStyle-PaddingLeft="1px"
                                runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Máy chấm công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSwipeMachine" runat="server" ToolTip="" TabIndex="3">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtGhichu" SkinID="Textbox1023" runat="server" Width="100%" TabIndex="7">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rglSwipeMachine" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ORG_NAME,ORG_ID,TERMINAL_CODE,TERMINAL_NAME,ADDRESS_PLACE,TERMINAL_IP,PASS,PORT,NOTE,TERMINAL_TYPE,TERMINAL_TYPE_NAME,TIME_RECORDER">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã máy chấm công%>" DataField="TERMINAL_CODE"
                                UniqueName="TERMINAL_CODE" SortExpression="TERMINAL_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên máy chấm công%>" DataField="TERMINAL_NAME"
                                UniqueName="TERMINAL_NAME" SortExpression="TERMINAL_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa điểm đặt máy%>" DataField="ADDRESS_PLACE"
                                UniqueName="ADDRESS_PLACE" SortExpression="ADDRESS_PLACE">
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME">
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: IP %>" DataField="TERMINAL_IP" UniqueName="TERMINAL_IP"
                                SortExpression="TERMINAL_IP" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Password %>" DataField="PASS" UniqueName="PASS"
                                SortExpression="PASS" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Port %>" DataField="PORT" UniqueName="PORT"
                                SortExpression="PORT" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại máy %>" DataField="TERMINAL_TYPE_NAME" UniqueName="TERMINAL_TYPE_NAME"
                                SortExpression="TERMINAL_TYPE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Máy chấm công %>" DataField="TIME_RECORDER_NAME" UniqueName="TIME_RECORDER_NAME"
                                SortExpression="TIME_RECORDER_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
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
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        var splitterID = 'ctl00_MainContent_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSwipeMachine_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlSwipeMachine_RadPane2';
        var validateID = 'MainContent_ctrlSwipeMachine_valSum';
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
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rglSwipeMachine.ClientID %>').get_masterTableView().get_dataItems().length;
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rglSwipeMachine');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rglSwipeMachine.ClientID %>').get_masterTableView().get_selectedItems().length;
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
function rbtClicked(sender, eventArgs) {
    enableAjax = false;
}

    </script>
</tlk:RadCodeBlock>
