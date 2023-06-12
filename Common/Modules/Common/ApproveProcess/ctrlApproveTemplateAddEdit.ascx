<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlApproveTemplateAddEdit.ascx.vb"
    Inherits="Common.ctrlApproveTemplateAddEdit" %>
<tlk:RadToolBar ID="tbarEdit" runat="server" OnClientButtonClicking="clientButtonClicking" Width="100%" />
<asp:ValidationSummary runat="server" ID="valSum" />
<table width="100%" class="table-form" >
    <tr>
        <td class="lb">
            <%# Translate("Tên Template:") %><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTemplateName" Width="200px">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator runat="server" ID="reqTemplateName" ControlToValidate="txtTemplateName" ErrorMessage='<%$ Translate: Bạn chưa nhập tên Template %>' ToolTip='<%$ Translate: Bạn chưa nhập tên Template %>'>
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Đối tượng áp dụng:")%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboTemplateType" Width="200px">
                <Items>
                    <tlk:RadComboBoxItem Value="0" Text='<%$ Translate: Đơn vị/Phòng ban %>' />
                    <tlk:RadComboBoxItem Value="1" Text='<%$ Translate: Nhân viên %>' />
                </Items>
            </tlk:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Độ ưu tiên:")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="txtTemplateOrder" ShowSpinButtons="true"
                Width="60" MinValue="1" MaxValue="999">
                <NumberFormat DecimalDigits="0" />
            </tlk:RadNumericTextBox>
            <asp:RequiredFieldValidator ID="reqTemplateOrder" ControlToValidate="txtTemplateOrder" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập độ ưu tiên %>" ToolTip="<%$ Translate: Bạn phải nhập độ ưu tiên %>">
                    </asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<tlk:RadAjaxManager runat="server" ID="radAjaxManager"></tlk:RadAjaxManager>
<script type="text/javascript">
    

    function GridCreated(sender, eventArgs) {
        registerOnfocusOut('RAD_SPLITTER_RadSplitter1');
    }
    function clientButtonClicking(s, e) {

        switch (e.get_item().get_commandName()) {
            case 'CANCEL':
                getRadWindow().close(null);
                e.set_cancel(true);
                break;
        }
    }

    //mandatory for the RadWindow dialogs functionality
    function getRadWindow() {
        if (window.radWindow) {
            return window.radWindow;
        }
        if (window.frameElement && window.frameElement.radWindow) {
            return window.frameElement.radWindow;
        }
        return null;
    }
</script>
