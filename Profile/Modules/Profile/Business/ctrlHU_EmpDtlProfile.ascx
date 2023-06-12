<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpDtlProfile.ascx.vb"
    Inherits="Profile.ctrlHU_EmpDtlProfile" %>
<%@ Import Namespace="Common" %>
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupDirect" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupLevel" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phEmp_Find" runat="server"></asp:PlaceHolder>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />

<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style type="text/css">
    div .rlbItem {
        float: left;
        width: 250px;
    }

    .lb3 {
        text-align: right;
        padding-right: 5px;
        padding-left: 5px;
        vertical-align: middle;
        width: 10%;
        height: 35px;
    }

    .control3 {
        width: 15%;
        height: 35px;
    }

    .RadListBox_Metro .rlbGroup, .RadListBox_Metro .rlbTemplateContainer {
        border: none !important;
    }
    .RadComboBox_Metro .rcbInput
{
    width:250%;
}
 .RadInput_Metro, .RadInputMgr_Metro 
    {
        float:left;
    }
  .RadComboBox table td.rcbInputCell
  {
      overflow: hidden;
  }  
  .RadWindow_Metro .RadAjaxPanel p:nth-child(2)
  {
      height:  50px !important;
      overflow: auto;
  }
    .rpHeaderTemplate:hover {
        display: block;
        cursor: pointer;
        background-color: #e0e0e0 !important;
        background-position: center !important;
    }
    .RadPanelBar_Metro div.rpExpandable.rpFocused, .RadPanelBar_Metro div.rpExpandable.rpSelected,.RadPanelBar_Metro a.rpExpanded, .RadPanelBar_Metro div.rpExpanded{
        color: #000000 !important;
    }
    .RadPanelBar_Metro .rpRootLink.rpSelected{
        border-color: #f9f9f9;
        background-color: #f9f9f9;
    }
    .RadPanelBar_Metro .rpRootLink.rpFocused {
        box-shadow: inset 0 0 5px #f9f9f9;
    }
    .RadPanelBar .rpOut {
        border: none;
    }
    .RadPanelBar_Metro a.rpExpanded, .RadPanelBar_Metro div.rpExpanded {
        background-color: #ccc !important;
    }
    .RadPanelBar_Metro .rpRootLink.rpSelected{
        border-color: #e0e0e0;
    }
    li.rpLast div{
        border: none !important;
    }
    .RadPanelBar_Metro .rpGroup .rpLink.rpFocused{
        box-shadow: inset 0 0 5px #e7e7e7 !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="ToolbarPane" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server" OnClientButtonClicking="clientButtonClicking"
            ValidationGroup="EmpProfile" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Visible="false" Scrolling="X">
        <asp:Panel runat="server" ID="Panel1">
            <table class="table-form" style="width: 99%" onkeydown="return (event.keyCode!=13)">
               
            </table>
        </asp:Panel>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane3" runat="server" Height="40px" Scrolling="None">
        <table class="table-form" >
            <tr>
                <td>
                    <asp:Label ID="Label35" runat="server" Text="Xem Mã/ Tên nhân viên khác"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode_Find" AutoPostBack="true"  runat="server" Width="150px" >
                    </tlk:RadTextBox>
                </td>
                <td>
                    <tlk:RadButton runat="server" ID="btnCurciculumViate" Text="LL Trích Ngang" Width="100px" OnClientClicking="OpenCuriculumVitae">
                    </tlk:RadButton>
                </td>
                <td>
                    <div style="margin-left: 450px;">
                    <asp:CheckBox ID="chkID_NO" Text="Kiểm tra trùng Số CMND" runat="server" Checked="true"/>
                    <asp:Label ID="lable11" runat="server" Text=""></asp:Label>
                    <asp:CheckBox ID="chkNAME" Text="Kiểm tra trùng Họ & tên nhân viên" runat="server"  Checked="true"/>
                    </div>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="DetailPane" runat="server" Scrolling="None" Height="83%">
        
        <tlk:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server" Width="100%"
            Style="overflow: auto" Height="99%">
            <tlk:RadPageView ID="rpvEmpInfo" runat="server" Width="100%">
                <table style="width: 99%" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td colspan="6">
                            <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
                                ValidationGroup="EmpProfile" />
                        </td>
                    </tr>
                </table>
                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbEmoCode" Text="Mã nhân viên"></asp:Label>
                        </td>
                        <td class="control3">
                            <asp:HiddenField ID="hidID" runat="server" />
                            <asp:HiddenField ID="hidIDCopy" runat="server" />
                            <asp:HiddenField ID="hidIsTer" runat="server" />
                            <asp:HiddenField ID="hidContractID" runat="server" />
                            <asp:HiddenField ID="hidWorkingID" runat="server" />
                            <asp:HiddenField ID="hidPrevious" runat="server" />
                            <asp:HiddenField ID="hidNext" runat="server" />
                            <asp:HiddenField ID="hidOrgID" runat="server" />
                            <asp:HiddenField ID="hidDirectManager" runat="server" />
                            <asp:HiddenField ID="hidLevelManager" runat="server" />
                            <tlk:RadTextBox ID="txtEmpCODE" runat="server" ReadOnly="true">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbWorkStatus" Text="Trạng thái nhân viên"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboWorkStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbTimeID" Text="Mã chấm công"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadTextBox ID="txtTimeID" runat="server" >
                            </tlk:RadTextBox>
                        </td>

                        <td class="lb3">
                            <asp:Label runat="server" ID="Label9" Text="Mã nhân viên cũ"></asp:Label>
                            <span class="lbReq"></span>
                        </td>
                        <td class="control3">
                            <tlk:RadTextBox ID="rtEmpCode_OLD" runat="server">
                            </tlk:RadTextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbFirstNameVN" Text="Họ và tên lót"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td class="control3">
                            <tlk:RadTextBox ID="txtFirstNameVN" runat="server"  ClientEvents-OnValueChanged="OnTextChanged">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqFirstNameVN" ControlToValidate="txtFirstNameVN"
                                runat="server" ErrorMessage="Bạn phải nhập họ và tên lót" ToolTip="Bạn phải nhập họ và tên lót ">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="lbLastNameVN" Text="Tên nhân viên"></asp:Label>
                            <span class="lbReq">*</span>
                        </td>
                        <td class="control3">
                            <tlk:RadTextBox ID="txtLastNameVN" runat="server"  ClientEvents-OnValueChanged="OnTextChanged">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqLastNameVN" ControlToValidate="txtLastNameVN"
                                runat="server" ErrorMessage="Bạn phải nhập tên" ToolTip="Bạn phải nhập tên">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="Label1" Text="Loại nhân viên"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboEmpStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                OnClientItemsRequesting="OnClientItemsRequesting">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb3">
                            <asp:Label runat="server" ID="Label13" Text="Mã thẻ"></asp:Label>
                        </td>
                        <td class="control3">
                            <tlk:RadComboBox runat="server" ID="cboMaThe" >
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                </table>
                <tlk:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%" ExpandMode="MultipleExpandedItems"
                    OnClientItemClicking="HeightGridClick">
                    <Items>
                        <tlk:RadPanelItem Expanded="false" runat="server" id="cusValText">
                            <HeaderTemplate>
                                <span id="Span1" class="rpText" runat="server" style="margin-right: -10px">THÔNG TIN CÔNG TÁC
                                            <span style="text-align: right; position: unset; right: 5px; color: red;">*
                                                <span style="padding-right: 5px;"></span>
                                                <span class="rpExpandHandle"></span>
                                            </span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbOrgName2" Text="Phòng ban"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td colspan="3">
                                            <tlk:RadTextBox runat="server" ID="txtOrgName2" ReadOnly="true" Width="465px" style="margin: 2px 0 0 0;"/>
                                            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator1"
                                                ControlToValidate="txtOrgName2" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>"
                                                ToolTip="<%$ Translate: Bạn phải chọn bộ phận %>">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label37" Text="Công ty"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtCongTy" ReadOnly="true"></tlk:RadTextBox>
                                        </td>
                                        
                                        
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label42" Text="Đơn vị ký hợp đồng"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td colspan="3" class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboContractedUnit" AutoPostBack="true"  width="490px" CausesValidation="false">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator11" ControlToValidate="cboContractedUnit"
                                                runat="server" ErrorMessage="Bạn phải chọn Đơn vị ký hợp đồng" ToolTip="Bạn phải chọn Đơn vị ký hợp đồng">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label10" Text="Đối tượng nhân viên"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="rcOBJECT_EMPLOYEE">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator6" ControlToValidate="rcOBJECT_EMPLOYEE"
                                                runat="server" ErrorMessage="Bạn phải chọn Đối tượng nhân viên" ToolTip="Bạn phải chọn Đối tượng nhân viên">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbTitle" Text="Vị trí công việc"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3" colspan="3">
                                             <tlk:RadComboBox runat="server" ID="cboTitle" AutoPostBack ="true" width="490px" CausesValidation="false"
                                                Filter="Contains"
                                                HighlightTemplatedItems="true">
                                                 <HeaderTemplate>
                                                    <table style="width: 1070px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 570px;">
                                                                Tên vị trí
                                                            </td>
                                                            <td style="width: 250px;">
                                                                Master
                                                            </td>
                                                                <td style="width: 250px;">
                                                                Interim
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table style="width: 1070px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 570px;">
                                                                <%# Eval("NAME")%>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <%# Eval("MASTER_NAME")%>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <%# Eval("INTERIM_NAME")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ValidationGroup="EmpProfile" ID="cusTitle" runat="server" ErrorMessage="Bạn phải chọn Chức danh công việc TT"
                                                ToolTip="Bạn phải chọn Chức danh công việc TT" ClientValidationFunction="cusTitle">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label62" Text="Cấp bậc"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboJobLevel">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqJobLevel" ControlToValidate="cboJobLevel"
                                                runat="server" ErrorMessage="Bạn phải chọn Cấp bậc" ToolTip="Bạn phải chọn Cấp bậc">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label63" Text="Thâm niên"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="rtThamNien" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbDirectManager" Text="Vị trí quản lý trực tiếp"></asp:Label>
                                            <span class="lbReq"></span>
                                        </td>
                                        <td class="control3" colspan="3">
                                            <tlk:RadTextBox runat="server" ID="txtDirectManager" ReadOnly="true" Width="490px" />
                                            
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbmanager" Text="Quản lý trực tiếp"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtmanager" ReadOnly="true"></tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbObject" Text="Đối tượng chấm công"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboObject" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator9" ControlToValidate="cboObject"
                                                runat="server" ErrorMessage="Bạn phải chọn Đối tượng chấm công" ToolTip="Bạn phải chọn Đối tượng chấm công">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label11" Text="Nơi làm việc"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="rcRegion">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator7" ControlToValidate="rcRegion"
                                                runat="server" ErrorMessage="Bạn phải chọn Nơi làm việc" ToolTip="Bạn phải chọn Nơi làm việc">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label12" Text="Thời gian làm việc"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="rcOBJECT_ATTENDANT">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator8" ControlToValidate="rcOBJECT_ATTENDANT"
                                                runat="server" ErrorMessage="Bạn phải chọn Thời gian làm việc" ToolTip="Bạn phải chọn Thời gian làm việc">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbObjectLabor" Text="Loại hình lao động"></asp:Label>
                                            <span class="lbReq" runat="server" id="spObjectLabor">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboObjectLabor" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="reqObjectLabor" ControlToValidate="cboObjectLabor"
                                                runat="server" ErrorMessage="Bạn phải chọn loại hình lao động" ToolTip="Bạn phải chọn loại hình lao động">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label44" Text="Ngày vào tổng công ty"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdCoporationDate" Enabled="false" DateInput-Enabled="true">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbJoinDate" Text="Ngày vào làm/thử việc"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdJoinDate" Enabled="false" DateInput-Enabled="true">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label4" Text="Ngày vào làm chính thức"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdJoinDateState" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbter_effect_date" Text="Ngày nghỉ việc"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdter_effect_date" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label8" Text="Ngày tính thâm niên"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdSeniorityDate">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContractNo" Text="Số hợp đồng"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContractType" Text="Loại hợp đồng"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtContractType" runat="server" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContractEffectDate" Text="Từ ngày"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContractExpireDate" Text="Đến ngày"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" Enabled="false" DateInput-Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" runat="server" id="cusValText1">
                            <HeaderTemplate>
                                <span id="Span2" class="rpText" runat="server" style="margin-right: -10px">SƠ YẾU LÝ LỊCH
                                            <span style="text-align: right; position: unset; right: 5px; color: red;">*
                                                <span style="padding-right: 5px;"></span>
                                                <span class="rpExpandHandle"></span>
                                            </span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBirthDate" Text="Ngày sinh"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdBirthDate">
                                            </tlk:RadDatePicker>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator13" ControlToValidate="rdBirthDate"
                                                runat="server" ErrorMessage="Bạn phải chọn Ngày sinh" ToolTip="Bạn phải chọn Ngày sinh">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPROVINCEEMP_ID" Text="Tỉnh/TP NS"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cbPROVINCEEMP_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator10" ControlToValidate="cbPROVINCEEMP_ID"
                                                runat="server" ErrorMessage="Bạn phải chọn Tỉnh/Thành phố Nơi đăng ký khai sinh" ToolTip="Bạn phải chọn Tỉnh/Thành phố Nơi đăng ký khai sinh">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbDISTRICTEMP_ID" Text="Quận/Huyện NS"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cbDISTRICTEMP_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbWARDEMP_ID" Text="Xã/Phường NS"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cbWARDEMP_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbGender" Text="Giới tính"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboGender" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator25" ControlToValidate="cboGender"
                                                runat="server" ErrorMessage="Bạn phải chọn Giới tính" ToolTip="Bạn phải chọn Giới tính">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbFamilyStatus" Text="Tình trạng hôn nhân"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboFamilyStatus" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator15" ControlToValidate="cboFamilyStatus"
                                                runat="server" ErrorMessage="Bạn phải chọn Tình trạng hôn nhân" ToolTip="Bạn phải chọn Tình trạng hôn nhân">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNative" Text="Dân tộc"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNative" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator19" ControlToValidate="cboNative"
                                                runat="server" ErrorMessage="Bạn phải chọn Dân tộc" ToolTip="Bạn phải chọn Dân tộc">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbReligion" Text="Tôn giáo"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboReligion" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator20" ControlToValidate="cboReligion"
                                                runat="server" ErrorMessage="Bạn phải chọn Tôn giáo" ToolTip="Bạn phải chọn Tôn giáo">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td class="lb3">
                                            <asp:Label runat="server" ID="lbNationlity" Text="Quốc tịch"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboNationlity" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator16" ControlToValidate="cboNationlity"
                                                runat="server" ErrorMessage="Bạn phải chọn Quốc tịch" ToolTip="Bạn phải chọn Quốc tịch">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbID_NO" Text="CMND/CCCD"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtID_NO">
                                            </tlk:RadTextBox>
                                            
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbIDDate" Text="Ngày cấp"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdIDDate">
                                            </tlk:RadDatePicker>
                                            
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbIDPlace" Text="Nơi cấp"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboIDPlace">
                                            </tlk:RadComboBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPROVINCENQ_ID" Text="Nguyên quán"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cbPROVINCENQ_ID" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting" EnabledLoadOnDemand="True">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator14" ControlToValidate="cbPROVINCENQ_ID"
                                                runat="server" ErrorMessage="Bạn phải chọn Nguyên quán" ToolTip="Bạn phải chọn Nguyên quán">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPitCode" Text="Mã số Thuế"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtPitCode" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label61" Text="Ngày cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdDayPitcode" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label38" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="rcPlacePitCodeMST">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNhomMau" Text="Nhóm máu"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtNhomMau" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbChieuCao" Text="Chiều cao(cm)"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtChieuCao" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCanNang" Text="Cân nặng(kg)"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtCanNang" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label14" Text="Huyết áp"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtHuyeAp" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td class="lb3">
                                            <asp:Label runat="server" ID="lblLOAI_SUC_KHOE" Text="Loại sức khỏe"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="rcLOAI_SUC_KHOE" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label48" Text="Mắt trái"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtMatTrai" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label49" Text="Mắt phải"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtMatPhai" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label50" Text="Tim"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtTim" runat="server" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lblNGAY_KHAM" Text="Ngày khám"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdNGAY_KHAM">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lblGHI_CHU_SUC_KHOE" Text="Ghi chú sức khỏe"></asp:Label>
                                        </td>
                                        <td class="control3" colspan="5">
                                            <tlk:RadTextBox ID="rtGHI_CHU_SUC_KHOE" runat="server" Width="100%" SkinID="Textbox50">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>

                        <tlk:RadPanelItem Expanded="false">
                            <HeaderTemplate>
                                <span id="Span5" class="rpText" runat="server" style="margin-right: -10px">THÔNG TIN BẢO HIỂM
                                        <span class="rpExpandHandle"></span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                     <tr> 
                                         <td></td>
                                        <td class="lb3" align="right" style="text-align:left">
                                            <asp:CheckBox ID="chkForeign" Text="Người nước ngoài" runat="server"/>
                                        </td>                                      
                                    </tr>  
                                    <tr>                                         
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbInsRegion" Text="Vùng bảo hiểm"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboInsRegion" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" Enabled="false">
                                            </tlk:RadComboBox>
                                            
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBookNo" Text="Số sổ BHXH"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="rtBookNo">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label15" Text="Nơi KCB"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="rcNoiKhamChuaBenh">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label51" Text="Số thẻ BHYT"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtHealthNo">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                    </tr>
                                    <tr id="hid_ins0" runat="server">
                                        <td colspan="3" style="color: blue; font-weight: bold;">
                                            <asp:Label runat="server" ID="Label28" Text="Thông tin bảo hiểm sức khỏe"></asp:Label></td>
                                    </tr>
                                    <tr id="hid_ins1" runat="server">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label16" Text="Số HĐ bảo hiểm"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtHDBaoHiem" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label17" Text="Đơn vị bảo hiểm"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtDVBH" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                    </tr>
                                    <tr id="hid_ins2" runat="server">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label18" Text="Chương trình BH"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtChuongTrinhBH" ReadOnly="true">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label19" Text="Số tiền CTBH"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadNumericTextBox runat="server" ID="rnSoTien" ReadOnly="true">
                                            </tlk:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr id="hid_ins3" runat="server">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label20" Text="Ngày hiệu lực"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdNgayHieuLuc" SkinID="LoadDemand" Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label21" Text="Ngày hết hiệu lực"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdNgayHetHL" ReadOnly="true" Enabled="false">
                                            </tlk:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr id="hid_ins4" runat="server">
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label22" Text="Số tiền bảo hiểm"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadNumericTextBox runat="server" ID="rnMoneyBH" ReadOnly="true">
                                            </tlk:RadNumericTextBox>
                                        </td>

                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>

                        <tlk:RadPanelItem Expanded="false" runat="server" id="cusValText2">
                            <HeaderTemplate>
                                <span id="Span3" class="rpText" runat="server" style="margin-right: -10px">TRÌNH ĐỘ VĂN HÓA
                                            <span style="text-align: right; position: unset; right: 5px; color: red;">*
                                                <span style="padding-right: 5px;"></span>
                                                <span class="rpExpandHandle"></span>
                                            </span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbAcademy" Text="Trình độ văn hóa"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboAcademy" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLearningLevel" Text="Trình độ học vấn"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboLearningLevel" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator21" ControlToValidate="cboLearningLevel"
                                                runat="server" ErrorMessage="Bạn phải chọn Trình độ học vấn" ToolTip="Bạn phải chọn Trình độ học vấn">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbMajor" Text="Trình độ chuyên môn"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboMajor" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbSchool" Text="Trường học"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboGraduateSchool" runat="server">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNamTN" Text="Năm tốt nghiệp"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadNumericTextBox ID="txtNamTN" runat="server" NumberFormat-GroupSeparator="">
                                            </tlk:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLangLevel" Text="Trình độ ngoại ngữ"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox ID="cboLangLevel" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbLangMark" Text="Điểm số"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtLangMark" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" runat="server" id="cusValText3">
                            <HeaderTemplate>
                                <span class="rpOut">
                                    <span id="Span4" class="rpText" runat="server" style="margin-right: -10px">THÔNG TIN LIÊN HỆ
                                            <span style="text-align: right; position: unset; right: 0px; color: red;">*                                                                                              
                                            </span>
                                        <span class="rpExpandHandle"></span>
                                    </span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNoHouseHolds" Text="Số hộ khẩu"></asp:Label>
                                        </td>
                                        <td class="control3" style="width: 15%">
                                            <tlk:RadTextBox runat="server" ID="txtNoHouseHolds">
                                            </tlk:RadTextBox>
                                            
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCodeHouseHolds" Text="Mã hộ gia đình"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtCodeHouseHolds">
                                            </tlk:RadTextBox>
                                        </td>

                                        <td class="lb3">
                                            <asp:Label ID="Label36" runat="server" Text="Quan hệ với chủ hộ"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboRELATE_OWNER">
                                            </tlk:RadComboBox>
                                        </td>
                                        
                                        <td class="lb3"  style="text-align:left">
                                            <asp:CheckBox ID="ckCHUHO" Text="Là chủ hộ" runat="server" Checked="false" onclick="enableTextbox(this.id)" AutoPostBack="true" />
                                        </td>
                                        <td class="control3">

                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPerAddress" Text="Địa chỉ thường trú"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtPerAddress" runat="server" Width="97%">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator5"
                                                ControlToValidate="txtPerAddress" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Địa chỉ thường trú %>"
                                                ToolTip="<%$ Translate:  Bạn phải nhập Địa chỉ thường trú %>">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3" style="text-align:left">
                                            <asp:CheckBox ID="chkSaoChep" Text="Sao chép ĐC" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckBox_CheckChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPer_Province" Text="Thành phố"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator2"
                                                ControlToValidate="cboPer_Province" runat="server" ErrorMessage="Bạn phải chọn Thành phố địa chỉ thường trú"
                                                ToolTip="Bạn phải chọn Thành phố địa chỉ thường trú">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPer_District" Text="Quận huyện"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator4"
                                                ControlToValidate="cboPer_District" runat="server" ErrorMessage="Bạn phải chọn Quận huyện địa chỉ thường trú"
                                                ToolTip="Bạn phải chọn Quận huyện địa chỉ thường trú">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPer_Ward" Text="Xã phường"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboPer_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNavAddress1" Text="Địa chỉ liên hệ"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtNavAddress" runat="server" Width="97%">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3" align="right" style="text-align:left">
                                            <asp:CheckBox ID="chkTamtru" Text="Tạm trú" runat="server" Checked="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNav_Province" Text="Thành phố"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_Province" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ID="cusNav_Province" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thành phố địa chỉ tạm trú %>"
                                                ToolTip="<%$ Translate: Bạn phải chọn Thành phố địa chỉ tạm trú %>" ClientValidationFunction="cusNav_Province">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNav_District" Text="Quận huyện"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_District" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                            <asp:CustomValidator ID="cusNav_District" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Quận huyện địa chỉ tạm trú %>"
                                                ToolTip="<%$ Translate: Bạn phải chọn Quận huyện địa chỉ tạm trú %>" ClientValidationFunction="cusNav_District">
                                            </asp:CustomValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNav_Ward" Text="Xã phường"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="cboNav_Ward" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbMobilePhone" Text="Điện thoại di động"></asp:Label>
                                            <span class="lbReq">*</span>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtMobilePhone">
                                            </tlk:RadTextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="EmpProfile" ID="RequiredFieldValidator24"
                                                ControlToValidate="txtMobilePhone" runat="server" ErrorMessage="Bạn phải nhập Điện thoại di động"
                                                ToolTip="Bạn phải nhập Điện thoại di động">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbHomePhone" Text="Điện thoại cố định"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtHomePhone">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPerEmail" Text="Email cá nhân"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtPerEmail">
                                            </tlk:RadTextBox>
                                            <asp:RegularExpressionValidator ValidationGroup="EmpProfile" ID="RegularExpressionValidator1"
                                                ControlToValidate="txtPerEmail" ValidationExpression="^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"
                                                runat="server" ErrorMessage="<%$ Translate: Định dạng Email cá nhân không chính xác %>"
                                                ToolTip="<%$ Translate: Định dạng Email cá nhân không chính xác %>"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbWorkEmail" Text="Email công ty"></asp:Label>
                                        </td>
                                        <td class="control3" colspan="3">
                                            <tlk:RadTextBox runat="server" ID="txtWorkEmail">
                                            </tlk:RadTextBox>
                                            <asp:RegularExpressionValidator ValidationGroup="EmpProfile" ID="regEMAIL" ControlToValidate="txtWorkEmail"
                                                ValidationExpression="^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"
                                                runat="server" ErrorMessage="<%$ Translate: Định dạng Email công ty không chính xác %>"
                                                ToolTip="<%$ Translate: Định dạng Email công ty không chính xác %>"></asp:RegularExpressionValidator>
                                            <tlk:RadButton ID="btnCheckEmail" runat="server" Text="<%$ Translate: Kiểm tra %>"
                                                OnClientClicking="btnCheckEmailClick" AutoPostBack="false">
                                            </tlk:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" >
                            <HeaderTemplate>
                                <span id="Span6" class="rpText" runat="server" style="margin-right: -10px">THÔNG TIN LIÊN HỆ KHẨN CẤP VÀ GIẤY TỜ TÙY THÂN
                                        <span class="rpExpandHandle"></span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContactPerson" Text="Người liên hệ"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtContactPerson" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label3" Text="Mối quan hệ NLH"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboRelationNLH" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbContactPersonPhone" Text="Số điện thoại NLH"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtContactPersonPhone" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label7" Text="Điện thoại di động"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtContactMobilePhone" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label23" Text="Số CMND"></asp:Label>
                                            <span class="lbReq"></span>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtCONTACT_PER_IDNO">
                                            </tlk:RadTextBox>


                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label24" Text="Ngày cấp"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdCONTACT_PER_EFFECT_DATE_IDNO">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label25" Text="Ngày hết hạn"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdCONTACT_PER_EXPIRE_DATE_IDNO">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label27" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="cboCONTACT_PER_PLACE_IDNO">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label5" Text="Địa chỉ NLH"></asp:Label>
                                        </td>
                                        <td colspan="5">
                                            <tlk:RadTextBox ID="txtAddressPerContract" runat="server" Width="100%">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>

                                    
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false">
                            <HeaderTemplate>
                                <span id="Span7" class="rpText" runat="server" style="margin-right: -10px">THÔNG TIN VISA, PASSPORT, SỐ LAO ĐỘNG
                                        <span class="rpExpandHandle"></span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPassNo" Text="Số hộ chiếu"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtPassNo" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPassDate" Text="Ngày cấp"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker ID="rdPassDate" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPassExpireDate" Text="Ngày hết hạn"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker ID="rdPassExpireDate" runat="server">
                                            </tlk:RadDatePicker>
                                            <asp:CompareValidator ValidationGroup="EmpProfile" ID="cvalPassDate" runat="server"
                                                ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>" ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                                ControlToValidate="rdPassExpireDate" ControlToCompare="rdPassDate" Operator="GreaterThanEqual"
                                                Type="Date">
                                            </asp:CompareValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label39" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox runat="server" ID="rcNoiCapHoChieu">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>
                                    

                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbVisa" Text="Visa"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtVisa" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbVisaDate" Text="Ngày cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdVisaDate" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbVisaExpireDate" Text="Ngày hết hạn"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker ID="rdVisaExpireDate" runat="server">
                                            </tlk:RadDatePicker>
                                            <asp:CompareValidator ValidationGroup="EmpProfile" ID="compare_VisaDate_ExpireDate"
                                                runat="server" ToolTip="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>"
                                                ErrorMessage="<%$ Translate: Ngày hết hạn phải lớn hơn ngày cấp %>" Type="Date"
                                                Operator="GreaterThan" ControlToCompare="rdVisaDate" ControlToValidate="rdVisaExpireDate"></asp:CompareValidator>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label26" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="rcNoiCapVisa">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label31" Text="Số sổ lao động"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox runat="server" ID="txtSoSoLaoDong" SkinID="Number">
                                            </tlk:RadTextBox>
                                        </td>

                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label32" Text="Ngày cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdNgayCapSSLD">
                                            </tlk:RadDatePicker>
                                        </td>

                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label33" Text="Ngày hết hạn"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadDatePicker runat="server" ID="rdNgayHetHanSSLD">
                                            </tlk:RadDatePicker>
                                        </td>

                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label34" Text="Nơi cấp"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadComboBox runat="server" ID="rcNoiCapSSLD">
                                            </tlk:RadComboBox>
                                        </td>
                                    </tr>

                                    
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false">
                            <HeaderTemplate>
                                <span id="Span8" class="rpText" runat="server" style="margin-right: -10px">THÔNG TIN TÀI KHOẢN
                                        <span class="rpExpandHandle"></span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPerson_Inheritance" Text="Tên người thụ hưởng"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtPerson_Inheritance">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBankNo" Text="Số tài khoản"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtBankNo" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                    </tr>
                                    <tr>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBank" Text="Ngân hàng"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboBank" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        <td class="lb3"></td>
                                        <td class="control3"></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbBankBranch" Text="Chi nhánh"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadComboBox ID="cboBankBranch" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                                                OnClientItemsRequesting="OnClientItemsRequesting">
                                            </tlk:RadComboBox>
                                        </td>
                                        
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false" >
                            <HeaderTemplate>
                                <span id="Span9" class="rpText" runat="server" style="margin-right: -10px">THÔNG TIN CHÍNH TRỊ XÃ HỘI
                                        <span class="rpExpandHandle"></span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;margin-bottom: 25px;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td class="lb3"></td>
                                        <td class="control3" >
                                            <asp:CheckBox ID="ckCONG_DOAN" Text="Đoàn viên ĐTN" runat="server" />
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label6" Text="Chức vụ ĐTN"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtChucVuDTN">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label52" Text="Ngày vào ĐTN"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker ID="rdNgayVaoDTN" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label53" Text="Nơi vào"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtNoiVaoDTN" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="control3">
                                            <asp:CheckBox ID="chkDoanPhi" runat="server" Text="<%$ Translate: Tham gia công đoàn %>" />
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCHUC_VU_DOANs" Text="Chức vụ công đoàn"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="rtCHUC_VU_DOAN">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNgayVaoDoan" Text="Ngày vào Đoàn"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker ID="rdNgayVaoDoan" runat="server">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNoiVaoDoan" Text="Nơi vào"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox ID="txtNoiVaoDoan" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td class="control3">
                                            <asp:CheckBox ID="ckDANG" Text="Là Đảng viên" runat="server" />
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNGAY_DB_DANG" Text="Ngày vào Đảng"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdNGAY_VAO_DANG_DB">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbNGAY_VAO_DANG" Text="Ngày vào Đảng chính thức"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadDatePicker runat="server" ID="rdNGAY_VAO_DANG">
                                            </tlk:RadDatePicker>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbPlaceDang" Text="Nơi vào"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtNoiVaoDang" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="lbCHUC_VU_DANG" Text="Chức vụ Đảng"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="rtCHUC_VU_DANG">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label54" Text="Chi bộ sinh hoạt"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtCBOSinhHoat" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label55" Text="Số thẻ Đảng"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtSoTheDang" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label56" Text="TĐ Lý luận chính trị"></asp:Label>
                                        </td>
                                        <td class="control3">
                                            <tlk:RadTextBox runat="server" ID="txtLLCT">
                                            </tlk:RadTextBox>
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label57" Text="Số lý lịch"></asp:Label>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtSoLyLich" runat="server">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                        <tlk:RadPanelItem Expanded="false"  ID="rpItemCheck" runat="server">
                            <HeaderTemplate>
                                <span id="Span10" class="rpText" runat="server" style="margin-right: -10px">Sao chép dữ liệu cũ
                                        <span class="rpExpandHandle"></span>
                                </span>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <table class="table-form" style="width: 99%; padding: 0 30px; display: inline-table; text-align: left;" onkeydown="return (event.keyCode!=13)">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChkCopy_Woking" Text="Sao chép quá trình công tác" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ChkCopy_Salary" Text="Sao chép quá trình lương" runat="server" />
                                        </td>
                                        <td colspan="2">
                                            <asp:CheckBox ID="ChkCopy_Training" Text="Sao chép quá trình đào tạo" runat="server" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="ChkCopy_Family" Text="Sao chép thông tin người thân" runat="server" />
                                        </td>
                                         <td>
                                            <asp:CheckBox ID="ChkCopy_ExpWorking" Text="Sao chép kinh nghiệm làm việc" runat="server" />
                                        </td>
                                    </tr>
                                    <tr runat="server" ID="LineCopyWToExp">
                                        <td >
                                            <asp:CheckBox ID="ChkCopy_WorkingToExp" Text="Sao chép Quá trình công tác --> kinh nghiệm làm việc" runat="server" />
                                        </td>
                                        <td >
                                            <asp:CheckBox ID="ChkCal_SENIORITY" Text="Tính thâm niên" runat="server" AutoPostBack="true" CausesValidation="false" />
                                        </td>
                                        <td class="lb3">
                                            <asp:Label runat="server" ID="Label29" Text="Thời gian gián đoạn"></asp:Label>
                                        </td>
                                        <td >
                                            <tlk:RadTextBox ID="txtMonths" runat="server" Width="45px" ReadOnly="true">
                                            </tlk:RadTextBox>                                        
                                            <asp:Label runat="server" ID="Label30" Text="Tháng"></asp:Label>
                                        </td>
                                       
                                    </tr>
                                    <tr></tr>
                                </table>
                            </ContentTemplate>
                        </tlk:RadPanelItem>
                    </Items>
                </tlk:RadPanelBar>
            </tlk:RadPageView>
            
        </tlk:RadMultiPage>
    </tlk:RadPane>
    <tlk:RadPane ID="checkIDNOandNAME" runat="server" Height="50px">
        <table class="table-form" style="width: 99%; " onkeydown="return (event.keyCode!=13)">
            <tr>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="Hồ sơ lương" ID="btnWage" OnClientClicking="btnWageClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="Hợp đồng" ID="btnContract" OnClientClicking="btnContractClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="Phụ lục HĐ" ID="btnContractAppendix" OnClientClicking="btnContractAppendixClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="KNLV Trước" ID="btnBeforeWorkExp" OnClientClicking="btnBeforeWorkExpClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="Người thân" ID="btnFamily" OnClientClicking="btnFamilyClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="Đào tạo" ID="btnTraining" OnClientClicking="btnTrainingClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="Kiêm nhiệm" ID="btnConcurrently" OnClientClicking="btnConcurrentlyClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="Phụ cấp" ID="btnAllowance" OnClientClicking="btnAllowanceClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
                <td style="padding-left: 20px;">
                    <tlk:RadButton runat="server" Text="QTLV" ID="btnWorkingProcess" OnClientClicking="btnWorkingProcessClick" AutoPostBack="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1050px"
            Height="300px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false" OnClientClose="OnClientClose">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadScriptBlock runat="server" ID="ScriptBlock">
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $find("<%# RadMultiPage1.ClientID%>")._element.control._element.scrollLeft;
            yPos = $find("<%# RadMultiPage1.ClientID%>")._element.control._element.scrollTop;
        }
        function EndRequestHandler(sender, args) {
            $find("<%# RadMultiPage1.ClientID%>")._element.control._element.scrollLeft = xPos;
            $find("<%# RadMultiPage1.ClientID%>")._element.control._element.scrollTop = yPos;
        }
        function cusPer_District(oSrc, args) {
            var cbo = $find("<%# cboPer_District.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusPer_Province(oSrc, args) {
            var cbo = $find("<%# cboPer_Province.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusNav_Province(oSrc, args) {
            var cbo = $find("<%# cboNav_Province.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusNav_District(oSrc, args) {
            var cbo = $find("<%# cboNav_District.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusInsRegion(oSrc, args) {
            var cbo = $find("<%# cboInsRegion.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusGender(oSrc, args) {
            var cbo = $find("<%# cboGender.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseWindow() {
            debugger;
            var oWindow = GetRadWindow();
            if (oWindow) oWindow.close();
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboBank.ClientID %>':
                    cbo = $find('<%= cboBankBranch.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboPer_Province.ClientID %>':
                    cbo = $find('<%= cboPer_District.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboPer_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboPer_District.ClientID %>':
                    cbo = $find('<%= cboPer_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboNav_Province.ClientID %>':
                    cbo = $find('<%= cboNav_District.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboNav_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cboNav_District.ClientID %>':
                    cbo = $find('<%= cboNav_Ward.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cbPROVINCEEMP_ID.ClientID %>':
                    cbo = $find('<%= cbDISTRICTEMP_ID.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cbWARDEMP_ID.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                case '<%= cbDISTRICTEMP_ID.ClientID %>':
                    cbo = $find('<%= cbWARDEMP_ID.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
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
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboBankBranch.ClientID %>':
                    cbo = $find('<%= cboBank.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboTitle.ClientID %>':
                    value = $get("<%= hidOrgID.ClientID %>").value;
                    break;
                case '<%= cboPer_District.ClientID %>':
                    cbo = $find('<%= cboPer_Province.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cbDISTRICTEMP_ID.ClientID %>':
                    cbo = $find('<%= cbPROVINCEEMP_ID.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cbWARDEMP_ID.ClientID %>':
                    cbo = $find('<%= cbDISTRICTEMP_ID.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboPer_Ward.ClientID %>':
                    cbo = $find('<%= cboPer_District.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboNav_District.ClientID %>':
                    cbo = $find('<%= cboNav_Province.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboNav_Ward.ClientID %>':
                    cbo = $find('<%= cboNav_District.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboNative.ClientID %>':
                    cbo = $find('<%= cboNative.ClientID %>');
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

        function OnClientClose(oWnd, args) {
            var arg = args.get_argument();
            if (arg == '1') {
                location.reload();
            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

        function btnWageClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi làm Hồ sơ lương';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }

            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business&empID=' + empID, "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnContractClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;            
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            var oWindow;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi thêm hợp đồng';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
          
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }

            oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_ContractNewEdit&group=Business&empid=' + empID, "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnContractAppendixClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var contractID = $get("<%= hidContractID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            var oWindow;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi thêm phụ lục hợp đồng';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (!contractID) {
                m = 'Bạn phải thêm Hợp đồng trước khi thêm phụ lục hợp đồng';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }

            oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_ContractTemplete&group=Business&empid=' + empID+ '&Is_dis=dis_emp', "rwPopup");
            //oWindow.setSize(1450, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnBeforeWorkExpClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var workingID = $get("<%= hidWorkingID.ClientID %>").value;
            var contractID = $get("<%= hidContractID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi thêm kinh nghiệm làm việc trước đây';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_WorkInfoNewEdit&group=Business&empID=' + empID+ '&Is_dis=dis_emp', "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnFamilyClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var workingID = $get("<%= hidWorkingID.ClientID %>").value;
            var contractID = $get("<%= hidContractID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi thêm thông tin người thân';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_FamilytNewEdit&group=Business&empID=' + empID + '&Is_dis=dis_emp', "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnTrainingClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var workingID = $get("<%= hidWorkingID.ClientID %>").value;
            var contractID = $get("<%= hidContractID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi thêm Đào tạo';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_CertificateNewEdit&group=Business&empID=' + empID+ '&Is_dis=dis_emp', "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnConcurrentlyClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&empID=' + empID + '&kind=is_new_edit&noscroll=1&FormType=0', "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnAllowanceClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi thêm Đào tạo';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_Allowance&group=Business&empID=' + empID + '&kind=isNewEdit', "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnWorkingProcessClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi thêm Đào tạo';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&empID=' + empID + '&kind=isNewEdit', "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }
        function btnChangeInfoClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var workingID = $get("<%= hidWorkingID.ClientID %>").value;
            var contractID = $get("<%= hidContractID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi làm Thay đổi thông tin nhân sự';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (!contractID) {
                m = 'Bạn phải thêm Hợp đồng trước khi làm Thay đổi thông tin nhân sự';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_ChangeInfoNewEdit&group=Business&empID=' + empID + '&kind=isNewEdit', "rwPopup");
            //oWindow.setSize(1420, $(window).height());
            oWindow.maximize(true);
            oWindow.center();
            oWindow.add_close(OnClientClose);
        }

        function btnTransferTripartiteClick(sender, args) {
            var empID = $get("<%= hidID.ClientID %>").value;
            var workingID = $get("<%= hidWorkingID.ClientID %>").value;
            var contractID = $get("<%= hidContractID.ClientID %>").value;
            var isTer = $get("<%= hidIsTer.ClientID %>").value;
            if (!empID) {
                m = 'Bạn phải thêm nhân viên trước khi làm Điều chuyển 3 bên';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (!contractID) {
                m = 'Bạn phải thêm Hợp đồng trước khi làm Điều chuyển 3 bên';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            if (isTer) {
                m = 'Nhân viên trạng thái nghỉ việc. Không được phép chỉnh sửa thông tin';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                return;
            }
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteNewEdit&group=Business&empID=' + empID + '&parentID=Emp', "rwPopup"); /*
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_TransferTripartiteNewEdit&group=Business&empID=' + empID + '&parentID=Emp', "rwPopup"); /*
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            oWindow.add_close(OnClientClose);
        }


        function clientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }
        function HeightGrid() {
            var itemHeight_01 = $('#table_01').height();
            var itemHeight_02 = $('#table_02').height();
            var availHeight = $(window).height();
            var difference = availHeight - itemHeight_01 - itemHeight_02 - 35 - 20;
            $('#RadPaneData').css('height', difference + 'px');
            //alert(difference);
        }

        function HeightGridClick(sender, args) {
            HeightGrid();
            //  enumerateChildItems(args.get_item());
            //  $('#RadPaneData').css('height', 230 + 'px');
        }
        function txtFirstNameVNOnValueChanged(args) {
            var cbo = $find("<%# txtFirstNameVN.ClientID %>");
            cbo.set_value(toTitleCase(cbo.get_value()));
        }
        function txtLastNameVNOnValueChanged(args) {
            var cbo = $find("<%# txtLastNameVN.ClientID %>");
            cbo.set_value(toTitleCase(cbo.get_value()));
        }
        function toTitleCase(str) {
            return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
        }
        function enableTextbox(checkbox) {
            document.getElementById('<%= txtNoHouseHolds.ClientID %>').disabled = !document.getElementById(checkbox).checked;
            document.getElementById('<%= txtCodeHouseHolds.ClientID %>').disabled = !document.getElementById(checkbox).checked;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function btnCheckEmailClick (sender, args) {
            var bCheck = $find('<%# txtWorkEmail.ClientID %>').get_value();
            var Empcode = $find('<%# txtEmpCODE.ClientID %>').get_value();
            if (bCheck == '') {
                m = '<%# Translate("Chưa nhập Email") %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            $.ajax({
                type: "POST",
                url: '<%= ResolveClientUrl("~/default.aspx/CheckExistsEmail") %>',
                data: '{Email: "' + bCheck + '",Empcode: "' + Empcode + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (response.d == true){
                        var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_EmpMailPopup&group=Business&EM=' + bCheck + '&Empcode=' + Empcode + '&noscroll=1', "rwPopup");
                        var pos = $("html").offset();
						oWindow.moveTo(pos.center);
						oWindow.setSize(600, 450);
						oWindow.center();
					}else{
                        m = '<%# Translate("Bạn có thể sử dụng Email này") %>';
                        n = noty({ text: m, dismissQueue: true, type: 'success' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                        return;
                    }
                }, error: function (response) {
                    console.log('error');
                }, failure: function (response) {

                }
            });

        }
        function xoa_dau(str) {
            str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
            str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
            str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
            str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
            str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
            str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
            str = str.replace(/đ/g, "d");
            str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
            str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
            str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
            str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
            str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
            str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
            str = str.replace(/Đ/g, "D");
            return str;
        }
        function OnTextChanged() {
            var txtFirstNameVN = $find('<%# txtFirstNameVN.ClientID %>');
            var txtLastNameVN = $find('<%# txtLastNameVN.ClientID %>');
            var txtPerson_Inheritance = $find('<%# txtPerson_Inheritance.ClientID %>');
            txtPerson_Inheritance._SetValue("");
            txtPerson_Inheritance._SetValue(xoa_dau(txtFirstNameVN._textBoxElement.value.trim().toUpperCase()) + " " + xoa_dau(txtLastNameVN._textBoxElement.value.trim().toUpperCase()));
        }
        function OpenCuriculumVitae() {
            var empID = $('#<%= hidID.ClientID%>').val();
            if (empID == "") {
                var m = '<%= Translate("Chưa chọn nhân viên") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_EmpCuriculumVitae&group=Business&emp=' + empID, "_self");
        }
    </script>
</tlk:RadScriptBlock>
