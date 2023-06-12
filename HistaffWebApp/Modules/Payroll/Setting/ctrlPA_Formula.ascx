<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPA_Formula.ascx.vb"
    Inherits="Payroll.ctrlPA_Formula" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<style type="text/css">
   @media screen and (-webkit-min-device-pixel-ratio:0) {
        #ctl00_MainContent_ctrlPA_Formula_txtDesc    
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
    #ctl00_MainContent_ctrlPA_Formula_rgData_ctl00_ctl02_ctl02_FilterTextBox_STATUS
    {
        display : none;
    }
</style>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="228px" Scrolling="None">
        <tlk:RadToolBar ID="tbarSalaryGroups" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <%# Translate("Đối tượng lương")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadComboBox ID="rcboObjSalary" runat="server">
                    </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqObjSalary" ControlToValidate="rcboObjSalary"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn Đối tượng lương %>" ToolTip="<%$ Translate: Bạn phải chọn Đối tượng lương %>">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvalObjSalary" runat="server" ControlToValidate="rcboObjSalary"
                        ErrorMessage="<%$ Translate: Đối tượng lương không tồn tại hoặc đã ngừng áp dụng. %>"
                        ToolTip="<%$ Translate: Đối tượng lương không tồn tại hoặc đã ngừng áp dụng. %>" >
                    </asp:CustomValidator>
                </td>
                <td> <%# Translate("Tên công thức")%><span class="lbReq">*</span></td>
                <td><tlk:RadTextBox ID="rtxtNameVN" runat="server">
                    </tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqNameVN" ControlToValidate="rtxtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhóm lương %>" ToolTip="<%$ Translate: Bạn phải nhập nhóm lương %>">
                    </asp:RequiredFieldValidator>
                </td>                    
            </tr>
            <tr>
              <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpStartDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqStartDate" ControlToValidate="rdpStartDate"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"
                        ToolTip="<%$ Translate: Bạn phải nhập ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày kết thúc")%>
                </td>
                <td>
                    <tlk:RadDatePicker ID="rdpEndDate" runat="server">
                    </tlk:RadDatePicker>
                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="rdpEndDate"
                        ControlToCompare="rdpStartDate" Operator="GreaterThanEqual" Type="Date" 
                        ErrorMessage="Ngày kết thúc phải lớn hơn ngày hiệu lực" />
                </td>
            </tr>            
            <tr>
                <td><%# Translate("Mã thưởng")%><span class="lbReq">*</span></td>
                <td>
                    <tlk:RadTextBox ID="rtAward_Code" runat="server" MaxLength="100">
                    </tlk:RadTextBox>
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rtxtNameVN"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải nhập nhóm lương %>" ToolTip="<%$ Translate: Bạn phải nhập nhóm lương %>">
                    </asp:RequiredFieldValidator>--%>
                </td>
                <td class="lb">
                    <%# Translate("Thứ tự")%>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="rnmIdx" runat="server" Width="60px" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" MinValue="1" NumberFormat-AllowRounding="false">
                    </tlk:RadNumericTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Ghi chú")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="txtDesc" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
<tr>
                <td class="lb">
                    <%# Translate("Sao chép tới")%>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID="rcboCopyObjSalary" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
             <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
             </ClientSettings>
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="OBJ_SAL_ID,ID,NAME_VN,NAME_EN,START_DATE,END_DATE,STATUS,SDESC,IDX,AWARD_CODE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">                        
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="OBJ_SAL_ID" Visible="false" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên công thức %>" DataField="NAME_VN" SortExpression="SALARY_GROUP_NAME" UniqueName="SALARY_GROUP_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã thưởng %>" DataField="AWARD_CODE" SortExpression="AWARD_CODE" UniqueName="AWARD_CODE" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng lương %>" DataField="OBJ_SAL_NAME" SortExpression="OBJ_SAL_NAME" UniqueName="OBJ_SAL_NAME" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="START_DATE" UniqueName="START_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="START_DATE" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc %>" DataField="END_DATE" UniqueName="END_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="END_DATE" CurrentFilterFunction="EqualTo">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>   
                    <tlk:GridTemplateColumn UniqueName="STATUS" DataField="STATUS" HeaderText="<%$ Translate: Trạng thái %>"
                        SortExpression="STATUS" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="ChkSTATUS" runat="server" Readonly="true" Checked='<%# IIf(DataBinder.Eval(Container.DataItem,"STATUS").ToString() <> "0",true,false) %>' />
                        </ItemTemplate>
                    </tlk:GridTemplateColumn>          
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true" AllowKeyboardNavigation="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnRowDblClick="gridRowDblClick" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
                <KeyboardNavigationSettings AllowSubmitOnEnter="true" EnableKeyboardShortcuts="true" />
            </ClientSettings>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px"  EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin thiết lập công thức lương%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_ctrlPA_Formula_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Formula_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlPA_Formula_RadPane2';
        var validateID = 'MainContent_ctrlPA_Formula_valSum';
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

        function cusObjSalary(oSrc, args) {
            var cbo = $find("<%# rcboObjSalary.ClientID %>");
            args.IsValid = (cbo.get_value().length != 0);
        }

        function CheckValidate() {
            var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            return 0;
        }

        function OpenEdit() {
            var id = $find('<%= rgData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('/Default.aspx?mid=Payroll&fid=ctrlPA_FormulaDetail&group=Setting&gUID=' + id + '', "_self"); /*
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = CheckValidate();
            var n;
            var m;
            if (bCheck == 0) {
                OpenEdit();
            }
            if (bCheck == 1) {
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            args.set_cancel(true);

        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
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
            }

            if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
                }
                else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clRadDatePicker() {
            $('#ctl00_MainContent_ctrlPA_Formula_rdpStartDate_dateInput').val('');
            $('#ctl00_MainContent_ctrlPA_Formula_rdpEndDate_dateInput').val('');
        }

        function setDefaultSize() {
            ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgData', 0, 0, 7);
        }

    </script>
</tlk:RadCodeBlock>
