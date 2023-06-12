<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlIns_DBInsRate.ascx.vb"
    Inherits="Insurance.ctrlIns_DBInsRate" %>
<style type="text/css">
    .lblInfo
    {
        font-weight: bold;
        color: #2196f3;
    }
</style>
<link href="../../../Styles/font-awesome.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal"
    SkinID="Demo">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize="50" ID="rgGridDataRate" runat="server" Height="100%">
            <MasterTableView DataKeyNames="TYPE_NAME" ClientDataKeyNames="TYPE_NAME">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại bảo hiểm %>" DataField="TYPE_NAME"
                        UniqueName="TYPE_NAME" SortExpression="TYPE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Công ty đóng %>" DataFormatString="{0:N0} %"
                        DataField="COMPANY_RATE" UniqueName="COMPANY_RATE" SortExpression="COMPANY_RATE">
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Nhân viên đóng %>" DataFormatString="{0:N0} %"
                        DataField="EMPLOYEE_RATE" UniqueName="EMPLOYEE_RATE" SortExpression="EMPLOYEE_RATE">
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng cộng %>" DataFormatString="{0:N0} %"
                        DataField="TOTAL_RATE" UniqueName="TOTAL_RATE" SortExpression="TOTAL_RATE">
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                    </tlk:GridNumericColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function RaiseCommand(sender, eventArgs) {
            var item = eventArgs.get_commandName();
            if (item == "ExportToExcel") {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            //            eventArgs.set_enableAjax(enableAjax);
            //            enableAjax = true;
        }

        //        function GetRadWindow() {
        //            var oWindow = null;

        //            if (window.radWindow)
        //                oWindow = window.radWindow;
        //            else if (window.frameElement.radWindow)
        //                oWindow = window.frameElement.radWindow;

        //            return oWindow;
        //        }

    </script>
</tlk:RadScriptBlock>
