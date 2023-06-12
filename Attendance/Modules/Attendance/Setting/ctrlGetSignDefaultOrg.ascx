<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlGetSignDefaultOrg.ascx.vb"
    Inherits="Attendance.ctrlGetSignDefaultOrg" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    .AutoHeight
    {
        height: auto !important;
    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <%--<tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>--%>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RSMain" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RPTbar" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RPItem" runat="server" Scrolling="None" CssClass="AutoHeight">
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cbYear" />
                        </td>
                        <td class="lb">
                            <%# Translate("Lịch làm việc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtCalendar" />
                        </td>
                        <td class="lb">
                            <%# Translate("Ca Thứ 2")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSign" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSign" ControlToValidate="cboSign" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca Thứ 2. %>" ToolTip="<%$ Translate: Bạn phải chọn ca Thứ 2. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca Thứ 3")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignTue" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignTue" ControlToValidate="cboSignTue" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca Thứ 3. %>" ToolTip="<%$ Translate: Bạn phải chọn ca Thứ 3. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca Chủ nhật")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignSun" runat="server">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqSignSun" ControlToValidate="cboSignSun" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca Chủ nhật %>" ToolTip="<%$ Translate: Bạn phải chọn ca Chủ nhật. %>"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Đơn vị")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" AutoPostBack="true">
                                <ClientEvents OnKeyPress="OnKeyPress" />
                            </tlk:RadTextBox>
                            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label2" Text="Nơi làm việc"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cbWorkPlace">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca Thứ 4")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignWed" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignWed" ControlToValidate="cboSignWed" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca Thứ 4. %>" ToolTip="<%$ Translate: Bạn phải chọn ca Thứ 4. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca Thứ 5")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignThu" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignThu" ControlToValidate="cboSignThu" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca Thứ 5. %>" ToolTip="<%$ Translate: Bạn phải chọn ca Thứ 5. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label10" Text="Đối tượng nhân viên"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="rcOBJECT_EMPLOYEE">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbObject" Text="Đối tượng công"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboObject">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca Thứ 6")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignFri" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqSignFri" ControlToValidate="cboSignFri" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca Thứ 6. %>" ToolTip="<%$ Translate: Bạn phải chọn ca Thứ 6. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ca Thứ 7")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboSignSat" runat="server">
                            </tlk:RadComboBox>
                            <%--<asp:RequiredFieldValidator ID="reqSignSat" ControlToValidate="cboSignSat" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn ca Thứ 7 %>" ToolTip="<%$ Translate: Bạn phải chọn ca Thứ 7. %>"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực từ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqFromDate" ControlToValidate="rdFromDate" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực đến")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                                ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                                Type="Date" Operator="GreaterThan" ControlToCompare="rdFromDate" ControlToValidate="rdToDate">
                            </asp:CompareValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Mô tả")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RPRGrid" runat="server" Scrolling="None" Style="max-height: 100%;
                width: 100%">
                <tlk:RadGrid PageSize="50" ID="rgSignDefaultOrg" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,ORG_ID,ORG_NAME,EFFECT_DATE_FROM,EFFECT_DATE_TO,SIGN_MON,SIGN_TUE,SIGN_WED,SIGN_THU,SIGN_FRI,SIGN_SAT,SIGN_SUN,NOTE,WORKPLACE_ID,OBJ_ATTENDANT_ID,OBJ_EMPLOYEE_ID,YEAR,CALENDAR">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                        SortExpression="ORG_NAME">
                                        <HeaderStyle Width="200px" />
                                        <ItemStyle Width="200px" />
                                    </tlk:GridBoundColumn>--%>
                            <%--<tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                    </asp:Label>
                                    <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                        RelativeTo="Element" Position="BottomCenter">
                                        <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                    </tlk:RadToolTip>
                                </ItemTemplate>
                            </tlk:GridTemplateColumn>
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Nơi làm việc%>" DataField="WORKPLACE_NAME"
                                UniqueName="WORKPLACE_NAME" SortExpression="WORKPLACE_NAME" />
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng nhân viên%>" DataField="OBJ_EMPLOYEE_NAME"
                                UniqueName="OBJ_EMPLOYEE_NAME" SortExpression="OBJ_EMPLOYEE_NAME" />
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng chấm công%>" DataField="OBJ_ATTENDANT_NAME"
                                UniqueName="OBJ_ATTENDANT_NAME" SortExpression="OBJ_ATTENDANT_NAME" />
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T2%>" DataField="SIGN_MON_NAME"
                                UniqueName="SIGN_MON_NAME" SortExpression="SIGN_MON_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T3%>" DataField="SIGN_TUE_NAME"
                                UniqueName="SIGN_TUE_NAME" SortExpression="SIGN_TUE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T4%>" DataField="SIGN_WED_NAME"
                                UniqueName="SIGN_WED_NAME" SortExpression="SIGN_WED_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T5%>" DataField="SIGN_THU_NAME"
                                UniqueName="SIGN_THU_NAME" SortExpression="SIGN_THU_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca T6%>" DataField="SIGN_FRI_NAME"
                                UniqueName="SIGN_FRI_NAME" SortExpression="SIGN_FRI_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca  T7%>" DataField="SIGN_SAT_NAME"
                                UniqueName="SIGN_SAT_NAME" SortExpression="SIGN_SAT_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ca CN%>" DataField="SIGN_SUN_NAME"
                                UniqueName="SIGN_SUN_NAME" SortExpression="SIGN_SUN_NAME" />

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực từ %>" DataField="EFFECT_DATE_FROM"
                                UniqueName="EFFECT_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE_FROM">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực đến %>" DataField="EFFECT_DATE_TO"
                                UniqueName="EFFECT_DATE_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE_TO">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>

                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                                UniqueName="ACTFLG" SortExpression="ACTFLG">
                                <HeaderStyle Width="70px" />
                            </tlk:GridBoundColumn>--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridBoundColumn>--%>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="1" />
                    </ClientSettings>
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlGetSignDefaultOrg_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlGetSignDefaultOrg_RPItem';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlGetSignDefaultOrg_RPRGrid';
        var validateID = 'MainContent_ctrlGetSignDefaultOrg_valSum';
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

        $(document).ready(function () {
            ResizeRadGrid();
        });
        $(window).resize(function () {
            ResizeRadGrid();
        });
        function ResizeRadGrid() {
            debugger;
            var splitter = $find("<%= RSMain.ClientID%>");
            var pane2 = splitter.getPaneById('<%= RPTbar.ClientID%>');
            var pane3 = splitter.getPaneById('<%= RPItem.ClientID%>');
            var pane = splitter.getPaneById('<%= RPRGrid.ClientID%>');
            var height = pane.getContentElement().scrollHeight;
            pane.set_height(splitter._element.clientHeight - pane3._element.clientHeight - 50);
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            ResizeRadGrid();
        }
        function EndRequestHandler(sender, args) {
            ResizeRadGrid();
        }
        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'SAVE') {

            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgSignDefaultOrg.ClientID%>");
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
            }
            ResizeRadGrid();
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgSignDefaultOrg', 0, 0, 7);
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlGetSignDefaultOrg_rdFromDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlGetSignDefaultOrg_rdToDate_dateInput').val('');
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlGetSignDefaultOrg_txtOrgName') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
