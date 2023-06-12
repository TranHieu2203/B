<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WelfareList.ascx.vb"
    Inherits="Profile.ctrlHU_WelfareList" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarWelfareLists" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td colspan="4">
                </td>
                <td>
                    <%# Translate("Giới tính")%>
                </td>
                <td>
                    <%# Translate("Loại hợp đồng")%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã chế độ phúc lợi")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <td class="lb">
                        <%# Translate("Tên chế độ phúc lợi")%><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                        </tlk:RadTextBox>
                        <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                            CausesValidation="false" ErrorMessage="<%$ Translate: Bạn phải nhập tên chế độ phúc lợi. %>"
                            ToolTip="<%$ Translate: Bạn phải nhập tên chế độ phúc lợi. %>"></asp:RequiredFieldValidator>
                    </td>
                    <tlk:RadTextBox ID="txtCode" SkinID="ReadOnly" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        CausesValidation="false" ErrorMessage="<%$ Translate: Bạn phải nhập mã chế độ phúc lợi. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập mã chế độ phúc lợi. %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã chế độ phúc lợi đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã chế độ phúc lợi đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td rowspan="5" style="vertical-align: top;">
                    <tlk:RadListBox ID="lstbGender" CheckBoxes="true" runat="server" Height="60px" Width="100px"
                        OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging">
                    </tlk:RadListBox>
                </td>
                <td rowspan="5" style="vertical-align: top;">
                    <tlk:RadListBox ID="lstCONTRACT_TYPE" CheckBoxes="true" runat="server" Height="135px"
                        Width="290px">
                    </tlk:RadListBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số tiền")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmMONEY" MinValue="1" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                <%--    <asp:RequiredFieldValidator ID="cvalMONEY" ControlToValidate="nmMONEY" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập số tiền. %>" ToolTip="<%$ Translate: Bạn phải nhập số tiền. %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSENIORITY" runat="server" Text="Thâm niên (tháng)"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmSENIORITY" SkinID="Number" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDenSoThang" runat="server" Text="Đến số tháng "></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmDenSoThang" SkinID="Number" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ToolTip="<%$ Translate: Nhập đến số tháng lớn hơn từ số tháng  %>"
                        ErrorMessage="<%$ Translate: Nhập đến số tháng lớn hơn từ số tháng  %>" Type="Integer"
                        Operator="GreaterThan" ControlToCompare="nmSENIORITY" ControlToValidate="nmDenSoThang"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tuổi con nhỏ từ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCHILD_OLD_FROM" MinValue="0" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("đến")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCHILD_OLD_TO" MinValue="0" MaxLength="38" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="dpSTART_DATE">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="dpSTART_DATE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="dpEND_DATE">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn Ngày hiệu lực %>"
                        ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn Ngày hiệu lực %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="dpSTART_DATE" ControlToValidate="dpEND_DATE"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                </td>
                <td>
                    <tlk:RadButton ID="chkIS_AUTO" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="<%$ Translate: Tự động áp dụng %>">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
        <tlk:RadGrid PageSize="50" ID="rgWelfareList" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME,CONTRACT_TYPE_NAME,CONTRACT_TYPE,GENDER,SENIORITY,MONEY,START_DATE,END_DATE,IS_AUTO">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã chế độ phúc lợi %>" DataField="CODE"
                        SortExpression="CODE" UniqueName="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chế độ phúc lợi %>" DataField="NAME"
                        SortExpression="NAME" UniqueName="NAME">
                        <HeaderStyle Width="200px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACT_TYPE_NAME"
                        SortExpression="CONTRACT_TYPE_NAME" UniqueName="CONTRACT_TYPE_NAME">
                        <HeaderStyle Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giới tính %>" DataField="GENDER_NAME"
                        SortExpression="GENDER_NAME" UniqueName="GENDER_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thâm niên %>" DataField="SENIORITY"
                        SortExpression="SENIORITY" UniqueName="SENIORITY" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tuổi con nhỏ từ %>" DataField="CHILD_OLD_FROM"
                        SortExpression="CHILD_OLD_FROM" UniqueName="CHILD_OLD_FROM" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tuổi con nhỏ đến %>" DataField="CHILD_OLD_TO"
                        SortExpression="CHILD_OLD_TO" UniqueName="CHILD_OLD_TO" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số tiền %>" DataField="MONEY" SortExpression="MONEY"
                        DataFormatString="{0:n0}" UniqueName="MONEY" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="START_DATE"
                        DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE" UniqueName="START_DATE" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="END_DATE"
                        DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE" UniqueName="END_DATE" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Tự động áp dụng %>" DataField="IS_AUTO"
                        SortExpression="IS_AUTO" UniqueName="IS_AUTO" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                </Columns>
                <HeaderStyle Width="120px" />
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
        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
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
