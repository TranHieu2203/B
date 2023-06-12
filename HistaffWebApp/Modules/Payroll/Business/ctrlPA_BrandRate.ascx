<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_BrandRate.ascx.vb"
    Inherits="Payroll.ctrlPA_BrandRate" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="185px" Scrolling="None">
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
                            <%# Translate("Tỷ lệ DT tối thiểu")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox runat="server" ID="rnRate" CausesValidation="false">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnRate"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tỷ lệ DT tối thiểu. %>" ToolTip="<%$ Translate: Bạn phải nhập tỷ lệ DT tối thiểu. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkDoanhthu" Text="Mức hưởng theo doanh thu" />
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                            </tlk:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEffectDate" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
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
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="BRAND_ID,BRAND_NAME,RATE,EFFECT_DATE,NOTE,IS_DOANHTHU">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhãn hàng %>" DataField="BRAND_NAME"
                                SortExpression="BRAND_NAME" UniqueName="BRAND_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tỷ lệ DT tối thiểu %>" DataField="RATE"
                                SortExpression="RATE" UniqueName="RATE" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Mức hưởng theo DT %>" DataField="IS_DOANHTHU"
                                SortExpression="IS_DOANHTHU" UniqueName="IS_DOANHTHU" AllowFiltering="false" HeaderStyle-Width="40px"
                                HeaderStyle-HorizontalAlign="Center"/>
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
