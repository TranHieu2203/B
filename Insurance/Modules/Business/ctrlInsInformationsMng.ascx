<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlInsInformationsMng.ascx.vb"
    Inherits="Insurance.ctrlInsInformationsMng" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="300" Width="300px" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">          
            <tlk:RadPane ID="RadPane3" runat="server" Height="100px" Scrolling="None">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnFind">      
                <div style="display: none;">
                    <tlk:RadTextBox ID="txtID" Text="0" runat="server">
                    </tlk:RadTextBox>
                    <tlk:RadTextBox ID="txtEMPID" Text="0" runat="server">
                    </tlk:RadTextBox>
                </div>
                <div>
	                <fieldset style="width:auto; height:auto">
		                <legend>
			                <%# Translate("Thông tin tìm kiếm")%>
		                </legend>
		                <table width="100%" class="td-padding">
                            <tr>
                                <td class="lb">
                                    <%# Translate("Mã/Tên NV")%>
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
            <tlk:RadPane ID="RadPane4" runat="server">
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
                <tlk:RadGrid ID="rgGridData" runat="server" Height="95%" AllowPaging="True" AllowSorting="True"  EnableLinqExpressions="false"
                    CellSpacing="0" ShowStatusBar="true" GridLines="None"
                    PageSize="500" AutoGenerateColumns="false" AllowFilteringByColumn="true">
                    <ClientSettings>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="3" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,EMPLOYEE_ID,SI,HI,UI,BIRTH_DATE,PLACE_OF_BIRTH_NAME,ID_NO,ID_DATE,
                        POSITION_NAME,INS_ORG_NAME,EMPLOYEE_CODE,EMPID,FULL_NAME,DEP_NAME,INS_ORG_ID,SENIORITY_INSURANCE,SENIORITY_INSURANCE_COMPANY,SOCIAL_NUMBER,
                        SOCIAL_STATUS_NM,SOCIAL_SUBMIT_DATE,SOCIAL_GRANT_DATE,SOCIAL_SAVE_NUMBER,SOCIAL_DELIVER_DATE,SOCIAL_RETURN_DATE,SOCIAL_RECEIVER,SOCIAL_NOTE,
                        HEALTH_NUMBER,HEALTH_STATUS_NM,HEALTH_EFFECT_FROM_DATE,HEALTH_EFFECT_TO_DATE,HEALTH_AREA_NM,HEALTH_RECEIVE_DATE,HEALTH_RECEIVER,HEALTH_RETURN_DATE,
                        UNEMP_FROM_MONTH,UNEMP_TO_MONTH,UNEMP_REGISTER_MONTH,SOCIAL_SUBMIT,SI_FROM_MONTH,SI_TO_MONTH,HI_FROM_MONTH,HI_TO_MONTH,SALARY,INS_NAME,BHTNLD_BNN,
                        REGISTER_PLACE,REGISTER_PLACE_NAME,SALARY_PC,SALARY_HD,TITLE_BHXH_NAME,ORG_DESC">
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
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại nhân viên %>" DataField="EMP_STATUS_NAME" UniqueName="EMP_STATUS_NAME" SortExpression="EMP_STATUS_NAME" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEP_NAME" UniqueName="DEP_NAME" SortExpression="DEP_NAME"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="170px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: INS_ORG_ID %>" DataField="INS_ORG_ID"
                                UniqueName="INS_ORG_ID" SortExpression="INS_ORG_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị bảo hiểm %>" DataField="INS_NAME" UniqueName="INS_NAME" SortExpression="INS_NAME" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="300px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày sinh%>" DataField="BIRTH_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="BIRTH_DATE" SortExpression="BIRTH_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true"  HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi sinh%>" DataField="PLACE_OF_BIRTH_NAME" UniqueName="PLACE_OF_BIRTH_NAME" SortExpression="PLACE_OF_BIRTH_NAME" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: CMND%>" DataField="ID_NO" UniqueName="ID_NO" SortExpression="ID_NO"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp%>" DataField="ID_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="ID_DATE" SortExpression="ID_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí công việc%>" DataField="POSITION_NAME" UniqueName="POSITION_NAME" SortExpression="POSITION_NAME" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="150px"/>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Vị trí CV BHXH%>" DataField="TITLE_BHXH_NAME" UniqueName="TITLE_BHXH_NAME" SortExpression="TITLE_BHXH_NAME" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="150px"/>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lương%>" DataField="SALARY_F" UniqueName="SALARY_F" SortExpression="SALARY_F" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Lương hợp đồng%>" DataField="SALARY_HD" UniqueName="SALARY_HD" SortExpression="SALARY_HD" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>--%>
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Phụ cấp%>" DataField="SALARY_PC" UniqueName="SALARY_PC" SortExpression="SALARY_PC" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>--%>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thâm niên BH(Trước) %>" DataField="SENIORITY_INSURANCE_TEXT" UniqueName="SENIORITY_INSURANCE_TEXT" SortExpression="SENIORITY_INSURANCE_TEXT" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Thâm niên(Công ty) %>" DataField="SENIORITY_INSURANCE_COMPANY" UniqueName="SENIORITY_INSURANCE_COMPANY" SortExpression="SENIORITY_INSURANCE_COMPANY" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHXH(Từ tháng) %>" DataField="SI_FROM_MONTH" DataFormatString="{0:MM/yyyy}" UniqueName="SI_FROM_MONTH" SortExpression="SI_FROM_MONTH" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHXH(Đến tháng) %>" DataField="SI_TO_MONTH" DataFormatString="{0:MM/yyyy}" SortExpression="SI_TO_MONTH" UniqueName="SI_TO_MONTH"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số sổ BHXH %>" DataField="SOCIAL_NUMBER" UniqueName="SOCIAL_NUMBER" SortExpression="SOCIAL_NUMBER" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng %>" DataField="SOCIAL_STATUS_NM" UniqueName="SOCIAL_STATUS_NM" SortExpression="SOCIAL_STATUS_NM" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày nộp sổ %>" DataField="SOCIAL_SUBMIT_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="SOCIAL_SUBMIT_DATE" SortExpression="SOCIAL_SUBMIT_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày cấp sổ %>" DataField="SOCIAL_GRANT_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="SOCIAL_GRANT_DATE" SortExpression="SOCIAL_GRANT_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số lưu trữ %>" DataField="SOCIAL_SAVE_NUMBER" UniqueName="SOCIAL_SAVE_NUMBER" SortExpression="SOCIAL_SAVE_NUMBER" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người giao sổ %>" DataField="SOCIAL_SUBMIT" UniqueName="SOCIAL_SUBMIT" SortExpression="SOCIAL_SUBMIT" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày trả sổ %>" DataField="SOCIAL_RETURN_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="SOCIAL_RETURN_DATE" SortExpression="SOCIAL_RETURN_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người nhận sổ %>" DataField="SOCIAL_RECEIVER" UniqueName="SOCIAL_RECEIVER" SortExpression="SOCIAL_RECEIVER" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="SOCIAL_NOTE" UniqueName="SOCIAL_NOTE" SortExpression="SOCIAL_NOTE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="150px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT(Từ tháng) %>" DataField="HI_FROM_MONTH" DataFormatString="{0:MM/yyyy}" UniqueName="HI_FROM_MONTH" SortExpression="HI_FROM_MONTH" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHYT(Đến tháng) %>" DataField="HI_TO_MONTH" DataFormatString="{0:MM/yyyy}" UniqueName="HI_TO_MONTH" SortExpression="HI_TO_MONTH" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Số thẻ Y tế %>" DataField="HEALTH_NUMBER" UniqueName="HEALTH_NUMBER" SortExpression="HEALTH_NUMBER" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tình trạng thẻ %>" DataField="HEALTH_STATUS_NM" UniqueName="HEALTH_STATUS_NM" SortExpression="HEALTH_STATUS_NM" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ tháng %>" DataField="HEALTH_EFFECT_FROM_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="HEALTH_EFFECT_FROM_DATE" SortExpression="HEALTH_EFFECT_FROM_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến tháng %>" DataField="HEALTH_EFFECT_TO_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="HEALTH_EFFECT_TO_DATE" SortExpression="HEALTH_EFFECT_TO_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: HEALTH_AREA_INS_ID %>" DataField="HEALTH_AREA_INS_ID"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="HEALTH_AREA_INS_ID" SortExpression="HEALTH_AREA_INS_ID" Visible="false"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi khám chữa bệnh %>" DataField="HEALTH_AREA_NM" UniqueName="HEALTH_AREA_NM" SortExpression="HEALTH_AREA_NM" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="200px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày nhận thẻ %>" DataField="HEALTH_RECEIVE_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="HEALTH_RECEIVE_DATE" SortExpression="HEALTH_RECEIVE_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Người nhận thẻ %>" DataField="HEALTH_RECEIVER" DataFormatString="{0:dd/MM/yyyy}" UniqueName="HEALTH_RECEIVER" SortExpression="HEALTH_RECEIVER" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ngày trả thẻ %>" DataField="HEALTH_RETURN_DATE" DataFormatString="{0:dd/MM/yyyy}" UniqueName="HEALTH_RETURN_DATE" SortExpression="HEALTH_RETURN_DATE" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHTN(Từ tháng) %>" DataField="UNEMP_FROM_MONTH" DataFormatString="{0:MM/yyyy}" UniqueName="UNEMP_FROM_MONTH" SortExpression="UNEMP_FROM_MONTH" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHTN(Đến tháng) %>" DataField="UNEMP_TO_MONTH" DataFormatString="{0:MM/yyyy}" UniqueName="UNEMP_TO_MONTH" SortExpression="UNEMP_TO_MONTH" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: UNEMP_REGISTER_MONTH %>" DataField="UNEMP_REGISTER_MONTH"
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="UNEMP_REGISTER_MONTH" SortExpression="UNEMP_REGISTER_MONTH" Visible="false"/>    
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHTNLD_BNN(Từ tháng) %>" DataField="BHTNLD_BNN_FROM_MONTH" DataFormatString="{0:MM/yyyy}" UniqueName="BHTNLD_BNN_FROM_MONTH" SortExpression="BHTNLD_BNN_FROM_MONTH" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"/>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: BHTNLD_BNN(Đến tháng) %>" DataField="BHTNLD_BNN_TO_MONTH" DataFormatString="{0:MM/yyyy}" UniqueName="BHTNLD_BNN_TO_MONTH" SortExpression="BHTNLD_BNN_TO_MONTH" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="110px"/>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHXH %>" DataField="SI_CHK" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="SI_CHK" UniqueName="SI_CHK" >
                                <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHYT %>" DataField="HI_CHK" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="HI_CHK" UniqueName="HI_CHK" >
                                <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTN %>" DataField="UI_CHK" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="UI_CHK" UniqueName="UI_CHK" >
                                <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                            </tlk:GridCheckBoxColumn>
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: BHTNLD_BNN %>" DataField="BHTNLD_BNN_CHK" DataType="System.Boolean" FilterControlWidth="20px"   
                                SortExpression="BHTNLD_BNN_CHK" UniqueName="BHTNLD_BNN_CHK" >
                                <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                            </tlk:GridCheckBoxColumn>   
                            <%--<tlk:GridBoundColumn HeaderText="<%$ Translate: Nơi đóng bảo hiểm %>" DataField="REGISTER_PLACE_NAME" DataFormatString="{0:MM/yyyy}" UniqueName="REGISTER_PLACE_NAME" SortExpression="REGISTER_PLACE_NAME" 
                                FilterControlWidth="99%" ShowFilterIcon="false" CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="110px"/>  --%>                                                   
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
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
            if (item.get_commandName() == "EXPORT" || args.get_item().get_commandName() == 'EXPORT_TEMPLATE') {
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
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsInformations&group=Business&Status=0', "rwPopup");
            oWindow.maximize(true);
            oWindow.center();
        }

        function OpenEdit(status) {
            var bCheck = $find('<%= rgGridData.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var emp_id = $find('<%= rgGridData.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var oWindow = radopen('Dialog.aspx?mid=Insurance&fid=ctrlInsInformations&group=Business&Status=' + status + '&IDSelect=' + id + '&EmployeeID=' + emp_id, "rwPopup");
            oWindow.maximize(true);
            oWindow.center();
            return 0;
        }

        function gridRowDblClick(sender, args) {
            OpenEdit(1); //view
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


        function OnClientBeforeClose(sender, args) {
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
