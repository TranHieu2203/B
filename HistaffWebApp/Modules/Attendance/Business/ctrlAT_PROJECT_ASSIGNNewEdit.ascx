<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_PROJECT_ASSIGNNewEdit.ascx.vb"
    Inherits="Attendance.ctrlAT_PROJECT_ASSIGNNewEdit" %>
<%@ Import Namespace="Common" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="115px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromdate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqFromdate" ControlToValidate="rdFromdate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn chưa nhập từ ngày. %>" ToolTip="<%$ Translate: Bạn chưa nhập từ ngày. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEnddate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEnddate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập đến ngày. %>" ToolTip="<%$ Translate: Bạn chưa nhập đến ngày. %>"> </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cusvalDate" runat="server" ErrorMessage="<%$ Translate: ĐẾN NGÀY PHẢI LỚN HƠN TỪ NGÀY %>"
                        ToolTip="<%$ Translate: ĐẾN NGÀY PHẢI LỚN HƠN TỪ NGÀY %>" ClientValidationFunction="cuavalDate">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <%# Translate("Công việc trong dự án")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProjectWork" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusProjectWork" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Công việc trong dự án %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Công việc trong dự án %>" ClientValidationFunction="cusProjectWork">
                    </asp:CustomValidator>
                </td>
                <td>
                    <%# Translate("Giờ công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtHours" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rntxtHours"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa nhập Giờ công %>" ToolTip="<%$ Translate: Bạn chưa chọn Giờ công %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgWorkschedule" AllowPaging="false" runat="server"
            Height="100%">
            <MasterTableView AllowPaging="true" AllowCustomPaging="true" DataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID"
                ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID" CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnFindEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
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
            <ClientSettings>
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
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctl00_MainContent_PagePlaceHolderPanel';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAT_PROJECT_ASSIGNNewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAT_PROJECT_ASSIGNNewEdit_RadPane2';
        var validateID = 'MainContent_ctrlAT_PROJECT_ASSIGNNewEdit_valSum';
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

        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
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

        function cusProjectWork(oSrc, args) {
            var cbo = $find("<%# cboProjectWork.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cuavalDate(oSrc, args) {
            var fromDate = $find("<%# rdFromdate.ClientID %>");
            var enddate = $find("<%# rdEnddate.ClientID %>");
            if (fromDate.get_selectedDate() > enddate.get_selectedDate()) 
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    </script>
</tlk:RadCodeBlock>
