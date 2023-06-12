<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SETUP_COMPLETION_KPI_SHOPMANAGER.ascx.vb"
    Inherits="Payroll.ctrlPA_SETUP_COMPLETION_KPI_SHOPMANAGER" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="185px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryTypes" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Nhãn hàng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboBrand" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqBrand" ControlToValidate="cboBrand" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn nhãn hàng. %>" ToolTip="<%$ Translate: Bạn phải chọn nhãn hàng. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("TLHT KPI(%) từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnFROM_COMPLETION_RATE">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnFROM_COMPLETION_RATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLHT KPI(%) từ. %>" ToolTip="<%$ Translate: Bạn phải nhập TLHT KPI(%) từ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEffectDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("TLHT KPI(%) đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnTO_COMPLETION_RATE">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnTO_COMPLETION_RATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLHT KPI(%) đến. %>" ToolTip="<%$ Translate: Bạn phải nhập TLHT KPI(%) đến. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số LDT")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnFACTOR" SkinID="Decimal">
                        <NumberFormat DecimalDigits="2" AllowRounding="true" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnFACTOR"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Hệ số LDT. %>" ToolTip="<%$ Translate: Bạn phải nhập Hệ số LDT. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNOTE" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="BRAND,FROM_COMPLETION_RATE,TO_COMPLETION_RATE, EFFECT_DATE,FACTOR, NOTE ">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhãn hàng %>" DataField="BRAND_NAME"
                        SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: TLHT KPI(%) từ %>" DataField="FROM_COMPLETION_RATE"
                        SortExpression="FROM_COMPLETION_RATE" UniqueName="FROM_COMPLETION_RATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: TLHT KPI(%) đến %>" DataField="TO_COMPLETION_RATE"
                        SortExpression="TO_COMPLETION_RATE" UniqueName="TO_COMPLETION_RATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số LDT %>" DataField="FACTOR"
                        SortExpression="FACTOR" UniqueName="FACTOR"  DataFormatString="{0:N2}"/>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" ItemStyle-HorizontalAlign="Center"
                        DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" SortExpression="NOTE"
                        UniqueName="NOTE" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPA_SETUP_COMPLETION_KPI_SHOPMANAGER_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SETUP_COMPLETION_KPI_SHOPMANAGER_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SETUP_COMPLETION_KPI_SHOPMANAGER_RadPane2';
        var validateID = 'MainContent_ctrlPA_SETUP_COMPLETION_KPI_SHOPMANAGER_ValidationSummary1';
        var enableAjax = true;
        var oldSize = $('#' + pane1ID).height();
        //        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                //ResizeSplitter();
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
        }

    </script>
</tlk:RadCodeBlock>
