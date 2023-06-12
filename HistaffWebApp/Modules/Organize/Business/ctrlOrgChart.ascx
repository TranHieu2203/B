<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlOrgChart.ascx.vb"
    Inherits="Profile.ctrlOrgChart" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane2" runat="server">
        <div id="note">
            <div class="note-header">
                <%--<span><%# Translate("Sơ Đồ Tổ Chức")%></span>--%>
            </div>
            <div class="note-comment">
                <span>
                    <%# Translate("Click vào dấu cộng (+), trừ (-) để điều khiển đóng, mở cơ cấu tổ chức")%></span>
            </div>
            <p></p>
            <div class="note-comment">
                <tlk:RadButton ID="btnExport" runat="server" OnClientClicked="ExportImage" Text="<%$ Translate: Xuất hình %>">
                </tlk:RadButton>
            </div>
        </div>
        <div id="RestrictionZone" class="RestrictionZone">
            <tlk:RadOrgChart runat="server" ID="chartData1" Width="100%" EnableCollapsing="true"
                Skin="Default" DataFieldID="ID" DataFieldParentID="PARENT_ID" DataTextField="ORG_NAME"
                DisableDefaultImage="false" EnableDrillDown="true">
                <ItemTemplate>
                    <asp:Panel runat="server" ID="Panel1" Width="100%">
                        <b>
                            <%# Eval("ORG_NAME")%></b>
                    </asp:Panel>
                    <tlk:RadToolTip runat="server" ID="RadToolTip1" Width="230px" TargetControlID="Panel1"
                        RelativeTo="Element" ShowEvent="OnMouseOver" Position="BottomCenter" AutoCloseDelay="0"
                        ShowDelay="0" HideDelay="0" Skin="Default">
                        <table class="table-form" style="width: auto">
                            <tr>
                                <td style="text-align: center; background-position: center" rowspan="3">
                                    <tlk:RadBinaryImage ID="rbiImage1" CropPosition="Center" Width="60px" Height="75px"
                                        ResizeMode="Fill" runat="server" DataValue='<%# Eval("IMAGE_BINARY") %>' Visible='<%# if( Eval("IMAGE_BINARY") is Nothing , False, True)  %>' />
                                </td>
                                <td align="left">
                                    <b>
                                        <%# Eval("LAST_NAME_VN")%></b>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%# Eval("FIRST_NAME_VN")%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%# Eval("TITLE_NAME_VN") %>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="color: Gray">
                                    Mobile:
                                </td>
                                <td align="left">
                                    <%# Eval("MOBILE_PHONE")%>
                                </td>
                            </tr>
                            <%--  <tr>
                                <td align="right" style="color: Gray">
                                    Email:
                                </td>
                                <td align="left">
                                    <%# Eval("WORK_EMAIL")%>
                                </td>
                            </tr>--%>
                        </table>
                    </tlk:RadToolTip>
                </ItemTemplate>
            </tlk:RadOrgChart>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script src="../../../Scripts/html2canvas.min.js" type="text/javascript"></script>
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
        
        function DownloadPNG(canvas, filename) {
            var lnk = document.createElement('a'), e;
            lnk.download = filename;
            lnk.href = canvas.toDataURL("image/png;base64");
            if (document.createEvent) {
                e = document.createEvent("MouseEvents");
                e.initMouseEvent("click", true, true, window,
                    0, 0, 0, 0, 0, false, false, false,
                    false, 0, null);

                lnk.dispatchEvent(e);
            } else if (lnk.fireEvent) {
                lnk.fireEvent("onclick");
            }
        }

        function ExportImage(sender, args) {
            var ro = document.querySelector("div.rocViewPort");
            ro.style.visibility = "visible";
            var useWidth = ro.offsetWidth;
            var useHeight = ro.offsetHeight;
            html2canvas(ro, {
                width: useWidth,
                height: useHeight
            }).then(canvas => {
                DownloadPNG(canvas, 'OrgChart.png')
            });
          }
    </script>
</tlk:RadCodeBlock>
