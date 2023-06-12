<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Organization.ascx.vb"
    Inherits="Profile.ctrlHU_Organization" %>
<style type="text/css">
    #btnSaveImage {
        display: none;
    }

    .RadUpload .ruFakeInput {
        display: none;
    }

    .RadUpload .ruBrowse {
        width: 120px !important;
        _width: 120px !important;
        width: 120px;
        _width: 120px;
        background-position: 0 -46px !important;
    }

    .hide {
        display: none !important;
    }

    .btnChooseImage {
        margin-left: -5px;
    }

    .ruInputs {
        width: 0px;
        text-align: center;
    }
</style>
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:radpane id="LeftPane" runat="server" minwidth="200" width="400px">
        <tlk:radtreeview id="treeOrgFunction" runat="server" causesvalidation="False" height="93%">
        </tlk:radtreeview>
        <asp:CheckBox ID="chkViewCommitee" runat="server" Text="<%$ Translate:Xem Ủy ban%>"
            AutoPostBack="True" CausesValidation="false"/>
        <asp:CheckBox ID="cbDissolve" runat="server" Text="<%$ Translate: Hiển thị các đơn vị giải thể %>"
            AutoPostBack="True"  CausesValidation="false"/>
    </tlk:radpane>
    <tlk:radsplitbar id="RadSplitBar1" runat="server" collapsemode="Forward">
    </tlk:radsplitbar>
    <tlk:radpane id="MainPane" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidOrgID" runat="server" />
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
                    <tlk:radtextbox id="txtParent_Name" runat="server" readonly="true" width="93%">
                    </tlk:radtextbox>
                    <tlk:RadButton ID="btnParent" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                </tlk:RadButton>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="checkCommittee" />
                    <asp:Label ID="lblCommittee" runat="server" Text="Ủy ban"></asp:Label>
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
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radtextbox id="txtCode" runat="server" skinid="Textbox50">
                    </tlk:radtextbox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="txtCode" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Mã đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Mã đơn vị %>">
                    </asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <asp:Label ID="lbShortName" runat="server" Text="Tên viết tắt"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radtextbox id="txtShortName" runat="server" skinid="Textbox50">
                    </tlk:radtextbox>
                    <asp:RequiredFieldValidator ID="reqShortName" ControlToValidate="txtShortName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên viết tắt %>" ToolTip="<%$ Translate: Bạn phải nhập Tên viết tắt %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbFOUNDATION_DATE" runat="server" Text="Ngày thành lập"></asp:Label>

                </td>
                <td>
                    <tlk:raddatepicker id="rdFOUNDATION_DATE" runat="server">
                    </tlk:raddatepicker>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEndDate" runat="server" Text="Ngày giải thể"></asp:Label>

                </td>
                <td>
                    <tlk:raddatepicker id="rdEndDate" runat="server">
                    </tlk:raddatepicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdEndDate"
                        Type="Date" ControlToCompare="rdFOUNDATION_DATE" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày giải thể phải lớn hơn ngày thành lập %>"
                        ToolTip="<%$ Translate: Ngày giải thể phải lớn hơn ngày thành lập %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label1" Text="Tổng giám đốc"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox runat="server" ID="cboCEO_Org" Width="100%" AutoPostBack="true" NoWrap="false" 
                        CausesValidation="false" Filter="Contains" HighlightTemplatedItems="true">
                        <HeaderTemplate>
                        <table style="width: 1070px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 570px;">
                                    Tên vị trí
                                </td>
                                <td style="width: 250px;">
                                    Master
                                </td>
                                    <td style="width: 250px;">
                                    Interim
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <table style="width: 1070px" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 570px;">
                                    <%# Eval("NAME_VN")%>
                                </td>
                                <td style="width: 250px;">
                                    <%# Eval("MASTER_NAME")%>
                                </td>
                                <td style="width: 250px;">
                                    <%# Eval("INTERIM_NAME")%>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <tlk:RadTextBox runat="server" ID="txtNameCEO_Org" Enabled="false" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="lbHomePhone" Text="Số Điện thoại"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:radtextbox runat="server" id="txtMobliePhone" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>                
                <td class="lb">
                    <asp:Label ID="lbADDRESS" runat="server" Text="Địa chỉ"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="rtADDRESS" runat="server" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <%--<tr>
                
                <td class="lb">
                    <asp:Label ID="Label7" runat="server" Text="Mã thẻ"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCost_center_code" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Mã thẻ không được trùng"
                        ToolTip="Mã thẻ không được trùng">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRegion" runat="server" Text="Vùng BH"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRegion" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqRegion" ControlToValidate="cboRegion"
                        runat="server" ErrorMessage="Vui lòng chọn vùng BH" ToolTip="Vui lòng chọn vùng BH">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbU_insurance" runat="server" Text="Đơn vị đóng bảo hiểm"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="cboU_insurance" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="cboU_insurance"
                        runat="server" ErrorMessage="Bạn phải nhập đơn vị đóng bảo hiểm"
                        ToolTip="Bạn phải nhập đơn vị đóng bảo hiểm">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbNUMBER_BUSINESS" runat="server" Text="Mã số thuế"></asp:Label>
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
                    <asp:Label ID="lbDATE_BUSINESS" runat="server" Text="Ngày cấp giấy phép ĐKKD" Visible="false"></asp:Label>

                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDATE_BUSINESS" runat="server" Visible="false">
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
                    <asp:Label runat="server" ID="Label8" Text="Số Fax"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtFAX">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbRepresentativeName" runat="server" Text="Người đại diện"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRepresentativeName" runat="server" Width="130px" ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindRepresentative" SkinID="ButtonView" runat="server" CausesValidation="false"
                        Width="40px">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <asp:Label runat="server" ID="Label9" Text="SĐT Người đại diện"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtRepresentative_Phone" ReadOnly="True">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbWebsite" runat="server" Text="Website"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtWebsite" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>--%>

            <tr>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Thông tin khác 1"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtInfor1" runat="server" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label3" runat="server" Text="Thông tin khác 2"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtInfor2" runat="server" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label4" runat="server" Text="Thông tin khác 3"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:radtextbox id="txtInfor3" runat="server" width="100%">
                    </tlk:radtextbox>
                </td>
            </tr>
            <%--<tr>
                <td class="lb">
                    <asp:Label ID="Label5" runat="server" Text="Ca"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtInfor4" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="Label6" runat="server" Text="Tổ"></asp:Label>
                    <span class="lbReq"></span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtInfor5" runat="server" Width="100%">
                    </tlk:RadTextBox>
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
            </tr>--%>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbOrg_level" runat="server" Text="Cấp đơn vị"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboOrg_level" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="cboOrg_level"
                        runat="server" ErrorMessage="Bạn phải nhập Cấp đơn vị" ToolTip="Bạn phải nhập Cấp đơn vị">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="ldOrdNo" runat="server" Text="Số thứ tự"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:radnumerictextbox id="rdOrdNo" runat="server" skinid="NUMBER"></tlk:radnumerictextbox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdOrdNo"
                        runat="server" ErrorMessage="Bạn phải nhập Số thứ tự" ToolTip="Bạn phải nhập Số thứ tự">
                    </asp:RequiredFieldValidator>
                </td>
                <%--<td class="lb">
                    <asp:Label ID="lblOrgReg" runat="server" Text="Vùng miền"></asp:Label>
                </td>
                <td>
                    <tlk:radcombobox id="cboOrgReg" runat="server">
                    </tlk:radcombobox>
                </td>--%>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkOrgChart" />
                    <asp:Label ID="lbOrgChart" runat="server" Text="Hiển thị org chart"></asp:Label>
                    <span class="lbReq"></span>
                </td>
            </tr>
            
            <tr>
                <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Tập tin đính kèm"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtUpload" ReadOnly="true" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    </td>
                <td colspan="3">
                    <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" CausesValidation="false"
                        TabIndex="3" Enabled="true" />
                    <tlk:RadButton ID="btnDownload" runat="server" Text="<%$ Translate: Tải tập tin%>"
                        CausesValidation="false" OnClientClicked="rbtClicked" TabIndex="3" EnableViewState="false">
                    </tlk:RadButton>
                </td>
            </tr>
             <tr style="visibility: hidden">
                <td class="lb">
                    <tlk:RadTextBox ID="txtRemindLink" runat="server">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:radpane>
</tlk:radsplitter>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<common:ctrlupload id="ctrlUpload" runat="server" />
<asp:PlaceHolder ID="phFindOrgRP" runat="server"></asp:PlaceHolder>
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
        function validationFailed(sender, args) {
            sender.deleteFileInputAt(0);
            var message = '<%=Translate("Dung lượng ảnh phải < 4MB") %>';
            var notify104126 = noty({ text: message, dismissQueue: true, type: 'error' });
            setTimeout(function () { $.noty.close(notify104126.options.id); }, 2000);
        }
    </script>
</tlk:radscriptblock>
