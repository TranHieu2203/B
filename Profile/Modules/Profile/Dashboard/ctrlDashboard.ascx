<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDashboard.ascx.vb"
    Inherits="Profile.ctrlDashboard" %>
<asp:HiddenField runat="server" ID="hidOrgID" />
<style>
    #RadWindowWrapper_ctl00_MainContent_ctrlDashboard_rw5 {
        position: absolute;
        transform: none;
        backface-visibility: visible;
        visibility: visible;
        left: 7px !important;
        top: 97px !important;
        z-index: 9999;
        border: none !important;

    }
    .RadWindow_Metro{
        border:none !important;
    }
    .RadWindow_Custom{
        /*width: 45% !important;*/
        /*height: 37% !important;*/
    }
    table#RAD_SPLITTER_RadSplitter1,td#RadPaneMain,#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_RadPaneMain,#ctl00_MainContent_RadPaneMain,#RAD_SPLITTER_ctl00_MainContent_RadSplitter1 {
        border: none;
    }
    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_RadPaneMain,.brackcrum,#WindowMainRegion{
        background: #f9f9f9;
    }
    #ctl00_MainContent_ctrlDashboard_rw5_C , RadWindowWrapper_ctl00_MainContent_ctrlDashboard_rw5 .rwTable{
        width: 100% !important;
    }
</style>
<tlk:radwindowmanager id="RadWindowManager1" behaviors="None" runat="server" width="200px"
    visibletitlebar="false" visiblestatusbar="false" enableshadow="false" showcontentduringload="false"
    visibleonpageload="true" style="z-index: 2000 !important;">
    <windows>
        <tlk:radwindow runat="server" id="rw1" cssclass="RadWindow_Custom">
        </tlk:radwindow>
        <tlk:radwindow runat="server" id="rw2" cssclass="RadWindow_Custom">
        </tlk:radwindow>
        <tlk:radwindow runat="server" id="rw3" cssclass="RadWindow_Custom">
        </tlk:radwindow>
        <tlk:radwindow runat="server" id="rw4" cssclass="RadWindow_Custom">
        </tlk:radwindow>
        <tlk:radwindow runat="server" id="rw5" cssclass="RadWindow_Custom" MaxHeight="38px">
            <contenttemplate>
                <style type="text/css">
                    html {
                        overflow: hidden !important;
                    }
                </style>
                <tlk:radajaxpanel id="RadAjaxPanel1" runat="server">
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Công ty")%>
                            </td>
                            <td>
                                <tlk:radcombobox id="cboOrgName2" runat="server" onclientselectedindexchanged="OnClientSelected_OrgName2">
                                </tlk:radcombobox>
                            </td>
                        </tr>
                    </table>
                </tlk:radajaxpanel>
            </contenttemplate>
        </tlk:radwindow>
    </windows>
</tlk:radwindowmanager>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winW, winH, pos;
        var oWnd1, oWnd2, oWnd3, oWnd4, oWnd5;

        var i1 = 0;
        var i2 = 0;
        var i3 = 0;
        var i4 = 0;
        var i5 = 0;
        function SizeToFit(resize, isReload = 0) {
            window.setTimeout(function () {
                winW = ($("#WindowMainRegion").width() - 16) / 3;
                winH = ($("#WindowMainRegion").height() - 13);
                pos = $("#WindowMainRegion").offset();
                var left = pos.left + 8;
                var top = pos.top + 32;

                oWnd1 = $find("<%=rw1.ClientID %>");
                oWnd2 = $find("<%=rw2.ClientID %>");
                oWnd3 = $find("<%=rw3.ClientID %>");
                oWnd4 = $find("<%=rw4.ClientID %>");
                if (isReload) {
                    i1 = 0;
                    i2 = 0;
                    i3 = 0;
                    i4 = 0;
                }
                var rw5Width = "" + (winW * 3 - 15) + "px";
                document.getElementById("RadWindowWrapper_ctl00_MainContent_ctrlDashboard_rw5").style.width = rw5Width;
                document.getElementById("RadWindowWrapper_ctl00_MainContent_ctrlDashboard_rw5").style.marginLeft = "5px";
                // oWnd5.setSize(winW * 3, 50);
                //oWnd5.moveTo(left, top);
                var orgID = $get('<%= hidOrgID.ClientID %>').value;
                if (!orgID) orgID = 1;

                oWnd1.setSize(winW * 2 - 130, winH / 2 - 45);
                oWnd1.moveTo(left + 5, top + 40);
                if (i1 == 0) {
                    radopen('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeStatistic&group=Dashboard&width=' + (oWnd1.get_width()) + '&height=' + (oWnd1.get_height() - 30) + "&noscroll=1&resize=" + resize + "&orgid=" + orgID, "rw1");
                    i1 = 1;
                }

                oWnd2.setSize(winW * 2 - 130, winH / 2 - 30);
                oWnd2.moveTo(left + 5, top + winH / 2 + 15);
                if (i2 == 0) {
                    radopen('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeChange&group=Dashboard&width=' + (oWnd1.get_width()) + '&height=' + (oWnd1.get_height() - 30) + "&noscroll=1&resize=" + resize + "&orgid=" + orgID, "rw2");
                    i2 = 1;
                }

                oWnd3.setSize(winW + 100, (winH / 2) - 45);
                oWnd3.moveTo(left + winW * 2 - 110, top + 40);
                if (i3 == 0) {
                    radopen('Dialog.aspx?mid=Profile&fid=ctrlDBSeniority&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize + "&orgid=" + orgID, "rw3");
                    i3 = 1;
                }

                oWnd4.setSize(winW + 100, (winH / 2) - 30);
                oWnd4.moveTo(left + winW * 2 - 110, top + winH / 2 + 15);
                if (i4 == 0) {
                    radopen('Dialog.aspx?mid=Profile&fid=ctrlDBInfoProfile&group=Dashboard&width=' + (oWnd1.get_width() - 20) + '&height=' + (oWnd1.get_height() - 80) + "&noscroll=1&resize=" + resize + "&orgid=" + orgID, "rw4");
                    i4 = 1;
                }

            }, 100);
        }
        $(window).resize(function () {
            SizeToFit(1);
        });
        function setSrc() {
            var src1, src2, src3, src4;
            var src1 = $("#RadWindowWrapper_<%=rw1.ClientID %>").find("iframe").attr('src');
            var src2 = $("#RadWindowWrapper_<%=rw2.ClientID %>").find("iframe").attr('src');
            var src3 = $("#RadWindowWrapper_<%=rw3.ClientID %>").find("iframe").attr('src');
            var src4 = $("#RadWindowWrapper_<%=rw4.ClientID %>").find("iframe").attr('src');
            //console.log(src1);
            //console.log(src2);
            //console.log(src3);
            //console.log(src4);
            $("#RadWindowWrapper_<%=rw1.ClientID %>").find("iframe").attr('src', src1);
            $("#RadWindowWrapper_<%=rw2.ClientID %>").find("iframe").attr('src', src2);
            $("#RadWindowWrapper_<%=rw3.ClientID %>").find("iframe").attr('src', src3);
            $("#RadWindowWrapper_<%=rw4.ClientID %>").find("iframe").attr('src', src4);

            <%--var winW, winH, pos;
            var oWnd1, oWnd2, oWnd3, oWnd4;
            winW = ($("#WindowMainRegion").width() - 16) / 3;
            winH = ($("#WindowMainRegion").height() - 13);

            oWnd1 = $find("<%=rw1.ClientID %>");
            oWnd2 = $find("<%=rw2.ClientID %>");
            oWnd3 = $find("<%=rw3.ClientID %>");
            oWnd4 = $find("<%=rw4.ClientID %>");

            oWnd1.setSize(winW * 2 - 100, winH / 2);
            oWnd2.setSize(winW * 2 - 100, winH / 2);
            oWnd3.setSize(winW + 100, (winH / 2) - 1);
            oWnd4.setSize(winW + 100, (winH / 2) - 1);--%>

        }
        function OnClientSelected_OrgName2(sender, args) {
            var org = $get('<%= hidOrgID.ClientID %>');
            var cbo = $find("<%# cboOrgName2.ClientID %>");
            org.value = cbo.get_value();
            SizeToFit(1, 1);
        }

    </script>
</tlk:radcodeblock>
