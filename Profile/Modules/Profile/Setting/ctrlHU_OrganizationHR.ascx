<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_OrganizationHR.ascx.vb"
    Inherits="Profile.ctrlHU_OrganizationHR" %>
<style type="text/css">
    #btnSaveImage
    {
        display: none;
    }
    
    .RadUpload .ruFakeInput
    {
        display: none;
    }
    
    .RadUpload .ruBrowse
    {
        width: 120px !important;
        _width: 120px !important;
        width: 120px;
        _width: 120px;
        background-position: 0 -46px !important;
    }
    
    .hide
    {
        display: none !important;
    }
    
    .btnChooseImage
    {
        margin-left: -5px;
    }
    
    .ruInputs
    {
        width: 0px;
        text-align: center;
    }
</style>
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:radpane id="LeftPane" runat="server" minwidth="200" width="400px">
        <tlk:radtreeview id="treeOrgFunction" runat="server" causesvalidation="False" height="93%">
        </tlk:radtreeview>
        <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị giải thể %>"
            AutoPostBack="True" />
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="MainPane" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidParentID" runat="server" />
        <asp:HiddenField ID="hidRepresentative" runat="server" />
        <tlk:radtoolbar id="tbarOrgFunctions" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                  <asp:Label ID="lbParent_Name" runat="server" Text="Đơn vị cha"></asp:Label>             
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtParent_Name" runat="server" readonly="true" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                   <asp:Label ID="lbNameVN" runat="server" Text="Tên đơn vị (VN)"></asp:Label>
                   <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtNameVN" runat="server" width="100%">
                    </tlk:radtextbox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="Bạn phải nhập Tên đơn vị" ToolTip="Bạn phải nhập Tên đơn vị"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbNameEN" runat="server" Text="Tên đơn vị (EN)"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtNameEN" runat="server" width="100%">
                    </tlk:radtextbox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Tên đơn vị %>"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                
                <td class="lb" style = "display : none">
                   <asp:Label ID="lbUNIT_LEVEL" runat="server" Text="Bậc đơn vị"></asp:Label>
                </td>
                <td colspan ="2" style = "display : none">
                    <tlk:RadComboBox  id="cbUNIT_LEVEL" runat="server" width="100%">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCode" runat="server" Text="Mã đơn vị"></asp:Label>
                   <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:radtextbox id="txtCode" runat="server" skinid="Textbox50">
                    </tlk:radtextbox>
                   <%-- <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Mã đơn vị %>">
                    </asp:RequiredFieldValidator>--%>
                </td>

                <td class="lb">
                    <asp:Label ID="Label1" runat="server" Text="Tên viết tắt"></asp:Label>
                   <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:radtextbox id="txtShortName" runat="server" skinid="Textbox50">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                 <td class="lb">
                   <asp:Label ID="lbFOUNDATION_DATE" runat="server" Text="Ngày thành lập"></asp:Label>
                  
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFOUNDATION_DATE" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                   <asp:Label ID="lbRepresentativeName" runat="server" Text="Quản lý đơn vị"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:radtextbox id="txtRepresentativeName" runat="server" width="130px" readonly="True">
                    </tlk:radtextbox>
                     <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtRepresentativeName" runat="server"
                        ErrorMessage="Bạn phải chọn Quản lý đơn vị" ToolTip="Bạn phải chọn Quản lý đơn vị"></asp:RequiredFieldValidator>--%>
                    <tlk:radbutton id="btnFindRepresentative" skinid="ButtonView" runat="server" causesvalidation="false"
                        width="40px">
                    </tlk:radbutton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrg_level" runat="server" Text="Cấp phòng ban"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td >
                    <tlk:radcombobox id="cboOrg_level" runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboOrg_level"
                        runat="server" ErrorMessage="Bạn phải nhập cấp phòng ban" ToolTip="Bạn phải nhập cấp phòng ban">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label10" runat="server" Text="Mã chi phí"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCost_center_code" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRegion" runat="server" Text="Vùng lương"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radcombobox id="cboRegion" runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="reqRegion" ControlToValidate="cboRegion"
                        runat="server" ErrorMessage="Vui lòng chọn vùng lương" ToolTip="Vui lòng chọn vùng lương">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Mã chi phí không được trùng"
                        ToolTip="Mã chi phí không được trùng">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbU_insurance" runat="server" Text="Đơn vị đóng bảo hiểm"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:radcombobox id="cboU_insurance" runat="server">
                    </tlk:radcombobox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboU_insurance"
                        runat="server" ErrorMessage="Bạn phải nhập đơn vị đóng bảo hiểm"
                        ToolTip="Bạn phải nhập đơn vị đóng bảo hiểm">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
           <tr>
                <td class="lb">
                    <asp:Label ID="Label7" runat="server" Text="Mã số thuế"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtNUMBER_BUSINESS" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbWorkEmail" Text="Email"></asp:Label>
                </td>
                <td class="control3">
                    <tlk:RadTextBox runat="server" ID="txtEmail">
                    </tlk:RadTextBox>
                    <asp:RegularExpressionValidator ValidationGroup="EmpProfile" ID="regEMAIL" ControlToValidate="txtEmail"
                        ValidationExpression="^([a-zA-Z0-9_\.-]+)@([a-zA-Z0-9_\.-]+)\.([a-zA-Z\.]{2,6})$"
                        runat="server" ErrorMessage="<%$ Translate: Định dạng Email công ty không chính xác %>"
                        ToolTip="<%$ Translate: Định dạng Email công ty không chính xác %>"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label8" runat="server" Text="Ngày cấp giấy phép ĐKKD" Visible="false"></asp:Label>

                </td>
                <td>
                    <tlk:RadDatePicker ID="RadDatePicker1" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbHomePhone" Text="Số Điện thoại"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtMobliePhone">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="Số Fax"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFAX">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                   <asp:Label ID="lbNUMBER_BUSINESS" runat="server" Text="Giấy phép ĐKKD (Mã số thuế)" Visible="false"></asp:Label>
                </td>
                <td>
      
                </td>
                <td class="lb">
                  <asp:Label ID="lbDATE_BUSINESS" runat="server" Text="Ngày cấp giấy phép ĐKKD" Visible="false"></asp:Label>
                   
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDATE_BUSINESS" runat="server" Visible="false">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbADDRESS" runat="server" Text="Địa chỉ"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="rtADDRESS" runat="server"  width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>

            
            <tr>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Thông tin khác 1"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtInfor1" runat="server"  width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Thông tin khác 2"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtInfor2" runat="server"  width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Thông tin khác 3"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtInfor3" runat="server"  width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label5" runat="server" Text="Thông tin khác 4"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtInfor4" runat="server"  width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label6" runat="server" Text="Thông tin khác 5"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtInfor5" runat="server"  width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>


            <tr style = "display : none">
                <td class="lb">
                    <asp:Label ID="lbLocationWork" runat="server" Text="Địa điểm làm việc"></asp:Label>
                  <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtLocationWork" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr  style = "display : none">
                <td class="lb">
                  <asp:Label ID="lbTypeDecision" runat="server" Text="Loại quyết định"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtTypeDecision" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr  style = "display : none">
                <td class="lb">
                 <asp:Label ID="lbNumberDecision" runat="server" Text="Số quyết định"></asp:Label>
                 <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNumberDecision" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                   <asp:Label ID="lbEffectDate" runat="server" Text="Ngày hiệu lực của quyết định"></asp:Label>
                  
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            
            <tr>
                <td class="lb">
                  <asp:Label ID="lbREMARK" runat="server" Text="Ghi chú"></asp:Label>
                   <span class="lbReq"></span>
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="txtREMARK" runat="server" SkinID="Textbox9999" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                
                <td class="lb">
                   <asp:Label ID="ldOrdNo" runat="server" Text="Số thứ tự"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rdOrdNo" runat="server" SkinID="NUMBER"></tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <asp:Label ID="lblOrgReg" runat="server" Text="Vùng miền"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboOrgReg" runat="server">
                    </tlk:radcombobox>
                </td>
                <td class="lb"  style = "display : none">
                    <asp:Label ID="lbDicision_Date" runat="server" Text="Ngày giải thể"></asp:Label>
                </td>
                <td  style = "display : none">
                    <tlk:RadDatePicker ID="rdDicision_Date" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkOrgChart" />
                  <asp:Label ID="lbOrgChart" runat="server" Text="Hiển thị org chart"></asp:Label>
                  <span class="lbReq"></span>
                 <%--   <tlk:RadButton CausesValidation="false" Text="Hiển thị org chart" ToggleType="CheckBox"
                        runat="server" ID="chkOrgChart" ButtonType="ToggleButton">
                    </tlk:RadButton>--%>
                </td>
                <td  style = "display : none">
                    <asp:Label ID="lbFile" runat="server" Text="Tập tin đính kèm"></asp:Label>
                    <span class="lbReq"></span>
                </td>
               
                <td colspan="2"  style = "display : none">
                    <tlk:RadListBox ID="lstFile" runat="server" CheckBoxes="true" Height="100px" OnClientSelectedIndexChanging="OnClientItemSelectedIndexChanging"
                        Width="80%" />
                </td>
                 <td  style = "display : none">
                    <tlk:RadButton ID="btnUploadFile" runat="server" Text="<%$ Translate: Upload %>"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnDownloadFile" runat="server" Text="<%$ Translate: Tải %>"
                        CausesValidation="false" OnClientClicked="rbtClicked">
                    </tlk:RadButton>
                   
                </td>
            </tr>
        </table>
        <div style="display: none;">
            <asp:Button runat="server" ID="btnSaveImage" CausesValidation="false" />
            <asp:Button runat="server" ID="btnSaveFile" CausesValidation="false" />
        </div>
    </tlk:radpane>
</tlk:radsplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:radscriptblock id="rscriptblock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function btnDownFile_Clicking(sender, args) {
            enableAjax = false;
        }
        function fileUploaded(sender, args) {
            $get('<%= btnSaveImage.ClientID %>').click();
        }
        function fileUploaded1(sender, args) {
            $get('<%= btnSaveFile.ClientID %>').click();
        }
        function validationFailed(sender, args) {
            sender.deleteFileInputAt(0);
            var message = '<%=Translate("Dung lượng ảnh phải < 4MB") %>';
            var notify104126 = noty({ text: message, dismissQueue: true, type: 'error' });
            setTimeout(function () { $.noty.close(notify104126.options.id); }, 2000);
        }
    </script>
</tlk:radscriptblock>
