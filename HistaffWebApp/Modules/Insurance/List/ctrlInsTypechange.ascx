<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsTypechange.ascx.vb"
    Inherits="Insurance.ctrlInsTypechange" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="180px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOtherLists" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"  CssClass="validationsummary"  />
                <div style="display: none;">
                    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
                    </tlk:RadTextBox>
                </div>
                <div>
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <%# Translate("Thông tin chung")%>
                        </legend>
                        <table class="table-form">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Mã")%><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtCODE" MaxLength="50" runat="server">
                                    </tlk:RadTextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtCODE" ControlToValidate="txtCODE" runat="server"
                                        Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Mã  %>" ToolTip="<%$ Translate: Bạn phải nhập Mã . %>"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Tên loại thay đổi")%><span class="lbReq">*</span>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtNAME" MaxLength="255" Width="400px" runat="server">
                                    </tlk:RadTextBox>
                                    <asp:RequiredFieldValidator ID="reqOtherNameVN" ControlToValidate="txtNAME" runat="server"
                                        Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Tên loại thay đổi. %>"
                                        ToolTip="<%$ Translate: Bạn phải nhập Tên loại thay đổi. %>"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Ghi chú")%>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtNote" MaxLength="255" Width="400px" runat="server">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            
                        </table>
                    </fieldset>
                </div>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgGridData" runat="server" Height="100%" AllowPaging="True" AllowSorting="True"
                    CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true" GridLines="None"
                     AutoGenerateColumns="false" AllowFilteringByColumn="true" >
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" EnablePostBackOnRowClick="True" >
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME,NOTE,STATUS">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã  %>" DataField="CODE"
                                HeaderStyle-Width="50px" UniqueName="CODE" SortExpression="CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên Loại thay đổi %>" DataField="NAME"
                                HeaderStyle-Width="130px" UniqueName="NAME" SortExpression="NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                                HeaderStyle-Width="250px" SortExpression="NOTE" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Áp dụng %>" DataField="STATUS_NAME" UniqueName="STATUS_NAME"
                                HeaderStyle-Width="50px" SortExpression="STATUS_NAME" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">


        var splitterID = 'ctl00_MainContent_ctrlInsTypechange_RadSplitter1';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsTypechange_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsTypechange_RadPane2';
        var validateID = 'MainContent_ctrlInsListRegimes_valSum';
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
            if (item.get_commandName() == "EXPORT") {
                var rows = $find('<%= rgGridData.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGridData');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgGridData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
