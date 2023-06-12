<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_DebtMngNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_DebtMngNewEdit" %>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #RAD_SPLITTER_PANE_CONTENT_RadPaneMain{
        overflow: hidden !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal" Scrolling="None">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarTerminate" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidDecisionID" runat="server" />
        <asp:HiddenField ID="hidEmpID" runat="server" />
        <asp:HiddenField ID="hidTitleID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />        
        <asp:HiddenField ID="hiSalbasic" runat="server" />
        <asp:HiddenField ID="hidWorkStatus" runat="server" />
        <asp:HiddenField ID="hidCheckDelete" runat="server" />
        <asp:HiddenField ID="Hid_IsEnter" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <fieldset >
            <legend>
                <%# Translate("Thông tin chung")%>
            </legend>
            <table class="table-form"  >
                <tr>
                    <td class="lb">
                        <asp:Label runat="server" ID="lbEmployeeCode" Text="MSNV"></asp:Label>
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
                        <asp:Label runat="server" ID="lbEmployeeName" Text="Họ tên nhân viên"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtEmployeeName" runat="server" SkinID="Readonly" ReadOnly="true">
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
                        <asp:Label runat="server" ID="lbOrgName" Text="Phòng ban"></asp:Label>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtOrgName" runat="server" SkinID="Readonly" ReadOnly="true">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <fieldset>
            <legend>
                <%# Translate("Thông tin Công nợ")%>
            </legend>
            <table class="table-form" onkeydown="return (event.keyCode!=13)">
                <tr>
                    <td class="lb">
                        <asp:Label runat="server" ID="Label1" Text="Ngày ghi nợ"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadDatePicker ID="rdDebtDate" runat="server">
                        </tlk:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdDebtDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Ngày ghi nợ %>"
                            ToolTip="<%$ Translate: Bạn phải chọn Ngày ghi nợn %>"> 
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label runat="server" ID="Label2" Text="Mô tả chi tiết"></asp:Label>
                    </td>
                    <td colspan="4">
                        <tlk:RadTextBox ID="txtRemark" runat="server" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label runat="server" ID="Label3" Text="Loại công nợ"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadComboBox ID="cboDebt_Type" runat="server">
                        </tlk:RadComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="cboDebt_Type"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Loại công nợ %>"
                            ToolTip="<%$ Translate: Bạn phải chọn Loại công nợ %>"> 
                        </asp:RequiredFieldValidator>
                    </td>
                    <td class="lb">
                        <asp:Label runat="server" ID="Label4" Text="Số tiền"></asp:Label><span class="lbReq">*</span>
                    </td>
                    <td>
                        <tlk:RadNumericTextBox ID="rntxtDebtMoney" MinValue="0" runat="server" SkinID="Money">
                        </tlk:RadNumericTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rntxtDebtMoney"
                            runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số tiền%>"
                            ToolTip="<%$ Translate: Bạn phải nhập số tiền %>"> 
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label runat="server" ID="Label5" Text="Trừ vào lương"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsDeDuct_Salary" runat="server" AutoPostBack="true" />
                    </td>
                     <td class="lb">
                        <asp:Label runat="server" ID="lbPeriod" Text="Tháng lương"></asp:Label>
                    </td>
                    <td >
                        <tlk:RadTextBox ID="txtPeriod" runat="server"  EmptyMessage="{MM/YYYY}">
                        </tlk:RadTextBox>
                         <asp:CustomValidator ID="cusvalPeriod" ControlToValidate="txtPeriod"
                        runat="server" ErrorMessage="<%$ Translate: Tháng lương không đúng định dạng {MM/YYYY}. %>"
                        ToolTip="<%$ Translate: Tháng lương không đúng định dạng {MM/YYYY}. %>">
                    </asp:CustomValidator>
                    </td>

                    <td class="lb"  style="display: none">
                        <%# Translate("Năm")%>
                    </td>
                    <td  style="display: none">
                        <tlk:RadComboBox ID="cboYear" runat="server"  AutoPostBack="true"
                            Width="80px">
                        </tlk:RadComboBox>
                    </td>
                    <td class="lb"  style="display: none">
                        <%# Translate("Kỳ công")%>
                    </td>
                    <td  style="display: none">
                        <tlk:RadComboBox ID="cboPeriodId" runat="server" AutoPostBack="true"
                            Width="150px">
                        </tlk:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label runat="server" ID="Label6" Text="Đã thanh toán"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsPaid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label runat="server" ID="Label7" Text="Hoàn lại tiền"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsPayBack" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <asp:Label runat="server" ID="lbRemark" Text="Ghi chú"></asp:Label>
                    </td>
                    <td colspan="5">
                        <tlk:RadTextBox ID="txtNote" runat="server" SkinID="TextBox1023" Width="100%">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("Đính kèm tập tin")%>
                    </td>
                    <td>
                        <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                        </tlk:RadTextBox>
                        <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                        </tlk:RadTextBox>
                        <tlk:RadButton runat="server" ID="btnUpload" SkinID="ButtonView" CausesValidation="false"
                            TabIndex="3" />
                        <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                            CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindSign" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

     

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
           if (args.get_item().get_commandName() == 'CANCEL') {
                //getRadWindow().close(0);
            } 
            if (args.get_item().get_commandName() == 'SAVE') {
                //getRadWindow().close(1);
                enableAjax = false;
           } 
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function btnDeleteDebtsOnClientClicking(sender, args) {

        }
      
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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
                if (e.target.id !== 'ctl00_MainContent_ctrlHU_DebtMngNewEdit_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
    </script>
</tlk:RadCodeBlock>
