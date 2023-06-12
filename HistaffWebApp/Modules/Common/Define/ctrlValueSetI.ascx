<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlValueSetI.ascx.vb"
    Inherits="Common.ctrlValueSetI" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Height="133px">
        <tlk:RadToolBar ID="rtbMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <div>
            <fieldset>
                <legend></legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="padding-right: 10px">
                            <%# Translate("VALUESET_NAME")%>&nbsp;
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtbValueSetName" runat="server" Height="22" ToolTip="<%$ Translate: VALUESET_NAME %>"
                                Width="100%" ReadOnly="true"/>                          
                        </td>
                    </tr>
                    <tr>
                        <td class="lb" style="padding-right: 10px">
                            <%# Translate("VALUE")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtbValue" runat="server" Height="22" ToolTip="<%$ Translate: VALUE %>"
                                Width="100%" ReadOnly="true"/>                                                   
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rtbValue"
                                ErrorMessage="<%$ Translate: VALUE_ERROR %>" ToolTip="<%$ Translate: VALUE_ERROR %>" />
                        </td>
                        <td class="lb" style="padding-right: 10px">
                            <%# Translate("VALUE_DESC")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtbValueDesc" runat="server" Height="22" ToolTip="<%$ Translate: VALUE_DESC %>"
                                Width="100%" ReadOnly="true"/>         
                        </td>  
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rtbValueDesc"
                                ErrorMessage="<%$ Translate: VALUE_DESC_ERROR %>" ToolTip="<%$ Translate: VALUE_DESC_ERROR %>" />                      
                        </td>                      
                    </tr>                    
                </table>
            </fieldset>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgValueSets" runat="server" AllowFilteringByColumn="true"
            AllowMultiRowSelection="true" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false"
            CellSpacing="0" GridLines="None" Height="100%" PageSize="50" ShowStatusBar="true">
            <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling UseStaticHeaders="true" />
                <Resizing AllowColumnResize="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
            <MasterTableView ClientDataKeyNames="FLEX_VALUE_SET_NAME,FLEX_VALUE_ID,FLEX_VALUE,FLEX_VALUE_DESCRIPTION"
                CommandItemDisplay="None" DataKeyNames="FLEX_VALUE_ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE_ID" HeaderText="<%$ Translate: FLEX_VALUE_ID %>"
                        UniqueName="FLEX_VALUE_ID" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE_SET_NAME" HeaderText="<%$ Translate: FLEX_VALUE_SET_NAME %>"
                        SortExpression="FLEX_VALUE_SET_NAME" UniqueName="FLEX_VALUE_SET_NAME">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE" HeaderText="<%$ Translate: FLEX_VALUE %>"
                        SortExpression="FLEX_VALUE" UniqueName="FLEX_VALUE">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE_DESCRIPTION" HeaderText="<%$ Translate:FLEX_VALUE_DESCRIPTION %>"
                        SortExpression="FLEX_VALUE_DESCRIPTION" UniqueName="FLEX_VALUE_DESCRIPTION">
                        <HeaderStyle HorizontalAlign="Center" />   
                    </tlk:GridBoundColumn>                                 
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlValueSetI_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlValueSetI_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlValueSetI_RadPane2';
        var validateID = 'MainContent_ctrlValueSetI_ValidationSummary1';
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
            if (item.get_commandName() == "SAVE") {
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

    </script>
</tlk:RadCodeBlock>
