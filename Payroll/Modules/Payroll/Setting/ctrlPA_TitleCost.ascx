<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_TitleCost.ascx.vb"
    Inherits="Payroll.ctrlPA_TitleCost" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTitle">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Chức danh %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Chức danh  %>" ClientValidationFunction="cusTitle">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày bắt đầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="valrdEffectDate" ControlToValidate="rdEffectDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày bắt đầu %>" ToolTip="<%$ Translate:  Bạn phải nhập Ngày bắt đầu %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày bắt đầu %>"
                        ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn Ngày bắt đầu %>" Type="Date"
                        Operator="GreaterThan" ControlToCompare="rdEffectDate" ControlToValidate="rdExpireDate"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lương cơ bản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryBasic" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phụ cấp điện thoại")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryAllowance" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phụ cấp bữa ăn")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryRice" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tiền BHXH công ty đóng")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryIns" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thưởng hiệu quả công việc")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalarySoft" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí khác")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryOther" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,TITLE_ID,SAL_BASIC,SAL_MOBILE,SAL_RICE,SAL_INS,SAL_SOFT,SAL_OTHER,EFFECT_DATE,EXPIRE_DATE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                        SortExpression="TITLE_NAME" UniqueName="TITLE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Lương cơ bản %>" DataField="SAL_BASIC"
                        SortExpression="SAL_BASIC" UniqueName="SAL_BASIC"   DataFormatString="{0:n0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Phụ cấp điện thoại %>" DataField="SAL_MOBILE"
                        SortExpression="SAL_MOBILE" UniqueName="SAL_MOBILE"   DataFormatString="{0:n0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Phụ cấp bữa ăn %>" DataField="SAL_RICE"
                        SortExpression="SAL_RICE" UniqueName="SAL_RICE"  DataFormatString="{0:n0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền BHXH công ty đóng %>" DataField="SAL_INS"
                        SortExpression="SAL_INS" UniqueName="SAL_INS"  DataFormatString="{0:n0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thưởng hiệu quả công việc %>" DataField="SAL_SOFT"
                        SortExpression="SAL_SOFT" UniqueName="SAL_SOFT"  DataFormatString="{0:n0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí khác %>" DataField="SAL_OTHER"
                        SortExpression="SAL_OTHER" UniqueName="SAL_OTHER"  DataFormatString="{0:n0}">
                    </tlk:GridNumericColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE">
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE">
                    </tlk:GridDateTimeColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }


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
