<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAtSetupELeave.ascx.vb"
    Inherits="Attendance.ctrlAtSetupELeave" %>
<%@ Import Namespace="Common" %>
<link type  = "text/css" href = "/Styles/StyleCustom.css" rel = "Stylesheet"/>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="330px" Scrolling="Y" >
        <asp:HiddenField ID="hidID" runat="server" />
        <tlk:RadToolBar ID="tbarCostCenters" runat="server" />
        <asp:ValidationSummary ID="valSum" runat="server" />
       <table class="table-form">
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label4" Text="Năm hiệu lực"></asp:Label><span class="lbReq">*</span>
                </td>
                <td>
                   <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" TabIndex="12" Width="80px">
                   </tlk:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqcboYear" ControlToValidate="cboYear"
                        runat="server" ErrorMessage="Bạn phải chọn Năm hiệu lực " ToolTip="Bạn phải chọn Năm hiệu lực"> 
                    </asp:RequiredFieldValidator>
                </td>
           </tr>
           <tr>
               <td class="lb">
                    <asp:Label runat="server" ID="Label5" Text="Cách tính phép"></asp:Label>
                </td>
               <td>
                    <asp:CheckBox ID="chkAnnualYear" runat="server" CssClass="cheb" Text="Phép năm"  onclick="NotCheck(this.id)" />
                     <asp:CheckBox ID="chkAnnualMonth" runat="server" CssClass="cheb" Text="Phép tháng"  onclick="NotCheck1(this.id)" />
                </td>
               <td class="lb">
                    <asp:Label runat="server" ID="Label6" Text="Ngày tính phép"></asp:Label>
                </td>
               <td>
                    <asp:CheckBox ID="chkOffcialDate" runat="server" CssClass="cheb" Text="Ngày chính thức"  onclick="NotCheck2(this.id)" />
                   <asp:CheckBox ID="chkStartDate" runat="server" CssClass="cheb" Text="Ngày vào làm"  onclick="NotCheck3(this.id)" />
               </td>
           </tr>
           <tr>
               <td class="lb" >
                    <asp:Label runat="server" ID="lbOldAnnual_Time" Text="Thời hạn phép cũ"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOldAnnual_Time" runat="server">
                    </tlk:RadTextBox>
                </td>
               <td class="lb" >
                    <asp:Label runat="server" ID="Label7" Text="Gia hạn phép cũ"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOldAnnual_Renew" runat="server">
                    </tlk:RadTextBox>
                </td>
               <td class="lb" >
                    <asp:Label runat="server" ID="Label8" Text="Phép tối đa chuyển"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_transfer" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
           </tr>
           <tr>
               <td class="lb" >
                    <asp:Label runat="server" ID="Label33" Text="Thời hạn phép cũ bù"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOldAnnual_Time_bu" runat="server">
                    </tlk:RadTextBox>
                </td>
               <td class="lb" >
                    <asp:Label runat="server" ID="Label34" Text="Gia hạn phép cũ bù"></asp:Label>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOldAnnual_Renew_bu" runat="server">
                    </tlk:RadTextBox>
                </td>
               <td class="lb" >
                    <asp:Label runat="server" ID="Label35" Text="Phép tối đa chuyển bù"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_transfer_bu" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
           </tr>
            <tr>
               <td class="lb" >
                    <asp:Label runat="server" ID="Label9" Text="Phép tối đa tính lương"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_Paid" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
           </tr>
            <tr>
                <td colspan="6">
                    <b>
                        <asp:Label ForeColor="Blue" runat="server" ID="DisciplineInfo" Text="Tính phép cho nhân viên mới vào trong tháng"></asp:Label>
                    </b>
                    <hr />
                </td>
            </tr>
           <tr>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label1" Text="Vào làm từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtStartdate_from1" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label2" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtStartdate_to1" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label3" Text="Được hưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_Start1" runat="server">
                    </tlk:RadNumericTextBox> <asp:Label ForeColor="Red" runat="server" ID="Label13" Text="ngày"></asp:Label>
                </td>
           </tr>
           <tr>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label10" Text="Vào làm từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtStartdate_from2" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label11" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtStartdate_to2" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label12" Text="Được hưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_Start2" runat="server">
                    </tlk:RadNumericTextBox> <asp:Label ForeColor="Red" runat="server" ID="Label14" Text="ngày"></asp:Label>
                </td>
           </tr>
           <tr>
                <td colspan="6">
                    <b>
                        <asp:Label ForeColor="Blue" runat="server" ID="Label15" Text="Tính phép cho nhân viên nghỉ việc trong tháng"></asp:Label>
                    </b>
                    <hr />
                </td>
            </tr>
           <tr>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label16" Text="Vào làm từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtLeavedate_from1" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label17" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtLeavedate_to1" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label18" Text="Được hưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_Leave1" runat="server">
                    </tlk:RadNumericTextBox> <asp:Label ForeColor="Red" runat="server" ID="Label19" Text="ngày"></asp:Label>
                </td>
           </tr>
           <tr>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label20" Text="Vào làm từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtLeavedate_from2" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label21" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtLeavedate_to2" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label22" Text="Được hưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_Leave2" runat="server">
                    </tlk:RadNumericTextBox> <asp:Label ForeColor="Red" runat="server" ID="Label23" Text="ngày"></asp:Label>
                </td>
           </tr>

            <tr>
                <td colspan="6">
                    <b>
                        <asp:Label ForeColor="Blue" runat="server" ID="Label24" Text="Tính phép cho nhân viên đang làm việc trong tháng"></asp:Label>
                    </b>
                    <hr />
                </td>
            </tr>
           <tr>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label25" Text="Vào làm từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtWorkdate_from1" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label26" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtWorkdate_to1" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label27" Text="Được hưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_Work1" runat="server">
                    </tlk:RadNumericTextBox> <asp:Label ForeColor="Red" runat="server" ID="Label28" Text="ngày"></asp:Label>
                </td>
           </tr>
           <tr>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label29" Text="Vào làm từ ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtWorkdate_from2" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label30" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtWorkdate_to2" runat="server">
                    </tlk:RadNumericTextBox>
                </td>
               <td class="lb" >
                    <asp:Label ForeColor="Red" runat="server" ID="Label31" Text="Được hưởng"></asp:Label>
                </td>
                <td>
                    <tlk:RadNumericTextBox ID="ntxtAnnual_Work2" runat="server">
                    </tlk:RadNumericTextBox> <asp:Label ForeColor="Red" runat="server" ID="Label32" Text="ngày"></asp:Label>
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
            <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,EFFECT_YEAR,ANNUAL_YEAR,ANNUAL_YEAR_NAME,ANNUAL_MONTH,ANNUAL_MONTH_NAME,OFFICIAL_DATE,OFFICIAL_DATE_NAME,START_DATE,START_DATE_NAME,OLDANNUAL_TIME,
OLDANNUAL_RENEW,ANNUAL_TRANSFER,ANNUAL_PAID,STARTDATE_FROM1,STARTDATE_TO1,ANNUAL_START1,STARTDATE_FROM2,STARTDATE_TO2,ANNUAL_START2,LEAVEDATE_FROM1,
LEAVEDATE_TO1,ANNUAL_LEAVE1,LEAVEDATE_FROM2,LEAVEDATE_TO2,ANNUAL_LEAVE2,WORKDATE_FROM1,WORKDATE_TO1,ANNUAL_WORK1,WORKDATE_FROM2,WORKDATE_TO2,ANNUAL_WORK2,OLDANNUAL_TIME_BU,OLDANNUAL_RENEW_BU,ANNUAL_TRANSFER_BU">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                    </tlk:GridClientSelectColumn>
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm hiệu lực %>" DataField="EFFECT_YEAR" UniqueName="EFFECT_YEAR"
                        SortExpression="EFFECT_YEAR">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép năm %>" DataField="ANNUAL_YEAR_NAME" UniqueName="ANNUAL_YEAR_NAME"
                        SortExpression="ANNUAL_YEAR_NAME">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép tháng %>" DataField="ANNUAL_MONTH_NAME" UniqueName="ANNUAL_MONTH_NAME"
                        SortExpression="ANNUAL_MONTH_NAME">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép tính ngày chính thức %>" DataField="OFFICIAL_DATE_NAME" UniqueName="OFFICIAL_DATE_NAME"
                        SortExpression="OFFICIAL_DATE_NAME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép tính ngày vào làm %>" DataField="START_DATE_NAME" UniqueName="START_DATE_NAME"
                        SortExpression="START_DATE_NAME">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời hạn phép cũ %>" DataField="OLDANNUAL_TIME" UniqueName="OLDANNUAL_TIME"
                        SortExpression="OLDANNUAL_TIME">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Gia hạn phép cũ %>" DataField="OLDANNUAL_RENEW" UniqueName="OLDANNUAL_RENEW"
                        SortExpression="OLDANNUAL_RENEW">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép tối đa chuyển %>" DataField="ANNUAL_TRANSFER" UniqueName="ANNUAL_TRANSFER"
                        SortExpression="ANNUAL_TRANSFER">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời hạn phép cũ bù%>" DataField="OLDANNUAL_TIME_BU" UniqueName="OLDANNUAL_TIME_BU"
                        SortExpression="OLDANNUAL_TIME_BU">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Gia hạn phép cũ bù%>" DataField="OLDANNUAL_RENEW_BU" UniqueName="OLDANNUAL_RENEW_BU"
                        SortExpression="OLDANNUAL_RENEW_BU">
                    </tlk:GridBoundColumn>
                     <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép tối đa chuyển bù%>" DataField="ANNUAL_TRANSFER_BU" UniqueName="ANNUAL_TRANSFER_BU"
                        SortExpression="ANNUAL_TRANSFER_BU">
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Phép tối đa tính lương %>" DataField="ANNUAL_PAID" UniqueName="ANNUAL_PAID"
                        SortExpression="ANNUAL_PAID">
                    </tlk:GridBoundColumn>
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
        var splitterID = 'ctl00_MainContent_ctrlAtSetupELeave_RadSplitter3';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAtSetupELeave_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlAtSetupELeave_RadPane2';
        var validateID = 'MainContent_ctrlAtSetupELeave_valSum';
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

        function NotCheck(checkbox) {
            if (document.getElementById(checkbox).checked) {
                document.getElementById('<%= chkAnnualMonth.ClientID%>').checked = false;
            }
        }
        function NotCheck1(checkbox) {
            if (document.getElementById(checkbox).checked) {
                document.getElementById('<%= chkAnnualYear.ClientID%>').checked = false;
            }
        }
        function NotCheck2(checkbox) {
            if (document.getElementById(checkbox).checked) {
                document.getElementById('<%= chkStartDate.ClientID%>').checked = false;
            }
        }

        function NotCheck3(checkbox) {
            if (document.getElementById(checkbox).checked) {
                document.getElementById('<%= chkOffcialDate.ClientID%>').checked = false;
            }
        }
    </script>
</tlk:RadCodeBlock>
