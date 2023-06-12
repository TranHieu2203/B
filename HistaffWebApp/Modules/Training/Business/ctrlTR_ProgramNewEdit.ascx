<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTR_ProgramNewEdit.ascx.vb"
    Inherits="Training.ctrlTR_ProgramNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidPlanID" runat="server" />
<asp:HiddenField ID="HiddenField1" runat="server" />
<asp:HiddenField ID="hidRequestID" runat="server" />
<asp:HiddenField ID="hidCourseID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="HidEmpId1" runat="server" />
<asp:HiddenField ID="HidEmpId2" runat="server" />
<asp:HiddenField ID="HidEmpId3" runat="server" />

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
                    <%# Translate("Khai báo yêu cầu đào tạo chi tiết")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Năm đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtYear" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="" MinValue="1900" MaxLength="2999" CausesValidation="false"
                        AutoPostBack="true" ShowSpinButtons="true">
                    </tlk:RadNumericTextBox>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Loại khóa đào tạo")%>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsPlan" AutoPostBack="true" CausesValidation="false" runat="server" Text="<%$ Translate: Theo nhu cầu %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtProgramCode">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqProgramCode" ControlToValidate="txtProgramCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã khóa đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập Mã khóa đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Khóa đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboPlan" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqcboPlan" ControlToValidate="cboPlan" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Khóa đào tạo %>" ToolTip="<%$ Translate: Bạn phải chọn Khóa đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Lĩnh vực đào tạo")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTrainField" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                    <tlk:RadComboBox runat="server" ID="cboTrainField" CausesValidation="false" Enabled="false" Visible="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hình thức")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboHinhThuc" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqcboHinhThuc" ControlToValidate="cboHinhThuc" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn hình thức %>" ToolTip="<%$ Translate: Bạn phải chọn hình thức %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tính chất nhu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboTinhchatnhucau" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqcboTinhchatnhucau" ControlToValidate="cboTinhchatnhucau"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn tính chất nhu cầu %>"
                        ToolTip="<%$ Translate: Bạn phải chọn tính chất nhu cầu %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Nơi đào tạo")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtVenue" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nội dung đào tạo")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtContent" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
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
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Tên chương trình đào tạo")%><span class="lbReq">*</span>
                </td>
                <td colspan="3" style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtName" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Nhóm chương trình")%>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtNhomCT" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thời gian đào tạo")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Thời lượng")%><span class="lbReq">*</span>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="rntxtDuration" runat="server" NumberFormat-GroupSeparator="."
                        NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="1" MinValue="0"
                        Width="75px">
                    </tlk:RadNumericTextBox>
                    <tlk:RadComboBox runat="server" ID="cboDurationType" Width="80px">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Từ ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Từ ngày %>" ToolTip="<%$ Translate: Bạn phải nhập Từ ngày %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdEndDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Đến ngày %>" ToolTip="<%$ Translate: Bạn phải nhập Đến ngày %>">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdEndDate"
                        ControlToCompare="rdStartDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"
                        ToolTip="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Đăng ký portal")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkIsPublic" AutoPostBack="true" runat="server" Text="<%$ Translate: Public portal %>" />
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái public portal")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboPublicStatus" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbPortalFrom" runat="server" Text="Thời hạn đăng ký portal từ" Visible="false"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdPortalFrom" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbPortalTo" runat="server" Text="Thời hạn đăng ký portal đến" Visible="false"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdPortalTo" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="rdPortalTo"
                        ControlToCompare="rdPortalFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Thời hạn đăng ký portal đến phải lớn hơn Thời hạn đăng ký portal từ %>"
                        ToolTip="<%$ Translate: Thời hạn đăng ký portal đến phải lớn hơn Thời hạn đăng ký portal từ %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Thời gian học")%>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox ID="cboDurationStudy" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Trong giờ HC")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="rntxtDurationHC" runat="server" NumberFormat-GroupSeparator="."
                        NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="1" MinValue="0">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Ngoài giờ HC")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="rntxtDurationOT" runat="server" NumberFormat-GroupSeparator="."
                        NumberFormat-DecimalSeparator="," NumberFormat-DecimalDigits="1" MinValue="0">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Chi phí đào tạo")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng học viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtNumberStudent" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rntxtNumberStudent"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số lượng học viên %>" ToolTip="<%$ Translate: Bạn phải nhập Số lượng học viên %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td style="display: none"></td>
                <td colspan="4" valign="middle" style="margin-left: 20px; display: none;">
                    <tlk:RadButton ID="btnFindOrgCost" runat="server" CausesValidation="false" Text="Thêm phòng ban" />
                    <tlk:RadButton ID="btnDel" runat="server" CausesValidation="false" Text="Xóa phòng ban" />
                    <tlk:RadButton ID="btnCalCost" runat="server" CausesValidation="false" Text="Tính chi phí chi tiết" />
                    <asp:Label ID="lblDVT" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lớp đào tạo")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnExpectClass" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Chi phí đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostCompany" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rntxtCostCompany"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập chi phí đào tạo %>" ToolTip="<%$ Translate: Bạn phải nhập chi phí đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị tiền tệ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboUnitPrice" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin giảng viên đào tạo")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Trung tâm đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCenters" runat="server" Width="100%" CheckBoxes="true" AutoPostBack="True" EnableCheckAllItemsCheckBox="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboCenters"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn trung tâm đào tạo %>" ToolTip="<%$ Translate: Bạn phải chọn trung tâm đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Giảng viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTeachers" runat="server" Width="100%" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboTeachers"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Giảng viên %>" ToolTip="<%$ Translate: Bạn phải chọn Giảng viên %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tài liệu đào tạo")%>
                </td>
                <td>
                    <tlk:RadButton ID="btnUploadFile" runat="server" Text="<%$ Translate: Upload %>"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                    <asp:Label ID="lblFilename" runat="server" Text=""></asp:Label>
                    <asp:HiddenField ID="hidUploadFile" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <asp:CheckBox ID="chkThilai" runat="server" Text="<%$ Translate: Có thi lại? %>" />
                </td>
                <td style="display: none">
                    <asp:CheckBox ID="chkIsReimburse" runat="server" Text="<%$ Translate: Có bồi hoàn? %>" />
                </td>
                <td colspan="4" rowspan="5" style="display: none">
                    <div id="divGrid" runat="server" style="float: left; width: 560px; height: 150px; margin-left: 20px">
                        <tlk:RadGrid ID="rgChiPhi" runat="server" AutoGenerateColumns="true" AllowPaging="false"
                            AllowSorting="false" AllowMultiRowSelection="false" CellSpacing="0" GridLines="Vertical"
                            Width="100%" Height="150px">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <PagerStyle Visible="false" />
                            <MasterTableView DataKeyNames="ORG_ID,ORG_NAME,COST_COMPANY" ClientDataKeyNames="ORG_ID,ORG_NAME,COST_COMPANY">
                                <Columns>
                                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    </tlk:GridClientSelectColumn>
                                    <tlk:GridBoundColumn DataField="ORG_ID" Visible="false" />
                                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="ORG_NAME"
                                        SortExpression="ORG_NAME" UniqueName="ORG_NAME">
                                    </tlk:GridBoundColumn>
                                    <tlk:GridTemplateColumn HeaderText="<%$ Translate: Chi phí đào tạo %>" DataField="COST_COMPANY"
                                        SortExpression="COST_COMPANY" UniqueName="COST_COMPANY" HeaderStyle-Width="150px">
                                        <ItemTemplate>
                                            <tlk:RadNumericTextBox ID="ValueTS" runat="server" CausesValidation="false" Width="120px"
                                                Value='<%# CInt(Eval("COST_COMPANY")) %>'>
                                                <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
                                                    GroupSizes="3" />
                                            </tlk:RadNumericTextBox>
                                        </ItemTemplate>
                                    </tlk:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </tlk:RadGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Tổng chi phí đào tạo (USD)")%>
                </td>
                <td style="display: none">
                    <tlk:RadNumericTextBox ID="rntxtCostCompanyUS" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Chi phí/1 học viên (USD)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostOfStudentUS" runat="server" NumberFormat-DecimalDigits="0"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true" CausesValidation="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Chi phí/1 học viên (VNĐ)")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtCostOfStudent" runat="server" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSeparator="," MinValue="0" ReadOnly="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Đánh giá sau đào tạo")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="cboCertificate" AutoPostBack="true" CausesValidation="false" runat="server" Text="<%$ Translate: Bằng cấp/Chứng chỉ đạt được %>" />
                </td>
                
                
                <td class="lb">
                    <asp:Label runat="server" ID="lbCertificate"  Visible="false"><%# Translate("Tên bằng cấp/Chứng chỉ")%><span class="lbReq">*</span>
                    </asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtCertificateName"  Visible="false">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkAfterTrain" AutoPostBack="true" runat="server" Text="<%$ Translate: Đánh giá sau đào tạo %>" />
                </td>
                 <td class="lb">
                     <asp:Label runat="server" ID="blbAss_Date"  Visible="false"> <%# Translate("Hạn chót đánh giá")%>
                    </asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdASS_DATE" Visible="false" runat="server">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rdEndDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Đến ngày %>" ToolTip="<%$ Translate: Bạn phải nhập Đến ngày %>">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="rdEndDate"
                        ControlToCompare="rdStartDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"
                        ToolTip="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkTrainCommit" runat="server" Text="<%$ Translate: Cam kết đào tạo %>" />
                </td>
                <td class="lb">
                    <%# Translate("Loại đào tạo")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTrainType" runat="server" Width="100%" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqTrainType" ControlToValidate="cboTrainType"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại đào tạo %>" ToolTip="<%$ Translate: Bạn phải chọn Loại đào tạo %>">
                    </asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat ="server" ID="lbDateReview1" Visible="false"><%# Translate("Ngày hết hạn đánh giá 1")%></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdReviewDate1" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID="lbDateReview2" Visible="false" ><%# Translate("Ngày hết hạn đánh giá 2")%></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdReviewDate2" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat ="server" ID="lbDateReview3" Visible="false"><%# Translate("Ngày hết hạn đánh giá 3")%></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdReviewDate3" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeCode1" Text="Người đánh giá 1" Visible="false"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode1" runat="server" Width="130px" AutoPostBack="true" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee1" runat="server" SkinID="ButtonView" CausesValidation="false" Visible="false">
                    </tlk:RadButton>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeCode2" Text="Người đánh giá 2" Visible="false"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode2" runat="server" Width="130px" AutoPostBack="true" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee2" runat="server" SkinID="ButtonView" CausesValidation="false" Visible="false">
                    </tlk:RadButton>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeCode3" Text="Người đánh giá 3" Visible="false"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode3" runat="server" Width="130px" AutoPostBack="true" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee3" runat="server" SkinID="ButtonView" CausesValidation="false" Visible="false">
                    </tlk:RadButton>
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
                    <tlk:RadComboBox ID="cboTitleGroup" runat="server" Width="100%" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitle" runat="server" Width="100%" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <%--<asp:RequiredFieldValidator ID="reqTitle" ControlToValidate="cboTitle" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Chức danh %>" ToolTip="<%$ Translate: Bạn phải chọn Chức danh %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="4">
                    <asp:Label runat="server" ID="lblMessage" Text="Danh sách học viên tham gia" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr style="display: none">
                <td></td>
                <td colspan="2">
                    <tlk:RadListBox ID="lstPartDepts" runat="server" Height="100px" Width="100%">
                    </tlk:RadListBox>
                </td>
                <td colspan="2">
                    <tlk:RadListBox ID="lstPositions" runat="server" Height="100px" Width="100%">
                    </tlk:RadListBox>
                </td>
                <td>
                    <tlk:RadListBox ID="lstWork" runat="server" Height="100px" Width="100%">
                    </tlk:RadListBox>
                </td>
            </tr>
            <tr style="display: none">
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin nhà cung cấp")%>
                    <hr />
                </td>
            </tr>
            <tr style="display: none">
                <td class="lb">
                    <%# Translate("Ngôn ngữ giảng dạy")%>
                </td>
                <td colspan="2">
                    <tlk:RadComboBox ID="cboLanguage" runat="server" Width="100%">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị chủ trì đào tạo")%>
                </td>
                <td colspan="2">
                    <tlk:RadComboBox ID="cboUnit" runat="server" AutoPostBack="true" CausesValidation="false"
                        Width="100%">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr style="display: none">
                <td></td>
                <td colspan="3">
                    <%# Translate("Trung tâm đào tạo")%>
                </td>
                <td>
                    <%# Translate("Giảng viên")%>
                </td>
                <%--<td class="lb">
                    <asp:CheckBox ID="chkIsLocal" runat="server" Text="<%$ Translate: Trong công ty %>"
                        AutoPostBack="true" CausesValidation="false" />
                </td>--%>
            </tr>
            <tr>
            </tr>
            <tr style="display: none">
                <td></td>
                <td colspan="3">
                    <tlk:RadListBox runat="server" ID="lstCenter" Width="100%" Height="100px" CheckBoxes="true"
                        OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging" />
                </td>
                <td colspan="2">
                    <tlk:RadListBox runat="server" ID="lstLecture" Width="100%" Height="100px" CheckBoxes="true"
                        OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td colspan="4">
                    <tlk:RadButton ID="btnExport" runat="server" Text="<%$ Translate: Xuất file mẫu %>"
                        CausesValidation="false" OnClientClicking="btnExportClicking" >
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnImport" runat="server" Text="<%$ Translate: Nhập file mẫu %>"
                        CausesValidation="false" >
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td></td>
                <td rowspan="4" colspan="5">
                    <tlk:RadGrid ID="rgEmployee" AllowPaging="false" PageSize="50" runat="server" EditMode="InPlace" Height="405px" Width="920px">
                        <MasterTableView DataKeyNames="ID,EMP_TYPE" ClientDataKeyNames="" CommandItemDisplay="Top">
                            <CommandItemStyle Height="25px" />
                            <CommandItemTemplate>
                                <div style="padding: 2px 0 0 0">
                                    <div style="float: left">
                                        <tlk:RadButton Width="72px" ID="btnEmployee" runat="server" Text="Thêm" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/add.png"
                                            CausesValidation="false" CommandName="FindEmployee" TabIndex="3">
                                        </tlk:RadButton>
                                    </div>
                                    <div style="float: right">
                                        <tlk:RadButton Width="70px" ID="btnDeleteEmp" runat="server" Text="Xóa" CausesValidation="false" Icon-PrimaryIconUrl="~/Static/Images/Toolbar/delete.png"
                                            CommandName="DeleteEmployee" TabIndex="3">
                                        </tlk:RadButton>
                                    </div>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                </tlk:GridClientSelectColumn>
                                <tlk:GridBoundColumn DataField="ID" UniqueName="ID" SortExpression="ID" Visible="false"
                                    ReadOnly="true" />
                                <tlk:GridBoundColumn HeaderText="Mã NV" DataField="EMPLOYEE_CODE" HeaderStyle-Width="100px"
                                    ReadOnly="true" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ItemStyle-HorizontalAlign="Center" />
                                <tlk:GridBoundColumn HeaderText="Họ tên" DataField="FULLNAME_VN" UniqueName="FULLNAME_VN"
                                    HeaderStyle-Width="150px" ReadOnly="true" SortExpression="FULLNAME_VN" ItemStyle-HorizontalAlign="Center" />
                                <tlk:GridDateTimeColumn HeaderText="Ngày vào công ty" DataField="JOIN_DATE" UniqueName="JOIN_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="JOIN_DATE" HeaderStyle-Width="150px">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </tlk:GridDateTimeColumn>
                                <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                    HeaderStyle-Width="150px" ReadOnly="true" SortExpression="ORG_NAME" ItemStyle-HorizontalAlign="Center" />
                                <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME_VN" UniqueName="TITLE_NAME_VN"
                                    HeaderStyle-Width="150px" ReadOnly="true" SortExpression="TITLE_NAME_VN" ItemStyle-HorizontalAlign="Center" />
                                <tlk:GridBoundColumn HeaderText="Nhóm chức danh" DataField="TITLE_GROUP_NAME" UniqueName="TITLE_GROUP_NAME"
                                    HeaderStyle-Width="150px" ReadOnly="true" SortExpression="TITLE_GROUP_NAME" ItemStyle-HorizontalAlign="Center" />
                                <tlk:GridBoundColumn HeaderText="Loại học viên" DataField="EMP_TYPE_NAME" UniqueName="EMP_TYPE_NAME"
                                    HeaderStyle-Width="150px" ReadOnly="true" SortExpression="EMP_TYPE_NAME" ItemStyle-HorizontalAlign="Center" />
                                <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="REGISTER_TRAINING_STATUS" UniqueName="REGISTER_TRAINING_STATUS"
                                    HeaderStyle-Width="150px" ReadOnly="true" SortExpression="REGISTER_TRAINING_STATUS" ItemStyle-HorizontalAlign="Center" />
                                
                            </Columns>
                        </MasterTableView>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </tlk:RadGrid>
                </td>
            </tr>

        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindPlan" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmp1" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmp2" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindEmp3" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var chk = $("#<%# chkIsPlan.ClientID%>");
            var paramsType = new URL(location.href).searchParams.get("typeConfirm");
            if (chk.is(":checked") === false && paramsType == "NC") {
                chk.click();
            }
        });
        function btnExportClicking(sender, args) {
            enableAjax = false;
        }
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $find("<%# RadPane2.ClientID%>")._scrollLeft;
            yPos = $find("<%# RadPane2.ClientID%>")._scrollTop;
            console.log(yPos);
        }
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function EndRequestHandler(sender, args) {
            setTimeout(function () {
                $find("<%# RadPane2.ClientID%>").SetScrollPos(xPos, yPos);
            }, 10);
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
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
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
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
                if (e.target.id !== 'ctl00_MainContent_ctrlTR_ProgramNewEdit_txtOrgName') {
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
