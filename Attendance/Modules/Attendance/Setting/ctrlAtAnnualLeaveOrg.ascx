<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAtAnnualLeaveOrg.ascx.vb"
    Inherits="Attendance.ctrlAtAnnualLeaveOrg" %>
<%@ Import Namespace="Common" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="220px" Scrolling="Y" >
        <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidOrg" runat="server" />
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
       <table class="table-form">
            <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label ID="Label1" runat="server" Text="Bộ phận"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox runat="server" ID="txtOrgName" Width="130px" SkinID="Readonly" ReadOnly="true" />
                    <tlk:RadButton runat="server" ID="btnFindOrg" SkinID="ButtonView" CausesValidation="false" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Bạn phải chọn đơn vị %>" ToolTip="<%$ Translate: Bạn phải chọn đơn vị %>"> </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbObjectEmp" Text="Loại nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjectEmp" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
           <tr>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="Label2" Text="Cấp bậc"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboGrade" runat="server">
                    </tlk:RadComboBox>
                </td>
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="Label3" Text="Đối tượng nhân viên"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboObjEmp" runat="server">
                    </tlk:RadComboBox>
                </td>
           </tr>
            <tr>   
                <td class="lb" style="width: 180px">
                    <asp:Label runat="server" ID="lbTitleName" Text="Vị trí công việc"></asp:Label>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboTitle" runat="server">
                    </tlk:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Phép chuẩn")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnualLeave" runat="server">
                    </tlk:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ntxtAnnualLeave"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Phép chuẩn. %>"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadDatePicker runat="server" ID="rdFromdateEffect">
                    </tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rdFromdateEffect"
                        runat="server" Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập Ngày hiệu lực. %>"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvDate" ControlToValidate="rdFromdateEffect" runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực đã tồn tại. %>"
                        ToolTip="<%$ Translate: Ngày hiệu lực đã tồn tại. %>"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <%# Translate("Mô tả")%>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rdNote" runat="server" SkinID="Textbox1023" Width="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
        </table>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" 
        style="margin-top: 11px">
        <tlk:RadGrid PageSize=50 ID="rgDanhMuc" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnGridCreated="GridCreated" />
                <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,ORG_ID,ORG_NAME,TITLE_ID,TITLE_NAME,ANNUALLEAVE,FROMDATE_EFFECT,NOTE,OBJ_EMPLOYEE_ID,OBJ_EMPLOYEE_NAME,OBJECT_EMPLOYEE_ID,OBJECT_EMPLOYEE_NAME,GRADE_ID,GRADE_NAME">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Bộ phận %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                        SortExpression="ORG_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nhân viên %>" DataField="OBJECT_EMPLOYEE_NAME" UniqueName="OBJECT_EMPLOYEE_NAME"
                        SortExpression="OBJECT_EMPLOYEE_NAME">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp bậc %>" DataField="GRADE_NAME" UniqueName="GRADE_NAME"
                        SortExpression="GRADE_NAME">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Đối tượng nhân viên %>" DataField="OBJ_EMPLOYEE_NAME" UniqueName="OBJ_EMPLOYEE_NAME"
                        SortExpression="OBJ_EMPLOYEE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="TITLE_NAME" UniqueName="TITLE_NAME"
                        SortExpression="TITLE_NAME">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép chuẩn %>" DataField="ANNUALLEAVE" UniqueName="ANNUALLEAVE"
                        SortExpression="ANNUALLEAVE">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="FROMDATE_EFFECT"
                        DataFormatString="{0:dd/MM/yyyy}" DataType="System.DateTime" UniqueName="FROMDATE_EFFECT"
                        SortExpression="FROMDATE_EFFECT" CurrentFilterFunction="EqualTo">
                        <HeaderStyle Width="80px" />
                        </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="NOTE" UniqueName="NOTE"
                        SortExpression="NOTE" />
                </Columns>
                <HeaderStyle Width="100px" />
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phFindOrg" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlAtAnnualLeaveOrg_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAtAnnualLeaveOrg_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAtAnnualLeaveOrg_RadPane2';
        var validateID = 'MainContent_ctrlAtAnnualLeaveOrg_valSum';
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


    </script>
</tlk:RadCodeBlock>
