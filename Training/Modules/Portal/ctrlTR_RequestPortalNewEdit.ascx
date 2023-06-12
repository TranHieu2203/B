<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_RequestPortalNewEdit.ascx.vb"
    Inherits="Training.ctrlTR_RequestPortalNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidSenderID" runat="server" />
<tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<table class="table-form">
    <tr>
        <td colspan="6">
            <b>
                <%# Translate("Thông tin chung")%></b>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Phòng ban")%><%--<span class="lbReq">*</span>--%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtOrgName" ReadOnly="true" Width="130px">
            </tlk:RadTextBox>
            <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                CausesValidation="false">
            </tlk:RadButton>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>--%>
        </td>
        <td class="lb">
            <%# Translate("Năm")%><%--<span>content</span> class="lbReq">*</span>--%>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" CausesValidation="false"
                AutoPostBack="true" Width="80px">
            </tlk:RadNumericTextBox>
            <div style="float: right">
                <tlk:RadButton ID="cbIrregularly" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton"
                    Text="<%$ Translate: Đột xuất%>" CausesValidation="false" AutoPostBack="true">
                </tlk:RadButton>
            </div>
            <%--<asp:RequiredFieldValidator ID="reqYear" ControlToValidate="rntxtYear" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm đào tạo %>">
                    </asp:RequiredFieldValidator>--%>
        </td>
        <td class="lb">
            <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboPlan" AutoPostBack="true" CausesValidation="false">
            </tlk:RadComboBox>
            <asp:CustomValidator ID="cusPlan" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
            </asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Nhóm chương trình")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtProgramGroup" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Hình thức đào tạo")%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboTrainForm" CausesValidation="false">
            </tlk:RadComboBox>
        </td>
        <td class="lb">
            <%# Translate("Tính chất nhu cầu")%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboPropertiesNeed" CausesValidation="false">
            </tlk:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Lĩnh vực đào tạo")%>
        </td>
        <td>
            <tlk:RadTextBox runat="server" ID="txtTrainField" ReadOnly="true">
            </tlk:RadTextBox>
        </td>
        <td class="lb">
            <%# Translate("Thời gian dự kiến")%><span class="lbReq">*</span>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdExpectedDate">
            </tlk:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdExpectedDate"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian dự kiến %>"
                ToolTip="<%$ Translate: Bạn phải nhập Thời gian dự kiến %>">
            </asp:RequiredFieldValidator>
        </td>
        <td class="lb">
            <%# Translate("Thời gian bắt đầu")%><%--<span class="lbReq">*</span>--%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdStartDate">
            </tlk:RadDatePicker>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian bắt đầu %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian bắt đầu %>">
                    </asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Nội dung đào tạo")%><span class="lbReq">*</span>
        </td>
        <td colspan="5">
            <tlk:RadTextBox runat="server" ID="txtContent" SkinID="Textbox1023" Width="100%">
            </tlk:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtContent"
                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Nội dung đào tạo %>"
                ToolTip="<%$ Translate: Bạn phải nhập Nội dung đào tạo %>">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="lb" rowspan="4">
            <%# Translate("Trung tâm đào tạo")%><%--<span class="lbReq">*</span>--%>
        </td>
        <td rowspan="4">
            <tlk:RadListBox runat="server" ID="lstCenter" Width="100%" Height="100px" CheckBoxes="true"
                AutoPostBack="true">
            </tlk:RadListBox>
            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
        </td>
        <td class="lb" rowspan="4">
            <%# Translate("Giảng viên")%><%--<span class="lbReq">*</span>--%>
        </td>
        <td rowspan="4">
            <tlk:RadListBox runat="server" ID="lstTeacher" Width="100%" Height="100px" CheckBoxes="true">
            </tlk:RadListBox>
            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
        </td>
        <td class="lb">
            <%--<%# Translate("Đơn vị chủ trì đào tạo")%><span class="lbReq">*</span>--%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboUnits" CausesValidation="false" Visible=false>
            </tlk:RadComboBox>
            <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="lb">
       <%--     <%# Translate("Chi phí dự kiến")%>--%>
        </td>
        <td>
            <tlk:RadNumericTextBox runat="server" ID="rntxtExpectedCost" MinValue="0" NumberFormat-GroupSeparator="," Visible=false>
            </tlk:RadNumericTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
        <%--    <%# Translate("Đơn vị tiền tệ")%>--%>
        </td>
        <td>
            <tlk:RadComboBox runat="server" ID="cboCurrency" Visible=false>
            </tlk:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Mục tiêu")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox runat="server" ID="txtTargetTrain" SkinID="Textbox1023" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Địa điểm tổ chức")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox runat="server" ID="txtVenue" SkinID="Textbox1023" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ngày gửi yêu cầu")%><%--<span class="lbReq">*</span>--%>
        </td>
        <td>
            <tlk:RadDatePicker runat="server" ID="rdRequestDate">
            </tlk:RadDatePicker>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdRequestDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi yêu cầu %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi yêu cầu %>">
                    </asp:RequiredFieldValidator>--%>
        </td>
        <td class="lb">
            <%# Translate("File đính kèm")%>
        </td>
        <td colspan="3">
            <tlk:RadButton ID="btnUploadFile" runat="server" Text="<%$ Translate: Upload %>"
                CausesValidation="false" Style="padding-right: 20px">
            </tlk:RadButton>
            <asp:Label ID="lblFilename" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="lb">
            <%# Translate("Ghi chú")%>
        </td>
        <td colspan="5">
            <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%">
            </tlk:RadTextBox>
        </td>
    </tr>
</table>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function cusPlan(oSrc, args) {
            var cbo = $find("<%# cboPlan.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
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

        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                                getRadWindow().close(null);
            //                                args.set_cancel(true);
            //            }
        }

    </script>
</tlk:RadCodeBlock>
