<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_ListSalaries.ascx.vb"
    Inherits="Payroll.ctrlPA_ListSalaries" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
   @media screen and (-webkit-min-device-pixel-ratio:0) {
        #ctl00_MainContent_ctrlPA_ListSalaries_txtRemark
        {       
            height: 56px;
        } 
    }
    .RadGrid_Metro .rgRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgAltRow>td:first-child 
    {
        padding :0 !important;
    }
    .RadGrid_Metro .rgHeader
    {
        padding :0 !important;
    }
    #ctl00_MainContent_ctrlPA_ListSalaries_btnSearch input
    {
        height: 24px !important;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="253px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboOBJ_SALARY" runat="server" SkinID="dDropdownList" AutoPostBack="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalOBJ_SALARY" runat="server" ControlToValidate="cboOBJ_SALARY"
                        ErrorMessage="<%$ Translate: Đối tượng lương không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Đối tượng lương không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                    <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm %>" SkinID="ButtonFind" style="height:24px; margin-left:10px">
                    </tlk:RadButton>
                </td>
            </tr>
            <tr>
                <td class="item-head" colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Nhóm ký hiệu lương")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboGROUP_TYPE" runat="server" SkinID="dDropdownList" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged"
                        AutoPostBack="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalGROUP_TYPE" runat="server" ControlToValidate="cboGROUP_TYPE"
                        ErrorMessage="<%$ Translate: Nhóm ký hiệu lương không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Nhóm ký hiệu lương không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Mã danh mục lương")%>
                </td>
                <td>
                    <%-- <tlk:RadTextBox ID="txtCOL_NAME" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>--%>
                    <tlk:RadComboBox ID="cboFIELD" runat="server" SkinID="LoadDemand" OnClientItemsRequesting="OnClientItemsRequesting"
                        AutoPostBack="false">
                    </tlk:RadComboBox>
                    <%--<asp:CustomValidator ID="cvalFIELD" runat="server" ControlToValidate="cboFIELD"
                        ErrorMessage="<%$ Translate: Mã danh mục lương không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Mã danh mục lương không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>--%>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="cboFIELD"
                        runat="server" ErrorMessage="<%$ Translate: Mã danh mục lương. %>"
                        ToolTip="<%$ Translate: Mã danh mục lương. %>"></asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Tên tiếng việt")%>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNAME_VN" runat="server" Width="100%" MaxLength="200">
                    </tlk:RadTextBox>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNAME_VN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập tên danh mục lương. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập tên danh mục lương. %>"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Kiểu dữ liệu")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDATA_TYPE" runat="server" SkinID="dDropdownList" AutoPostBack="false">
                        <Items>
                            <tlk:RadComboBoxItem Value="1" Text="Kiểu số" />
                            <tlk:RadComboBoxItem Value="0" Text="Kiểu chữ" />
                            <tlk:RadComboBoxItem Value="2" Text="Kiểu ngày" />
                        </Items>
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Thứ tự") %>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmCOL_INDEX" runat="server" SkinID="Number">
                    </tlk:RadNumericTextBox>
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="nmCOL_INDEX"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập thứ tự. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập thứ tự. %>"></asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><%--<span class="lbReq">*</span>--%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="dtpEffect" runat="server" Width="100%">
                    </tlk:RadDatePicker>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="dtpEffect"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb" style="display: none">
                    <%--<%# Translate("Ngày hết hiệu lực")%>--%>
                </td>
                <td style="display: none">
                    <%--<tlk:RadDatePicker ID="dtpExpire" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="dtpExpire"
                        ControlToCompare="dtpEffect" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"
                        ToolTip="<%$ Translate: Ngày hết hiệu lực phải lớn hơn ngày hiệu lực %>"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="5">
                    </tlk:RadButton>
                    <tlk:RadButton ID="chkIS_SumDay" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="Mapping Quỹ lương/Thưởng" CausesValidation="False">
                    </tlk:RadButton>
                    <tlk:RadButton ID="chkIS_WorkArising" AutoPostBack="false" ToggleType="CheckBox"
                        ButtonType="ToggleButton" runat="server" Text="Công thức theo biến động" CausesValidation="False">
                    </tlk:RadButton>
                    <tlk:RadButton ID="chkIS_SumArising" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="Tổng theo biến động" CausesValidation="False">
                    </tlk:RadButton>
                    <tlk:RadButton ID="chkIS_Payback" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="Payback" CausesValidation="False">
                    </tlk:RadButton>
                    <tlk:RadButton ID="chkIS_VISIBLE" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="Hiển thị trong bảng lương">
                    </tlk:RadButton>
                    <%--<tlk:RadButton ID="chkIS_INPUT" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="Dữ liệu đầu vào" Visible="False">
                    </tlk:RadButton>--%>
                    <%--<tlk:RadButton ID="chkIS_CALCULATE" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="Áp dụng công thức" Visible="False">
                    </tlk:RadButton>--%>
                    <tlk:RadButton ID="chkIS_IMPORT" AutoPostBack="false" ToggleType="CheckBox" ButtonType="ToggleButton"
                        runat="server" Text="Dữ liệu import">
                    </tlk:RadButton>
                </td>
            </tr>
            <%--<tr style="display: none">
                <td class="lb" style="display: none">
                    <%# Translate("Bảng lương")%>
                </td>
                <td colspan="3" style="display: none">
                    <tlk:RadComboBox ID="cboTYPE_PAYMENT" runat="server" SkinID="dDropdownList" >
                    </tlk:RadComboBox>
                </td>
            </tr>--%>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" AllowPaging="true" Width="100%"
            PageSize="50">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,TYPE_PAYMENT,COL_NAME,NAME_VN,NAME_EN,DATA_TYPE,COL_INDEX,STATUS,IS_VISIBLE,IS_CALCULATE,GROUP_TYPE_ID,GROUP_TYPE_NAME,OBJ_SAL_ID,
                OBJ_SAL_NAME,IS_IMPORT,COL_CODE,IS_SUMDAY,IS_WORKARISING,IS_SUMARISING,IS_PAYBACK, REMARK,EFFECTIVE_DATE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã danh mục lương %>" DataField="COL_NAME"
                        SortExpression="COL_NAME" UniqueName="COL_NAME" HeaderStyle-Width="100px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tiếng việt %>" DataField="NAME_VN"
                        SortExpression="NAME_VN" UniqueName="NAME_VN" HeaderStyle-Width="180px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm công thức %>" DataField="OBJ_SAL_NAME"
                        SortExpression="OBJ_SAL_NAME" UniqueName="OBJ_SAL_NAME" HeaderStyle-Width="180px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm danh mục lương %>" DataField="GROUP_TYPE_NAME"
                        SortExpression="GROUP_TYPE_NAME" UniqueName="GROUP_TYPE_NAME" HeaderStyle-Width="120px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu dữ liệu %>" DataField="DATA_TYPE"
                        SortExpression="DATA_TYPE" UniqueName="DATA_TYPE" Display="False" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECTIVE_DATE"
                        UniqueName="EFFECTIVE_DATE" SortExpression="EFFECTIVE_DATE"
                        HeaderStyle-Width="120px" DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="COL_INDEX"
                        SortExpression="COL_INDEX" UniqueName="COL_INDEX">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS"
                        SortExpression="STATUS" UniqueName="STATUS" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                    <tlk:GridCheckBoxColumn UniqueName="IS_SUMDAY" DataField="IS_SUMDAY" HeaderText="<%$ Translate: Mapping Quỹ lương/Thưởng %>"
                        SortExpression="IS_SUMDAY" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn UniqueName="IS_WORKARISING" DataField="IS_WORKARISING" HeaderText="<%$ Translate: Công thức theo biến động %>"
                        SortExpression="IS_WORKARISING" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn UniqueName="IS_SUMARISING" DataField="IS_SUMARISING" HeaderText="<%$ Translate: Tổng theo biến động %>"
                        SortExpression="IS_SUMARISING" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn UniqueName="IS_PAYBACK" DataField="IS_PAYBACK" HeaderText="<%$ Translate: Payback %>"
                        SortExpression="IS_PAYBACK" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn UniqueName="IS_VISIBLE" DataField="IS_VISIBLE" HeaderText="<%$ Translate: Hiển thị trong bảng lương %>"
                        SortExpression="IS_VISIBLE" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn UniqueName="IS_IMPORT" DataField="IS_IMPORT" HeaderText="<%$ Translate: Dữ liệu import %>"
                        SortExpression="IS_IMPORT" ItemStyle-HorizontalAlign="Center" AllowFiltering="false">
                    </tlk:GridCheckBoxColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <HeaderStyle HorizontalAlign="Center" />
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlPA_ListSalaries_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_TR_ctl00_MainContent_ctrlPA_ListSalaries_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_TR_ctl00_MainContent_ctrlPA_ListSalaries_RadPane2';
        var validateID = 'MainContent_ctrlPA_ListSalaries_valSum';
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
            if (args.get_item().get_commandName() == 'SAVE') {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            } else if (args.get_item().get_commandName() == 'EXPORT') {
                var grid = $find("<%=rgData.ClientID %>");
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                if (rows.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
       
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {

                case '<%= cboGROUP_TYPE.ClientID %>':
                    cbo = $find('<%= cboFIELD.ClientID %>');
                    clearSelectRadcombo(cbo);
                    break;
                default:
                    break;
            }
        }

        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }

        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;

            switch (id) {
                case '<%= cboFIELD.ClientID %>':
                    cbo = $find('<%= cboGROUP_TYPE.ClientID %>');
                    value = cbo.get_value();
                    break;
                default:
                    break;
            }

            if (!value) {
                value = null;
            }

            var context = eventArgs.get_context();
            context["valueCustom"] = value;
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlPA_ListSalaries_dtpEffect_dateInput').val('');
            $('#ctl00_MainContent_ctrlPA_ListSalaries_cboFIELD_Input').val('');
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
        }

    </script>
</tlk:RadCodeBlock>
