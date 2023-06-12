<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="HistaffWebApp._Default" ViewStateMode="Enabled"
    EnableEventValidation="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="MainBody" runat="server" ContentPlaceHolderID="MainContent" style="overflow: hidden">
    <tlk:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <tlk:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <tlk:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="LoadingPanel" />
                    <tlk:AjaxUpdatedControl ControlID="PagePlaceHolder" LoadingPanelID="LoadingPanel" />
                </UpdatedControls>
            </tlk:AjaxSetting>
        </AjaxSettings>
    </tlk:RadAjaxManager>
    <tlk:RadToolTipManager ID="RadToolTipManager1" runat="server" AutoTooltipify="true"
        RelativeTo="Element">
    </tlk:RadToolTipManager>
    <tlk:RadAjaxLoadingPanel runat="server" ID="LoadingPanel"
        InitialDelayTime="500">
    </tlk:RadAjaxLoadingPanel>
    <div id="WindowMainRegion">
        <div class="brackcrum">
        </div>
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
            <tlk:RadPane ID="RadPaneMain" runat="server" Scrolling="None" Width="100%" >
                <asp:Panel ID="Panel1" runat="server" Style="overflow: hidden">
                    <asp:PlaceHolder ID="PagePlaceHolder" runat="server"></asp:PlaceHolder>
                    <Common:ctrlSessionWarning ID="SessionWarning" runat="server" />
                </asp:Panel>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </div>
    <tlk:RadWindow runat="server" ID="rwMainPopup" VisibleStatusbar="false" Width="800px"
        Height="500px" EnableShadow="true" Behaviors="Maximize, Close" Modal="true" ShowContentDuringLoad="false">
    </tlk:RadWindow>
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            var winH;
            var winW;

            function SizeToFitMain() {
                Sys.Application.remove_load(SizeToFitMain);
                winH = $(window).height() - 91;
                winW = $(window).width();
                $("#WindowMainRegion").stop().animate({ height: winH, width: winW }, 0);
                Sys.Application.add_load(SizeToFitMain);
            }

            SizeToFitMain();

            $(document).ready(function () {
                SizeToFitMain();
            });
            $(window).resize(function () {
                SizeToFitMain();
            });

            function RowSelecting() {
                return false;
            }

            function ResetDountDownSession() {
                stopTimers();
                _popupTimer = window.setTimeout(showWarningPopup, (sessionTimeout - sessionTimeoutWarning) * 60 * 1000);
                updateCountDownTest();

            }

            function ResizeSplitter_Demo(sender, eventArgs) {
                if (!sender) return;
                var viewport = $telerik.getViewPortSize();
                sender.set_width(viewport.width + 3);
                sender.set_height(viewport.height - 85);
            }


        </script>
    </tlk:RadCodeBlock>
</asp:Content>
