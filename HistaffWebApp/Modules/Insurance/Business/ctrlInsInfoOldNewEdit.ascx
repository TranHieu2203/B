<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsInfoOldNewEdit.ascx.vb"
    Inherits="Insurance.ctrlInsInfoOldNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
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
                        <%# Translate("Thông tin bảo hiểm cũ")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 120px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" ReadOnly="True"
                        Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 120px">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChucDanh" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" SkinID="ReadOnly" ReadOnly="True" runat="server">
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
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCapNS" SkinID="ReadOnly" ReadOnly="True" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head"  colspan="4" align="left">
                    <asp:CheckBox ID="chkBHXH" Font-Bold="true" Text="Bảo hiểm xã hội" runat="server">
                    </asp:CheckBox>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ tháng")%>
                     <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdtuthangbhxh" DateInput-DateFormat="MM/yyyy" MaxLength="12"
                        runat="server" ToolTip="">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdtuthangbhxh"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập từ tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập từ tháng. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến tháng")%>
                     <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDenThangbhxh" DateInput-DateFormat="MM/yyyy" MaxLength="12"
                        runat="server" ToolTip="">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="compare_WorkPermitDate_PermitExpireDate" runat="server"
                        ToolTip="<%$ Translate: Từ tháng phải nhỏ hơn đến tháng %>" ErrorMessage="<%$ Translate: Từ tháng phải nhỏ hơn đến tháng %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="rdtuthangbhxh" ControlToValidate="rdDenThangbhxh"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdDenThangbhxh"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập đến tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập đến tháng. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đơn vị đóng")%>
                     <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg_ID_INS" runat="server">
                    </tlk:RadTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrg_ID_INS"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập đơn vị đóng. %>" ToolTip="<%$ Translate: Bạn phải nhập đơn vị đóng. %>"> </asp:RequiredFieldValidator>
                
                </td>
                <td class="lb">
                    <%# Translate("Lương tham gia BH")%>
                     <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtLuongThamGia" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtLuongThamGia"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Lương tham gia BH. %>" ToolTip="<%$ Translate: Bạn phải nhập Lương tham gia BH. %>"> </asp:RequiredFieldValidator>
                
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
            <%--  <tr>
                <td colspan="4" align="left">
                    <asp:CheckBox ID="chkBHTN" Font-Bold="true" Text="Bảo hiểm thất nghiệp" runat="server">
                    </asp:CheckBox>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Từ tháng")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdfromMonthBHTN" DateInput-DateFormat="MM/yyyy" MaxLength="12"
                        runat="server" ToolTip="">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến tháng")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdtoMonthBHTN" DateInput-DateFormat="MM/yyyy" MaxLength="12"
                        runat="server" ToolTip="">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                        ToolTip="<%$ Translate: Từ tháng phải nhỏ hơn đến tháng %>" ErrorMessage="<%$ Translate: Từ tháng phải nhỏ hơn đến tháng %>"
                        Type="Date" Operator="GreaterThan" ControlToCompare="rdfromMonthBHTN" ControlToValidate="rdtoMonthBHTN"></asp:CompareValidator>

                </td>
            </tr>--%>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            registerOnfocusOut('ctl00_MainContent_ctrlInsInfoOldNewEdit_RadPane2');
        });
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
        
    </script>
</tlk:RadCodeBlock>
