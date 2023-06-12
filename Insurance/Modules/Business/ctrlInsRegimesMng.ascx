<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsRegimesMng.ascx.vb"
    Inherits="Insurance.ctrlInsRegimesMng" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="250" Width="280px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="180px" Scrolling="None">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnFind">      
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
                        <table width="100%" class="td-padding">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Từ tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtFROMDATE"
                                        TabIndex="1" Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Đến tháng")%>
                                </td>
                                <td>
                                    <tlk:RadMonthYearPicker DateInput-DisplayDateFormat="MM/yyyy" runat="server" ID="txtTODATE"
                                        TabIndex="2"  Culture="en-US">
                                    </tlk:RadMonthYearPicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Loại chế độ")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox DataValueField="ID" DataTextField="NAME" ID="ddlREGIME_ID_SEARCH"
                                        runat="server" TabIndex="3" ToolTip="<%$ Translate: Loại chế độ %>" AutoPostBack="true"
                                        EmptyMessage="<%$ Translate: Loại chế độ %>">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td class="lb">
                                    <%# Translate("Đối tượng chi trả")%>
                                </td>
                                <td>
                                    <tlk:RadComboBox DataValueField="CODE" DataTextField="NAME" ID="ddlPAY_FORM_SEARCH"
                                        runat="server" TabIndex="4" EmptyMessage="<%$ Translate: Đối tượng chi trả %>"
                                        CheckBoxes="false" EnableCheckAllItemsCheckBox="false" DropDownAutoWidth="Enabled"
                                        Height="100px">
                                    </tlk:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Nhân viên")%>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtEMPLOYEEID_SEARCH" MaxLength="250" runat="server" TabIndex="5">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSTATUS" runat="server" Text='<%# Translate("Đã nghỉ việc")%>' />
                                    &nbsp;&nbsp;&nbsp;
                                    <tlk:RadButton EnableEmbeddedSkins="true" ID="btnFind" TabIndex="6" runat="server"
                                        Text="<%$ Translate: Tìm kiếm %>" ValidationGroup="Search" SkinID="ButtonFind"
                                        OnClick="btnFIND_Click">
                                    </tlk:RadButton>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </asp:Panel>
            </tlk:RadPane>
            <tlk:RadSplitBar ID="RadSplitBar3" runat="server">
            </tlk:RadSplitBar>
            <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" AutoPostBack="true" />
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <tlk:RadGrid ID="rgGridData" runat="server" Height="95%" Width="100%" AllowPaging="True" AllowSorting="True"  EnableLinqExpressions="false"
                    CellSpacing="0" ShowStatusBar="true" AllowMultiRowSelection="true" GridLines="None"
                    PageSize="20" AutoGenerateColumns="false" AllowFilteringByColumn="true">
                    <ClientSettings AllowColumnsReorder="True" EnableRowHoverStyle="true" Scrolling-AllowScroll="true" Scrolling-SaveScrollPosition="true"
                            Scrolling-UseStaticHeaders="true">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="4" />
                        <Selecting AllowRowSelect="true" />
                        <Scrolling UseStaticHeaders="true" />
                        <Resizing AllowColumnResize="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPID,FULL_NAME,DEP_NAME,SOCIAL_NUMBER,HEALTH_NUMBER,BIRTH_DATE,PLACE_OF_BIRTH_NAME,
                        REGIME_NM,REGIME_ID,PAY_FORM,FROM_DATE,TO_DATE,DAY_CALCULATOR,BORN_DATE,NAME_CHILDREN,CHILDREN_NO,
                        ACCUMULATE_DAY,SUBSIDY_SALARY,SUBSIDY_AMOUNT,DECLARE_DATE,PAYROLL_DATE,CONDITION,INS_PAY_AMOUNT,PAY_APPROVE_DATE,APPROV_DAY_NUM,NOTE,
                        FORM_ALLOW,FORM_ALLOW_NAME">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: ID%>" DataField="ID" UniqueName="ID"
                                SortExpression="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã NV %>" DataField="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="FULL_NAME" UniqueName="FULL_NAME" SortExpression="FULL_NAME" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEP_NAME" UniqueName="DEP_NAME" SortExpression="DEP_NAME"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="170px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày sinh%>" DataField="BIRTH_DATE" UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi sinh%>" DataField="PLACE_OF_BIRTH_NAME" UniqueName="PLACE_OF_BIRTH_NAME" SortExpression="PLACE_OF_BIRTH_NAME"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="170px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: REGIME_ID %>" DataField="REGIME_ID" UniqueName="REGIME_ID" SortExpression="REGIME_ID" Visible="false"/>   
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại hưởng chế độ%>" DataField="REGIME_NM" UniqueName="REGIME_NM" SortExpression="REGIME_NM"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="250px"/>   
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Hình thức nhận trợ cấp%>" DataField="FORM_ALLOW_NAME" UniqueName="FORM_ALLOW_NAME" SortExpression="FORM_ALLOW_NAME"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="250px"/>                         
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="FROM_DATE" UniqueName="FROM_DATE" SortExpression="FROM_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="TO_DATE" UniqueName="TO_DATE" SortExpression="TO_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày tính %>" DataField="DAY_CALCULATOR" UniqueName="DAY_CALCULATOR" SortExpression="DAY_CALCULATOR"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>                                                        
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày lũy kế %>" DataFormatString="{0:N0}" DataField="ACCUMULATE_DAY" UniqueName="ACCUMULATE_DAY" SortExpression="ACCUMULATE_DAY"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tiền lương tính hưởng BHXH %>" DataFormatString="{0:N0}" DataField="SUBSIDY_SALARY" UniqueName="SUBSIDY_SALARY" SortExpression="SUBSIDY_SALARY"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền hưởng theo chế độ %>" DataFormatString="{0:N0}" DataField="REGIMES_SAL" UniqueName="REGIMES_SAL" SortExpression="REGIMES_SAL"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền trợ cấp được hưởng %>" DataFormatString="{0:N0}" DataField="SUBSIDY" UniqueName="SUBSIDY" SortExpression="SUBSIDY"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px" />
                             <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền trợ cấp điều chỉnh %>" DataFormatString="{0:N0}" DataField="SUBSIDY_MODIFY" UniqueName="SUBSIDY_MODIFY" SortExpression="SUBSIDY_MODIFY"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đợt khai báo %>" DataField="DECLARE_DATE" UniqueName="DECLARE_DATE" SortExpression="DECLARE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thời điểm tính %>" DataField="PAYROLL_DATE" UniqueName="PAYROLL_DATE" SortExpression="PAYROLL_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Điều kiện hưởng %>" DataField="CONDITION" UniqueName="CONDITION" SortExpression="CONDITION"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="170px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số tiền duyệt chi %>" DataField="INS_PAY_AMOUNT" UniqueName="INS_PAY_AMOUNT" DataFormatString="{0:N0}" SortExpression="INS_PAY_AMOUNT"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="170px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày duyệt chi %>" DataField="PAY_APPROVE_DATE" UniqueName="PAY_APPROVE_DATE" SortExpression="PAY_APPROVE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số ngày duyệt chi %>" DataField="APPROV_DAY_NUM" UniqueName="APPROV_DAY_NUM" SortExpression="APPROV_DAY_NUM"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>                            
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="FindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>        
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"  OnClientBeforeClose="OnClientBeforeClose"
            OnClientClose="popupclose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize"
            Modal="true" ShowContentDuringLoad="false">                                    
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (item.get_commandName() == "CREATE") {
                OpenNew();
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EDIT") {
                if (OpenEdit(1) == 1) {
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
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsRegimes&group=Business&Status=0', "rwPopup");
            oWindow.setSize(800, 650);
            //oWindow.setSize($(window).width(), $(window).height());
            oWindow.center();
        }

        function OpenEdit(Status) {
            debugger;
            var bCheck = $find('<%= rgGridData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var emp_id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsRegimes&group=Business&Status=' + Status + '&IDSelect=' + id + '&EmployeeID=' + emp_id, "rwPopup");
            oWindow.setSize(800, 650);
            //oWindow.setSize($(window).width(), $(window).height());
            oWindow.center();
            return 0;
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEdit(1);
            //args.set_cancel(true);
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

        //        function popupclose(oWnd, args) {
        //            window.location.reload();
        //        }


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
</tlk:RadCodeBlock>
