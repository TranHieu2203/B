<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlViewImage.ascx.vb"
    Inherits="Portal.ctrlViewImage" %>
<style type="text/css">
    #ctrlViewImage_rbiEmployeeImage {
         width: 100%;
  height: auto;
    }
    #RAD_SPLITTER_PANE_CONTENT_RadPaneMain{
        overflow:hidden;
    }
</style>

    <tlk:RadBinaryImage ID="rbiEmployeeImage" runat="server" AutoAdjustImageControlSize="false" 
            ResizeMode="Fill" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winH;
        var winW;
        function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() ;
            winW = $(window).width() - 90;
            $("#RAD_SPLITTER_PANE_CONTENT_RadPaneMain").stop().animate({ height: winH }, 0);
            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
        });
        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>