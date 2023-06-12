<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SETUP_SHOP_GRADE.ascx.vb"
    Inherits="Payroll.ctrlPA_SETUP_SHOP_GRADE" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="235px" Scrolling="None">
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
                            <%# Translate("Doanh thu từ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnFROM_REVENUE">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnFROM_REVENUE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Doanh thu từ. %>" ToolTip="<%$ Translate: Bạn phải nhập Doanh thu từ. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>                   
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại cửa hàng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboType_Shop" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboType_Shop" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn Loại cửa hàng. %>" ToolTip="<%$ Translate: Bạn phải chọn Loại cửa hàng. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Doanh thu đến")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnTO_REVENUE">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnTO_REVENUE"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Doanh thu đến. %>" ToolTip="<%$ Translate: Bạn phải nhập Doanh thu đến. %>"></asp:RequiredFieldValidator>
                        </td>   
                        <asp:CompareValidator ID="compareToRvenue" runat="server" ControlToValidate="rnTO_REVENUE"
                            Type="Double" ControlToCompare="rnFROM_REVENUE" Operator="GreaterThanEqual" ErrorMessage="Doanh thu đến phải lớn hơn doanh thu từ"
                            ToolTip="Doanh thu đến phải lớn hơn doanh thu từ">
                        </asp:CompareValidator>                    
                    </tr>
                     <tr>                       
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                            </tlk:RadDatePicker>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEffectDate" runat="server" 
                                ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Xếp loại")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboGRADE" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboGRADE" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn Xếp loại. %>" ToolTip="<%$ Translate: Bạn phải chọn Xếp loại. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mức hưởng DTTT(<)")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnLessDTTT" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Mức hưởng DTTT(>=)")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnThanDTTT" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Mức hưởng theo DTTĐ")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnBenefit" runat="server">
                            </tlk:RadNumericTextBox>
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
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="BRAND,FROM_REVENVUE,TO_REVENVUE,TYPE_SHOP,GRADE,EFFECT_DATE,NOTE,LESS_DTTT,THAN_DTTT,BENEFIT_VALUE ">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhãn hàng %>" DataField="BRAND_NAME"
                                SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại cửa hàng %>" DataField="TYPE_SHOP_NAME"
                                SortExpression="TYPE_SHOP_NAME" UniqueName="TYPE_SHOP_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Doanh thu từ %>" DataField="FROM_REVENVUE"
                                SortExpression="FROM_REVENVUE" UniqueName="FROM_REVENVUE" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Doanh thu đến %>" DataField="TO_REVENVUE"
                                SortExpression="TO_REVENVUE" UniqueName="TO_REVENVUE" DataFormatString="{0:N0}" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp loại %>" DataField="GRADE_NAME"
                                SortExpression="GRADE_NAME" UniqueName="GRADE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức hưởng DTTT(<) %>" DataField="LESS_DTTT"
                                SortExpression="LESS_DTTT" UniqueName="LESS_DTTT" DataFormatString="{0:N0}"/>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức hưởng DTTT(>=) %>" DataField="THAN_DTTT"
                                SortExpression="THAN_DTTT" UniqueName="THAN_DTTT" DataFormatString="{0:N0}"/>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức hưởng theo DTTĐ %>" DataField="BENEFIT_VALUE"
                                SortExpression="BENEFIT_VALUE" UniqueName="BENEFIT_VALUE" DataFormatString="{0:N0}"/>
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
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
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
            } else if (item.get_commandName() == "NEXT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                //ResizeSplitter();
                ResizeSplitter(splitterID, pane1ID, pane2ID, oldSize, 'rgData');
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
