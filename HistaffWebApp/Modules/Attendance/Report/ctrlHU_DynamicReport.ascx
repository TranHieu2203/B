<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_DynamicReport.ascx.vb"
    Inherits="Profile.ctrlHU_DynamicReport" %>
<tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <tlk:RadDockZone runat="server" ID="RadDockZone4" CssClass="DockPanel" BorderWidth="0"
            BorderColor="Transparent" BorderStyle="None">
            <tlk:RadDock runat="server" ID="RadDock4" DefaultCommands="None" EnableDrag="false"
                AutoPostBack="true" Height="100%" Title="<%$ Translate: 1. Chọn phòng ban cần xuất báo cáo %>">
                <ContentTemplate>
                    <Common:ctrlOrganization ID="ctrlOrganization" runat="server" ShowDissolve="false" />
                </ContentTemplate>
            </tlk:RadDock>
        </tlk:RadDockZone>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane runat="server" ID="RadPane7" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter4" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane runat="server" ID="RadPane8" Height="35px" Scrolling="None">
                <tlk:RadToolBar runat="server" ID="tbarExport">
                </tlk:RadToolBar>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Width="400px" Height="280px" Scrolling="None"
                BorderStyle="None">
                <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                    <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                        <table class="table-form">
                            <tr>
                                <td>
                                    <%#Translate("Chọn báo cáo động: ")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox ID="cboReport" Width="290px" runat="server" AutoPostBack="true">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </tlk:RadPane>
                    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
                            <tlk:RadPane ID="RadPane6" runat="server" Width="200px" Scrolling="None" BorderStyle="None">
                                <tlk:RadDockZone runat="server" ID="RadDockZone2" CssClass="DockPanelNoScroll" BorderWidth="0"
                                    BorderColor="Transparent" BorderStyle="None">
                                    <tlk:RadDock runat="server" ID="RadDock3" DefaultCommands="None" EnableDrag="false"
                                        Height="100%" Title="<%$ Translate: 2. Danh sách cột dữ liệu %>">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
                                                <table style="margin-top: 3px; margin-bottom: 3px">
                                                    <tr>
                                                        <td>
                                                            <tlk:RadTextBox ID="txtFilter" EmptyMessage="Tìm kiếm" Width="130px" runat="server">
                                                            </tlk:RadTextBox>
                                                        </td>
                                                        <td>
                                                            <tlk:RadButton ID="Button1" runat="server" OnClick="Button1_Click" SkinID="ButtonView">
                                                            </tlk:RadButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <tlk:RadListBox ID="lstColumns" runat="server" Width="100%" Height="84%" SelectionMode="Multiple"
                                                AllowTransfer="true" AllowReorder="true" TransferToID="lstColumnSelected" AutoPostBackOnTransfer="false"
                                                EnableDragAndDrop="true">
                                                <Localization AllToRight="<%$ Translate: Chuyển tất cả qua phải %>" AllToLeft="<%$ Translate: Chuyển tất cả qua trái %>"
                                                    ToLeft="<%$ Translate: Chuyển qua trái %>" ToRight="<%$ Translate: Chuyển qua phải %>" />
                                            </tlk:RadListBox>
                                        </ContentTemplate>
                                    </tlk:RadDock>
                                </tlk:RadDockZone>
                            </tlk:RadPane>
                            <tlk:RadSplitBar ID="RadSplitBar2" runat="server" CollapseMode="Forward">
                            </tlk:RadSplitBar>
                            <tlk:RadPane ID="RadPane5" runat="server" Width="200px" Scrolling="None" BorderStyle="None">
                                <tlk:RadDockZone runat="server" ID="RadDockZone1" CssClass="DockPanelNoScroll" BorderWidth="0"
                                    BorderColor="Transparent" BorderStyle="None">
                                    <tlk:RadDock runat="server" ID="RadDock1" DefaultCommands="None" EnableDrag="false"
                                        Height="100%" Title="<%$ Translate: 3. Cột dữ liệu chọn %>">
                                        <ContentTemplate>
                                            <tlk:RadListBox ID="lstColumnSelected" runat="server" Width="100%" Height="98%" SelectionMode="Multiple"
                                                AutoPostBackOnTransfer="false" EnableDragAndDrop="true" AllowReorder="true" AutoPostBackOnReorder="false"
                                                Style="margin-top: 3px;">
                                                <Localization MoveDown="<%$ Translate: Chuyển xuống dưới %>" MoveUp="<%$ Translate: Chuyển lên trên %>" />
                                            </tlk:RadListBox>
                                        </ContentTemplate>
                                    </tlk:RadDock>
                                </tlk:RadDockZone>
                            </tlk:RadPane>
                            <tlk:RadSplitBar ID="RadSplitBar3" runat="server" CollapseMode="Forward">
                            </tlk:RadSplitBar>
                            <tlk:RadPane ID="RadPane9" runat="server" Scrolling="None" BorderStyle="None">
                                <tlk:RadDockZone runat="server" ID="RadDockZone3" CssClass="DockPanel" BorderWidth="0"
                                    BorderColor="Transparent" BorderStyle="None">
                                    <tlk:RadDock runat="server" ID="RadDock2" DefaultCommands="None" EnableDrag="false"
                                        Height="100%" Title="<%$ Translate: 4. Điều kiện %>">
                                        <ContentTemplate>
                                            <div style="padding: 0px 10px 10px 10px;">
                                                <tlk:RadFilter ID="RadFilter1" runat="server" ShowApplyButton="false" EnableViewState="true"
                                                    ViewStateMode="Enabled" AddExpressionToolTip="<%$ Translate: Thêm điều kiện %>"
                                                    AddGroupToolTip="<%$ Translate: Thêm nhóm điều kiện %>" RemoveToolTip="<%$ Translate: Xóa điều kiện %>">
                                                    <Localization FilterFunctionBetween="<%$ Translate: Giữa %>" FilterFunctionContains="<%$ Translate: Chứa %>"
                                                        FilterFunctionDoesNotContain="<%$ Translate: Không chứa %>" FilterFunctionEndsWith="<%$ Translate: Kết thúc bằng %>"
                                                        FilterFunctionEqualTo="<%$ Translate: Bằng %>" FilterFunctionGreaterThan="<%$ Translate: Lớn hơn %>"
                                                        FilterFunctionGreaterThanOrEqualTo="<%$ Translate: Lớn hơn hoặc bằng %>" FilterFunctionIsEmpty="<%$ Translate: Dữ liệu trống %>"
                                                        FilterFunctionLessThan="<%$ Translate: Nhỏ hơn %>" FilterFunctionLessThanOrEqualTo="<%$ Translate: Nhỏ hơn hoặc bằng %>"
                                                        FilterFunctionNotBetween="<%$ Translate: Không giữa %>" FilterFunctionNotEqualTo="<%$ Translate: Không bằng %>"
                                                        FilterFunctionNotIsEmpty="<%$ Translate: Dữ liệu không trống %>" FilterFunctionStartsWith="<%$ Translate: Bắt đầu bằng %>"
                                                        FilterFunctionIsNull="<%$ Translate: Dữ liệu trống %>" FilterFunctionNotIsNull="<%$ Translate: Dữ liệu không trống %>"
                                                        GroupOperationAnd="<%$ Translate: Và %>" GroupOperationNotAnd="<%$ Translate: Và không %>"
                                                        GroupOperationNotOr="<%$ Translate: Hoặc không %>" GroupOperationOr="<%$ Translate: Hoặc %>" />
                                                </tlk:RadFilter>
                                            </div>
                                        </ContentTemplate>
                                    </tlk:RadDock>
                                </tlk:RadDockZone>
                            </tlk:RadPane>
                        </tlk:RadSplitter>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" BorderStyle="None">
                <tlk:RadSplitter ID="RadSplitter5" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                    <tlk:RadPane ID="RadPane11" runat="server" Scrolling="None" BorderStyle="None" Height="50%">
                        <tlk:RadSplitter ID="RadSplitter6" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
                            <tlk:RadPane runat="server" ID="RadPane12" Height="40px" Scrolling="None">
                                <table class="table-form">
                                    <tr>
                                        <td class="lb">
                                            <%# Translate("Tên Báo cáo")%>
                                        </td>
                                        <td>
                                            <tlk:RadTextBox ID="txtReportName" Width="330px" runat="server" CausesValidation="false">
                                            </tlk:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                            </tlk:RadPane>
                            <tlk:RadPane ID="RadPane10" runat="server" Scrolling="None" BorderStyle="None" Height="100%">
                                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%" SkinID="GridNotPaging">
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,VIEW_ID,REPORT_NAME,CONDITION">
                                        <Columns>
                                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                            </tlk:GridClientSelectColumn>
                                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên báo cáo %>" DataField="REPORT_NAME"
                                                SortExpression="REPORT_NAME" UniqueName="REPORT_NAME" />
                                        </Columns>
                                    </MasterTableView>
                                </tlk:RadGrid>
                            </tlk:RadPane>
                        </tlk:RadSplitter>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindow ID="rwReport" runat="server" VisibleStatusbar="false" Modal="true"
    Width="900px" Height="500px" EnableShadow="false" Behaviors="Maximize,Close"
    ShowContentDuringLoad="false" OnClientClose="OnClientClose">
</tlk:RadWindow>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        function CheckAllColumn(checked) {
            var listbox = $find("<%# lstColumns.ClientID %>");
            var items = listbox.get_items();
            items.forEach(function (item) {
                item.set_checked(checked);
            });
        }
        function CheckAllColumnSelected(checked) {
            var listbox = $find("<%# lstColumnSelected.ClientID %>");
            var items = listbox.get_items();
            items.forEach(function (item) {
                item.set_checked(checked);
            });
        }

        function OnClientClose(oWnd, args) {
            postBack("Close");
        }
        function postBack(arg) {
            var ajaxManager = $find("<%# AjaxManagerId %>");
            ajaxManager.ajaxRequest(arg); //Making ajax request with the argument
        }

        function FilterMenuShowing(sender, args) {
            var filter = $find("<%#RadFilter1.ClientID %>");
            var currentExpandedItem = sender.get_attributes()._data.ItemHierarchyIndex;
            var fieldName = filter._expressionItems[currentExpandedItem];
            var allFields = filter._dataFields;
            var dataType = null;
            for (var i = 0, j = allFields.length; i < j; i++) {
                if (allFields[i].FieldName == fieldName) {
                    dataType = allFields[i].DataType;
                    break;
                }
            }
            sender.findItemByValue("Between").set_visible(false);
            sender.findItemByValue("NotBetween").set_visible(false);
            if (dataType == "System.Int32") {
                sender.findItemByValue("StartsWith").set_visible(false);
                sender.findItemByValue("EndsWith").set_visible(false);
                sender.findItemByValue("Contains").set_visible(false);
                sender.findItemByValue("DoesNotContain").set_visible(false);
                sender.findItemByValue("IsNull").set_visible(false);
                sender.findItemByValue("NotIsNull").set_visible(false);
            } else if (dataType == "System.String") {
                sender.findItemByValue("IsNull").set_visible(false);
                sender.findItemByValue("NotIsNull").set_visible(false);
            } else if (dataType == "System.DateTime") {
                sender.findItemByValue("StartsWith").set_visible(false);
                sender.findItemByValue("EndsWith").set_visible(false);
                sender.findItemByValue("Contains").set_visible(false);
                sender.findItemByValue("DoesNotContain").set_visible(false);
            }
        }

        function pageLoad() {
            var filter = $find("<%#RadFilter1.ClientID %>");
            var menu = filter.get_contextMenu();
            menu.add_showing(FilterMenuShowing);
        }

        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT" || item.get_commandName() == "APPROVE") {
                enableAjax = false;
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        } 
    </script>
</tlk:RadScriptBlock>
