<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlIns_DBInsCeiling.ascx.vb"
    Inherits="Insurance.ctrlIns_DBInsCeiling" %>
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
        <tlk:RadGrid PageSize="50" ID="rgGridDataRate" runat="server" Height="100%" AllowFilteringByColumn="false">
            <MasterTableView DataKeyNames="NAME" ClientDataKeyNames="NAME"  ShowHeader="false" TableLayout="Fixed">
                <Columns>
                    <tlk:GridBoundColumn HeaderText="" DataField="NAME" UniqueName="NAME" SortExpression="NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="" DataFormatString="{0:N0}   " DataField="VALUE"
                        UniqueName="VALUE" SortExpression="VALUE">
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle"/>
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

        function GetRadWindow() {
            var oWindow = null;

            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;

            return oWindow;
        }

    </script>
</tlk:RadScriptBlock>
