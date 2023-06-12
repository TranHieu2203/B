<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SETUP_FRAMEWORK_OFFICE.ascx.vb"
    Inherits="Payroll.ctrlPA_SETUP_FRAMEWORK_OFFICE" %>
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
                            <%# Translate("Nhãn hàng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboBrand" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqBrand" ControlToValidate="cboBrand" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn nhãn hàng. %>" ToolTip="<%$ Translate: Bạn phải chọn nhãn hàng. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Đối tượng nhân viên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTitle" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboTitle" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn đối tượng nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn đối tượng nhân viên. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("TLHT CTDT(%) từ (>=)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnFROM_RATE">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnFROM_RATE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLHT CTDT(%) từ (>=). %>" ToolTip="<%$ Translate: Bạn phải nhập TLHT CTDT(%) từ (>=). %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("TLHT CTDT(%) đến (<)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnTO_RATE">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnTO_RATE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLHT KPI(%) đến (<). %>" ToolTip="<%$ Translate: Bạn phải nhập TLHT KPI(%) đến (<). %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                         <td class="lb">
                            <%# Translate("CTDT từ (>=)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnFROM_TARGET">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnFROM_TARGET"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập CTDT từ (>=). %>" ToolTip="<%$ Translate: Bạn phải nhập CTDT từ (>=). %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("CTDT đến (<)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnTO_TARGET">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rnTO_TARGET"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập CTDT đến (<). %>" ToolTip="<%$ Translate: Bạn phải nhập CTDT đến (<). %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                         <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEffectDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("LDT chuẩn")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnStandard_Sales">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rnStandard_Sales"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập LDT chuẩn. %>" ToolTip="<%$ Translate: Bạn phải nhập LDT chuẩn. %>"></asp:RequiredFieldValidator>
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
                     <MasterTableView DataKeyNames="ID" ClientDataKeyNames="BRAND,TITLE_ID,FROM_RATE,TO_RATE,FROM_TARGET,TO_TARGET, EFFECT_DATE,STANDARD_SALES, NOTE ">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhãn hàng %>" DataField="BRAND_NAME"
                                SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng nhân viên %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: TLHT KPI(%) từ (>=) %>" DataField="FROM_RATE"
                                SortExpression="FROM_RATE" UniqueName="FROM_RATE" DataFormatString="{0:n0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: TLHT KPI(%) đến (<) %>" DataField="TO_RATE"
                                SortExpression="TO_RATE" UniqueName="TO_RATE" DataFormatString="{0:n0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: CTDT từ (>=) %>" DataField="FROM_TARGET"
                                SortExpression="FROM_TARGET" UniqueName="FROM_TARGET" DataFormatString="{0:n0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: CTDT đến (<) %>" DataField="TO_TARGET"
                                SortExpression="TO_TARGET" UniqueName="TO_TARGET" DataFormatString="{0:n0}"/>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: LDT chuẩn %>" DataField="STANDARD_SALES"  ItemStyle-HorizontalAlign="Center"
                                SortExpression="STANDARD_SALES" UniqueName="STANDARD_SALES" DataFormatString="{0:n0}" />
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
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_OFFICE_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_OFFICE_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SETUP_FRAMEWORK_OFFICE_RadPane2';
        var validateID = 'MainContent_ctrlPA_SETUP_FRAMEWORK_OFFICE_ValidationSummary1';
        var enableAjax = true;
        var oldSize = $('#' + pane1ID).height();
        //        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
            }
            else if (item.get_commandName() == "SAVE") {
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
