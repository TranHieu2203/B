<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDashboard.ascx.vb"
    Inherits="Profile.ctrlDashboard" %>
<asp:HiddenField runat="server" ID="hidOrgID" />
<%@ Import Namespace="Common" %>
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

    .RadWindow_Metro {
        border: none !important;
    }

    .RadWindow_Custom {
        /*width: 45% !important;*/
        /*height: 37% !important;*/
    }

    table#RAD_SPLITTER_RadSplitter1, td#RadPaneMain, #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_RadPaneMain, #ctl00_MainContent_RadPaneMain, #RAD_SPLITTER_ctl00_MainContent_RadSplitter1 {
        border: none;
    }

    #RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_RadPaneMain, .brackcrum, #WindowMainRegion {
        background: #f9f9f9;
    }

    #ctl00_MainContent_ctrlDashboard_rw5_C, RadWindowWrapper_ctl00_MainContent_ctrlDashboard_rw5 .rwTable {
        width: 100% !important;
    }

    .NodeFTEPlan {
        padding-left: 20px;
        background-color: transparent;
        background-image: url("../Static/Images/ftePlan.gif");
        background-repeat: no-repeat;
    }

    .NodeGroup {
        padding-left: 20px;
        background-color: transparent;
        background-image: url("../Static/Images/group.png");
        background-repeat: no-repeat;
    }

    .NodeFolder {
        padding-left: 20px;
        background-color: transparent;
        background-image: url("../Static/Images/folder.gif");
        background-repeat: no-repeat;
    }
</style>
<tlk:RadWindowManager ID="RadWindowManager1" Behaviors="None" runat="server" Width="200px"
    VisibleTitlebar="false" VisibleStatusbar="false" EnableShadow="false" ShowContentDuringLoad="false"
    VisibleOnPageLoad="true" Style="z-index: 2000 !important;">
    <Windows>
        <tlk:RadWindow runat="server" ID="rw1" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw2" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw3" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw4" CssClass="RadWindow_Custom">
        </tlk:RadWindow>
        <tlk:RadWindow runat="server" ID="rw5" CssClass="RadWindow_Custom" MaxHeight="38px">
            <ContentTemplate>
                <style type="text/css">
                    html {
                        overflow: hidden !important;
                    }
                </style>
                <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Công ty")%>
                            </td>
                            <td>
                                <tlk:RadDropDownTree RenderMode="Lightweight" runat="server" ID="cboOrgTree" Width="450px"
                                    DefaultMessage="Chọn đơn vị" DefaultValue="0" DataTextField="NAME" DataFieldID="ID"
                                    DataFieldParentID="PARENT_ID" DataValueField="ID" EnableFiltering="true" OnClientEntryAdded="OnClientSelected_OrgTree">
                                    <DropDownSettings Height="85%" CloseDropDownOnSelection="true" />
                                    <FilterSettings Filter="Contains" Highlight="Matches" />
                                    <DropDownNodeTemplate>
                                        <div class="<%# If(Eval("ID") < 1, "NodeFTEPlan", If(Eval("NHOMDUAN"), "NodeGroup", "NodeFolder")) %>">
                                            <span>
                                                <%# Eval("NAME")%>
                                            </span>
                                        </div>
                                    </DropDownNodeTemplate>
                                </tlk:RadDropDownTree>
                            </td>
                        </tr>
                    </table>
                </tlk:RadAjaxPanel>
            </ContentTemplate>
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var winW, winH, pos;
        var oWnd1, oWnd2, oWnd3, oWnd4, oWnd5;

        var i1 = 0;
        var i2 = 0;
        var i3 = 0;
        var i4 = 0;
        var i5 = 0;
        function SizeToFit(resize, isReload = 0) {
            debugger;
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
                if (isReload == 1) {
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
                var cbo = $find("<%# cboOrgTree.ClientID %>");
                var orgID = cbo._selectedValue * 1;
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

        function OnClientSelected_OrgTree(sender, args) {
            SizeToFit(1, 1);
        }

    </script>
</tlk:RadCodeBlock>
