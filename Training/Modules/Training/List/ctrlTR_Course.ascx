<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Course.ascx.vb"
    Inherits="Training.ctrlTR_Course" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30%" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Lĩnh vực đào tạo")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTrainField" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqTrainField" ControlToValidate="cboTrainField"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập lĩnh vực đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải nhập lĩnh vực đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Nhóm chương trình")%>
                    <span class="lbReq">*</span>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox ID="cboProgramGroup" runat="server">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="reqProgramGroup" ControlToValidate="cboProgramGroup"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhóm chương trình %>"
                        ToolTip="<%$ Translate: Bạn phải nhập nhóm chương trình %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã khóa đào tạo")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox15" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã khóa đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập mã khóa đào tạo %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã khóa đào tạo %>"
                        ToolTip="<%$ Translate: Mã khóa đào tạo đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên khóa đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập tên khóa đào tạo %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Nhóm chứng chỉ")%>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox ID="cboCertificateGroup" runat="server" CausesValidation="false"
                        AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Tên chứng chỉ")%>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox ID="cboCertificate" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" TextMode="MultiLine" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                </td>
                <td colspan="3">
                    <tlk:RadButton ID="cbDriver" ToggleType="CheckBox" ButtonType="ToggleButton" runat="server"
                        Text="<%$ Translate: Nhân viên lái xe %>">
                    </tlk:RadButton>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME,TR_CER_GROUP_ID,TR_CER_GROUP_NAME,TR_CERTIFICATE_ID,TR_CERTIFICATE_NAME,TR_TRAIN_FIELD_ID,TR_TRAIN_FIELD_NAME,TR_PROGRAM_GROUP_ID,TR_PROGRAM_GROUP_NAME,DRIVER,REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã khóa đào tạo %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên khóa đào tạo %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chứng chỉ %>" DataField="TR_CER_GROUP_ID"
                        UniqueName="TR_CER_GROUP_ID" SortExpression="TR_CER_GROUP_ID" Visible="false" />
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chứng chỉ %>" DataField="TR_CER_GROUP_NAME"
                        UniqueName="TR_CER_GROUP_NAME" SortExpression="TR_CER_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chứng chỉ %>" DataField="TR_CERTIFICATE_NAME"
                        UniqueName="TR_CERTIFICATE_NAME" SortExpression="TR_CERTIFICATE_NAME" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TR_TRAIN_FIELD_NAME"
                        UniqueName="TR_TRAIN_FIELD_NAME" SortExpression="TR_TRAIN_FIELD_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: ID Nhóm chương trình %>" DataField="TR_PROGRAM_GROUP_ID"
                        UniqueName="TR_PROGRAM_GROUP_ID" SortExpression="TR_PROGRAM_GROUP_ID" Visible="false" />
                    <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm chương trình %>" DataField="TR_PROGRAM_GROUP_NAME"
                        UniqueName="TR_PROGRAM_GROUP_NAME" SortExpression="TR_PROGRAM_GROUP_NAME" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Nhân viên lái xe %>" DataField="DRIVER"
                        UniqueName="DRIVER" SortExpression="DRIVER" ShowFilterIcon="true" />--%>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                        SortExpression="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" ShowFilterIcon="true" />
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
    </script>
</tlk:RadCodeBlock>
