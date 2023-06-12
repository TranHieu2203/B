<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsMaternityMng.ascx.vb"
    Inherits="Insurance.ctrlInsMaternityMng" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:radsplitter id="RadSplitter1" runat="server" width="100%" height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="320" Width="320px">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="120px" Scrolling="None">
                <div style="display: none;">
                    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadNumericTextBox MinValue="0" Value="0" ID="txtCurrDate" runat="server">
                        <NumberFormat DecimalDigits="0" />
                    </tlk:RadNumericTextBox>
                </div>
                <div>
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <%# Translate("Thông tin tìm kiếm")%>
                        </legend>
                        <table class="table-form">
                            <%--<tr>
                                <td>
                                    <%# Translate("Từ ngày")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="txtFROMDATE"
                                        TabIndex="1">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%# Translate("Đến ngày")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="txtTODATE"
                                        TabIndex="2">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <%# Translate("Nhân viên")%>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtEMPLOYEEID_SEARCH" MaxLength="250" runat="server" TabIndex="3">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSTATUS" runat="server" Text='<%# Translate("Đã nghỉ việc")%>' />
                                    <tlk:RadButton ID="btnFind" TabIndex="6" runat="server"
                                        Text="<%$ Translate: Tìm kiếm %>" ValidationGroup="Search"
                                        OnClick="btnFIND_Click">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server">
                <fieldset style="width: auto; height: 92%">
                    <legend>
                        <%# Translate("Danh sách đơn vị- phòng ban")%>
                    </legend>
                    <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
                </fieldset>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
                <tlk:RadGrid ID="rgGridData" runat="server" AllowPaging="True"
                    AllowSorting="True" CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true"
                    GridLines="None" PageSize="20" AutoGenerateColumns="false" AllowFilteringByColumn="true" height="96.5%">
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" Scrolling-AllowScroll="true"
                        Scrolling-SaveScrollPosition="true" Scrolling-UseStaticHeaders="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="3" />
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID, EMPLOYEE_ID,ORG_DESC">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID%>" DataField="ID" UniqueName="ID"
                                SortExpression="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="70px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULL_NAME"
                                UniqueName="FULL_NAME" SortExpression="FULL_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="DEP_NAME"
                                UniqueName="DEP_NAME" SortExpression="DEP_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc %>" DataField="POSITION_NAME"
                                UniqueName="POSITION_NAME" HeaderStyle-Width="160px" SortExpression="POSITION_NAME"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nghỉ sinh từ ngày %>" DataField="From_Date"
                                DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" UniqueName="From_Date"
                                SortExpression="From_Date" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nghỉ sinh đến ngày %>" DataField="To_Date"
                                DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" UniqueName="To_Date"
                                SortExpression="To_Date" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày hưởng chế độ thai sản %>" DataField="FROM_DATE_ENJOY"
                                DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" UniqueName="FROM_DATE_ENJOY"
                                SortExpression="FROM_DATE_ENJOY" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày kết thúc hưởng chế độ thai sản %>" DataField="TO_DATE_ENJOY"
                                DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" UniqueName="TO_DATE_ENJOY"
                                SortExpression="TO_DATE_ENJOY" HeaderStyle-Width="100px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền tạm ứng%>" DataField="Tien_Tam_Ung"
                                UniqueName="Tien_Tam_Ung" SortExpression="Tien_Tam_Ung" HeaderStyle-Width="100px" DataFormatString="{0:N0}"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Nghỉ thai sản %>" DataField="nghi_thai_san"  
                                SortExpression="nghi_thai_san" UniqueName="nghi_thai_san" HeaderStyle-Width="100px" AllowSorting="false" ShowFilterIcon="false" AllowFiltering="false">
                                <HeaderStyle HorizontalAlign="Center"/>
                                </tlk:GridCheckBoxColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày đi làm sớm %>" DataField="NGAY_DI_LAM_SOM"
                                DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" UniqueName="NGAY_DI_LAM_SOM"
                                SortExpression="NGAY_DI_LAM_SOM" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số sổ bảo hiểm %>" DataField="social_number"
                                UniqueName="social_number" HeaderStyle-Width="160px" SortExpression="social_number"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn  HeaderText="<%$ Translate: Ngày dự sinh %>" DataField="NGAY_SINH"
                                DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" UniqueName="NGAY_SINH"
                                SortExpression="NGAY_SINH" HeaderStyle-Width="100px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số con%>" DataField="So_Con" UniqueName="So_Con" DataFormatString="{0:N0}"
                                SortExpression="So_Con" HeaderStyle-Width="80px" FilterControlWidth="99%" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK"
                                UniqueName="REMARK" SortExpression="REMARK" HeaderStyle-Width="150px" FilterControlWidth="99%"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="INSURANCE_NAME"
                                UniqueName="INSURANCE_NAME" SortExpression="INSURANCE_NAME" HeaderStyle-Width="200px"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" />--%>
                                <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
    </tlk:RadPane>
</tlk:radsplitter>
<tlk:radwindowmanager id="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientBeforeClose="OnClientBeforeClose" OnClientClose="popupclose" Height="600px"
            EnableShadow="true" Behaviors="Close" Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:radwindowmanager>
<tlk:radcodeblock id="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            debugger;
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (item.get_commandName() == "CREATE") {
                OpenNew();
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EDIT") {
                if (OpenEdit() == 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OpenNew() {
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsMaternityDetail&group=Business&Status=0', "rwPopup");
            //oWindow.setSize(1024, 500);
            oWindow.maximize(true);
            oWindow.center();
        }

        function OpenEdit() {
            debugger;
            var bCheck = $find('<%= rgGridData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var emp_id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsMaternityDetail&group=Business&Status=1&IDSelect=' + id + '&EmployeeID=' + emp_id, "rwPopup");
            //oWindow.setSize(1024, 500);
            oWindow.maximize(true);
            oWindow.center();
            return 0;
        }

        function gridRowDblClick(sender, eventArgs, args) {
            OpenEdit();
            try {
                args.set_cancel(true);
            } catch (err) {
            }

        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $get("<%= btnFind.ClientId %>").click();
            }
        }

        function popupclose(oWnd, args) {
            $find("<%= rgGridData.ClientID %>").get_masterTableView().rebind();
            //window.location.reload();
        }


        function OnClientBeforeClose(sender, eventArgs) {
            if (!confirm("Bạn có muốn đóng màn hình không?")) {
                //if cancel is clicked prevent the window from closing
                args.set_cancel(true);
            }
        }

        function OnClientClose(oWnd, args) {
            oWnd = $find('<%#popupId %>');
            oWnd.setSize(screen.width - 250, screen.height - 300);
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }

    </script>
</tlk:radcodeblock>
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
