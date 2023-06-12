<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Center.ascx.vb"
    Inherits="Training.ctrlTR_Center" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="240px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã trung tâm")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="Textbox15" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã trung tâm %>" ToolTip="<%$ Translate: Bạn phải nhập mã trung tâm %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã trung tâm đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã trung tâm đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên tiếng việt")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập tên tiếng việt %>" ToolTip="<%$ Translate: Bạn phải nhập tên tiếng việt %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên tiếng anh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameEN" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtAddress" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Điện thoại")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPhone" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số fax")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFax" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Địa chỉ Web")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtWeb" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người liên hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPerson" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Điện thoại người lên hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPhone" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Email người liên hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactEmail" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã số thuế trung tâm")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTaxCode" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTrainField" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Người đại diện trung tâm")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRepresent" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả trung tâm")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtDescription" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME_VN,NAME_EN,ADDRESS,PHONE,FAX,WEB,CONTACT_PERSON,CONTACT_PHONE,CONTACT_EMAIL,TAX_CODE,TRAIN_FIELD,DESCRIPTION,REPRESENT,REMARK">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã trung tâm %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng việt %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng anh %>" DataField="NAME_EN"
                        UniqueName="NAME_EN" SortExpression="NAME_EN" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ %>" DataField="ADDRESS" UniqueName="ADDRESS"
                        SortExpression="ADDRESS" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại %>" DataField="PHONE"
                        UniqueName="PHONE" SortExpression="PHONE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số fax %>" DataField="FAX" UniqueName="FAX"
                        SortExpression="FAX" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ website %>" DataField="WEB"
                        UniqueName="WEB" SortExpression="WEB" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người liên hệ %>" DataField="CONTACT_PERSON"
                        UniqueName="CONTACT_PERSON" SortExpression="CONTACT_PERSON" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại người liên hệ %>" DataField="CONTACT_PHONE"
                        UniqueName="CONTACT_PHONE" SortExpression="CONTACT_PHONE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email người liên hệ %>" DataField="CONTACT_EMAIL"
                        UniqueName="CONTACT_EMAIL" SortExpression="CONTACT_EMAIL" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã số thuế trung tâm %>" DataField="TAX_CODE"
                        UniqueName="TAX_CODE" SortExpression="TAX_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TRAIN_FIELD"
                        UniqueName="TRAIN_FIELD" SortExpression="TRAIN_FIELD" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người đại diện trung tâm %>" DataField="REPRESENT"
                        UniqueName="REPRESENT" SortExpression="REPRESENT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả trung tâm %>" DataField="DESCRIPTION"
                        UniqueName="DESCRIPTION" SortExpression="DESCRIPTION" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" ShowFilterIcon="true" />
                </Columns>
                <ItemStyle Width="100px" />
                <HeaderStyle Width="100px" />
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
