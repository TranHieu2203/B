<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_TrainingManageNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_TrainingManageNewEdit" %>
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
                <td class="lb"  style="width: 200px">
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
                    <asp:Label ID="lbStartDate" runat="server" Text="Thời gian bắt đầu"></asp:Label>
                    
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdStartDate" runat="server" AutoPostBack="True">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdStartDate" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập ngày bắt đầu. %>" ToolTip="Bạn phải nhập ngày bắt đầu."> </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CompareStartDate" runat="server" ErrorMessage="Ngày bắt đầu phải lớn hơn ngày kết thúc gần nhất"
                        ToolTip="Ngày bắt đầu phải lớn hơn ngày kết thúc gần nhất">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbExpireDate" runat="server" Text="Thời gian kết thúc"></asp:Label>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdExpireDate"
                        Type="Date" ControlToCompare="rdStartDate" Operator="GreaterThanEqual" ErrorMessage="Ngày kết thúc phải lớn hơn ngày bắt đầu"
                        ToolTip="Ngày kết thúc phải lớn hơn ngày bắt đầu"></asp:CompareValidator>
                </td>
              </tr>
            <tr>
              <td class="lb">
                   <asp:Label ID="lbProgramTraining" runat="server" Text="Chương trình đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtProgramTraining" runat="server" >
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lbContractType" runat="server" Text="Hình thức đào tạo"></asp:Label>
                   
                </td>
                <td>
                    <tlk:RadComboBox ID="cboContractType" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                 
                </td>
                 <td class="lb">
                   <asp:Label ID="lbCertificate" runat="server" Text="Bằng cấp chứng chỉ"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCertificate" runat="server" >
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
              <td class="lb">
                   <asp:Label ID="lbUnit" runat="server" Text="Đơn vị đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUnit" runat="server" >
                    </tlk:RadTextBox>
                </td>
                 <td class="lb">
                   <asp:Label ID="lbCost" runat="server" Text="Chi phí"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtCost" runat="server" >
                    </tlk:RadNumericTextBox>
                </td>
                 <td class="lb">
                   <asp:Label ID="lbResultTraining" runat="server" Text="Kết quả đào tạo"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtResultTraining" runat="server" >
                    </tlk:RadTextBox>
                </td>
            </tr>
             <tr>
               <td class="lb">
                   <asp:Label ID="lbDegreeExpire" runat="server" Text="Ngày cấp văn bằng chứng chỉ"></asp:Label>
                </td>
                <td>
                   <tlk:RadDatePicker ID="rdDegreeExpire" runat="server">
                    </tlk:RadDatePicker>
                </td>
            <td class="lb">
                   <asp:Label ID="lbDegree" runat="server" Text="Thời hạn văn bằng chứng chỉ"></asp:Label>
                </td>
                <td>
                   <tlk:RadDatePicker ID="rdDegree" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRemark" runat="server" Text="Ghi chú"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                </tr>
                <tr>
                 <td class="lb">
                   <asp:Label ID="lbtxtLocation" runat="server" Text="Địa điểm"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtLocation" SkinID="Textbox1023" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript" language="javascript">

//        $(document).ready(function () {
//            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_ContractNewEdit_RadSplitter1');
//        });

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
