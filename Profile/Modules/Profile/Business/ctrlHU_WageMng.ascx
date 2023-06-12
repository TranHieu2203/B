<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_WageMng.ascx.vb"
    Inherits="Profile.ctrlHU_WageMng" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<%@ Import Namespace="HistaffWebAppResources.My.Resources" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganizationLoadOnDemand ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarWorkings" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="60px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbEffectDate" runat="server" Text="Từ ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdEffectDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbExpireDate" runat="server" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                        </td>
                        </tr>
                    <tr>
                        <td>
                        </td>
                        
                        <td>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkhsl" runat="server" Text="Hồ sơ lương mới nhất" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgWorking" runat="server" AllowMultiRowSelection="true"
                    Height="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EFFECT_DATE,STATUS_ID,DECISION_TYPE_ID,EMPLOYEE_CODE,ORG_DESC" ClientDataKeyNames="ID,EMPLOYEE_ID">
                        <Columns>
                           <%-- <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridTemplateColumn HeaderText="Đơn vị" DataField="ORG_NAME" SortExpression="ORG_NAME"
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
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE" ItemStyle-HorizontalAlign="Center"
                                SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày hết hiệu lực" DataField="EXPIRE_DATE" ItemStyle-HorizontalAlign="Center"
                                SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="Nhóm lương" DataField="SAL_TYPE_NAME" SortExpression="SAL_TYPE_NAME"
                                UniqueName="SAL_TYPE_NAME" />
                            <tlk:GridNumericColumn HeaderText="Biểu thuế" DataField="TAX_TABLE_Name" SortExpression="TAX_TABLE_Name"
                                UniqueName="TAX_TABLE_Name" />
                            <tlk:GridBoundColumn HeaderText="Thang lương " DataField="SAL_GROUP_NAME" UniqueName="SAL_GROUP_NAME"
                                SortExpression="SAL_GROUP_NAME" ShowFilterIcon="true" />
                            <tlk:GridNumericColumn HeaderText="Hệ số" DataField="FACTORSALARY" UniqueName="FACTORSALARY"
                                SortExpression="FACTORSALARY" ShowFilterIcon="true" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="Lương cơ bản" DataField="SAL_BASIC" SortExpression="SAL_BASIC"
                                UniqueName="SAL_BASIC" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="% Lương được hưởng" DataField="PERCENTSALARY"
                                UniqueName="PERCENTSALARY" SortExpression="PERCENTSALARY" ShowFilterIcon="true" />
                            <tlk:GridNumericColumn HeaderText="Thưởng hiệu quả công việc" DataField="OTHERSALARY2"
                                UniqueName="OTHERSALARY2" SortExpression="OTHERSALARY2" ShowFilterIcon="true"
                                DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="Tổng Lương" DataField="SAL_TOTAL" UniqueName="SAL_TOTAL"
                                SortExpression="SAL_TOTAL" ShowFilterIcon="true" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="Chi phí hỗ trợ " DataField="COST_SUPPORT" UniqueName="COST_SUPPORT"
                                SortExpression="COST_SUPPORT" ShowFilterIcon="true" DataFormatString="{0:n0}" />
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME" SortExpression="STATUS_NAME"
                                UniqueName="STATUS_NAME" />
                            <tlk:GridNumericColumn HeaderText="" DataField="ResponsibilityAllowances" SortExpression="ResponsibilityAllowances"
                                UniqueName="ResponsibilityAllowances" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="WorkAllowances" SortExpression="WorkAllowances"
                                UniqueName="WorkAllowances" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="AttendanceAllowances" SortExpression="AttendanceAllowances"
                                UniqueName="AttendanceAllowances" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="HousingAllowances" SortExpression="HousingAllowances"
                                UniqueName="HousingAllowances" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="CarRentalAllowances" SortExpression="CarRentalAllowances"
                                UniqueName="CarRentalAllowances" DataFormatString="{0:n0}" />
                            <tlk:GridNumericColumn HeaderText="" DataField="SAL_INS" SortExpression="SAL_INS"
                                UniqueName="SAL_INS" DataFormatString="{0:n0}" />
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />--%>
                        </Columns>
                        <HeaderStyle Width="120px" />
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
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="900px"
            OnClientClose="popupclose" Height="640px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
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


        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenWage() {
            var extented = '';
            var bCheck = $find('<%= rgWorking.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck > 1) {
                var n = noty({ text: 'Không thể chọn nhiều dòng để thực hiện thao tác này', dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            if (bCheck == 1) {
                empID = $find('<%= rgWorking.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                if (empID)
                    extented = '&empid=' + empID;
            }

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business' + extented, "_self"); /*
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function OpenWageEdit(e) {
            var bCheck = $find('<%= rgWorking.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var id = $find('<%= rgWorking.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_WageNewEdit&group=Business&ID=' + id, "_self"); /*
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            return 0;
        }

        function OnClientButtonClicking(sender, args) {
            var m;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenWage();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT') {
                if (OpenWageEdit(false) == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                }
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            if (OpenWageEdit(true) == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }
            args.set_cancel(true);
        }

        function popupclose(sender, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $get("<%= btnSearch.ClientId %>").click();
            }

        }

        function getUrlVars() {
            var vars = {};
            var parts = window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        }

    </script>
</tlk:RadCodeBlock>
