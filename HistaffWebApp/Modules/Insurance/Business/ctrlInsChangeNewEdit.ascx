<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsChangeNewEdit.ascx.vb"
    Inherits="Insurance.ctrlInsChangeNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style type="text/css">
    .item-head
    {
        font-size:14px;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="600px" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOT" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin biến động bảo hiểm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" SkinID="ReadOnly" ReadOnly="True"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucDanh" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDonVi" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CMND")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtcmnd" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nơi cấp")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiCap" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày sinh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNgaysinh" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Nơi sinh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoisinh" SkinID="ReadOnly" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin bảo hiểm")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại biến động")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLoaiBienDong" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusLoaiBienDong" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn loại biến động. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn loại biến động. %>" ClientValidationFunction="cusLoaiBienDong">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalLoaiBienDong" runat="server" ControlToValidate="cboLoaiBienDong"
                        ErrorMessage="<%$ Translate: Loại biến động không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Loại biến động không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
                <td colspan="2" class="lb">
                    <asp:CheckBox ID="chkbhxh" Text="BHXH" Enabled="false" runat="server"></asp:CheckBox>
                    <asp:CheckBox ID="chkbhyt" Text="BHYT" Enabled="false" runat="server"></asp:CheckBox>
                    <asp:CheckBox ID="chkbhtn" Text="BHTN" Enabled="false" runat="server"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tháng biến động")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadMonthYearPicker ID="rdThangBiendong" runat="server" DateInput-DisplayDateFormat="MM/yyyy"
                        DateInput-DateFormat="dd/MMM/yyyy">
                        <DateInput Enabled="false">
                        </DateInput>
                    </tlk:RadMonthYearPicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdThangBiendong"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tháng biến động. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập tháng biến động. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hệ số kỳ trước")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmLuongKyTruoc" SkinID="ReadOnly" ReadOnly="True" 
                        runat="server" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số kỳ này")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmLuongKyNay" runat="server" onchange="reload()">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày trả thẻ BHXH")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayTraTheBHXH" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày trả thẻ BHYT")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayTraTheBHYT" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtnote" runat="server" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin truy thu")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTuNgaytt" runat="server" AutoPostBack="true">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDenNgayTT" runat="server" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="compare_WorkPermitDate_PermitExpireDate" runat="server"
                        ToolTip="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày %>" ErrorMessage="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="rdTuNgaytt" ControlToValidate="rdDenNgayTT"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("BHXH")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtBHXHTT" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("BHYT")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtBHYTTT" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("BHTN")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtBHTNTT" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="4">
                        <%# Translate("Thông tin thoái thu")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTuNgayThoaiThu" runat="server" AutoPostBack="true">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rddenngayThoaiThu" runat="server" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ToolTip="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày %>"
                        ErrorMessage="<%$ Translate: Từ ngày phải nhỏ hơn đến ngày %>" Type="Date" Operator="GreaterThan"
                        ControlToCompare="rdTuNgayThoaiThu" ControlToValidate="rddenngayThoaiThu"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("BHXH")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtBHXHTOAITHU" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("BHYT")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtBHYTTOAITHU" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("BHTN")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtBHTNTOAITHU" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            fnRegisterOnfocusOut();
        });

        function fnRegisterOnfocusOut() {
            registerOnfocusOut("ctl00_MainContent_ctrlInsChangeNewEdit_RadSplitter3");
        }

        function cusLoaiBienDong(oSrc, args) {
            var cbo = $find("<%# cboLoaiBienDong.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
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
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }

        function reload() {
            $find("<%= rdTuNgaytt.ClientID %>").clear();
            $find("<%= rdDenNgayTT.ClientID %>").clear();
            $find("<%= txtBHXHTT.ClientID %>").clear();
            $find("<%= txtBHYTTT.ClientID %>").clear();
            $find("<%= txtBHTNTT.ClientID %>").clear();

            $find("<%= rdTuNgayThoaiThu.ClientID %>").clear();
            $find("<%= rddenngayThoaiThu.ClientID %>").clear();
            $find("<%= txtBHXHTOAITHU.ClientID %>").clear();
            $find("<%= txtBHYTTOAITHU.ClientID %>").clear();
            $find("<%= txtBHTNTOAITHU.ClientID %>").clear();
        }

    </script>
</tlk:RadCodeBlock>
