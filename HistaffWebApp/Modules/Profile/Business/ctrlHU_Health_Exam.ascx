<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Health_Exam.ascx.vb"
    Inherits="Profile.ctrlHU_Health_Exam" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarCommends" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="60px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <asp:Label ID="lbFromDate" runat="server" Text="Từ ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="lbToDate" runat="server" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server">
                            </tlk:RadDatePicker>
                        </td>
                        <td colspan="2">
                            <asp:CheckBox ID="chkChecknghiViec" runat="server" Text="Liệt kê cả nhân viên nghỉ việc" />
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="Tìm" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgCommend" runat="server" AllowMultiRowSelection="True" Height="100%"
                    AllowPaging="true" PageSize="50">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" HeaderText="ID" />
                            <tlk:GridBoundColumn HeaderText="Mã nhân viên" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" HeaderStyle-Width="60px" />
                            <tlk:GridBoundColumn HeaderText="Họ tên nhân viên" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Ban/Phòng" DataField="ORG_NAME"
                                SortExpression="ORG_NAME" UniqueName="ORG_NAME" HeaderStyle-Width="150px" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" HeaderStyle-Width="150px" />

                            <tlk:GridBoundColumn HeaderText="Năm" DataField="YEAR"
                                ItemStyle-HorizontalAlign="Center" SortExpression="YEAR" UniqueName="YEAR" />
                            <tlk:GridDateTimeColumn HeaderText="Ngày hiệu lực" DataField="EFFECT_DATE"
                                ItemStyle-HorizontalAlign="Center" SortExpression="EFFECT_DATE" UniqueName="EFFECT_DATE"
                                DataFormatString="{0:dd/MM/yyyy}" />

                            <tlk:GridBoundColumn HeaderText="Loại sức khỏe" DataField="LOAISK_NAME"
                                ItemStyle-HorizontalAlign="Center" SortExpression="LOAISK_NAME" UniqueName="LOAISK_NAME" />
                            <tlk:GridBoundColumn HeaderText="Nhóm máu" DataField="NHOM_MAU"
                                SortExpression="NHOM_MAU" UniqueName="NHOM_MAU"  />
                            <tlk:GridNumericColumn HeaderText="Nhịp tim" DataField="NHIP_TIM"
                                ItemStyle-HorizontalAlign="Right" SortExpression="NHIP_TIM"
                                UniqueName="NHIP_TIM" />
                            <tlk:GridBoundColumn HeaderText="Huyết áp" DataField="HUYET_AP"
                                SortExpression="HUYET_AP" UniqueName="HUYET_AP" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mắt trái %>" DataField="MAT_TRAI"
                                SortExpression="MAT_TRAI" UniqueName="MAT_TRAI" />
                            <tlk:GridBoundColumn HeaderText="Mắt phải" DataField="MAT_PHAI"
                                SortExpression="MAT_PHAI" UniqueName="MAT_PHAI" />
                            <tlk:GridBoundColumn HeaderText="Chiều cao" DataField="CHIEU_CAO"
                                SortExpression="CHIEU_CAO" UniqueName="CHIEU_CAO" />
                            <tlk:GridBoundColumn HeaderText="Cân nặng" DataField="CAN_NANG" SortExpression="CAN_NANG"
                                UniqueName="CAN_NANG" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE"
                                SortExpression="NOTE" UniqueName="NOTE" HeaderStyle-Width="150px" />
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <%--<ClientEvents OnGridCreated="GridCreated" />--%>
                        <ClientEvents OnCommand="ValidateFilter" />
                    </ClientSettings>
                    <HeaderStyle Width="120px" />
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlUpload ID="ctrlUpload1" runat="server" />
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="popupclose" Height="550px" EnableShadow="true" Behaviors="Close, Maximize, Move"
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

        //        function GridCreated(sender, eventArgs) {
        //            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlHU_Commend_RadSplitter3');
        //        }

        function OpenNew() {
            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CommendNewEdit&group=Business&FormType=0', "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
        }

        function OpenEdit() {
            var bCheck = $find('<%= rgCommend.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0)
                return 0;
            if (bCheck > 1)
                return 1;
            var id = $find('<%= rgCommend.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var id_status = $find('<%= rgCommend.ClientID%>').get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');

            window.open('/Default.aspx?mid=Profile&fid=ctrlHU_CommendNewEdit&group=Business&FormType=1&ID=' + id + '&Status=' + id_status, "_self"); /*
            var pos = $("html").offset();
            oWindow.setSize(1020, $(window).height());
            oWindow.center(); */
            return 2;
        }

        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == "CHECK") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'IMPORT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }

            if (args.get_item().get_commandName() == 'DELETE') {
                bCheck = $find('<%= rgCommend.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                    args.set_cancel(true);
                }

            }
        }

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = OpenEdit();
            if (bCheck == 0) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }
            if (bCheck == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
            }

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
    </script>
</tlk:RadCodeBlock>
