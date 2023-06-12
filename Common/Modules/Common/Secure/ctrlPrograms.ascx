<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPrograms.ascx.vb"
    Inherits="Common.ctrlPrograms" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<%@ Register Src="../ctrlUpload.ascx" TagName="ctrlUpload" TagPrefix="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .RadGrid_Metro .rgFilterRow td 
    {
        text-align: center;
    }
    .tdheight 
    {
        height: 28px;
    }
    #ctl00_MainContent_ctrlPrograms_rgPrograms_ctl00_ctl02_ctl02_FilterCheckBox_STATUS 
    {
        display:none;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Height="250px">
        <tlk:RadToolBar ID="rtbMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb tdheight" width="140px">
                    <%# Translate("CM_CTRLPROGRAMS_PROGRAM_CODE")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbProgramCode" runat="server" Width="250px" MaxLength="50" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_CODE %>"
                        Height="22" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rtbProgramCode"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_CODE_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_CODE_ERROR %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorCode" ControlToValidate="rtbProgramCode"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_CODE %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_CODE %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="rtbProgramCode" ValidationExpression="^[a-zA-Z0-9_]*$">
                    </asp:RegularExpressionValidator>
                </td>
                <td class="lb" style="padding-left: 40px">
                    <%# Translate("CM_CTRLPROGRAMS_PROGRAM_NAME")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbProgramName" runat="server" Width="300px" MaxLength="2000"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_NAME%>" Height="22" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rtbProgramName"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_NAME_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_NAME_ERROR %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorName" ControlToValidate="rtbProgramName"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_NAME %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_IS_EXSIST_PROGRAM_NAME %>"></asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("CM_CTRLPROGRAMS_PROGRAM_TYPE")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbProgramType" DataValueField="ID" DataTextField="NAME" runat="server"
                        SkinID="Number" AutoPostBack="True" CausesValidation="false" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_TYPE %>">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rcbProgramType"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_TYPE_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_TYPE_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb tdheight"">
                    <%# Translate("CM_CTRLPROGRAMS_PROGRAM_DESCRIPTION")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbProgramDesc" runat="server" Width="250px" MaxLength="100"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_DESCRIPTION %>" Height="22" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rtbProgramDesc"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_DESCRIPTION_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_DESCRIPTION_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="padding-right: 10px">
                    <%# Translate("CM_CTRLPROGRAMS_SQLLOADER_FILE")%>&nbsp;
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbSQLLoaderFile" runat="server" Width="300px" MaxLength="200"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_SQLLOADER_FILE %>" Height="22" />
                </td>
                <td class="lb" style="text-align:left">
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnUpload" runat="server" Text="<%$ Translate: CM_CTRLPROGRAMS_BROWSER %>">
                    </tlk:RadButton>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="rbtSQLLoaderDownload" runat="server"
                        CausesValidation="false" OnClientClicked="rbtClicked" Text="<%$ Translate: CM_CTRLPROGRAMS_SQLLOADER_DOWNLOAD %>">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="lb tdheight" style="display: block;width: 95%;padding-top:5px">
                    <%# Translate("CM_CTRLPROGRAMS_STORE_IN")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbStoreIn" runat="server" Width="250px" MaxLength="50" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_STORE_IN %>"
                        Height="15" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStoreIn" ControlToValidate="rtbStoreIn"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_STORE_IN_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_STORE_IN_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("CM_CTRLPROGRAMS_STORE_OUT")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbStoreOut" runat="server" Width="300px" MaxLength="50" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_STORE_OUT %>"
                        Height="22" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorStoreOut" ControlToValidate="rtbStoreOut"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_STORE_OUT_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_STORE_OUT_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("CM_CTRLPROGRAMS_PRIORITY")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="RadNTBPriority" MinValue="1" MaxValue="5"
                        Width="40px" Value="1" MaxLength="2" ShowSpinButtons="True">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="RadNTBPriority"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_PRIORITY_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PRIORITY_ERROR %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorPriority" ControlToValidate="RadNTBPriority"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_PRIORITY_ERROR_EXIST %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_PRIORITY_ERROR_EXIST %>"></asp:CustomValidator>                                            
                </td>
            </tr>
            <tr>
                <td class="lb tdheight" style="padding-left: 20px">
                    <%# Translate("CM_CTRLPROGRAMS_TEMPLATE_TYPE_IN")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbbTemplateTypeIn" DataValueField="ID" DataTextField="NAME"
                        SkinID="Number" runat="server" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATE_TYPE_IN %>">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTypeIn" ControlToValidate="rcbbTemplateTypeIn"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATE_TYPE_IN_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATE_TYPE_IN_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="padding-left: 40px">
                    <%# Translate("CM_CTRLPROGRAMS_TEMPLATE_TYPE_OUT")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbbTemplateTypeOut" DataValueField="ID" DataTextField="NAME"
                        SkinID="Number" runat="server" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATE_TYPE_OUT %>">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorTypeOut" ControlToValidate="rcbbTemplateTypeOut"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATE_TYPE_OUT_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATE_TYPE_OUT_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("CM_CTRLPROGRAMS_LINE_FROM")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="RadNTBLineFrom" MinValue="1" MaxValue="10000"
                        Width="40px" Value="1" MaxLength="6" AutoPostBack="true" ShowSpinButtons="True">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb tdheight" style="padding-left: 40px">
                    <%# Translate("CM_CTRLPROGRAMS_MODULE")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbbModule" DataValueField="ID" DataTextField="NAME" runat="server"
                        SkinID="Number" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_MODULE %>">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rcbbModule"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_MODULE_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_MODULE_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="padding-left: 40px">
                    <%# Translate("CM_CTRLPROGRAMS_GROUP")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbbGroup" DataValueField="ID" DataTextField="NAME" runat="server"
                        SkinID="Number" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_GROUP %>">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="rcbbGroup"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_GROUP_ERROR %>" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_GROUP_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                </td>
                <td>
                    <%--<tlk:RadButton ID="cbSTATUS" ToggleType="CheckBox" ButtonType="ToggleButton" runat="server"
                        Text="<%$ Translate: CM_CTRLPROGRAMS_STATUS %>">
                    </tlk:RadButton>--%>
                    <asp:CheckBox ID="chkUse" runat="server" Text="<%$ Translate: CM_CTRLPROGRAMS_STATUS %>"></asp:CheckBox>
                </td>
                <%--<td>
                    <tlk:RadButton runat="server" ID="rbtnFlexField" Text="Trường mở rộng">
                    </tlk:RadButton>  
                </td>--%>
            </tr>
            <tr>
                <td class="lb tdheight">
                    <%# Translate("CM_CTRLPROGRAMS_FILE_TEMPLATE_URL")%>&nbsp;
                </td>
                <td colspan="2">
                    <tlk:RadTextBox ID="rtbFileTemplateURL" runat="server" Width="100%" MaxLength="100"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_FILE_TEMPLATE_URL %>" Height="22" />
                </td>
                <td>
                    <tlk:RadComboBox ID="rcbbTemplates" DataValueField="ID" DataTextField="NAME" SkinID="Number"
                        runat="server" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATES %>">
                    </tlk:RadComboBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="rbtUpFileTemplate" runat="server"
                        Text="<%$ Translate: CM_CTRLPROGRAMS_UPLOAD %>">
                    </tlk:RadButton>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="rbtDeleteTemplate" CausesValidation="false"
                        runat="server" Text="<%$ Translate: CM_CTRLPROGRAMS_DELETE %>" Enabled="false">
                    </tlk:RadButton>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="rtbDownloadTemplate" CausesValidation="false"
                        OnClientClicked="rbtClicked" runat="server" Text="<%$ Translate: CM_CTRLPROGRAMS_DOWNLOAD %>"
                        Enabled="false">
                    </tlk:RadButton>
                </td>
                <td class="lb">
                    <%# Translate("CM_CTRLPROGRAMS_FILE_OUT_NAME")%>&nbsp;<span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="rtbFileOutName" runat="server" MaxLength="100" ToolTip="<%$ Translate: CM_CTRLPROGRAMS_FILE_OUT_NAME %>"
                        Height="22" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFileOutName" ControlToValidate="rtbFileOutName"
                        runat="server" ErrorMessage="<%$ Translate: CM_CTRLPROGRAMS_FILE_OUT_NAME_ERROR %>"
                        ToolTip="<%$ Translate: CM_CTRLPROGRAMS_FILE_OUT_NAME_ERROR %>">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="Y">
        <tlk:RadGrid ID="rgPrograms" runat="server" AllowPaging="True" AllowSorting="True" SkinID="GridSingleSelect"
            CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="false" GridLines="None"
            PageSize="50" Height="99%" AllowFilteringByColumn="true" AutoGenerateColumns="false" OnPreRender="rgPrograms_PreRender">
            <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" Scrolling-AllowScroll="true"
                EnablePostBackOnRowClick="true" Scrolling-SaveScrollPosition="true" Scrolling-UseStaticHeaders="true">
                <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="4" />
                <Selecting AllowRowSelect="true" />
                <Resizing AllowColumnResize="true" />
                <ClientEvents OnRowDblClick="gridRowDblClick" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
            <MasterTableView CommandItemDisplay="None" DataKeyNames="PROGRAM_ID" ClientDataKeyNames="PROGRAM_ID,CODE,NAME,PROGRAM_TYPE_ID,PROGRAM_TYPE,DESCRIPTION,STORE_EXECUTE_IN,STORE_EXECUTE_OUT,
                    TEMPLATE_TYPE_IN,TEMPLATE_TYPE_OUT,TEMPLATE_URL,PRIORITY,STATUS,FILE_OUT_NAME,MODULE_ID,MODULE_NAME,GROUP_ID,GROUP_NAME,SQLLOADER_FILE,LINE_FROM">
                <Columns>
                    <tlk:GridBoundColumn DataField="PROGRAM_ID" UniqueName="PROGRAM_ID" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_CODE %>"
                        DataField="CODE" FilterControlWidth="90%" SortExpression="CODE" UniqueName="CODE">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_PROGRAM_NAME %>"
                        DataField="NAME" FilterControlWidth="90%" SortExpression="NAME" UniqueName="NAME">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="PROGRAM_TYPE_ID" UniqueName="PROGRAM_TYPE_ID" Visible="false">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:CM_CTRLPROGRAMS_PROGRAM_TYPE %>" DataField="PROGRAM_TYPE"
                        FilterControlWidth="90%" SortExpression="PROGRAM_TYPE" UniqueName="PROGRAM_TYPE">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_DESCRIPTION %>" DataField="DESCRIPTION"
                        EmptyDataText="" FilterControlWidth="90%" SortExpression="DESCRIPTION" UniqueName="DESCRIPTION">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_STORE_EXECUTE_IN %>"
                        DataField="STORE_EXECUTE_IN" EmptyDataText="" FilterControlWidth="90%" SortExpression="STORE_EXECUTE_IN"
                        UniqueName="STORE_EXECUTE_IN">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_STORE_EXECUTE_OUT %>"
                        DataField="STORE_EXECUTE_OUT" EmptyDataText="" FilterControlWidth="90%" SortExpression="STORE_EXECUTE_OUT"
                        UniqueName="STORE_EXECUTE_OUT">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:TEMPLATE_URL %>" DataField="TEMPLATE_URL"
                        EmptyDataText="" FilterControlWidth="90%" SortExpression="TEMPLATE_URL" UniqueName="TEMPLATE_URL">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_FILE_OUT_NAME %>"
                        DataField="FILE_OUT_NAME" Visible="false" EmptyDataText="" SortExpression="FILE_OUT_NAME"
                        UniqueName="FILE_OUT_NAME">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATE_TYPE_IN %>"
                        DataField="TEMPLATE_TYPE_IN" Visible="false" EmptyDataText="" SortExpression="TEMPLATE_TYPE_IN"
                        UniqueName="TEMPLATE_TYPE_IN">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_TEMPLATE_TYPE_OUT %>"
                        DataField="TEMPLATE_TYPE_OUT" Visible="false" EmptyDataText="" SortExpression="TEMPLATE_TYPE_OUT"
                        UniqueName="TEMPLATE_TYPE_OUT">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_PRIORITY %>" DataField="PRIORITY"
                        Visible="false" SortExpression="PRIORITY" UniqueName="PRIORITY">
                        <HeaderStyle HorizontalAlign="Center" Width="250px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:MODULE_ID %>" DataField="MODULE_ID"
                        UniqueName="MODULE_ID" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_MODULE_NAME %>" DataField="MODULE_NAME"
                        FilterControlWidth="90%" SortExpression="MODULE_NAME" UniqueName="MODULE_NAME">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate:GROUP_ID %>" DataField="GROUP_ID"
                        Visible="false" SortExpression="GROUP_ID" UniqueName="GROUP_ID">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_GROUP_NAME %>" DataField="GROUP_NAME"
                        FilterControlWidth="90%" SortExpression="GROUP_NAME" UniqueName="GROUP_NAME">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: CM_CTRLPROGRAMS_STATUS %>" DataField="STATUS"
                        DataType="System.Boolean" FilterControlWidth="20px" SortExpression="STATUS" UniqueName="STATUS">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridBoundColumn DataField="SQLLOADER_FILE" UniqueName="SQLLOADER_FILE" Visible="false"
                        EmptyDataText="">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="LINE_FROM" UniqueName="LINE_FROM" Visible="false">
                    </tlk:GridBoundColumn>
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
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlUpload ID="ctrlUpload2" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientBeforeClose="OnClientBeforeClose" OnClientClose="popupclose" Height="600px"
            EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPrograms_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPrograms_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPrograms_RadPane2';
        var validateID = 'MainContent_ctrlPrograms_valSum';
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
            //            if (item.get_commandName() == "EDIT") {
            //                enableAjax = false;
            //            }
            if (item.get_commandName() == "IMPORT") {
                OpenImport(sender, args);
                args.set_cancel(true);
                enableAjax = false;
            }
            if (item.get_commandName() == "CALCULATE") {
                gridRowDblClick(sender, args);
                args.set_cancel(true);
            }
            if (item.get_commandName() == "CANCEL") {
                window.location.replace('/Default.aspx?mid=Common&fid=ctrlPrograms&group=Define');
                args.set_cancel(true);
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgPrograms');
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            var programCode = $find('<%# rgPrograms.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('CODE');
            window.open('/Default.aspx?mid=Common&fid=ctrlProGramParameters&group=Define&programCode=' + programCode, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 0;
        }

        function OpenImport(sender, eventArgs) {
            var programCode = "IMPORT_ASP_NET";
            window.open('/Default.aspx?mid=Common&fid=ctrlViewImport&programCode=' + programCode, "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
            return 0;
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

    </script>
</tlk:RadCodeBlock>
