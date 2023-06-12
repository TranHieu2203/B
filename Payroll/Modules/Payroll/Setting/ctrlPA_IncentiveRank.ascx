<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_IncentiveRank.ascx.vb"
    Inherits="Payroll.ctrlPA_IncentiveRank" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100%" Scrolling="Both">
        <tlk:RadToolBar ID="tbarIncentiveRanks" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDetailID" runat="server" />
        <asp:HiddenField ID="hidDetailIDTemp" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Thang lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryGroup" runat="server" AutoPostBack="True" CausesValidation="False"
                        SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        >
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryLevel" runat="server" AutoPostBack="True" CausesValidation="False" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqSalaryLevel" ControlToValidate="cboSalaryLevel"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm lương %>" ToolTip="<%$ Translate: Bạn phải chọn Nhóm lương %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Bậc lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryRank" runat="server" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqRank" ControlToValidate="cboSalaryRank" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập bậc lương %>" ToolTip="<%$ Translate: Bạn phải nhập bậc lương %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại lương thưởng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboIncentiveType" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqIncentiveType" ControlToValidate="cboIncentiveType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại lương thưởng %>"
                        ToolTip="<%$ Translate: Bạn phải chọn loại lương thưởng %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="cboIncentiveType" runat="server"
                        ErrorMessage="<%$ Translate: Loại lương thưởng theo ngach bậc đã tồn tại. %>"
                        ToolTip="<%$ Translate: Loại lương thưởng theo ngach bậc đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái")%>
                </td>
                <td>
                    <tlk:RadButton ID="cbACTFLG" ToggleType="CheckBox" ButtonType="ToggleButton" runat="server"
                        Visible="True" AutoPostBack="False" CausesValidation="False">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr style="display: none">
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
                <td colspan="3">
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="1" Width="60px"
                        Value="1" ShowSpinButtons="True" ReadOnly="true" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thiết lập định mức thưởng chi tiết")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <label>
                        <%# Translate("Từ")%></label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="txtFromTarget" MinValue="0" Value="0" ShowSpinButtons="True"
                        ReadOnly="False" CausesValidation="false" Width="50px">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <label>
                        <%# Translate("%  Đến")%></label>
                    <tlk:RadNumericTextBox runat="server" ID="txtToTarget" MinValue="0" Value="1" ShowSpinButtons="True"
                        ReadOnly="False" CausesValidation="false" Width="50px">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <label>
                        <%# Translate("%")%></label>
                </td>
                <td class="lb">
                    <%# Translate("Số tiền")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtAmount" runat="server" ShowSpinButtons="true" MinValue="0"
                        Value="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="3">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadButton ID="cbIncentiveType_Percent" AutoPostBack="False" ToggleType="CheckBox"
                        Text="<%$ Translate: Thưởng = Tiền * (% Thực đạt) %>" ButtonType="ToggleButton"
                        runat="server" Visible="True" CausesValidation="False" OnClientCheckedChanged="IncentiveTypePercentCheckedChange">
                    </tlk:RadButton>
                    <tlk:RadButton ID="cbIncentiveType_Amount" AutoPostBack="False" ToggleType="CheckBox"
                        Text="<%$ Translate: Thưởng = Mức tiền cố định (Khi đạt đến định mức thưởng) %>"
                        ButtonType="ToggleButton" runat="server" Visible="True" CausesValidation="False"
                        OnClientCheckedChanged="IncentiveTypeAmountCheckedChange">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <tlk:RadGrid ID="rgRankDetail" runat="server" Width="100%" Height="165px" SkinID="GridSingleSelect">
                        <MasterTableView DataKeyNames="ID,INCENTIVE_RANK_ID,FROM_TARGET,TO_TARGET,AMOUNT,INCENTIVE_PERCENT,INCENTIVE_AMOUNT,REMARK,ACTFLG,ORDERS"
                            ClientDataKeyNames="ID,INCENTIVE_RANK_ID,FROM_TARGET,TO_TARGET,AMOUNT,INCENTIVE_PERCENT,INCENTIVE_AMOUNT,REMARK,ACTFLG,ORDERS"
                            CommandItemDisplay="Top">
                            <CommandItemStyle Height="25px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnInsertRankDetail" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Thêm %>" ToolTip="SAVE"
                                            CommandName="InsertRankDetail">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: left">
                                        <tlk:RadButton ID="btnDeleteRankDetail" runat="server" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                            CausesValidation="false" Width="70px" Text="<%$ Translate: Xóa %>" CommandName="DeleteRankDetail"
                                            OnClientClicking="btnDeleteIncentiveDetailClick">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <tlk:GridBoundColumn DataField="ID" Visible="false" />
                                <tlk:GridBoundColumn DataField="INCENTIVE_RANK_ID" Visible="false" />
                                <tlk:GridTemplateColumn HeaderText="" UniqueName="hidFielID" Display="False">
                                    <ItemTemplate>
                                        <tlk:RadTextBox ID="txtIdTemp" Text='<%# Guid.NewGuid().ToString() %>' runat="server"
                                            SkinID="Textbox1023" Width="100%">
                                        </tlk:RadTextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </tlk:GridTemplateColumn>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Từ % %>" DataField="FROM_TARGET"
                                    SortExpression="FROM_TARGET" UniqueName="FROM_TARGET" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Đến % %>" DataField="TO_TARGET"
                                    SortExpression="TO_TARGET" UniqueName="TO_TARGET" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT"
                                    SortExpression="AMOUNT" UniqueName="AMOUNT" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </tlk:GridNumericColumn>
                                <tlk:GridTemplateColumn HeaderText="<%$ Translate: Thưởng = Tiền * (% Thực đạt) %>"
                                    UniqueName="INCENTIVE_PERCENT" DataField="INCENTIVE_PERCENT">
                                    <ItemTemplate>
                                        <tlk:RadButton ID="chkINCENTIVE_TYPE_Percent" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            Checked='<%# if(Eval("INCENTIVE_PERCENT").ToString() = "0", False , True) %>'
                                            Text="" runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                                        </tlk:RadButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </tlk:GridTemplateColumn>
                                <tlk:GridTemplateColumn HeaderText="<%$ Translate: Thưởng = Mức tiền cố định (Khi đạt đến định mức thưởng) %>"
                                    UniqueName="INCENTIVE_AMOUNT" DataField="INCENTIVE_AMOUNT">
                                    <ItemTemplate>
                                        <tlk:RadButton ID="chkINCENTIVE_TYPE_Amount" ToggleType="CheckBox" ButtonType="ToggleButton"
                                            Checked='<%# if(Eval("INCENTIVE_AMOUNT").ToString() = "0", False , True) %>'
                                            Text="" runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                                        </tlk:RadButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </tlk:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </tlk:RadGrid>
                    <tlk:RadButton ID="cbShowMaster" AutoPostBack="False" ToggleType="CheckBox" Font-Bold="True" Checked="True"
                        Text="<%$ Translate: Ẩn/ hiển thị danh sách lương thưởng %>" ButtonType="ToggleButton"
                        runat="server" Visible="True" CausesValidation="False" OnClientCheckedChanged="ShowHideMaster">
                    </tlk:RadButton>
                    
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" Scrolling="Both" runat="server" Collapsed="False" Height="200px">
        <tlk:RadGrid ID="rgRank" runat="server" Height="100%" AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,SAL_GROUP_ID,SAL_LEVEL_ID,SAL_RANK_ID,SAL_INCENTIVE_ID,EFFECT_DATE,REMARK,ORDERS,ACTFLG">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="SAL_GROUP_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="SAL_LEVEL_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="SAL_RANK_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="SAL_INCENTIVE_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thang lương %>" DataField="SAL_GROUP_NAME"
                        SortExpression="SAL_GROUP_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngạch lương %>" DataField="SAL_LEVEL_NAME"
                        SortExpression="SAL_LEVEL_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_LEVEL_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Bậc lương %>" DataField="RANK"
                        SortExpression="RANK" AutoPostBackOnFilter="true" ShowFilterIcon="false" AndCurrentFilterFunction="EqualTo"
                        UniqueName="RANK" NumericType="Number">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Loại lương thưởng %>" DataField="SAL_INCENTIVE_NAME"
                        SortExpression="SAL_INCENTIVE_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="EqualTo" UniqueName="SAL_INCENTIVE_NAME" NumericType="Number">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        SortExpression="EFFECT_DATE" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="EqualTo" UniqueName="EFFECT_DATE" NumericType="Number">
                    </tlk:GridNumericColumn>
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Thưởng = Mức tiền cố định (Khi đạt đến định mức thưởng) %>"
                        UniqueName="ACTFLG" DataField="ACTFLG">
                        <ItemTemplate>
                            <tlk:RadTextBox ID="txtrgACTFLG" ToolTip='<%# Eval("ACTFLG").ToString() %>' ButtonType="ToggleButton" Text='<%# if(Eval("ACTFLG").ToString() = "A", "Áp dụng", "Ngưng áp dụng") %>' 
                                runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadTextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS" Display="False">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" AutoPostBackOnFilter="true"
                        ShowFilterIcon="false" AndCurrentFilterFunction="Contains" SortExpression="REMARK"
                        UniqueName="REMARK" Display="False" />
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


        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboSalaryGroup.ClientID %>':
                    cbo = $find('<%= cboSalaryLevel.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboSalaryRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboSalaryLevel.ClientID %>':
                    cbo = $find('<%= cboSalaryRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboSalaryGroup.ClientID %>':
//                    cbo = $find('<%= rdEffectDate.ClientID %>');
//                    var date = cbo.get_selectedDate();
//                    if (date) {
//                        var day = cbo.get_selectedDate().getDate();
//                        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
//                        var month = months[cbo.get_selectedDate().getMonth()];
//                        var year = cbo.get_selectedDate().getFullYear();
//                        value = day + "/" + month + "/" + year;
//                    }
//                    break;
                case '<%= cboSalaryLevel.ClientID %>':
                    cbo = $find('<%= cboSalaryGroup.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboSalaryRank.ClientID %>':
                    cbo = $find('<%= cboSalaryLevel.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = null;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;

        }

        function btnDeleteIncentiveDetailClick(sender, args) {
            var bCheck = $find('<%# rgRankDetail.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                args.set_cancel(true);
            }
        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function ShowHideMaster(sender, args) {
            var pane = $find("<%=RadPane2.ClientID %>");
            var rgRankDetail = $find("<%=rgRankDetail.ClientID %>");
            if (args.get_checked()) {
                pane.expand();
                rgRankDetail.get_element().style.height = "165px";
            }
            else {
                pane.collapse();
                rgRankDetail.get_element().style.height = "350px";
            }
            rgRankDetail.repaint();
        }

        function IncentiveTypePercentCheckedChange(sender, args) {
            var percent = $find("<%=cbIncentiveType_Percent.ClientID %>");
            var amount = $find("<%=cbIncentiveType_Amount.ClientID %>");
            if (args.get_checked()) {
                percent.set_checked(true);
                amount.set_checked(false);
            } else {
                percent.set_checked(false);
                amount.set_checked(true);
            }
        }
        function IncentiveTypeAmountCheckedChange(sender, args) {
            var percent = $find("<%=cbIncentiveType_Percent.ClientID %>");
            var amount = $find("<%=cbIncentiveType_Amount.ClientID %>");
            if (args.get_checked()) {
                percent.set_checked(false);
                amount.set_checked(true);
            } else {
                percent.set_checked(true);
                amount.set_checked(false);
            }
        }
    </script>
</tlk:RadCodeBlock>
