﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlChildrenNewEdit.ascx.vb"
    Inherits="Attendance.ctrlChildrenNewEdit" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdWorkingday" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdWorkingday"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn chưa chọn ngày hiệu lực %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDay" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdExpireDay"
                        runat="server" ErrorMessage="<%$ Translate: Bạn chưa chọn ngày hết hiệu lực %>" ToolTip="<%$ Translate: Bạn chưa chọn ngày hết hiệu lực %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>         
            <tr>
                <td class="lb">
                    <%# Translate("Lý do")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtGhiChu" SkinID="Textbox1023" runat="server" Width="100%" Height="40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkTS_Exception" runat="server" Text="<%$ Translate: Ngoại lệ%>"
                                        />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgData" AllowPaging="false" runat="server"
            Height="100%">
            <MasterTableView AllowPaging="true" AllowCustomPaging="true" DataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID"
                ClientDataKeyNames="ID,EMPLOYEE_CODE,EMPLOYEE_ID" CommandItemDisplay="Top">
                <CommandItemStyle Height="28px" />
                <CommandItemTemplate>
                    <div style="padding: 2px 0 0 0">
                        <div style="float: left">
                            <tlk:RadButton Width="100px" ID="btnEmployee" runat="server" Text="<%$ Translate: Chọn nhân viên %>"
                                CausesValidation="false" CommandName="FindEmployee">
                            </tlk:RadButton>
                        </div>
                        <div style="float: right">
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
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
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
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlChildrenNewEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlChildrenNewEdit_RadPane2';
        var validateID = 'MainContent_ctrlChildrenNewEdit_valSum';
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
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

    </script>
</tlk:RadCodeBlock>
