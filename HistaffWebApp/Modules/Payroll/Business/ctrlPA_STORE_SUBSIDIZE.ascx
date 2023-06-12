<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_STORE_SUBSIDIZE.ascx.vb"
    Inherits="Payroll.ctrlPA_STORE_SUBSIDIZE" %>
<%@ Import Namespace="Framework.UI" %>
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidBrandID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="320px" Scrolling="None">
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
                   <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
               </td>
               <td>
                   <tlk:RadDatePicker ID="rdpEFFECT_DATE" runat="server">
                   </tlk:RadDatePicker>
                   <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdpEFFECT_DATE"
                       runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                       ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
               </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="lblORG_NAME" Text="Cửa hàng"></asp:Label><span class="lbReq">*</span>
                </td>
               <td>
                    <tlk:RadTextBox ID="txtORG_NAME" runat="server" ReadOnly="True" AutoPostBack="true" Width="130px">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqOrgSetName" ControlToValidate="txtORG_NAME"
                        runat="server" ErrorMessage="Bạn phải chọn cửa hàng " ToolTip="Bạn phải chọn cửa hàng"> 
                    </asp:RequiredFieldValidator>
                    <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false" />
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lblBrand_NAME" Text="Nhãn hàng"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox  ID="txtBrand_NAME" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                
                <td class="lb">
                    <asp:Label runat="server" ID="lblRate" Text="Tỉ lệ tối thiểu"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnRate" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lblTARGET_PLAN" Text="Target DT"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTARGET_PLAN" runat="server" >
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnTARGET_PLAN"
                        runat="server" ErrorMessage="Bạn phải nhập Target DT" ToolTip="Bạn phải nhập Target DT"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lblBENEFIT_VALUE" Text="Mức hưởng"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnBENEFIT_VALUE" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lblREVENUE_MIN" Text="DT tối thiểu"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnREVENUE_MIN" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("DT tối thiểu (<)")%><span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnLESS_REVENUE" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                     <%# Translate("DT tối thiểu (=>)")%><span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnTHAN_REVENUE" runat="server">
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
                    <tlk:RadDatePicker ID="rdFromDate" runat="server" >
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label7" Text="Đến tháng"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToDate" runat="server" >
                    </tlk:RadDatePicker>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,ORG_ID,ORG_NAME,BRAND_ID,BRAND_NAME,BRAND_RATE,TARGET_PLAN,REVENUE_MIN,LESS_REVENUE,THAN_REVENUE,BENEFIT_VALUE,EFFECT_DATE,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Cửa hàng" DataField="ORG_NAME" SortExpression="ORG_NAME"
                        UniqueName="ORG_NAME"/>
                    <tlk:GridBoundColumn HeaderText="Nhãn hàng" DataField="BRAND_NAME" SortExpression="BRAND_NAME"
                        UniqueName="BRAND_NAME"/>
                    <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE" SortExpression="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}"/>
                    <tlk:GridNumericColumn HeaderText="Tỉ lệ tối thiểu" DataField="BRAND_RATE" SortExpression="BRAND_RATE"
                        UniqueName="BRAND_RATE" DataFormatString="{0:n0}"/>
                    <tlk:GridNumericColumn HeaderText="Target doanh thu" DataField="TARGET_PLAN" SortExpression="TARGET_PLAN"
                        UniqueName="TARGET_PLAN" DataFormatString="{0:n0}"/>
                    <tlk:GridNumericColumn HeaderText="Doanh thu tối thiểu" DataField="REVENUE_MIN" SortExpression="REVENUE_MIN"
                        UniqueName="REVENUE_MIN" DataFormatString="{0:n0}"/>
                    <tlk:GridNumericColumn HeaderText="Doanh thu tối thiểu (<)" DataField="LESS_REVENUE" SortExpression="LESS_REVENUE"
                        UniqueName="LESS_REVENUE" DataFormatString="{0:n0}" />
                    <tlk:GridNumericColumn HeaderText="Doanh thu tối thiểu (=>)" DataField="THAN_REVENUE" SortExpression="THAN_REVENUE"
                        UniqueName="THAN_REVENUE" DataFormatString="{0:n0}"/>
                    <tlk:GridNumericColumn HeaderText="Mức hưởng" DataField="BENEFIT_VALUE" SortExpression="BENEFIT_VALUE"
                        UniqueName="BENEFIT_VALUE" DataFormatString="{0:n0}"/>
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
            }else if (args.get_item().get_commandName() == "NEXT"||args.get_item().get_commandName() == "IMPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
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
