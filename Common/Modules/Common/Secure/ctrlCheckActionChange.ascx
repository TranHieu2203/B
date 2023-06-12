<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlCheckActionChange.ascx.vb"
    Inherits="Common.ctrlCheckActionChange" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCheckAction" runat="server" OnClientButtonClicking="clientButtonClicking" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgActiocLog" runat="server" Height="100%">
            <MasterTableView>
                <Columns>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên trường dữ liệu %>" DataField="COL_NAME"
                        ReadOnly="true" SortExpression="COL_NAME" UniqueName="COL_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giá trị trước khi thao tác %>" DataField="OLD_VALUE"
                        SortExpression="OLD_VALUE" UniqueName="OLD_VALUE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Giá trị sau khi thao tác %>" DataField="NEW_VALUE"
                        SortExpression="NEW_VALUE" UniqueName="NEW_VALUE" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CANCEL') {
                getRadWindow().close(null);
                args.set_cancel(true);
            }
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }
    </script>
</tlk:RadCodeBlock>
