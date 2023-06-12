<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TrainingEvaluateNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_TrainingEvaluateNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgCode" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<asp:HiddenField ID="hidWorkStatus" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidPeriod" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarContract" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin đi công tác")%>
                    </b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="Mã nhân viên"></asp:Label>
                    
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbEmployeeName" runat="server" Text="Họ tên nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb" style="width: 200px">
                    <asp:Label ID="lbTITLE" runat="server" Text="Chức danh"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTITLE" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrg_Name" runat="server" Text="Đơn vị"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrg_Name" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbYear" runat="server" Text="Năm"></asp:Label>
                </td>
                <td>
                 <tlk:RadNumericTextBox ID="txtYear" AutoPostBack="true" 
                 SkinID="Number" TabIndex="18" runat="server" MaxLength="4" MinValue="1900" ShowSpinButtons="true">
                 <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" DecimalDigits="1" />
                    </tlk:RadNumericTextBox>
                  <asp:RequiredFieldValidator ID="reqYear" ControlToValidate="txtYear"
                        runat="server" ErrorMessage="Bạn phải nhập năm." ToolTip="Bạn phải nhập năm."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbContractType" runat="server" Text="Kỳ đánh giá"></asp:Label>
                     
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContractType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqContractType" ControlToValidate="cboContractType"
                        runat="server" ErrorMessage="Bạn phải chọn kỳ đánh giá." ToolTip="Bạn phải chọn kỳ đánh giá."> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRank" runat="server" Text="Xếp loại"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRank" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqRank" ControlToValidate="cboRank"
                        runat="server" ErrorMessage="Bạn phải chọn xếp loại." ToolTip="Bạn phải chọn xếp loại."></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbCapacity" runat="server" Text="Năng lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboCapacity" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqCapacity" ControlToValidate="cboCapacity"
                        runat="server" ErrorMessage="Bạn phải chọn năng lực." ToolTip="Bạn phải chọn năng lực."></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbDecisionNo" runat="server" Text="Số quyết định"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtDecisionNo" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqDecisionNo" ControlToValidate="txtDecisionNo"
                        runat="server" ErrorMessage="Bạn phải nhập số quyết định." ToolTip="Bạn phải nhập số quyết định."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbSignDate" runat="server" Text="Ngày ký"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdSignDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqSignDate" ControlToValidate="rdSignDate"
                        runat="server" ErrorMessage="Bạn phải chọn ngày ký." ToolTip="Bạn phải chọn ngày ký."></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEffectDate" ControlToValidate="rdEffectDate"
                        runat="server" ErrorMessage="Bạn phải chọn ngày ký." ToolTip="Bạn phải chọn ngày ký."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbContent" runat="server" Text="Nội dung"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtContent" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqContent" ControlToValidate="txtContent"
                        runat="server" ErrorMessage="Bạn phải nhập nội dung." ToolTip="Bạn phải nhập nội dung."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbtxtLocation" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtLocation" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqLocation" ControlToValidate="txtLocation"
                        runat="server" ErrorMessage="Bạn phải nhập ghi chú." ToolTip="Bạn phải nhập ghi chú."></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ContractNewEdit_RadSplitter1');
        });

        function cusContractType(oSrc, args) {
            var cbo = $find("<%# cboContractType.ClientID%>");
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


        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            //            if (args.get_item().get_commandName() == 'CANCEL') {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>
