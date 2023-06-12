<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="crtlATCoeffOTException.ascx.vb"
    Inherits="Attendance.crtlATCoeffOTException" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="185px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Mã hệ số OT")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtException_Coeff_Code" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtException_Coeff_Code"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập Mã hệ số OT. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Hệ số OT")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadNumericTextBox ID="rtxtException_Coeff" runat="server" Width="100%" SkinID="Decimal">
                                <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rtxtException_Coeff"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập hệ số OT. %>"></asp:RequiredFieldValidator>
                        </>
                    </tr>

                    <tr>
                        <td class="lb">
                            <%# Translate("Hệ số chịu thuế")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rtxtException_Coeff_PIT" runat="server" MinValue="0" SkinID="Decimal">
                                <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                            </tlk:RadNumericTextBox>

                        </td>
                        <td class="lb">
                            <%# Translate("Hệ số không chịu thuế")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadNumericTextBox ID="rtxtException_Coeff_NonPIT" runat="server" MinValue="0" Width="100%" SkinID="Decimal">
                                <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                            </tlk:RadNumericTextBox>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ntxtToDate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn hãy nhập Đến ngày. %>"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb"><%# Translate("Ghi chú")%></td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtNote" runat="server" Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb"><%# Translate("Tự động sinh")%></td>
                        <td>
                            <asp:CheckBox ID="chkAutoGen" runat="server" />
                        </td>
                        <td class="lb"><%# Translate("Ẩn thông tin")%></td>
                        <td>
                            <asp:CheckBox ID="chkHide" runat="server" />
                        </td>
                        <td class="lb"><%# Translate("Hệ số bù")%></td>
                        <td>
                            <asp:CheckBox ID="chkIsBu" runat="server" />
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgData" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EXCEPTION_COEFF,EXCEPTION_COEFF_PIT,EXCEPTION_COEFF_NONPIT,NOTE,EXCEPTION_COEFF_CODE,AUTOGEN,HIDE,ORDERBY,IS_BU">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã hệ số OT%>" DataField="EXCEPTION_COEFF_CODE"
                                UniqueName="EXCEPTION_COEFF_CODE" SortExpression="EXCEPTION_COEFF_CODE" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số OT%>" DataField="EXCEPTION_COEFF"
                                UniqueName="EXCEPTION_COEFF" SortExpression="EXCEPTION_COEFF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số chịu thuế%>" DataField="EXCEPTION_COEFF_PIT"
                                UniqueName="EXCEPTION_COEFF_PIT" SortExpression="EXCEPTION_COEFF_PIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số không chịu thuế %>" DataField="EXCEPTION_COEFF_NONPIT"
                                UniqueName="EXCEPTION_COEFF_NONPIT" SortExpression="EXCEPTION_COEFF_NONPIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridNumericColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú%>" DataField="NOTE" UniqueName="NOTE"
                                SortExpression="NOTE" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái%>" DataField="ACTFLG" UniqueName="ACTFLG"
                                SortExpression="ACTFLG" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridBoundColumn>
                            <tlk:GridCheckBoxColumn HeaderText="Tự động sinh" AllowFiltering="false"
                                DataField="AUTOGEN" SortExpression="AUTOGEN" UniqueName="AUTOGEN" />
                            <tlk:GridCheckBoxColumn HeaderText="Ẩn dữ liệu" AllowFiltering="false"
                                DataField="HIDE" SortExpression="HIDE" UniqueName="HIDE" />
                            <tlk:GridCheckBoxColumn HeaderText="Hệ số bù" AllowFiltering="false"
                                DataField="IS_BU" SortExpression="IS_BU" UniqueName="IS_BU" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;

        var splitterID = 'ctl00_MainContent_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlATTimePeriod_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_crtlATTimePeriod_RadPane2';
        var validateID = 'MainContent_crtlATTimePeriod_valSum';
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
                var rows = $find('<%= rgData.ClientID %>').get_masterTableView().get_dataItems().length;
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
        function displayDecimalFormat(sender, args) {
            sender.set_textBoxValue(sender.get_value().toString());
        }
        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value());
        }
    </script>
</tlk:RadCodeBlock>
