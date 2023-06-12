<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpCuriculumVitae.ascx.vb"
    Inherits="Profile.ctrlHU_EmpCuriculumVitae" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<style>
    .cv-header-title {
        font-weight: 700;
        margin: 25px 10px 0px 8px;
        width: 86%;
    }

        .cv-header-title p {
            margin: 0 auto;
        }

    .cv-content-grid {
        margin-left: 8px;
    }
    .emp-img{
        width: 100%;
        text-align: center;
    }
    .emp-img-content{
        width: 65px;
        height: auto;
        margin: 0 auto;
        margin-bottom: 10px;
        border: 1px solid rgba(0,0,0,0.3);
        padding: 4px 3.5px 1px 4px;
    }
    .emp-img-content img {
        max-width:100%;
        height: auto !important;
    }
</style>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="8">
                    <b>
                        <%# Translate("Thông tin nhân viên")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeName" runat="server" Text="<%$ Translate: Họ tên nhân viên %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Translate: Mã nhân viên cũ %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCodeOld" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td colspan="2" rowspan="5">
                    <div class="emp-img">
                        <div class="emp-img-content">
                            <tlk:RadBinaryImage ID="rbiEmployeeImage" runat="server" AutoAdjustImageControlSize="true" ResizeMode="Fill" />
                        </div>
                        <tlk:RadButton ID="btnEdit" SkinID="Button" runat="server" CausesValidation="false" OnClientClicking="OpenEdit" 
                            Width="70px" Text="Chỉnh sửa">
                        </tlk:RadButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="<%$ Translate: Đơn vị %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTITLE" runat="server" Text="<%$ Translate: Vị trí công việc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label5" runat="server" Text="<%$ Translate: Loại nhân viên %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeType" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Translate: Ngày nhận việc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdJoinDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label7" runat="server" Text="<%$ Translate: Ngày vào chính thức %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdJoindateState" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Translate: Loại hợp đồng %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractType" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <b>
                        <%# Translate("Thông tin sơ yếu lý lịch")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label9" runat="server" Text="<%$ Translate: Ngày sinh %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdBirthDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label10" runat="server" Text="<%$ Translate: Giới tính %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtGender" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label11" runat="server" Text="<%$ Translate: Tình trạng hôn nhân %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMarriage" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label12" runat="server" Text="<%$ Translate: Quốc tịch %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNational" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Translate: Nơi sinh %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBirthPlace" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Translate: Nguyên quán %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtProvinceNQ" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="<%$ Translate: Dân tộc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNative" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label13" runat="server" Text="<%$ Translate: Tôn giáo %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtReligion" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label14" runat="server" Text="<%$ Translate: Số CMND %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtIDNo" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label15" runat="server" Text="<%$ Translate: Ngày cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdIDDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label16" runat="server" Text="<%$ Translate: Ngày hết hạn %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireIDNO" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label17" runat="server" Text="<%$ Translate: Nơi cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtIDPlace" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label18" runat="server" Text="<%$ Translate: Nhóm máu %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNhomMau" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label19" runat="server" Text="<%$ Translate: Chiều cao(cm) %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtChieuCao" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label20" runat="server" Text="<%$ Translate: Cân nặng(kg) %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCanNang" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label21" runat="server" Text="<%$ Translate: Số sổ BHXH %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtBookNo" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label22" runat="server" Text="<%$ Translate: Mã số thuế %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPITNo" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label23" runat="server" Text="<%$ Translate: Ngày cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label24" runat="server" Text="<%$ Translate: Nơi cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPITPlace" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label25" runat="server" Text="<%$ Translate: Nơi KCB %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiKhamChuaBenh" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <b>
                        <%# Translate("Thông tin Visa, Passport")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label26" runat="server" Text="<%$ Translate: Số hộ chiếu %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPassNo" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label27" runat="server" Text="<%$ Translate: Ngày cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdPassDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label28" runat="server" Text="<%$ Translate: Ngày hết hạn %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdPassExpireDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label29" runat="server" Text="<%$ Translate: Nơi cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiCapHoChieu" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label30" runat="server" Text="<%$ Translate: Visa %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtVisa" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label31" runat="server" Text="<%$ Translate: Ngày cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdVisaDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label32" runat="server" Text="<%$ Translate: Ngày hết hạn %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdVisaExpireDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label33" runat="server" Text="<%$ Translate: Nơi cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiCapVisa" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label34" runat="server" Text="<%$ Translate: Số sổ lao động %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSoSoLaoDong" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label35" runat="server" Text="<%$ Translate: Ngày cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayCapSSLD" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label36" runat="server" Text="<%$ Translate: Ngày hết hạn %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdNgayHetHanSSLD" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="Label37" runat="server" Text="<%$ Translate: Nơi cấp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNoiCapSSLD" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <b>
                        <%# Translate("Thông tin liên hệ")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label38" runat="server" Text="<%$ Translate: Địa chỉ thường trú %>"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtPerAddress" runat="server" ReadOnly="True"  Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label41" runat="server" Text="<%$ Translate: Điện thoại di động %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMobilePhone" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label39" runat="server" Text="<%$ Translate: Thành phố %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPerProvince" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label40" runat="server" Text="<%$ Translate: Quận huyện %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPerDistrict" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label42" runat="server" Text="<%$ Translate: Xã phường %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPerWard" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label43" runat="server" Text="<%$ Translate: Điện thoại cố định %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtHomePhone" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label44" runat="server" Text="<%$ Translate: Địa chỉ liên hệ %>"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtNavAddress" runat="server" ReadOnly="True"  Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label45" runat="server" Text="<%$ Translate: Email cá nhân %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPerEmail" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label46" runat="server" Text="<%$ Translate: Thành phố %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNavProvince" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label47" runat="server" Text="<%$ Translate: Quận huyện %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNavDistrict" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label48" runat="server" Text="<%$ Translate: Xã phường %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNavWard" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label49" runat="server" Text="<%$ Translate: Email công ty %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtWorkEmail" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <b>
                        <%# Translate("Thông tin liên hệ khẩn cấp")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label50" runat="server" Text="<%$ Translate: Người liên hệ %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPerson" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label51" runat="server" Text="<%$ Translate: Mối quan hệ NLH %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRelationNLH" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label52" runat="server" Text="<%$ Translate: Số CMND %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPerIDNo" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label53" runat="server" Text="<%$ Translate: Điện thoại di động %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPerMobilePhone" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label54" runat="server" Text="<%$ Translate: Địa chỉ NLH %>"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtAddressPerContact" runat="server" ReadOnly="True"  Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label55" runat="server" Text="<%$ Translate: Điện thoại cố định %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContactPerPhone" runat="server" ReadOnly="True" >
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin quá trình công tác")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid ID="rgChangeInfo" runat="server" Height="300px" Width="99%" PageSize="5" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID,STATUS_ID,DECISION_TYPE_ID,EMPLOYEE_CODE,DECISION_TYPE_NAME,CODE,EFFECT_DATE"
                    ClientDataKeyNames="ID,EMPLOYEE_ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                            UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="120px" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE"
                            UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="120px" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Loại quyết định" DataField="DECISION_TYPE_NAME"
                            UniqueName="DECISION_TYPE_NAME" SortExpression="DECISION_TYPE_NAME" HeaderStyle-Width="130px">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                            UniqueName="DECISION_NO" SortExpression="DECISION_NO" HeaderStyle-Width="120px">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Quản lý trực tiếp" DataField="DIRECT_MANAGER_NAME"
                            UniqueName="DIRECT_MANAGER_NAME" SortExpression="DIRECT_MANAGER_NAME" HeaderStyle-Width="130px">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Loại hình lao động" DataField="OBJECT_LABOR_TITLE" Visible="false"
                            UniqueName="OBJECT_LABOR_TITLE" SortExpression="OBJECT_LABOR_TITLE">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Đối tượng chấm công" DataField="OBJECT_ATTENDANCE_NAME" Visible="false"
                            UniqueName="OBJECT_ATTENDANCE_NAME" SortExpression="OBJECT_ATTENDANCE_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Đối tượng nhân vi" DataField="OBJECT_EMPLOYEE_NAME" Visible="false"
                            UniqueName="OBJECT_EMPLOYEE_NAME" SortExpression="OBJECT_EMPLOYEE_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Đối tượng công" DataField="OBJECT_ATTENDANT_NAME" Visible="false"
                            UniqueName="OBJECT_ATTENDANT_NAME" SortExpression="OBJECT_ATTENDANT_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Nơi làm việc" DataField="WORK_PLACE_NAME"
                            UniqueName="WORK_PLACE_NAME" SortExpression="WORK_PLACE_NAME" HeaderStyle-Width="140px">
                        </tlk:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin hợp đồng")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgContract" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Số hợp đồng %>" DataField="CONTRACT_NO"
                            UniqueName="CONTRACT_NO" SortExpression="CONTRACT_NO" HeaderStyle-Width="100px">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hợp đồng %>" DataField="CONTRACTTYPE_NAME"
                            UniqueName="CONTRACTTYPE_NAME" SortExpression="CONTRACTTYPE_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày bắt đầu %>" DataField="START_DATE"
                            UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="EXPIRE_DATE"
                            UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EXPIRE_DATE" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày thanh lý %>" DataField="LIQUIDATION_DATE"
                            UniqueName="LIQUIDATION_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="LIQUIDATION_DATE" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi làm việc %>" DataField="WORK_PLACE_NAME"
                            UniqueName="WORK_PLACE_NAME" SortExpression="WORK_PLACE_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời gian làm việc %>" DataField="WORK_TIME"
                            UniqueName="WORK_TIME" SortExpression="WORK_TIME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả công việc %>" DataField="WORK_DES"
                            UniqueName="WORK_DES" SortExpression="WORK_DES">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Giai đoạn học lý thuyết từ %>" DataField="THEORY_PHASE_FROM"
                            UniqueName="THEORY_PHASE_FROM" SortExpression="THEORY_PHASE_FROM">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Giai đoạn học lý thuyết đến %>" DataField="THEORY_PHASE_TO"
                            UniqueName="THEORY_PHASE_TO" SortExpression="THEORY_PHASE_TO">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Giai đoạn thực tập từ %>" DataField="PRACTICE_PHASE_FROM"
                            UniqueName="PRACTICE_PHASE_FROM" SortExpression="PRACTICE_PHASE_FROM">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Giai đoạn thực tập đến %>" DataField="PRACTICE_PHASE_TO"
                            UniqueName="PRACTICE_PHASE_TO" SortExpression="PRACTICE_PHASE_TO">
                        </tlk:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin phụ lục hợp đồng")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgContractAppendix" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="Số hợp đồng" DataField="CONTRACT_NO"
                            UniqueName="CONTRACT_NO" SortExpression="CONTRACT_NO">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Số phụ lục hợp đồng" DataField="APPEND_NUMBER"
                            UniqueName="APPEND_NUMBER" SortExpression="APPEND_NUMBER">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Loại phụ lục hợp đồng" DataField="APPEND_TYPE_NAME" UniqueName="APPEND_TYPE_NAME"
                            SortExpression="APPEND_TYPE_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Ngày bắt đầu" DataField="START_DATE"
                            UniqueName="START_DATE" SortExpression="START_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Ngày kết thúc" DataField="EXPIRE_DATE"
                            UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Nội dung thay đổi" DataField="CONTENT_APPEND" UniqueName="CONTENT_APPEND"
                            SortExpression="CONTENT_APPEND">
                        </tlk:GridBoundColumn>
                    </Columns>
                    <HeaderStyle Width="120px" />
                </MasterTableView>
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin hồ sơ lương")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgWorkingSal" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                            UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm lương %>" DataField="SAL_TYPE_NAME"
                            UniqueName="SAL_TYPE_NAME" SortExpression="SAL_TYPE_NAME" HeaderStyle-Width="120px">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Biểu thuế %>" DataField="TAX_TABLE_NAME"
                            UniqueName="TAX_TABLE_NAME" SortExpression="TAX_TABLE_NAME" HeaderStyle-Width="120px">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nhân viên %>" DataField="EMPLOYEE_TYPE_NAME"
                            UniqueName="EMPLOYEE_TYPE_NAME" SortExpression="EMPLOYEE_TYPE_NAME" HeaderStyle-Width="120px">
                        </tlk:GridBoundColumn>
                        <tlk:GridNumericColumn HeaderText="Lương cơ bản" DataField="SAL_BASIC" UniqueName="SAL_BASIC" SortExpression="SAL_BASIC" DataFormatString="{0:N0}">
                        </tlk:GridNumericColumn>
                        <tlk:GridNumericColumn HeaderText="Thưởng HQCV" DataField="OTHERSALARY1" UniqueName="OTHERSALARY1" SortExpression="OTHERSALARY1" DataFormatString="{0:N0}">
                        </tlk:GridNumericColumn>
                        <tlk:GridNumericColumn HeaderText="% Hưởng lương" DataField="PERCENTSALARY" UniqueName="PERCENTSALARY" SortExpression="PERCENTSALARY" DataFormatString="{0:N0}">
                        </tlk:GridNumericColumn>
                        <tlk:GridNumericColumn HeaderText="Tổng mức lương" DataField="SAL_TOTAL" UniqueName="SAL_TOTAL" SortExpression="SAL_TOTAL" DataFormatString="{0:N0}">
                        </tlk:GridNumericColumn>
                        <tlk:GridNumericColumn HeaderText="Lương đóng BHXH" DataField="SALARY_BHXH" UniqueName="SALARY_BHXH" SortExpression="SALARY_BHXH" DataFormatString="{0:N0}">
                        </tlk:GridNumericColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin phụ cấp/ giảm trừ")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgAllowance" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên khoản cộng/trừ %>" DataField="ALLOWANCE_LIST_NAME"
                            UniqueName="ALLOWANCE_LIST_NAME" SortExpression="ALLOWANCE_LIST_NAME" HeaderStyle-Width="200px" />
                        <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Là khoản trừ %>" DataField="IS_DEDUCT"
                            UniqueName="IS_DEDUCT" SortExpression="IS_DEDUCT" HeaderStyle-Width="50px" ShowFilterIcon="true" AllowFiltering="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền %>" DataField="AMOUNT" UniqueName="AMOUNT"
                            SortExpression="AMOUNT" DataFormatString="{0:n2}" HeaderStyle-Width="100px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Hệ số %>" DataField="FACTOR" UniqueName="FACTOR"
                            SortExpression="FACTOR" HeaderStyle-Width="100px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Diễn giải cộng khác %>" DataField="DETAIL_ADD_NAME"
                            UniqueName="DETAIL_ADD_NAME" SortExpression="DETAIL_ADD_NAME" HeaderStyle-Width="120px" />

                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                            SortExpression="REMARK" HeaderStyle-Width="200px" />
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin kiêm nhiệm")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgCon" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="Công ty kiêm nhiệm" DataField="COM_ORG_CON_NAME"
                            SortExpression="COM_ORG_CON_NAME" UniqueName="COM_ORG_CON_NAME" HeaderStyle-Width="200px" />
                        <tlk:GridBoundColumn HeaderText="Ban/Phòng kiêm nhiệm" DataField="ORG_CON_NAME"
                            SortExpression="ORG_CON_NAME" UniqueName="ORG_CON_NAME" HeaderStyle-Width="200px" />
                        <tlk:GridBoundColumn HeaderText="Vị trí công việc kiêm nhiệm" DataField="TITLE_CON_NAME"
                            SortExpression="TITLE_CON_NAME" UniqueName="TITLE_CON_NAME" HeaderStyle-Width="150px" />
                        <tlk:GridBoundColumn HeaderText="Ngày hiệu lực kiêm nhiệm" DataField="EFFECT_DATE_CON" HeaderStyle-Width="150px"
                            ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE_CON" UniqueName="EFFECT_DATE_CON"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Ngày kết thúc" DataField="EXPIRE_DATE_CON" HeaderStyle-Width="150px"
                            ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE_CON" UniqueName="EXPIRE_DATE_CON"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="CON_NO"
                            SortExpression="CON_NO" UniqueName="CON_NO" HeaderStyle-Width="150px" />
                        <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGN_NAME"
                            SortExpression="SIGN_NAME" UniqueName="SIGN_NAME" HeaderStyle-Width="120px" />
                        <tlk:GridBoundColumn HeaderText="Vị trí công việc người ký " DataField="SIGN_TITLE_NAME"
                            SortExpression="SIGN_TITLE_NAME" UniqueName="SIGN_TITLE_NAME" HeaderStyle-Width="120px" />
                        <tlk:GridBoundColumn HeaderText="Ngày ký" DataField="SIGN_DATE" HeaderStyle-Width="150px"
                            ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin khen thưởng")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgCommend" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME"
                            ItemStyle-HorizontalAlign="Center" SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                        <tlk:GridBoundColumn HeaderText="Năm khen thưởng" DataField="YEAR"
                            ItemStyle-HorizontalAlign="Center" SortExpression="YEAR" UniqueName="YEAR" />
                        <tlk:GridBoundColumn HeaderText="Danh hiệu khen thưởng" DataField="COMMEND_TITLE_NAME"
                            SortExpression="COMMEND_TITLE_NAME" UniqueName="COMMEND_TITLE_NAME" HeaderStyle-Width="150px" />
                        <tlk:GridNumericColumn HeaderText="Mức thưởng" DataField="MONEY"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" SortExpression="MONEY"
                            UniqueName="MONEY" />
                        <tlk:GridBoundColumn HeaderText="Hình thức khen thưởng" DataField="Commend_TYPE_NAME"
                            SortExpression="Commend_TYPE_NAME" UniqueName="Commend_TYPE_NAME" HeaderStyle-Width="150px" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức trả thưởng %>" DataField="COMMEND_PAY_NAME"
                            SortExpression="COMMEND_PAY_NAME" UniqueName="COMMEND_PAY_NAME" HeaderStyle-Width="150px" />
                        <tlk:GridBoundColumn HeaderText="Loại khen thưởng" DataField="Commend_OBJ_NAME"
                            SortExpression="Commend_OBJ_NAME" UniqueName="Commend_OBJ_NAME" HeaderStyle-Width="150px" />
                        <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="DECISION_NO"
                            SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                        <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                        <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" SortExpression="REMARK"
                            UniqueName="REMARK" />
                        <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGNER_NAME"
                            SortExpression="SIGNER_NAME" UniqueName="SIGNER_NAME" HeaderStyle-Width="150px" />
                        <tlk:GridBoundColumn HeaderText="Vị trí công việc người ký" DataField="SIGNER_TITLE"
                            SortExpression="SIGNER_TITLE" UniqueName="SIGNER_TITLE" HeaderStyle-Width="150px" />
                        <tlk:GridBoundColumn HeaderText="Ngày ký" DataField="SIGN_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin kỷ luật")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgDiscipline" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                            ItemStyle-HorizontalAlign="Center" SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng %>" DataField="DISCIPLINE_OBJ_NAME"
                            SortExpression="DISCIPLINE_OBJ_NAME" UniqueName="DISCIPLINE_OBJ_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do kỷ luật %>" DataField="DISCIPLINE_REASON_NAME"
                            SortExpression="DISCIPLINE_REASON_NAME" UniqueName="DISCIPLINE_REASON_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức kỷ luật %>" DataField="DISCIPLINE_TYPE_NAME"
                            SortExpression="DISCIPLINE_TYPE_NAME" UniqueName="DISCIPLINE_TYPE_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm kỷ luật %>" DataField="YEAR"
                            SortExpression="YEAR" UniqueName="YEAR" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp kỷ luật %>" DataField="LEVEL_NAME"
                            SortExpression="LEVEL_NAME" UniqueName="LEVEL_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày vi phạm %>" DataField="VIOLATION_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="VIOLATION_DATE" UniqueName="VIOLATION_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Số quyết định %>" DataField="DECISION_NO"
                            SortExpression="DECISION_NO" UniqueName="DECISION_NO" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                        <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                            SortExpression="ORG_DESC" Visible="false" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Người ký %>" DataField="SIGNER_NAME"
                            SortExpression="SIGNER_NAME" UniqueName="SIGNER_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc người ký %>" DataField="SIGNER_TITLE"
                            SortExpression="SIGNER_TITLE" UniqueName="SIGNER_TITLE" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày ký %>" DataField="SIGN_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Thêm mới từ Portal %>" DataField="IS_PORTAL_NAME"
                            SortExpression="IS_PORTAL_NAME" UniqueName="IS_PORTAL_NAME" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Người tạo %>" DataField="CREATED_BY"
                            SortExpression="CREATED_BY" UniqueName="CREATED_BY" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày tạo %>" DataField="CREATED_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="CREATED_DATE" UniqueName="CREATED_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Người cập nhật %>" DataField="MODIFIED_BY"
                            SortExpression="MODIFIED_BY" UniqueName="MODIFIED_BY" />
                        <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cập nhật %>" DataField="MODIFIED_DATE"
                            ItemStyle-HorizontalAlign="Center" SortExpression="MODIFIED_DATE" UniqueName="MODIFIED_DATE"
                            DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo" />
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin đào tạo")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgTraining" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn DataField="CERTIFICATE" HeaderText="Loại bằng cấp/ chứng chỉ"
                            UniqueName="CERTIFICATE" SortExpression="CERTIFICATE">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="CERTIFICATE_NAME" HeaderText="Tên bằng cấp/ chứng chỉ"
                            UniqueName="CERTIFICATE_NAME" SortExpression="CERTIFICATE_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="FROM_DATE_1" HeaderText="Từ tháng"
                            UniqueName="FROM_DATE_1" SortExpression="FROM_DATE_1" DataFormatString="{0:MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="TO_DATE_1" HeaderText="Tới tháng"
                            UniqueName="TO_DATE_1" SortExpression="TO_DATE_1" DataFormatString="{0:MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="LEVEL_NAME" HeaderText="Trình độ"
                            UniqueName="LEVEL_NAME" SortExpression="LEVEL_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="SPECIALIZED_TRAIN" HeaderText="Chuyên ngành"
                            UniqueName="SPECIALIZED_TRAIN" SortExpression="SPECIALIZED_TRAIN">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="YEAR_GRA" HeaderText="Năm tốt nghiệp" UniqueName="YEAR_GRA"
                            ShowFilterIcon="false" SortExpression="YEAR_GRA">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="RESULT_TRAIN" HeaderText="Xếp loại tốt nghiệp" UniqueName="RESULT_TRAIN"
                            ShowFilterIcon="false" SortExpression="RESULT_TRAIN">
                        </tlk:GridBoundColumn>
                        <tlk:GridNumericColumn DataField="POINT_LEVEL" HeaderText="Điểm số" UniqueName="POINT_LEVEL"
                            ShowFilterIcon="false" SortExpression="POINT_LEVEL" DataFormatString="{0:N1}">
                        </tlk:GridNumericColumn>
                        <tlk:GridBoundColumn HeaderText="Hiệu lực chứng chỉ từ" DataField="EFFECTIVE_DATE_FROM"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_FROM"
                            UniqueName="EFFECTIVE_DATE_FROM" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn HeaderText="Hiệu lực chứng chỉ đến" DataField="EFFECTIVE_DATE_TO"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECTIVE_DATE_TO"
                            UniqueName="EFFECTIVE_DATE_TO" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="CONTENT_LEVEL" HeaderText="Nội dung đào tạo"
                            UniqueName="CONTENT_LEVEL" SortExpression="CONTENT_LEVEL">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="FORM_TRAIN_NAME" HeaderText="Hình thức đào tạo"
                            UniqueName="FORM_TRAIN_NAME" SortExpression="FORM_TRAIN_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="TRAIN_PLACE_NAME" HeaderText="Nơi đào tạo"
                            UniqueName="TRAIN_PLACE_NAME" SortExpression="TRAIN_PLACE_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="NOTE" HeaderText="Ghi chú"
                            UniqueName="NOTE" SortExpression="NOTE">
                        </tlk:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin người thân")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgFamily" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn DataField="FULLNAME" HeaderText="Họ tên người thân"
                            UniqueName="FULLNAME" SortExpression="FULLNAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="RELATION_NAME" HeaderText="Mối quan hệ"
                            UniqueName="RELATION_NAME" SortExpression="RELATION_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="BIRTH_DATE" HeaderText="Ngày sinh"
                            UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE" DataFormatString="{0:MM/yyyy}">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="ID_NO" HeaderText="CMND"
                            UniqueName="ID_NO" SortExpression="ID_NO">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="TAXTATION" HeaderText="Mã số thuế"
                            UniqueName="TAXTATION" SortExpression="TAXTATION">
                        </tlk:GridBoundColumn>
                        <tlk:GridCheckBoxColumn DataField="IS_OWNER" HeaderText="Là chủ hộ" UniqueName="IS_OWNER"
                            ShowFilterIcon="false" SortExpression="IS_OWNER" AllowFiltering="false" HeaderStyle-Width="70px">
                        </tlk:GridCheckBoxColumn>
                        <tlk:GridCheckBoxColumn DataField="IS_DEDUCT" HeaderText="Thuộc đối tượng giảm trừ" UniqueName="IS_DEDUCT"
                            ShowFilterIcon="false" SortExpression="IS_DEDUCT" AllowFiltering="false" HeaderStyle-Width="70px">
                        </tlk:GridCheckBoxColumn>
                        <tlk:GridDateTimeColumn HeaderText="Ngày đăng ký giảm trừ" DataField="DEDUCT_REG"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_REG"
                            UniqueName="DEDUCT_REG">
                        </tlk:GridDateTimeColumn>
                        <tlk:GridDateTimeColumn HeaderText="Ngày giảm trừ" DataField="DEDUCT_FROM"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_FROM"
                            UniqueName="DEDUCT_FROM">
                        </tlk:GridDateTimeColumn>
                        <tlk:GridDateTimeColumn HeaderText="Ngày kết thúc" DataField="DEDUCT_TO"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DEDUCT_TO"
                            UniqueName="DEDUCT_TO">
                        </tlk:GridDateTimeColumn>
                        <tlk:GridBoundColumn DataField="BIRTH_CODE" HeaderText="Giấy khai sinh"
                            UniqueName="BIRTH_CODE" SortExpression="BIRTH_CODE">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="QUYEN" HeaderText="Quyển số"
                            UniqueName="QUYEN" SortExpression="QUYEN">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="BIRTH_NATION_NAME" HeaderText="Quốc tịch"
                            UniqueName="BIRTH_NATION_NAME" SortExpression="BIRTH_NATION_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="BIRTH_PROVINCE_NAME" HeaderText="Tỉnh/TP nơi sinh"
                            UniqueName="BIRTH_PROVINCE_NAME" SortExpression="BIRTH_PROVINCE_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="BIRTH_DISTRICT_NAME" HeaderText="Quận/huyện nơi sinh"
                            UniqueName="BIRTH_DISTRICT_NAME" SortExpression="BIRTH_DISTRICT_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="BIRTH_WARD_NAME" HeaderText="Phường/Xã nơi sinh"
                            UniqueName="BIRTH_WARD_NAME" SortExpression="BIRTH_WARD_NAME">
                        </tlk:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <HeaderStyle Width="120px" />
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin kinh nghiệm làm việc")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgWorkInfo" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn DataField="JOIN_DATE" HeaderText="Ngày bắt đầu"
                            UniqueName="JOIN_DATE" SortExpression="JOIN_DATE" DataFormatString="{0:MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="END_DATE" HeaderText="Ngày kết thúc"
                            UniqueName="END_DATE" SortExpression="END_DATE" DataFormatString="{0:MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="COL_THAM_NIEN" HeaderText="Thâm niên"
                            UniqueName="COL_THAM_NIEN" SortExpression="COL_THAM_NIEN">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="COMPANY_NAME" HeaderText="Tên công ty"
                            UniqueName="COMPANY_NAME" SortExpression="COMPANY_NAME">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="COMPANY_ADDRESS" HeaderText="Địa chỉ công ty"
                            UniqueName="COMPANY_ADDRESS" SortExpression="COMPANY_ADDRESS">
                        </tlk:GridBoundColumn>
                        <%--<tlk:GridBoundColumn DataField="TITLE_NAME_BEFORE" HeaderText="Chức vụ trước đây"
                            UniqueName="TITLE_NAME_BEFORE" SortExpression="TITLE_NAME_BEFORE">
                        </tlk:GridBoundColumn>--%>
                        <tlk:GridBoundColumn DataField="WORK" HeaderText="Vị trí công việc trước đây"
                            UniqueName="WORK" SortExpression="WORK">
                        </tlk:GridBoundColumn>
                        <tlk:GridBoundColumn DataField="TER_REASON" HeaderText="Lý do nghỉ việc"
                            UniqueName="TER_REASON" SortExpression="TER_REASON">
                        </tlk:GridBoundColumn>
                        <tlk:GridCheckBoxColumn DataField="IS_HSV" HeaderText="HSV/Hoàn vũ" UniqueName="IS_HSV"
                            ShowFilterIcon="false" SortExpression="IS_HSV" AllowFiltering="false" HeaderStyle-Width="70px">
                        </tlk:GridCheckBoxColumn>
                        <tlk:GridCheckBoxColumn DataField="IS_THAMNIEN" HeaderText="Tính thâm niên" UniqueName="IS_THAMNIEN"
                            ShowFilterIcon="false" SortExpression="IS_THAMNIEN" AllowFiltering="false" HeaderStyle-Width="70px">
                        </tlk:GridCheckBoxColumn>
                    </Columns>
                    <HeaderStyle Width="120px" />
                </MasterTableView>
            </tlk:RadGrid>
        </div>

        <div class="item-head cv-header-title">
            <p>
                <%# Translate("Thông tin hồ sơ lưu trữ")%>
            </p>
            <hr />
        </div>
        <div class="cv-content-grid">
            <tlk:RadGrid PageSize="5" ID="rgDocument" runat="server" Height="300px" Width="99%" AllowPaging="True" AllowSorting="True" AllowMultiRowSelection="true">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ID">
                    <Columns>
                        <tlk:GridBoundColumn HeaderText="Tên tài liệu" DataField="DOCUMENT_NAME" ReadOnly="true"
                            UniqueName="DOCUMENT_NAME" SortExpression="DOCUMENT_NAME" />
                        <tlk:GridBoundColumn HeaderText="Loại tài liệu" DataField="TYPE_DOCUMENT_NAME" UniqueName="TYPE_DOCUMENT_NAME"
                            ReadOnly="true" SortExpression="TYPE_DOCUMENT_NAME" />
                        <tlk:GridCheckBoxColumn HeaderText="Là tài liệu bắt buộc" DataField="MUST_HAVE" ReadOnly="true"
                            UniqueName="MUST_HAVE" SortExpression="MUST_HAVE" AllowFiltering="false" HeaderStyle-Width="50px" />
                        <tlk:GridCheckBoxColumn HeaderText="Cho phép upload file" DataField="ALLOW_UPLOAD_FILE"
                            ReadOnly="true" UniqueName="ALLOW_UPLOAD_FILE" SortExpression="ALLOW_UPLOAD_FILE"
                            AllowFiltering="false" HeaderStyle-Width="50px" />
                        <tlk:GridBoundColumn HeaderText="Ngày nộp" DataField="DATE_SUBMIT" UniqueName="DATE_SUBMIT" SortExpression="DATE_SUBMIT"
                            HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        </tlk:GridBoundColumn>
                        <tlk:GridCheckBoxColumn HeaderText="Đã nộp" DataField="IS_SUBMITED"
                            ReadOnly="true" UniqueName="IS_SUBMITED" SortExpression="IS_SUBMITED"
                            AllowFiltering="false" HeaderStyle-Width="50px" />
                        <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="REMARK" UniqueName="REMARK" SortExpression="REMARK">
                        </tlk:GridBoundColumn>
                    </Columns>
                    <HeaderStyle Width="120px" />
                </MasterTableView>
            </tlk:RadGrid>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">


        var enableAjax = true;

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnKeyPress(sender, eventArgs) {
            debugger;
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_EmpCuriculumVitae_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true)
        function OpenEdit() {
            var empID = $('#<%= hidID.ClientID%>').val();
            if (empID == "") {
                var m = '<%= Translate("Chưa chọn nhân viên") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_EmpDtl&group=Business&emp=' + empID + '&state=Edit', "_self");
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
