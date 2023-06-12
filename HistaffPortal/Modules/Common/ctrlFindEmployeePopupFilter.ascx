<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlFindEmployeePopupFilter.ascx.vb"
    Inherits="Common.ctrlFindEmployeePopupFilter" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" BorderStyle="None">
        <tlk:RadFilter ID="RadFilter1" runat="server" ShowApplyButton="false" AddExpressionToolTip="<%$ Translate: Thêm điều kiện %>"
            AddGroupToolTip="<%$ Translate: Thêm nhóm điều kiện %>" RemoveToolTip="<%$ Translate: Xóa điều kiện %>">
            <Localization FilterFunctionBetween="<%$ Translate: Giữa %>" FilterFunctionContains="<%$ Translate: Chứa %>"
                FilterFunctionDoesNotContain="<%$ Translate: Không chứa %>" FilterFunctionEndsWith="<%$ Translate: Kết thúc bằng %>"
                FilterFunctionEqualTo="<%$ Translate: = %>" FilterFunctionGreaterThan="<%$ Translate: > %>"
                FilterFunctionGreaterThanOrEqualTo="<%$ Translate: >= %>" FilterFunctionIsEmpty="<%$ Translate: Dữ liệu trống %>"
                FilterFunctionLessThan="<%$ Translate: < %>" FilterFunctionLessThanOrEqualTo="<%$ Translate: <= %>"
                FilterFunctionNotBetween="<%$ Translate: Không giữa %>" FilterFunctionNotEqualTo="<%$ Translate: <> %>"
                FilterFunctionNotIsEmpty="<%$ Translate: Dữ liệu không trống %>" FilterFunctionStartsWith="<%$ Translate: Bắt đầu bằng %>"
                FilterFunctionIsNull="<%$ Translate: Dữ liệu trống %>" FilterFunctionNotIsNull="<%$ Translate: Dữ liệu không trống %>"
                GroupOperationAnd="<%$ Translate: Và %>" GroupOperationNotAnd="<%$ Translate: Và không %>"
                GroupOperationNotOr="<%$ Translate: Hoặc không %>" GroupOperationOr="<%$ Translate: Hoặc %>" />
        </tlk:RadFilter>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" Height="35px" runat="server" BorderStyle="None">
        <div style="padding-top:10px;padding-right:10px;vertical-align:middle; text-align:right;">
            <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Chọn %>"
                Font-Bold="true" CausesValidation="false">
            </tlk:RadButton>
            <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                Font-Bold="true" CausesValidation="false" OnClientClicked="btnCancelClick">
            </tlk:RadButton>
        </div>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function ChangeWindowSize() {
            var radwin = GetRadWindow();
            radwin.autoSize();
        }

        function FilterMenuShowing(sender, args) {
            var filter = $find("<%=RadFilter1.ClientID %>");
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
            if (dataType == "System.Int32") {
                sender.findItemByValue("StartsWith").set_visible(false);
                sender.findItemByValue("EndsWith").set_visible(false);
                sender.findItemByValue("Contains").set_visible(false);
                sender.findItemByValue("DoesNotContain").set_visible(false);
                //                    sender.findItemByValue("GreaterThan").set_visible(true);
                //                    sender.findItemByValue("GreaterThanOrEqualTo").set_visible(true);
                //                    sender.findItemByValue("LessThan").set_visible(true);
                //                    sender.findItemByValue("LessThanOrEqualTo").set_visible(true);
                sender.findItemByValue("Between").set_visible(false);
                sender.findItemByValue("NotBetween").set_visible(false);
                sender.findItemByValue("IsNull").set_visible(false);
                sender.findItemByValue("NotIsNull").set_visible(false);
            } else if (dataType == "System.String") {
                //                    sender.findItemByValue("StartsWith").set_visible(true);
                //                    sender.findItemByValue("EndsWith").set_visible(true);
                //                    sender.findItemByValue("Contains").set_visible(true);
                //                    sender.findItemByValue("DoesNotContain").set_visible(true);
                //sender.findItemByValue("GreaterThan").set_visible(true);
                //sender.findItemByValue("GreaterThanOrEqualTo").set_visible(true);
                //sender.findItemByValue("LessThan").set_visible(true);
                //sender.findItemByValue("LessThanOrEqualTo").set_visible(true);
                sender.findItemByValue("Between").set_visible(false);
                sender.findItemByValue("NotBetween").set_visible(false);
                sender.findItemByValue("IsNull").set_visible(false);
                sender.findItemByValue("NotIsNull").set_visible(false);
            } else if (dataType == "System.DateTime") {
                sender.findItemByValue("StartsWith").set_visible(false);
                sender.findItemByValue("EndsWith").set_visible(false);
                sender.findItemByValue("Contains").set_visible(false);
                sender.findItemByValue("DoesNotContain").set_visible(false);
                //                    sender.findItemByValue("GreaterThan").set_visible(true);
                //                    sender.findItemByValue("GreaterThanOrEqualTo").set_visible(true);
                //                    sender.findItemByValue("LessThan").set_visible(true);
                //                    sender.findItemByValue("LessThanOrEqualTo").set_visible(true);
                sender.findItemByValue("Between").set_visible(false);
                sender.findItemByValue("NotBetween").set_visible(false);
            }
        }

        function btnYesClick(args) {
            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();
            //Close the RadWindow and send the argument to the parent page
            oWnd.close(args);
        }

        function btnCancelClick() {
            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();
            //Close the RadWindow and send the argument to the parent page
            oWnd.close('Cancel');
        }

        function pageLoad() {
            var filter = $find("<%=RadFilter1.ClientID %>");
            var menu = filter.get_contextMenu();
            menu.add_showing(FilterMenuShowing);
        }
    </script>
</tlk:RadScriptBlock>
