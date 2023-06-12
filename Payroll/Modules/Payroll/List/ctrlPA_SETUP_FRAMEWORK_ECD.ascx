<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SETUP_FRAMEWORK_ECD.ascx.vb"
    Inherits="Payroll.ctrlPA_SETUP_FRAMEWORK_ECD" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="195px" Scrolling="None">
                <tlk:RadToolBar ID="tbarSalaryTypes" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
                    CssClass="validationsummary" />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Nhãn hàng")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboBrand" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Nhóm nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboGroupEmp" runat="server">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("DT trung bình từ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnFROM_AVG_SALE">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnFROM_AVG_SALE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Hệ số LDT. %>" ToolTip="<%$ Translate: Bạn phải nhập Hệ số LDT. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("DT trung bình đến")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnTO_AVG_SALE">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rnTO_AVG_SALE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Hệ số LDT. %>" ToolTip="<%$ Translate: Bạn phải nhập Hệ số LDT. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("TLDT(%) từ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnFROM_RATE">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnFROM_RATE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLHT CTDT(%) từ. %>" ToolTip="<%$ Translate: Bạn phải nhập TLHT CTDT(%) từ. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("TLDT(%) đến")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnTO_RATE">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnTO_RATE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLHT KPI(%) đến. %>" ToolTip="<%$ Translate: Bạn phải nhập TLHT KPI(%) đến. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        
                        <td class="lb">
                            <%# Translate("LDT chuẩn")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnLDTC" >
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rnLDTC"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập LDT chuẩn. %>" ToolTip="<%$ Translate: Bạn phải nhập LDT chuẩn. %>"></asp:RequiredFieldValidator>
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
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="FROM_RATE,TO_RATE,FROM_AVG_SALE,TO_AVG_SALE, EFFECT_DATE, NOTE,BRAND,LDTC,GROUP_EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhãn hàng %>" DataField="BRAND_NAME"
                                SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm nhân viên %>" DataField="GROUP_EMPLOYEE_NAME"
                                SortExpression="GROUP_EMPLOYEE_NAME" UniqueName="GROUP_EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: TLDT(%) từ%>" DataField="FROM_RATE"
                                SortExpression="FROM_RATE" UniqueName="FROM_RATE"  DataFormatString="{0:n2}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: TLDT(%) đến %>" DataField="TO_RATE"
                                SortExpression="TO_RATE" UniqueName="TO_RATE"  DataFormatString="{0:n2}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: DT trung bình từ %>" DataField="FROM_AVG_SALE"
                                SortExpression="FROM_AVG_SALE" UniqueName="FROM_AVG_SALE"  DataFormatString="{0:n2}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: DT trung bình đến %>" DataField="TO_AVG_SALE"
                                SortExpression="TO_AVG_SALE" UniqueName="TO_AVG_SALE"  DataFormatString="{0:n2}" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE">
                            </tlk:GridDateTimeColumn>
                            
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: LDT chuẩn %>" DataField="LDTC"
                                SortExpression="LDTC" UniqueName="LDTC" DataFormatString="{0:n2}" />

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
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_ECD_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_ECD_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_ECD_RadPane2';
        var validateID = 'MainContent_ctrlPA_SETUP_FRAMEWORK_ECD_ValidationSummary1';
        var enableAjax = true;
        var oldSize = $('#' + pane1ID).height();
        //        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT" || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                //ResizeSplitter();
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
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


    </script>
</tlk:RadCodeBlock>
