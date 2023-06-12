<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Classification.ascx.vb"
    Inherits="Payroll.ctrlPA_Classification" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="100px" Scrolling="None">
        <tlk:RadToolBar ID="tbarMenu" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Mã xếp loại")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtCode" runat="server">
                    </tlk:RadTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtCode" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Mã xếp loại. %>" ToolTip="<%$ Translate: Bạn phải nhập Mã xếp loại. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Tên xếp loại")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtName" runat="server">
                    </tlk:RadTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtName" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Tên xếp loại. %>" ToolTip="<%$ Translate: Bạn phải nhập Tên xếp loại. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                    </tlk:RadDatePicker>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="rdEffectDate" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>" ToolTip="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Khoảng từ")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmValueFrom" runat="server">
                    </tlk:RadNumericTextBox>
                    %
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rnmValueFrom" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập khoảng từ. %>" ToolTip="<%$ Translate: Bạn phải nhập khoảng từ. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Khoảng đến")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmValueTo" runat="server">
                    </tlk:RadNumericTextBox>
                    %
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rnmValueTo" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập khoảng đến. %>" ToolTip="<%$ Translate: Bạn phải nhập khoảng đến. %>"></asp:RequiredFieldValidator>
                     <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rnmValueTo"
                        Type="Double" ControlToCompare="rnmValueFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Khoảng đến phải lớn hơn khoảng từ %>"
                        ToolTip="<%$ Translate: Khoảng đến phải lớn hơn khoảng từ %>"></asp:CompareValidator>
                </td>
                <td class="lb">
                    <%# Translate("Hệ số")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnFactor" runat="server" SkinID="Decimal">
                    </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rnFactor" runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Hệ số. %>" ToolTip="<%$ Translate: Bạn phải nhập Hệ số. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME,FACTOR,VALUE_FROM,VALUE_TO,EFFECT_DATE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã xếp loại %>" DataField="CODE" SortExpression="CODE"
                        UniqueName="CODE" HeaderStyle-Width="120px"/>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên xếp loại %>" DataField="NAME" SortExpression="NAME"
                        UniqueName="NAME" HeaderStyle-Width="120px"/>
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Từ %>" DataField="VALUE_FROM" SortExpression="VALUE_FROM"
                        UniqueName="VALUE_FROM" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Đến %>" DataField="VALUE_TO" SortExpression="VALUE_TO"
                        UniqueName="VALUE_TO" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số %>" DataField="FACTOR" SortExpression="FACTOR"
                        UniqueName="FACTOR" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE"
                        UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="EFFECT_DATE" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else if (item.get_commandName() == "EDIT") {
                var items = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems();
                if (items.length == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                } else if (items.length > 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
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
        }
    </script>
</tlk:RadCodeBlock>
