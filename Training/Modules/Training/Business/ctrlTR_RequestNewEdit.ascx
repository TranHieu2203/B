<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_RequestNewEdit.ascx.vb"
    Inherits="Training.ctrlTR_RequestNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidSenderID" runat="server" />
<asp:HiddenField ID="hidSenderTitle" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />

<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="Y">
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
                    <%# Translate("Ngày gửi yêu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdRequestDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="rqRequestDate" ControlToValidate="rdRequestDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi yêu cầu %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi yêu cầu %>">
                    </asp:RequiredFieldValidator>
                </td>
                 <td class="lb">
                    <%# Translate("Năm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" CausesValidation="false"
                        AutoPostBack="true" Width="80px">
                    </tlk:RadNumericTextBox>
                    <div style="float: right">
                        <tlk:RadButton ID="cbIrregularly" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" Visible="false"
                            Text="<%$ Translate: Đột xuất%>" CausesValidation="false" AutoPostBack="true">
                        </tlk:RadButton>
                    </div>
                    <asp:RequiredFieldValidator ID="reqYear" ControlToValidate="rntxtYear" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Năm đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
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
            </tr>
            <tr>
                

                <td class="lb">
                    <%# Translate("Người gửi")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSender" ReadOnly="true" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindSender" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtSender"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Người gửi %>" ToolTip="<%$ Translate: Bạn phải chọn Người gửi %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Email người gửi")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSenderMail" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Điện thoại người gửi")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtSenderMobile" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã YCĐT")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtRequestCode" >
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtRequestCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã YCTD %>" ToolTip="<%$ Translate: Bạn phải nhập Mã YCTD %>">
                    </asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPlan" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbOtherCourse" Visible="false">
                        <%# Translate("Khóa đào tạo khác")%>
                    </asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOtherCourse" Visible="false">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqOtherCourse" ControlToValidate="txtOtherCourse"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Khóa đào tạo khác %>" ToolTip="<%$ Translate: Bạn phải nhập Khóa đào tạo khác %>">
                    </asp:RequiredFieldValidator>
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
                    <%# Translate("Hình thức đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTrainForm" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusHinhThuc" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn hình thức đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn hình thức đào tạo %>" ClientValidationFunction="cusHinhThuc">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tính chất nhu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPropertiesNeed" CausesValidation="false" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusCourse" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Tính chất nhu cầu %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tính chất nhu cầu %>" ClientValidationFunction="cusCourse">
                    </asp:CustomValidator>
                </td>
               
            </tr>
            <tr>
                 <td class="lb">
                    <%# Translate("Nơi đào tạo")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtTrPlace" Width="100%">
                    </tlk:RadTextBox>
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
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkCertificate" Text="<%$ Translate: Bằng cấp/Chứng chỉ đạt được %>" />
                </td>
            </tr>
            <%--------------------%>
            <tr>
                <td class="lb">
                    <%# Translate("Mục tiêu")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtTargetTrain" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkCommit" Text="<%$ Translate: Cam kết đào tạo %>" />
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Nhóm chương trình")%>
                </td>
                <td style="display: none"> 
                    <tlk:RadTextBox runat="server" ID="txtProgramGroup" ReadOnly="true">
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
                    <%# Translate("Thời gian dự kiên đến")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdExpectDateTo" >
                    </tlk:RadDatePicker>
                </td>
                <td class="lb" style="display:none">
                    <%# Translate("Thời gian bắt đầu")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td style="display:none">
                    <tlk:RadDatePicker runat="server" ID="rdStartDate">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thời gian bắt đầu %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thời gian bắt đầu %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Trung tâm đào tạo")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboLstCenter" CausesValidation="false" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Giảng viên")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboLstTeacher" CausesValidation="false" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
                </td>
            </tr>
            <%----------------------------%>
            <tr>
                <td class="lb">
                    <%# Translate("Số học viên dự kiến")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnTrainerNumber" SkinID="Number" NumberFormat-AllowRounding="true" NumberFormat-DecimalDigits="0">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí dự kiến")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtExpectedCost" MinValue="0" NumberFormat-GroupSeparator=",">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị tiền tệ")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboCurrency">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display:none">
                    <%# Translate("Đơn vị chủ trì đào tạo")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td style="display:none">
                    <tlk:RadComboBox runat="server" ID="cboUnits" CausesValidation="false">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ClientValidationFunction="cusPlan">
                    </asp:CustomValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <%--<td class="lb">
                    <%# Translate("File đính kèm")%>
                </td>
                <td>
                    <tlk:RadButton ID="btnUploadFile" runat="server" Text="<%$ Translate: Upload %>"
                        CausesValidation="false" >
                    </tlk:RadButton>
                    <asp:Label ID="lblFilename" runat="server" Text=""></asp:Label>
                </td>--%>
                 <td class="lb">
                    <asp:Label runat="server" ID="lbUploadFile" Text="<%$ Translate: Tập tin đính kèm %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server" Width="30px">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboStatus">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr style="display:none">
                <td class="lb">
                    <%# Translate("Địa điểm tổ chức")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtVenue" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin học viên")%>
                    <div style="float: right;">
                        <%# Translate("Số lượng học viên thực tế: ")%>
                        <b>
                            <asp:Label runat="server" ID="lblNumOfRealTrainee" Text="0"></asp:Label>
                            <%--/
                            <asp:Label runat="server" ID="lblNumOfPlanTrainee" Text="0"></asp:Label></b>--%>
                    </div>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td colspan="4">
                    <tlk:RadButton ID="btnAdd" runat="server" Text="<%$ Translate: Thêm học viên %>"
                        CausesValidation="false" >
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnRemove" runat="server" Text="<%$ Translate: Xóa học viên %>"
                        CausesValidation="false" >
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnExport" runat="server" Text="<%$ Translate: Xuất file mẫu %>"
                        CausesValidation="false" OnClientClicking="btnExportClicking" >
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnImport" runat="server" Text="<%$ Translate: Nhập file mẫu %>"
                        CausesValidation="false" >
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
        <div style="margin-left: 10px; height: 200px">
            <tlk:RadGrid ID="rgData" runat="server" Height="200px" Width="99%">
                <MasterTableView DataKeyNames="EMPLOYEE_ID" ClientDataKeyNames="EMPLOYEE_ID">
                    <Columns>
                        <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        </tlk:GridClientSelectColumn>
                        <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                            SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="EMPLOYEE_NAME"
                            SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                        <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày sinh %>" DataField="BIRTH_DATE"
                            SortExpression="BIRTH_DATE" UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </tlk:GridDateTimeColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                            SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="COM_NAME" SortExpression="COM_NAME"
                            UniqueName="COM_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                            SortExpression="ORG_NAME" UniqueName="ORG_NAME" />
                    </Columns>
                </MasterTableView>
            </tlk:RadGrid>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
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
        function cusCourse(oSrc, args) {
            var cbo = $find("<%# cboPropertiesNeed.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusHinhThuc(oSrc, args) {
            var cbo = $find("<%# cboTrainForm.ClientID %>");
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
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //                window.open('/Default.aspx?mid=Training&fid=ctrlTR_Request&group=Business', "_self");
            //            }
        }

        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlTR_RequestNewEdit_txtOrgName') {
                }
            }
        }, true);

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
