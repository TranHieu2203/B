<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlViewMap.ascx.vb"
    Inherits="Common.ctrlViewMap" %>
<style type="text/css">
    #map {
        height: 100%;
        width: 100%;
    }
</style>
<asp:HiddenField ID="hidLng" runat="server" />
<asp:HiddenField ID="hidLat" runat="server" />
<script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>
<div id="map"></div>
<script
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAFMusygo3Z54bPWYmCgTu934sa_fnquAI&callback=initMap&v=weekly"
    defer
></script>
<script type="text/javascript">
    var myLatlng = { lat: Number(document.getElementById("<%= hidLat.ClientID %>").value), lng: Number(document.getElementById("<%= hidLng.ClientID %>").value) };
    function initMap() {
        var lng = Number(document.getElementById("<%= hidLng.ClientID %>").value);
        var lat = Number(document.getElementById("<%= hidLat.ClientID %>").value);
        // The location of Uluru
        const uluru = { lat: lat, lng: lng };
        // The map, centered at Uluru
        const map = new google.maps.Map(document.getElementById("map"), {
            zoom: 15,
            center: uluru,
        });

        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: "Click to zoom",
        });
        //map.addListener("center_changed", function () {
        //    // 3 seconds after the center of the map has changed, pan back to the
        //    // marker.
        //    window.setTimeout(function () {
        //        map.panTo(marker.getPosition());
        //    }, 3000);
        //});
        marker.addListener("click", function () {
            map.setZoom(18);
            map.setCenter(marker.getPosition());
        });
    }
    window.initMap = initMap;
</script>