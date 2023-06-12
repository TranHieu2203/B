<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_LabourProtectionMng.ascx.vb"
    Inherits="Profile.ctrlHU_LabourProtectionMng" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="BottomPanel" runat="server" MinWidth="200" Width="250px" Height="100%"
        Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMenu" runat="server" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="37px">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Ngày cấp phát từ")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFrom" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td align="left">
                            <%# Translate("đến")%>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdTo" runat="server" TabIndex="1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                                    ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" LabelWidth="40%" 
                                    TabIndex="1">
                                </DateInput>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" TabIndex="1" />
                            </tlk:RadDatePicker>
                             <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="rdTo"
                                Type="Date" ControlToCompare="rdFrom" Operator="GreaterThanEqual" ErrorMessage="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"
                                ToolTip="<%$ Translate: Đến ngày phải lớn hơn Từ ngày %>"></asp:CompareValidator>
                        </td>
                                                <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="<%$ Translate: Tìm%>" runat="server" 
                                ToolTip="" SkinID="ButtonFind" TabIndex="2">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
                <tlk:RadGrid PageSize="50" ID="rgLabourProtection" runat="server" Height="100%" 
                    TabIndex="3">
                    <MasterTableView DataKeyNames="ID,WORK_STATUS" EditMode="InPlace" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                ReadOnly="true" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên nhân viên %>" DataField="EMPLOYEE_NAME"
                                ReadOnly="true" UniqueName="EMPLOYEE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                ReadOnly="true" UniqueName="TITLE_NAME">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                            </tlk:GridBoundColumn>
                           
                            <tlk:GridTemplateColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME"  ReadOnly="true"
                                UniqueName="ORG_NAME">
                                <HeaderStyle Width="200px" />
                                <ItemTemplate>
                                 <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ORG_NAME") %>'>
                                </asp:Label>
                                <tlk:RadToolTip RenderMode="Lightweight" ID="RadToolTip1" runat="server" TargetControlID="Label1"
                                                    RelativeTo="Element" Position="BottomCenter">
                                            <%# DrawTreeByString(DataBinder.Eval(Container, "DataItem.ORG_DESC"))%>
                                </tlk:RadToolTip>
                            </ItemTemplate>
                            </tlk:GridTemplateColumn>
                           
						    
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Cấp nhân sự %>" DataField="STAFF_RANK_NAME"
                                ReadOnly="true" UniqueName="STAFF_RANK_NAME" HeaderStyle-Width="90px" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại bảo hộ %>" DataField="LABOURPROTECTION_NAME"
                                ReadOnly="true" UniqueName="LABOURPROTECTION_NAME" HeaderStyle-Width="200px"/>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng %>" DataField="QUANTITY"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" UniqueName="QUANTITY"
                                DataType="System.UInt64" HeaderStyle-Width="90px"  />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Size %>" DataField="LABOUR_SIZE_NAME"
                                ItemStyle-HorizontalAlign="Right" UniqueName="LABOUR_SIZE_NAME" HeaderStyle-Width="50px" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Giá trị %>" DataField="UNIT_PRICE"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" UniqueName="UNIT_PRICE"
                                DataType="System.UInt64" HeaderStyle-Width="90px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp phát %>" DataField="DAYS_ALLOCATED"
                                ItemStyle-HorizontalAlign="Center" UniqueName="DAYS_ALLOCATED" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cần cấp lại %>" DataField="RETRIEVE_DATE"
                                ItemStyle-HorizontalAlign="Center" UniqueName="RETRIEVE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền đặt cọc %>" DataField="DEPOSIT"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" UniqueName="DEPOSIT"
                                DataType="System.UInt64" HeaderStyle-Width="100px" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đã thu hồi %>" DataField="IS_RETRIEVED"
                                SortExpression="IS_RETRIEVED" UniqueName="IS_RETRIEVED" HeaderStyle-Width="90px" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thu hồi %>" DataField="RECOVERY_DATE"
                                ItemStyle-HorizontalAlign="Center" UniqueName="RECOVERY_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="SDESC" ReadOnly="true"
                                UniqueName="SDESC" />
                        </Columns>
                        <HeaderStyle Width="300px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="1000"
            Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move" Modal="true"
            ShowContentDuringLoad="false" OnClientClose="popupclose">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
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
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_LabourProtectionMng_RadSplitter1');
        }
        function CheckValidate() {
            var bCheck = $find('<%= rgLabourProtection.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            return 0;
        }
        function OpenEditContract() {
            var bCheck = $find('<%= rgLabourProtection.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            } else if (bCheck > 1) {
                return 2;
            }
            var id = $find('<%= rgLabourProtection.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var emp_id = $find('<%= rgLabourProtection.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');


            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMngNewEdit&group=Business&gUID=' + id + '', "_self"); /*

            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            return 0;
        }
        function OpenEdit() {
            if (OpenEditContract() == 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            } else if (OpenEditContract() == 2) {
                var m = '<%= Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
            }
            if ($find('<%= rgLabourProtection.ClientID%>').get_masterTableView().get_selectedItems().length == 1) {
                var id = $find('<%= rgLabourProtection.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMngNewEdit&group=Business&gUID=' + id + '', "_self");
            }
            /*
            oWindow.setSize(900, 500);
            oWindow.center(); 
            */
        }
        function OpenNew() {
            
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMngNewEdit&group=Business', "_self"); /*
            oWindow.setSize(900, 500);
            oWindow.center(); */
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT') {
                OpenEdit();
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }
        function gridRowDblClick(sender, eventArgs) {
            OpenEdit();
        }

        var enableAjax = true;
        var oldSize = 0;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgLabourProtection.ClientID %>").get_masterTableView().rebind();
            }
        }

    </script>
</tlk:RadCodeBlock>
