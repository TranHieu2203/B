<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_HU.ascx.vb"
    Inherits="Recruitment.ctrlRC_HU" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidIS_EMP" runat="server" />
<style type="text/css">
    div.new table td.rcbInputCell
    {
        padding-left: 0;
        padding-right: 0px;
    }
    div.new table td.rcbInputCell .rcbInput
    {
        height: 20px;
        padding-left: 2px;
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
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin yêu cầu tuyển dụng")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã YCTD")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtcodeYCTD" ReadOnly="true" SkinID="Readonly" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tên YCTD")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtnameYCTD" ReadOnly="true" SkinID="Readonly" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày YCTD")%><span class="lbReq"></span>
                </td>
                <td colspan="2">
                    <tlk:RadDatePicker ID="rdSendDate" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban YCTD")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitle" runat="server" ReadOnly="true" SkinID="Readonly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("Địa điểm làm việc")%>
                </td>
                <td style="display: none">
                    <tlk:RadComboBox ID="cboLocation" runat="server" Enabled="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Người YCTD")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtPerRequest" ReadOnly="true" SkinID="Readonly" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin nhân viên")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Họ & tên nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTenNV" SkinID="Readonly" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName_EMP" runat="server" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindOrg" runat="server" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName_EMP"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập phòng ban %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Chức danh")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboChucDanh" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Quản lý trực tiếp")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPersonRequest" AutoPostBack="true" runat="server" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindEmployee" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPersonRequest"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Quản lý trực tiếp %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Quản lý trực tiếp %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đối tượng công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="rcOBJECT_ATTENDANT" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="rcOBJECT_ATTENDANT"
                        runat="server" ErrorMessage="Bạn phải chọn Đối tượng công" ToolTip="Bạn phải chọn Đối tượng công">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Đối tượng chấm công")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboObject" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="cboObject"
                        runat="server" ErrorMessage="Bạn phải chọn Đối tượng chấm công" ToolTip="Bạn phải chọn Đối tượng chấm công">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng nhân viên")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="rcOBJECT_EMPLOYEE" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rcOBJECT_EMPLOYEE"
                        runat="server" ErrorMessage="Bạn phải chọn Đối tượng nhân viên" ToolTip="Bạn phải chọn Đối tượng nhân viên">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Nơi làm việc")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="rcRegion" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rcRegion"
                        runat="server" ErrorMessage="Bạn phải chọn Nơi làm việc" ToolTip="Bạn phải chọn Nơi làm việc">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbObjectLaborNew" runat="server" Text="Loại hình lao động"></asp:Label><span
                        class="lbReq" runat="server" id="spObjectLaborNew">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboObjectLabor" SkinID="LoadDemand">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqObjectLabor" ControlToValidate="cboObjectLabor"
                        runat="server" ErrorMessage="Bạn phải chọn loại hình lao động" ToolTip="Bạn phải chọn loại hình lao động">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin lương")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalTYPE" CssClass="new" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Biểu thuế")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTaxTable" CssClass="new" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Loại nhân viên")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboEmployeeType" CssClass="new" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lương cơ bản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnProbationSal" SkinID="Readonly" ReadOnly="true" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Lương hiệu quả công việc")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnOtherSalary1" SkinID="Readonly" ReadOnly="true" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("% hưởng lương")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnPercentSalary" SkinID="Readonly" ReadOnly="true" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tổng mức lương")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnOfficialSal" SkinID="Readonly" ReadOnly="true" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <%# Translate("Thông tin hợp đồng")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Loại hợp đồng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContractType" CssClass="new" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Từ ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" SkinID="Readonly" ReadOnly="true" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Đến ngày")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" SkinID="Readonly" ReadOnly="true" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="6">
                    <asp:CheckBox ID="chkSend" runat="server" Text="<%$ Translate: Chuyển thông tin lương và hợp đồng qua hồ sơ nhân viên %>"
                        Checked="false" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="6">
                    <asp:CheckBox ID="chkIsCopyAllowance" runat="server" Text="<%$ Translate: Chuyển thông tin phụ cấp qua hồ sơ nhân viên %>"
                        Checked="false" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

       <%-- function cusRecruitReason(oSrc, args) {
            var cbo = $find("<%# cboRecruitReason.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID %>");
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



        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }


        function clientButtonClicking(sender, args) {
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }


    </script>
</tlk:RadCodeBlock>
