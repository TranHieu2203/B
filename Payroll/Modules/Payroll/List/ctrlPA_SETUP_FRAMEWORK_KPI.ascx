<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SETUP_FRAMEWORK_KPI.ascx.vb"
    Inherits="Payroll.ctrlPA_SETUP_FRAMEWORK_KPI" %>
<%@ Import Namespace="Framework.UI" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="195px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryTypes" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Nhãn hàng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboBrand" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqBrand" ControlToValidate="cboBrand" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn nhóm Nhãn hàng. %>" ToolTip="<%$ Translate: Bạn phải chọn nhóm Nhãn hàng. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đối tượng nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboGroupTitle" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboGroupTitle" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn Đối tượng nhân viên. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Loại chỉ số")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboIndexType" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqIndexType" ControlToValidate="cboIndexType" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Loại chỉ số. %>" ToolTip="<%$ Translate: Bạn phải chọn Loại chỉ số. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("% TLHT từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnFROM_RATE" CausesValidation="false" AutoPostBack="False">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnFROM_RATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập % TLHT từ. %>" ToolTip="<%$ Translate: Bạn phải nhập % TLHT từ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("% TLHT đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnTO_RATE" CausesValidation="false" AutoPostBack="False">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnTO_RATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập % TLHT đến. %>" ToolTip="<%$ Translate: Bạn phải nhập % TLHT đến. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hệ số KPI")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnFROM_AVG_SALE" MaxLength="255" runat="server" SkinID="MoneyEN">
                        <NumberFormat AllowRounding="true" DecimalDigits="2" KeepNotRoundedValue="true" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnFROM_AVG_SALE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Hệ số KPI. %>" ToolTip="<%$ Translate: Bạn phải nhập Hệ số KPI. %>"></asp:RequiredFieldValidator>

                </td>
                <%--<td>
                            <tlk:RadNumericTextBox runat="server" ID="rnFROM_AVG_SALE" CausesValidation="false" AutoPostBack="True" NumberFormat-DecimalDigits="2">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnFROM_AVG_SALE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Hệ số KPI. %>" ToolTip="<%$ Translate: Bạn phải nhập Hệ số KPI. %>"></asp:RequiredFieldValidator>
                        </td>--%>
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
            <MasterTableView DataKeyNames="ID,FROM_RATE,TO_RATE,KPI_FACTOR,EMPLOYEE_OBJECT_ID,EMPLOYEE_OBJECT_NAME, EFFECT_DATE, NOTE, BRAND_NAME, BRAND_ID, INDEX_TYPE_NAME, INDEX_TYPE_ID"
                ClientDataKeyNames="ID,FROM_RATE,TO_RATE,KPI_FACTOR,EMPLOYEE_OBJECT_ID,EMPLOYEE_OBJECT_NAME, EFFECT_DATE, NOTE, BRAND_NAME, BRAND_ID, INDEX_TYPE_NAME, INDEX_TYPE_ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhãn hàng%>" DataField="BRAND_NAME"
                        SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng nhân viên%>" DataField="EMPLOYEE_OBJECT_NAME"
                        SortExpression="EMPLOYEE_OBJECT_NAME" UniqueName="EMPLOYEE_OBJECT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại chỉ số%>" DataField="INDEX_TYPE_NAME"
                        SortExpression="INDEX_TYPE_NAME" UniqueName="INDEX_TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: % TLDT từ%>" DataField="FROM_RATE"
                        SortExpression="FROM_RATE" UniqueName="FROM_RATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: % TLDT đến %>" DataField="TO_RATE"
                        SortExpression="TO_RATE" UniqueName="TO_RATE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số KPI %>" DataField="KPI_FACTOR"
                        SortExpression="KPI_FACTOR" UniqueName="KPI_FACTOR" DataFormatString="{0:0.00}" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" ItemStyle-HorizontalAlign="Center"
                        DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" SortExpression="NOTE"
                        UniqueName="NOTE" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<common:ctrlupload id="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_KPI_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_KPI_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_KPI_RadPane2';
        var validateID = 'MainContent_ctrlPA_SETUP_FRAMEWORK_KPI_valSum';
        var enableAjax = true;
        var oldSize = $('#' + pane1ID).height();
        //        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }

            if (args.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }
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
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


    </script>
</tlk:RadCodeBlock>
