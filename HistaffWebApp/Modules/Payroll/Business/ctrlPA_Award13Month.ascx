<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Award13Month.ascx.vb"
    Inherits="Payroll.ctrlPA_Award13Month" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrganization" runat="server" checkboxes="All" checkchildnodes="true" autopostback="false" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="170px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbYear" Text="Năm"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cbYear" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="item-head" colspan="6">
                            <%# Translate("Thông tin chung")%>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label1" Text="Năm"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYearAllow" runat="server" AutoPostBack="true" CausesValidation="false">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="Label2" Text="Tháng chi thưởng"></asp:Label><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriodAllow" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnAutoFill" Text="<%$ Translate: Điền nhanh%>" runat="server" ToolTip="">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true" AllowMultiRowEdit="true">
                    <MasterTableView DataKeyNames="ID,PERIOD_ID,EMPLOYEE_ID" ClientDataKeyNames="ID,PERIOD_ID,EMPLOYEE_ID" EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridCheckBoxColumn HeaderText="Trạng thái khóa" AllowFiltering="false" DataField="IS_LOCK" SortExpression="IS_LOCK"
                                UniqueName="IS_LOCK" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Năm" DataField="AWARD_YEAR" SortExpression="AWARD_YEAR" DataFormatString="{0:####}"
                                UniqueName="AWARD_YEAR" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Lương cơ bản" DataField="SALARY_AWARD" SortExpression="SALARY_AWARD"
                                UniqueName="SALARY_AWARD" DataFormatString="{0:N1}" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Trợ cấp trách nhiệm" DataField="SALARY_LIABILITY" SortExpression="SALARY_LIABILITY"
                                UniqueName="SALARY_LIABILITY" DataFormatString="{0:N1}" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Thâm niên trong năm" DataField="SENIORITY_MONTH" SortExpression="SENIORITY_MONTH"
                                UniqueName="SENIORITY_MONTH" DataFormatString="{0:N0}" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Số ngày NKL" DataField="NONSALARY_UNIT" SortExpression="NONSALARY_UNIT"
                                UniqueName="NONSALARY_UNIT" DataFormatString="{0:N1}" ReadOnly="true" />
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Số ngày NKL điều chỉnh %>" UniqueName="NONSALARY_UNITEDIT" HeaderStyle-Width="150px">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "NONSALARY_UNITEDIT")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tlk:RadNumericTextBox ID="rnNONSALARY_UNITEDIT" runat="server" SkinID="Decimal" Width="95%">
                                    </tlk:RadNumericTextBox>
                                </EditItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridNumericColumn HeaderText="Số tháng tính thưởng" DataField="SENIORITY_REALIRY" SortExpression="SENIORITY_REALIRY"
                                UniqueName="SENIORITY_REALIRY" DataFormatString="{0:N0}" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Tiền thưởng" DataField="AWARD_13MONTH" SortExpression="AWARD_13MONTH"
                                UniqueName="AWARD_13MONTH" DataFormatString="{0:N1}" ReadOnly="true" />
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Ghi chú %>" UniqueName="NOTE" HeaderStyle-Width="200px">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "NOTE")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <tlk:RadTextBox ID="txtNOTE" runat="server">
                                    </tlk:RadTextBox>
                                </EditItemTemplate>
                            </tlk:GridTemplateColumn>
                            <tlk:GridBoundColumn HeaderText="Tháng chi thưởng" DataField="PERIOD_NAME" SortExpression="PERIOD_NAME"
                                UniqueName="PERIOD_NAME" ReadOnly="true"/>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Award13Month_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Award13Month_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Award13Month_RadPane2';
        var validateID = 'MainContent_ctrlPA_Award13Month_valSum';
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

            } else if (args.get_item().get_commandName() == "EDIT") {
                ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;

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
