<%@ Page Title="Home Page" Language="vb" MasterPageFile="~/Site.Master" AutoEventWireup="false"
    CodeBehind="Default.aspx.vb" Inherits="HistaffPortal._Default" ViewStateMode="Enabled"
    EnableEventValidation="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <tlk:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="none">
    </tlk:RadFormDecorator>
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
    <tlk:RadAjaxLoadingPanel runat="server" ID="LoadingPanel">
    </tlk:RadAjaxLoadingPanel>
    <div id="WindowMainRegion"  style="margin-top: 3px;border-radius: 0px;border: 0px; border-bottom-left-radius: 10px;border-bottom-right-radius: 10px;">
        <div class="panel_bar">
            <div class="panel_title">
                <asp:Literal ID="liTitle" runat="server"></asp:Literal>
            </div>
        </div>
        <asp:Panel ID="Panel1" runat="server" Style="padding-bottom: 5px;">
            <asp:PlaceHolder ID="PagePlaceHolder" runat="server"></asp:PlaceHolder>
            <Common:ctrlSessionWarning ID="SessionWarning" runat="server" />
        </asp:Panel>
    </div>
    <tlk:RadWindow runat="server" ID="rwMainPopup" VisibleStatusbar="false" Width="800px"
        Height="500px" EnableShadow="true" Behaviors="Maximize, Close" Modal="true" ShowContentDuringLoad="false">
    </tlk:RadWindow>
    <tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function onItemClicked(sender, args) {
                if (args.get_item().get_value().toString().indexOf(";") > 0) {
                    var value = args.get_item().get_value().toString().split(";");
                    var fid = value[1];
                    var mid = value[0];
                    window.location.replace("default.aspx?fid=" + fid + "&mid=" + mid);
                }
            }

            function ResetDountDownSession() {
                stopTimers();
                _popupTimer = window.setTimeout(showWarningPopup, (sessionTimeout - sessionTimeoutWarning) * 60 * 1000);
                updateCountDownTest();

            }

        </script>
    </tlk:RadCodeBlock>
</asp:Content>
