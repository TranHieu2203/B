<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_PlanNewEdit.ascx.vb"
    Inherits="Training.ctrlTR_PlanNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidRequestID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />

<style type="text/css">
    div.RadComboBox_Office2007 {
        height: 22px;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqYear" ControlToValidate="rntxtYear" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Loại kế hoạch")%>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkPlanType" Text="Theo nhu cầu" 
                         AutoPostBack="true" CausesValidation ="false"/>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 140px">
                    <%# Translate("Mã kế hoạch đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPlanCode">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqPlanCode" ControlToValidate="txtPlanCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã kế hoạch %>" ToolTip="<%$ Translate: Bạn phải nhập Mã kế hoạch %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 140px">
                    <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCourse" AutoPostBack="true" CausesValidation="false"
                        Width="285px">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCourse" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn khóa đào tạo %>" ClientValidationFunction="cusCourse">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtLinhvuc" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            
            <tr>
                <td class="lb">
                    <%# Translate("Hình thức")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboHinhThuc" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusHinhThuc" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn hình thức đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn khóa đào tạo %>" ClientValidationFunction="cusHinhThuc">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tính chất nhu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTinhchatnhucau" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTinhchatnhucau" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Tính chất nhu cầu %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tính chất nhu cầu %>" ClientValidationFunction="cusCourse">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Nơi đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtVenue" runat="server" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nội dung đào tạo")%><span class="lbReq">*</span>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox runat="server" ID="txtContent"  SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="rqContent" ControlToValidate="txtContent"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Nội dung đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Nội dung đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mục tiêu")%>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox runat="server" ID="txtTargetTrain"  SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Thời gian dự kiến từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpectFrom" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rqExpectFrom" ControlToValidate="rdExpectFrom"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thời gian dự kiến từ %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Thời gian dự kiến từ %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian dự kiến đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpectTo" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rqExpectTo" ControlToValidate="rdExpectTo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thời gian dự kiến đến %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Thời gian dự kiến đến %>">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="comExpectTo" runat="server" ControlToValidate="rdExpectTo"
                                Type="Date" ControlToCompare="rdExpectFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Thời gian dự kiến từ phải lớn hơn Thời gian dự kiến đến %>"
                                ToolTip="<%$ Translate: Thời gian dự kiến từ phải lớn hơn Thời gian dự kiến đến %>"></asp:CompareValidator>
                </td>
                <td></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkCommit" Text="Cam kết đào tạo" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng học viên dự kiến")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtStudents" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí đào tạo")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtTotal" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị tiền tệ")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCurrency" >
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lớp dự kiến đào tạo")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnExpectClass" runat="server" SkinID="Number" NumberFormat-AllowRounding="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Trung tâm đào tạo")%>
                </td>
                <td colspan="2">
                    <%--<tlk:RadListBox ID="lstCenter" runat="server" CheckBoxes="true" Height="100px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                        Width="100%" />--%>
                    <tlk:RadComboBox runat="server" ID="cboCenter" CheckBoxes="true">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkAfterTrain" Text="Đánh giá sau đào tạo" />
                </td>
                <td></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkCertificate" Text="Bằng cấp/ Chứng chỉ đạt được" AutoPostBack="true" CausesValidation="false"/>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCertificateName" Visible="false"><%# Translate("Tên bằng cấp/ Chứng chỉ")%><span class="lbReq">*</span></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCertificateName" Visible="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboType" AutoPostBack ="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="rqType" ControlToValidate="cboType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Loại đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID="lbDateReview1" Visible="false"><%# Translate("Ngày đến hạn đánh giá 1")%></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDateReview1" runat="server" Enabled="false" Visible="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID="lbDateReview2" Visible="false" ><%# Translate("Ngày đến hạn đánh giá 2")%></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDateReview2" runat="server" Enabled="false" Visible="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID="lbDateReview3" Visible="false"><%# Translate("Ngày đến hạn đánh giá 3")%></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDateReview3" runat="server" Enabled="false" Visible="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Danh sách nhóm đào tạo")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm chức danh")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitleGroup" runat="server" CheckBoxes="true" AutoPostBack="true"  CausesValidation="false" EnableCheckAllItemsCheckBox="true">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td colspan="3">
                    <%--<tlk:RadListBox ID="lstPositions" runat="server" Height="80px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                        Width="100%">
                    </tlk:RadListBox>--%>
                    <tlk:RadComboBox runat="server" ID="cboTitles" CheckBoxes="true"></tlk:RadComboBox>
                </td>
            </tr>
            <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="lbUploadFile" Text="<%$ Translate: Tập tin đính kèm %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="6">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="pgFindMultiEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindRequest" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        viewPopUp();
        function viewPopUp() {
            var chk = $("#<%# chkPlanType.ClientID%>");
            var paramsType = new URL(location.href).searchParams.get("typeConfirm");
            if (chk.is(":checked") === false && paramsType == "NC") {
                chk.click();
            }
        }
        function cusCourse(oSrc, args) {
            var cbo = $find("<%# cboCourse.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusHinhThuc(oSrc, args) {
            var cbo = $find("<%# cboHinhThuc.ClientID %>");
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

        function clientButtonClicking(sender, args) {

        }

        function OnClientValueChanged(sender, args) {

        }

        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlTR_PlanNewEdit_txtOrgName') {
                }
            }
        }, true);

        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
