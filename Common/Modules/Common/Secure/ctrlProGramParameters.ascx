<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="ctrlProGramParameters.ascx.vb"
    Inherits="Common.ctrlProGramParameters" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style>
    #ctl00_MainContent_ctrlProGramParameters_rgProgramParameters_ctl00_ctl02_ctl02_FilterTextBox_IS_REQUIRE
    {
        display:none;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Height="140px">
        <tlk:RadToolBar ID="rtbMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb" style="padding-right: 10px">
                    <%# Translate("CM_CTRLPROGRAMPARAMETERS_PROGRAM_CODE")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                </td>
                <td colspan="4">
                    <tlk:RadTextBox ID="rtbProgramCode" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_PROGRAM_CODE %>"
                        Width="70%" SkinID="Readonly"/>
                </td>
            </tr>
            <tr>
                <td class="lb" style="padding-right: 10px">
                    <%# Translate("CM_CTRLPROGRAMPARAMETERS_PARAMETER_NAME")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbParameterName" runat="server" Height="22" MaxLength="50" ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_PARAMETER_NAME %>" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rtbParameterName"
                        ErrorMessage="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_PARAMETER_NAME_ERROR %>" ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_PARAMETER_NAME_ERROR %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorParaName" runat="server" ControlToValidate="rtbParameterName"
                        Display="Dynamic" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_NAME %>" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_NAME %>"></asp:CustomValidator>
                </td>                        
                <td class="lb" style="padding-right: 10px">
                    <%# Translate("CM_CTRLPROGRAMPARAMETERS_VALUE_SET")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbValueSet" runat="server" DataTextField="NAME" DataValueField="ID" SkinID="Number"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_VALUE_SET %>">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rcbValueSet"
                        ErrorMessage="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_VALUE_SET_ERROR %>" ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_VALUE_SET_ERROR %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="padding-right: 10px">
                    <%# Translate("CM_CTRLPROGRAMPARAMETERS_DEFAULT")%>&nbsp;
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbDefault" runat="server" DataTextField="NAME" DataValueField="ID" SkinID="Number"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_DEFAULT %>">
                    </tlk:RadComboBox>                            
                </td>
            </tr>
            <tr>
                <td class="lb" style="padding-right: 10px">
                    <%# Translate("CM_CTRLPROGRAMPARAMETERS_PARAMETER_DESC")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbParaDesc" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_PARAMETER_DESC %>" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rtbParaDesc"
                        ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_DESCRIPTION_ERROR %>" 
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_DESCRIPTION_ERROR %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="padding-right: 10px">
                    <%# Translate("CM_CTRLPROGRAMPARAMETERS_PARAMETER_DATA_TYPE")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbbDataType" runat="server" DataTextField="NAME" DataValueField="ID" SkinID="Number"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_PARAMETER_DATA_TYPE %>">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rcbbDataType"
                        ErrorMessage="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_PARAMETER_DATA_TYPE_ERROR %>" ToolTip="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_PARAMETER_DATA_TYPE_ERROR %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="padding-right: 10px">
                    <%# Translate("CM_CTRLPROGRAMPARAMETERS_PARAMETER_SEQUENCE")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="RadNTBSequence" runat="server" AutoPostBack="true" MaxLength="2"
                        MaxValue="30" MinValue="1" ShowSpinButtons="True" Value="1" Width="40px">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadNTBSequence"
                        ErrorMessage="<%$ Translate: BẠN PHẢI NHẬP THỨ TỰ. %>" ToolTip="<%$ Translate: BẠN PHẢI NHẬP THỨ TỰ. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorSequence" ControlToValidate="RadNTBSequence"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_SEQUENCE %>" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_IS_EXSIST_PARAMETER_SEQUENCE %>"
                        Display="Dynamic"></asp:CustomValidator>
                </td>
                <td>
                    <%--<tlk:RadButton ID="cbRequire" runat="server" ButtonType="ToggleButton" Text="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_REQUIRE %>"
                        ToggleType="CheckBox">
                    </tlk:RadButton>--%>
                    <asp:CheckBox ID="cbRequire" runat="server" Text="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_REQUIRE %>"/>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="Y">
        <tlk:RadGrid ID="rgProgramParameters" runat="server" AllowFilteringByColumn="true" SkinID="GridSingleSelect"
            AllowMultiRowSelection="false" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false" OnPreRender="rgProgramParameters_PreRender"
            CellSpacing="0" GridLines="None" Height="100%" PageSize="50" ShowStatusBar="true">
            <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling UseStaticHeaders="true" />
                <Resizing AllowColumnResize="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
            <MasterTableView ClientDataKeyNames="FLEX_VALUE_SET_ID,FLEX_VALUE_SET_NAME,DESCRIPTIVE_FLEXFIELD_NAME,APPLICATION_COLUMN_NAME,LABEL_COLUMN_NAME,SEQUENCE,
                                DESCRIPTIVE_FLEX_COLUMN_ID,DESCRIPTION,DATA_TYPE_ID,TYPE_FIELD,IS_REQUIRE"
                CommandItemDisplay="None" DataKeyNames="DESCRIPTIVE_FLEX_COLUMN_ID">
                <Columns>
                    <tlk:GridBoundColumn DataField="DESCRIPTIVE_FLEX_COLUMN_ID" HeaderText="<%$ Translate: DESCRIPTIVE_FLEX_COLUMN_ID %>"
                        UniqueName="DESCRIPTIVE_FLEX_COLUMN_ID" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE_SET_ID" HeaderText="<%$ Translate: FLEX_VALUE_SET_ID %>" 
                        UniqueName="FLEX_VALUE_SET_ID" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE_SET_NAME" HeaderText="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_FLEX_VALUE_SET_NAME %>"  FilterControlWidth="90%"
                        SortExpression="FLEX_VALUE_SET_NAME" UniqueName="FLEX_VALUE_SET_NAME">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="APPLICATION_COLUMN_NAME" HeaderText="<%$ Translate: APPLICATION_COLUMN_NAME %>" EmptyDataText="" FilterControlWidth="90%"
                        SortExpression="APPLICATION_COLUMN_NAME" UniqueName="APPLICATION_COLUMN_NAME" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LABEL_COLUMN_NAME" HeaderText="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_LABEL_COLUMN_NAME %>" EmptyDataText="" FilterControlWidth="90%"
                        SortExpression="LABEL_COLUMN_NAME" UniqueName="LABEL_COLUMN_NAME">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="SEQUENCE" HeaderText="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_SEQUENCE %>"  FilterControlWidth="90%"
                        SortExpression="SEQUENCE" UniqueName="SEQUENCE">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="DESCRIPTION" HeaderText="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_DESCRIPTION %>" EmptyDataText="" FilterControlWidth="90%"
                        SortExpression="DESCRIPTION" UniqueName="DESCRIPTION">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="DATA_TYPE_ID" HeaderText="<%$ Translate:DATA_TYPE_ID %>"
                        SortExpression="DATA_TYPE_ID" UniqueName="DATA_TYPE_ID" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TYPE_FIELD" HeaderText="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_TYPE_FIELD %>" EmptyDataText="" FilterControlWidth="90%"
                        SortExpression="TYPE_FIELD" UniqueName="TYPE_FIELD">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn DataField="IS_REQUIRE" HeaderText="<%$ Translate: CM_CTRLPROGRAMPARAMETERS_IS_REQUIRE %>" FilterControlWidth="90%"
                        SortExpression="IS_REQUIRE" UniqueName="IS_REQUIRE">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlProGramParameters_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlProGramParameters_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlProGramParameters_RadPane2';
        var validateID = 'MainContent_ctrlProGramParameters_valSum';
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_RadSplitter1');
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgProgramParameters');
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
            enableAjax = false;
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
