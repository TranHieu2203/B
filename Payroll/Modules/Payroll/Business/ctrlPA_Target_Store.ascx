<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Target_Store.ascx.vb"
    Inherits="Payroll.ctrlPA_Target_Store" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="270" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="260px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbYear" Text="Năm"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cbYear" runat="server" SkinID="dDropdownList" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqYear" ControlToValidate="cbYear" runat="server"
                                ErrorMessage="Bạn phải chọn Năm" ToolTip="Bạn phải chọn Năm">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbMonth" Text="Tháng"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <asp:HiddenField ID="hidPeriodID" runat="server" />
                            <tlk:RadComboBox ID="cbMonth" runat="server" SkinID="dDropdownList" CausesValidation="false">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqMonth" ControlToValidate="cbMonth" runat="server"
                                ErrorMessage="Bạn phải chọn Tháng" ToolTip="Bạn phải chọn Tháng">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbTargetGroup" Text="Nhóm Target"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cbTargetGroup" runat="server" AutoPostBack="true" CausesValidation="false" SkinID="dDropdownList">
                            </tlk:RadComboBox>
                            <asp:RequiredFieldValidator ID="reqTargetGroup" ControlToValidate="cbTargetGroup" runat="server"
                                ErrorMessage="Bạn phải chọn Nhóm Target" ToolTip="Bạn phải chọn Nhóm Target">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbStore" Text="Mã cửa hàng"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <asp:HiddenField ID="hidStoreID" runat="server" />
                            <tlk:RadTextBox ID="txtStore" runat="server">
                            </tlk:RadTextBox>
                            <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                            <asp:RequiredFieldValidator ID="reqStore" ControlToValidate="txtStore" runat="server"
                                ErrorMessage="Bạn phải chọn Mã cửa hàng" ToolTip="Bạn phải chọn Mã cửa hàng">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbTarget_CON" Text="Target_CON"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnTarget_CON" runat="server">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lb" Text="Target_DT"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnTarget_DT" CausesValidation="false" runat="server">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="reqTarget_DT" ControlToValidate="rnTarget_DT" runat="server"
                                ErrorMessage="Bạn phải nhập Target_DT" ToolTip="Bạn phải nhập Target_DT">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbTarget_UPT" Text="Target_UPT"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnTarget_UPT" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbMBS_Target" Text="MBS_Target"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnMBS_Target" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbRR6_Target" Text="RR6_Target"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnRR6_Target" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbSLBill_Target" Text="SLBill_Target"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rnSLBill_Target" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Target_DT giảm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="ntxtTargetDtReduce" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label2" Text="Số ngày Target"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="ntxtDayTargetNumber" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbNote" Text="Ghi chú"></asp:Label>
                        </td>
                        <td colspan="5">
                            <tlk:RadTextBox ID="txtNote" runat="server" SkinID="Textbox1023" Height="60px" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RegularExpressionValidator ID="regexNote" runat="server" ErrorMessage="Thông tin nhập liệu có chứa mã html"
                                ControlToValidate="txtNote" ValidationExpression="^(?!.*<[^>]+>).*">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,PERIOD_YEAR,PERIOD_NAME,DAYS_TARGET_NUMBER,PERIOD_ID,STORE_NAME,TARGET_DT,TARGET_CON,TARGET_UPT,NOTE,MBS_TARGET,RR6_TARGET,SLBILL_TARGET,TARGET_GROUP,STORE_CODE,TARGET_DT_REDUCE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Năm" DataField="PERIOD_YEAR" SortExpression="PERIOD_YEAR"
                                UniqueName="PERIOD_YEAR" />
                            <tlk:GridBoundColumn HeaderText="Tháng" DataField="PERIOD_NAME" SortExpression="PERIOD_NAME"
                                UniqueName="PERIOD_NAME" />
                            <tlk:GridBoundColumn HeaderText="Nhóm Target" DataField="TARGET_GROUP_NAME" SortExpression="TARGET_GROUP_NAME"
                                UniqueName="TARGET_GROUP_NAME" />
                            <tlk:GridBoundColumn HeaderText="Mã cửa hàng" DataField="STORE_NAME" SortExpression="STORE_NAME"
                                UniqueName="STORE_NAME" />
                            <tlk:GridNumericColumn HeaderText="TARGET_DT" DataField="TARGET_DT" SortExpression="TARGET_DT"
                                UniqueName="TARGET_DT" />
                            <tlk:GridNumericColumn HeaderText="TARGET_CON" DataField="TARGET_CON" SortExpression="TARGET_CON"
                                UniqueName="TARGET_CON" />
                            <tlk:GridNumericColumn HeaderText="TARGET_UPT" DataField="TARGET_UPT" SortExpression="TARGET_UPT"
                                UniqueName="TARGET_UPT" />
                            <tlk:GridNumericColumn HeaderText="MBS_TARGET" DataField="MBS_TARGET" SortExpression="MBS_TARGET"
                                UniqueName="MBS_TARGET" />
                            <tlk:GridNumericColumn HeaderText="RR6_TARGET" DataField="RR6_TARGET" SortExpression="RR6_TARGET"
                                UniqueName="RR6_TARGET" />
                            <tlk:GridNumericColumn HeaderText="SLBILL_TARGET" DataField="SLBILL_TARGET" SortExpression="SLBILL_TARGET"
                                UniqueName="SLBILL_TARGET" />
                            <tlk:GridNumericColumn HeaderText="TARGET_DT Giảm" DataField="TARGET_DT_REDUCE" SortExpression="TARGET_DT_REDUCE"
                                UniqueName="TARGET_DT_REDUCE" />
                            <tlk:GridBoundColumn HeaderText="Số ngày target" DataField="DAYS_TARGET_NUMBER" SortExpression="DAYS_TARGET_NUMBER"
                             UniqueName="DAYS_TARGET_NUMBER"/>
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" SortExpression="NOTE"
                                UniqueName="NOTE" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                        <ClientEvents OnGridCreated="GridCreated" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Target_Store_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Target_Store_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Target_Store_RadPane2';
        var validateID = 'MainContent_ctrlPA_Target_Store_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'EXPORT') {
                var rows = $find('<%= rgMain.ClientID %>').get_masterTableView().get_dataItems().length;
                if (rows == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            } else if (args.get_item().get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate(""))
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain');
                else
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck > 1) {
                    var l = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var k = noty({ text: l, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(k.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else if (args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
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

function rbtClicked(sender, eventArgs) {
    enableAjax = false;
}
    </script>
</tlk:RadCodeBlock>

<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
