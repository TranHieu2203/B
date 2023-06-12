<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_ChartEmployee.ascx.vb"
    Inherits="Profile.ctrlHU_ChartEmployee" %>
<%@ Import Namespace="Common" %>
<style>
    /** Customize the org chart */
    html .RadOrgChart_Sunset
    {
        font-size: 12px;
    }
    
    html .RadOrgChart_Metro .rocGroup
    {
        color: white;
        background-color: rgb(50, 50, 50);
        background-color: rgba(0, 0, 0, .5);
    }
    html .RadOrgChart_Metro .rocItemTemplate
    {
        width: 170px;
        height: 80px;
    }
    html .RadOrgChart_Metro .rocItemContent, html .RadOrgChart_Metro .rocItemTemplate
    {
        background-color: #c7d7e6;
        border-color: #839cb2;
        background-image: none;
        color: #0d2b4d;
        text-align: left;
    }
    
    .pseudoImage
    {
        margin: 0 10px 0 0;
        border: 1px solid #cccccc;
        border-radius: 5px;
        width: 50px;
        height: 50px;
        background-position: center center;
        background-repeat: no-repeat;
        display: inline;
    }
    .rocImpo
    {
        font-weight: bold;
    }
    .rocImage
    {
        float: left;
        width: 50px;
        height: 70px;
    }
    .rocImage img
    {
        width: 47px;
    }
</style>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None" Visible="False">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="Both">
                <tlk:RadOrgChart rendermode="Lightweight" ID="rcEmployee" runat="server" Skin="Metro" DisableDefaultImage="false"
                    EnableDrillDown="true" EnableGroupCollapsing="true" EnableCollapsing="true" DataFieldID="EMPLOYEE_ID"
                    DataFieldParentID="DIRECT_MANAGER" DataTextField="FULLNAME_VN">
                    <ItemTemplate>
                        <asp:Panel runat="server" ID="Panel1" CssClass="TemplateHolder">
                            <div class="rocImage">
                                <tlk:RadBinaryImage ID="rbiImage1" CropPosition="Center" Width="45px" Height="50px"
                                    ResizeMode="Fill" runat="server" DataValue='<%# Eval("IMAGE_BINARY") %>' Visible='<%# if( Eval("IMAGE_BINARY") is Nothing , False, True)  %>' />
                            </div>
                            <div class="rocCustomTextWrap">
                                <span class="rocImpo">
                                    <%# Eval("FULLNAME_VN")%>
                                </span>
                                <br />
                                <span>
                                    <%--<i class="fa fa-user" aria-hidden="true"></i>--%> <%# Eval("TITLE_NAME_VN")%>
                                </span>
                                <br />
                                <span>
                                    <%--<i class="fa fa-sitemap" aria-hidden="true"></i>--%> <%# Eval("ORG_NAME")%>
                                </span>
                            </div>
                        </asp:Panel>
                    </ItemTemplate>
                </tlk:RadOrgChart>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }


        function clientButtonClicking(sender, args) {

        }
        function OnClientClose(oWnd, args) {

        }
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
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
