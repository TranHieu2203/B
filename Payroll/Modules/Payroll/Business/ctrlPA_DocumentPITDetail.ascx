<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_DocumentPITDetail.ascx.vb"
    Inherits="Payroll.ctrlPA_DocumentPITDetail" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField runat="server" ID="hidEmp" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <table class="table-form">
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="<%$ Translate: Ký hiệu %>"></asp:Label>
                    <tlk:RadTextBox ID="txtSymbol" SkinID="Readonly" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="lbNo" runat="server" Text="<%$ Translate: Số(No) %>"></asp:Label>
                    <tlk:RadTextBox ID="txtPITNo" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtPITNo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số(No). %>" ToolTip="<%$ Translate: Bạn phải nhập Số(No). %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <b>
                        <%# Translate("[I] THÔNG TIN TỔ CHỨC TRẢ THU NHẬP (Information of the income paying organization)")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Translate: 01. Tên tổ chức trả thu nhập( Name of the income paying organization) %>"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" Width="100%" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="<%$ Translate: 02. Mã số thuế (Tax identification number) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtOrgPITNo" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="<%$ Translate: 03. Địa chỉ (Address) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtOrgAddress" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="<%$ Translate: 04. Điện thoại (Telephone number) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtOrgPhoneNumber" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <b>
                        <%# Translate("[II] THÔNG TIN NGƯỜI NỘP THUẾ (Information of taxpayer)")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="<%$ Translate: 05. Họ và tên (Full name) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtEmployeeName" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="<%$ Translate: 06. Mã số thuế (Tax identification number) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtEmployeePITCode" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="<%$ Translate: 07. Quốc tịch(Nationality) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtNationality" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton runat="server" ID="chkResident" GroupName="individual" Text="08. Cá nhân cư trú (Resident individual)" />
                </td>
                <td>
                    <asp:RadioButton runat="server" ID="chkNonResident" GroupName="individual" Text="09. Cá nhân không cư trú (Non-resident individual)" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="Label9" runat="server" Text="<%$ Translate: 10. Địa chỉ hoặc điện thoại liên hệ (Contact Address or Telephone Number) %>"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <tlk:RadTextBox runat="server" ID="txtContact" ReadOnly="true" Width="100%" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="<%$ Translate: 11. Số CMND hoặc số hộ chiếu (ID/Passport Number) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtIDNo" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="<%$ Translate: 12.  Nơi cấp (Place of issue) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtIDPlace" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="<%$ Translate: 13.  Ngày cấp (Date of issue) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtIDDate" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <b>
                        <%# Translate("[III] THÔNG TIN THUẾ THU NHẬP CÁ NHÂN KHẤU TRỪ (Information of personal income tax withholding)")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="<%$ Translate: 14. Khoản thu nhập (Type of income) %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtTypeIncome" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label14" runat="server" Text="<%$ Translate: 15. Thời điểm trả thu nhập (Time of income payment): %>"></asp:Label>
                    Tháng(month)
                    <tlk:RadTextBox runat="server" ID="txtMonth"></tlk:RadTextBox>
                    Năm(Year)
                    <tlk:RadTextBox runat="server" ID="txtYear" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label15" runat="server" Text="<%$ Translate: 16. Tổng thu nhập chịu thuế đã trả (Total taxable income paid) %>"></asp:Label>
                    <tlk:RadNumericTextBox runat="server" ID="rnThuNhapQTT" ReadOnly="true" SkinID="Decimal"></tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label16" runat="server" Text="<%$ Translate: 17. Số thuế thu nhập cá nhân đã khấu trừ (Amount of personal income tax withheld) %>"></asp:Label>
                    <tlk:RadNumericTextBox runat="server" ID="rnThueTNCNQTT" ReadOnly="true" SkinID="Decimal"></tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label17" runat="server" Text="<%$ Translate: 18. Số thu nhập cá nhân còn được nhận (Amount of income received by individual)[(16) - (17)] %>"></asp:Label>
                    <tlk:RadNumericTextBox runat="server" ID="rnAmount" ReadOnly="true" SkinID="Decimal"></tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label18" runat="server" Text="<%$ Translate: Trạng thái %>"></asp:Label>
                    <tlk:RadTextBox runat="server" ID="txtStatus" ReadOnly="true" SkinID="Readonly"></tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
