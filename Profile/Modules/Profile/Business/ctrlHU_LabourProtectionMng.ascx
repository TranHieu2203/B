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
            <tlk:RadPane ID="RadPane3" runat="server" Scrolling="None" Height="80px">
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
                            <tlk:RadDatePicker ID="rdTo" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="<%$ Translate: Liệt kê cả nhân viên nghỉ việc %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" Text="Tìm kiếm" runat="server" ToolTip="">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None" Height="100%">
                <tlk:RadGrid PageSize=50 ID="rgLabourProtection" runat="server" Height="100%">
                    <MasterTableView DataKeyNames="ID,WORK_STATUS" EditMode="InPlace" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" ReadOnly="true" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                ReadOnly="true" UniqueName="EMPLOYEE_CODE" />
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
                                <HeaderStyle Width="150px" />
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
                                ReadOnly="true" UniqueName="STAFF_RANK_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại bảo hộ %>" DataField="LABOURPROTECTION_NAME"
                                ReadOnly="true" UniqueName="LABOURPROTECTION_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng %>" DataField="QUANTITY"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" UniqueName="QUANTITY"
                                DataType="System.UInt64" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Size %>" DataField="LABOUR_SIZE_NAME" ItemStyle-HorizontalAlign="Right"
                                 UniqueName="LABOUR_SIZE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Giá trị %>" DataField="UNIT_PRICE"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" UniqueName="UNIT_PRICE"
                                DataType="System.UInt64" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày cấp phát %>" DataField="DAYS_ALLOCATED"
                                ItemStyle-HorizontalAlign="Center" UniqueName="DAYS_ALLOCATED" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày thu hồi %>" DataField="RETRIEVE_DATE"
                                ItemStyle-HorizontalAlign="Center" UniqueName="RETRIEVE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px" />
                            </tlk:GridDateTimeColumn>
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Tiền đặt cọc %>" DataField="DEPOSIT"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" UniqueName="DEPOSIT"
                                DataType="System.UInt64" />
                            <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Đã thu hồi %>" DataField="IS_RETRIEVED"
                                SortExpression="IS_RETRIEVED" UniqueName="IS_RETRIEVED" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mô tả %>" DataField="SDESC" ReadOnly="true"
                                UniqueName="SDESC" />
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
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
        function CheckValidate()
        {
            var bCheck = $find('<%= rgLabourProtection.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
            {
                return 1;
            }
            return 0;
        }
        function OpenNew()
        {
            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMngNewEdit&group=Business&noscroll=1', "rwPopup");
            oWindow.setSize(900, 500);
            oWindow.center();
        }
        function OpenEdit()
        {
            var id = $find('<%= rgLabourProtection.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_LabourProtectionMngNewEdit&group=Business&gUID=' + id + '&noscroll=1', "rwPopup");
            oWindow.setSize(900, 500);
            oWindow.center();
        }

        function OnClientButtonClicking(sender, args)
        {
            var item = args.get_item();
            if (args.get_item().get_commandName() == 'CREATE')
            {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT')
            {
                OpenEdit();
                args.set_cancel(true);
            }
            if (item.get_commandName() == "EXPORT")
            {
                enableAjax = false;
            }
        }
        var enableAjax = true;
        var oldSize = 0;
        function onRequestStart(sender, eventArgs)
        {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function gridRowDblClick(sender, eventArgs)
        {
            OpenEdit();
        }

        function popupclose(sender, args)
        {
            var m;
            var arg = args.get_argument();
            if (arg == '1')
            {
                m = '<%= Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                $find("<%= rgLabourProtection.ClientID %>").get_masterTableView().rebind();
            }

        }

    </script>
</tlk:RadCodeBlock>
