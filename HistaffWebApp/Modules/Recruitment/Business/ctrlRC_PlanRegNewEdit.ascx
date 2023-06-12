<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_PlanRegNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_PlanRegNewEdit" %>
<style type="text/css">
    .ages
    {
        width: 39% !important;
        float: left;
    }
    .LevelLanguage
    {
        width: 44% !important;
        float: left;
    }
    .ages span
    {
        width: 100% !important;
    }
    .LevelLanguage div
    {
        width: 100% !important;
    }
</style>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="130px">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập phòng ban %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Vị trí tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitle" AutoPostBack="true" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusTitle" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Vị trí tuyển dụng %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Vị trí tuyển dụng %>" ClientValidationFunction="cusTitle">
                    </asp:CustomValidator>
                </td>
                <td class="lb" style="width: 150px">
                    <%# Translate("Số lượng tuyển")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRecruitNumber" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="10" MinValue="0">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtRecruitNumber"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số lượng tuyển %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Số lượng tuyển %>"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSendDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdSendDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày đi làm dự kiến")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpectedJoinDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdExpectedJoinDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày đi làm dự kiến %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày đi làm dự kiến %>"> 
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpectedJoinDate"
                        ControlToCompare="rdSendDate" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày đi làm dự kiến phải lớn hơn Ngày gửi kế hoạch %>"
                        ToolTip="<%$ Translate: Ngày đi làm dự kiến phải lớn hơn Ngày gửi kế hoạch %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRecruitReason" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số lượng định biên")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtPayrollLimit" ReadOnly="true" runat="server" SkinID="number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng hiện có")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtCurrentNumber" ReadOnly="true" runat="server" SkinID="number">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng tăng giảm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtDifferenceNumber" ReadOnly="true" runat="server" SkinID="number">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Độ tuổi từ")%>
                </td>
                <td style="width: 1px">
                    <div style="margin-right: 7px;" class="ages">
                        <tlk:RadNumericTextBox ID="txtAgesFrom" runat="server" NumberFormat-DecimalDigits="1"
                            NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="18"
                            MaxValue="100">
                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                        </tlk:RadNumericTextBox>
                    </div>
                    <label style="float: left; margin-right: 5px; font-weight: normal">
                        đến</label>
                    <div class="ages">
                        <tlk:RadNumericTextBox ID="txtAgesTo" runat="server" NumberFormat-DecimalDigits="1"
                            NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="18"
                            MaxValue="100">
                            <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                        </tlk:RadNumericTextBox>
                    </div>
                </td>
                <td class="lb">
                    <%# Translate("Trình độ học vấn")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboEducationLevel" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Trình độ tin học")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboComputerLevel" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngoại ngữ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLanguage" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Trình độ ngoại ngữ")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboLanguageLevel" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Điểm")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtScores" runat="server" NumberFormat-DecimalDigits="1"
                        NumberFormat-GroupSeparator="" ShowSpinButtons="true" MaxLength="3" MinValue="0"
                        MaxValue="100">
                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                </td>
                <td colspan="2">
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindEmployee" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <%# Translate("Chọn người được thay thế")%>
                </td>
                <td colspan="2" class="lb">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nghiệp vụ chuyên môn")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboQualification" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Kỹ năng đặc biệt")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSpecialSkills" runat="server" SkinID="number">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                </td>
                <td>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                </td>
                <td colspan="5">
                    <tlk:RadListBox ID="lstEmployee" runat="server" Width="100%" Height="100px">
                    </tlk:RadListBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhiệm vụ chính")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtMainTask" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Yêu cầu kinh nghiệm")%><br>
                    <%# Translate("chuyên môn")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtQualificationRequest" runat="server" TextMode="MultiLine"
                        Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusTitle(oSrc, args) {
            var cbo = $find("<%# cboTitle.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
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
    </script>
</tlk:RadCodeBlock>
