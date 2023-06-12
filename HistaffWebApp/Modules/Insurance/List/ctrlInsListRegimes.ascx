<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsListRegimes.ascx.vb"
    Inherits="Insurance.ctrlInsListRegimes" %>
<%@ Register Src="~/Modules/Common/ctrlMessageBox.ascx" TagName="ctrlMessageBox"
    TagPrefix="Common" %>
      <%@ Import Namespace="Common" %>
            <link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
                <tlk:RadToolBar ID="tbarOtherLists" runat="server" />
                <asp:HiddenField ID="hidID" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"  CssClass="validationsummary"  />
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Nhóm hưởng chế độ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="rcboCHANGE_TYPE" runat="server" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqCHANGE_TYPE" ControlToValidate="rcboCHANGE_TYPE" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải chọn nhóm hưởng chế độ. %>" ToolTip="<%$ Translate:  Bạn phải chọn nhóm hưởng chế độ. %>">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cusCHANGE_TYPE" runat="server" ErrorMessage="<%$ Translate: Nhóm hưởng chế độ bảo hiểm không tồn tại hoặc đã ngừng áp dụng %>"
                                ToolTip="<%$ Translate: Nhóm hưởng chế độ bảo hiểm không tồn tại hoặc đã ngừng áp dụng%>" ControlToValidate="rcboCHANGE_TYPE" ></asp:CustomValidator>
                        </td>
                         <td class="lb">
                            <%# Translate("Mã chế độ")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode" runat="server" CausesValidation="false">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCode" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mã chế độ. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tổng số ngày")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnmDAY_OFF_SUMMARY" SkinID="number" MinValue="0" runat="server" CausesValidation="false">
                                <NumberFormat GroupSeparator="," DecimalDigits="0" />
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="reqrnmDAY_OFF_SUMMARY" ControlToValidate="rnmDAY_OFF_SUMMARY" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tổng số ngày. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Tên")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNAME" runat="server" CausesValidation="false">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="reqOtherNameVN" ControlToValidate="txtNAME" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên chế độ. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Mức hưởng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnmENJOY_LEVEL" SkinID="Decimal" MinValue="0" MaxValue="100" runat="server" CausesValidation="false">
                                <NumberFormat GroupSeparator="," DecimalDigits="2" />
                            </tlk:RadNumericTextBox>
                            (%)
                            <asp:RequiredFieldValidator ID="reqENJOY_LEVEL" ControlToValidate="rnmENJOY_LEVEL" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập mức hưởng. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Loại lương tính")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlREGIME_SALARY_TYPE" runat="server">
                            </tlk:RadComboBox>
                             <asp:RequiredFieldValidator ID="reqddlREGIME_SALARY_TYPE" ControlToValidate="ddlREGIME_SALARY_TYPE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Loại lương tính. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Loại lương tính. %>"></asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Loại ngày tính")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="ddlREGIME_DAY_TYPE" runat="server">
                            </tlk:RadComboBox>
                             <asp:RequiredFieldValidator ID="reqddlREGIME_DAY_TYPE" ControlToValidate="ddlREGIME_DAY_TYPE"
                                runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Loại ngày tính. %>"
                                ToolTip="<%$ Translate: Bạn phải nhập Loại ngày tính. %>"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgGridData" runat="server" Height="100%" AllowPaging="True" AllowSorting="True"
                    CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true" GridLines="None"
                     AutoGenerateColumns="false" AllowFilteringByColumn="true" OnPreRender="rgGridData_PreRender" OnItemCreated="rgGridData_ItemCreated">
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" EnablePostBackOnRowClick="True" >
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID, CODE,GROUP_ARISING_TYPE_ID,GROUP_ARISING_TYPE_NAME,NAME_VN,NAME_EN,DAY_OFF_SUMMARY,REGIME_SALARY_TYPE,REGIME_SALARY_NAME,ENJOY_LEVEL,REGIME_DAY_TYPE,REGIME_DAY_NAME,STATUS,CREATED_DATE,CREATED_BY">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã Nhóm %>" DataField="GROUP_ARISING_TYPE_ID"
                                UniqueName="GROUP_ARISING_TYPE_ID" SortExpression="GROUP_ARISING_TYPE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nhóm hưởng chế độ %>" DataField="GROUP_ARISING_TYPE_NAME"
                                UniqueName="GROUP_ARISING_TYPE_NAME" SortExpression="GROUP_ARISING_TYPE_NAME"
                                Visible="true">
                                <HeaderStyle Width="300px" />
                                <ItemStyle Width="300px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên chế độ %>" DataField="NAME_VN"
                                UniqueName="NAME_VN" SortExpression="NAME_VN" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã chế độ %>" DataField="CODE"
                                UniqueName="CODE" SortExpression="CODE" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tổng số ngày %>" ItemStyle-HorizontalAlign="Center"
                                DataField="DAY_OFF_SUMMARY" UniqueName="DAY_OFF_SUMMARY" SortExpression="DAY_OFF_SUMMARY" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Mức hưởng %>" DataFormatString="{0:N2}"
                                DataField="ENJOY_LEVEL" UniqueName="ENJOY_LEVEL" SortExpression="ENJOY_LEVEL" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã loại lương tính %>" DataField="REGIME_SALARY_TYPE"
                                UniqueName="REGIME_SALARY_TYPE" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại lương tính %>" DataField="REGIME_SALARY_NAME"
                                UniqueName="REGIME_SALARY_NAME" SortExpression="REGIME_SALARY_NAME" Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã loại ngày tính %>" DataField="REGIME_DAY_TYPE"
                                UniqueName="REGIME_DAY_TYPE" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại ngày tính %>" DataField="REGIME_DAY_NAME"
                                UniqueName="REGIME_DAY_NAME" Visible="true">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" ItemStyle-HorizontalAlign="Center"
                                DataField="STATUS" UniqueName="STATUS" SortExpression="STATUS" />
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


        var splitterID = 'ctl00_MainContent_ctrlInsListRegimes_RadSplitter1';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsListRegimes_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlInsListRegimes_RadPane2';
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
        function cusCHANGE_TYPE(oSrc, args) {
            var cbo = $find("<%# rcboCHANGE_TYPE.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
