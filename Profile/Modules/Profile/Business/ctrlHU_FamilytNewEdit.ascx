<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_FamilytNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_FamilytNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgCode" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<asp:HiddenField ID="hidWorkStatus" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidSign2" runat="server" />
<asp:HiddenField ID="hidPeriod" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidLinkNPT" runat="server" />
<asp:HiddenField ID="hidLinkFamily" runat="server" />
<style type="text/css">
    /*div.RadToolBar .rtbUL{
        text-align: right !important;
    }*/
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarFamily" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" >
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 140px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" AutoPostBack="true" runat="server" Width="130px">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEmployeeName" runat="server" Text="<%$ Translate: Họ tên %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbTITLE" runat="server" Text="<%$ Translate: Vị trí công việc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Đơn vị %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>

            </tr>



            <%--============================================--%>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin gia cảnh người thân")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRelationship" runat="server" Text="Mối quan hệ"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRelationship">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboRelationship"
                        runat="server" ErrorMessage="Bạn phải chọn Mối quan hệ" ToolTip="Bạn phải chọn Mối quan hệ">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbb" Text="Cùng công ty"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkis_same_company" runat="server" AutoPostBack="false" />
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCopyEmployeeAddress" Text="Sao chép địa chỉ NV"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkCopyEmployeeAddress" runat="server" AutoPostBack="True" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbFullName" runat="server" Text="Họ tên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFullName">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                        ErrorMessage="Bạn phải nhập họ tên" ToolTip="Bạn phải nhập họ tên">
                    </asp:RequiredFieldValidator>
                </td>


                <td class="lb">
                    <asp:Label ID="lbBirthDate" runat="server" Text="Ngày sinh"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdBirthDate"
                        runat="server" ErrorMessage="Bạn phải nhập Ngày sinh" ToolTip="Bạn phải nhập Ngày sinh">
                    </asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <%# Translate("Giới tính")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboGender" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboGender"
                        runat="server" ErrorMessage="Bạn phải chọn giới tính" ToolTip="Bạn phải chọn giới tính">
                    </asp:RequiredFieldValidator>
                </td>


                <td style="display: none">
                    <asp:Label ID="lbNguyenQuan" runat="server" Text="Nguyên quán"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox runat="server" ID="cboNguyenQuan">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>

                <td class="lb">
                    <asp:Label ID="lbIDNO" runat="server" Text="Số CMND"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIDNO" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>

                <td class="lb">
                    <%# Translate("Ngày cấp CMND")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdIDDate">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Nơi cấp CMND")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtIDPlace" SkinID="Textbox15">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <%--  -------------------------------------------------------------%>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Chủ hộ"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkHousehold" runat="server" AutoPostBack="true" />
                </td>

                  <td class="lb">
                    <asp:Label ID="Label14" runat="server" Text="Mối quan hệ với chủ hộ"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRELATE_OWNER">
                    </tlk:RadComboBox>
                </td>


                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Số hộ khẩu"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSoHoKhau" SkinID="Textbox250" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Mã hộ gia đình"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMaHoGiaDinh" SkinID="Textbox250" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td style="display: none">
                    <asp:Label ID="Label4" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="RadTextBox1">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%--  -------------------------------------------------------------%>
            <tr>

                <td class="lb">
                    <asp:Label ID="lbCareer" runat="server" Text="Nghề nghiệp"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtCareer" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <%---------------------------------------------------------------%>
            <tr>
                <td class="lb">
                    <%# Translate("Số điện thoại")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPhone" />
                </td>

                <td class="lb">
                    <asp:Label ID="lbTax" runat="server" Text="Mã số thuế"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTax">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp MST")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdMSTDate">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nơi cấp mã số thuế")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txt_MSTPLACE" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbAdresss" runat="server" Text="Địa chỉ thường trú:"></asp:Label>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox runat="server" ID="txtAdress1" Width="100%" />
                </td>
                <td class="lb">
                    <%# Translate("Quốc gia")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNationlity" SkinID="LoadDemand" AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>


                   <td class="lb">
                    <asp:Label ID="Label15" runat="server" Text="Dân tộc"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNATIVE">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Tỉnh/TP"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProvince_City1" SkinID="LoadDemand" runat="server" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="Quận/Huyện"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDistrict1" SkinID="LoadDemand" runat="server" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbBangCap" Text="Phường/Xã"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommune1" SkinID="LoadDemand" runat="server" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label8" runat="server" Text="Địa chỉ tạm trú: "></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtAdress_TT" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="Tỉnh/TP"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboProvince_City2" SkinID="LoadDemand" runat="server" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label10" Text="Quận/Huyện"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDistrict2" SkinID="LoadDemand" runat="server" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label11" Text="Phường/Xã"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCommune2" SkinID="LoadDemand" runat="server" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>

            </tr>
            <%--  -------------------------------------------------------------%>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label12" runat="server" AutoPostBack="true"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkDaMat" AutoPostBack="true" Text="Đã mất" />
                </td>
                <td class="lb" style="display: none">
                    <asp:Label ID="Label7" runat="server" Text="Thôn/Ấp"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtHamlet1" Width="100%" />
                </td>
            </tr>
            <tr>
                <td class="lb"></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkIsDeduct" AutoPostBack="true" Text="Đối tượng giảm trừ" />
                </td>
            </tr>
            <tr runat="server" id="hid_Isdeduct" visible="false">
                <td class="lb">
                    <asp:Label ID="lbDeductReg" runat="server" Text="Ngày đăng ký giảm trừ"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductReg" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDeductFrom" runat="server" Text="Ngày bắt đầu"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductFrom" Enabled="false" AutoPostBack="true">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqDeductFrom" ControlToValidate="rdDeductFrom" Enabled="false"
                        runat="server" ErrorMessage="Bạn phải chọn Ngày bắt đầu" ToolTip="Bạn phải chọn Ngày bắt đầu">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDeductTo" runat="server" Text="Ngày kết thúc"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDeductTo" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            
            <tr>
                <td class="lb">
                    <asp:Label ID="lbUploadNPT" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUploadNPT" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFileNPT" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFileNPT" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownloadNPT" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnViewNPT" runat="server" Text="<%$ Translate: Xem ảnh%>"
                        CausesValidation="false" OnClientClicked="ViewImageNPTClick" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td colspan="7" style="color: red">
                    <asp:Label runat="server" ID="lbNKS" Text="Nơi đăng ký khai sinh:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giấy khai sinh")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtBIRTH_CODE">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Quyển")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtQuyen">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Quốc tịch")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboNATIONALITYFAMILY" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting" AutoPostBack="true">
                    </tlk:RadComboBox>
                </td>
            </tr>

            <tr>

                <td class="lb">
                    <%# Translate("Tỉnh/Thành phố")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtPROVINCE_ID" SkinID="LoadDemand" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Quận/Huyện")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtDISTRICT_ID" SkinID="LoadDemand" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Xã/Phường")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbTempKtWARD_ID" SkinID="LoadDemand" Width="160px" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting">
                    </tlk:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label ID="Label13" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%" Height="35px" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbUploadFamily" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUploadFamily" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFileFamily" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFileFamily" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownloadFamily" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnViewFamily" runat="server" Text="<%$ Translate: Xem ảnh%>"
                        CausesValidation="false" OnClientClicked="ViewImageFamilyClick" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_FamilytNewEdit_RadSplitter1');
        });
        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboProvince_City1.ClientID %>':
                    cbo = $find('<%= cboDistrict1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboCommune1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboDistrict1.ClientID %>':
                    cbo = $find('<%= cboCommune1.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;

                case '<%= cboProvince_City2.ClientID %>':
                    cbo = $find('<%= cboDistrict2.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboCommune2.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboDistrict2.ClientID %>':
                    cbo = $find('<%= cboCommune2.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;

                case '<%= cbTempKtPROVINCE_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtDISTRICT_ID.ClientID%>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cbTempKtWARD_ID.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cbTempKtDISTRICT_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtWARD_ID.ClientID%>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
        }
        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {

                case '<%= cboDistrict1.ClientID %>':
                    cbo = $find('<%= cboProvince_City1.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboCommune1.ClientID %>':
                    cbo = $find('<%= cboDistrict1.ClientID %>');
                    value = cbo.get_value();
                    break;

                case '<%= cboDistrict2.ClientID %>':
                    cbo = $find('<%= cboProvince_City2.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboCommune2.ClientID %>':
                    cbo = $find('<%= cboDistrict2.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cbTempKtDISTRICT_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtPROVINCE_ID.ClientID%>');
                    value = cbo.get_value();
                    break;
                case '<%= cbTempKtWARD_ID.ClientID%>':
                    cbo = $find('<%= cbTempKtDISTRICT_ID.ClientID%>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();

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


        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'SAVE') {
                debugger;
                for (var i = 0; i < document.getElementsByClassName("rtbBtn").length; i++) {
                    if (document.getElementsByClassName("rtbBtn")[i].outerText == "Lưu") {
                        document.getElementsByClassName("rtbBtn")[i].style.pointerEvents = "none"
                        break;
                    }
                }
            }
        }
        function ViewImageNPTClick(sender, eventArgs) {
            debugger;
            var url = document.getElementById("<%=hidLinkNPT.ClientID %>").value;
            var oWnd = $find('ctl00_MainContent_rwMainPopup');
            oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=' + url);
            oWnd.show();
        }
        function ViewImageFamilyClick(sender, eventArgs) {
            debugger;
            var url = document.getElementById("<%=hidLinkFamily.ClientID %>").value;
            var oWnd = $find('ctl00_MainContent_rwMainPopup');
            oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=' + url);
            oWnd.show();
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function OnKeyPress(sender, eventArgs) 
        { 
           var c = eventArgs.get_keyCode(); 
           if (c == 13) 
           { 
             document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_FamilytNewEdit_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
        function <%=ClientID%>_OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if(arg == null)
            {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>
