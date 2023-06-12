<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRegisterOTNewEdit.ascx.vb"
    Inherits="Attendance.ctrlRegisterOTNewEdit" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
   @media screen and (-webkit-min-device-pixel-ratio:0) {
        #ctl00_MainContent_ctrlRegisterOTNewEdit_txtGhiChu
        {       
            height: 56px;
        } 
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="198px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Ngày đăng ký")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdregisterDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdregisterDate"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn ngày đăng ký làm thêm. %>"
                        ToolTip="<%$ Translate: Chưa chọn ngày đăng ký làm thêm. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số làm thêm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbohs_ot" Width="160px" MaxLength="80" runat="server" ToolTip="">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cbohs_ot"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn chọn hệ số làm thêm. %>"
                        ToolTip="<%$ Translate: Chưa chọn chọn hệ số làm thêm. %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cval_cbohs_ot" runat="server" ControlToValidate="cbohs_ot"
                        ErrorMessage="<%$ Translate: Chưa chọn hệ số làm thêm. %>"
                        ToolTip="<%$ Translate: Chưa chọn hệ số làm thêm. %>" >
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="txtTuGio" runat="server">
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTuGio"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn làm thêm từ giờ. %>" ToolTip="<%$ Translate: Chưa chọn làm thêm từ giờ. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến giờ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTimePicker ID="txtDenGio" runat="server">
                    </tlk:RadTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtDenGio"
                        runat="server" ErrorMessage="<%$ Translate: Chưa chọn làm thêm đến giờ. %>" ToolTip="<%$ Translate: Chưa chọn làm thêm đến giờ. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr style="display:none">
                <td class="lb">
                    <%# Translate("Nghỉ bù")%>
                </td>
                <td colspan="3">
                    <asp:CheckBox ID="cboIs_nb" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lý do làm thêm")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtGhiChu" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgWorkschedule" AllowPaging="true" runat="server" Height="100%">
            <MasterTableView  DataKeyNames="ID,EMPLOYEE_CODE"
                ClientDataKeyNames="ID,EMPLOYEE_CODE" CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                        </div>
                        <div style ="float:right">
                            <tlk:RadButton Width="100px" ID="btnDeleteEmployee" runat="server" Text="<%$ Translate: Xóa nhân viên %>"
                                CausesValidation="false" CommandName="DeleteEmployee">
                            </tlk:RadButton>
                        </div>
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                        UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME" />
                </Columns>
            </MasterTableView>
            <HeaderStyle HorizontalAlign="Center" />
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlRegisterOTNewEdit_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRegisterOTNewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRegisterOTNewEdit_RadPane2';
        var validateID = 'MainContent_ctrlRegisterOTNewEdit_valSum';
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

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule');
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

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule');
        }
    </script>
</tlk:RadCodeBlock>
