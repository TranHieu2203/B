<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListGroup.ascx.vb"
    Inherits="Common.ctrlListGroup" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
        <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" OnClientResized="PaneClientResized">
        <tlk:RadGrid PageSize="50" ID="rgGrid" runat="server" Height="100%" OnPreRender="rgGrid_PreRender" OnItemCreated="RadGrid1_ItemCreated">
            <MasterTableView DataKeyNames="ID,CODE,IS_HR_ADMIN">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="IS_HR_ADMIN" Visible="false" />
                    <tlk:GridBoundColumn DataField="IS_ADMIN" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhóm %>" DataField="CODE" UniqueName="CODE"
                        SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhóm %>" DataField="NAME" UniqueName="NAME"
                        SortExpression="NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE"
                        UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                   <tlk:GridCheckBoxColumn HeaderText="Nhóm PQ chức năng" DataField="IS_FUNCTION_PERMISSION" UniqueName="IS_FUNCTION_PERMISSION" AllowFiltering="false"
                        SortExpression="IS_FUNCTION_PERMISSION" HeaderStyle-Width="100px" />
                   <tlk:GridCheckBoxColumn HeaderText="Nhóm PQ SĐTC" DataField="IS_ORG_PERMISSION" UniqueName="IS_ORG_PERMISSION" AllowFiltering="false"
                        SortExpression="IS_ORG_PERMISSION" HeaderStyle-Width="100px" />
                </Columns>
            </MasterTableView>
            <ClientSettings>
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlListGroup_RadSplitter1';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlListGroup_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlListGroup_RadPane2';
        var validateID = 'MainContent_ctrlListGroup_ctrlListGroupNewEdit_valSum';
        var oldSize = $('#' + pane1ID).height();

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
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "SAVE") {
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGrid');
                } else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function PaneClientResized(sender, eventArgs) {
            var grid = $find("<%=rgGrid.ClientID %>");
            grid.repaint();
        }
    </script>
</tlk:RadScriptBlock>
