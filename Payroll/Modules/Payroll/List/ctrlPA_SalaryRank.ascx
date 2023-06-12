<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_SalaryRank.ascx.vb"
    Inherits="Payroll.ctrlPA_SalaryRank" %>
<%@ Import Namespace="Framework.UI" %>
<%@ Import Namespace="Common" %>

<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryRanks" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Bảng lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryGroup" runat="server" AutoPostBack="true" CausesValidation="false">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="cusSalaryGroup" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Bảng lương %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Bảng lương  %>" ClientValidationFunction="cusSalaryGroup">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngạch lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboSalaryLevel" runat="server">
                    </tlk:RadComboBox>
                    <asp:CustomValidator ID="reqSalaryLevel" runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Nhóm lương %>"
                        ToolTip="<%$ Translate: Bạn phải chọn Nhóm lương  %>" ClientValidationFunction="cusSalaryLevel">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cusSalaryLevel" ControlToValidate="cboSalaryLevel" runat="server" 
                        ErrorMessage="<%$ Translate: Ngạch lương không tồn tại hoặc đã ngừng áp dụng. %>" ToolTip="<%$ Translate: Ngạch lương không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb" id="tdSLlbOrders" style="width: 80px">
                    <label id="lbOrders">
                        <%# Translate("Thứ tự")%></label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox runat="server" ID="rntxtOrders" MinValue="1" Width="60px"
                        Value="1" ShowSpinButtons="True" ReadOnly="true" CausesValidation="false">
                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rntxtOrders"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập số thứ tự. %>" ToolTip="<%$ Translate: Bạn phải nhập số thứ tự. %>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Bậc lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtRank" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqRank" ControlToValidate="txtRank" runat="server"
                        ErrorMessage="<%$ Translate: Bạn phải nhập bậc lương %>" ToolTip="<%$ Translate: Bạn phải nhập bậc lương %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="txtRank" runat="server" ErrorMessage="<%$ Translate: Bậc lương theo bảng lương và ngạch lương đã tồn tại. %>"
                        ToolTip="<%$ Translate: Bậc lương theo bảng lương và ngạch lương đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                <%--<td colspan="2">
                    <div runat="server" id="divSalaryBasic">
                        <div style="float: left;padding-right: 6px;padding-left: 5px;text-align: right; width: 30%">
                            <label> <%# Translate("Số tiền/ Hệ số")%></label><span class="lbReq">*</span>
                        </div>
                        <div style="float: left;">
                            <tlk:RadNumericTextBox ID="rntxtSalaryBasic" runat="server" ShowSpinButtons="true"
                                MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSizes="3">
                            </tlk:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtSalaryBasic"
                                runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số tiền %>" ToolTip="<%$ Translate: Bạn phải nhập Số tiền %>">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </td>--%>
                <td class="lb">
                    <%# Translate("Mức lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <%--<tlk:RadTextBox ID="rntxtSalaryBasic" runat="server" Style="text-align: right">
                        <ClientEvents OnKeyPress="keyPress" OnBlur="OnBlur" />
                    </tlk:RadTextBox>--%>
                    <tlk:RadNumericTextBox ID="rntxtSalaryBasic" runat="server" SkinID="Decimal" NumberFormat-AllowRounding="false" NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rntxtSalaryBasic"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Mức lương %>" ToolTip="<%$ Translate: Bạn phải nhập Mức lương %>">
                    </asp:RequiredFieldValidator>
                </td>

                <td class="lb">
                    <%# Translate("Hệ số")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtheso" runat="server" SkinID="Decimal" NumberFormat-AllowRounding="false" NumberFormat-DecimalDigits="2">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtheso"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Hệ số %>" ToolTip="<%$ Translate: Bạn phải nhập Hệ số %>">
                    </asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="txtRemark" runat="server" SkinID="Textbox1023" Width="100%" Height="40px">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true"
            AllowFilteringByColumn="true">
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="SAL_LEVEL_ID, RANK, HE_SO,SALARY_BASIC, REMARK,ORDERS">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bảng lương/thưởng HQCV %>" DataField="SAL_GROUP_NAME"
                        SortExpression="SAL_GROUP_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngạch lương/thưởng HQCV %>" DataField="SAL_LEVEL_NAME"
                        SortExpression="SAL_LEVEL_NAME" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        AndCurrentFilterFunction="Contains" UniqueName="SAL_LEVEL_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Bậc lương/thưởng HQCV %>" DataField="RANK"
                        SortExpression="RANK" AutoPostBackOnFilter="true" ShowFilterIcon="false" AndCurrentFilterFunction="EqualTo"
                        UniqueName="RANK" NumericType="Number">
                    </tlk:GridNumericColumn>
                    <tlk:GridNumericColumn DataField="SALARY_BASIC" HeaderText="<%$ Translate: Mức lương %>"
                        SortExpression="SALARY_BASIC" UniqueName="SALARY_BASIC" ShowFilterIcon="false"
                        Display="True" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Visible="True"
                        DataFormatString="{0:#.###}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" />
                    </tlk:GridNumericColumn>

                    <tlk:GridNumericColumn DataField="HE_SO" HeaderText="<%$ Translate: Hệ số %>"
                        SortExpression="HE_SO" UniqueName="HE_SO" ShowFilterIcon="false"
                        Display="True" AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" Visible="True"
                        DataFormatString="{0:#.###}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="right" />
                    </tlk:GridNumericColumn>

                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Thứ tự %>" DataField="ORDERS" SortExpression="ORDERS"
                        UniqueName="ORDERS">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Right" />
                    </tlk:GridNumericColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" AutoPostBackOnFilter="true"
                        ShowFilterIcon="false" AndCurrentFilterFunction="Contains" SortExpression="REMARK"
                        UniqueName="REMARK" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        SortExpression="ACTFLG" UniqueName="ACTFLG" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctl00_MainContent_PagePlaceHolderPanel';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SalaryRank_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_SalaryRank_RadPane2';
        var validateID = 'MainContent_ctrlPA_SalaryRank_ValidationSummary1';
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
                var rows = $find('<%= rgData.ClientID %>').get_masterTableView().get_dataItems().length;
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
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData');
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

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();

            if (!text.match('^[0-9.]+$'))
                args.set_cancel(true);
        }

        function OnBlur(sender, args) {
            //sender.set_value(sender.get_value());
            try {
                var value = parseFloat(sender.get_value());
                var ctl = $find("<%= rntxtSalaryBasic.ClientID%>");
                ctl.set_value(value);
            }
            catch (err) {
                sender.set_value(0);
            }
        }

        function cusSalaryGroup(oSrc, args) {
            var cbo = $find("<%# cboSalaryGroup.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function cusSalaryLevel(oSrc, args) {
            var cbo = $find("<%# cboSalaryLevel.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }
    </script>
</tlk:RadCodeBlock>
