<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CB_AssessmentNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_CB_AssessmentNewEdit" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style type="text/css">
    .ctl00_MainContent_ctrlHU_CB_AssessmentNewEdit_RadTextBox1 {
        height: 100px;
    }
</style>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidSigner" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="400px">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <div style="width: 100%; float: left;">
            <div style="width: 100%; float: left;">
                <table class="table-form">
                    <tr>
                        <td colspan="6">
                            <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <b style="color: red">
                                <%# Translate("Thông tin đánh giá")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm ghi nhận")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnConfirmYear" MinValue="1900" MaxValue="9999" NumberFormat-GroupSeparator="">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnConfirmYear"
                                runat="server" ErrorMessage="<%$ Translate: Chưa nhập Năm ghi nhận %>"
                                ToolTip="<%$ Translate: Chưa nhập Năm ghi nhận %>"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Năm đánh giá")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnAssessmentYear" MinValue="1900" MaxValue="9999">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnAssessmentYear"
                                runat="server" ErrorMessage="<%$ Translate: Chưa nhập Năm đánh giá %>"
                                ToolTip="<%$ Translate: Chưa nhập Năm đánh giá %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Nội dung")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtContent" runat="server" SkinID="Textbox1023" Height="40" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtContent"
                                runat="server" ErrorMessage="<%$ Translate: Chưa nhập Nội dung %>"
                                ToolTip="<%$ Translate: Chưa nhập Nội dung %>"> </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Height="40" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <b style="color: red">
                                <%# Translate("Danh sách cán bộ ")%></b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã CBCNV")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployeeCode" AutoPostBack="true" Width="81%">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmp" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên CBCNV")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtEmployeName" ReadOnly="true" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                            <tlk:RadTextBox runat="server" ID="txtMaThe" ReadOnly="true" SkinID="ReadOnly" Visible="false">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Đơn vị")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Chức danh NQL")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtCurrentTitle" ReadOnly="true" SkinID="ReadOnly">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kết quả đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboResult" >
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtDtlRemark" runat="server" SkinID="Textbox1023" Height="40" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" AllowPaging="true" runat="server" Height="100%" Scrolling="None">
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView AllowPaging="false" AllowCustomPaging="false" DataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_NAME" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_NAME" CommandItemDisplay="Top">
                <CommandItemStyle Height="25px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="72px" ID="btnAdd" runat="server" Text="Thêm" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                CausesValidation="false" CommandName="Add" TabIndex="3">
                            </tlk:RadButton>
                            <tlk:RadButton Width="112px" ID="btnExport" runat="server" Text="Xuất file mẫu" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/export1.png"
                                CausesValidation="false" CommandName="Export" OnClientClicked="btnExportTemplate" TabIndex="5">
                            </tlk:RadButton>
                            <tlk:RadButton Width="112px" ID="btnImport" runat="server" Text="Nhập file mẫu" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/import1.png"
                                CausesValidation="false" CommandName="Import" TabIndex="6">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
                            <tlk:RadButton Width="70px" ID="btnDelete" runat="server" Text="Xóa" CausesValidation="false" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                CommandName="Delete" TabIndex="3">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                        ReadOnly="true" />
                    <tlk:GridBoundColumn HeaderText="Mã CBCNV" DataField="EMPLOYEE_CODE"
                        ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="130px"/>
                    <tlk:GridBoundColumn HeaderText="Mã thẻ" DataField="MATHE_NAME" HeaderStyle-Width="100px"
                        ReadOnly="true" UniqueName="MATHE_NAME" SortExpression="MATHE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Tên CBCNV" DataField="EMPLOYEE_NAME" HeaderStyle-Width="120px"
                        ReadOnly="true" UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Chức danh NQL" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        HeaderStyle-Width="130px" ReadOnly="true" SortExpression="TITLE_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Kết quả đánh giá" DataField="RESULT_NAME" UniqueName="RESULT_NAME"
                        HeaderStyle-Width="130px" ReadOnly="true" SortExpression="RESULT_NAME" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" UniqueName="REMARK"
                        HeaderStyle-Width="130px" ReadOnly="true" SortExpression="REMARK" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSigner" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlHU_CB_AssessmentNewEdit_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_CB_AssessmentNewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_CB_AssessmentNewEdit_RadPane2';
        var validateID = 'MainContent_ctrlHU_CB_AssessmentNewEdit_valSum';
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

        function btnExportTemplate(sender, args) {
            enableAjax = false;
        }
        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    //                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                    ResizeSplitterDefault();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            debugger;
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == 'CANCEL') {
                //  getRadWindow().close(null);
                //  args.set_cancel(true);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
    </script>
</tlk:RadCodeBlock>
