<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlMngInfoEvalTargetDetail.ascx.vb"
    Inherits="Performance.ctrlMngInfoEvalTargetDetail" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %>
<link href="/Styles/StyleCustom.css" rel="stylesheet" type="text/css" />
<Common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane3" runat="server" Height="35px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMngInfoEvalTarget" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="68px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboYear" SkinID="dDropdownList" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Kỳ đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboPeriod" SkinID="dDropdownList" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                        <td class="lb">
                            <%# Translate("Trạng thái")%>
                        </td>
                        <td>
                            <tlk:RadComboBox ID="cboStatus" SkinID="dDropdownList" runat="server" AutoPostBack="true">
                            </tlk:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb">
                            <asp:Label ID="Label1" runat="server" Text="Từ ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdFromDate" runat="server" DateInput-Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td class="lb">
                            <asp:Label ID="Label2" runat="server" Text="Đến ngày"></asp:Label>
                        </td>
                        <td>
                            <tlk:RadDatePicker ID="rdToDate" runat="server" DateInput-Enabled="false">
                            </tlk:RadDatePicker>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" Text="<%$ Translate: Tìm kiếm %>" SkinID="ButtonFind">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid PageSize="50" ID="rgMngInfoEvalTarget" runat="server" Height="100%"
                    AllowMultiRowSelection="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnRowDblClick="gridRowDblClick" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" FrozenColumnsCount="3" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn DataField="ID" Visible="false" HeaderText="ID" />
                            <tlk:GridBoundColumn HeaderText="ID Chỉ số đo lường" DataField="KPI_ASSESSMENT" Visible="false"
                                ReadOnly="true" UniqueName="KPI_ASSESSMENT" SortExpression="KPI_ASSESSMENT" />
                            <tlk:GridBoundColumn HeaderText="trạng thái" DataField="STATUS_NAME" HeaderStyle-Width="100px"
                                ReadOnly="true" UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Mã NV" DataField="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE"
                                UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="Họ và tên" DataField="EMPLOYEE_NAME" SortExpression="EMPLOYEE_NAME"
                                UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Chức danh" DataField="TITLE_NAME" SortExpression="TITLE_NAME"
                                UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="Phòng ban" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="Chỉ số đo lường" DataField="KPI_ASSESSMENT_TEXT" HeaderStyle-Width="100px"
                                ReadOnly="true" UniqueName="KPI_ASSESSMENT_TEXT" SortExpression="KPI_ASSESSMENT_TEXT" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Loại KPI" DataField="KPI_TYPE" HeaderStyle-Width="100px"
                                ReadOnly="true" UniqueName="KPI_TYPE" SortExpression="KPI_TYPE" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Đơn vị tính" DataField="UNIT_NAME" UniqueName="UNIT_NAME"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="UNIT_NAME" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Tần suất đo" DataField="FREQUENCY_NAME" UniqueName="FREQUENCY_NAME"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="FREQUENCY_NAME" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Công thức tính" DataField="DESCRIPTION" UniqueName="DESCRIPTION"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="DESCRIPTION" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Nguồn đo" DataField="SOURCE_NAME" UniqueName="SOURCE_NAME"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="SOURCE_NAME" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Loại tiêu chí" DataField="GOAL_TYPE_NAME" UniqueName="GOAL_TYPE_NAME"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="GOAL_TYPE_NAME" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Chỉ tiêu" DataField="TARGET" UniqueName="TARGET"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="TARGET" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Ngưỡng" DataField="TARGET_MIN" UniqueName="TARGET_MIN"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="TARGET_MIN" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Trọng số" DataField="RATIO" UniqueName="RATIO"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="RATIO" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Điểm chuẩn" DataField="BENCHMARK" UniqueName="BENCHMARK"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="BENCHMARK" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="NV thực hiện" DataField="EMPLOYEE_ACTUAL" UniqueName="EMPLOYEE_ACTUAL"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="EMPLOYEE_ACTUAL" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridNumericColumn HeaderText="Điểm NV" DataField="EMPLOYEE_POINT" UniqueName="EMPLOYEE_POINT" DataFormatString="{0:N0}"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="EMPLOYEE_POINT" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú" DataField="NOTE" UniqueName="NOTE"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="NOTE" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="QL đánh giá" DataField="DIRECT_ACTUAL" UniqueName="DIRECT_ACTUAL"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="DIRECT_ACTUAL" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridNumericColumn HeaderText="Điểm đánh giá" DataField="DIRECT_POINT" UniqueName="DIRECT_POINT" DataFormatString="{0:N0}"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="DIRECT_POINT" ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridBoundColumn HeaderText="Ghi chú của QLTT" DataField="NOTE_QLTT" UniqueName="NOTE_QLTT"
                                HeaderStyle-Width="75px" ReadOnly="true" SortExpression="NOTE_QLTT" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <HeaderStyle Width="120px" />
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" VisibleStatusbar="false" OnClientClose="popupclose"
            Width="1000" Height="600px" EnableShadow="true" Behaviors="Close, Maximize, Move"
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
        function GridCreated(sender, eventArgs) {
            registerOnfocusOut('RAD_SPLITTER_ctl00_MainContent_ctrlMngInfoEvalTarget_RadSplitter1');
        }

        function gridRowDblClick(sender, eventArgs) {
            var bCheck = CheckValidate();
            var n;
            var m;
            if (bCheck != 1) {
                OpenEdit();
            }
            if (bCheck == 1) {
                m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 10000);
            }

        }
        function clientButtonClicking(sender, args) {
            var bCheck;
            var n;
            var m;
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'EXPORT' || args.get_item().get_commandName() == 'PRINT') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = CheckValidate();
                if (bCheck == 0) {
                    OpenEdit();
                }
                if (bCheck == 1) {
                    m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                        n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    }
                    if (bCheck == 2) {
                        m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                        n = noty({ text: m, dismissQueue: true, type: 'warning' });
                        setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    }
                    args.set_cancel(true);
                }

                if (args.get_item().get_commandName() == "DELETE") {
                    bCheck = CheckValidate();

                    if (bCheck == 1) {
                        m = '<%= Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 10000);
                    args.set_cancel(true);
                }
            }
        }

        function CheckValidate() {
            var bCheck = $find('<%= rgMngInfoEvalTarget.ClientID %>').get_masterTableView().get_selectedItems().length;
            if (bCheck == 0) {
                return 1;
            }
            if (bCheck > 1) {
                return 2;
            }
            return 0;
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

        var enableAjax = true;
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

    </script>
</tlk:RadCodeBlock>
