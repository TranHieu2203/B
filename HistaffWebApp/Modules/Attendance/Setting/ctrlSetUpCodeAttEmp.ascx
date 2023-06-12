<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlSetUpCodeAttEmp.ascx.vb"
    Inherits="Attendance.ctrlSetUpCodeAttEmp" %>
<%@ Import Namespace="Common" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="250px" Scrolling="Y" >
        <asp:HiddenField ID="hiEMPLOYEE_ID" runat="server" />
        <asp:HiddenField ID="hiORG_ID" runat="server" />
        <asp:HiddenField ID="hiTITLE_ID" runat="server" />
        <asp:HiddenField ID="hiMACHINE_ID" runat="server" />
        <asp:HiddenField ID="hidID" runat="server" />
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
        <table class="table-form">
            <tr>
                <td class="lb">
                    <asp:Label ID="lbEMPLOYEE_CODE" runat ="server"  Text ="Mã nhân viên"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                   <tlk:RadTextBox ID="rtEMPLOYEE_CODE" runat="server" SkinID="ReadOnly" Width="130px" ReadOnly="true">
                    </tlk:RadTextBox>
                    <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqEMPLOYEE_CODE" ControlToValidate="rtEMPLOYEE_CODE"
                        runat="server" Text="*" ErrorMessage="Bạn phải chọn nhân viên"></asp:RequiredFieldValidator>
                </td>
                <td class="lb">
                    <asp:Label ID="lbEMPLOYEE_NAME" runat ="server"  Text ="Họ tên"></asp:Label>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtEMPLOYEE_NAME" ReadOnly ="true" SkinID="Readonly" runat="server" Width ="100%">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbORG_ID" runat ="server"  Text ="Đơn vị"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="rtORG_ID" runat="server" SkinID="ReadOnly" Width="100%" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbTITLE_ID" runat ="server"  Text ="Vị trí công việc"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="rtTITLE_ID" runat="server" SkinID="ReadOnly" Width="100%" ReadOnly="true">
                    </tlk:RadTextBox>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbMACHINE_CODE" runat ="server"  Text ="Máy chấm công"></asp:Label><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadComboBox ID ="cbMACHINE_CODE" runat ="server"  Width="100%" ></tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqMACHINE_CODE" ControlToValidate="cbMACHINE_CODE"
                        runat="server" Text="*" ErrorMessage="Bạn phải chọn máy chấm công"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbCODE_ATT" runat ="server"  Text ="Mã chấm công"></asp:Label><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadTextBox ID="rtCODE_ATT" runat ="server" Width="100%" ></tlk:RadTextBox>
                    <asp:RequiredFieldValidator ID="reqCODE_ATT" ControlToValidate="rtCODE_ATT"
                        runat="server" Text="*" ErrorMessage="Bạn phải chọn mã chấm công">
                    </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cvaCODE_ATT" ControlToValidate="rtCODE_ATT" runat="server"
                        ErrorMessage="Mã chấm công đã tồn tại, vui lòng kiểm tra lại!" ToolTip="Mã chấm công đã tồn tại, vui lòng kiểm tra lại!">
                    </asp:CustomValidator>
                </td>
                 <td class="lb">
                    <asp:Label ID="lbAPPROVE_DATE" runat ="server"  Text ="Ngày áp dụng"></asp:Label><span class="lbReq">*</span>
                </td>
                <td colspan="3">
                    <tlk:RadDatePicker ID="rdAPPROVE_DATE" runat ="server"  Width="100%" ></tlk:RadDatePicker>
                    <asp:RequiredFieldValidator ID="reqAPPROVE_DATE" ControlToValidate="rdAPPROVE_DATE"
                        runat="server" Text="*" ErrorMessage="Bạn phải chọn ngày áp dụng">
                     </asp:RequiredFieldValidator>
                     <asp:CustomValidator ID="cvaAPPROVE_DATE" ControlToValidate="rdAPPROVE_DATE" runat="server"
                        ErrorMessage="Không được nhập trùng ngày hiệu lực trong cùng một hệ thống, vui lòng kiểm tra lại!" ToolTip="Không được nhập trùng ngày hiệu lực trong cùng một hệ thống, vui lòng kiểm tra lại!">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="lb">
                    <asp:Label ID="lbNOTE" runat ="server" Text ="Mô tả"></asp:Label>
                </td>
                <td colspan="5">
                    <tlk:RadTextBox ID="rtNOTE" runat ="server"  Width ="100%" 
                        TextMode="MultiLine" ></tlk:RadTextBox>
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
            <MasterTableView DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_ID,ORG_NAME,TITLE_ID,TITLE_NAME
            ,CODE_ATT,MACHINE_NAME,MACHINE_ID,APPROVE_DATE,NOTE">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" />
                    <tlk:GridBoundColumn HeaderText="Họ tên" DataField="EMPLOYEE_NAME"
                        UniqueName="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME"
                        UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                    <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME"
                        UniqueName="ORG_NAME" SortExpression="ORG_NAME" />
                    <tlk:GridBoundColumn HeaderText="Máy chấm công" DataField="MACHINE_NAME"
                        UniqueName="MACHINE_NAME" SortExpression="MACHINE_NAME" />
                     <tlk:GridBoundColumn HeaderText="Mã chấm công" DataField="CODE_ATT"
                        UniqueName="CODE_ATT" SortExpression="CODE_ATT" />
                    <tlk:GridBoundColumn HeaderText="Ngày áp dụng" DataField="APPROVE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="APPROVE_DATE" UniqueName="APPROVE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
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
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var splitterID = 'ctl00_MainContent_ctrlATTimeManual_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlATTimeManual_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlATTimeManual_RadPane2';
        var validateID = 'MainContent_ctrlATTimeManual_valSum';
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
