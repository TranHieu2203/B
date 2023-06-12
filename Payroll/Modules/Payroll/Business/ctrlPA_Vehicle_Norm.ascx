<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Vehicle_Norm.ascx.vb"
    Inherits="Payroll.ctrlPA_Vehicle_Norm" %>
<%@ Import Namespace="Framework.UI" %>
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="270px" Scrolling="None">
        <tlk:RadToolBar ID="tbMain" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
       <table class="table-form">
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin thêm mới/ Chỉnh sửa")%>
                    <hr />
                </td>
            </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="lblvehicleName" Text="Cửa hàng"></asp:Label><span class="lbReq">*</span>
                </td>
               <td>
                    <tlk:RadTextBox ID="txtVehicleName" runat="server" ReadOnly="True" AutoPostBack="true" Width="130px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqOrgSetName" ControlToValidate="txtVehicleName"
                        runat="server" ErrorMessage="Bạn phải chọn cửa hàng " ToolTip="Bạn phải chọn cửa hàng"> 
                    </asp:RequiredFieldValidator>
                    <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false" />
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lblCode_Vehicle" Text="Mã cửa hàng"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode_Vehicle" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                
                <td class="lb">
                    <asp:Label runat="server" ID="lblCostCenter" Text="CostCenter"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCostCenter" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lblVehicle_type" Text="Loại cửa hàng"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboVehicle_type" runat="server" Width="130px" >
                    </tlk:RadComboBox>
                    
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lblEffect_month" Text="Tháng hiệu lục"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadMonthYearPicker runat="server" ID="rdEffect_month" TabIndex="4" Culture="en-US">
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="MM/yyyy">
                        </DateInput>
                    </tlk:RadMonthYearPicker>
                    <asp:RequiredFieldValidator ID="reqEffect_month" ControlToValidate="rdEffect_month"
                        runat="server" ErrorMessage="Bạn phải chọn tháng hiệu lực" ToolTip="Bạn phải chọn tháng hiệu lực"> 
                    </asp:RequiredFieldValidator>
                </td>
                 <td class="lb">
                    <asp:Label runat="server" ID="lblMoney_Norm" Text="Định mức"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnMoney_Norm" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lblNote" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin Tìm kiếm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Từ tháng"></asp:Label>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdFromDate" runat="server" Culture="en-US">
                        <DateInput ID="rdFromDateInput" runat="server" DisplayDateFormat="MM/yyyy">
                        </DateInput>
                    </tlk:RadMonthYearPicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label7" Text="Đến tháng"></asp:Label>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdToDate" runat="server" Culture="en-US">
                        <DateInput ID="rdToDateInput" runat="server" DisplayDateFormat="MM/yyyy">
                        </DateInput>
                    </tlk:RadMonthYearPicker>
                </td>
                <td>
                    <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,ORG_ID,ORG_NAME,ORG_CODE,TYPE_SHOP_ID,TYPE_SHOP_NAME,EFFECT_MONTH,MONEY_NORM,ORG_COST_CENTER_CODE,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Cửa hàng CH" DataField="ORG_NAME" SortExpression="ORG_NAME"
                        UniqueName="ORG_NAME"/>
                    <tlk:GridBoundColumn HeaderText="Mã CH" DataField="ORG_CODE" SortExpression="ORG_CODE"
                        UniqueName="ORG_CODE"/>
                    <tlk:GridBoundColumn HeaderText="CostCenter" DataField="ORG_COST_CENTER_CODE" SortExpression="ORG_COST_CENTER_CODE"
                        UniqueName="ORG_COST_CENTER_CODE"/>
                    <tlk:GridBoundColumn HeaderText="Loại cửa hàng" DataField="TYPE_SHOP_NAME" SortExpression="TYPE_SHOP_NAME"
                        UniqueName="TYPE_SHOP_NAME"/>
                    <tlk:GridDateTimeColumn HeaderText="Tháng hiệu lực" DataField="EFFECT_MONTH" SortExpression="EFFECT_MONTH"
                        UniqueName="EFFECT_MONTH" DataFormatString="{0:MM/yyyy}"/>
                    <tlk:GridNumericColumn HeaderText="Định mức" DataField="MONEY_NORM" SortExpression="MONEY_NORM"
                        UniqueName="MONEY_NORM" DataFormatString="{0:n0}"/>
                    <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE"
                        SortExpression="NOTE" UniqueName="NOTE" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            }else if (args.get_item().get_commandName() == "NEXT"||args.get_item().get_commandName() == "IMPORT") {
                enableAjax = false;
            }else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }
    </script>
</tlk:RadCodeBlock>
