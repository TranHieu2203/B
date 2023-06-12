<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListParamSystem.ascx.vb"
    Inherits="Attendance.ctrlListParamSystem" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    @media screen and (-webkit-min-device-pixel-ratio:0) {
        #ctl00_MainContent_ctrlListParamSystem_txtNOTE
        {       
            height: 56px;
        } 
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="230px" Scrolling="None">
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdEFFECT_DATE_FROM">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdEFFECT_DATE_FROM"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalCode" ControlToValidate="rdEFFECT_DATE_FROM" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực đã tồn tại. %>"
                        ToolTip="<%$ Translate: Ngày hiệu lực đã tồn tại. %>">
                    </asp:CustomValidator>
                </td>
                 <td class="lb">
                    <%# Translate("Ngày hết hạn nghỉ bù năm trước")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdEffect_date_to_NB">
                    </tlk:RadDatePicker>
                </td>
                 <td class="lb">
                    
                </td>
                <td>
                    <asp:CheckBox runat ="server" Text ="Không hết hạn nghỉ bù năm trước" ID ="ckNoEffect_date_NB"/>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số giờ làm thêm quy đổi tối đa được thanh toán")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmHOUR_MAX_OT" SkinID="Decimal" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hết hạn phép năm trước")%>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdTO_LEAVE_YEAR">
                    </tlk:RadDatePicker>
                </td>
                <td class="lb">
                    
                </td>
                <td>
                    <asp:CheckBox runat ="server" Text ="Không hết hạn phép cũ năm trước" ID ="ckNoEffect_date"/>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số giờ làm thêm tối thiểu được tính làm thêm giờ")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="nmHOUR_CAL_OT" SkinID="Decimal" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
                <td class="lb">
                    <%# Translate("Cấp nhân sự trở xuống được thanh toán lương làm thêm")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboRANK_PAY_OT" runat="server"></tlk:RadComboBox>
                    <asp:CustomValidator ID="cvalRANK_PAY_OT" ControlToValidate="cboRANK_PAY_OT" runat="server"
                        ErrorMessage="<%$ Translate: Cấp nhân sự không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Cấp nhân sự không tồn tại hoặc đã ngừng áp dụng. %>">
                    </asp:CustomValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbOrg_id" runat ="server" Text ="Công ty"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox runat ="server" ID ="cbOrg_id" ></tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Số phép năm áp dụng")%> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rdYear" SkinID="Decimal" runat="server">
                    </tlk:RadNumericTextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdYear"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Số phép năm áp dụng. %>">
                     </asp:RequiredFieldValidator>
                </td>
                
                <td class="lb">
                    <%# Translate("Cách tính phép thâm niên")%> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rdYearTN" SkinID="Decimal" runat="server" Width ="50px">
                    </tlk:RadNumericTextBox> Năm
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdYearTN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Cách tính phép thâm niên. %>">
                     </asp:RequiredFieldValidator>

                     <tlk:RadNumericTextBox ID="rdDayTN" SkinID="Decimal" runat="server" Width ="50px">
                    </tlk:RadNumericTextBox> Ngày
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="rdDayTN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập Cách tính phép thâm niên. %>">
                     </asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Số ngày nghỉ phép tối đa trong tháng")%> <span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="txtAbsentDayLimit" SkinID="Decimal" runat="server"
                        Width="50px" MinValue="0.5"></tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtNOTE" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,CODE,NAME,EFFECT_DATE_FROM,EFFECT_DATE_TO_NB,
                TO_LEAVE_YEAR,RANK_PAY_OT_NAME,HOUR_CAL_OT,HOUR_MAX_OT,CREATE_BY_SHOW,CREATE_DATE_SHOW,ACTFLG,
                NOTE,YEAR_P,YEAR_TN,DAY_TN,ORG_ID,NO_EFFECT_ENT,NO_EFFECT_NB">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                     <tlk:GridBoundColumn HeaderText="Công ty" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE_FROM"
                        UniqueName="EFFECT_DATE_FROM" SortExpression="EFFECT_DATE_FROM" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hạn nghỉ bù %>" DataField="EFFECT_DATE_TO_NB"
                        UniqueName="EFFECT_DATE_TO_NB" SortExpression="EFFECT_DATE_TO_NB" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hạn phép năm trước %>"
                        DataField="TO_LEAVE_YEAR" UniqueName="TO_LEAVE_YEAR" SortExpression="TO_LEAVE_YEAR"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự trở xuống được thanh toán lương làm thêm %>"
                        DataField="RANK_PAY_OT_NAME" UniqueName="RANK_PAY_OT_NAME" SortExpression="RANK_PAY_OT_NAME" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số giờ làm thêm tối thiểu được tính làm thêm giờ %>"
                        DataField="HOUR_CAL_OT" UniqueName="HOUR_CAL_OT" SortExpression="HOUR_CAL_OT" />
                    <tlk:GridNumericColumn HeaderText="<%$ Translate: Số giờ làm thêm quy đổi tối đa được thanh toán %>"
                        DataField="HOUR_MAX_OT" UniqueName="HOUR_MAX_OT" SortExpression="HOUR_MAX_OT" />
                     <tlk:GridNumericColumn HeaderText="<%$ Translate: Số phép năm áp dụng %>"
                        DataField="YEAR_P" UniqueName="YEAR_P" SortExpression="YEAR_P" />
                     <tlk:GridNumericColumn HeaderText="<%$ Translate: Cách tính tham niên theo năm %>"
                        DataField="YEAR_TN" UniqueName="YEAR_TN" SortExpression="YEAR_TN" />
                     <tlk:GridNumericColumn HeaderText="<%$ Translate: Số ngày được tính tham niên theo năm %>"
                        DataField="DAY_TN" UniqueName="DAY_TN" SortExpression="DAY_TN" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Người tạo %>" DataField="CREATE_BY_SHOW"
                        UniqueName="CREATE_BY_SHOW" SortExpression="CREATE_BY_SHOW" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tạo %>" DataField="CREATE_DATE_SHOW"
                        UniqueName="CREATE_DATE_SHOW" SortExpression="CREATE_DATE_SHOW" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="120px" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày nghỉ tối đa trong tháng %>" DataField="ABSENT_DAY_LIMIT"
                        UniqueName="ABSENT_DAY_LIMIT" SortExpression="ABSENT_DAY_LIMIT" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE">
                        <HeaderStyle Width="200px" />
                        <ItemStyle Width="200px" />
                    </tlk:GridBoundColumn>
                    <tlk:GridCheckBoxColumn HeaderText="Không hết hạn phép cũ năm trước" DataField="NO_EFFECT_ENT"
                        UniqueName="NO_EFFECT_ENT" SortExpression="NO_EFFECT_ENT"> </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="Không hết hạn nghỉ bù năm trước" DataField="NO_EFFECT_NB"
                        UniqueName="NO_EFFECT_NB" SortExpression="NO_EFFECT_NB"> </tlk:GridCheckBoxColumn>
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
             <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var splitterID = 'ctl00_MainContent_ctrlListParamSystem_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlListParamSystem_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlListParamSystem_RadPane2';
        var validateID = 'MainContent_ctrlListParamSystem_valSum';
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
            } else if (args.get_item().get_commandName() == "SAVE") {
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

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgDanhMuc');
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlListParamSystem_rdTO_LEAVE_YEAR_dateInput').val('');
            $('#ctl00_MainContent_ctrlListParamSystem_rdEffect_date_to_NB_dateInput').val('');
            $('#ctl00_MainContent_ctrlListParamSystem_rdEFFECT_DATE_FROM_dateInput').val('');
        }

        function RowClick(sender, eventArgs) {
            var grid = sender;
            var MasterTable = grid.get_masterTableView();
            var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
            var cell = MasterTable.getCellByColumnUniqueName(row, "NOTE");

            var item_txt = $find('<%= txtNOTE.ClientID %>')
            item_txt.set_value(cell.innerHTML);
        }
    </script>
</tlk:RadCodeBlock>
