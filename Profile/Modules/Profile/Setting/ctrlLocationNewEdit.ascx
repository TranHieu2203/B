<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLocationNewEdit.ascx.vb"
    Inherits="Profile.ctrlLocationNewEdit" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI" %>
<style>
    #ctrlLocationNewEdit_btnFindOrg {
        margin-left: 4px !important;
    }
        .RadToolBar .rtbOuter {
    padding: 2px;
    border-width: 1px;
    border-style: solid;
    float: right;
}
.RadToolBar_Metro {
    
     line-height: 0px; 
  
}
  .RadToolBar_Metro .rtbWrap {
    padding: 2px 5px;
    padding-top: 0px;
    padding-right: 5px;
    padding-bottom: 2px;
    padding-left: 5px;
    border-width: 1px;
    border-style: solid;
    border-color: transparent;
    *color: #000;
}
</style>
<asp:PlaceHolder ID="phPopupOrg" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupDirect" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phPopupLevel" runat="server"></asp:PlaceHolder>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadToolBar ID="tbarMain" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:HiddenField ID="hfLawAgent" runat="server" />
        <asp:HiddenField ID="hfSignUpAgent" runat="server" />
        <asp:HiddenField ID="hfOrg" runat="server" />
        <asp:HiddenField ID="hfBank" runat="server" />
        <asp:HiddenField ID="hfBank_Branch" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb" style="width: 100px">
                    <%# Translate("Chọn đơn vị")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtLocationVN" Width="93%" ReadOnly="true" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="reqSDTC" ControlToValidate="txtLocationVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn sơ đồ tổ chức. %>" ToolTip="<%$ Translate: Bạn phải chọn sơ đồ tổ chức. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("CTRLLOCATION_LBLLOCATIONCODE")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" SkinID="readonly" ReadOnly="true" ID="txtLocationCODE" />
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("CTRLLOCATION_LBLLOCATIONSHORT")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLocationShort" runat="server" ReadOnly="true" SkinID="ReadOnly" />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 100px">
                    <%# Translate("Tên tiếng Việt")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtNameVn" Width="100%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNameVn" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên tiếng Việt. %>" ToolTip="<%$ Translate: Bạn phải nhập Tên tiếng Việt. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("CTRLLOCATION_LBLLOCATIONEN")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" Width="437px" ID="txtLocationEn" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLADDRESS")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtAddress" Width="100%" />
                    <asp:RequiredFieldValidator ID="reqAddress" ControlToValidate="txtAddress" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Địa chỉ theo GPKD. %>" ToolTip="<%$ Translate: Bạn phải nhập Địa chỉ theo GPKD. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Vùng")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboRegion" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboRegion" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Vùng. %>" ToolTip="<%$ Translate: Bạn phải chọn Vùng. %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 100px">
                    <%# Translate("CTRLLOCATION_LBLPHONE")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtPhone" />
                </td>
            </tr>
            <tr>
                <td class="lb" style="width: 100px">
                    <%# Translate("CTRLLOCATION_LBLADDRESSEMP")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" Width="100%" ID="txtAddress_Emp" />
                </td>
                <td class="lb" style="display: none">
                    <%# Translate("CTRLLOCATION_LBLOFFICEPLACE")%>
                </td>
                <td style="display: none">
                    <tlk:RadTextBox runat="server" ID="txtOfficePlace" />
                </td>

                <td class="lb" style="width: 100px; display: none">
                    <%# Translate("CTRLLOCATION_ISSIGNCONTRACT")%>
                </td>
                <td style="display: none;">
                    <asp:CheckBox runat="server" ID="ckIsSignContract" />
                </td>
                <td class="lb">
                    <%# Translate("Đơn vị đóng bảo hiểm")%><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox runat="server" ID="rcOrgInsurance" Width="438px" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rcOrgInsurance" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị đóng bảo hiểm. %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị đóng bảo hiểm. %>"> </asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_PROVINCE")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" SkinID="LoadDemand" ID="cboProvince" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_DISTRICT")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" SkinID="LoadDemand" ID="cboDistrict" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_WARD")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" SkinID="LoadDemand" ID="cboWard" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        OnClientItemsRequesting="OnClientItemsRequesting" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUploadFile_LG" Text="<%$ Translate: CTRLLOCATION_LBLOGO %>"></asp:Label>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtUpload_LG" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile_LG" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile_LG" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload_LG" runat="server" Text="Tải xuống" CausesValidation="false"
                        OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLACCOUNTNUMBER")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtAccountNumber" CausesValidation="false" />
                    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtAccountNumber" runat="server"
                        ErrorMessage="<%$ Translate: Số tài khoản đã tồn tại %>" ToolTip="<%$ Translate: Số tài khoản đã tồn tại %>"> </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLBANKID")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cbBank" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLBANK_BRANCH_ID")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRank_Banch" runat="server" CausesValidation="false">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUploadFile_HD" Text="<%$ Translate: CTRLLOCATION_LBHD %>"></asp:Label>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtUpload_HD" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile_HD" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile_HD" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload_HD" runat="server" Text="Tải xuống" CausesValidation="false"
                        OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLTAXCODE")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTaxCode" />
                </td>
                <td class="lb">
                    <%# Translate("Lần thay đổi MST thứ ")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtChange_tax_code" />
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLTAXDATE")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpTaxDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="lbUploadFile_FT" Text="<%$ Translate: CTRLLOCATION_LBFT %>"></asp:Label>
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="txtUpload_FT" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile_FT" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <tlk:RadButton runat="server" ID="btnUploadFile_FT" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" />
                    <tlk:RadButton ID="btnDownload_FT" runat="server" Text="Tải xuống" CausesValidation="false"
                        OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLLAWAGENT")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtLawAgentId" ReadOnly="true" Width="132px" />
                    <tlk:RadButton runat="server" ID="btnLawAgentId" SkinID="ButtonView" CausesValidation="false" />
                </td>
                <td class="lb">
                    <%--<%# Translate("CTRLLOCATION_LBLLAWAGENTTITLE")%>--%>
                    <asp:Label runat="server" ID="Label1" Text="<%$ Translate: Vị trí công việc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtLawAgentTitle" ReadOnly="true" />
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLLAWAGENTNATIONALITY")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtLawAgentNationality" ReadOnly="true" />
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLTAXPLACE")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtTaxPlace" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLSIGNUPAGENT")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtSignupAgent" ReadOnly="true" Width="132px" />
                    <tlk:RadButton runat="server" ID="btnSignupAgent" SkinID="ButtonView" CausesValidation="false" />
                </td>
                <td class="lb">
                    <%--<%# Translate("CTRLLOCATION_LBLSIGNUPAGENTTITLE")%>--%>
                    <asp:Label runat="server" ID="Label2" Text="<%$ Translate: Vị trí công việc %>"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtSignupAgentTitle" ReadOnly="true" />
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLSIGNUPAGENTNATIONALITY")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" SkinID="readonly" ID="txtSigupAgentNationality" ReadOnly="true" />
                </td>

            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLNUMBERBUSINESS")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtNumberBusiness" />
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLREGISTERDATE")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdRegisterDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb" style="visibility: hidden; display: none">
                    <%# Translate("CTRLLOCATION_LBLNAMEBUSINESS")%>
                </td>
                <td style="visibility: hidden; display: none">
                    <tlk:RadTextBox runat="server" ID="txtNameBusiness" />
                </td>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLWEBSITE")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtWebsite" />
                </td>

                <td class="lb" style="width: 100px">
                    <%# Translate("CTRLLOCATION_LBLFAX")%>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFax" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("CTRLLOCATION_LBLNOTE")%>
                </td>
                <td colspan="7">
                    <tlk:RadTextBox runat="server" ID="txtNote" Width="100%" TextMode="MultiLine" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindEmployee_Contract" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="FindOrganization" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
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

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
            switch (id) {
                case '<%= cboDistrict.ClientID %>':
                    cbo = $find('<%= cboProvince.ClientID %>');
                    value = cbo.get_value();
                    break;
                case '<%= cboWard.ClientID %>':
                    cbo = $find('<%= cboDistrict.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }
            if (!value) {
                value = 0;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
            context["value"] = sender.get_value();
        }

        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {
                case '<%= cboProvince.ClientID %>':
                    cbo = $find('<%= cboDistrict.ClientID %>');
                    clearSelectRadcombo(cbo);
                    cbo = $find('<%= cboWard.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }

    </script>
</tlk:RadCodeBlock>
