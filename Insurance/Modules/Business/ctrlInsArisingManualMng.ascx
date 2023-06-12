<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsArisingManualMng.ascx.vb"
    Inherits="Insurance.ctrlInsArisingManualMng" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="150px" Scrolling="None">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnFind">      
                <div style="display: none;">
                    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
                    </tlk:RadTextBox>
                </div>
                <div>
                    <fieldset style="width: auto; height: auto">
                        <legend>
                            <%# Translate("Thông tin tìm kiếm")%>
                        </legend>
                        <table width="100%" class="td-padding">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Từ ngày")%>
                                </td>
                                <td>
                                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="txtFROMDATE"
                                        TabIndex="1">
                                    </tlk:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("Đến ngày")%>
                                </td>
                                <td>
                                    <tlk:RadDatePicker runat="server" DateInput-DateFormat="dd/MM/yyyy" ID="txtTODATE"
                                        TabIndex="2">
                                    </tlk:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                    <%# Translate("NV/CMND")%>
                                </td>
                                <td>
                                    <tlk:RadTextBox ID="txtEMPLOYEEID_SEARCH" MaxLength="250" runat="server" TabIndex="3">
                                    </tlk:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lb">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkSTATUS" runat="server" Text='<%# Translate("Đã nghỉ việc")%>' />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <tlk:RadButton EnableEmbeddedSkins="true" ID="btnFind" TabIndex="4" runat="server"
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
                <Common:ctrlOrganization ID="ctrlOrg" runat="server" AutoPostBack="true"/>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadToolBar ID="rtbMain" runat="server" />
                <tlk:RadGrid ID="rgGridData" runat="server" Height="95%" AllowPaging="True" AllowSorting="True" EnableLinqExpressions="false"
                    CellSpacing="0" ShowStatusBar="true" GridLines="None"
                    PageSize="500" AutoGenerateColumns="false" AllowFilteringByColumn="true">
                    <ClientSettings>
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                           <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_ID,SI,HI,UI,BIRTH_DATE,PLACE_OF_BIRTH_NAME,ID_NO,ID_DATE,POSITION_NAME,INS_ORG_NAME,EMPLOYEE_CODE,EMPID,FULL_NAME,DEP_NAME,INS_ORG_ID,INS_ARISING_TYPE_ID,SALARY_PRE_PERIOD,SALARY_NOW_PERIOD,FROM_HEALTH_INS_CARD,EFFECTIVE_DATE,DECLARE_DATE,ARISING_FROM_MONTH,ARISING_TO_MONTH,NOTE,SOCIAL_NOTE,HEALTH_NUMBER,HEALTH_STATUS,HEALTH_EFFECT_FROM_DATE,HEALTH_EFFECT_TO_DATE,HEALTH_AREA_INS_ID,HEALTH_RECEIVE_DATE,HEALTH_RECEIVER,HEALTH_RETURN_DATE,UNEMP_FROM_MOTH,UNEMP_TO_MONTH,UNEMP_REGISTER_MONTH,R_FROM,O_FROM,R_TO,O_TO,R_SI,O_SI,R_HI,O_HI,R_UI,O_UI,A_FROM,A_TO,A_SI,A_HI,SALARY_HD,SALARY_PC,ORG_DESC">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>    
                            <tlk:GridBoundColumn HeaderText="ID" DataField="ID" UniqueName="ID"
                                SortExpression="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã NV" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="70px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Họ tên" DataField="FULL_NAME"
                                UniqueName="FULL_NAME" SortExpression="FULL_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="DEP_NAME"
                                UniqueName="DEP_NAME" SortExpression="DEP_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="POSITION_NAME"
                                UniqueName="POSITION_NAME" HeaderStyle-Width="160px" SortExpression="POSITION_NAME" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="CMND" DataField="ID_NO" UniqueName="ID_NO"
                                SortExpression="ID_NO" HeaderStyle-Width="80px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Nơi cấp" DataField="ID_PLACE_NAME" UniqueName="ID_PLACE_NAME"
                                SortExpression="ID_PLACE_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Ngày cấp" DataField="ID_DATE" UniqueName="ID_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                SortExpression="ID_DATE" HeaderStyle-Width="80px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" />      
                            <tlk:GridBoundColumn HeaderText="Ngày sinh" DataField="BIRTH_DATE" UniqueName="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                SortExpression="BIRTH_DATE" HeaderStyle-Width="80px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Nơi sinh" DataField="PLACE_OF_BIRTH_NAME"
                                UniqueName="PLACE_OF_BIRTH_NAME" SortExpression="PLACE_OF_BIRTH_NAME" HeaderStyle-Width="150px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />                     
                            <tlk:GridBoundColumn HeaderText="INS_ORG_ID" DataField="INS_ORG_ID"
                                UniqueName="INS_ORG_ID" SortExpression="INS_ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="INS_ARISING_TYPE_ID" DataField="INS_ARISING_TYPE_ID"
                                UniqueName="INS_ARISING_TYPE_ID" SortExpression="INS_ARISING_TYPE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị BH" DataField="INS_ORG_NAME"
                                UniqueName="INS_ORG_NAME" SortExpression="INS_ORG_NAME" HeaderStyle-Width="200px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Loại biến động" DataField="ARISING_TYPE_NM"
                                UniqueName="ARISING_TYPE_NM" SortExpression="ARISING_TYPE_NM" HeaderStyle-Width="180px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />
                            <tlk:GridCheckBoxColumn HeaderText="BHXH" DataField="SI_CHK" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="SI_CHK" UniqueName="SI_CHK" >
                                <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="BHYT" DataField="HI_CHK" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="HI_CHK" UniqueName="HI_CHK" >
                                <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="BHTN" DataField="UI_CHK" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="UI_CHK" UniqueName="UI_CHK" >
                                <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                            </tlk:GridCheckBoxColumn>     
                            <tlk:GridNumericColumn HeaderText="Lương kỳ trước" DataFormatString="{0:###,##0.##}"
                                DataField="SALARY_PRE_PERIOD" UniqueName="SALARY_PRE_PERIOD" SortExpression="SALARY_PRE_PERIOD" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" />
                            <tlk:GridNumericColumn HeaderText="Lương kỳ này" DataFormatString="{0:###,##0.##}"
                                DataField="SALARY_NOW_PERIOD" UniqueName="SALARY_NOW_PERIOD" SortExpression="SALARY_NOW_PERIOD" HeaderStyle-Width="90px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" />                           
                            <tlk:GridBoundColumn HeaderText="Ngày hiệu lực" DataField="EFFECTIVE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="EFFECTIVE_DATE" SortExpression="EFFECTIVE_DATE" HeaderStyle-Width="90px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Đợt khai báo" DataFormatString="{0:dd/MM/yyyy}" DataField="DECLARE_DATE" UniqueName="DECLARE_DATE" SortExpression="DECLARE_DATE" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" />
                            <tlk:GridBoundColumn HeaderText="Từ tháng(Khai báo)" DataField="ARISING_FROM_MONTH" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                UniqueName="ARISING_FROM_MONTH" SortExpression="ARISING_FROM_MONTH" DataFormatString="{0:MM/yyyy}"/>
                            <tlk:GridBoundColumn HeaderText="Tới tháng(Khai báo)" DataField="ARISING_TO_MONTH" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                UniqueName="ARISING_TO_MONTH" SortExpression="ARISING_TO_MONTH" DataFormatString="{0:MM/yyyy}"/>
                            <tlk:GridBoundColumn HeaderText="Ngày trả thẻ" DataField="HEALTH_RETURN_DATE" DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                UniqueName="HEALTH_RETURN_DATE" SortExpression="HEALTH_RETURN_DATE" HeaderStyle-Width="100px"/>   
                            <tlk:GridBoundColumn HeaderText="NOTE" DataField="NOTE" UniqueName="NOTE" SortExpression="NOTE" HeaderStyle-Width="180px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" />                                                                                                                                                            
                            <tlk:GridBoundColumn HeaderText="Từ tháng(truy thu BH)" DataField="A_FROM" UniqueName="A_FROM" DataFormatString="{0:MM/yyyy}" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="A_FROM"/>
                            <tlk:GridBoundColumn HeaderText="Tới tháng(truy thu BH)" DataField="A_TO" UniqueName="A_TO" DataFormatString="{0:MM/yyyy}" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="A_TO"/>
                            <tlk:GridNumericColumn HeaderText="BHXH" DataFormatString="{0:###,##0.##}" DataField="A_SI" UniqueName="A_SI" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="A_SI"/>
                            <tlk:GridNumericColumn HeaderText="BHYT" DataFormatString="{0:###,##0.##}" DataField="A_HI" UniqueName="A_HI" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="A_HI"/>
                            <tlk:GridNumericColumn HeaderText="BHTN" DataFormatString="{0:###,##0.##}" DataField="A_UI" UniqueName="A_UI" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="A_UI"/>
                            <tlk:GridBoundColumn HeaderText="Từ tháng(thoái thu BH)" DataField="R_FROM" UniqueName="R_FROM" DataFormatString="{0:MM/yyyy}" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="R_FROM"/>                            
                            <tlk:GridBoundColumn HeaderText="Tới tháng(thoái thu BH)" DataField="R_TO" UniqueName="R_TO" DataFormatString="{0:MM/yyyy}" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="R_TO"/>                            
                            <tlk:GridNumericColumn HeaderText="BHXH" DataFormatString="{0:###,##0.##}" DataField="R_SI" UniqueName="R_SI" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="R_SI"/>                            
                            <tlk:GridNumericColumn HeaderText="BHYT" DataFormatString="{0:###,##0.##}" DataField="R_HI" UniqueName="R_HI" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="R_HI"/>                            
                            <tlk:GridNumericColumn HeaderText="BHTN" DataFormatString="{0:###,##0.##}" DataField="R_UI" UniqueName="R_UI" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="R_UI"/>
                            <tlk:GridBoundColumn HeaderText="Từ tháng(bổ sung BH)" DataField="O_FROM" UniqueName="O_FROM" DataFormatString="{0:MM/yyyy}" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="O_FROM"/>
                            <tlk:GridBoundColumn HeaderText="Tới tháng(bổ sung BH)" DataField="O_TO" UniqueName="O_TO" DataFormatString="{0:MM/yyyy}" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="O_TO"/>
                            <tlk:GridNumericColumn HeaderText="BHYT" DataFormatString="{0:###,##0.##}" DataField="O_HI" UniqueName="O_HI" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="O_HI"/>
                            <tlk:GridNumericColumn HeaderText="BHTN" DataFormatString="{0:###,##0.##}" DataField="O_UI" UniqueName="O_UI" HeaderStyle-Width="100px" FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" 
                                SortExpression="O_UI"/>--%>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server" Scrolling="None">
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
            debugger;
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
            var bCheck = $find('<%= rgGridData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsArisingManual&group=Business&Status=0', "rwPopup");
                oWindow.maximize(true);
                oWindow.center();
            } else if (bCheck == 1) {
                var emp_id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID')
                var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsArisingManual&group=Business&Status=0&EmployeeID=' + emp_id, "rwPopup");
                oWindow.maximize(true);
                oWindow.center();
            } else {
                var n = noty({ text: 'Không thể chọn nhiều dòng để thực hiện thao tác này', dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
        }

        function OpenEdit(Status) {
            var bCheck = $find('<%= rgGridData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var emp_id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsArisingManual&group=Business&Status=' + Status + '&IDSelect=' + id + '&EmployeeID=' + emp_id, "rwPopup");
            oWindow.maximize(true);
            oWindow.center();
            return 0;
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEdit(1);
            args.set_cancel(true);
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
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />