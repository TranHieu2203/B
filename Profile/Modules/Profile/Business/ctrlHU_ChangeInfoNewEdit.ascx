<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ChangeInfoNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_ChangeInfoNewEdit" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<%@ Import Namespace="Profile" %>
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidStaffRank" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidManager" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<style type="text/css">
    .borderRight
    {
        border-right: 1px solid #C1C1C1;
    }
    
    .RadButton_Metro.rbSkinnedButton, .RadButton_Metro .rbDecorated
    {
        height: 22px;
    }
    
    @media screen and (-webkit-min-device-pixel-ratio:0)
    {
        .RadButton_Metro.rbSkinnedButton, .RadButton_Metro .rbDecorated
        {
            height: 21px;
        }
    }
    /*div.RadToolBar .rtbUL{
        text-align: right !important;
    }*/
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMassTransferSalary" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" >
            <tr>
                <td colspan="8" style="text-align: right">
                    <tlk:RadButton runat="server" Text="Thêm Hồ sơ lương" ID="btnWage" AutoPostBack="false" ForeColor="Green" CausesValidation="false" OnClientClicking="btnWageClick">
                    </tlk:RadButton>    
                     <tlk:RadButton runat="server" Text="Thêm Phụ lục HĐ" ID="btnContractAppendix" OnClientClicking="btnContractAppendixClick" AutoPostBack="false" ForeColor="Green" CausesValidation="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td style="width: 150px"></td>
                <td style="width: 210px"></td>
                <td style="width: 150px"></td>
                <td style="width: 225px"></td>
                <td style="width: 150px"></td>
                <td style="width: 210px"></td>
                <td style="width: 150px"></td>
                <td style="width: 210px"></td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" AutoPostBack="true" Width="182px">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Họ tên")%>
                </td>
                <td style="padding-right: 15px;">
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="item-head">
                    <%# Translate("Thông tin hiện tại")%>
                    <hr />
                </td>
                <td colspan="4" class="item-head">
                    <%# Translate("Thông tin điều chỉnh")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại quyết định")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionTypeOld" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDecisionold" runat="server" Text="Số quyết định"></asp:Label>
                </td>
                <td class="borderRight" style="padding-right: 15px;">
                    <tlk:RadTextBox ID="txtDecisionold" SkinID="Readonly" ReadOnly="true" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDecisionType" runat="server" Text="Loại quyết định"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDecisionType" runat="server" AutoPostBack="true" CausesValidation="false" Width="100%">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusDecisionType" runat="server" ErrorMessage="<%# GetYouMustChoseMsg(UI.DecisionType) %>"
                        ToolTip="<%# GetYouMustChoseMsg(UI.DecisionType) %>" ClientValidationFunction="cusDecisionType"> 
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDecision" runat="server" Text="Số quyết định"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecision" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbEffectDateOld" runat="server" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDateOld" runat="server" SkinID="Readonly" Width="100%">
                        <DateInput Enabled="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDateOld" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td class="borderRight" style="padding-right: 15px;">
                    <tlk:RadDatePicker ID="rdExpireDateOld" runat="server" SkinID="Readonly" Width="100%">
                        <DateInput Enabled="false">
                        </DateInput>
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" AutoPostBack="true" Width="100%">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cusExistEffectDate" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"
                        ToolTip="<%$ Translate: Ngày hiệu lực phải lớn hơn ngày hiệu lực gần nhất %>"> </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDate" runat="server" Text="Ngày hết hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server" Width="100%">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày kết thúc phải lớn hơn ngày hiệu lực %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrgNameOld" runat="server" Text="Đơn vị"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgNameOld" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTitleNameOld" runat="server" Text="Vị trí công việc"></asp:Label>
                </td>
                <td class="borderRight" style="padding-right: 15px;">
                    <tlk:RadTextBox ID="txtTitleNameOld" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOrgName" runat="server" Text="Đơn vị"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" AutoPostBack="true" Width="182px">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbTitle" runat="server" Text="Vị trí công việc"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td class="control3">
                    <tlk:RadComboBox runat="server" ID="cboTitle" OnClientItemsRequesting="OnClientItemsRequesting" Filter="Contains" AutoPostBack="true" CausesValidation="false"
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" HighlightTemplatedItems="true" EnableLoadOnDemand="true" Width="100%">
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
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Nơi làm việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtWorkPlaceOld" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label7" runat="server" Text="Cấp bậc (WL)"></asp:Label>
                </td>
                <td class="borderRight" style="padding-right: 15px;">
                    <tlk:RadTextBox ID="txtJobLevelOld" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Nơi làm việc"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboWorkPlace" Width="100%"></tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboWorkPlace"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nơi làm việc %>" ToolTip="<%$ Translate: Bạn phải nơi làm việc %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label62" Text="Cấp bậc (WL)"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td class="control3">
                    <tlk:RadComboBox runat="server" ID="cboJobLevel" SkinID="LoadDemand" 
                        EnableLoadOnDemand="true" OnClientItemsRequesting="OnClientItemsRequesting" Width="100%">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboJobLevel"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Cấp bậc (WL) %>" ToolTip="<%$ Translate: Bạn phải chọn Cấp bậc (WL) %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label5" runat="server" Text="Thời gian làm việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtObjAttendantOld" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Đối tượng nhân viên"></asp:Label>
                </td>
                <td class="borderRight" style="padding-right: 15px;">
                    <tlk:RadTextBox ID="txtObjEmployeeOld" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label6" runat="server" Text="Thời gian làm việc"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjAttendant" runat="server" Width="100%">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboWorkPlace"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Thời gian làm việc %>" ToolTip="<%$ Translate: Bạn phải Thời gian làm việc %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Đối tượng nhân viên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboObjEmployee" Width="100%"></tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboObjEmployee"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng nhân viên %>" ToolTip="<%$ Translate: Bạn phải chọn Đối tượng nhân viên %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOBJECT_ATTENDANCE_OLD" runat="server" Text="Đối tượng chấm công"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtOBJECT_ATTENDANCE_OLD" runat="server" ReadOnly="true" SkinID="Readonly" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbObjectLaborOld" runat="server" Text="Loại hình lao động"></asp:Label>
                </td>
                <td class="borderRight" style="padding-right: 15px;">
                    <tlk:RadTextBox ID="txtObjectLaborOld" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOBJECT_ATTENDANCE" runat="server"  Text="Đối tượng chấm công"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbOBJECT_ATTENDANCE" runat="server" Width="100%">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="cbOBJECT_ATTENDANCE"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng chấm công %>" ToolTip="<%$ Translate: Bạn phải chọn Đối tượng chấm công %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbObjectLaborNew" runat="server" Text="Loại hình lao động"></asp:Label>
                    <span class="lbReq" runat="server" id="spObjectLaborNew">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboObjectLaborNew" SkinID="LoadDemand" 
                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" OnClientItemsRequesting="OnClientItemsRequesting" Width="100%">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="cusObjectLaborNew" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng lao động %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Đối tượng lao động %>" ClientValidationFunction="cusObjectLaborNew">
                    </asp:CustomValidator>--%>
                     <asp:RequiredFieldValidator ID="reqObjectLabor" ControlToValidate="cboObjectLaborNew"
                    runat="server" ErrorMessage="Bạn phải chọn loại hình lao động" ToolTip="Bạn phải chọn loại hình lao động">
                </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="8">
                    <%# Translate("Thông tin phê duyệt")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbStatus" runat="server" Text="Trạng thái"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server" SkinID="LoadDemand" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged" Width="100%">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="<%$Translate: Bạn phải chọn trạng thái %>"
                        ToolTip="<%$ Translate: Bạn phải chọn trạng thái %>" ClientValidationFunction="cusStatus"> </asp:CustomValidator>
                    <asp:CustomValidator ID="cvalStatus" ControlToValidate="cboStatus" runat="server"
                        ErrorMessage="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng thái không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignDate" runat="server" Text="Ngày ký"></asp:Label>
                </td>
                <td style="padding-right: 15px;">
                    <tlk:RadDatePicker ID="rdSignDate" runat="server" Width="100%">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="7" style="padding-bottom: 8px">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbSignName" runat="server" Text="Người ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSignName" runat="server" SkinID="Readonly" ReadOnly="true" Width="182px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindSigner" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label ID="lbSignTitle" runat="server" Text="Vị trí công việc"></asp:Label>
                </td>
                <td style="padding-right: 15px;">
                    <tlk:RadTextBox ID="txtSignTitle" runat="server" SkinID="Readonly" ReadOnly="true" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server" Width="182px">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <asp:CheckBox ID="chkIsProcess" runat="server" Checked="true" Text="<%$ Translate: Có lưu dữ liệu sang Quá trình công tác %>" />
                </td>
            </tr>

            <!--==================================================================================================-->

            <%--<tr>
                  <td class="lb">
                    <asp:Label runat="server" ID="lbOBJECT_ATTENDANCE_OLD" Text="Đối tượng chấm công"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="rtOBJECT_ATTENDANCE_OLD" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                    <asp:Label runat="server" ID="lbFILING_DATE_OLD" Text="Ngày nộp đơn"></asp:Label>
                </td>
                <td class="borderRight">
                    <tlk:RadDatePicker runat="server" ID="rdFILING_DATE_OLD" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadDatePicker>
                </td>
                 <td class="lb">
                    <asp:Label runat="server" ID="lbOBJECT_ATTENDANCE" Text="Đối tượng chấm công"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cbOBJECT_ATTENDANCE">
                    </tlk:RadComboBox>
                </td>
                  <td class="lb">
                    <asp:Label  runat ="server" ID="lbFILING_DATE" Text ="Ngày nộp đơn"></asp:Label>
                </td>
                  <td>
                    <tlk:RadDatePicker runat="server" ID="rdFILING_DATE">
                    </tlk:RadDatePicker>
                </td>
            </tr>--%>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Chức danh công việc")%>
                </td>
                <td class="borderRight" style="display: none">
                    <tlk:RadTextBox ID="txtTitleGroupOLD" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Chức danh công việc")%>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox ID="txtTitleGroup" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display: none">
                    <%# Translate("Quản lý trực tiếp")%>
                </td>
                <td class="borderRight" style="display: none">
                    <tlk:RadTextBox ID="txtManagerOld" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="display: none">
                    <asp:Label ID="lbManagerNew" runat="server" Text="Quản lý trực tiếp"></asp:Label>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtManagerNew" ReadOnly="true" Width="130px" />
                    <tlk:RadButton runat="server" ID="btnFindDirect" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr style="display:none">
                <td class="lb">
                    <asp:Label ID="lbFileAttach_Link" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbFileAttach" runat="server" Visible="false" />
                    <tlk:RadTextBox ID="txtFileAttach_Link" runat="server" ReadOnly="true" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtFileAttach_Link1" runat="server" ReadOnly="true" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnDownloadOld" runat="server" Width="160px" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtOldClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
                <td></td>
                <td class="borderRight" ></td>
            </tr>
            <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>        
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"  OnClientBeforeClose="OnClientBeforeClose"
            OnClientClose="popupclose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize"
            Modal="true" ShowContentDuringLoad="false">                                    
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        //        $(document).ready(function () {
        //            registerOnfocusOut('RAD_SPLITTER_PANE_TR_ctl00_MainContent_ctrlHU_ChangeInfoNewEdit_LeftPane');
        //        });

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function cusDecisionType(oSrc, args) {
            var cbo = $find("<%# cboDecisionType.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        //function cusObjectLaborNew(oSrc, args) {
        //var cbo = $find("<%# cboObjectLaborNew.ClientID %>");
        // args.IsValid = (cbo.get_value().length != 0);
        // }
        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

       <%-- function cusSalLevel(oSrc, args) {
            var cbo = $find("<%# cboSalLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusSalRank(oSrc, args) {
            var cbo = $find("<%# cboSalRank.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }--%>

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

        function OnDateSelected(sender, e) {
            var datePicker = $find("<%= rdEffectDate.ClientID %>");
            var date = datePicker.get_selectedDate();
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            debugger;
            switch (id) {
                <%--   case '<%= cboSalGroup.ClientID %>':
                    cbo = $find('<%= cboSalLevel.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboSalRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    break;
                   
                case '<%= cboSalLevel.ClientID %>':
                    cbo = $find('<%= cboSalRank.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    break;
                case '<%= cboSalRank.ClientID %>':
                    cbo = $find('<%= rntxtSalBasic.ClientID %>');
                    clearSelectRadnumeric(cbo);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("SALARY_BASIC"));
                    }
                    break;
         --%>
                case '<%= cboTitle.ClientID %>':
                    cbo = $find('<%= txtTitleGroup.ClientID %>');
                    clearSelectRadtextbox(cbo);
                    cbo = $find('<%= cboJobLevel.ClientID %>');
                    clearSelectRadcombo(cbo);
                    var item = eventArgs.get_item();
                    if (item) {
                        cbo.set_value(item.get_attributes().getAttribute("GROUP_NAME"));
                    }
                    break;
                default:
                    break;
            }
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            debugger;
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboJobLevel.ClientID %>':
                    cbo = $find('<%= cboTitle.ClientID %>');
                    value = cbo.get_value();
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
        function clearSelectRadcombo(cbo) {
            debugger;
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function rbtOldClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function clearSelectRadtextbox(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }

        function getGridAllowInfo(gridClientId) {
            var allowanceIns = 0;
            var allowanceTotal = 0;
            var rgAllowItems = $find(gridClientId).get_masterTableView().get_dataItems();
            for (var i = 0; i < rgAllowItems.length; i++) {
                var allowAmount = parseFloat(dataItems[i].getDataKeyValue("AMOUNT"));
                if (dataItems[i].getDataKeyValue("IS_INSURRANCE") === "True") {
                    allowanceIns += allowAmount;
                }
                allowanceTotal += allowAmount;
            }
            return { allowanceIns: allowanceIns, allowanceTotal: allowanceTotal };
        }

        function toggleControls(clientIds, status) {
            $.each(clientIds, function (index, value) {
                toggleControl(value);
            });
        }
        function toggleControl(id, status) {
            var control = $find(id);
            if (control) {
                if (status) {
                    control.enable();
                } else {
                    control.disable();
                }
            }
        }
        

        function clientButtonClicking(sender, args) {
            debugger;
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            } else if(args.get_item().get_commandName() == "SAVE"){
                enableAjax = false;
            }
        }
        function btnWageClick(sender, args) { 
             var oWindow;
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business', "rwPopup");
            oWindow.setSize(1000, 600);
            oWindow.center(); 
            //oWindow.add_close(OnClientClose);
        }

        function btnContractAppendixClick(sender, args) {      
             var oWindow;
            oWindow = radopen('/Dialog.aspx?mid=Profile&fid=ctrlHU_ContractTemplete&group=Business&noscroll=1&add=1', "rwPopup");
            oWindow.setSize(1000, 600);
            oWindow.center(); 
            //oWindow.add_close(OnClientClose);
        }

         function OnClientBeforeClose(sender, eventArgs) {
            if (!confirm("Bạn có muốn đóng màn hình không?")) {
                //if cancel is clicked prevent the window from closing
                args.set_cancel(true);
            }
        }
        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
          
        }
        function OnKeyPress(sender, eventArgs)
        {
            debugger;
           var c = eventArgs.get_keyCode(); 
           if (c == 13) 
           { 
             document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_ChangeInfoNewEdit_txtEmployeeCode' && e.target.id !== 'ctl00_MainContent_ctrlHU_ChangeInfoNewEdit_txtOrgName') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
