<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlManGroupSunCareNewEdit.ascx.vb"
    Inherits="Insurance.ctrlManGroupSunCareNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style type="text/css">
    .item-head
    {
        font-size:14px;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Đăng ký thông tin bảo hiểm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 160px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" Enabled="false" runat="server"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" SkinID="ButtonView" runat="server" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="txtEmployeeCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 160px">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDonVi" SkinID="ReadOnly" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh cũ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitleOld" Enabled="false" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh mới")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitleNew" Enabled="false" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Cấp nhân sự cũ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStaffRankOld" Enabled="false" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Cấp nhân sự mới")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStaffRankNew" Enabled="false" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm bảo hiểm cũ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCostLeverOld" Enabled="false" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Nhóm bảo hiểm mới")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCostLeverNew" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalCostLeverNew" runat="server" ControlToValidate="cboCostLeverNew"
                        ErrorMessage="<%$ Translate: Nhóm bảo hiểm không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Nhóm bảo hiểm không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực nhóm BH mới")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Mức chi phí")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmMucCP" ReadOnly="true" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var oldSize = 0;
        var splitterID = 'ctl00_MainContent_ctrlManGroupSunCareNewEdit_RadSplitter3';

        $(document).ready(function () {
            fnRegisterOnfocusOut()
        });

        function fnRegisterOnfocusOut() {
            registerOnfocusOut(splitterID);
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

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                //getRadWindow().close(null);
                //args.set_cancel(true);
            } else if (args.get_item().get_commandName() == "SAVE") {
                if (!Page_ClientValidate("")) {
                    ResizeSplitter();
                }
                else {
                    ResizeSplitterDefault();      
                }
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
