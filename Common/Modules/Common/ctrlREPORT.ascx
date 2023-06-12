<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlREPORT.ascx.vb"
    Inherits="Common.ctrlREPORT" %>
<%@ Register Src="ctrlOrganization.ascx" TagName="ctrlOrganization" TagPrefix="Common" %>
<tlk:RadSplitter runat="server" ID="RadSplitter1" Width="100%" Height="100%">
    <tlk:RadPane runat="server" ID="RadPane5" MinWidth="260" Width="340px" Height="100%" Style="overflow: auto !important">
        <div style="margin-top:10px">
            <b><%# Translate("- Danh sách báo cáo")%></b>
            <hr />
                <tlk:RadTreeView ID="treeOtherListType" runat="server" CausesValidation="false">
                </tlk:RadTreeView>
        </div>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane runat="server" ID="RadPane1" MinWidth="260" Width="370px" Height="100%">
        <div style="margin-top:10px">
            <b><%# Translate("- Sơ đồ tổ chức")%></b>
            <hr />
                <Common:ctrlOrganization runat="server" ID="ctrlOrg" />
        </div>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane runat="server" ID="RadPane2" Scrolling="None">
        <tlk:RadSplitter runat="server" ID="RadSplitter2" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Width="100%" Height="330px">
                <tlk:RadToolBar ID="rtbMain" runat="server">
                </tlk:RadToolBar>
                <br />
                <table style="text-align: center; width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblProgram" runat="server" Font-Size="Large" Font-Bold="true">&nbsp;</asp:Label>
                        </td>
                        <td style="width: 100px">
                            <asp:Label ID="lblRequest" runat="server" Font-Size="Large" Font-Bold="true" Visible="false">&nbsp;</asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="margin-top: 10px; margin-bottom: 10px">
                    <table>
                        <tr>
                            <td class="lb" style="padding-right: 5px; padding-left: 20px">
                                <asp:Label ID="lblScanTime" runat="server" Visible="false"> Text="<%# Translate("CM_CTRLREPORT_SCAN_TIME")%>"</asp:Label>
                            </td>
                            <td>
                                <tlk:RadNumericTextBox runat="server" ID="RadNumTB" MinValue="1" MaxValue="30" Width="40px"
                                    Visible="false" Value="5" MaxLength="2" AutoPostBack="true" ShowSpinButtons="True">
                                    <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                </tlk:RadNumericTextBox>
                            </td>
                            <td class="lb" style="padding-left: 30px">
                                <asp:Label ID="lblStatus" runat="server" Font-Size="Large" Font-Bold="true" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb" style="padding-right: 5px; padding-left: 20px; padding-top: 10px">
                                <%# Translate("CM_CTRLPROGRAMS_TEMPLATES")%>&nbsp;<span class="lbReq">*</span>
                            </td>
                            <td colspan="2" style="padding-top: 10px">
                                <tlk:RadComboBox ID="rcbbTemplates" DataValueField="ID" DataTextField="NAME" runat="server"
                                    SkinID="Number" AutoPostBack="True" CausesValidation="false" Width="400px" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATES %>">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <fieldset>
                        <legend>
                            <%# Translate("CM_CTRLREPORT_PARAMETER_INFO")%>
                        </legend>
                        <table style="margin: auto">
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="myPlaceHoder" runat="server"></asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar3" runat="server" CollapseMode="Forward">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPaneEmployee" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgEmployee" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                    Height="100%" AllowSorting="True" AllowMultiRowSelection="false" CellSpacing="0"
                    PageSize="10000" GridLines="None">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" EditMode="InPlace" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE"
                                HeaderStyle-Width="50px" SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="FULLNAME_VN"
                                HeaderStyle-Width="100px" SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                HeaderStyle-Width="150px" UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp chức danh %>" DataField="TITLE_NAME_LEVEL"
                                HeaderStyle-Width="120px" SortExpression="TITLE_NAME_LEVEL" UniqueName="TITLE_NAME_LEVEL" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" HeaderStyle-Width="120px" UniqueName="TITLE_NAME" />
                        </Columns>
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:Timer ID="TimerRequest" runat="server" Interval="5000" Enabled="False" EnableViewState="True">
</asp:Timer>
<asp:HiddenField runat="server" ID="hidRequestID" Value="0" />
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Height="300px"
            Width="500px" AutoSize="true" OnClientBeforeClose="OnClientBeforeClose" OnClientClose="popupclose"
            EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="false"
            Title="<%$ Translate: CM_CTRLREPORT_LOG_INFO %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<script type="text/javascript">
    var enableAjax = true;
    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "CALCULATE" || item.get_commandName() == "SEARCH" || item.get_commandName() == "EXPORT" || item.get_commandName() == "IMPORT") {
            enableAjax = false;
        }
        if (item.get_commandName() == "PRINT") {
            OpenLog(sender, args);
            args.set_cancel(true);
        }
    }

    function changeEnableAjax() {
        enableAjax = false;
    }

    function OpenLog(sender, eventArgs) {
        var requestID = document.getElementById("<%= hidRequestID.ClientID %>").value;
        var oWindow = radopen('Dialog.aspx?mid=Common&fid=ctrlRESULT&noscroll=1&requestID=' + requestID + '&typeLog=1', "rwPopup");
        //        var pos = $("html").offset();
        //        oWindow.moveTo(pos.left, pos.top);
        //        oWindow.setSize($(window).width(), $(window).height());
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
