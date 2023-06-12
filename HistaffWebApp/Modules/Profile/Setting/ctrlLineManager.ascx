<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlLineManager.ascx.vb"
    Inherits="Profile.ctrlLineManager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPane1" runat="server">
        <tlk:RadSlider ID="RadSlider_Ticks" runat="server" SmallChange="0.1" Height="25px"
            Width="370px" MinimumValue="0.3" MaximumValue="1" Value="1" AnimationDuration="400"
            OnClientValueChanged="onClientValueChanged" IncreaseText="Zoom In" DecreaseText="Zoom Out"
            Style="margin: auto;">
        </tlk:RadSlider>
            <div id="RestrictionZone" style="overflow: auto;">
                <tlk:RadOrgChart runat="server" ID="chartData1" EnableDrillDown="true" EnableCollapsing="true">
                </tlk:RadOrgChart>
            </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var scaleParamX;
        var scaleParamY;

        function onClientValueChanged(sender, args) {
            var zoomValue = sender.get_value();

            zoom(zoomValue);
        }

        function zoomAfterDrillDown() {
            if (scaleParamX && scaleParamY)
                setZoom();
        }

        function zoom(zoomValue) {
            scaleParamX = zoomValue;
            scaleParamY = zoomValue;

            setZoom();
        }

        function setZoom() {
            var $ = $telerik.$;
            window.setTimeout(function () {
                var org = $("#RestrictionZone").find(".RadOrgChart").get(0);

                if ($telerik.isFirefox)
                    org.style.MozTransform = "scale(" + scaleParamX + "," + scaleParamY + ")";
                else if ($telerik.isIE && !$telerik.isIE7)
                    org.style.zoom = scaleParamX;
                else if ($telerik.isIE7)
                    $(org).find(".rocRootNodeList").get(0).style.zoom = scaleParamX;
                else if ($telerik.isOpera || $telerik.isSafari)
                    org.style.WebkitTransform = "scale(" + scaleParamX + ")";
            }, 100);
        }
    </script>
</tlk:RadCodeBlock>
