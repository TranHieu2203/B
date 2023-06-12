<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Concurrently.ascx.vb"
    Inherits="Profile.ctrlHU_Concurrently" %>
<%@ Import Namespace="Common" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
         <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="80px" Scrolling="None">
                <tlk:RadToolBar ID="tbarContracts" runat="server" OnClientButtonClicking="clientButtonClicking" />
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                    <table class="table-form">
                        <tr>
                            <td class="lb">
                                <%# Translate("Mã/Tên NV")%>
                            </td>
                            <td>
                                <tlk:RadTextBox ID="txtEmployee" runat="server" CausesValidation="false">
                                </tlk:RadTextBox>
                            </td>
                            <td class="lb">
                                <%# Translate("Trạng thái QĐ kiêm nhiệm")%>
                            </td>
                            <td>
                                <tlk:RadComboBox 
                                    ID="cbSTATUS" runat="server">
                                </tlk:RadComboBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkTerminate" runat="server" Text="<%$ Translate: Liệt kê NV nghỉ việc %>" />
                            </td>
                            <td>
                                <tlk:RadButton ID="btnSearch" SkinID="ButtonFind" OnClick="btnSearch_Click" runat="server"
                                    Text="<%$ Translate: Tìm kiếm %>">
                                </tlk:RadButton>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="lb">
                                <%# Translate("Ngày bắt đầu")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdStartDate" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            <td class="lb">
                                <%# Translate("Ngày kết thúc")%>
                            </td>
                            <td>
                                <tlk:RadDatePicker ID="rdExpireDate" runat="server">
                                </tlk:RadDatePicker>
                            </td>
                            
                        </tr>
                    </table>
                </asp:Panel>
                <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgConcurrently" runat="server" Height="100%" AllowPaging="True" CssClass="MyCustomClass"
                    AllowSorting="True" AllowMultiRowSelection="True">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,STATUS,EXPIRE_DATE_CON,SIGN_NAME,SIGN_TITLE_NAME,SIGN_DATE,ORG_DESC,EMPLOYEE_ID" ClientDataKeyNames="ID,EMPLOYEE_ID,STATUS,EXPIRE_DATE_CON,ORG_ID_DESC,ORG_CON,TITLE_CON,ORG_NAME,TITLE_NAME,TITLE_ID,ORG_ID,STATUS_STOP,SIGN_NAME,SIGN_TITLE_NAME,SIGN_DATE">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn DataField="EMPLOYEE_ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="Họ và tên" DataField="FULLNAME_VN"
                                SortExpression="FULLNAME_VN" UniqueName="FULLNAME_VN" HeaderStyle-Width="150px" />
                             <tlk:GridBoundColumn HeaderText="Trạng thái" DataField="STATUS_NAME"
                                SortExpression="STATUS_NAME" UniqueName="STATUS_NAME" HeaderStyle-Width="120px" />
                            <%--<tlk:GridBoundColumn HeaderText="Trạng thái thôi kiêm nhiệm" DataField="STATUS_STOP_NAME"
                                SortExpression="STATUS_STOP_NAME" UniqueName="STATUS_STOP_NAME" HeaderStyle-Width="120px" />--%>

                            <tlk:GridBoundColumn HeaderText="Công ty" DataField="COM_ORG_NAME"
                                SortExpression="COM_ORG_NAME" UniqueName="COM_ORG_NAME" HeaderStyle-Width="200px" />  

                            <tlk:GridBoundColumn HeaderText="Ban/Phòng" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="200px" />  
                            <tlk:GridBoundColumn HeaderText="Vị trí công việc" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME"  HeaderStyle-Width="150px" />

                            <tlk:GridBoundColumn HeaderText="Công ty kiêm nhiệm" DataField="COM_ORG_CON_NAME"
                                SortExpression="COM_ORG_CON_NAME" UniqueName="COM_ORG_CON_NAME" HeaderStyle-Width="200px" />  

                            <tlk:GridBoundColumn HeaderText="Ban/Phòng kiêm nhiệm" DataField="ORG_CON_NAME"
                                SortExpression="ORG_CON_NAME" UniqueName="ORG_CON_NAME" HeaderStyle-Width="200px" />  
                            <tlk:GridBoundColumn HeaderText="Vị trí kiêm nhiệm" DataField="TITLE_CON_NAME"
                                SortExpression="TITLE_CON_NAME" UniqueName="TITLE_CON_NAME"  HeaderStyle-Width="150px" />

                            
                            <%--<tlk:GridBoundColumn HeaderText="Chức vụ kiêm nhiệm" DataField="CHUCVU_CON_NAME"
                                SortExpression="CHUCVU_CON_NAME" UniqueName="CHUCVU_CON_NAME"  HeaderStyle-Width="150px" />--%>
                            <%--<tlk:GridBoundColumn HeaderText="Thù lao kiêm nhiệm" DataField="TITLE_CON_NAME2"
                                SortExpression="TITLE_CON_NAME2" UniqueName="TITLE_CON_NAME2"  HeaderStyle-Width="150px" />--%>

                            <tlk:GridBoundColumn HeaderText="Ngày hiệu lực kiêm nhiệm" DataField="EFFECT_DATE_CON"  HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE_CON" UniqueName="EFFECT_DATE_CON"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="Ngày kết thúc" DataField="EXPIRE_DATE_CON"  HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EXPIRE_DATE_CON" UniqueName="EXPIRE_DATE_CON"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                           <%-- <tlk:GridDateTimeColumn HeaderText="Ngày thôi kiêm nhiệm" DataField="EFFECT_DATE_STOP"  HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE_STOP" UniqueName="EFFECT_DATE_STOP"
                                DataFormatString="{0:dd/MM/yyyy}">
                            </tlk:GridDateTimeColumn>--%>
                             <tlk:GridBoundColumn HeaderText="Số quyết định" DataField="CON_NO"
                                SortExpression="CON_NO" UniqueName="CON_NO"  HeaderStyle-Width="150px" />
                             <tlk:GridBoundColumn HeaderText="Người ký" DataField="SIGN_NAME"
                                SortExpression="SIGN_NAME" UniqueName="SIGN_NAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="Chức danh người ký " DataField="SIGN_TITLE_NAME"
                                SortExpression="SIGN_TITLE_NAME" UniqueName="SIGN_TITLE_NAME" HeaderStyle-Width="120px" />
                            <tlk:GridBoundColumn HeaderText="Ngày ký" DataField="SIGN_DATE"  HeaderStyle-Width="150px"
                                ItemStyle-HorizontalAlign="Center" SortExpression="SIGN_DATE" UniqueName="SIGN_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" CurrentFilterFunction="EqualTo">
                            </tlk:GridBoundColumn>
                            <%--<tlk:GridBoundColumn HeaderText="Người ký 2" DataField="SIGN_TITLE_NAME"
                                SortExpression="SIGN_NAME2" UniqueName="SIGN_NAME2" HeaderStyle-Width="120px" />  --%>
                            <%--<tlk:GridBoundColumn HeaderText="Chức danh người ký 2" DataField="SIGN_TITLE_NAME2"
                                SortExpression="SIGN_TITLE_NAME2" UniqueName="SIGN_TITLE_NAME2" HeaderStyle-Width="120px" />      --%>   
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" Width="950px"
            OnClientBeforeClose="OnClientBeforeClose" OnClientClose="OnClientClose" Height="600px"
            EnableShadow="true" Behaviors="Close, Maximize" Modal="true" ShowContentDuringLoad="true">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OpenTransfer() {
            window.open('Default.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=0', "_blank");
            //var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=0', "rwPopup");
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            //oWindow.setSize($(window).width(), $(window).height());
        }
        function getDateTime() {
            var now = new Date();
            var year = now.getFullYear();
            var month = now.getMonth() + 1;
            var day = now.getDate();
            var hour = now.getHours();
            var minute = now.getMinutes();
            var second = now.getSeconds();
            if (month.toString().length == 1) {
                var month = '0' + month;
            }
            if (day.toString().length == 1) {
                var day = '0' + day;
            }
            if (hour.toString().length == 1) {
                var hour = '0' + hour;
            }
            if (minute.toString().length == 1) {
                var minute = '0' + minute;
            }
            if (second.toString().length == 1) {
                var second = '0' + second;
            }
            var dateTime = day + '/' + month + '/' + year + ' ' + hour + ':' + minute + ':' + second;
            return dateTime;
        }
        function OpenTransfer_New(e) {

            var bCheck = $find('<%# rgConcurrently.ClientID %>').get_masterTableView().get_selectedItems().length;

            if (bCheck == 0) {
                return 1;
            }
            if (bCheck > 1) {
                return 3;
            }
            var status = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_STOP');
            if (status == 1) {
                return 2;
            }
            if (bCheck == 1) {
                return 5;
            }

            //var EXPIRE_DATE_CON = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EXPIRE_DATE_CON');
            //var currentdate = getDateTime();

            //            if (EXPIRE_DATE_CON !== "") {
            //                if (EXPIRE_DATE_CON < currentdate) {
            //                    return 4;
            //                }
            //            };

            return 0;
        }
        function OpenImportTransfer() {

            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlDecisionTransfer&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
        }

        function OpenCreateBatch() {

            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlDecisionBatch&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
        }

        function OpenTransfers() {

            var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlTransfers&group=Business&noscroll=1', "rwPopup");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width(), $(window).height());
        }

        function OpenEditTransfer(e) {
            var bCheck = $find('<%# rgConcurrently.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            if (bCheck > 1) {
                return 3;
            }
            if (e == false) {
                var status_code = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS');
                if (status_code == 1) {
                    return 2;
                }
            }

            //var empID = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            //var idDecision = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

            //var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=1&empID=' + empID + '&IDSelect=' + idDecision, "rwPopup");
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            //oWindow.setSize($(window).width(), $(window).height());
            //window.open('Default.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=1&empID=' + empID + '&IDSelect=' + idDecision, "_blank");
            return 0;
        }
        function OpenEditTransfer2(e) {
            var bCheck = $find('<%# rgConcurrently.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return 1;
            }
            if (bCheck > 1) {
                m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return 3;
            }
            if (e == false) {
                var status_code = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS');
                if (status_code != 447) {
                    m = '<%# Translate("Bản ghi đang ở trạng thái chưa phê duyệt không khai báo thông tin thôi kiêm nhiệm. Kiểm tra lại") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return 2;
                }
            }
            var empID = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var idDecision = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            window.open('Default.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=1&empID=' + empID + '&IDSelect=' + idDecision + '&IsConStop=1', "_blank");
            return 0;
        }

        function clientButtonClicking(sender, args) {
            var m;
            //alert(args.get_item().get_commandName());
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenTransfer();
                args.set_cancel(true);
            }
            debugger;
            if (args.get_item().get_commandName() == 'ACTIVE') {
                if (OpenTransfer_New(false) == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenTransfer_New(false) == 3) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenTransfer_New(false) == 2) {
                    m = 'Bản ghi đang ở trạng thái phê duyệt không thể sửa. Kiểm tra lại';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenTransfer_New(false) == 4) {
                    m = 'Ngày hết hiệu lực kiêm nhiệm đã hết hạn';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenTransfer_New(false) == 5) {
                    var empID = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');

                    var idDecision = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');

                    //var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&Is_con=1&FormType=1&empID=' + empID + '&IDSelect=' + idDecision, "rwPopup");
                    //var pos = $("html").offset();
                    //oWindow.moveTo(pos.left, pos.top);
                    //oWindow.setSize($(window).width(), $(window).height());
                    window.open('Default.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&Is_con=1&FormType=1&empID=' + empID + '&IDSelect=' + idDecision, "_blank");
                }
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EDIT') {
                if (OpenEditTransfer(false) == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenEditTransfer(false) == 2) {
                    m = '<%# Translate("Chỉ khai báo thông tin thôi kiêm nhiệm có trạng thái phê duyệt. Kiểm tra lại") %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenEditTransfer(false) == 3) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (OpenEditTransfer(false) == 0) {
                    var empID = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                    var idDecision = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID'); 
                    window.open('Default.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=1&empID=' + empID + '&IDSelect=' + idDecision, "_blank");
                }
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT" || args.get_item().get_commandName() == "REJECT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "DECISION_IMPORT") {
                OpenImportTransfer();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "CREATE_BATCH") {
                OpenEditTransfer2(false);
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'RESET_PASSWORD') {
                var bCheck = $find('<%# rgConcurrently.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    return false;
                }
                var status_code = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS');
                //alert(status_code);
                if (status_code == 1) {
                    var empID = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                    var idDecision = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                    var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=1&Check=1&empID=' + empID + '&IDSelect=' + idDecision, "rwPopup");
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


        function gridRowDblClick(sender, eventArgs) {
            var empID = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
            var idDecision = $find('<%# rgConcurrently.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            //var oWindow = radopen('Dialog.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=2&empID=' + empID + '&IDSelect=' + idDecision, "rwPopup");
            //var pos = $("html").offset();
            //oWindow.moveTo(pos.left, pos.top);
            //oWindow.setSize($(window).width(), $(window).height());
            window.open('Default.aspx?mid=Profile&fid=ctrlHU_ConcurrentlyNewEdit&group=Business&noscroll=1&FormType=2&empID=' + empID + '&IDSelect=' + idDecision, "_blank");
            return 0;
        }

        function popupclose(oWnd, args) {
            window.location.reload();
        }

        function getUrlVars() {
            var vars = {};
            var parts = wiandow.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
                vars[key] = value;
            });
            return vars;
        }
        function OnClientBeforeClose(sender, eventArgs) {
            if (!confirm("Bạn có muốn đóng màn hình không?")) {
                //if cancel is clicked prevent the window from closing
                args.set_cancel(true);
            }
        }
        function OpenDialogEmployee(e) {
            var fn = function () {
                var oWnd = $find('<%= popupId %>');
                var grid = $find('<%= rgConcurrently.ClientID%>')
                var empId = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                var empCode = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_CODE');
                oWnd.add_close(OnClientClose);
                oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlEmpDtl&group=Business&gUID=' + empId + '&emp=' + empCode + '&state=Normal');
                oWnd.setSize(screen.width - 50, screen.height - 100);
                oWnd.show();
                Sys.Application.remove_load(fn);
            };
            Sys.Application.add_load(fn);
        }
        function OnClientClose(oWnd, args) {

            $find("<%= rgConcurrently.ClientID %>").get_masterTableView().rebind();
        }
        function OnClientItemsRequesting(sender, eventArgs) {


        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            switch (id) {

                default:
                    break;
            }
        }
        function clearSelectRadcombo(cbo) {
            if (cbo) {
                cbo.clearItems();
                cbo.clearSelection();
                cbo.set_text('');
            }
        }
        function OnClientItemSelectedIndexChanging(sender, args) {
            var item = args.get_item();
            item.set_checked(!item.get_checked());
            args.set_cancel(true);
        }

    </script>
</tlk:RadCodeBlock>
