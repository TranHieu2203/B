<%@ Control Language="vb" AutoEventWireup="false" CodeBehind=".ascx.vb"
    Inherits="Profile.ctrlHU_ContractAppendix" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<asp:HiddenField ID="hidID" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="130" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <asp:Label ID="lbEmployee" runat="server" Text="Mã/Tên NV"></asp:Label>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtEmployee" runat="server" CausesValidation="false">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <asp:Label ID="lbStatus" runat="server" Text="Trạng thái"></asp:Label>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cbStatus" runat="server" CssClass="RadComboBox_Hay" AutoPostBack="false"
                                    CausesValidation="false">
                                </tlk:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="lb">
                                <asp:Label ID="lbStartDate" runat="server" Text="Ngày bắt đầu"></asp:Label>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdStartDate" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <asp:Label ID="lbExpireDate" runat="server" Text="Ngày kết thúc"></asp:Label>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="lb">
                                <asp:Label ID="lbUpload" runat="server" Text="Tập tin đính kèm"></asp:Label>
                            </td>
                            <td>
                                <tlk:RadComboBox ID="cboUpload" runat="server" CheckBoxes="true" SkinID="number"
                                    Width="150px">
                                </tlk:RadComboBox>
                                <tlk:RadTextBox ID="txtUploadFile" runat="server" Visible="false">
                                </tlk:RadTextBox>
                                <tlk:RadButton runat="server" ID="btnUploadFile" SkinID="ButtonView" />
                                <tlk:RadButton ID="btnDownload" runat="server" Text="Tải tập tin" OnClientClicked="rbtClicked">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkTerminate" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                            </td>
                            <td>
                                <tlk:RadButton ID="btnSearch" EnableEmbeddedSkins="false" SkinID="ButtonFind" OnClick="btnSearch_Click"
                                    runat="server" Text="Tìm kiếm">
                                </tlk:RadButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgContract" runat="server" Height="100%" AllowPaging="True" CssClass="MyCustomClass"
                    AllowSorting="True" AllowMultiRowSelection="True" Width="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS_ID,ORG_DESC" ClientDataKeyNames="ID,ORG_ID,FORM_ID,EMPLOYEE_ID,STATUS_ID,STATUS_NAME,EMPLOYEE_CODE,START_DATE,CONTRACTTYPE_CODE">
                        <Columns>
                            <%--   <tlk:GridClientSelectColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px"
                                ItemStyle-HorizontalAlign="Center" UniqueName="cbStatus">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                              <tlk:GridBoundColumn DataField="STATUS_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="FORM_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="MSNV" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Số HĐLĐ" DataField="CONTRACT_NO"
                                EmptyDataText="" SortExpression="CONTRACT_NO" UniqueName="CONTRACT_NO" />
                            <tlk:GridBoundColumn HeaderText="Loại hợp đồng" DataField="CONTRACTTYPE_NAME"
                                SortExpression="CONTRACTTYPE_NAME" UniqueName="CONTRACTTYPE_NAME" />
                                 <tlk:GridBoundColumn HeaderText="Số phụ lục hợp đồng" DataField="APPEND_NUMBER"
                                SortExpression="APPEND_NUMBER" UniqueName="APPEND_NUMBER" />
                                 <tlk:GridBoundColumn HeaderText="Loại phụ lục hợp đồng" DataField="APPEND_TYPE_NAME"
                                SortExpression="APPEND_TYPE_NAME" UniqueName="APPEND_TYPE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" />
                            <tlk:GridBoundColumn HeaderText="Ngày bắt đầu" DataField="START_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="START_DATE" UniqueName="START_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="Ngày kết thúc" DataField="EXPIRE_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE" UniqueName="EXPIRE_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />
                            <tlk:GridBoundColumn HeaderText="Ngày ký" DataField="SIGN_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" Visible="false" />--%>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phliquidate" runat="server"></asp:PlaceHolder>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientClose="popupclose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize"
            Modal="true" ShowContentDuringLoad="false" Title="<%$ Translate: Tạo hợp đồng %>">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        function OpenDeclare() {

            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContractNewEdit&group=Business&noscroll=1&FormType=0', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
        }

        function OpenTemplete() {
            var bCheck = $find('<%# rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ContractTemplete&group=Business&noscroll=1&add=1', "_self");
                var pos = $("html").offset();
                oWindow.maximize(true);
            }
            else if (bCheck == 1) {
                var emp_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                console.log(emp_id);
                window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ContractTemplete&group=Business&noscroll=1&add=1&EmpID=' + emp_id, "_self");
                var pos = $("html").offset();
                oWindow.maximize(true);
            }
            else {
                var n = noty({ text: 'Không thể chọn nhiều dòng để thực hiện khai báo PLHĐ', dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
        }

        function OpenExtend() {
            var bCheck = $find('<%# rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var status_code = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_CODE');

            if (status_code == 0) {
                return 2;
            }
            var id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var org_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ORG_ID');
            var emp_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            if (id == null) {
                return 1;
            }

            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContractNewEdit&group=Business&noscroll=1&IDSelect=' + id + '&OrgID=' + org_id + '&empid=' + emp_id + '&FormType=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            return 0;
        }

        function OpenDetail() {
            var bCheck = $find('<%# rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return false;
            }
            var id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var org_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ORG_ID');
            var emp_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');


            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContractNewEdit&group=Business&noscroll=1&IDSelect=' + id + '&OrgID=' + org_id + '&empid=' + emp_id + '&FormType=2', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            return true;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'DECLARE') {
                OpenDeclare();
                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == 'APPROVE') {
                OpenTemplete();
                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == 'EXTEND') {
                if (OpenExtend() == 1) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenExtend() == 2) {
                    var m = '<%# Translate("Hợp đồng chưa phê duyệt, không thể thực hiện thao tác này") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);

            }
            if (args.get_item().get_commandName() == 'DETAIL') {
                if (OpenDetail() == false) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == "SYNC" || args.get_item().get_commandName() == "NEXT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                if (OpenEditContract() == 1) {
                    var m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenEditContract() == 2) {
                    var m = '<%= Translate("Bạn không thể sửa nhiều bản ghi! Không thể thực hiện thao tác này") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'RESET_PASSWORD') {
                var bCheck = $find('<%# rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return false;
                }
                var status_code = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');
                //alert(status_code);
                if (status_code == 471) {
                    var id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                    var org_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ORG_ID');
                    var emp_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                    var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContractNewEdit&group=Business&noscroll=1&IDSelect=' + id + '&OrgID=' + org_id + '&empid=' + emp_id + '&FormType=0' + '&statuscode=' + status_code + '&APPROVAL=' + 1, "rwPopup");
                    var pos = $("html").offset();
                    oWindow.moveTo(pos.left, pos.top);
                    oWindow.setSize($(window).width(), $(window).height());
                    args.set_cancel(true);
                }
                else {
                    m = '<%# Translate("Chức năng này chỉ sử dụng để sửa những bản ghi đã phê duyệt!") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return false;
                }
            };

        }

        function OpenEditContract() {
            var bCheck = $find('<%# rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            } else if (bCheck > 1) {
                return 2;
            }
            OpenEditContractTemplate();
            return 0;
            //            var status_code = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_CODE');

            //            if (status_code == 1) {
            //                return 2;
            //            }
            //            var id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            //            var org_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ORG_ID');
            //            var emp_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');


            //            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContractNewEdit&group=Business&noscroll=1&IDSelect=' + id + '&OrgID=' + org_id + '&empid=' + emp_id + '&FormType=0' + '&statuscode=' + status_code, "rwPopup");
            //            var pos = $("html").offset();
            //            oWindow.moveTo(pos.left, pos.top);
            //            oWindow.setSize($(window).width(), $(window).height());
            //            return 0;
        }

        function OpenEditContractDbl() {
            var bCheck = $find('<%# rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var status_code = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_CODE');

            var id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var org_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ORG_ID');
            var emp_id = $find('<%# rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');


            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlContractNewEdit&group=Business&noscroll=1&IDSelect=' + id + '&OrgID=' + org_id + '&empid=' + emp_id + '&FormType=0' + '&statuscode=' + status_code, "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
            return 0;
        }

        function OpenEditContractTemplate() {
            var bCheck = $find('<%= rgContract.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            var id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var emp_id = $find('<%= rgContract.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');


            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_ContractTemplete&group=Business&noscroll=1&IDSelect=' + id + '&empid=' + emp_id, "_self");

            oWindow.maximize(true);
            return 0;
        }

        var enableAjax = true;
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

        function btnAdvancedFind_Click() {
            var fn = function () {
                var oWnd = $find('<%= popupId %>');
                var grid = $find('<%= rgContract.ClientID%>')
                var emp_id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                var idCT = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                oWnd.add_close(OnClientClose);
                oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlContract_Liquidate&group=Business&noscroll=1&empid=' + emp_id + '&idCT=' + idCT);
                oWnd.show();
                Sys.Application.remove_load(fn);
            };
            Sys.Application.add_load(fn);
        }

        function OnClientClose(oWnd, args) {
            oWnd = $find('<%#popupId %>');
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }

        function OpenDialogEmployee(e) {
            var fn = function () {
                var oWnd = $find('<%= popupId2 %>');
                var grid = $find('<%= rgContract.ClientID%>')
                var empId = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                var empCode = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_CODE');
                oWnd.add_close(OnClientClose2);
                oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlEmpDtl&group=Business&Decimal=' + empId + '&emp=' + empCode + '&state=Normal');
                oWnd.setSize(screen.width - 50, screen.height - 100);
                oWnd.show();
                Sys.Application.remove_load(fn);
            };
            Sys.Application.add_load(fn);
        }
        function OnClientClose2(oWnd, args) {
            oWnd = $find('<%#popupId2 %>');
            oWnd.setSize(screen.width - 250, screen.height - 300);
            oWnd.remove_close(OnClientClose);
            var arg = args.get_argument();
            if (arg) {
                postBack(arg);
            }
        }

        function rbtClicked(sender, eventArgs) {
            enableAjax = false;
        }

        function gridRowDblClick(sender, eventArgs) {
            OpenEditContractTemplate();
        }
    </script>
</tlk:RadCodeBlock>
