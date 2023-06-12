<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_TaxIncomeYear.ascx.vb"
    Inherits="Payroll.ctrlPA_TaxIncomeYear" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="260" Width="260px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrganization" runat="server" CheckBoxes="All" CheckChildNodes="true" AutoPostBack="false" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="150px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" />
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbYear" Text="Năm QTT"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" ToolTip="" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <b style="color: red">
                                <%# Translate("Thông tin chung")%>
                            </b>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYearAdd" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Tháng QTT")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnAdd" Text="<%$ Translate: Điền nhanh%>" runat="server">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMain" runat="server" AutoGenerateColumns="false"
                    AllowPaging="True" Height="100%" AllowSorting="True" AllowMultiRowSelection="true" AllowMultiRowEdit="true">
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID" ClientDataKeyNames="ID,EMPLOYEE_ID" EditMode="InPlace">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridCheckBoxColumn HeaderText="Đã khóa" AllowFiltering="false" DataField="IS_LOCK" SortExpression="IS_LOCK"
                                UniqueName="IS_LOCK" ReadOnly="true" HeaderStyle-Width="65px" />
                            <tlk:GridBoundColumn HeaderText="Mã số thuế" DataField="PIT_CODE" SortExpression="PIT_CODE"
                                UniqueName="PIT_CODE" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Năm" DataField="YEAR" SortExpression="YEAR"
                                UniqueName="YEAR" ReadOnly="true" HeaderStyle-Width="65px" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" ReadOnly="true" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế năm" DataField="CHIUTHUE_YEAR"
                                SortExpression="CHIUTHUE_YEAR" UniqueName="CHIUTHUE_YEAR" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập thuế TNCN năm" DataField="THUETNCN_YEAR"
                                SortExpression="THUETNCN_YEAR" UniqueName="THUETNCN_YEAR" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập chịu thuế bổ sung" DataField="CHIUTHUE_BOSUNG"
                                SortExpression="CHIUTHUE_BOSUNG" UniqueName="CHIUTHUE_BOSUNG" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tiền thuế TNCN bổ sung" DataField="THUETNCN_BOSUNG"
                                SortExpression="THUETNCN_BOSUNG" UniqueName="THUETNCN_BOSUNG" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng thu nhập chịu thuế" DataField="THUNHAP_CHIUTHUE"
                                SortExpression="THUNHAP_CHIUTHUE" UniqueName="THUNHAP_CHIUTHUE" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng thuế TNCN đã đóng" DataField="TOTAL_THUE_TNCN"
                                SortExpression="TOTAL_THUE_TNCN" UniqueName="TOTAL_THUE_TNCN" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền bảo hiểm" DataField="BHXH"
                                SortExpression="BHXH" UniqueName="BHXH" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Định mức GTBT" DataField="DM_GTBT"
                                SortExpression="DM_GTBT" UniqueName="DM_GTBT" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Số NPT" DataField="NPT"
                                SortExpression="NPT" UniqueName="NPT" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Số tháng phụ thuộc" DataField="NPT_MONTH"
                                SortExpression="NPT_MONTH" UniqueName="NPT_MONTH" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Định mức GTNPT" DataField="DM_NPT"
                                SortExpression="DM_NPT" UniqueName="DM_NPT" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng tiền GTGC" DataField="GTGC"
                                SortExpression="GTGC" UniqueName="GTGC" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thu nhập tính thuế" DataField="THUNHAP_QTT"
                                SortExpression="THUNHAP_QTT" UniqueName="THUNHAP_QTT" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thuế TNCN quyết toán" DataField="THUETNCN_QTT"
                                SortExpression="THUETNCN_QTT" UniqueName="THUETNCN_QTT" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thuế TNCN phải nộp thêm" DataField="THUETNCN_PAY"
                                SortExpression="THUETNCN_PAY" UniqueName="THUETNCN_PAY" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Thuế TNCN hoàn lại" DataField="THUETNCN_REFUND"
                                SortExpression="THUETNCN_REFUND" UniqueName="THUETNCN_REFUND" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                            <tlk:GridNumericColumn HeaderText="Tháng lương QTT" DataField="PERIOD_NAME"
                                SortExpression="PERIOD_NAME" UniqueName="PERIOD_NAME" ReadOnly="true" HeaderStyle-Width="150px" DataFormatString="{0:N0}" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="false">
                        <ClientEvents OnGridCreated="GridCreated" />
                        <Scrolling AllowScroll="true" />
                    </ClientSettings>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlupload id="ctrlUpload1" runat="server" />
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_TaxIncomeYear_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_TaxIncomeYear_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_TaxIncomeYear_RadPane2';
        var validateID = 'MainContent_ctrlPA_TaxIncomeYear_valSum';
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
