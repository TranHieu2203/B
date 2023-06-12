<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_AssetMngNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_AssetMngNewEdit" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidEmployeeID" runat="server" />
<asp:HiddenField ID="hidAssetID" runat="server" />
<style type="text/css">
    .RadButton_Metro.rbSkinnedButton, .RadButton_Metro .rbDecorated
    {       
        height:22px;
    }    
    @media screen and (-webkit-min-device-pixel-ratio:0) {
        .RadButton_Metro.rbSkinnedButton, .RadButton_Metro .rbDecorated
        {       
            height:21px;
        } 
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="TopPane" runat="server" Scrolling="None" Height="35px">
        <tlk:RadToolBar ID="tbarAssetMng" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="LeftPane" runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form"  onkeydown="return (event.keyCode!=13)">
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Mã nhân viên")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeeID" SkinID="ReadOnly" runat="server" Width="130px" 
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnEmployee" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px" TabIndex="6">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEmployeeID" ControlToValidate="txtEmployeeID"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn nhân viên. %>" ToolTip="<%$ Translate: Bạn phải chọn nhân viên. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 180px">
                    <%# Translate("Tên nhân viên")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtEmployeName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Chức danh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtTitleName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Cấp nhân sự")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtStaffRank" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
               
                <td class="lb">
                    <%# Translate("Mã loại tài sản")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtAssetID" runat="server" Width="130px" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnAsset" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px" TabIndex="6">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqAssetID" ControlToValidate="txtAssetID" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn mã loại tài sản. %>" ToolTip="<%$ Translate: Bạn phải chọn mã loại tài sản. %>"> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalAsset" ControlToValidate="txtAssetID" runat="server"
                        ErrorMessage="<%$ Translate: Tài sản không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Tài sản không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
            </tr>

            <tr>
                <td class="lb">
                    <%# Translate("Tên tài sản")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtAssetName" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>

                <td class="lb">
                    <%# Translate("Nhóm tài sản")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtAssGroup" runat="server" ReadOnly="True" SkinID="ReadOnly">
                    </tlk:RadTextBox>
                </td>
            </tr>
              <tr>
                <td class="lb">
                    <%# Translate("Mã vạch tài sản")%>
                     <span class="lbReq">*</span>
                </td>
                <td>
                     <tlk:RadTextBox ID="txtAssetBarcode" runat="server" TabIndex="6">
                    </tlk:RadTextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAssetBarcode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập mã vạch tài sản . %>" ToolTip="<%$ Translate: Bạn phải nhập mã vạch tài sản . %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số serial")%>
                </td>
                <td>
                 <tlk:RadTextBox ID="txtAssetSerial" runat="server" TabIndex="6">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 130px">
                    <%# Translate("Bộ phận bàn giao")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtORG_TRANFER" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnORGTRANSFER" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px" TabIndex="6">
                    </tlk:RadButton>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Bộ phận nhận bàn giao")%>
                </td>
               <td>
                    <tlk:RadTextBox ID="txtORG_RECEIVE" SkinID="ReadOnly" runat="server" Width="130px"
                        ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnORGRECEIVE" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px" TabIndex="6">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Giá trị tài sản")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtAmount" runat="server" SkinID="Money" ValidationGroup="Allowance" TabIndex="6">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày cấp phát")%>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdIssueDate" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                        TabIndex="6">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqIssueDate" ControlToValidate="rdIssueDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày cấp phát. %>" ToolTip="<%$ Translate: Bạn phải nhập ngày cấp phát. %>"> </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tiền đặt cọc")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmDeposits" runat="server" SkinID="Money" ValidationGroup="Allowance" TabIndex="6">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày thu hồi")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdReturnDate" runat="server" DateInput-DateFormat="dd/MM/yyyy"
                        TabIndex="6">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cvalReturnDate_IssueDate" runat="server" ErrorMessage="<%$ Translate: Ngày thu hồi phải lớn hơn ngày cấp phát. %>"
                        ToolTip="<%$ Translate: Ngày thu hồi phải lớn hơn ngày cấp phát. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                 <td class="lb" style="width: 130px">
                    <%# Translate("Trạng thái tài sản")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSTATUS_ID" runat="server" TabIndex="6">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalStatusID" ControlToValidate="cboSTATUS_ID" runat="server" 
                        ErrorMessage="<%$ Translate: Trạng thái tài sản không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Trạng thái tài sản không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                 </td>
                <td>
                 </td>
            </tr>

            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtDesc" runat="server" Width="100%" SkinID="Textbox1023" TabIndex="6">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindAsset" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrgTransfer" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindOrgReceive" runat="server"></asp:PlaceHolder>

<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlHU_AssetMngNewEdit_LeftPane');
        });

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
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
            //            if (args.get_item().get_commandName() == 'CANCEL')
            //            {
            //                getRadWindow().close(null);
            //                args.set_cancel(true);
            //            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
        }

        
    </script>
</tlk:RadCodeBlock>
