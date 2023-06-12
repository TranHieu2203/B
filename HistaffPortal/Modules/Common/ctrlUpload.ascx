<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUpload.ascx.vb"
    Inherits="Common.ctrlUpload" %>
<style type="text/css">
    .RadUpload input.ruFakeInput {
        width: 160px;
    }
</style>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="Close" VisibleStatusbar="false"
    Height="300" Width="400" Modal="true" Title="<%$ Translate: Tải dữ liệu %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <fieldset style="height: 190px">
                <legend>
                    <%# Translate("Danh sách file")%></legend>
                <tlk:RadAsyncUpload runat="server" ID="RadAsyncUpload1"
                    AllowedFileExtensions="xls,xlsx" MaxFileSize="6096000" UploadedFilesRendering="BelowFileInput" 
                    Skin="Metro" Width="250px" Style="padding: 20px 0px 0px 10px;">
                    <Localization Select="<%$ Translate: Chọn %>" />
                    <Localization Remove="<%$ Translate: Xóa %>" />
                    <Localization Cancel="<%$ Translate: Hủy %>" />
                </tlk:RadAsyncUpload>
            </fieldset>
            <div style="margin: 0px 0px 10px 10px; text-align: right;">
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
