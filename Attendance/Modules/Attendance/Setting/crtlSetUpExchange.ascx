<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="crtlSetUpExchange.ascx.vb"
    Inherits="Attendance.crtlSetUpExchange" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
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
                        <td>
                            <%# Translate("Tên công ty")%>
                        </td>
                        <td>
                            <tlk:RadTextBox runat="server" ID="txtCongty">
                            </tlk:RadTextBox>
                        </td>
                        <td>
                            <%# Translate("Ngày hiệu lực")%>
                            <span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker runat="server" ID="rdEffectDate">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdEffectDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy chọn ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <%# Translate("Đối tượng chấm công")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboObjectAttendace">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# Translate("Loại")%>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboType">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Từ (phút)")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rtxtFromMinute" runat="server" SkinID="Number" NumberFormat-GroupSeparator=""
                                ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Đến (phút)")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rtxtToMinute" runat="server" SkinID="Number" NumberFormat-GroupSeparator=""
                                ShowSpinButtons="true" NumberFormat-DecimalDigits="0">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Số ngày công bị trừ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rtxtDateDeducted" runat="server">
                                <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1"
                                    DecimalSeparator="." />
                                <ClientEvents OnBlur="displayDecimalFormat" OnLoad="displayDecimalFormat" OnValueChanged="displayDecimalFormat" />
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Số thứ tự")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rtxtSTT" runat="server">
                            </tlk:RadNumericTextBox>
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
                    <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ORG_NAME,ORG_ID,EFFECT_DATE,OBJECT_ATTENDACE,OBJECT_ATTENDACE_NAME,TYPE_EXCHANGE,TYPE_EXCHANGE_NAME,FROM_MINUTE,TO_MINUTE,NUMBER_DATE,STT">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate:Tên công ty%>" DataField="ORG_NAME"
                                UniqueName="ORG_NAME" SortExpression="ORG_NAME">
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực%>" DataField="EFFECT_DATE"
                                UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng chấm công%>" DataField="OBJECT_ATTENDACE_NAME"
                                UniqueName="OBJECT_ATTENDACE_NAME" SortExpression="OBJECT_ATTENDACE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại%>" DataField="TYPE_EXCHANGE_NAME"
                                UniqueName="TYPE_EXCHANGE_NAME" SortExpression="TYPE_EXCHANGE_NAME">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ phút %>" DataField="FROM_MINUTE"
                                UniqueName="FROM_MINUTE" SortExpression="FROM_MINUTE">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến phút%>" DataField="TO_MINUTE"
                                UniqueName="TO_MINUTE" SortExpression="TO_MINUTE">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày%>" DataField="NUMBER_DATE"
                                UniqueName="NUMBER_DATE" SortExpression="NUMBER_DATE">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số thứ tự%>" DataField="STT" UniqueName="STT"
                                SortExpression="STT">
                            </tlk:GridNumericColumn>
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
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        var splitterID = 'ctl00_MainContent_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlSetUpExchange_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlSetUpExchange_RadPane2';
        var validateID = 'MainContent_crtlSetUpExchange_valSum';
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
        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }

    </script>
</tlk:RadCodeBlock>
