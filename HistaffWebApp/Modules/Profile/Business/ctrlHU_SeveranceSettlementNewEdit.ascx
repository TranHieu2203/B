<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_SeveranceSettlementNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_SeveranceSettlementNewEdit" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_SeveranceSettlementNewEdit_RadPane1 {
        height: 35px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarTerminate" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="180px" Scrolling="None">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
        <asp:HiddenField ID="hidOrgAbbr" runat="server" />
        <asp:HiddenField ID="hiSalbasic" runat="server" />
        <asp:HiddenField ID="hidWorkStatus" runat="server" />
        <asp:HiddenField ID="hidCheckDelete" runat="server" />
        <asp:HiddenField ID="Hid_IsEnter" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmployeeCode" Text="MSNV"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbOrgName" Text="Phòng ban"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleName" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>

                <td class="lb">
                    <asp:Label runat="server" ID="lbJoinDateState" Text="Ngày vào làm"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdJoinDateState" runat="server" ReadOnly="true" SkinID="ReadOnly">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSeniority" Text="Thâm niên công tác"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtSeniority" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractNo" Text="Hợp đồng hiện tại"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtContractNo" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractEffectDate" Text="Từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractEffectDate" runat="server" ReadOnly="True" Enabled ="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbContractExpireDate" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdContractExpireDate" runat="server" ReadOnly="True" Enabled ="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Ngày nghỉ việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdTerDate" runat="server" ReadOnly="true" Enabled ="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực QĐ nghỉ việc"> </asp:Label>

                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server" ReadOnly="true" Enabled ="false">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbLastDate" Text="Ngày làm việc cuối cùng"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdLastDate" runat="server" ReadOnly="true" Enabled ="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <%--<tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin khoản trừ")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                 <td class="lb">
                    <asp:Label runat="server" ID="Label3" Text="Loại truy thu BHYT"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTruyThu_BHYT" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="Truy thu BHYT"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtO_HI_EMP" MinValue="0" runat="server" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
             <tr>
                 <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Tiền đồng phục được cấp"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtUniform" runat="server" SkinID="Money" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label8" Text="Tiền đồng phục hoàn lại"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtPaybak_Uniform" runat="server" SkinID="Money" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label11" Text="Bồi hoàn đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtTraining_Costs" MinValue="0" runat="server" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label12" Text="Bồi hoàn vi phạm hợp đồng"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtAmount_Violations" MinValue="0" runat="server" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label13" Text="Khoản trừ khác"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtOther_Compensation" MinValue="0" runat="server" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>--%>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin trợ cấp")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbSalaryMedium_loss" Text="Lương TB 6 tháng liền kề"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtSalaryMedium_loss" runat="server" MinValue="0" AutoPostBack="true">
                        <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
                            GroupSizes="3" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbRemainingLeave" Text="Phép năm còn lại"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtRemainingLeave" runat="server" AutoPostBack="true"
                        NumberFormat-DecimalDigits="1">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPaymentLeave" Text="Tiền phép còn lại"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtPaymentLeave" runat="server" SkinID="Money" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTimeAccidentIns_loss" Text="Thời gian trợ cấp thôi việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTimeAccidentIns_loss" runat="server" AutoPostBack="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbyearforallow_loss" Text="Số năm tính trợ cấp thôi việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtyearforallow_loss" MinValue="0" runat="server" SkinID="Decimal" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                    <asp:Label runat="server" ID="lbAllowanceTerminate" Text="Tiền trợ cấp thôi việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAllowanceTerminate" runat="server" AutoPostBack="true">
                        <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
                            GroupSizes="3" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Thời gian trợ cấp mất việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMonth_Job_loss_Allowance" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="Số năm tính trợ cấp mất việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtYear_Job_loss_Allowance" MinValue="0" runat="server" SkinID="Decimal" AutoPostBack="true">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label14" Text="Tiền trợ cấp mất việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rtxtMoney_Job_loss_allowance" runat="server" AutoPostBack="true">
                        <NumberFormat AllowRounding="false" DecimalDigits="0" DecimalSeparator="." GroupSeparator=","
                            GroupSizes="3" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Tổng tiền quyết toán")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbMoneyReturn" Text="Số tiền quyết toán"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtMoneyReturn" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label15" Text="Trả vào lương"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsReturnInSal" runat="server" AutoPostBack="true" />
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbPeriod" Text="Tháng lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPeriod" runat="server" EmptyMessage="{MM/YYYY}" AutoPostBack="true">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cusvalPeriod" ControlToValidate="txtPeriod"
                        runat="server" ErrorMessage="<%$ Translate: Tháng lương không đúng định dạng {MM/YYYY}. %>"
                        ToolTip="<%$ Translate: Tháng lương không đúng định dạng {MM/YYYY}. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
               <td class="lb" >
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNote" runat="server" Width="100%" SkinID="Textbox1023">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane5" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb" ></td>
                        <td>
                             &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            <tlk:RadButton ID="btnCal" runat="server" Text="<%$ Translate: Tính QT %>">
                            </tlk:RadButton>
                        </td>
                        <td class="lb" ></td>
                        <td>
                            <tlk:RadButton ID="btnSave" runat="server" Text="<%$ Translate: Lưu QT %>">
                            </tlk:RadButton>
                        </td>
                        <td class="lb" ></td>
                        <td>
                            <tlk:RadButton ID="btnDelete" runat="server" Text="<%$ Translate: Xóa QT %>">
                            </tlk:RadButton>
                        </td>
                        <td class="lb" ></td>
                        <td>
                            <tlk:RadButton ID="btnClose" runat="server" Text="<%$ Translate: Đóng %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $find("<%# RadSplitter3.ClientID%>")._element.control._element.scrollLeft;
            yPos = $find("<%# RadSplitter3.ClientID%>")._element.control._element.scrollTop;
        }
        function EndRequestHandler(sender, args) {
            $find("<%# RadSplitter3.ClientID%>")._element.control._element.scrollLeft = xPos;
            $find("<%# RadSplitter3.ClientID%>")._element.control._element.scrollTop = yPos;
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
            if (args.get_item().get_commandName() == "UNLOCK") {
                enableAjax = false;
            }

        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    </script>
</tlk:RadCodeBlock>
