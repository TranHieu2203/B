<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlImageUpload.ascx.vb"
    Inherits="Profile.ctrlImageUpload" %>
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
        margin-left: -12px;
        margin-top: 9px;
    }
    .ruInputs
    {
        width: 0px;
        text-align: center;
    }
</style>
<div class="box_image">
    <div class="image_frame">
        <tlk:RadBinaryImage ID="rbiEmployeeImage" runat="server" AutoAdjustImageControlSize="true"
            Width="90px" Height="120px" ResizeMode="Fill" />
        <asp:HiddenField runat="server" ID="hidEmployeeID" />
    </div>
    <tlk:RadAsyncUpload Width="120px" Height="20px" runat="server" ID="_radAsynceUpload"
        ControlObjectsVisibility="None" OnClientFileUploaded="fileUploaded" OnClientValidationFailed="validationFailed"
        EnableAjaxSkinRendering="true" AllowedFileExtensions="jpeg,jpg,gif,png,bmp" MaxFileSize="4096000"
        CssClass="btnChooseImage" HideFileInput="False" DisablePlugins="True">
        <Localization Select="<%$ Translate: Select image %>" />
    </tlk:RadAsyncUpload>
    <div style="display: none;">
        <asp:Button runat="server" ID="btnSaveImage" CausesValidation="false" />
    </div>
</div>
<tlk:RadScriptBlock ID="rscriptblock" runat="server">
    <script type="text/javascript">
        function changeToDefaultImage(imgEle) {
            imgEle.src = '/images/userGraphic.png';
        }
        function fileUploaded(sender, args) {
            $get('<%= btnSaveImage.ClientID %>').click();
        }
        function validationFailed(sender, args) {
            sender.deleteFileInputAt(0);
            var message = '<%=Translate("Dung lượng ảnh phải <4MB") %>';
            var notify104126 = noty({ text: message, dismissQueue: true, type: 'error' });
            setTimeout(function () { $.noty.close(notify104126.options.id); }, 2000);
        }
    </script>
</tlk:RadScriptBlock>
