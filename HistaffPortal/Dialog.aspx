<%@ Page Title="Home Page" Language="vb" AutoEventWireup="false" CodeBehind="Dialog.aspx.vb"
    Inherits="HistaffPortal.Dialog" ViewStateMode="Enabled" EnableEventValidation="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Scripts/DateTimeFormat.js" type="text/javascript"></script>
</head>
<body style="overflow: hidden">
    <form id="Form1" runat="server">
    <tlk:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        <StyleSheets>
            <tlk:StyleSheetReference Path="~/Styles/reset.css" />
            <tlk:StyleSheetReference Path="~/Styles/Site.css" />
            <tlk:StyleSheetReference Path="~/Styles/Layout.css" />
            <tlk:StyleSheetReference Path="~/Styles/jMenu.jquery.css" />
            <tlk:StyleSheetReference Path="~/Styles/RadGrid.css" />
            <tlk:StyleSheetReference Path="~/Styles/Scheduler.css" />
        </StyleSheets>
    </tlk:RadStyleSheetManager>
    <tlk:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="360000">
        <Scripts>
            <tlk:RadScriptReference Path="~/Scripts/jquery-1.7.1.min.js" />
            <tlk:RadScriptReference Path="~/Scripts/common.js" />
            <tlk:RadScriptReference Path="~/Scripts/jMenu.jquery.js" />
            <tlk:RadScriptReference Path="~/Scripts/noty/jquery.noty.js" />
            <tlk:RadScriptReference Path="~/Scripts/noty/layouts/center.js" />
            <tlk:RadScriptReference Path="~/Scripts/noty/layouts/topCenter.js" />
            <tlk:RadScriptReference Path="~/Scripts/noty/themes/default.js" />
            <tlk:RadScriptReference Path="~/Scripts/PreventBackSpace.js" />
        </Scripts>
    </tlk:RadScriptManager>
    <tlk:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="None">
    </tlk:RadFormDecorator>
    <tlk:RadToolTipManager ID="RadToolTipManager1" runat="server" AutoTooltipify="true"
        RelativeTo="Element">
    </tlk:RadToolTipManager>
    <tlk:RadAjaxManager runat="server" ID="RadAjaxManager1" ClientEvents-OnResponseEnd="AjaxClientEnded">
        <AjaxSettings>
            <tlk:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <tlk:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </tlk:AjaxSetting>
        </AjaxSettings>
    </tlk:RadAjaxManager>
    <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
        <tlk:RadPane ID="RadPaneMain" runat="server" Scrolling="None">
            <asp:Panel ID="Panel1" runat="server">
                <asp:PlaceHolder ID="PagePlaceHolder" runat="server"></asp:PlaceHolder>
            </asp:Panel>
            <tlk:RadSkinManager ID="rsSkin" Enabled="true" Visible="false" PersistenceMode="Cookie"
                ShowChooser="true" runat="server">
            </tlk:RadSkinManager>
        </tlk:RadPane>
    </tlk:RadSplitter>
    <tlk:RadWindow runat="server" ID="rwMainPopup" VisibleStatusbar="false" Width="800px"
        Height="450px" EnableShadow="true" Behaviors="Maximize,Close" Modal="true" ShowContentDuringLoad="false">
    </tlk:RadWindow>
    <tlk:RadAjaxLoadingPanel runat="server" ID="LoadingPanel" InitialDelayTime="1000">
    </tlk:RadAjaxLoadingPanel>
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //Thiết lập notify
            $.noty.defaults = {
                layout: 'topCenter',
                theme: 'defaultTheme',
                type: 'alert',
                text: '',
                dismissQueue: true, // If you want to use queue feature set this true
                template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
                animation: {
                    open: { height: 'toggle' },
                    close: { height: 'toggle' },
                    easing: 'swing',
                    speed: 500 // opening & closing animation speed
                },
                timeout: false, // delay for closing event. Set false for sticky notifications
                force: false, // adds notification to the beginning of queue when set to true
                modal: false,
                closeWith: ['click'], // ['click', 'button', 'hover']
                callback: {
                    onShow: function () { },
                    afterShow: function () { },
                    onClose: function () { },
                    afterClose: function () { }
                },
                buttons: false // an array of buttons
            };
            function getURLParameter(name) {
                return decodeURI(
                    (RegExp(name + '=' + '(.+?)(&|$)').exec(location.search) || [, null])[1]
                );
            }
            function ExportReport(arg) {
                window.location = "Export.aspx?id=" + arg;
            }
            function GetObjectViolation(id, violation, type) {
                window.location = "VI_GetFile.aspx?violation=" + violation + "&type=" + type + "&id=" + id;
            }
            function AjaxClientEnded(s, e) {
                var parentPage = window.parent;

                if (parentPage == null) {
                    return;
                }

                while (parentPage != null) {
                    if (parentPage.ResetDountDownSession) {
                        parentPage.ResetDountDownSession();
                        break;
                    }
                    else {
                        parentPage = parentPage.parent;
                    }
                }

            }


            function ResizeSplitter_Demo(sender, eventArgs) {
                if (!sender) return;
                var viewport = $telerik.getViewPortSize();
                sender.set_width(viewport.width + 5);
                sender.set_height(viewport.height + 10);
            }
        </script>
    </tlk:RadCodeBlock>
    </form>
</body>
</html>
