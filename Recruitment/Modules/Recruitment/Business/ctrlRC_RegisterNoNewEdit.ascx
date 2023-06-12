<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_RegisterNoNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_RegisterNoNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidRequietOrgID" runat="server" />
<asp:HiddenField ID="hidPerfromOrgID" runat="server" />
<asp:HiddenField ID="hidRequiredID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Năm khai báo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboYear" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusYear" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn năm khai báo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn năm khai báo %>" ClientValidationFunction="cusYear">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Phiếu yêu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRequired" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="RadButton1" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtRequired"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitle" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lương định biên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmNUMBER_PLAN" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lương lao động hiện tại")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmNUMBER_NOW" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày tạo mã tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdRC_CREATE_DATE" runat="server" SkinID="Number">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Số lương cần tuyển")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmNUMBER_MUST" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Kiểm tra định biên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRC_CHECK" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("SL tuyển dụng cho 1 mã chi tiết")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmNUMBER_REQUIRED_DTL" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số mã tuyển dụng chi tiết")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmREQUIRED_ID_DTL" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian cần tuyển")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdREQUIRED_DATE" runat="server" SkinID="Number">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại hình tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTYPE_REQUIRED" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Giới tính")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboGENDER" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Độ tuổi")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmFromAge" runat="server" Width="75px" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    -
                    <tlk:RadNumericTextBox ID="nmToAge" runat="server" Width="75px" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trình độ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLEARNING_LEVEL" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Chuyên ngành")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMAJOR" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Kinh nghiệm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmEXPERIENCE" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đv y/c tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtREQUEST_ORG" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtREQUEST_ORG"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đv thực hiện tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPERFORM_ORG" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg2" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPERFORM_ORG"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Môn thi 1")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEXAM1" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Môn thi 2")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEXAM2" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Môn thi 3")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEXAM3" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kỹ năng")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtSKILLS" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrgRe" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrgPe" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusYear(oSrc, args) {
            var cbo = $find("<%# cboYear.ClientID %>");
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
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }
    </script>
</tlk:RadCodeBlock>
