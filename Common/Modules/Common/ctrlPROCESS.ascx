<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPROCESS.ascx.vb"
    Inherits="Common.ctrlPROCESS" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter runat="server" ID="RadSplitter1" Width="100%" Height="100%" Orientation="Horizontal">
 <tlk:RadPane runat="server" ID="RadPane1" Width="100%" Height="50%" Scrolling="None">
        <tlk:RadSplitter runat="server" ID="RadSplitter2" Width="100%" Height="100%">
            <tlk:RadPane runat="server" ID="RadPane3" MinWidth="260" Width="340px" Style="overflow: auto !important">
                <tlk:RadTreeView ID="treeOtherListType" runat="server" CausesValidation="false" Height="100%">
                </tlk:RadTreeView>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
            </tlk:RadSplitBar>
            <tlk:RadPane runat="server" ID="RadPane4" MinWidth="260" Width="370px" Height="100%"
                Scrolling="None">
                <Common:ctrlOrganization runat="server" ID="ctrlOrg" />
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
            </tlk:RadSplitBar>
            <tlk:RadPane runat="server" ID="RadPane5" Scrolling="None" Height="100%">
                <tlk:RadToolBar ID="rtbMain" runat="server">
                </tlk:RadToolBar>
                <br />
                <div>
                    <asp:Label ID="lblProgram" runat="server" Font-Size="Large" Font-Bold="true" CssClass="spancustomleft">&nbsp;</asp:Label>
                    <asp:Label ID="lblRequest" runat="server" Font-Size="Large" Font-Bold="true" CssClass="spancustomright">&nbsp;</asp:Label>
                </div>
                <div style="margin-top: 10px; margin-bottom: 10px">
                    <table>
                        <tr>
                            <td class="lb" style="padding-right: 5px; padding-left: 20px">
                                <%# Translate("CM_CTRLPROCESS_SCAN_TIME")%>&nbsp;
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="RadNumTB" MinValue="1" MaxValue="30" Width="50px"
                                    Value="5" MaxLength="2" AutoPostBack="true" ShowSpinButtons="True">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </tlk:RadNumericTextBox>
                                <asp:Label ID="lblStatus" runat="server" Font-Size="Large" CssClass="spancustomstatus"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <%-- <fieldset>
                        <legend>
                            <%# Translate("CM_CTRLPROCESS_PARAMETER_INFO")%>
                        </legend>--%>
                    <table style="text-align:left; margin-left:20px";>
                        <tr>
                            <td class="item-head" colspan="6">
                                <%# Translate("CM_CTRLPROCESS_PARAMETER_INFO")%>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:PlaceHolder ID="myPlaceHoder" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                    </table>
                    <%--      </fieldset>--%>
                </div>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar3" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane runat="server" ID="RadPane2" Width="100%" Height="50%" Scrolling="None">
        <tlk:RadGrid runat="server" ID="rgLoadData" AllowPaging="True" GridLines="None" AutoGenerateColumns="False"
            AllowFilteringByColumn="true" ShowStatusBar="true" AllowMultiRowSelection="True" EnableHeaderContextMenu="true"
            CellSpacing="0" Height="98%" AllowSorting="True" PageSize="50" AllowMultiRowEdit="true" OnDetailTableDataBind="rgLoadData_DetailTableDataBind">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <ClientSettings EnableRowHoverStyle="true" Scrolling-AllowScroll="true" Scrolling-SaveScrollPosition="true"
                Scrolling-UseStaticHeaders="true">
                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" >
                </Scrolling>
                <Selecting AllowRowSelect="true" />
                <Resizing AllowColumnResize="true" />
            </ClientSettings>
            <MasterTableView AllowMultiColumnSorting="True" ShowHeadersWhenNoRecords="true"
                ViewStateMode="Enabled" CommandItemDisplay="None" EditMode="InPlace" DataKeyNames="EMPLOYEE_CODE">
                <DetailTables>
                    <tlk:GridTableView  Name="rgLoadDataDetails" Width="100%" AllowPaging="false" DataKeyNames="EMPLOYEE_CODE_DT">
                    <ParentTableRelation>
                      <tlk:GridRelationFields DetailKeyField="EMPLOYEE_CODE_DT" MasterKeyField="EMPLOYEE_CODE" />  
                    </ParentTableRelation> 
                        <Columns>
                        </Columns>
                    </tlk:GridTableView>
                </DetailTables>
                <Columns>
                </Columns>
                     <HeaderStyle Width="350px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:Timer ID="TimerRequest" runat="server" Interval="5000" Enabled="False" EnableViewState="True">
</asp:Timer>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Height="300px"
            Width="500px" AutoSize="true" OnClientBeforeClose="OnClientBeforeClose" OnClientClose="popupclose"
            EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="false"
            Title="<%$ Translate: CM_CTRLPROCESS_LOG_INFO%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:HiddenField runat="server" ID="hidRequestID" Value="0" />
<script type="text/javascript">

    var enableAjax = true;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "SEARCH") {
            enableAjax = false;
        } else if (item.get_commandName() == "PRINT") {
            OpenLog(sender, args);
            args.set_cancel(true);
        } else if (item.get_commandName() == "EXPORT" || item.get_commandName() == "IMPORT") {
            var rows = $find('<%= rgLoadData.ClientID %>').get_masterTableView().get_dataItems().length;
            if (rows == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            enableAjax = false;
        }
    }

    function OpenLog(sender, eventArgs) {
        //var requestID = document.getElementById("<%= hidRequestID.ClientID %>").value;
        var requestID = document.getElementById("<%= lblRequest.ClientID %>").innerText;
        var oWindow = radopen('Dialog.aspx?mid=Common&fid=ctrlRESULT&noscroll=1&requestID=' + requestID + '&typeLog=1', "rwPopup");
        /*
        //        var pos = $("html").offset();
        //        oWindow.moveTo(pos.left, pos.top);
        //        oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        return 0;
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

    function popupclose(oWnd, args) {
        //window.location.reload();
    }


    function OnClientBeforeClose(sender, eventArgs) {
        if (!confirm("Bạn có muốn đóng màn hình không?")) {
            //if cancel is clicked prevent the window from closing
            args.set_cancel(true);
        }
    }

    function OnClientClose(oWnd, args) {
        oWnd = $find('<%#popupId %>');
        oWnd.setSize(screen.width - 250, screen.height - 300);
        oWnd.remove_close(OnClientClose);
        var arg = args.get_argument();
        if (arg) {
            postBack(arg);
        }
    }
   
</script>
