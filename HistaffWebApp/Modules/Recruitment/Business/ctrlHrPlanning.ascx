<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHrPlanning.ascx.vb"
    Inherits="Recruitment.ctrlHrPlanning" %>
<%@ Import Namespace="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="LeftPane" runat="server" MinWidth="200" Width="250px" Scrolling="None">
        <common:ctrlorganization id="ctrlOrg" runat="server" />
        <asp:HiddenField runat="server" ID="hidOrg" />
        <asp:HiddenField runat="server" ID="hidIsDissolve" />
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
                <tlk:RadGrid ID="rgData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Height="100%" AllowSorting="True" AllowMultiRowSelection="true" AllowPaging="true" PageSize="50">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ID" ClientDataKeyNames="ID,ORG_ID,TITLE_ID,YEAR">
                        <Columns>
                            <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            </tlk:GridClientSelectColumn>
                            <tlk:GridBoundColumn DataField="ID" Visible="false" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Đơn vị %>" DataField="ORG_NAME" UniqueName="ORG_NAME"
                                SortExpression="ORG_NAME" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Chức danh %>" DataField="TITLE_NAME"
                                UniqueName="TITLE_NAME" SortExpression="TITLE_NAME" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng nhân sự hiện tại %>" DataField="HR_CURRENT"
                                UniqueName="HR_CURRENT" SortExpression="HR_CURRENT" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Định biên đầu năm %>" DataField="HR_BEGIN_YEAR"
                                UniqueName="HR_BEGIN_YEAR" SortExpression="HR_BEGIN_YEAR" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Định biên xin thêm %>" DataField="HR_ADD"
                                UniqueName="HR_ADD" SortExpression="HR_ADD" />
                            <tlk:GridNumericColumn HeaderText="<%$ Translate: Số lượng cần tuyển %>" DataField="NUM_REQUIRED"
                                UniqueName="NUM_REQUIRED" SortExpression="NUM_REQUIRED" />
                            <tlk:GridBoundColumn HeaderText="<%$ Translate: Ghi chú %>" DataField="REMARK" UniqueName="REMARK"
                                SortExpression="REMARK" />
                        </Columns>
                    </MasterTableView>
                </tlk:RadGrid>
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
<common:ctrlmessagebox id="ctrlMessageBox" runat="server" />
<asp:PlaceHolder ID="phPopup" runat="server"></asp:PlaceHolder>
<common:ctrlupload id="ctrlUpload1" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            if (args.get_item().get_commandName() == 'CREATE') {
                OpenEdit();
                args.set_cancel(true);
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        function OpenEdit() {
            var grid = $find('<%# rgData.ClientID %>');
            var gridSelected = grid.get_masterTableView().get_selectedItems();
            var bCheck = gridSelected.length;
            if (bCheck == 0) {
                return;
            }
            var orgID = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('ORG_ID');
            var titleID = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('TITLE_ID');
            var year = grid.get_masterTableView().get_selectedItems()[0].getDataKeyValue('YEAR');
            if (year == null) {
                m = '<%# Translate("Chức danh chưa có định biên") %>';
                var n = noty({ text: m, dismissQueue: true, type: 'warning' });
                setTimeout(function () { $.noty.close(n.options.id); }, 5000);
                return;
            }
            window.open('/Default.aspx?mid=Recruitment&fid=ctrlPlanningNewEdit&group=Business&orgID=' + orgID + '&titleID=' + titleID + '&year=' + year, "rwPopupChild");


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
