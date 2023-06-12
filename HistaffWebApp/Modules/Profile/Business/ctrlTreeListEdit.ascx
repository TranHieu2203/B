<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlTreeListEdit.ascx.vb"
    Inherits="Profile.ctrlTreeListEdit" %>
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
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="leftpane" runat="server" MinWidth="200" Width="400px" Visible="false">
        <tlk:RadTreeView ID="treeorgfunction" runat="server" CausesValidation="false" Height="93%">
        </tlk:RadTreeView>
        <asp:CheckBox ID="cbdissolve" runat="server" Text="<%$ translate: hiển thị các đơn vị giải thể %>"
            AutoPostBack="true" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="radsplitbar1" runat="server" CollapseMode="forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server">
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidParentID" runat="server" />
        <asp:HiddenField ID="hidRepresentative" runat="server" />
        <tlk:RadToolBar ID="tbarOrgFunctions" runat="server" />
        <table class="table-form">
            <tr>
                <td style="text-align: right !important">
                    <%# Translate("Tên tiếng Việt")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="txtNameVN" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Tên đơn vị %>" ToolTip="<%$ Translate: Bạn phải nhập Tên tiếng Việt %>"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <%# Translate("Tên tiếng Anh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameEN" runat="server" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên viết tắt")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server" SkinID="Textbox50">
                    </tlk:RadTextBox>
                </td>
                <td class="lb3">
                    <asp:Label runat="server" ID="lbTitle" Text="Giám đốc bộ phận"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td class="control3">
                    <tlk:RadComboBox runat="server" ID="cboTitle" AutoPostBack="true"
                        OnClientItemsRequesting="OnClientItemsRequesting" Filter="Contains"
                        HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true">
                        <HeaderTemplate>
                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 280px;">Tên vị trí
                                    </td>
                                    <td style="width: 125px;">Nhân viên
                                    </td>
                                   <%-- <td style="width: 125px;">Interim
                                    </td>--%>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 280px;">
                                        <%# Eval("NAME")%>
                                    </td>
                                    <td style="width: 125px;">
                                        <%# Eval("MASTER_NAME")%>
                                    </td>
                                    <%--<td style="width: 125px;">
                                        <%# Eval("INTERIM_NAME")%>
                                    </td>--%>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ValidationGroup="EmpProfile" ID="cusTitle" runat="server" ErrorMessage="Bạn phải chọn Chức danh công việc TT"
                                                ToolTip="Bạn phải chọn Chức danh công việc TT" ClientValidationFunction="cusTitle">
                                            </asp:CustomValidator>--%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtGdBp" runat="server" SkinID="Textbox51">
                    </tlk:RadTextBox>
                </td>
               
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Cấp tổ chức")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCap" runat="server" SkinID="Textbox51">
                    </tlk:RadTextBox>
                </td>
                <td>
                    <asp:CheckBox ID="ckbnhom" runat="server" Text="<%$ Translate: Hội đồng? %>" />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mã chi phí")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCostCenterCode" runat="server">
                    </tlk:RadTextBox>
                   <%-- <asp:RequiredFieldValidator ID="reqCostCenterCode" ControlToValidate="txtCostCenterCode"
                        runat="server" ErrorMessage="<%$ Translate: Mã chi phí không được trống %>" ToolTip="<%$ Translate: Mã chi phí không được trống %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCostCenterCode" runat="server" ErrorMessage="<%$ Translate: Mã chi phí không được trùng %>"
                        ToolTip="<%$ Translate: Mã chi phí không được trùng %>">
                    </asp:CustomValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày thành lập")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdFoundationDate" runat="server">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    <%# Translate("Ngày giải thể")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDissolveDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CustomValidator ID="cval_FoundDate_DissDate" runat="server" ErrorMessage="<%$ Translate: Ngày giải thể phải lớn hơn hoặc bằng ngày thành lập. %>"
                        ToolTip="<%$ Translate: Ngày giải thể phải lớn hơn hoặc bằng ngày thành lập. %>">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số điện thoại")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtMobile" runat="server" MaxLength="12">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tỉnh thành")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtProvinceName" runat="server" MaxLength="100">
                    </tlk:RadTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Mã số thuế")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtPIT_NO" runat="server" MaxLength="30">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <%-- <td class="lb">
                    <%# Translate("Thứ tự sắp xếp")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rntxtOrdNo" runat="server">
                    </tlk:RadNumericTextBox>
                </td>--%>
            </tr>
            <tr>
                <%--<td class="lb">
                    <%# Translate("Số giấy phép kinh doanh")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNumber_business" runat="server">
                    </tlk:RadTextBox>
                </td>--%>
                <td class="lb">
                    <%# Translate("Ngày cấp giấy phép kinh doanh")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdDate_Business" runat="server">
                    </tlk:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Địa chỉ")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtAddress" runat="server" SkinID="Textbox1023" Width="100%" MaxLength="100">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr runat="server" id="tr01" visible="false">
                <td class="lb">
                    <%# Translate("Giấy phép đăng ký kinh doanh")%>
                </td>
                <td>
                    <tlk:RadAsyncUpload Width="120px" Height="20px" runat="server" ID="_radAsynceUpload1"
                        ControlObjectsVisibility="None" OnClientFileUploaded="fileUploaded1" OnClientValidationFailed="validationFailed"
                        EnableAjaxSkinRendering="true" MaxFileSize="4096000" CssClass="btnChooseImage"
                        HideFileInput="False" DisablePlugins="True" MaxFileInputsCount="1" MultipleFileSelection="Disabled">
                        <Localization Select="<%$ Translate: Select file %>" />
                    </tlk:RadAsyncUpload>
                </td>
                <td>
                    <tlk:RadButton ID="btnDownFile" runat="server" Visible="false" Text="<%$ Translate: Tải file %>"
                        AutoPostBack="true" CausesValidation="false" OnClientClicking="btnDownFile_Clicking"
                        Style="margin-top: 1px !important;">
                    </tlk:RadButton>
                </td>
                <td class="lb" style="display: none;">
                    <%# Translate("Logo (90x120)")%>
                </td>
                <td>
                    <tlk:RadAsyncUpload Width="120px" Height="20px" runat="server" ID="_radAsynceUpload"
                        ControlObjectsVisibility="None" OnClientFileUploaded="fileUploaded" OnClientValidationFailed="validationFailed"
                        EnableAjaxSkinRendering="true" AllowedFileExtensions="jpeg,jpg,gif,png,bmp" MaxFileSize="4096000"
                        CssClass="btnChooseImage" HideFileInput="False" DisablePlugins="True" MaxFileInputsCount="1"
                        MultipleFileSelection="Disabled" Visible="false">
                        <Localization Select="<%$ Translate: Select logo %>" />
                    </tlk:RadAsyncUpload>
                </td>
            </tr>
            <tr runat="server" id="tr02" visible="false">
                <td></td>
                <td>
                    <tlk:RadBinaryImage ID="rbiEmployeeImage" runat="server" AutoAdjustImageControlSize="true"
                        ResizeMode="Fill" Width="90px" Height="120px" />
                    <asp:HiddenField runat="server" ID="hidEmployeeID" />
                </td>
            </tr>
            <tr>
                <td class="lb3">
                    <%# Translate("Trạng thái")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="cboStatus">
                    </tlk:RadComboBox>
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
                </td>
            </tr>
        </table>
        <div style="display: none;">
            <asp:Button runat="server" ID="btnSaveImage" CausesValidation="false" />
            <asp:Button runat="server" ID="btnSaveFile" CausesValidation="false" />
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="rscriptblock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
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
        function OnKeyPress(sender, eventArgs) {
            var e = eventArgs.get_domEvent();
            if (e.charCode == 13) {
                e.charCode = 9;
                $('#ctl00_MainContent_ctrlHU_Organization_txtParent_Name').focus();
            }
        }
        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;
        }
    </script>
</tlk:RadScriptBlock>
