<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_AccidentNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_AccidentNewEdit" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarTerminate" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
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
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmployeeCode" Text="MSNV"></asp:Label> <span style="color: red">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" runat="server" AutoPostBack="true" Width="130px">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="Bạn phải nhập nhân viên." ToolTip="Bạn phải nhập nhân viên."> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEmployeeName" Text="Tên NV"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                    <asp:Label runat="server" ID="Label18" Text="Mã thẻ"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMaThe" runat="server" SkinID="Readonly" ReadOnly="true">
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
                    <asp:Label runat="server" ID="lbOrgName" Text="Đơn vị"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label19" Text="Ngày tai nạn"></asp:Label> <span style="color: red">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="txtAccidentDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAccidentDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày tai nạn. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Ngày tai nạn %>"> </asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <asp:Label runat="server" ID="Label20" Text="Nguyên nhân"></asp:Label>
                    <span style="color: red">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboReasin" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="cboReasin"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nguyên nhân. %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nguyên nhân %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label21" Text="Chi phí điều trị"></asp:Label><span style="color: red">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtCode" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Chi phí điều trị. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Chi phí điều trị %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label22" Text="Thông tin chấn thương"></asp:Label><span style="color: red">*</span>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtInfo" runat="server" Width="100%" SkinID="TextBox1023">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtInfo"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Thông tin chấn thương. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Thông tin chấn thương %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label23" Text="Nơi điều trị"></asp:Label><span style="color: red">*</span>
                </td>
                <td colspan ="5">
                    <tlk:RadTextBox ID="txtTreatment" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtTreatment"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Nơi điều trị. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập Nơi điều trị %>"> </asp:RequiredFieldValidator>
                </td>

            </tr>
            <tr>
                 <td class="lb">
                    <asp:Label runat="server" ID="Label24" Text="Tiền bồi thường"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtMoney" MinValue="0" runat="server" SkinID="Money">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label25" Text="Ngày nhận tiền"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdMoney" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label26" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan ="5">
                    <tlk:RadTextBox ID="RadTextBox1" runat="server" Width="100%" SkinID="TextBox1023">
                    </tlk:RadTextBox>
                </td>
            </tr>

        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
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

        function btnDeleteReasonClick(sender, args) {

        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function btnDeleteDebtsOnClientClicking(sender, args) {

        }
        function btnAddDebtsOnClientClicking(sender, args) {
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnKeyPress(sender, eventArgs) {
            var c = eventArgs.get_keyCode();
            if (c == 13) {
                document.getElementById("<%= Hid_IsEnter.ClientID %>").value = "ISENTER";
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_AccidentNewEdit_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
