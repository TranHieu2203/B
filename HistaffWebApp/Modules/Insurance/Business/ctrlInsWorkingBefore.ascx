<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsWorkingBefore.ascx.vb"
    Inherits="Insurance.ctrlInsWorkingBefore" %>
<%@ Import Namespace="Common" %>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:HiddenField ID="hidEmp" runat="server" />

<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="260px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="140px" Scrolling="None">
                <asp:HiddenField ID="hidID" runat="server" />
                <table class="table-form" onkeydown="return (event.keyCode!=13)">
                    <tr>
                        <td class="lb">
                           <%# Translate("MSNV")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtEMPLOYEE_CODE" MaxLength="50" Width="60%" runat="server">
                            </tlk:RadTextBox>
                            <tlk:RadButton ID="btnFindEmployee" runat="server" SkinID="ButtonView" CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqtxtCODE" ControlToValidate="rtEMPLOYEE_CODE" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải chọn MSNV. %>" ToolTip="<%$ Translate: Bạn phải chọn MSNV. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                           <%# Translate("Họ tên nhân viên")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtEMPLOYEE_NAME" ReadOnly="true" SkinID="Readonly" runat="server">
                            </tlk:RadTextBox>

                        </td>
                        <td class="lb">
                           <%# Translate("Phòng ban")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtORGNAME" ReadOnly="true" SkinID="Readonly" runat="server">
                            </tlk:RadTextBox>

                        </td>
                        <td class="lb">
                           <%# Translate("Vị trí công việc")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="rtEMPLOYEE_TITTLE" ReadOnly="true" SkinID="Readonly" runat="server">
                            </tlk:RadTextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="lb">
                           <%# Translate("Công ty")%><span class="lbReq">*</span>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="rtCOMPANY" MaxLength="50" runat="server" Width="100%">
                            </tlk:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="rtCOMPANY" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập tên công ty. %>" ToolTip="<%$ Translate: Bạn phải nhập tên công ty. %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Từ tháng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker runat="server" ID="rdFROMMONTH" TabIndex="5" Culture="en-US">
                                <DateInput ID="DateInput3" runat="server" DisplayDateFormat="MM/yyyy"></DateInput>
                            </tlk:RadMonthYearPicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdFROMMONTH" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập từ tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập từ tháng. %>">
                            </asp:RequiredFieldValidator>
                            <%--<tlk:RadDatePicker ID="rdFROMMONTH" runat="server">
                            </tlk:RadDatePicker>--%>
                        </td>
                        <td align="left">
                            <%# Translate("Đến tháng")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadMonthYearPicker runat="server" ID="rdTOMONTH" TabIndex="5" Culture="en-US">
                                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="MM/yyyy"></DateInput>
                            </tlk:RadMonthYearPicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="rdTOMONTH" runat="server"
                                Text="*" ErrorMessage="<%$ Translate: Bạn phải nhập đến tháng. %>" ToolTip="<%$ Translate: Bạn phải nhập đến tháng. %>">
                            </asp:RequiredFieldValidator>
                            <%--<tlk:RadDatePicker ID="rdTOMONTH" runat="server">
                            </tlk:RadDatePicker>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                           <%# Translate("Vị trí công việc")%>
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="rtTITTLE" MaxLength="50" Width="100%" runat="server">
                            </tlk:RadTextBox>
                        </td>
                        <td class="lb">
                           <%# Translate("Ghi chú")%>
                        </td>
                        <td  colspan="3">
                            <tlk:RadTextBox ID="rtREMARK" Width="100%" runat="server">
                            </tlk:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <%--<td class="lb">
                           <%# Translate("Hệ số")%>
                        </td>
                        <td >
                            <tlk:RadNumericTextBox ID="rnCOEFFICIENT" MaxLength="50" runat="server" SkinID="Decimal">
                            </tlk:RadNumericTextBox>
                        </td>--%>
                        <td class="lb">
                           <%# Translate("Lương đóng BHXH")%>
                        </td>
                        <td >
                            <tlk:RadNumericTextBox ID="rnINS_SALARY" MaxLength="50" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                        <%--<td class="lb">
                           <%# Translate("Tiền lương")%>
                        </td>
                        <td >
                            <tlk:RadNumericTextBox ID="rnSALARY" MaxLength="50" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                           <%# Translate("Độc hại")%>
                        </td>
                        <td >
                            <tlk:RadNumericTextBox ID="rnTOXIC" MaxLength="50" runat="server" SkinID="Money">
                            </tlk:RadNumericTextBox>
                        </td>--%>
                    </tr>

                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%" AllowPaging="True" AllowSorting="True"
                    CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true" GridLines="None"
                     AutoGenerateColumns="false" AllowFilteringByColumn="true" >
                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,FROM_MONTH,TO_MONTH,INS_SALARY,SALARY,TOXIC,COEFFICIENT,COMPANY,TITLE,REMARK,ORG_NAME,TITTLE_EMP_NAME">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" UniqueName="ID" Visible="false" />
                           <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc%>" DataField="TITLE"
                                SortExpression="TITLE" UniqueName="TITLE">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ tháng%>" DataField="FROM_MONTH"
                                DataFormatString="{0:MM/yyyy}" SortExpression="START_DATE" UniqueName="FROM_MONTH" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="120px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến tháng %>" DataField="TO_MONTH"
                                DataFormatString="{0:MM/yyyy}" SortExpression="TO_MONTH" UniqueName="TO_MONTH" CurrentFilterFunction="EqualTo">
                                <HeaderStyle Width="120px" />
                            </tlk:GridBoundColumn>

                             <tlk:GridNumericColumn HeaderText="<%$ Translate: Lương BHXH %>" DataField="INS_SALARY"
                                SortExpression="INS_SALARY" DataFormatString="{0:n0}" UniqueName="INS_SALARY"  >
                                <HeaderStyle Width="100px" />
                            </tlk:GridNumericColumn>

                             <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền lương %>" DataField="SALARY"
                                SortExpression="SALARY" DataFormatString="{0:n0}" UniqueName="SALARY">
                                 <HeaderStyle Width="100px" />
                             </tlk:GridNumericColumn>--%>
                            <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Đọc hại %>" DataField="TOXIC"
                                SortExpression="TOXIC" DataFormatString="{0:n0}" UniqueName="TOXIC">
                                <HeaderStyle Width="100px" /></tlk:GridNumericColumn>--%>
                             <%--<tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số %>" DataField="COEFFICIENT"
                                SortExpression="COEFFICIENT" DataFormatString="{0:n2}" UniqueName="COEFFICIENT">
                                 <HeaderStyle Width="100px" />
                             </tlk:GridNumericColumn> --%>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty %>" DataField="COMPANY"
                                SortExpression="COMPANY" UniqueName="COMPANY">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú%>" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>

               <%-- <tlk:RadGrid PageSize=50 ID="rgData" runat="server" Height="100%" AllowSorting="True" AllowMultiRowSelection="true"
                    AllowPaging="True" AutoGenerateColumns="False">
                    <ClientSettings EnableRowHoverStyle="true">
                         <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="3" />
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                         <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ tháng%>" DataField="FROM_MONTH"
                                DataFormatString="{0:MM/yyyy}" SortExpression="START_DATE" UniqueName="FROM_MONTH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến tháng %>" DataField="TO_MONTH"
                                DataFormatString="{0:MM/yyyy}" SortExpression="TO_MONTH" UniqueName="TO_MONTH">
                                <HeaderStyle Width="120px" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Lương BHXH %>" DataField="INS_SALARY"
                                SortExpression="INS_SALARY" DataFormatString="{0:n0}" UniqueName="INS_SALARY" />
                             <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền lương %>" DataField="SALARY"
                                SortExpression="SALARY" DataFormatString="{0:n0}" UniqueName="SALARY" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Đọc hại %>" DataField="TOXIC"
                                SortExpression="TOXIC" DataFormatString="{0:n0}" UniqueName="TOXIC" />
                             <tlk:GridNumericColumn HeaderText="<%$ Translate: Hệ số %>" DataField="COEFFICIENT"
                                SortExpression="COEFFICIENT" DataFormatString="{0:n0}" UniqueName="COEFFICIENT" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Công ty %>" DataField="COMPANY"
                                SortExpression="COMPANY" UniqueName="COMPANY">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc%>" DataField="TITLE"
                                SortExpression="TITLE" UniqueName="TITLE">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú%>" DataField="REMARK"
                                SortExpression="REMARK" UniqueName="REMARK">
                                <HeaderStyle Width="150px" />
                            </tlk:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                </tlk:RadGrid>--%>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="500" Height="500px"
            OnClientClose="OnClientClose" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Thông tin chi tiết nhân viên%>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<tlk:RadScriptBlock ID="scriptBlock" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;
        var splitterID = 'ctl00_MainContent_ctrlInsWorkingBefore_RadSplitter1';
        //function gridRowDblClick(sender, eventArgs) {
        //    OpenEditWindow("Normal");
        //}

       <%-- function OpenInsertWindow() {
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsManagerHeathInsNewEdit&group=Business&FormType=0&noscroll=1', "rwPopup");
            oWindow.maximize(true);
            oWindow.center();
        }
        function OpenEditWindow() {
            var grid = $find('<%# rgData.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var id = 0
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            if (gridSelected != "") {
                id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            }
            if (id > 0) {
                var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsManagerHeathInsNewEdit&group=Business&VIEW=TRUE&FormType=0&ID=' + id + '&noscroll=1', "rwPopup");
                oWindow.maximize(true);
                oWindow.center();
            }
        }--%>
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
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
        function OnClientClose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
           
        }
        function OnClientButtonClicking(sender, args) {
            <%--if (args.get_item().get_commandName() == 'CREATE') {
                OpenInsertWindow();
                args.set_cancel(true);
            } else if (args.get_item().get_commandName() == 'EDIT') {
                var bCheck = $find('<%= rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }
                else {
                    OpenEditWindow();
                    args.set_cancel(true);
                }
            }else--%> if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "CHECK") {
                
                enableAjax = false;
            }
    }
    function OpenInNewTab(url) {
        var win = window.open(url, '_blank');
        win.focus();
    }
    </script>
</tlk:RadScriptBlock>
