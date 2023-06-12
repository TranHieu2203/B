<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSetupGPSNewEdit.ascx.vb"
    Inherits="Attendance.ctrlSetupGPSNewEdit" %>
<style type="text/css">
    #RAD_SPLITTER_PANE_CONTENT_ctrlSetupGPSNewEdit_RadPane3{
        height: 28px !important;
    }
    #map {
        height: 100%;
        width: 100%;
    }
</style>
<asp:HiddenField ID="hidLng" runat="server" />
<asp:HiddenField ID="hidLat" runat="server" />
<asp:HiddenField ID="hidRadius" runat="server" />
<asp:HiddenField ID="hidLngDefault" runat="server" />
<asp:HiddenField ID="hidLatDefault" runat="server" />
<asp:HiddenField ID="hidRadiusDefault" runat="server" />
<asp:HiddenField ID="hidZoomDefault" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True" Width="133px" >
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false" AccessKey="">
                    </tlk:RadButton> 
                        <asp:RequiredFieldValidator ID="reqOrgName" ControlToValidate="txtOrgName" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập Phòng ban %>" ToolTip="<%$ Translate: Bạn phải nhập Phòng ban %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Tên địa điểm")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNameVN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Tên địa điểm. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Địa chỉ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtaddress" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtaddress"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Địa chỉ. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Lat (Vĩ độ)")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLatVD" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtLatVD"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Lat (Vĩ độ). %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Long (Kinh độ)")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtLongKD" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtLongKD"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Long (Kinh độ). %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="Label2" runat="server" Text="Bán kính (chấm công)"></asp:Label>
                    <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtRadius" MinValue="0" runat="server" AutoPostBack="false">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtRadius"
                        runat="server" ErrorMessage="Bạn phải nhập Bán kính (chấm công)" ToolTip="Bạn phải nhập Bán kính (chấm công)"> 
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server">
        <script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>
        <div id="map"></div>
        <script
            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAFMusygo3Z54bPWYmCgTu934sa_fnquAI&callback=initMap&v=weekly&libraries=drawing"
            defer
        ></script>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }
        function isNumeric(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }
        function initMap() {
            debugger;
            var historicalOverlay;
            var marker;
            var lng = Number(document.getElementById("<%= hidLng.ClientID %>").value);
            var lat = Number(document.getElementById("<%= hidLat.ClientID %>").value);
            var rad = Number(document.getElementById("<%= hidRadius.ClientID %>").value);
            var lngDefault = Number(document.getElementById("<%= hidLngDefault.ClientID %>").value);
            var latDefault = Number(document.getElementById("<%= hidLatDefault.ClientID %>").value);
            var radDefault = Number(document.getElementById("<%= hidRadiusDefault.ClientID %>").value); 
            var zoomDefault = Number(document.getElementById("<%= hidZoomDefault.ClientID %>").value);
            var zoom = 10;
            if (rad < 0 || !isNumeric(rad)) {
                lat = latDefault;
                lng = lngDefault;
                rad = radDefault;
            }
            if (lat < -90 || lat > 90 || !isNumeric(lat)) {
                lat = latDefault;
                lng = lngDefault;
                rad = radDefault;
            }
            else {
                zoom = zoomDefault;
            }
            if (lng < -180 || lng > 180 || !isNumeric(lng)) {
                lat = latDefault;
                lng = lngDefault;
                rad = radDefault;
            }
            else {
                zoom = zoomDefault;
            }
            const markerPos = { lat: lat, lng: lng };
            const map = new google.maps.Map(document.getElementById("map"), {
                zoom: zoom,
                center: markerPos,
            });

            if (lat != latDefault && lng != lngDefault && rad != radDefault) {
                historicalOverlay = new google.maps.Circle({
                    strokeColor: "black",
                    strokeOpacity: 1,
                    strokeWeight: 2,
                    fillColor: "black",
                    fillOpacity: 0.4,
                    map,
                    center: markerPos,
                    radius: rad,
                });
                marker = new google.maps.Marker({
                    position: markerPos,
                    map: map,
                    title: "Click to zoom",
                });
                zoom = zoomDefault;
            }
            else {
                historicalOverlay = new google.maps.Circle({
                    strokeColor: "black",
                    strokeOpacity: 1,
                    strokeWeight: 2,
                    fillColor: "black",
                    fillOpacity: 0.4,
                    map,
                });
                marker = new google.maps.Marker({
                    map: map,
                    title: "Click to zoom",
                });
            }
            var drawingManager = new google.maps.drawing.DrawingManager({
                drawingControl: true,
                drawingControlOptions: {
                    position: google.maps.ControlPosition.TOP_CENTER,
                    drawingModes: [
                        google.maps.drawing.OverlayType.CIRCLE
                    ],
                },
                circleOptions: {
                    fillColor: "black",
                    fillOpacity: 0.4,
                    strokeWeight: 2,
                    clickable: false,
                    zIndex: 1
                },
            });
            google.maps.event.addListener(drawingManager, 'circlecomplete', function (circle) {
                historicalOverlay.setMap(null);
                historicalOverlay = new google.maps.Circle({
                    strokeColor: "black",
                    strokeOpacity: 1,
                    strokeWeight: 2,
                    fillColor: "black",
                    fillOpacity: 0.4,
                    map,
                    center: circle.getCenter(),
                    radius: circle.getRadius(),
                });
                marker.setMap(null);
                marker = new google.maps.Marker({
                    position: circle.getCenter(),
                    map: map,
                    title: "Click to zoom",
                });
                var radius = circle.getRadius();
                var center = circle.getCenter();
                var lng = circle.getCenter().lng();
                var lat = circle.getCenter().lat();
                var txtRadius = $find('<%= txtRadius.ClientID %>');
                txtRadius.set_value(radius);
                var txtLatVD = $find('<%= txtLatVD.ClientID %>');
                txtLatVD.set_value(lat);
                var txtLongKD = $find('<%= txtLongKD.ClientID %>');
                txtLongKD.set_value(lng);
                console.log("Bán kính nè: " + radius);
                console.log("Kinh độ, vĩ độ nè: " + center);
                if (circle && circle.setMap) circle.setMap(null);
            });

            drawingManager.setMap(map);
        }
        window.initMap = initMap;
    </script>
</tlk:RadCodeBlock>
