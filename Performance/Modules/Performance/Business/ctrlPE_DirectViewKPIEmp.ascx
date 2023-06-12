<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPE_DirectViewKPIEmp.ascx.vb"
    Inherits="Performance.ctrlPE_DirectViewKPIEmp" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="clientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane4" runat="server" Height="33px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm đánh giá")%>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntYear" runat="server" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSeparator="" ShowSpinButtons="true" Width="70">
                            </tlk:RadNumericTextBox>
                        </td>
                        <td>
                            <tlk:RadButton ID="btnSearch" runat="server" SkinID="ButtonFind" CausesValidation="false"
                                Text="<%$ Translate: Tìm %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                   <MasterTableView DataKeyNames="ID,PE_PERIO_ID,OBJECT_GROUP_ID,EMPLOYEE_ID" ClientDataKeyNames="ID,PE_PERIO_ID,OBJECT_GROUP_ID,EMPLOYEE_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE"
                                SortExpression="EMPLOYEE_CODE" UniqueName="EMPLOYEE_CODE" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Họ và tên %>" DataField="EMPLOYEE_NAME"
                                SortExpression="EMPLOYEE_NAME" UniqueName="EMPLOYEE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" SortExpression="ORG_NAME"
                                UniqueName="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                SortExpression="TITLE_NAME" UniqueName="TITLE_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Năm đánh giá %>" DataField="PE_PERIO_YEAR"
                                UniqueName="PE_PERIO_YEAR" SortExpression="PE_PERIO_YEAR" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kỳ đánh giá %>" DataField="PE_PERIO_NAME"
                                UniqueName="PE_PERIO_NAME" SortExpression="PE_PERIO_NAME" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Kiểu đánh giá %>" DataField="PE_PERIO_TYPE_ASS_NAME"
                                UniqueName="PE_PERIO_TYPE_ASS_NAME" SortExpression="PE_PERIO_TYPE_ASS_NAME" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Từ ngày %>" DataField="PE_PERIO_START_DATE"
                                UniqueName="PE_PERIO_START_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PE_PERIO_START_DATE"
                                ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đến ngày %>" DataField="PE_PERIO_END_DATE"
                                UniqueName="PE_PERIO_END_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PE_PERIO_END_DATE"
                                ShowFilterIcon="true" CurrentFilterFunction="EqualTo">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng điểm đánh giá NV %>" DataField="RESULT_CONVERT_NV"
                                UniqueName="RESULT_CONVERT_NV" SortExpression="RESULT_CONVERT_NV" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp hạng NV %>" DataField="CLASSIFICATION_NAME_NV"
                                UniqueName="CLASSIFICATION_NAME_NV" SortExpression="CLASSIFICATION_NAME_NV" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tổng điểm đánh giá %>" DataField="RESULT_CONVERT"
                                UniqueName="RESULT_CONVERT" SortExpression="RESULT_CONVERT" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Xếp hạng %>" DataField="CLASSIFICATION_NAME"
                                UniqueName="CLASSIFICATION_NAME" SortExpression="CLASSIFICATION_NAME" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="PE_STATUS_NAME"
                                UniqueName="PE_STATUS_NAME" SortExpression="PE_STATUS_NAME" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" FilterControlWidth="100%">
                                <HeaderStyle HorizontalAlign="Center" />
                            </tlk:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopup" Width="800px" VisibleStatusbar="false"
            OnClientClose="OnClientClose" Height="500px" EnableShadow="true" Behaviors="Close, Maximize, Move"
            Modal="true" ShowContentDuringLoad="false">
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


        function clientButtonClicking(sender, args) {
            var m;
            var bCheck;
            var n;
            if (args.get_item().get_commandName() == 'CREATE') {
                //                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "EDIT") {
                bCheck = OpenEdit();
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
                if (bCheck == 1) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW) %>';
                    n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }

                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == "PRINT" || args.get_item().get_commandName() == "EXPORT") {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'DELETE' ||
                args.get_item().get_commandName() == 'APROVE' ||
                args.get_item().get_commandName() == 'REJECT') {
                bCheck = $find('<%# rgData.ClientID %>').get_masterTableView().get_selectedItems().length;
                if (bCheck == 0) {
                    m = '<%# Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW) %>';
                    var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                    setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                }
            }
        }

        function OnClientClose(oWnd, args) {
            var m;
            var arg = args.get_argument();
            if (arg == '1') {
                m = '<%# Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS) %>';
                var n = noty({ text: m, dismissQueue: true, type: 'success' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
            }
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
