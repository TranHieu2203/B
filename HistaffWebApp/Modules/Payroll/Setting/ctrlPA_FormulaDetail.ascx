<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_FormulaDetail.ascx.vb"
    Inherits="Payroll.ctrlPA_FormulaDetail" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitterMain" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="RadPaneLeft" runat="server" MinWidth="300" Width="800px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitterLeft" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPaneGrid" runat="server" Scrolling="None" Width="100%">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,COL_INDEX,NAME_VN,COL_NAME,FORMULER">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderStyle-Width="50px" HeaderText="<%$ Translate: Thứ tự %>" DataField="COL_INDEX" SortExpression="COL_INDEX"
                                UniqueName="COL_INDEX" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderStyle-Width="150px" HeaderText="<%$ Translate: Tên danh mục lương %>"
                                DataField="NAME_VN" SortExpression="NAME_VN" UniqueName="NAME_VN" />
                            <tlk:GridBoundColumn HeaderStyle-Width="180px" HeaderText="<%$ Translate: Tên Trường dữ liệu %>"
                                DataField="COL_NAME" SortExpression="COL_NAME" UniqueName="COL_NAME" />                            
                            <tlk:GridBoundColumn Visible="true" HeaderStyle-Width="300px" HeaderText="<%$ Translate: Công thức lương %>"
                                DataField="FORMULER" SortExpression="FORMULER" UniqueName="FORMULER" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <ClientEvents OnRowClick="RowClick" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPaneRight" runat="server" Height="100%" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitterRight" runat="server" Orientation="Horizontal">
            <tlk:RadPane ID="RadPaneRightTop" runat="server" Height="100%" Scrolling="None">
                <tlk:RadSplitter ID="RadSplitter1" runat="server" Orientation="Vertical">
                    <tlk:RadPane ID="RadPane1" runat="server" Height="100%" Width="30%">
                        <tlk:RadTreeView runat="server" ID="ctrlCalculation" Height="100%" OnClientNodeClicking="onNodeClicking_match" Scrolling="True">
                        </tlk:RadTreeView>
                    </tlk:RadPane>
                    <tlk:RadPane ID="RadPane6" runat="server" Height="100%" >
                        <tlk:RadTreeView runat="server" CausesValidation="false" ID="ctrlListSalary" OnClientNodeClicking="onNodeClicking" Scrolling="True">
                        </tlk:RadTreeView>
                    </tlk:RadPane>
                </tlk:RadSplitter>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneRightMid" runat="server" Height="55px" Scrolling="None" Width="100%">
                <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" />          
                <%# Translate("Thứ tự ")%> <tlk:RadNumericTextBox runat="server" ID="rnmIndex" Width="50px" SkinID="Number"></tlk:RadNumericTextBox>      
            </tlk:RadPane>
            <tlk:RadPane ID="RadPaneRightBotton" runat="server" Scrolling="None" Width="100%" Height="200px">
                 <tlk:RadTextBox runat="server" ID="txtFormula" Width="100%" Height="200px" TextMode="MultiLine" SkinID="Textbox9999"></tlk:RadTextBox>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var pos = 0;
        var splitterID = 'ctl00_MainContent_ctrlPA_FormulaDetail_RadSplitterLeft';
        var pane1ID = 'RAD_SPLITTER_PANE_TR_ctl00_MainContent_ctrlPA_FormulaDetail_RadPaneGrid';
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
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }
        function onNodeClicking(sender, args) {
            var val_tv = args.get_node().get_value();
            var item_txt = $find('<%= txtFormula.ClientID %>')
            item_txt.set_value(item_txt.get_value() + 'NVL(' + val_tv + ',0)');
        }

         function onNodeClicking_match(sender, args) {
            var val_tv = args.get_node().get_value();
            var item_txt = $find('<%= txtFormula.ClientID %>')
            item_txt.set_value(item_txt.get_value() + val_tv);
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function RowClick(sender, eventArgs) {
            var grid = sender;
            var MasterTable = grid.get_masterTableView();
            var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var cell = MasterTable.getCellByColumnUniqueName(row, "FORMULER");
            var item_txt = $find('<%= txtFormula.ClientID %>')
            var a = document.createElement('a');
            a.innerHTML = cell.innerHTML;
            item_txt.set_value(a.textContent);

            $find('<%= rnmIndex.ClientID %>').set_value(MasterTable.getCellByColumnUniqueName(row, "COL_INDEX").innerHTML);
        };

    </script>
</tlk:RadCodeBlock>
