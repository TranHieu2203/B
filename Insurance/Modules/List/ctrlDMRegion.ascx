<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMRegion.ascx.vb"
    Inherits="Insurance.ctrlDMRegion" %>
     <%@ Import Namespace="Common" %>
    <link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="120px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Vùng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="ddlAREA_ID" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlAREA_ID"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn vùng. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="txtEFFECTIVE_DATE" DateInput-DateFormat="dd/MM/yyyy"
                        runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEFFECTIVE_DATE"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <%# Translate("Mức lương tối thiểu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtMIN_AMOUNT" MaxLength="10" MinValue="0" runat="server">
                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMIN_AMOUNT"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Mức lương tối thiểu. %>"></asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <%# Translate("Mức trần BHTN")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtCEILING_AMOUNT" MaxLength="10" MinValue="0" runat="server">
                        <NumberFormat GroupSeparator="," DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCEILING_AMOUNT"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Mức trần BHTN. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" runat="server" Width="100%" Height = "40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="AREA_ID,AREA_NAME,NOTE,EFFECTIVE_DATE,CEILING_AMOUNT,MIN_AMOUNT">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="AREA_ID" UniqueName="AREA_ID" Visible="false"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên vùng %>" DataField="AREA_NAME"
                        UniqueName="AREA_NAME" SortExpression="AREA_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataFormatString="{0:dd/MM/yyyy}"
                        DataField="EFFECTIVE_DATE" UniqueName="EFFECTIVE_DATE" SortExpression="EFFECTIVE_DATE" CurrentFilterFunction="EqualTo" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức lương tối thiểu %>" DataField="MIN_AMOUNT"
                        UniqueName="MIN_AMOUNT" SortExpression="MIN_AMOUNT"  DataFormatString="{0:N0}"/>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức trần BHTN %>" DataField="CEILING_AMOUNT"
                        UniqueName="CEILING_AMOUNT" SortExpression="CEILING_AMOUNT"  DataFormatString="{0:N0}"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlDMRegion_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMRegion_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMRegion_RadPane2';
        var validateID = 'MainContent_ctrlDMRegion_valSum';
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
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                var grid = $find("<%=rgDanhMuc.ClientID %>");
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
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
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
