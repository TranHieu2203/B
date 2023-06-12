<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLicense.ascx.vb"
    Inherits="Common.ctrlLicense" %>
<style type="text/css">
    .RadUpload input.ruFakeInput
    {
        width: 100px;
    }
</style>
<tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
    <div style="overflow: hidden">
        <fieldset>
            <legend>
                <%# Translate("Thông tin License")%>
            </legend>
            <table class="table-form">
                <tr>
                    <td class="lb">
                        <%# Translate("Key:")%>
                    </td>
                    <td>
                        <b>
                            <asp:Label ID="lbLicenseKey" runat="server"></asp:Label>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td class="lb">
                        <%# Translate("License:")%>
                    </td>
                    <td>
                        <b>
                            <asp:Label ID="lbLicense" runat="server"></asp:Label>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="padding-bottom: 10px">
                        <%# Translate("Chọn file")%>
                    </td>
                    <td>
                        <tlk:RadAsyncUpload runat="server" ID="RadAsyncUpload1" OnClientFilesUploaded="onClientFileUploaded"
                            OnClientFileUploadRemoved="OnClientFileUploadRemoved" PostbackTriggers="btnYES"
                            AllowedFileExtensions=".key" MaxFileInputsCount="1" Width="100px" Skin="Metro">
                            <Localization Select="<%$ Translate: Chọn %>" />
                            <Localization Remove="<%$ Translate: Xóa %>" />
                        </tlk:RadAsyncUpload>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <tlk:RadButton ID="btnYES" Style="right: 20px; position: absolute" runat="server"
                            Width="60px" Text="<%$ Translate: Kích hoạt %>" Font-Bold="true" Enabled="false">
                        </tlk:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
        <center>
            Copyright &copy; 2013 Tinhvan Consulting
            <br />
            <img src="Static/Images/tvc_logo.png" width="115" alt="tvc_logo" />
        </center>
    </div>
</tlk:RadAjaxPanel>
<tlk:RadScriptBlock ID="rscriptblock" runat="server">
    <script type="text/javascript">

        function onClientFileUploaded(sender, args) {
            var btn = $find("<%= btnYES.ClientID %>");

            btn.set_enabled(true);
        }

        function OnClientFileUploadRemoved(sender, args) {
            var btn = $find("<%= btnYES.ClientID %>");

            btn.set_enabled(false);
        }
          
    </script>
</tlk:RadScriptBlock>
