<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDynamicControl.ascx.vb"
    Inherits="Common.ctrlDynamicControl" %>
<style type="text/css">
    .RadGrid_Metro .rgRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgAltRow>td:first-child 
    {
        padding :0 !important;
    }
</style>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None">
        <tlk:RadToolBar ID="tbarApproveProcesss" runat="server" OnButtonClick=/>
        <asp:ValidationSummary ID="valSum" runat="server" />
        <asp:Table ID="tblControl" runat="server" CssClass="table-form">
            
        </asp:Table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgApproveProcess" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên quy trình %>" DataField="NAME"
                        SortExpression="NAME" UniqueName="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email thông báo %>" DataField="EMAIL"
                        SortExpression="EMAIL" UniqueName="EMAIL" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>

<script type="text/javascript">
    var enableAjax = true;
    var oldSize = 100;
    var defaultPage = 0;
    var iCheck = 0;

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
        registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlApproveProcess_RadSplitter3');
    }

    function OnClientButtonClicking(sender, args) {
        var item = args.get_item();
        if (item.get_commandName() == "SAVE") {
            // Nếu nhấn nút SAVE thì resize            
            ResizeSplitter();
        }
    }

    function onRequestStart(sender, eventArgs) {
        eventArgs.set_enableAjax(enableAjax);
        enableAjax = true;
    }

    function pageLoad(sender, args) {
        $(document).ready(function () {
            ResizeSplitterDefault();
        });
    }

    // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
    function ResizeSplitter() {
        setTimeout(function () {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            var validateHeight = $("#<%= valSum.ClientID%>").height();
            var ePager = $("#ctl00_MainContent_ctrlApproveProcess_rgApproveProcess_GridData");

            var height = pane.getContentElement().scrollHeight;

            if (height > oldSize) {
                space = height - validateHeight - oldSize - 20;
            }

            pane.set_height(height - space);

            if (validateHeight > 0) {
                iCheck = validateHeight;
                var re = defaultPage - (validateHeight + 11 + 13);
                ePager.css({ 'height': re + 'px', 'overflow': 'auto' });
            }
            else {
                iCheck = 0;
            }
        }, 200);
    }

    // Hàm khôi phục lại Size ban đầu cho Splitter
    function ResizeSplitterDefault() {
        var splitter = $find("<%= RadSplitter3.ClientID%>");
        var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
        var validateHeight = $("#<%= valSum.ClientID%>").height();
        var ePager = $("#ctl00_MainContent_ctrlApproveProcess_rgApproveProcess_GridData");

        defaultPage = ePager.height();

        if (oldSize == 0) {
            oldSize = pane.getContentElement().scrollHeight;
        } else {
            //            var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
            //            splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
            //            pane.set_height(oldSize);
            //            pane2.set_height(splitter.get_height() - oldSize - 1);

            if (iCheck != 0) {
                window.location.replace('/Default.aspx?mid=Common&fid=ctrlApproveProcess&group=ApproveProcess');
            }
        }
    }

</script>
