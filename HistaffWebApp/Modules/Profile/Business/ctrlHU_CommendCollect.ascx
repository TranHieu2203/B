<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CommendCollect.ascx.vb"
    Inherits="Profile.ctrlHU_CommendCollect" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Tổng hợp")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboCommendType" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Ngày xét")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="rcbbCommendDate" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsAll" runat="server" Text="<%$ Translate: Hiển thị tất cả %>" />
                        </td>
                    <td>
                            <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFind" TabIndex="8" runat="server"
                                Text="<%$ Translate: Tìm %>" ValidationGroup="Search" SkinID="ButtonFind"
                                OnClick="btnFIND_Click">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid runat="server" ID="rgLoadData" AllowPaging="True" EnableViewState="false"
                    GridLines="None" AutoGenerateColumns="False" AllowFilteringByColumn="true" ShowStatusBar="true"
                    AllowMultiRowSelection="True" CellSpacing="0" Height="100%" AllowSorting="True"
                    PageSize="50">
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <ClientSettings AllowColumnsReorder="true" AllowDragToGroup="true" ReorderColumnsOnClient="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <GroupingSettings ShowUnGroupButton="true"></GroupingSettings>
                    <MasterTableView ClientDataKeyNames="EMPLOYEE_CODE,ORG_ID">
                        <ColumnGroups>
                            <%--<tlk:GridColumnGroup HeaderText="Thông tin nhân viên" Name="EmployeeInfor" HeaderStyle-HorizontalAlign="Center">
                            </tlk:GridColumnGroup>--%>
                        </ColumnGroups>
                        <Columns>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" OnClientClose="popupclose"
            Width="1000" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('ctl00_MainContent_ctrlHU_CommendCollect_RadSplitter3');
        }

        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OpenEdit() {
            var bCheck = $find('<%= rgLoadData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            var empCode = $find('<%= rgLoadData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_CODE');
            var orgID = $find('<%= rgLoadData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ORG_ID');
            if (empCode == "") {
                return;
            }
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CommendCollectDetail&group=Business&EmpCode=' + empCode + '&OrgID=' + orgID, "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize($(window).width(), 500);
            oWindow.center(); */
            return 2;
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = OpenEdit();
            if (bCheck == 0) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }

        }

        function popupclose(sender, args) {

        }

    </script>
</tlk:RadCodeBlock>
