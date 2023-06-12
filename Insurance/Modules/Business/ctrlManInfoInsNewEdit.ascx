<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlManInfoInsNewEdit.ascx.vb"
    Inherits="Insurance.ctrlManInfoInsNewEdit" %>
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
                <td class="lb">
                    <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" SkinID="ReadOnly" ReadOnly="True"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" SkinID="ButtonView" runat="server" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqrcboCHANGE_TYPE" ControlToValidate="txtEmployeeCode"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên để làm biến động. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucDanh" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDonVi" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CMND")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtcmnd" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nơi cấp")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiCap" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày sinh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNgaysinh" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nơi sinh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiSinh" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCapNS" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Lương tham gia đóng BH")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnSalary" SkinID="ReadOnly" runat="server" ReadOnly="true">
                        <NumberFormat AllowRounding="true" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                        <asp:CheckBox ID="cbkBHXH" runat="server" Text="<%$ Translate: Bảo hiểm xã hội %>" />
                        <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ tháng")%>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdFromBHXH" SkinID="ReadOnly" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                        DateInput-DateFormat="dd/MMM/yyyy">
                    </tlk:RadMonthYearPicker>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian đóng BH trước khi vào CT")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmTotalBefor" MinValue="0" MaxValue="100" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số sổ")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSoSo" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng sổ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbotinhtrangSo" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalTinhTrangSo" runat="server" ControlToValidate="cbotinhtrangSo"
                        ErrorMessage="<%$ Translate: Tình trạng sổ không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Tình trạng sổ không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày cấp")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgaycapXH" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày nộp sổ cho công ty")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayNopSo" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ToolTip="<%$ Translate: Ngày nộp sổ cho công ty phải nhỏ hơn ngày cấp %>"
                        ErrorMessage="<%$ Translate: Ngày nộp sổ cho công ty phải nhỏ hơn ngày cấp %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="rdNgaycapXH" ControlToValidate="rdNgayNopSo"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                        <asp:CheckBox ID="cbkBHYT" runat="server" Text="<%$ Translate: Bảo hiểm y tế%>" />
                        <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số thẻ y tế")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSoThe" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tình trạng thẻ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTinhTrangThe" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalTinhTrangThe" runat="server" ControlToValidate="cboTinhTrangThe"
                        ErrorMessage="<%$ Translate: Tình trạng thẻ không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Tình trạng thẻ không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiêu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayHieuLuc" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hiệu lực")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayhetHL" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ToolTip="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                        ErrorMessage="<%$ Translate: Ngày hiệu lực phải nhỏ hơn ngày hết hiệu lực %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="rdNgayHieuLuc" ControlToValidate="rdNgayhetHL"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi khám chữa bệnh")%>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboNoiKham" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalNoiKham" runat="server" ControlToValidate="cboNoiKham"
                        ErrorMessage="<%$ Translate: Tình trạng thẻ không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Tình trạng thẻ không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                        <asp:CheckBox ID="cbkBHTN" runat="server" Text="<%$ Translate: Bảo hiểm thất nghiệp %>" />
                        <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ tháng")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFromTN" SkinID="ReadOnly" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến tháng")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdToTN" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ToolTip="<%$ Translate: Từ tháng phải nhỏ hơn đến tháng %>"
                        ErrorMessage="<%$ Translate: Từ tháng phải nhỏ hơn đến tháng %>" Type="Date"
                        Operator="GreaterThan" ControlToCompare="rdFromTN" ControlToValidate="rdToTN"></asp:CompareValidator>
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

        $(document).ready(function () {
            fnRegisterOnfocusOut();
        });

        function fnRegisterOnfocusOut() {
            registerOnfocusOut("ctl00_MainContent_ctrlManInfoInsNewEdit_RadSplitter3");
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
                //                getRadWindow().close(null);
                //                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == "SAVE") {
                //                // Nếu nhấn nút SAVE thì resize
                //                if (!Page_ClientValidate("")) {
                //                    ResizeSplitter();
                //                }
                //                else {
                //                    ResizeSplitterDefault();
                //                }
                //            } else {
                //                // Nếu nhấn các nút khác thì resize default
                //                ResizeSplitterDefault();
                //            }
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
