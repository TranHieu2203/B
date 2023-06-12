<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WorkInfoNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_WorkInfoNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgCode" runat="server" />
<asp:HiddenField ID="hidWorkingID" runat="server" />
<asp:HiddenField ID="hidWorkStatus" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidSign" runat="server" />
<asp:HiddenField ID="hidSign2" runat="server" />
<asp:HiddenField ID="hidPeriod" runat="server" />
<asp:HiddenField ID="Hid_IsEnter" runat="server" />
<asp:HiddenField ID="hidLink" runat="server" />
<style type="text/css">
    div.RadToolBar .rtbUL{
        text-align: right !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
        <tlk:RadToolBar ID="tbarFamily" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form" >
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin chung")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 140px">
                    <asp:Label ID="lbEmployeeCode" runat="server" Text="<%$ Translate: Mã nhân viên %>"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeCode" AutoPostBack="true" runat="server" Width="130px">
                        <ClientEvents OnKeyPress="OnKeyPress" />
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeCode" ControlToValidate="txtEmployeeCode"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhân viên. %>" ToolTip="<%$ Translate: Bạn phải nhập nhân viên. %>"> </asp:RequiredFieldValidator>
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




            <%--============================================--%>
            <tr>
                <td class="item-head" colspan="6">
                    <b>
                        <%# Translate("Thông tin quá trình công tác")%></b>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label runat="server" ID="lbCompanyName" Text="Tên công ty"></asp:Label><span class="lbReq">*</span>
                </td>

                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtCompanyName" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCompanyName" ControlToValidate="txtCompanyName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên công ty %>" ToolTip="<%$ Translate: Bạn phải nhập tên công ty  %>">
                    </asp:RequiredFieldValidator>
                </td>
                

            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <asp:Label runat="server" ID="Label1" Text="Phòng ban"></asp:Label><span class="lbReq">*</span>
                </td>

                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtDEPARTMENT" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDEPARTMENT"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban  %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTitleName" Text="Vị trí công việc"></asp:Label><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtTitleName" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqTitleBefore" ControlToValidate="txtTitleName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Vị trí công việc %>" ToolTip="<%$ Translate: Bạn phải nhập Vị trí công việc  %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbCompanyAddress" Text="Địa chỉ công ty"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtCompanyAddress" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <%--<tr>
                <td class="lb"></td>
                <td>
                    <asp:CheckBox ID="chkHSV" runat="server" Text="HSV/Hoàn Vũ" />
                </td>
                <td class="lb"></td>
                <td>
                    <asp:CheckBox ID="chkIs_Thamnien" runat="server" Text="Tính thâm niên" />
                </td>
            </tr>--%>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbJoinDate" Text="Từ ngày"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdJoinDate" runat="server" AutoPostBack="true">
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy">
                        </DateInput>
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqJoinDate" ControlToValidate="rdJoinDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Từ ngày %>" ToolTip="<%$ Translate: Bạn phải nhập Từ ngày  %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbEndDate" Text="Đến ngày"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEndDate" runat="server" AutoPostBack="true">
                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy">
                        </DateInput>
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqEndDate" ControlToValidate="rdEndDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Đến ngày %>" ToolTip="<%$ Translate: Bạn phải nhập Đến ngày  %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lblThamNien" Text="Thâm niên (tháng)"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtThamNien" MinValue="0" MaxLength="5" Enabled="false">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label2" Text="Năm tương ứng"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtThamNien_Detail" Enabled="false">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lblWork" Text="Công việc chính"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtWork" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbTerReason" Text="Lý do nghỉ việc"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtTerReason" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải xuống%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnView" runat="server" Text="<%$ Translate: Xem ảnh%>"
                        CausesValidation="false" OnClientClicked="ViewImageClick" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

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
        $(document).ready(function () {
            registerOnfocusOut('ctl00_MainContent_ctrlHU_WorkInfoNewEdit_RadSplitter1');
        });
        function postBack(arg) {
            var ajaxManager = $find("<%= AjaxManagerId %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }

        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'SAVE') {
                document.getElementsByClassName("rtbBtn")[0].style.pointerEvents = "none"
            }
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
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
               console.log(document.getElementById("<%= Hid_IsEnter.ClientID %>").value);
            }
        }
        window.addEventListener('keydown', function (e) {
            if (e.keyIdentifier == 'U+000A' || e.keyIdentifier == 'Enter' || e.keyCode == 13) {
                if (e.target.id !== 'ctrlHU_WorkInfoNewEdit_txtEmployeeCode') {
                    e.preventDefault();
                    return false;
                }
            }
        }, true);
        function ViewImageClick(sender, eventArgs) {
            debugger;
            var url = document.getElementById("<%=hidLink.ClientID %>").value;
            var oWnd = $find('rwMainPopup');
            oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=' + url);
            oWnd.show();
        }
        function <%=ClientID%>_OnClientClose(oWnd, args) {
            debugger;
            oWnd = $find('<%=popupId %>');
            oWnd.remove_close(<%=ClientID%>_OnClientClose);
            var arg = args.get_argument();
            if (arg == null) {
                arg = new Object();
                arg.ID = 'Cancel';
            }
            if (arg) {
                var ajaxManager = $find("<%= AjaxManagerId %>");
                ajaxManager.ajaxRequest("<%= ClientID %>_PopupPostback:" + arg.ID);
            }
        }
    </script>
</tlk:RadCodeBlock>
<asp:PlaceHolder ID="ViewImage" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSigner" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindSalary" runat="server"></asp:PlaceHolder>
