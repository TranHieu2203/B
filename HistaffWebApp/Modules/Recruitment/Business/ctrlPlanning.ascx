<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlPlanning.ascx.vb"
    Inherits="Recruitment.ctrlPlanning" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <Common:ctrlOrganization ID="ctrlOrg" runat="server" />
        <asp:HiddenField runat="server" ID="hidOrg" />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane4" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter1" runat="server" Orientation="Horizontal" Width="100%"
            Height="100%">
            <tlk:RadPane ID="RadPane3" runat="server" Height="33px" Scrolling="None">
                <tlk:RadToolBar ID="tbarMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane1" runat="server" Height="40px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td class="lb">
                            <%# Translate("Năm") %>
                        </td>
                        <td>
                            <tlk:RadNumericTextBox ID="rntxtYear" NumberFormat-DecimalDigits="0" runat="server"
                                CausesValidation="false" AutoPostBack="true" SkinID="Number" ShowSpinButtons="true">
                            </tlk:RadNumericTextBox>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <tlk:RadGrid ID="rgData" runat="server" Height="100%" PageSize="50" AllowPaging="true" SkinID="GridSingleSelect">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,YEAR,STATUS_ID">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên đợt %>" DataField="NAME" UniqueName="NAME"
                                SortExpression="NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng yêu cầu %>" DataField="NUM_REQUEST"
                                UniqueName="NUM_REQUEST" SortExpression="NUM_REQUEST"  ItemStyle-HorizontalAlign="Right" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Lý do tuyển dụng %>" DataField="RC_REASON_NAME"
                                UniqueName="RC_REASON_NAME" SortExpression="RC_REASON_NAME" />
                            <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày tiếp nhận %>" DataField="RECEIVE_DATE"
                                UniqueName="RECEIVE_DATE" SortExpression="RECEIVE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                                ItemStyle-HorizontalAlign="Center" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Chi phí %>" DataField="COST" UniqueName="COST"
                                SortExpression="COST" ItemStyle-HorizontalAlign="Right" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                                SortExpression="REMARK" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái kế hoạch %>" DataField="STATUS_NAME"
                                UniqueName="STATUS_NAME" SortExpression="STATUS_NAME" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane5" runat="server" Height="35px" Scrolling="None">
                <table class="table-form">
                    <tr>
                        <td>
                            <tlk:RadButton runat="server" ID="btnCandidateList" Text="<%$ Translate: Quản lý ứng viên %>"
                                OnClientClicking="btnCandidateList_Click">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton runat="server" ID="btnResult" Text="<%$ Translate: Lịch thi - kết quả %>"
                                OnClientClicking="btnResult_Click">
                            </tlk:RadButton>
                        </td>
                        <td>
                            <tlk:RadButton runat="server" ID="btnCandidatePass" Text="<%$ Translate: Trúng tuyển - tiếp nhận%>"
                                OnClientClicking="btnCandidatePass_Click">
                            </tlk:RadButton>
                        </td>
                    </tr>
                </table>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadWindowManager ID="RadWindowManager1" runat="server">
    <Windows>
        <tlk:RadWindow runat="server" ID="rwPopupChild" VisibleStatusbar="false" Width="950px"
            OnClientClose="popupclose" Height="600px" EnableShadow="true" Behaviors="Close, Maximize"
            Modal="true" ShowContentDuringLoad="false">
        </tlk:RadWindow>
    </Windows>
</tlk:RadWindowManager>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'EDIT') {
                OpenEdit();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenNew();
                args.set_cancel(true);
            }
            if (args.get_item().get_commandName() == 'PRINT_PLAN') {
                enableAjax = false;
            }
            if (args.get_item().get_commandName() == 'PRINT_TN') {
                enableAjax = false;
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function btnResult_Click(sender, eventArgs) {
            window.location.href = "Default.aspx?mid=Recruitment&fid=ctrlCandidateResult&group=Business";
            args.set_cancel(true);
        }

        function btnCandidatePass_Click(sender, eventArgs) {
            window.location.href = "Default.aspx?mid=Recruitment&fid=ctrlCandidateReceive&group=Business";
            args.set_cancel(true);
        }
        function btnCandidateList_Click(sender, args) {
            window.location.href = "Default.aspx?mid=Recruitment&fid=ctrlCandidateList&group=Business";
            args.set_cancel(true);
        }
        function OpenNew() {
            var grid = $find('<%# rgData.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var bCheck = gridSelected.length;
            if (bCheck == 0) {
                return;
            }
            var id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var status = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');
            if (status == 1084) {
                var m = '<%# Translate("Kế hoạch tuyển dụng đã kết thúc") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                $find("<%= rgData.ClientID %>").get_masterTableView().rebind();
                return;
            }
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlCanDtl&group=Business&plan=' + id, "rwPopupChild");
            var pos = $("html").offset();
            oWindow.moveTo(pos.left, pos.top);
            oWindow.setSize($(window).width() - 30, $(window).height() - 30); oWindow.center(); */

        }

        function OpenEdit() {
            var grid = $find('<%# rgData.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var bCheck = gridSelected.length;
            if (bCheck == 0) {
                return;
            }
            var id = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ID');
            var year = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('YEAR');
            var status = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('STATUS_ID');

            window.open('/Default.aspx?mid=Recruitment&fid=ctrlPlanningNewEdit&group=Business&id=' + id + '&year=' + year + '&status=' + status, "rwPopupChild");


        }

        function popupclose(sender, args) {
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
