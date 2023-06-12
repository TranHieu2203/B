<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUpload.ascx.vb"
    Inherits="Common.ctrlUpload" %>
<style type="text/css">
    .RadUpload input.ruFakeInput
    {
        width: 160px;
    }
</style>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="Close" VisibleStatusbar="false"
    Height="300" Width="400" Modal="true" Title="<%$ Translate: Tải dữ liệu %>">
    <ContentTemplate>
        <fieldset style="height:180px">
        <legend><%# Translate("Danh sách file")%></legend>
            <tlk:RadAsyncUpload runat="server" ID="RadAsyncUpload1" OnClientValidationFailed="validationFailed"
                AllowedFileExtensions="xls,xlsx,xml" MaxFileSize="4096000" UploadedFilesRendering="BelowFileInput"
                Skin="Metro" EnableFileInputSkinning="true" Width="250px" Style="padding: 20px 0px 0px 10px;">
                <Localization Select="<%$ Translate: Chọn %>" />
                <Localization Remove="<%$ Translate: Xóa %>" />
                <Localization Cancel="<%$ Translate: Hủy %>" />
            </tlk:RadAsyncUpload>
        </fieldset>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <div style="margin: 0px 10px 20px 10px; position: absolute; bottom: 0px; right: 0px;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Hoàn tất %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
<tlk:RadScriptBlock ID="rscriptblock" runat="server">
    <script type="text/javascript">
        function validationFailed(sender, args) {
            sender.deleteFileInputAt(0);
        }
    </script>
</tlk:RadScriptBlock>
