<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryRank.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryRank" %>
<%@ Import Namespace="Framework.UI" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="165px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryRanks" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Bảng lương/thưởng HQCV")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngạch lương/thưởng HQCV")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryLevel" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSalaryLevel" ControlToValidate="cboSalaryLevel"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm lương %>" ToolTip="<%$ Translate: Bạn phải chọn Nhóm lương %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Bậc lương/thưởng HQCV")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRank" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqRank" ControlToValidate="txtRank" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập bậc lương %>" ToolTip="<%$ Translate: Bạn phải nhập bậc lương %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtRank" runat="server" ErrorMessage="<%$ Translate: Bậc lương theo bảng lương và ngạch lương đã tồn tại. %>"
                        ToolTip="<%$ Translate: Bậc lương theo bảng lương và ngạch lương đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                <%--<td colspan="2">
                    <div runat="server" id="divSalaryBasic">
                        <div style="float: left;padding-right: 6px;padding-left: 5px;text-align: right; width: 30%">
                            <label> <%# Translate("Số tiền/ Hệ số")%></label><span class="lbReq">*</span>
                        </div>
                        <div style="float: left;">
                            <tlk:RadNumericTextBox ID="rntxtSalaryBasic" runat="server" ShowSpinButtons="true"
                                MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="3">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtSalaryBasic"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số tiền %>" ToolTip="<%$ Translate: Bạn phải nhập Số tiền %>">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </td>--%>
                <td class="lb">
                    <%# Translate("Hệ số/Mức lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rntxtSalaryBasic" runat="server" Style="text-align: right">
                        <ClientEvents OnKeyPress="keyPress" OnBlur="OnBlur" />
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtSalaryBasic"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số tiền %>" ToolTip="<%$ Translate: Bạn phải nhập Số tiền %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" id="tdSLlbOrders">
                    <label id="lbOrders">
                        <%# Translate("Thứ tự")%></label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="1" Width="60px"
                        Value="1" ShowSpinButtons="True" ReadOnly="true" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="SAL_LEVEL_ID, RANK, SALARY_BASIC, REMARK,ORDERS">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bảng lương/thưởng HQCV %>" DataField="SAL_GROUP_NAME"
                        SortExpression="SAL_GROUP_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngạch lương/thưởng HQCV %>" DataField="SAL_LEVEL_NAME"
                        SortExpression="SAL_LEVEL_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_LEVEL_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Bậc lương/thưởng HQCV %>" DataField="RANK"
                        SortExpression="RANK" AutoPostBackOnFilter="true" ShowFilterIcon="false" AndCurrentFilterFunction="EqualTo"
                        UniqueName="RANK" NumericType="Number">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn DataField="SALARY_BASIC" HeaderText="<%$ Translate: Hệ số/Mức lương %>"
                        SortExpression="SALARY_BASIC" UniqueName="SALARY_BASIC" ShowFilterIcon="false"
                        Display="True" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Visible="True"
                        DataFormatString="{0:n0}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" AutoPostBackOnFilter="true"
                        ShowFilterIcon="false" AndCurrentFilterFunction="Contains" SortExpression="REMARK"
                        UniqueName="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
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
        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();

            if (!text.match('^[0-9.]+$'))
                args.set_cancel(true);
        }
        function OnBlur(sender, args) {
            //sender.set_value(sender.get_value());
            try {
                var value = parseFloat(sender.get_value());
                var ctl = $find("<%= rntxtSalaryBasic.ClientID%>");
                ctl.set_value(value);
            }
            catch (err) {
                sender.set_value(0);
            }
        }
    </script>
</tlk:RadCodeBlock>
