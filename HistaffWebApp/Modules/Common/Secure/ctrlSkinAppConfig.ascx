<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSkinAppConfig.ascx.vb" Inherits="Common.ctrlSkinAppConfig" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<style>
    /* the preview image element should not exceed the following size */
    #preview-image {
        max-height: 200px;
        max-width: 400px;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane2" runat="server" Height="130px" Width="700px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMainToolBar" runat="server"></tlk:RadToolBar>
        <table class="table-form" align="center">
            <tr>
                <td colspan="2" align="left" style="vertical-align:top;">
                    <%# Translate("Mã Skin")%>
                </td>
                <td>
                    <tlk:RadComboBox runat="server" ID="rcbSkin" AutoPostBack="true" >
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqCode" ControlToValidate="rcbSkin" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải chọn Mã Skin %>" ToolTip="<%$ Translate:  Bạn phải chọn Mã Skin %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="vertical-align:top;">
                    <%# Translate("Màu sắc")%>
                 </td>
                <td>
                    <tlk:RadColorPicker runat="server" ID="rcpColor" AutoPostBack="true"></tlk:RadColorPicker> 
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" style="vertical-align:top;">
                    <%# Translate("Hình nền Menu")%>
                </td>
                <td>
                    <tlk:RadAsyncUpload ID="raUpload" runat="server" MaxFileInputsCount="1" AllowedFileExtensions="jpg, jpeg, png" OnClientFileUploadRemoved="OnClientFileUploadRemoved" OnClientFileSelected="OnClientFileSelected">
                    </tlk:RadAsyncUpload>
                    <img src="http://ctt.trains.com/sitefiles/images/no-preview-available.png" id="preview-image" alt="Preview image here"  />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="130px" Scrolling="Y">
        <table class="table-form" align="center">
            <tr>
                <td class="lb">
                    <img src="~/Static/Images/skinList1.png" runat="server" id="imgShow" />
                </td>
            </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:HiddenField ID="countImg" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OnClientFileUploadRemoved(sender, args) {
            document.getElementById("<%= countImg.ClientID %>").value = "0";
            $telerik.$("#preview-image").attr('src', "http://ctt.trains.com/sitefiles/images/no-preview-available.png");
            
        }

        function OnClientFileSelected(sender, args) {
            var file = args.get_file();
            if (file) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $telerik.$("#preview-image").attr('src', e.target.result);
                }
                reader.readAsDataURL(file);
            }
        }

        Telerik.Web.UI.RadAsyncUpload.prototype._onFileSelected = function (row, fileInput, fileName, shouldAddNewInput, file) {
            document.getElementById("<%= countImg.ClientID %>").value = "1";
            var args = {
                row: row,
                fileInputField: fileInput,
                file: file
            };
            args.rowIndex = $telerik.$(row).index();
            args.fileName = fileName;
            this._selectedFilesCount++;
            shouldAddNewInput = shouldAddNewInput &&
            (this.get_maxFileCount() == 0 || this._selectedFilesCount < this.get_maxFileCount());
            this._marshalUpload(row, fileName, shouldAddNewInput);
            var labels = $telerik.$("label", row);
            if (labels.length > 0)
                labels.remove();
            $telerik.$.raiseControlEvent(this, "fileSelected", args);
        }
    </script>
</tlk:RadCodeBlock>