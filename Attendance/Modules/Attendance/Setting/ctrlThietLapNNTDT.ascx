<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlThietLapNNTDT.ascx.vb"
    Inherits="Attendance.ctrlThietLapNNTDT" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjcet" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusObjcet" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đối tượng công. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn đối tượng công. %>" ClientValidationFunction="cusObjcet">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Kiểu công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboType" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusType" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn kiểu công. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn kiểu công. %>" ClientValidationFunction="cusType">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số ngày hưởng lương HĐ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtSalaryContract" MaxLength="255" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtSalaryContract"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn số ngày hưởng lương HD. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số ngày hưởng lương cơ bản")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtSalaryBase" MaxLength="255" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtSalaryBase"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn số ngày hưởng lương cơ bản. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdDate"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox SkinID="Textbox1023" Width="100%" ID="rdNote" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="EMP_OBJECT,TYPE_SHIT,SALARIED_DATES,SALARIED_DATES_CB,EFFECT_DATE,ACTFLG,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng công %>" DataField="EMP_OBJECT_NAME"
                        UniqueName="EMP_OBJECT_NAME" SortExpression="EMP_OBJECT_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu cống %>" DataField="TYPE_SHIT_NAME"
                        UniqueName="TYPE_SHIT_NAME" SortExpression="TYPE_SHIT_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày hưởng lương HĐ %>" DataField="SALARIED_DATES"
                        UniqueName="SALARIED_DATES" SortExpression="SALARIED_DATES">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày hưởng lương cơ bản %>"
                        DataField="SALARIED_DATES_CB" UniqueName="SALARIED_DATES_CB" SortExpression="SALARIED_DATES_CB">
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridNumericColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" UniqueName="NGAYHL"
                        SortExpression="NGAYHL">
                        <HeaderStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle Width="70px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE">
                        <HeaderStyle Width="200px" />
                    </tlk:GridBoundColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<script type="text/javascript">
    function cusObjcet(oSrc, args) {
        var cbo = $find("<%# cboObjcet.ClientID %>");
        args.IsValid = (cbo.get_value().length != 0);
    }
    function cusType(oSrc, args) {
        var cbo = $find("<%# cboType.ClientID %>");
        args.IsValid = (cbo.get_value().length != 0);
    }
    var enableAjax = true;
    var oldSize = 0;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (args.get_item().get_commandName() == "EXPORT") {
            enableAjax = false;
        } else if (args.get_item().get_commandName() == "SAVE") {
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
