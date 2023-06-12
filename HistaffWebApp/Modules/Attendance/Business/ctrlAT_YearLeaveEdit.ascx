<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_YearLeaveEdit.ascx.vb"
    Inherits="Attendance.ctrlAT_YearLeaveEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:ValidationSummary ID="valSum" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmpCode" AutoPostBack="true" runat="server" Width="130px">
                                <ClientEvents OnKeyPress="OnKeyPress" /> 
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmpCode"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>"></asp:RequiredFieldValidator>
                            <tlk:RadButton Width="100" ID="btnChooseEmployee" runat="server" CausesValidation="false"
                                SkinID="ButtonView">
                            </tlk:RadButton>
                        </td>
                        <td class="lb">
                            <%# Translate("Họ tên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtEmpName" SkinID="ReadOnly" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đơn vị")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtOrg" SkinID="ReadOnly" runat="server" Width="250px">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Vị trí công việc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTitle" SkinID="ReadOnly" runat="server">
                            </tlk:RadTextBox>
                        </td> 
                         <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                         <td>
                            <tlk:RadNumericTextBox ID="ntxtYear" runat="server" AutoPostBack ="true">
                              <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" DecimalSeparator="." />
                               <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />
                           </tlk:RadNumericTextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ntxtYear"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập năm. %>"></asp:RequiredFieldValidator>
                        </td>    

                        <td class="lb">
                            <%# Translate("Phép năm ĐC")%>
                        </td>         
                        <td>
                            <tlk:RadTextBox ID="txtLeaveNumber" runat="server" SkinID="Decimal">
                            </tlk:RadTextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtLeaveNumber"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Số phép điều chỉnh. %>"></asp:RequiredFieldValidator>
                        </td> 
                         <td class="lb">
                            <%# Translate("Phép năm còn lại")%>
                        </td>         
                        <td>
                            <tlk:RadTextBox ID="txtLeaveHave" runat="server" SkinID="Decimal" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mô tả")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                         <td class="lb">
                            <%# Translate("Phép năm cũ ĐC")%>
                        </td>         
                        <td>
                            <tlk:RadTextBox ID="txtLeaveOld" runat="server" SkinID="Decimal">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgWorkschedule" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_CODE,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,TITLE_NAME,ORG_NAME,NOTE,YEAR,LEAVE_NUMBER,LEAVE_OLD">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME">
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
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR"
                                UniqueName="YEAR" SortExpression="YEAR" DataFormatString="{0:0}">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Phép năm ĐC %>" DataField="LEAVE_NUMBER"
                                UniqueName="LEAVE_NUMBER" SortExpression="LEAVE_NUMBER" DataFormatString="{0:N2}">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Phép năm cũ ĐC %>" DataField="LEAVE_OLD"
                                UniqueName="LEAVE_OLD" SortExpression="LEAVE_OLD" DataFormatString="{0:N2}">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
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
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlAT_YearLeaveEdit_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAT_YearLeaveEdit_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAT_YearLeaveEdit_RadPane2';
        var validateID = 'MainContent_ctrlAT_YearLeaveEdit_valSum';
        var oldSize = $('#' + pane1ID).height();

        var txtBoxName = 'ctl00_MainContent_ctrlAT_YearLeaveEdit_txtEmpName_wrapper';
        var txtBoxTitle = 'ctl00_MainContent_ctrlAT_YearLeaveEdit_txtTitle_wrapper';

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

            if (args.get_item().get_commandName() == 'EXPORT_TEMP') {
                enableAjax = false;
            }

            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    $('#' + txtBoxName + ',' + '#' + txtBoxTitle).css("width", "100%");
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgWorkschedule.ClientID %>");
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
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgWorkschedule', 0, 0, 7);
        }

        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }
        function OnKeyPress(sender, eventArgs) 
        { 
           var c = eventArgs.get_keyCode(); 
           if (c == 13) 
           { 
             document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
