<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_Unit.ascx.vb"
    Inherits="Training.ctrlTR_Unit" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="276px">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã đơn vị")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập mã %>" ToolTip="<%$ Translate: Bạn phải nhập mã %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên đơn vị")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên %>" ToolTip="<%$ Translate: Bạn phải nhập tên %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtAddress" MaxLength="255" runat="server">
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
                    <%# Translate("Số Fax")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtFax" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ web")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtWeb" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Mã số thuế")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTaxCode" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtTrainField" MaxLength="255" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
               <tr>
                <td class="lb">
                    <%# Translate("Mô tả trung tâm")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtDesc" MaxLength="255" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người đại diện")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRepresent" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Người liên hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContact" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Điện thoại người liên hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPhone" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Địa chỉ Email người liên hệ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactEmail" MaxLength="255" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="100%">
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="CODE,NAME,ADDRESS,PHONE,FAX,WEB,TAX_CODE,TRAIN_FIELD,REPRESENT,CONTACT_PERSON,CONTACT_PHONE,CONTACT_EMAIL,REMARK,DESC_U">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã đơn vị %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên đơn vị %>" DataField="NAME"
                        UniqueName="NAME" SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ %>" DataField="ADDRESS"
                        UniqueName="ADDRESS" SortExpression="ADDRESS" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại %>" DataField="PHONE"
                        UniqueName="PHONE" SortExpression="PHONE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Fax %>" DataField="FAX"
                        UniqueName="FAX" SortExpression="FAX" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Web %>" DataField="WEB"
                        UniqueName="WEB" SortExpression="WEB" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã số thuế %>" DataField="TAX_CODE"
                        UniqueName="TAX_CODE" SortExpression="TAX_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Lĩnh vực đào tạo %>" DataField="TRAIN_FIELD"
                        UniqueName="TRAIN_FIELD" SortExpression="TRAIN_FIELD" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người đại diện %>" DataField="REPRESENT"
                        UniqueName="REPRESENT" SortExpression="REPRESENT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người liên hệ %>" DataField="CONTACT_PERSON"
                        UniqueName="CONTACT_PERSON" SortExpression="CONTACT_PERSON" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Điện thoại %>" DataField="CONTACT_PHONE"
                        UniqueName="CONTACT_PHONE" SortExpression="CONTACT_PHONE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="CONTACT_EMAIL"
                        UniqueName="CONTACT_EMAIL" SortExpression="CONTACT_EMAIL" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                        UniqueName="REMARK" SortExpression="REMARK" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" ShowFilterIcon="true" />
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
