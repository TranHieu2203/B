<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlValueSet.ascx.vb"
    Inherits="Common.ctrlValueSet" %>
<%@ Import Namespace="Common" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Height="105px">
        <tlk:RadToolBar ID="rtbMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <div>
            <table class="table-form">
                <tr>
                    <td class="lb" style="padding-right: 10px">
                        <%# Translate("CM_CTRLVALUESET_VALUESET_NAME")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                    </td>
                    <td>
                        <tlk:RadTextBox ID="rtbValueSetName" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_NAME %>"
                            Width="100%" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rtbValueSetName"
                            ErrorMessage="<%$ Translate: CM_CTRLVALUESET_VALUESET_NAME_ERROR %>" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_NAME_ERROR %>" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="rtbValueSetName"
                            Display="Dynamic" ErrorMessage="<%$ Translate: CM_CTRLVALUESET_VALUESET_NAME_ERROR_EXIST %>"
                            ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_NAME_ERROR_EXIST %>" />
                    </td>
                    <td class="lb" style="padding-right: 10px">
                        <%# Translate("CM_CTRLVALUESET_VALUESET_TYPE")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                    </td>
                    <td>
                        <tlk:RadComboBox ID="rcbbVSType" runat="server" DataTextField="NAME" DataValueField="ID"
                            SkinID="Number" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_TYPE %>">
                        </tlk:RadComboBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rcbbVSType"
                                ErrorMessage="<%$ Translate: CM_CTRLVALUESET_VALUESET_TYPE_ERROR %>" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_TYPE_ERROR %>" />
                    </td>
                    <td class="lb" style="padding-right: 10px">
                        <%# Translate("CM_CTRLVALUESET_VALUESET_DATATYPE")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                    </td>
                    <td>
                        <tlk:RadComboBox ID="rcbbVS_DataType" runat="server" DataTextField="NAME" DataValueField="ID"
                            SkinID="Number" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_DATATYPE %>">
                        </tlk:RadComboBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rcbbVS_DataType"
                                ErrorMessage="<%$ Translate: CM_CTRLVALUESET_VALUESET_DATATYPE_ERROR %>" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_DATATYPE_ERROR %>" />
                    </td>
                    <td class="lb" style="padding-right: 10px">
                        <%# Translate("CM_CTRLVALUESET_VALUESET_CODE")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                    </td>
                    <td>
                        <tlk:RadTextBox ID="rtbCode" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_CODE %>"
                            Width="100%" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rtbCode"
                            ErrorMessage="<%$ Translate: CM_CTRLVALUESET_VALUESET_NAME_ERROR %>" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_CODE_ERROR %>" />
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="rtbCode"
                            Display="Dynamic" ErrorMessage="<%$ Translate: CM_CTRLVALUESET_VALUESET_CODE_ERROR_EXIST %>"
                            ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_CODE_ERROR_EXIST %>" />
                    </td>
                </tr>
                <tr>
                    <td class="lb" style="padding-right: 10px">
                        <%# Translate("CM_CTRLVALUESET_VALUESET_DESC")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                    </td>
                    <td colspan="10">
                        <tlk:RadTextBox ID="rtbVSDescr" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLVALUESET_VALUESET_DESC %>"
                            Width="100%" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rtbVSDescr"
                            ErrorMessage="<%$ Translate: Bạn phải nhập diễn giải %>" ToolTip="<%$ Translate: Bạn phải nhập diễn giải %>" />
                    </td>
                </tr>
            </table>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgValueSets" runat="server" AllowFilteringByColumn="True" AllowMultiRowSelection="True"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0"
            GridLines="None" Height="100%" PageSize="50" ShowStatusBar="True">
            <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                <Resizing AllowColumnResize="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
            <MasterTableView ClientDataKeyNames="FLEX_VALUE_SET_ID,FLEX_VALUE_SET_NAME,DESCRIPTION,TYPE_ID,TYPE,DATA_TYPE,CODE"
                CommandItemDisplay="None" DataKeyNames="FLEX_VALUE_SET_ID">
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True" />
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True" />
                <Columns>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE_SET_ID" HeaderText="<%$ Translate: FLEX_VALUE_SET_ID %>"
                        UniqueName="FLEX_VALUE_SET_ID" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="CODE" HeaderText="<%$ Translate: CM_CTRLVALUESET_VALUESET_CODE %>"
                        EmptyDataText="" SortExpression="CODE" UniqueName="CODE" FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE_SET_NAME" HeaderText="<%$ Translate: CM_CTRLVALUESET_FLEX_VALUE_SET_NAME %>"
                        EmptyDataText="" SortExpression="FLEX_VALUE_SET_NAME" UniqueName="FLEX_VALUE_SET_NAME"
                        FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="DESCRIPTION" HeaderText="<%$ Translate: CM_CTRLVALUESET_DESCRIPTION %>"
                        EmptyDataText="" SortExpression="DESCRIPTION" UniqueName="DESCRIPTION" FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TYPE_ID" HeaderText="<%$ Translate:TYPE_ID %>" Visible="false"
                        SortExpression="TYPE_ID" UniqueName="TYPE_ID" FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TYPE" HeaderText="<%$ Translate: CM_CTRLVALUESET_TYPE %>"
                        SortExpression="TYPE" UniqueName="TYPE" FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="DATA_TYPE" HeaderText="<%$ Translate: CM_CTRLVALUESET_DATA_TYPE %>"
                        SortExpression="DATA_TYPE" UniqueName="DATA_TYPE" FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </tlk:RadGrid>
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientBeforeClose="OnClientBeforeClose" OnClientClose="popupclose" Height="600px"
            EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="false"
            Title="<%$ Translate: CM_CTRLVALUESET_DEFINE_PARAM_DETAIL %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlValueSet_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlValueSet_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlValueSet_RadPane2';
        var validateID = 'MainContent_ctrlValueSet_ValidationSummary1';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

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
            if (item.get_commandName() == "CALCULATE") {
                var value = $find('<%# rgValueSets.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('TYPE_ID');
                if (value == "I") {
                    if (OpenValueSetI() == 1) {
                        var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                        var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    }
                    args.set_cancel(true);
                }
                else {
                    if (OpenValueSetT() == 1) {
                        var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                        var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    }
                    args.set_cancel(true);
                }
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgValueSets');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function OpenValueSetT() {
            var bCheck = $find('<%# rgValueSets.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var vsName = $find('<%# rgValueSets.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('FLEX_VALUE_SET_NAME');
            window.open('/Default.aspx?mid=Common&fid=ctrlValueSetT&group=Define&vsName=' + vsName, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 0;
        }

        function OpenValueSetI() {
            var bCheck = $find('<%# rgValueSets.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var vsName = $find('<%# rgValueSets.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('FLEX_VALUE_SET_NAME');
            window.open('/Default.aspx?mid=Common&fid=ctrlValueSetI&group=Define&vsName=' + vsName, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 0;
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function popupclose(oWnd, args) {
            //window.location.reload();
        }


        function OnClientBeforeClose(sender, eventArgs) {
            if (!confirm("Bạn có muốn đóng màn hình không?")) {
                //if cancel is clicked prevent the window from closing
                args.set_cancel(true);
            }
        }

        function OnClientClose(oWnd, args) {
            oWnd = $find('<%#popupId %>');
            oWnd.setSize(screen.width - 250, screen.height - 300);
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }

    </script>
</tlk:RadCodeBlock>
