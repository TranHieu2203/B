<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_SalaryNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_SalaryNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidEmp" runat="server" />
<asp:HiddenField ID="hidempid1" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidOrg" runat="server" />
<asp:HiddenField ID="hidTitle" runat="server" />
<asp:HiddenField ID="hidStaffRank" runat="server" />
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="HidFrameProductivity" runat="server" />
<asp:HiddenField ID="hidFrameProductivityRank" runat="server" />
<asp:HiddenField ID="hidDM_ID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarMassTransferSalary" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary"
            ValidationGroup="" />
        <table class="table-form" >
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin chung")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeCode" Text="Mã nhân viên"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải chọn Nhân viên " ToolTip="Bạn phải chọn Nhân viên"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbOrgName" Text="Đơn vị"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>

            </tr>
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleName" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            
            <tr>
                <td class="item-head" colspan="6">
                    <%# Translate("Thông tin hệ số năng suất")%>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEffectDate" Text="Ngày hiệu lực"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" AutoPostBack="true" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" runat="server" ControlToValidate="rdEffectDate"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực" ToolTip="Bạn phải nhập ngày hiệu lực"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Bậc năng suất"></asp:Label><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="rtSAL_RANK_ID" Width="130px" AutoPostBack="true">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnFindFrameProductivity" SkinID="ButtonView" CausesValidation="false" />
                </td>
                <td class="lb" >
                    <asp:Label runat="server" ID="Label4" Text="Hệ số năng suất"></asp:Label><span class="lbReq">*</span>
                </td>
                <td >
                    <tlk:RadNumericTextBox ID="rnCOEFFICIENT" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rnCOEFFICIENT"
                        ErrorMessage="Bạn phải nhập ngày hiệu lực" ToolTip="Hệ số năng suất"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="6">
                    <asp:Label ID="lbInfo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmp" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindFrameProductivity" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function clearSelectRadnumeric(cbo) {
            if (cbo) {
                cbo.clear();
            }
        }
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'SAVE') {
                <%-- var objSalBasic = $find('<%= basicSalary.ClientID %>');
               --%>
                var valueSalBasic = 0;
                var valueSalTotal = 0;
                var valueCostSupport = 0;
                var valueHSDieuChinh = 0;
                var valueTGGiuBac = 0;
                var valueLuongDieuTiet = 0;

                if (args.get_item().get_commandName() == "PRINT") {
                    enableAjax = false;
                }
            }
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
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_SalaryNewEdit_txtEmployeeCode' && e.target.id !== 'ctl00_MainContent_ctrlHU_SalaryNewEdit_rtSAL_RANK_ID') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
