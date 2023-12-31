﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRitual.ascx.vb"
    Inherits="Attendance.ctrlRitual" %>
    <%@ Import Namespace="Common" %>
    <link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="145px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã ngày nghỉ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" SkinID="ReadOnly" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtID" runat="server" Visible="false">
                    </tlk:RadTextBox>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Mã ngày nghỉ lễ đã tồn tại. %>"
                        ToolTip="<%$ Translate: Mã ngày nghỉ lễ đã tồn tại. %>">
                    </asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="<%$ Translate: Mã không được chứa ký tự đặc biệt và khoảng trắng %>"
                        ControlToValidate="txtCode" ValidationExpression="^[a-zA-Z0-9_]*$"></asp:RegularExpressionValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên ngày nghỉ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtNameVN"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên ngày nghỉ. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ngày nghỉ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdDate">
                    </tlk:RadDatePicker>
                     <asp:CustomValidator ID="cvDate" ControlToValidate="rdDate" runat="server" ErrorMessage="<%$ Translate: Ngày nghỉ lễ đã tồn tại. %>"
                        ToolTip="<%$ Translate: Ngày nghỉ lễ đã tồn tại. %>"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdDate"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày nghỉ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Nghỉ bù lễ")%><span class="lbReq"></span>
                </td>
                <td>
                    <asp:CheckBox ID="chkOFFDAY" runat="server" />
<%--                    <tlk:RadButton ID="cbOFFDAY" ToggleType="CheckBox" ButtonType="ToggleButton"
                            Text="" runat="server" AutoPostBack="false" CausesValidation="false">
                        </tlk:RadButton>--%>

                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rdNote" runat="server" SkinID="Textbox1023" Width="100%" Height = "37px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true" >
            <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME_VN,NAME_EN,WORKINGDAY,YEAR,ACTFLG,NOTE,OFFDAY">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Năm %>" DataField="YEAR" UniqueName="YEAR"
                        SortExpression="YEAR" DataFormatString = "{0:####}">
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày nghỉ %>" DataField="WORKINGDAY"
                        DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" UniqueName="WORKINGDAY"
                        SortExpression="WORKINGDAY" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="120px" />
                        </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã ngày nghỉ %>" DataField="CODE"
                        UniqueName="CODE" SortExpression="CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên ngày nghỉ %>" DataField="NAME_VN"
                        UniqueName="NAME_VN" SortExpression="NAME_VN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Nghỉ bù lễ %>" DataField="OFFDAY" UniqueName="OFFDAY"
                        SortExpression="OFFDAY" DataType="System.Boolean" AllowFiltering="false"/>
                    <%--<tlk:GridCheckBoxColumn HeaderText="Nghỉ bù lễ" DataField="OFFDAY"
                        UniqueName="OFFDAY" SortExpression="OFFDAY"> </tlk:GridCheckBoxColumn>--%>

                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlRitual_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRitual_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlRitual_RadPane2';
        var validateID = 'MainContent_ctrlRitual_valSum';
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
            if (args.get_item().get_commandName() == "EXPORT") {
                var grid = $find("<%=rgDanhMuc.ClientID %>");
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
            } else if (args.get_item().get_commandName() == "SAVE") 
            /*{
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }*/
            {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } 
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        /*
        function ResizeSplitter() {
            setTimeout(function () {
                var splitter = $find("<%= RadSplitter3.ClientID%>");
                var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(splitter.get_height() + pane.get_height() - height);
                pane.set_height(height);
            }, 200);
        }
        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {
            var splitter = $find("<%= RadSplitter3.ClientID%>");
            var pane = splitter.getPaneById('<%= RadPane1.ClientID %>');
            if (oldSize == 0) {
                oldSize = pane.getContentElement().scrollHeight;
            } else {
                var pane2 = splitter.getPaneById('<%= RadPane2.ClientID %>');
                splitter.set_height(splitter.get_height() + pane.get_height() - oldSize);
                pane.set_height(oldSize);
                pane2.set_height(splitter.get_height() - oldSize - 1);
            }
        }* /

    </script>
</tlk:RadCodeBlock>
