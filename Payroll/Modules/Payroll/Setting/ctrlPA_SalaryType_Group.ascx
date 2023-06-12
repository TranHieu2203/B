<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryType_Group.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryType_Group" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="170px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryLevels" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Salary_Group")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryType" runat="server" AutoPostBack="False">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboSalaryType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhóm lương %>" ToolTip="<%$ Translate: Bạn phải chọn nhóm lương %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="cboSalaryType" runat="server"
                        ErrorMessage="<%$ Translate: Nhóm lương đã được thiết lập. %>" ToolTip="<%$ Translate: Nhóm lương đã được thiết lập. %>">
                    </asp:CustomValidator>
                </td>
                <td>
                    <%# Translate("Không theo ngạch bậc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadButton ID="chkIs_Direct_Salary" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Visible="True" AutoPostBack="False" CausesValidation="False" OnClientCheckedChanged="ShowHideGroup">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr id="trGroup">
                <td class="lb" id="tdGroupLable">
                    <%# Translate("Bảng lương")%><span class="lbReq">*</span>
                </td>
                <td id="tdGroupCombo" colspan="3">
                    <tlk:RadComboBox ID="cboSalaryGroup" runat="server" AutoPostBack="False">
                    </tlk:RadComboBox>
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
                        <%# Translate("Thứ tự")%></label><span class="lbReq">*</span>
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
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,SAL_TYPE_ID,SAL_GROUP_ID,IS_DIRECT_SALARY,REMARK,ACTFLG,IS_DELETED,ORDERS,SAL_TYPE_NAME,SAL_GROUP_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="SAL_TYPE_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="SAL_GROUP_ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm lương %>" DataField="SAL_TYPE_NAME"
                        SortExpression="SAL_TYPE_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_TYPE_NAME" />
                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Không theo ngạch bậc %>"
                        UniqueName="IS_DIRECT_SALARY" DataField="IS_DIRECT_SALARY">
                        <ItemTemplate>
                            <tlk:RadButton ID="chkIS_DIRECT_SALARY" ToggleType="CheckBox" ButtonType="ToggleButton"
                                Checked='<%# Eval("IS_DIRECT_SALARY") %>'
                                Text="" runat="server" AutoPostBack="false" CausesValidation="false" ReadOnly="True">
                            </tlk:RadButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    </tlk:GridTemplateColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Thang lương %>" DataField="SAL_GROUP_NAME"
                        SortExpression="SAL_GROUP_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" SortExpression="REMARK"
                        UniqueName="REMARK" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
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
        function ShowHideGroup(check) {
            var chk = $find("<%= chkIs_Direct_Salary.ClientID%>");
            if (chk == null) {
                if (check == 1)
                    $("#trGroup").hide();
                else {
                    $("#trGroup").show();
                }
            } else {
                if (chk.get_checked()) {
                    $("#trGroup").hide();
                } else {
                    $("#trGroup").show();
                }

            }
        }
    </script>
</tlk:RadCodeBlock>
