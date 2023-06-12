<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_CommendCollect.ascx.vb"
    Inherits="Profile.ctrlHU_CommendCollect" %>
<%@ Import Namespace="Common" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="110px" Scrolling="None">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnFind">
                    <%--<div>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Năm")%>
                                </td>
                                <td>
                                    <tlk:RadNumericTextBox MinValue="1900" MaxValue="9999" ID="txtYear" Width="50px"
                                        runat="server" AutoPostBack="true">
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                    </tlk:RadNumericTextBox>
                                </td>
                                <td class="lb">
                                    <%# Translate("Tổng hợp")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboCommendType" runat="server">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <div>
                        <fieldset style="width: auto; height: auto">
                            <legend>
                                <%# Translate("Thông tin tìm kiếm")%>
                            </legend>
                            <table width="100%" class="td-padding">
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Tổng hợp")%>
                                    </td>
                                    <td>
                                        <tlk:RadComboBox ID="cboCommendType" runat="server" AutoPostBack="true">
                                        </tlk:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="lb">
                                        <%# Translate("Ngày xét")%>
                                    </td>
                                    <td>
                                        <tlk:RadComboBox ID="rcbbCommendDate" runat="server" AutoPostBack="true">
                                        </tlk:RadComboBox>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td class="lb">
                                        <%# Translate("Nhân viên")%>
                                    </td>
                                    <td>
                                        <tlk:RadTextBox ID="txtEmployee" runat="server">
                                        </tlk:RadTextBox>
                                    </td>
                                </tr>--%>                                
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="chkIsAll" runat="server" Text="<%$ Translate: Hiển thị tất cả %>" />
                                    </td>
                                    <td>
                                        <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFind" TabIndex="8" runat="server"
                                            Text="<%$ Translate: Tìm kiếm %>" ValidationGroup="Search" SkinID="ButtonFind"
                                            OnClick="btnFIND_Click">
                                        </tlk:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar3" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" AutoPostBack="true" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid runat="server" ID="rgLoadData" AllowPaging="True" ShowGroupPanel="true"
                    EnableViewState="false" GridLines="None" AutoGenerateColumns="False" AllowFilteringByColumn="true"
                    ShowStatusBar="true" AllowMultiRowSelection="True" CellSpacing="0" Height="100%"
                    AllowSorting="True">
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <ClientSettings AllowColumnsReorder="true" AllowDragToGroup="true" ReorderColumnsOnClient="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Resizing AllowColumnResize="true" EnableRealTimeResize="true" />
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
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false" OnClientClose="popupclose">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
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
            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_CommendCollectDetail&group=Business&EmpCode=' + empCode + '&OrgID=' + orgID, "rwPopup");
            var pos = $("html").offset();
            oWindow.setSize($(window).width(), 500);
            oWindow.center();
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
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />