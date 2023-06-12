<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPortalFamily_Edit.ascx.vb"
    Inherits="Profile.ctrlPortalFamily_Edit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidFamilyID" runat="server" />
<tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
<asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<div style="overflow: auto">
    <table class="table-form">
        <tr>
            <td class="lb" style="width: 150px">
                <%# Translate("Mối quan hệ")%><span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboRelationship" CausesValidation="false">
                </tlk:RadComboBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboRelationship"
                    runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Mối quan hệ %>" ToolTip="<%$ Translate: Bạn phải chọn Mối quan hệ  %>">
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
            <td class="lb" style="width: 150px">
                <%# Translate("Họ tên")%><span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtFullName">
                </tlk:RadTextBox>
                <asp:RequiredFieldValidator ID="reqFullname" ControlToValidate="txtFullName" runat="server"
                    ErrorMessage="<%$ Translate: Bạn phải nhập họ tên %>" ToolTip="<%$ Translate: Bạn phải nhập họ tên  %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb">
                <%# Translate("Ngày sinh")%>
                <span class="lbReq">*</span>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                </tlk:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdBirthDate"
                    runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày sinh %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày sinh %>">
                </asp:RequiredFieldValidator>
            </td>
            <td class="lb">
                <%# Translate("Giới tính")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboGender" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                    OnClientItemsRequesting="OnClientItemsRequesting">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Số CMND")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtIDNO" SkinID="Textbox15">
                </tlk:RadTextBox>
                <asp:CustomValidator ID="cvalIDNO" runat="server" ErrorMessage="<%$ Translate: Số CMND không được phép trùng %>"
                    ToolTip="<%$ Translate: Số CMND không được phép trùng %>">
                </asp:CustomValidator>
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
        <tr>
            <td class="lb">
                <%# Translate("Là chủ hộ")%>
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkIs_Owner" AutoPostBack="true" />
            </td>
            <td class="lb">
                <%# Translate("Số hộ khẩu")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtHouseCertificate_Num" />
            </td>
            <td class="lb">
                <%# Translate("Mã hộ gia đình")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtHouseCertificate_Code" />
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Nghề nghiệp")%>
            </td>
            <td colspan="5">
                <tlk:RadTextBox runat="server" ID="txtCareer" Width="100%">
                </tlk:RadTextBox>
            </td>
            <td style="display: none">
                <%# Translate("Chức danh")%>
            </td>
            <td style="display: none">
                <tlk:RadTextBox runat="server" ID="txtTitle" SkinID="Textbox1023">
                </tlk:RadTextBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Số điện thoại")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtPhone" />
            </td>
            <td class="lb">
                <%# Translate("Mã số thuế")%>
            </td>
            <td>
                <tlk:RadTextBox runat="server" ID="txtTax" SkinID="Textbox15">
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
        <tr style="display: none">
            <td class="lb">
                <%# Translate("Nguyên quán")%>
            </td>
            <td colspan="2">
                <tlk:RadComboBox runat="server" ID="cboNguyenQuan">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Địa chỉ thường trú")%>
            </td>
            <td colspan="3">
                <tlk:RadTextBox runat="server" ID="txtAdress" Width="100%" />
            </td>
            <td class="lb">
                <%# Translate("Quốc gia")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cboNationlity" SkinID="LoadDemand" AutoPostBack="true">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Tỉnh/Thành phố")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbPROVINCE_ID" SkinID="LoadDemand" AutoPostBack="true"
                    CausesValidation="false" OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Quận/Huyện")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbDISTRICT_ID" SkinID="LoadDemand" AutoPostBack="true"
                    CausesValidation="false" OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Xã/Phường")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbWARD_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                    OnClientItemsRequesting="OnClientItemsRequesting">
                </tlk:RadComboBox>
            </td>
            <td class="lb" style="display: none">
                <%# Translate("Thôn/Ấp")%>
            </td>
            <td style="display: none">
                <tlk:RadTextBox runat="server" ID="txtAD_Village" />
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Địa chỉ tạm trú")%>
            </td>
            <td colspan="5">
                <tlk:RadTextBox runat="server" ID="txtTempAdress" Width="100%" />
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Tỉnh/Thành phố")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbTempPROVINCE_ID" SkinID="LoadDemand" AutoPostBack="true"
                    CausesValidation="false" OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Quận/Huyện")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbTempDISTRICT_ID" SkinID="LoadDemand" AutoPostBack="true"
                    CausesValidation="false" OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Xã/Phường")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbTempWARD_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                    OnClientItemsRequesting="OnClientItemsRequesting">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkIs_Pass" AutoPostBack="true" Text="<%$ Translate : Đã mất  %>" />
            </td>
        </tr>
        <tr>
            <td class="lb">
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkIsDeduct" AutoPostBack="true" Text="<%$ Translate : Đối tượng giảm trừ  %>" />
            </td>
        </tr>
        <tr runat="server" id="hid_Deduct" visible="false">
            <td class="lb">
                <%# Translate("Ngày đăng ký giảm trừ")%>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdDeductReg">
                </tlk:RadDatePicker>
            </td>
            <td class="lb">
                <%# Translate("Ngày giảm trừ")%>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdDeductFrom">
                </tlk:RadDatePicker>
            </td>
            <td class="lb">
                <%# Translate("Ngày kết thúc")%>
            </td>
            <td>
                <tlk:RadDatePicker runat="server" ID="rdDeductTo">
                </tlk:RadDatePicker>
                <asp:CompareValidator ID="compareDeductTo_DeductFrom" runat="server" ErrorMessage="<%$ Translate: Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu %>"
                    ToolTip="<%$ Translate: Ngày kết thúc giảm trừ phải lớn hơn ngày bắt đầu %>"
                    ControlToValidate="rdDeductTo" ControlToCompare="rdDeductFrom" Operator="GreaterThanEqual"
                    Type="Date">
                </asp:CompareValidator>
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
                <tlk:RadComboBox runat="server" ID="cbTempKtPROVINCE_ID" SkinID="LoadDemand" AutoPostBack="true"
                    CausesValidation="false" OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Quận/Huyện")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbTempKtDISTRICT_ID" SkinID="LoadDemand" AutoPostBack="true"
                    CausesValidation="false" OnClientItemsRequesting="OnClientItemsRequesting" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                </tlk:RadComboBox>
            </td>
            <td class="lb">
                <%# Translate("Xã/Phường")%>
            </td>
            <td>
                <tlk:RadComboBox runat="server" ID="cbTempKtWARD_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                    OnClientItemsRequesting="OnClientItemsRequesting">
                </tlk:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="lb">
                <%# Translate("Ghi chú")%>
            </td>
            <td colspan="5">
                <tlk:RadTextBox runat="server" ID="txtRemark" SkinID="Textbox1023" Width="100%" />
            </td>
        </tr>
    </table>
    <tlk:RadGrid PageSize="50" ID="rgFamilyEdit" runat="server" Height="250px" Width="99%">
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,IS_DEDUCT,DEDUCT_FROM,DEDUCT_TO,ADDRESS,BIRTH_DATE,REMARK,DEDUCT_REG,FK_PKEY,STATUS,REASON_UNAPROVE,TAXTATION,CAREER,TITLE_NAME,PROVINCE_NAME,PROVINCE_ID,CERTIFICATE_NUM,CERTIFICATE_CODE,ADDRESS_TT,AD_PROVINCE_ID,AD_DISTRICT_ID,AD_WARD_ID,AD_VILLAGE,TT_PROVINCE_ID,TT_DISTRICT_ID,TT_WARD_ID,IS_OWNER,IS_PASS,IS_SAME_COMPANY"
            ClientDataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,IS_DEDUCT,DEDUCT_FROM,DEDUCT_TO,ADDRESS,BIRTH_DATE,REMARK,DEDUCT_REG,FK_PKEY,STATUS,REASON_UNAPROVE,TAXTATION,CAREER,TITLE_NAME,PROVINCE_NAME,PROVINCE_ID,CERTIFICATE_NUM,CERTIFICATE_CODE,ADDRESS_TT,AD_PROVINCE_ID,AD_DISTRICT_ID,AD_WARD_ID,AD_VILLAGE,TT_PROVINCE_ID,TT_DISTRICT_ID,TT_WARD_ID,IS_OWNER,IS_PASS,NATION_ID,ID_NO_DATE,ID_NO_PLACE_NAME,PHONE,TAXTATION_DATE,TAXTATION_PLACE,BIRTH_CODE,QUYEN,BIRTH_NATION_ID,BIRTH_PROVINCE_ID,BIRTH_DISTRICT_ID,BIRTH_WARD_ID,GENDER,IS_SAME_COMPANY"
            Caption="<%$ Translate: Thông tin chỉnh sửa %>">
            <Columns>
                <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                    HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                </tlk:GridClientSelectColumn>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow" UniqueName="EDIT_ROW">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Quan hệ %>" DataField="RELATION_NAME"
                    UniqueName="RELATION_NAME" SortExpression="RELATION_NAME">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                    UniqueName="FULLNAME" SortExpression="FULLNAME">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate: Ngày sinh%>"
                    ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="BIRTH_DATE">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate: CMND %>" UniqueName="ID_NO"
                    Visible="True" EmptyDataText="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã thuế %>" DataField="TAXTATION"
                    UniqueName="TAXTATION" SortExpression="TAXTATION">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn DataField="DEDUCT_REG" HeaderText="<%$ Translate: Ngày đăng ký giảm trừ%>"
                    UniqueName="DEDUCT_REG" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Giảm trừ %>" DataField="IS_DEDUCT"
                    UniqueName="IS_DEDUCT" SortExpression="IS_DEDUCT" HeaderStyle-Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridCheckBoxColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày giảm trừ %>" DataField="DEDUCT_FROM"
                    UniqueName="DEDUCT_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_FROM">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="DEDUCT_TO"
                    UniqueName="DEDUCT_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_TO">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <%-- <tlk:GridBoundColumn DataField="CAREER" HeaderText="<%$ Translate: Nghề nghiệp %>"
                    UniqueName="CAREER" Visible="True" EmptyDataText="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Là chủ hộ %>" DataField="IS_OWNER"
                    UniqueName="IS_OWNER" SortExpression="IS_OWNER" HeaderStyle-Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridCheckBoxColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã hộ gia đình %>" DataField="CERTIFICATE_CODE"
                    UniqueName="CERTIFICATE_CODE" SortExpression="CERTIFICATE_CODE">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ thường trú %>" DataField="ADDRESS"
                    UniqueName="ADDRESS" SortExpression="ADDRESS">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                    SortExpression="REMARK">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>--%>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                    UniqueName="STATUS_NAME" SortExpression="STATUS_NAME">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không phê duyệt %>" DataField="REASON_UNAPROVE"
                    UniqueName="REASON_UNAPROVE" SortExpression="REASON_UNAPROVE">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <br />
    <tlk:RadGrid PageSize="50" ID="rgFamily" runat="server" Height="250px" Width="99%">
        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
        <MasterTableView DataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,IS_DEDUCT,DEDUCT_FROM,DEDUCT_TO,ADDRESS,BIRTH_DATE,REMARK,DEDUCT_REG,CAREER,TITLE_NAME,PROVINCE_NAME,PROVINCE_ID,TAXTATION,CERTIFICATE_NUM,CERTIFICATE_CODE,ADDRESS_TT,AD_PROVINCE_ID,AD_DISTRICT_ID,AD_WARD_ID,AD_VILLAGE,TT_PROVINCE_ID,TT_DISTRICT_ID,TT_WARD_ID,IS_OWNER,IS_PASS,IS_SAME_COMPANY"
            ClientDataKeyNames="ID,ID_NO,FULLNAME,RELATION_ID,IS_DEDUCT,DEDUCT_FROM,DEDUCT_TO,ADDRESS,BIRTH_DATE,REMARK,DEDUCT_REG,CAREER,TITLE_NAME,PROVINCE_NAME,PROVINCE_ID,TAXTATION,CERTIFICATE_NUM,CERTIFICATE_CODE,ADDRESS_TT,AD_PROVINCE_ID,AD_DISTRICT_ID,AD_WARD_ID,AD_VILLAGE,TT_PROVINCE_ID,TT_DISTRICT_ID,TT_WARD_ID,IS_OWNER,IS_PASS,NATION_ID,ID_NO_DATE,ID_NO_PLACE_NAME,PHONE,TAXTATION_DATE,TAXTATION_PLACE,BIRTH_CODE,QUYEN,BIRTH_NATION_ID,BIRTH_PROVINCE_ID,BIRTH_DISTRICT_ID,BIRTH_WARD_ID,GENDER,IS_SAME_COMPANY"
            Caption="<%$ Translate: Thông tin hiện tại %>">
            <Columns>
                <tlk:GridButtonColumn HeaderText="" Text="Sửa" CommandName="EditRow" UniqueName="EDIT_ROW">
                    <HeaderStyle Wrap="False" Width="60px" />
                    <ItemStyle Font-Underline="true" Wrap="false" Width="60px" ForeColor="Blue" />
                </tlk:GridButtonColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Quan hệ %>" DataField="RELATION_NAME"
                    UniqueName="RELATION_NAME" SortExpression="RELATION_NAME">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="FULLNAME"
                    UniqueName="FULLNAME" SortExpression="FULLNAME">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn UniqueName="BIRTH_DATE" HeaderText="<%$ Translate: Ngày sinh%>"
                    ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" DataField="BIRTH_DATE">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridBoundColumn DataField="ID_NO" HeaderText="<%$ Translate: CMND %>" UniqueName="ID_NO"
                    Visible="True" EmptyDataText="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã thuế %>" DataField="TAXTATION"
                    UniqueName="TAXTATION" SortExpression="TAXTATION">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridDateTimeColumn DataField="DEDUCT_REG" HeaderText="<%$ Translate: Ngày đăng ký giảm trừ%>"
                    UniqueName="DEDUCT_REG" Visible="true" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Giảm trừ %>" DataField="IS_DEDUCT"
                    UniqueName="IS_DEDUCT" SortExpression="IS_DEDUCT" HeaderStyle-Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridCheckBoxColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày giảm trừ %>" DataField="DEDUCT_FROM"
                    UniqueName="DEDUCT_FROM" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_FROM">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="DEDUCT_TO"
                    UniqueName="DEDUCT_TO" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_TO">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridDateTimeColumn>
                <%-- <tlk:GridBoundColumn DataField="CAREER" HeaderText="<%$ Translate: Nghề nghiệp %>"
                    UniqueName="CAREER" Visible="True" EmptyDataText="">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Là chủ hộ %>" DataField="IS_OWNER"
                    UniqueName="IS_OWNER" SortExpression="IS_OWNER" HeaderStyle-Width="80px">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </tlk:GridCheckBoxColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã hộ gia đình %>" DataField="CERTIFICATE_CODE"
                    UniqueName="CERTIFICATE_CODE" SortExpression="CERTIFICATE_CODE">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Địa chỉ thường trú %>" DataField="ADDRESS"
                    UniqueName="ADDRESS" SortExpression="ADDRESS">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>
                <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                    SortExpression="REMARK">
                    <HeaderStyle HorizontalAlign="Center" />
                </tlk:GridBoundColumn>--%>
            </Columns>
        </MasterTableView>
    </tlk:RadGrid>
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function clientButtonClicking(sender, args) {

            }

            var enableAjax = true;
            function onRequestStart(sender, eventArgs) {
                eventArgs.set_enableAjax(enableAjax);
                enableAjax = true;
            }

            function OnClientSelectedIndexChanged(sender, eventArgs) {

            }

            function clearSelectRadcombo(cbo) {
                if (cbo) {
                    cbo.clearItems();
                    cbo.clearSelection();
                    cbo.set_text('');
                }
            }
            function clearSelectRadtextbox(cbo) {
                if (cbo) {
                    cbo.clear();
                }
            }

            function OnClientItemsRequesting(sender, eventArgs) {
            };
            function rgFamilyEditRadGridDeSelecting(sender, eventArgs) {

            };
            function rgFamilyEditOnClientRowSelected(sender, eventArgs) {

            };
            function rgFamilyEditRadGridSelecting(sender, eventArgs) {

            };
            function rgFamilyRadGridDeSelecting(sender, eventArgs) {

            };
            function rgFamilyOnClientRowSelected(sender, eventArgs) {

            };
            function rgFamilyRadGridSelecting(sender, eventArgs) {

            };
             
        </script>
    </tlk:RadCodeBlock>
</div>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
