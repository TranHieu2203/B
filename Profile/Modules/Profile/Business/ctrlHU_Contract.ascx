<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Contract.ascx.vb"
    Inherits="Profile.ctrlHU_Contract" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="38px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="70px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbFromDate" runat="server" Text="Ngày bắt đầu từ"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                           <asp:Label ID="lbToDate" runat="server" Text="Đến"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="Loại hợp đồng"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboContractType" runat="server">
                            </tlk:RadComboBox>
                        </td>
                        <td></td>
                        <td >
                            <asp:CheckBox ID="chkchg" runat="server" Text="<%$ Translate: Hợp đồng mới nhất %>" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgContract" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                         <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3"/>
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ORG_ID,EMPLOYEE_ID,EMPLOYEE_CODE,STATUS_CODE,STATUS_ID,CONTRACTTYPE_ID,CONTRACTTYPE_TYPE_CODE,CONTRACTTYPE_CODE,ORG_DESC,LIQUIDATION_DATE,REMARK_LIQUIDATION"
                        ClientDataKeyNames="ID,ORG_ID,EMPLOYEE_ID,STATUS_CODE,CONTRACTTYPE_CODE,STATUS_ID,CONTRACTTYPE_ID,EMPLOYEE_CODE,CONTRACTTYPE_TYPE_CODE,LIQUIDATION_DATE,REMARK_LIQUIDATION" >
                        
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="STATUS_CODE" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viêngFormatCurrencyVN" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />  
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
                            <tlk:GridBoundColumn HeaderText="Chức danhgFormatCurrencyVN" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Số HĐLĐgFormatCurrencyVN" DataField="CONTRACT_NO"
                                SortExpression="CONTRACT_NO" UniqueName="CONTRACT_NO" />
                            <tlk:GridBoundColumn HeaderText="Loại hợp đồnggFormatCurrencyVN" DataField="CONTRACTTYPE_NAME"
                                SortExpression="CONTRACTTYPE_NAME" UniqueName="CONTRACTTYPE_NAME" HeaderStyle-Width="250px" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày bắt đầugFormatCurrencyVN" DataField="START_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="START_DATE" UniqueName="START_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridDateTimeColumn HeaderText="Ngày kết thúcgFormatCurrencyVN" DataField="EXPIRE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Người kýgFormatCurrencyVN" DataField="SIGNER_NAME"
                                SortExpression="SIGNER_NAME" UniqueName="SIGNER_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày kýgFormatCurrencyVN" DataField="SIGN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>
                            <tlk:GridBoundColumn HeaderText="Trạng tháigFormatCurrencyVN" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="ORG_DESC" DataField="ORG_DESC" UniqueName="ORG_DESC"
                                SortExpression="ORG_DESC" Visible="false" />--%>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>         
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientClose="popupclose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Tạo hợp đồng %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OpenNew() {
            var extented = '';
            var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 1) {
                empID = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                if (empID)
                    extented = '&empid=' + empID;
            } else if (bCheck > 1) {
                var m = '<%= Translate("Bạn không thể copy dữ liệu từ nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ContractNewEdit&group=Business' + extented, "_self");
        }

        function OpenEditContract() {
            var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            } else if (bCheck > 1) {
                return 2;
            }
            var id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var emp_id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');


            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ContractNewEdit&group=Business&IDSelect=' + id + '&empid=' + emp_id, "_self"); /*

            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            return 0;
        }
        function OPENTHANHLY() {
            var items = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            } else if (items.length > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }            
            var status = items[0].getDataKeyValue('STATUS_ID');
            if (status == 446) {
                var m = '<%= Translate("Không thể thanh lý hợp đồng chưa phê duyệt") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            var emp_id = items[0].getDataKeyValue('EMPLOYEE_ID');
            var idCT = items[0].getDataKeyValue('ID');
            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContract_Liquidate&group=Business&noscroll=1&empid=' + emp_id + '&idCT=' + idCT, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize(800, 300);
            oWindow.center();
        }
        var enableAjax = true;
        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "CHECK") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "UNLOCK") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "IMPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'REFRESH') {
                OPENTHANHLY();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "PRINT_SN") {
                var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "NEXT") {
                var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                    return;
                }
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                if (OpenEditContract() == 1) {
                    var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                } else if (OpenEditContract() == 2) {
                    var m = '<%= Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);
            }
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEditContract();
        }

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
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $get("<%= btnSearch.ClientId %>").click();
            }

        }


        function btnPrintSupportClick(sender, args) {
            var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                args.set_cancel(true);
                return;
            }
            //            if (bCheck > 1) {
            //                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
            //                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
            //                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            //                args.set_cancel(true);
            //                return;
            //            }
            enableAjax = false;
        }
//        function Liquidation_Click() {
//            var emp_id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
//            var idCT = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
//            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContract_Liquidate&group=Business&noscroll=1&empid=' + emp_id + '&idCT=' + idCT, "rwPopup");
//            var pos = $("html").offset();
//            oWindow.moveTo(pos.left, pos.top);
//            oWindow.setSize($(window).width(), $(window).height());
//        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
