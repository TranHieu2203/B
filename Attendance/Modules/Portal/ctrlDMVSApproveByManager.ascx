<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDMVSApproveByManager.ascx.vb"
    Inherits="Attendance.ctrlDMVSApproveByManager
    " %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidValid" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" >
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMainToolBar" runat="server" Width="100%" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbYear" Text='<%# Translate("Năm")%>'></asp:Label>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="txtYear" runat="server" SkinID="Number" NumberFormat-GroupSeparator=""
                                ShowSpinButtons="true" MaxLength="4" MinValue="2000" NumberFormat-DecimalDigits="0"
                                Width="60px">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td class="lb">
                            <asp:Label runat="server" ID="lbStatus" Text='<%# Translate("Trạng thái")%>'></asp:Label>
                        </td>
                        <td>
                            <tlk:RadComboBox runat="server" ID="cboStatus">
                            </tlk:RadComboBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgMain" runat="server" Height="100%" Width="100%" AllowPaging="true"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID,ID_REGGROUP,ID_EMPLOYEE,WORKINGDAY,STATUS,NOTE"
                        ClientDataKeyNames="ID,ID_REGGROUP,ID_EMPLOYEE,WORKINGDAY,STATUS,NOTE">
                        <Columns>
                            <%--<tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ tên %>" DataField="VN_FULLNAME"
                                UniqueName="VN_FULLNAME" SortExpression="VN_FULLNAME" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh%>" DataField="JOBTITLE"
                                UniqueName="JOBTITLE" SortExpression="JOBTITLE" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Phòng ban %>" DataField="DEPARTMENT"
                                UniqueName="DEPARTMENT" SortExpression="DEPARTMENT" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridBoundColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày đăng ký %>"
                                DataField="WORKINGDAY" UniqueName="WORKINGDAY" SortExpression="WORKINGDAY" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                <HeaderStyle Width="100px" />
                            </tlk:GridDateTimeColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại thông tin %>" HeaderStyle-Width="200px"
                                DataField="REGIST_INFO_NAME" SortExpression="REGIST_INFO_NAME" UniqueName="REGIST_INFO_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Loại đăng ký %>" DataField="SIGN_NAME"
                                SortExpression="SIGN_NAME" UniqueName="SIGN_NAME" HeaderStyle-Width="100px" />

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Từ giờ %>" DataField="FROM_HOUR" HeaderStyle-Width="70px"
                                AllowFiltering="false" UniqueName="FROM_HOUR" DataFormatString="{0:HH:mm}" SortExpression="FROM_HOUR">
                            </tlk:GridDateTimeColumn>

                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Đến giờ %>" DataField="TO_HOUR" HeaderStyle-Width="70px"
                                AllowFiltering="false" UniqueName="TO_HOUR" DataFormatString="{0:HH:mm}" SortExpression="TO_HOUR">
                            </tlk:GridDateTimeColumn>

                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số phút %>" DataField="MINUTE" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px"
                                DataFormatString="{0:n0}" SortExpression="MINUTE" UniqueName="MINUTE">
                            </tlk:GridNumericColumn>

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do %>" DataField="NOTE" SortExpression="NOTE" HeaderStyle-Width="100px"
                                UniqueName="NOTE" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị checkin %>" DataField="ORG_CHECK_IN_NAME" SortExpression="ORG_CHECK_IN_NAME" HeaderStyle-Width="100px"
                                UniqueName="ORG_CHECK_IN_NAME" />

                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do không phê duyệt%>" DataField="REASON_NOT_AGREE" SortExpression="REASON_NOT_AGREE" HeaderStyle-Width="100px"
                                UniqueName="REASON_NOT_AGREE" />--%>

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
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlCommon_Reject ID="ctrlCommon_Reject" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function clientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgMain.ClientID %>").get_masterTableView().rebind();
            }
        }

        //mandatory for the RadWindow dialogs functionality
        function getRadWindow() {
            if (window.radWindow) {
                return window.radWindow;
            }
            if (window.frameElement && window.frameElement.radWindow) {
                return window.frameElement.radWindow;
            }
            return null;
        }

        function gridRowDblClick(sender, args) {
            debugger;
            var bCheck = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                //args.set_cancel(true);
            }
            else if (bCheck > 1) {
                var m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                //args.set_cancel(true);
            }
            else {

                var id = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
                var empId = $find('<%= rgMain.ClientID %>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('EMPLOYEE_ID');
                OpenInNewTab('Default.aspx?mid=Attendance&fid=ctrlLeaveRegistrationNewEdit&id=' + id + '&typeUser=LM&empId=' + empId + '&idCtrl=' + idCtrl);
                //args.set_cancel(true);
            }
        }

        function OpenInNewTab(url) {
            window.location.href = url;
        }
        var winH;
        var winW;

         function SizeToFitMain() {
            Sys.Application.remove_load(SizeToFitMain);
            winH = $(window).height() - 210;
            winW = $(window).width() - 90;
            $("#ctl00_MainContent_ctrlDMVSApproveByManager_RadSplitter1").stop().animate({ height: winH, width: winW }, 0);
            $("#ctl00_MainContent_ctrlDMVSApproveByManager_RadSplitter3").stop().animate({ height: winH, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMVSApproveByManager_MainPane").stop().animate({ height: winH, width: winW }, 0);
           $("#ctl00_MainContent_ctrlDMVSApproveByManager_rgMain").stop().animate({ height: winH-76, width: winW }, 0);
            $("#RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlDMVSApproveByManager_RadPane2").stop().animate({ height: winH, width: winW }, 0);
            
            Sys.Application.add_load(SizeToFitMain);
        }

        SizeToFitMain();

        $(document).ready(function () {
            SizeToFitMain();
        });
        $(window).resize(function () {
            SizeToFitMain();
        });
    </script>
</tlk:RadCodeBlock>
