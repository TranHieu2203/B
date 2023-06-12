<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_AllowanceList.ascx.vb"
    Inherits="Profile.ctrlHU_AllowanceList" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="200px" Scrolling="None">
        <tlk:RadToolBar ID="tbarAllowanceLists" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã phụ cấp")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox50" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã phụ cấp. %>" ToolTip="<%$ Translate: Bạn phải nhập mã phụ cấp. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã phụ cấp đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã phụ cấp đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên phụ cấp")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên phụ cấp. %>" ToolTip="<%$ Translate: Bạn phải nhập tên phụ cấp. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại phụ cấp")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboAllowaceType" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboAllowaceType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại phụ cấp %>" ToolTip="<%$ Translate: Bạn phải chọn loại phụ cấp %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Trả vào lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadButton ID="cbIs_Pay" ToggleType="CheckBox" ButtonType="ToggleButton" runat="server" Checked="True"
                        Visible="True" AutoPostBack="False" CausesValidation="False">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tỷ lệ %")%>
                </td>
                <td>
                    <tlk:RadButton ID="cbIs_Contract" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Visible="True" AutoPostBack="False" CausesValidation="False">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Đóng BHLĐ")%>
                </td>
                <td>
                    <tlk:RadButton ID="cbIs_Insurance" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Visible="True" AutoPostBack="False" CausesValidation="False">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
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
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgMain" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,REMARK,ALLOWANCE_TYPE,ORDERS,IS_CONTRACT,IS_INSURANCE,IS_PAY,ALLOWANCE_TYPE_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ALLOWANCE_TYPE" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã phụ cấp %>" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên phụ cấp %>" DataField="NAME"
                        SortExpression="NAME" UniqueName="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại phụ cấp %>" DataField="ALLOWANCE_TYPE_NAME"
                        SortExpression="ALLOWANCE_TYPE_NAME" UniqueName="ALLOWANCE_TYPE_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                        UniqueName="REMARK" />
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Trả vào lương %>"
                        UniqueName="IS_PAY" DataField="IS_PAY">
                        <ItemTemplate>
                            <tlk:RadButton ID="chkIS_PAY" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# if(Eval("IS_PAY").ToString() = "0", False , True) %>'
                                Text="" runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Tỷ lệ % %>"
                        UniqueName="IS_CONTRACT" DataField="IS_CONTRACT">
                        <ItemTemplate>
                            <tlk:RadButton ID="chkIS_CONTRACT" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# if(Eval("IS_CONTRACT").ToString() = "0", False , True) %>'
                                Text="" runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đóng BHLĐ %>"
                        UniqueName="IS_INSURANCE" DataField="IS_INSURANCE">
                        <ItemTemplate>
                            <tlk:RadButton ID="chkIS_INSURANCE" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# if(Eval("IS_INSURANCE").ToString() = "0", False , True) %>'
                                Text="" runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS" Display="True">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Trạng thái %>" UniqueName="ACTFLG"
                        DataField="ACTFLG">
                        <ItemTemplate>
                            <tlk:RadTextBox ID="txtrgACTFLG" ToolTip='<%# Eval("ACTFLG").ToString() %>' ButtonType="ToggleButton"
                                Text='<%# if(Eval("ACTFLG").ToString() = "A", "Áp dụng", "Ngưng áp dụng") %>'
                                runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadTextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
    </script>
</tlk:RadCodeBlock>
