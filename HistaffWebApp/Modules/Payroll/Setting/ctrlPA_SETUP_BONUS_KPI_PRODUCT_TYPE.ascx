<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SETUP_BONUS_KPI_PRODUCT_TYPE.ascx.vb"
    Inherits="Payroll.ctrlPA_SETUP_BONUS_KPI_PRODUCT_TYPE" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">    
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="220px">
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
                            <tlk:RadComboBox ID="cboBrand" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqBrand" ControlToValidate="cboBrand" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn nhãn hàng. %>" ToolTip="<%$ Translate: Bạn phải chọn nhãn hàng. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại cửa hàng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboTypeShop" runat="server">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboTypeShop" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn loại cửa hàng. %>" ToolTip="<%$ Translate: Bạn phải chọn loại cửa hàng. %>">
                            </asp:RequiredFieldValidator>
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
                            <%# Translate("TLHT KPI (%) từ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnFromRate"  CausesValidation="false">                               
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnFromRate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLHT KPI (%) từ. %>" ToolTip="<%$ Translate: Bạn phải nhập TLHT KPI (%) từ. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("TLT (sản phẩm NG)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnNG"  CausesValidation="false" MinValue="0" MaxValue="100" SkinID="Decimal">                               
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnNG"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLT (sản phẩm NG). %>" ToolTip="<%$ Translate: Bạn phải nhập TLT (sản phẩm NG). %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("TLHT KPI (%) đến")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnToRate"  CausesValidation="false">                               
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rnToRate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLHT KPI (%) đến. %>" ToolTip="<%$ Translate: Bạn phải nhập TLHT KPI (%) đến. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("TLT (sản phẩm KNG1)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnKNG1"  CausesValidation="false" MinValue="0" MaxValue="100" SkinID="Decimal">                               
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rnKNG1"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLT (sản phẩm KNG1). %>" ToolTip="<%$ Translate: Bạn phải nhập TLT (sản phẩm KNG1). %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mức hoàn thành")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboCompleteLv" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="cboCompleteLv" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn Mức hoàn thành. %>" ToolTip="<%$ Translate: Bạn phải chọn Mức hoàn thành. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("TLT (sản phẩm KNG2)")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnKNG2"  CausesValidation="false" MinValue="0" MaxValue="100" SkinID="Decimal">                               
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rnKNG2"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập TLT (sản phẩm KNG2). %>" ToolTip="<%$ Translate: Bạn phải nhập TLT (sản phẩm KNG2). %>"></asp:RequiredFieldValidator>
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
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="BRAND, EFFECT_DATE, NOTE,FROM_RATE,TO_RATE,NG,NG1,NG2,COMPLETE_LV,TYPE_SHOP">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhãn hàng %>" DataField="BRAND_NAME"
                                SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại cửa hàng %>" DataField="TYPE_SHOP_NAME"
                                SortExpression="TYPE_SHOP_NAME" UniqueName="TYPE_SHOP_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE">
                            </tlk:GridNumericColumn>                        
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉ lệ hoàn thành KPI (%) từ %>" DataField="FROM_RATE"
                                SortExpression="FROM_RATE" UniqueName="FROM_RATE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỉ lệ hoàn thành KPI (%) đến %>" DataField="TO_RATE" 
                                SortExpression="TO_RATE" UniqueName="TO_RATE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mức hoàn thành %>" DataField="COMPLETE_LV_NAME"
                                SortExpression="COMPLETE_LV_NAME" UniqueName="COMPLETE_LV_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ lệ thưởng (sản phẩm NG) %>" DataField="NG"
                                SortExpression="NG" UniqueName="NG" />   
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ lệ thưởng (sản phẩm KNG1) %>" DataField="NG1"
                                SortExpression="NG1" UniqueName="NG1" />  
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ lệ thưởng (sản phẩm KNG2) %>" DataField="NG2"
                                SortExpression="NG2" UniqueName="NG2" />  
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
        var splitterID = 'ctl00_MainContent_ctrlPA_Period_RadSplitter1';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Period_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Period_RadPane2';
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
                ResizeSplitter(splitterID, pane1ID, pane2ID, oldSize, 'rgData');
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
                enableAjax = false;
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
