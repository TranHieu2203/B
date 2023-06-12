<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CertificateNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_CertificateNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidEmp_ID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidLink" runat="server" />
<style type="text/css">
    div.RadToolBar .rtbUL{
        text-align: right !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContract" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="4">
                    <b>
                        <%# Translate("Thông tin nhân viên")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" AutoPostBack="true" runat="server" Width="133px">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false" Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên. %>">
                    </asp:RequiredFieldValidator>
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
            <tr>
                <td class="item-head" colspan="4">
                    <b>
                        <%# Translate("Thông tin đào tạo")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="<%$ Translate: Loại bằng cấp/Chứng chỉ %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCertificate" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboCertificate" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn Loại bằng cấp/Chứng chỉ. %>" ToolTip="<%$ Translate: Bạn phải chọn Loại bằng cấp/Chứng chỉ. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" id="td_Label_IS_MAIN" runat="server">
                    <asp:Label ID="Label6" runat="server" Text="<%$ Translate: Là bằng chính %>"></asp:Label>
                </td>
                <td id="td_CheckBox_IS_MAIN" runat="server">
                    <asp:CheckBox ID="chkIS_MAIN" runat="server" />
                </td>
            </tr>
            <tr id="tr_CertificateGroup_CertificateType" runat="server">
                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Translate: Nhóm chứng chỉ %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCertificateGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="Required_cboCertificateGroup" ControlToValidate="cboCertificateGroup" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm chứng chỉ. %>" ToolTip="<%$ Translate: Bạn phải chọn Nhóm chứng chỉ. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="<%$ Translate: Loại chứng chỉ %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCertificateType" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="Required_cboCertificateType" ControlToValidate="cboCertificateType" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn Loại chứng chỉ. %>" ToolTip="<%$ Translate: Bạn phải chọn Loại chứng chỉ. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Tên bằng cấp/Chứng chỉ"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtCertificateName" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="CusVal_txtCertificateName" runat="server"  
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên bằng cấp/Chứng chỉ. %>" ToolTip="<%$ Translate: Bạn phải nhập Tên bằng cấp/Chứng chỉ. %>"></asp:CustomValidator>
                </td>
            </tr>
            <tr id="tr_Frommonth_Tomonth" runat="server">
                <td class="lb">
                    <%# Translate("Thời gian đào tạo từ")%>
                </td>
                <td>
                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="rdFROM_DATE" Culture="en-US">
                    </tlk:RadMonthYearPicker>
                </td>
                <td class="lb">
                    <%# Translate("Thời gian đào tạo đến")%>
                </td>
                <td>
                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="rdTO_DATE" Culture="en-US">
                    </tlk:RadMonthYearPicker>
                </td>
            </tr>
            <tr id="tr_Level_SpecializedTrain" runat="server">
                <td class="lb">
                    <asp:Label ID="lbLevel" runat="server" Text="<%$ Translate: Trình độ %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLevel" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="Required_cboLevel" ControlToValidate="cboLevel" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn Trình độ. %>" ToolTip="<%$ Translate: Bạn phải chọn Trình độ. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label5" runat="server" Text="Chuyên ngành"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSpecializedTrain" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="Required_txtSpecializedTrain" ControlToValidate="txtSpecializedTrain" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải nhập Chuyên ngành. %>" ToolTip="<%$ Translate: Bạn phải nhập Chuyên ngành. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="tr_Major1_IsMajor1" runat="server">
                <td class="lb">
                    <asp:Label ID="Label7" runat="server" Text="<%$ Translate: Trình độ chuyên môn %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboMajor1" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="Required_cboMajor1" ControlToValidate="cboMajor1" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn Trình độ chuyên môn. %>" ToolTip="<%$ Translate: Bạn phải chọn Trình độ chuyên môn. %>">
                    </asp:RequiredFieldValidator>
                </td>
<%--                <td class="lb">
                    <asp:Label ID="Label8" runat="server" Text="<%$ Translate: Chuyên môn cao nhất %>"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsMajor1" runat="server" Enabled="false" />
                </td>--%>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbTrainContent" runat="server" Text="Nội dung đào tạo"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtContent" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr id="tr_School1" runat="server">
                <td class="lb">
                    <asp:Label ID="Label9" runat="server" Text="<%$ Translate: Trường đào tạo %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboSchool1" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="Required_cboSchool1" ControlToValidate="cboSchool1" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải chọn Trường đào tạo. %>" ToolTip="<%$ Translate: Bạn phải chọn Trường đào tạo. %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb" id="td_Label_GraduateYear" runat="server">
                    <asp:Label ID="lbYear" runat="server" Text="<%$ Translate: Năm tốt nghiệp %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td id="td_RadNumericTextBox_GraduateYear" runat="server">
                    <tlk:RadNumericTextBox ID="rntGraduateYear" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="4" MinValue="1900"
                        MaxValue="9999" SkinID="Number" CausesValidation="false">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="Required_rntGraduateYear" ControlToValidate="rntGraduateYear" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải nhập Năm tốt nghiệp. %>" ToolTip="<%$ Translate: Bạn phải nhập Năm tốt nghiệp. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbScore" Text="<%$ Translate: Điểm số (thang 10) %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rnScore" NumberFormat-DecimalDigits="1" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr id="tr_Result_TrainingForm" runat="server">
                <td class="lb">
                    <asp:Label ID="lbResult" runat="server" Text="<%$ Translate: Xếp loại tốt nghiệp %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtResult" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="Label10" runat="server" Text="<%$ Translate: Hình thức đào tạo %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTrainingForm" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr id="tr_EffectDate_ExpireDate" runat="server">
                <td class="lb">
                    <asp:Label runat="server" ID="lbFrom" Text="Hiệu lực chứng chỉ từ"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="Required_rdEffectDate" ControlToValidate="rdEffectDate" runat="server" 
                        ErrorMessage="<%$ Translate: Bạn phải nhập Hiệu lực chứng chỉ từ. %>" ToolTip="<%$ Translate: Bạn phải nhập Hiệu lực chứng chỉ từ. %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTo" Text="Hiệu lực chứng chỉ đến"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator id="CompareValidator" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdEffectDate" Operator="GreaterThanEqual" 
                        ErrorMessage="<%$ Translate: Hiệu lực chứng chỉ đến phải lớn hơn Hiệu lực chứng chỉ từ. %>" ToolTip="Hiệu lực chứng chỉ đến phải lớn hơn Hiệu lực chứng chỉ từ.">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <%--<td class="lb">
                    <asp:Label runat="server" ID="Label11" Text="Nơi đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTrainPlace" runat="server">
                    </tlk:RadComboBox>
                </td>--%>
                <td class="lb">
                    <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server" Width="133px">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" EnableViewState="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnView" runat="server" Text="<%$ Translate: Xem ảnh%>"
                        CausesValidation="false" OnClientClicked="ViewImageClick" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="<%$ Translate: Ghi chú %>"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
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
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<common:ctrlupload id="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_CertificateNewEdit_RadSplitter1');
        });

        function ViewImageClick(sender, eventArgs) {
            debugger;
            var url = document.getElementById("<%=hidLink.ClientID %>").value;
            var oWnd = $find('rwMainPopup');
            oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=' + url);
            oWnd.show();
        }

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
            if (args.get_item().get_commandName() == 'SAVE') {
                document.getElementsByClassName("rtbBtn")[0].style.pointerEvents = "none"
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }

        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctrlHU_CertificateNewEdit_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>