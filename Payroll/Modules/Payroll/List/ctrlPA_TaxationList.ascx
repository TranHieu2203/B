<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_TaxationList.ascx.vb"
    Inherits="Payroll.ctrlPA_TaxationList" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valsum" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Đối tượng cư trú")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcboResident" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmValueFrom" runat="server" Width="150px">
                    </tlk:RadNumericTextBox>
                    <%# Translate("VNĐ")%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnmValueFrom"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập từ VNĐ. %>" ToolTip="<%$ Translate: Bạn phải nhập từ VNĐ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("Đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmValueTo" runat="server" Width="130px">
                    </tlk:RadNumericTextBox>
                    <%# Translate("VNĐ")%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnmValueTo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập đến VNĐ. %>" ToolTip="<%$ Translate: Bạn phải nhập đến VNĐ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("Tỉ lệ (%)")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmRate" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rnmRate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tỉ lệ (%). %>" ToolTip="<%$ Translate: Bạn phải nhập tỉ lệ (%). %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trừ nhanh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmExceptFast" runat="server" Width="150px">
                    </tlk:RadNumericTextBox>
                    <%# Translate("VNĐ")%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rnmExceptFast"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập trừ nhanh. %>" ToolTip="<%$ Translate: Bạn phải nhập trừ nhanh. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdpEffectDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td style="display: none">
                    <tlk:RadDatePicker ID="rdpExpireDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb" id="tdSGlbOrders">
                    <label id="lbOrders">
                        <%# Translate("Thứ tự")%></label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="1" Value="1" ShowSpinButtons="True"
                        CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="rtxtDesc" runat="server" SkinID="Textbox1023" Width="100%" Height = "40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true" OnPreRender ="rgData_PreRender">
            <ClientSettings EnablePostBackOnRowClick="True">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID, RESIDENT_ID, VALUE_FROM, VALUE_TO, RATE, EXCEPT_FAST, FROM_DATE, TO_DATE, ACTFLG, SDESC,ORDERS">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng cơ trú %>" DataField="RESIDENT_NAMEVN"
                        SortExpression="RESIDENT_NAMEVN" UniqueName="RESIDENT_NAMEVN" Display="False" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Từ %>" DataField="VALUE_FROM" SortExpression="VALUE_FROM"
                        UniqueName="VALUE_FROM" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Đến %>" DataField="VALUE_TO" SortExpression="VALUE_TO"
                        UniqueName="VALUE_TO" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tỉ lệ (%) %>" DataField="RATE"
                        SortExpression="RATE" UniqueName="RATE" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Trừ nhanh %>" DataField="EXCEPT_FAST"
                        SortExpression="EXCEPT_FAST" UniqueName="EXCEPT_FAST" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="FROM_DATE"
                        UniqueName="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="FROM_DATE" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="TO_DATE"
                        UniqueName="TO_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="TO_DATE"
                        Display="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="SDESC" SortExpression="SDESC"
                        UniqueName="SDESC" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" HeaderStyle-Width="120px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlPA_TaxationList_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_TaxationList_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_TaxationList_RadPane2';
        var validateID = 'MainContent_ctrlPA_TaxationList_valsum';
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
            if (item.get_commandName() == "EXPORT") {
                var grid = $find("<%=rgData.ClientID %>");
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
            } else if (item.get_commandName() == "SAVE") {
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
