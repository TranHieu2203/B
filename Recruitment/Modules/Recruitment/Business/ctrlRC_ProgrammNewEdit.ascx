<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgrammNewEdit.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgrammNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidEmpID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã YCTD")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtcodeYCTD" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Tên YCTD")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtnameYCTD" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="100%" BorderStyle="None"
                        Font-Bold="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Vị trí tuyển dụng")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitle" runat="server" ReadOnly="True" BorderStyle="None" Font-Bold="True">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="display:none">
                    <%# Translate("Địa điểm làm việc")%>
                </td>
                <td style="display:none">
                    <tlk:RadComboBox ID="cboLocation" runat="server" Enabled="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày gửi yêu cầu")%><span class="lbReq">*</span>
                </td>
                <td colspan="2">
                    <tlk:RadDatePicker ID="rdSendDate" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdSendDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày gửi kế hoạch %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cần đáp ứng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdRespone" runat="server" Enabled="false">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdRespone"
                        runat="server" ErrorMessage="<%$ Translate: Ngày cần đáp ứng %>" ToolTip="<%$ Translate:Ngày cần đáp ứng %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="display:none">
                </td>
                <td style="display:none">
                    <asp:CheckBox ID="chkTNGSupport" runat="server" Enabled="false" Text="<%$ Translate: TNG hỗ trợ triển khai %>"
                        Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hình thức tuyển dụng")%>
                </td>
                <td colspan="2">
                    <tlk:RadComboBox ID="cboTypeContract" runat="server" Enabled="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Lý do tuyển dụng")%><span class="lbReq">*</span>
                </td>
                <td colspan="2">
                    <tlk:RadComboBox ID="cboRecruitReason" runat="server" Enabled="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusRecruitReason" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Lý do tuyển dụng%>"
                        ToolTip="<%$ Translate: Bạn phải chọn Lý do tuyển dụng %>" ClientValidationFunction="cusRecruitReason">
                    </asp:CustomValidator>
                </td>
                <td class="lb" style="display:none">
                    <%# Translate("Tính chất tuyển dụng")%>
                </td>
                <td style="display:none">
                    <tlk:RadComboBox ID="cboTCRecruit" runat="server" Enabled="false">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                
                <td class="lb">
                    <%# Translate("Số lượng cần tuyển")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRecruitNumber" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                </td>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkVuotDB" runat="server" Enabled="false" Text="<%$ Translate: Vượt định biên %>"
                        Font-Bold="true" />
                </td>
            </tr>
            <tr style="display:none">
                <td class="lb">
                    <%# Translate("Số lượng hiện có")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtCountNow" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng định biên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCountDB" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Số lượng còn lại")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCountTG" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Người yêu cầu")%>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtPerRequest" Enabled="false" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Diễn giải chi tiết")%>
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="txtRecruitReason" runat="server" ReadOnly="true" SkinID="Textbox1023"
                        Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Hồ sơ đã nhận")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtprofileReceive" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td></td>
                <td class="lb">
                    <%# Translate("Số lượng đã tuyển được")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCountRecruited" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="display:none">
                    <%# Translate("Mã tuyển dụng")%>
                </td>
                <td style="display:none" >
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Trạng thái")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboStatus" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusStatus" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn trạng thái%>"
                        ToolTip="<%$ Translate: Bạn phải chọn trạng thái %>" ClientValidationFunction="cusStatus">
                    </asp:CustomValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function cusRecruitReason(oSrc, args) {
            var cbo = $find("<%# cboRecruitReason.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function cusStatus(oSrc, args) {
            var cbo = $find("<%# cboStatus.ClientID %>");
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
