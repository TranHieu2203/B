<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUpdateManagerDialog.ascx.vb"
    Inherits="Profile.ctrlUpdateManagerDialog" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #RAD_SPLITTER_PANE_CONTENT_ctrlUpdateManagerDialog_RadPane1 table{
        display: flex !important;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctrlUpdateManagerDialog_RadPane1 table tbody{
            margin: 0 auto !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
            <table class="table-form">
                <tr>
                    <td>
                        <asp:CheckBox ID="isChangeLM" runat="server" Text="Thay đổi vị trí Quản lý trực tiếp" />
                    </td>
                </tr>
                <tr>
                    <td class="lbl" style="padding-left: 23px!important;">
                        <%# Translate("Vị trí QLTT mới")%>
                    </td>
                    <td>
                        <asp:HiddenField ID="hidLM" runat="server" />
                        <tlk:RadTextBox ID="txtLM" runat="server"  ReadOnly="true" SkinID="ReadOnly" Width="400px">
                        </tlk:RadTextBox>
                        <tlk:RadButton ID="btnFindLM" runat="server" SkinID="ButtonView" CausesValidation="false">
                        </tlk:RadButton>
                    </td>
                </tr>
                <tr>
                    <td class="lbl" style="padding-left: 23px!important;">
                        <%# Translate("Họ và tên QLTT")%>
                    </td>
                    <td>
                         <tlk:RadTextBox ID="txtLMName" runat="server" ReadOnly="true" SkinID="ReadOnly"  Width="428px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="isChangeCSM" runat="server" Text="Thay đổi vị trí Quản lý gián tiếp" />
                    </td>
                </tr>
                <tr>
                    <td class="lbl" style="padding-left: 23px!important;">
                        <%# Translate("Vị trí QLGT mới")%>
                    </td>
                    <td>
                        <asp:HiddenField ID="hidCSM" runat="server" />
                        <tlk:RadTextBox ID="txtCSM" runat="server"  ReadOnly="true" SkinID="ReadOnly"  Width="400px">
                        </tlk:RadTextBox>
                        <tlk:RadButton ID="btnFindCSM" runat="server" SkinID="ButtonView" CausesValidation="false">
                        </tlk:RadButton>
                    </td>
                </tr>
                <tr>
                    <td class="lbl" style="padding-left: 23px!important;">
                        <%# Translate("Họ và tên QLGT")%>
                    </td>
                    <td>
                         <tlk:RadTextBox ID="txtCSMName" runat="server" ReadOnly="true" SkinID="ReadOnly"  Width="428px">
                        </tlk:RadTextBox>
                    </td>
                </tr>
            </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" MinHeight="50" Height="50px" Scrolling="None">
                <div style="margin: 20px 10px 10px 10px; text-align: right; vertical-align: middle">
                    <asp:HiddenField ID="hidSelected" runat="server" />
                    <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Lưu %>"
                        Font-Bold="true" CausesValidation="false">
                    </tlk:RadButton>
                    <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                        Font-Bold="true" CausesValidation="false" OnClientClicked="btnCancelClick">
                    </tlk:RadButton>
                </div>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindLM" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="phFindCSM" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_PANE_CONTENT_RadPaneMain');
        //        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function btnYesClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = $("#<%=hidSelected.ClientID %>").val();
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

        function btnCancelClick() {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            oArg.ID = 'Cancel';
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

        function OnClientClose(oWnd, args) {
            oWnd = $find('<%=popupId2 %>');
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }

        function postBack(arg) {
            var ajaxManager = $find("<%= AjaxManagerId2 %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }
    </script>
</tlk:RadScriptBlock>
