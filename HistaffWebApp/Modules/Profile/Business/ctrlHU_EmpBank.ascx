<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_EmpBank.ascx.vb"
    Inherits="Profile.ctrlHU_EmpBank" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
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
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="50px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbFromDate" runat="server" Text="Ngày vào làm từ"></asp:Label>
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
                         <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr style="display:none">
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
                       
                    </tr>
                </table>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgContract" runat="server" Height="100%" AllowPaging="True"
                    AllowSorting="True" AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                         <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3"/>
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_NAME,TITLE_NAME,STK,BANK_NAME,PERSON_INHERITANCE,ORG_DESC"
                        ClientDataKeyNames="ID,EMPLOYEE_ID,EMPLOYEE_CODE,EMPLOYEE_NAME,ORG_NAME,TITLE_NAME,STK,BANK_NAME,PERSON_INHERITANCE" >
                        
                        <Columns>
                            
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
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_EmpBankNewEdit&group=Business' + extented, "_self");
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


            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_EmpBankNewEdit&group=Business&IDSelect=' + id + '&empid=' + emp_id, "_self"); /*

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
            var typeCode = items[0].getDataKeyValue('CONTRACTTYPE_CODE');
            if (typeCode == 'HDKXDTH') {
                var m = '<%= Translate("Không thể thanh lý Hợp đồng không xác định thời hạn") %>';
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
            if (args.get_item().get_commandName() == "PRINT") {
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
